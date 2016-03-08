using Microsoft.AspNet.SignalR;
using ScriptRunner.Interface;
using ScriptRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner
{
    public class ScriptHub:Hub<IScriptClient>
    {
        public void ReadyForTask(string ClientId)
        {
            ScriptTaskInfo sti = new ScriptTaskInfo()
            {
                Location = ScriptRunnerManager.CurrentScript.Location,
                ScriptType = ScriptRunnerManager.CurrentScript.TargetClass,

            };

            

            //var obj = ScriptRunnerManager.ScriptTasks.Where(s => s.ClientId == ClientId).First();
            Clients.Caller.RunScript(sti);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public void ReportProgress(string Id,ProcessInfo p)
        {

        }

        public void Complete(string ClientId)
        {
            var scriptTask = ScriptRunnerManager.ScriptTasks.Where(s => s.ClientId == ClientId).FirstOrDefault();
            if (scriptTask != null)
                ScriptRunnerManager.ScriptTasks.Remove(scriptTask);
            Clients.Caller.Complete();
        }
    }

}
