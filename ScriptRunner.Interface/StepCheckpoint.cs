using ScriptRunner.Interface.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public class StepCheckpoint : StepCheckpoint<Checkpoint>
    {
        public StepCheckpoint(StepAttribute step) : base(step) {
        }
    }

    public class StepCheckpoint<T>
    {
        private List<T> _checkPoints;

        private StepAttribute _step;

        public StepCheckpoint(StepAttribute step) {
            _step = step;
            _checkPoints = new List<T>();
        }

        public StepAttribute Step { get { return _step; } }

        public List<T> Checkpoints { get { return _checkPoints; } }
    }
}
