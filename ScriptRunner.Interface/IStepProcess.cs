using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public interface IStepProcess
    {
        event BeforeStepExecutionHandler BeforeStepExecution;
        event AfterStepExecutionHandler AfterStepExecution;
        event OnScriptFinishedHandler Completed;
    }
}
