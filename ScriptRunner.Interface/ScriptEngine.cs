using ScriptRunner.Interface.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{

    public delegate void BeforeStepExecutionHandler(StepAttribute step);
    public delegate void AfterStepExecutionHandler(StepAttribute step);
    public delegate void OnScriptFinishedHandler();


    public interface IStepProcess
    {
        event BeforeStepExecutionHandler BeforeStepExecution;
        event AfterStepExecutionHandler AfterStepExecution;
        event OnScriptFinishedHandler Completed;
    }

    public interface IScriptEngine:IStepProcess
    {
        string Run(object data);
    }

    public interface IScriptEngine<TScript,TScriptModel,TStepProgress>:IScriptEngine where TScript:ScriptBase<TScriptModel, TStepProgress>
    {
        string Run(TScriptModel data);

        Progress<TStepProgress> StepProgress { get; }
    }

    public class ScriptEngine<TScript,TScriptModel>:ScriptEngine<TScript,TScriptModel,ProgressInfo> where TScript: ScriptBase<TScriptModel,ProgressInfo>,new()
    {
        
    }


    public class ScriptEngine<TScript,TScriptModel,TStepProgress>:IScriptEngine<TScript,TScriptModel,TStepProgress> where TScript: ScriptBase<TScriptModel,TStepProgress>,new()
    {
        public event BeforeStepExecutionHandler BeforeStepExecution;
        public event AfterStepExecutionHandler AfterStepExecution;
        public event OnScriptFinishedHandler Completed;

        private Dictionary<int, Tuple<StepAttribute, MethodInfo>> _stepDic;


        private ScriptBase<TScriptModel,TStepProgress> _obj = null;

        public ScriptEngine() {
            this._obj = new TScript();
        }

        public ScriptEngine(ScriptBase<TScriptModel, TStepProgress> Script) {
            this._obj = Script;
        }  
        

        public Progress<TStepProgress> StepProgress { get; } = new Progress<TStepProgress>();



        public string Run(object data) {
            return Run((TScriptModel)data);
        }


        public string Run(TScriptModel data) {
            if (_stepDic == null)
                addMethod();
            _obj.SetInputData(data);
            _obj.Initial();
            _obj.SetStepReport(StepProgress);

            foreach (var item in _stepDic.OrderBy(o => o.Key)) {
                BeforeStepExecution?.Invoke(item.Value.Item1);
                try {
                    item.Value.Item2.Invoke(_obj, null);
                }
               
                catch (TargetInvocationException ex) {
                    if (ex.InnerException is BreakException)
                        return ex.Message;
                    throw ex.InnerException;
                }
                AfterStepExecution?.Invoke(item.Value.Item1);
            }
            Completed?.Invoke();
            return null;
        }

        //public void Run(TScriptModel data, int stepNum) {
        //    if (_stepDic == null)
        //        addMethod();

        //    _obj.SetInputData(data);
        //    _obj.SetStepReport(StepProgress);

        //    BeforeStepExecution?.Invoke(_stepDic[stepNum].Item1);
        //    _stepDic[stepNum].Item2.Invoke(_obj, null);
        //    AfterStepExecution?.Invoke(_stepDic[stepNum].Item1);
        //}

        private void addMethod() {
            _stepDic = new Dictionary<int, Tuple<StepAttribute, MethodInfo>>();

            var methods = _obj.GetType().GetMethods();
            foreach (var method in _obj.GetType().GetMethods().Where(m => m.IsPublic)) {
                var stepAttr = method.GetCustomAttribute<StepAttribute>(true);
                if (stepAttr != null && method.GetParameters().Count() == 0) {
                    _stepDic.Add(stepAttr.Id, new Tuple<StepAttribute, MethodInfo>(stepAttr, method));
                }
            }
        }

       
    }

    public sealed class BreakException : Exception
    {
        public BreakException(string Msg) : base(Msg) {

        }
    }


    

}
