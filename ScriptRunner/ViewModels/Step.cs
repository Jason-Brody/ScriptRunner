using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptRunner.Interface;

namespace ScriptRunner.ViewModels
{
    

    public class Step:WPFNotify
    {
        public Step() { }

        public Step(StepMarshal s)
        {
            this.Id = s.Id;
            this.Name = s.Name;
            this.Description = s.Description;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        private bool _isComplete;

        public bool IsComplete
        {
            get { return _isComplete; }
            set {
                StepProgress.IsIndeterMine = false;
                IsRunning = false;
                StepProgress.Current = 0;
                StepProgress.Total = 0;
                SetProperty(ref _isComplete, value);
            }
        }

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }

        public StepProgress StepProgress { get; } = new StepProgress();
    }

    public class StepMarshal : MarshalByRefObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
