using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public interface IScriptRunner<T,T1>  where T :class,new()
    {
        void SetInputData(T data, IProgress<T1> MyProgress);
    }

    public interface IScriptRunner<T> : IScriptRunner<T,ProgressInfo> where T:class,new()
    {

    }
}
