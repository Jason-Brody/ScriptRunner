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
            set { SetProperty(ref _isComplete, value); }
        }
    }

    public class StepMarshal : MarshalByRefObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
