using ScriptRunner.Interface.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    

    public class ScriptEngine<T> where T :class,new()
    {
        private Dictionary<int, Tuple<StepAttribute, MethodInfo>> _stepDic;

        private IScriptRunner<T> _obj = null;

        public ScriptEngine(IScriptRunner<T> obj)
        {
            this._obj = obj;
        }

        public void Run(T data)
        {
            if (_stepDic == null)
                addMethod();

            
            _obj.SetInputData(data, new Progress<ProcessInfo>());


            foreach (var item in _stepDic.OrderBy(o => o.Key))
            {
                item.Value.Item2.Invoke(_obj, null);
            }
        }

        public void Run(T data, int stepNum)
        {
            if (_stepDic == null)
                addMethod();

            _obj.SetInputData(data, new Progress<ProcessInfo>());

            _stepDic[stepNum].Item2.Invoke(_obj, null);
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
    }
}
