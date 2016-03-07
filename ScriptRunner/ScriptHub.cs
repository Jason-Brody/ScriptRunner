using Microsoft.AspNet.SignalR;
using ScriptRunner.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner
{
    public class ScriptHub:Hub<IScriptClient>
    {
        public void Connect(string id)
        {
            var obj = ScriptRunnerManager.ScriptTasks.Where(s => s.Id == id).First();
            Clients.Caller.RunScript(obj.TaskData);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }

}
