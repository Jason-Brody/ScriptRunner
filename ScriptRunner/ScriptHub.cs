using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using ScriptRunner.Interface;
using ScriptRunner.Models;
using ScriptRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ScriptRunner
{
    public class ScriptHub : Hub<IScriptClient>
    {
        
        private string getGUID()
        {
            return Context.QueryString["guid"];
        }

        public ScriptTaskInfo ReadyForTask()
        {
            var guid = getGUID();
            var obj = ScriptRunnerManager.Robots.Find(s => s.Guid == guid);
            return obj.TaskData;
        }

        public override Task OnConnected()
        {
            var guid = getGUID();
            var robot = ScriptRunnerManager.Robots.Find(s => s.Guid == guid);
            robot.ConnectionId = Context.ConnectionId;
            return base.OnConnected();
        }

        public void ReportProgress(int stepId,ProgressInfo p)
        {
           
            var step = findStep( stepId);
            step.StepProgress.Current = p.Current;
            step.StepProgress.Total = p.Total;
            step.StepProgress.Msg = p.Msg;
            step.StepProgress.IsIndeterMine = !p.IsProgressKnow;
        }

        public void BeforeStep(int stepId)
        {
            var guid = getGUID();
            var step = findStep(stepId);
            step.IsRunning = true;
        }

        public void AfterStep(int stepId)
        {
           
            var step = findStep(stepId);
            step.IsComplete = true;
            
        }

        public void Complete(string JsonString)
        {
            var guid = getGUID();
            var script = findScript();
            var data = JsonConvert.DeserializeObject<IDictionary<int,System.Dynamic.ExpandoObject>>(JsonString);
            script.SetData(data);
            var scriptTask = ScriptRunnerManager.Robots.Find(s => s.Guid == guid);
            if (scriptTask != null)
                ScriptRunnerManager.Robots.Remove(scriptTask);
        }

        private Step findStep(int StepId)
        {
            var script = findScript();
            return script.Steps.Find(s => s.Id == StepId);
        }

        private Script findScript()
        {
            var guid = getGUID();
            var obj = ScriptRunnerManager.Robots.Find(s => s.Guid == guid);
            var script = ScriptRunnerManager.Scripts.Where(s => s.Location == obj.TaskData.Location && s.TargetClass == obj.TaskData.ScriptType).First();
            return script;
        }
    }

}
