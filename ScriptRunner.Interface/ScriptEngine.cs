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

    public interface IScriptEngine
    {
        event BeforeStepExecutionHandler BeforeStepExecution;
        event AfterStepExecutionHandler AfterStepExecution;

        void Run(object data);

        void Run(object data, int StepNum);

        Progress<ProgressInfo> MyProgress { get; }
    }

    
    public class ScriptEngine<T>: IScriptEngine where T :class,new()
    {
        public event BeforeStepExecutionHandler BeforeStepExecution;
        public event AfterStepExecutionHandler AfterStepExecution;

        private Dictionary<int, Tuple<StepAttribute, MethodInfo>> _stepDic;

        private IScriptRunner<T> _obj = null;

        public ScriptEngine(IScriptRunner<T> obj)
        {
            this._obj = obj;
        }

        public Progress<ProgressInfo> MyProgress { get; } = new Progress<ProgressInfo>();

        private void beforeStepExecution(StepAttribute step)
        {
            if (BeforeStepExecution != null)
                BeforeStepExecution(step);
        }

        private void afterStepExecution(StepAttribute step)
        {
            if (AfterStepExecution != null)
                AfterStepExecution(step);
        }

        public void Run(T data)
        {
            if (_stepDic == null)
                addMethod();

            
            _obj.SetInputData(data, MyProgress);


            foreach (var item in _stepDic.OrderBy(o => o.Key))
            {
                beforeStepExecution(item.Value.Item1);
                item.Value.Item2.Invoke(_obj, null);
                afterStepExecution(item.Value.Item1);
            }
        }



        public void Run(T data, int stepNum)
        {
            if (_stepDic == null)
                addMethod();

            _obj.SetInputData(data, new Progress<ProgressInfo>());

            beforeStepExecution(_stepDic[stepNum].Item1);
            _stepDic[stepNum].Item2.Invoke(_obj, null);
            afterStepExecution(_stepDic[stepNum].Item1);
        }

        private void addMethod()
        {
            _stepDic = new Dictionary<int, Tuple<StepAttribute, MethodInfo>>();
           
            var methods = _obj.GetType().GetMethods();
            foreach (var method in _obj.GetType().GetMethods().Where(m => m.IsPublic))
            {
                var stepAttr = method.GetCustomAttribute<StepAttribute>(true);
                if (stepAttr != null && method.GetParameters().Count()==0)
                {
                    _stepDic.Add(stepAttr.Id, new Tuple<StepAttribute, MethodInfo>(stepAttr, method));
                }
            }
        }

        public void Run(object data)
        {
            Run(data as T);
        }

        public void Run(object data, int StepNum)
        {
            Run(data as T, StepNum);
        }
    }
}
