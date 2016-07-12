using ScriptRunner.Interface.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ScriptRunner.Interface.Exceptions;

namespace ScriptRunner.Interface
{

    public delegate void BeforeStepExecutionHandler(StepAttribute step);
    public delegate void AfterStepExecutionHandler(StepAttribute step);
    public delegate void OnScriptFinishedHandler();
    public delegate void OnExecuteErrorHandler(object sender, StepAttribute step,Exception ex);
    public class ScriptEngine<TScript,TScriptData>:ScriptEngine<TScript,TScriptData, Checkpoint>
        where TScript : ScriptBase<TScriptData>, new()
    {

    }
    public class ScriptEngine<TScript,TScriptData,TCheckpoint>:ScriptEngine<TScript, TScriptData, TCheckpoint, ProgressInfo> 
        where TScript: ScriptBase<TScriptData,TCheckpoint,ProgressInfo>,new() 
        where TCheckpoint:class
    {
        
    }
    public class ScriptEngine<TScript,TScriptData,TCheckpoint,TStepProgress>:IScriptEngine<TScript,TScriptData,TCheckpoint,TStepProgress> 
        where TScript: ScriptBase<TScriptData,TCheckpoint,TStepProgress>,new() 
        where TCheckpoint:class
    {
        public event BeforeStepExecutionHandler BeforeStepExecution;
        public event AfterStepExecutionHandler AfterStepExecution;
        public event OnScriptFinishedHandler Completed;
        public event OnExecuteErrorHandler OnExecuteError;

        private Dictionary<int, Tuple<StepAttribute, MethodInfo>> _stepDic;

        private CancellationTokenSource source = new CancellationTokenSource();

        private ScriptBase<TScriptData,TCheckpoint, TStepProgress> _obj = null;

        public ScriptEngine() {
            this._obj = new TScript();
        }

        public ScriptEngine(ScriptBase<TScriptData, TCheckpoint, TStepProgress> Script) {
            this._obj = Script;
        }  
        

        public Progress<TStepProgress> StepProgress { get; } = new Progress<TStepProgress>();



        public string Run(object data) {
            return Run((TScriptData)data);
        }

        public void Cancel() {
            source.Cancel();
        }
       
        public string Run(TScriptData data) {
            return Run(data, null);
        }
       

        public string Run(TScriptData data,int? stepId = null) {
            if (_stepDic == null)
                addMethod(stepId);
            
            _obj.Initial(data,StepProgress);
           

            var steps = _stepDic.OrderBy(o => o.Key).ToList();

            int skipToStep = steps.First().Key;

            for(int i = 0; i < steps.Count; i++) {
                _obj.SetStepCheckPoint(steps[i].Value.Item1);
                if (skipToStep == steps[i].Key) {
                    BeforeStepExecution?.Invoke(steps[i].Value.Item1);
                    try {
                        steps[i].Value.Item2.Invoke(_obj, null);

                        if (i < steps.Count-1)
                            skipToStep = steps[i + 1].Key;

                    }

                    catch (TargetInvocationException ex) {
                        if (ex.InnerException is BreakException)
                            return (ex.InnerException as BreakException).Message;

                        if (ex.InnerException is SkipException) {
                            skipToStep = (ex.InnerException as SkipException).StepId;
                            continue;
                        }

                        OnExecuteError?.Invoke(_obj, steps[i].Value.Item1, ex.InnerException);
                        return ex.InnerException?.Message;
                    }
                    AfterStepExecution?.Invoke(steps[i].Value.Item1);
                }


                
            }

            //foreach (var item in _stepDic.OrderBy(o => o.Key)) {
            //    BeforeStepExecution?.Invoke(item.Value.Item1);
            //    try {
            //        item.Value.Item2.Invoke(_obj, null);
            //    }
               
            //    catch (TargetInvocationException ex) {
            //        if (ex.InnerException is BreakException)
            //            return ex.Message;
            //        throw ex.InnerException;
            //    }
            //    AfterStepExecution?.Invoke(item.Value.Item1);
            //}
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

        private void addMethod(int? stepId = null) {
            _stepDic = new Dictionary<int, Tuple<StepAttribute, MethodInfo>>();

            var methods = _obj.GetType().GetMethods();
            foreach (var method in _obj.GetType().GetMethods().Where(m => m.IsPublic)) {
                var stepAttr = method.GetCustomAttribute<StepAttribute>(true);
                if (stepAttr != null && method.GetParameters().Count() == 0) {
                    if(stepId == null) {
                        var tempItem = new Tuple<StepAttribute, MethodInfo>(stepAttr, method);
                        _stepDic.Add(stepAttr.Id, tempItem);
                    }else {
                        if(stepAttr.Id >= stepId) {
                            _stepDic.Add(stepAttr.Id, new Tuple<StepAttribute, MethodInfo>(stepAttr, method));
                        }
                    }
                    
                }
            }
        }

       
    }

}
