using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public interface IScriptRunner<T>  where T :class,new()
    {
        void SetInputData(T data, IProgress<ProgressInfo> MyProgress);
    }

    public interface IScriptData<T>:IScriptRunner<T> where T :class,new()
    {
        T GetSampleData();
    }
}
