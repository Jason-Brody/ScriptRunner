using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public interface IScriptEngine : IStepProcess
    {
        string Run(object data);
    }

    public interface IScriptEngine<TStepProgress> : IScriptEngine
    {
        Progress<TStepProgress> StepProgress { get; }
    }

    public interface IScriptEngine<TScript, TScriptData, TCheckpoint, TStepProgress> : IScriptEngine<TStepProgress>
        where TScript : ScriptBase<TScriptData, TCheckpoint, TStepProgress>
        where TCheckpoint : class
    {
        string Run(TScriptData data);

       
    }
}
