using ScriptRunner.Interface.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public abstract class ScriptBase<TScriptData,TStepCheckpoint,TStepProgress>
    {
        protected IProgress<TStepProgress> _stepReporter;

        protected TScriptData _data;

        protected List<StepCheckpoint<TStepCheckpoint>> _stepCheckpoints;

        public List<StepCheckpoint<TStepCheckpoint>> Checkpoints { get { return _stepCheckpoints; } }

        protected StepCheckpoint<TStepCheckpoint> _currentStepCheckpoint;

        public virtual void Initial(TScriptData data,IProgress<TStepProgress> StepReporter) {
            this._data = data;
            this._stepReporter = StepReporter;
            _stepCheckpoints = new List<StepCheckpoint<TStepCheckpoint>>();
        }

        public void SetStepCheckPoint(StepAttribute Step) {
            _currentStepCheckpoint = new StepCheckpoint<TStepCheckpoint>(Step);
            _stepCheckpoints.Add(_currentStepCheckpoint);
        }

        protected void SkipTo(int id) {
            throw new SkipException(id);
        }

        protected void Return(string Msg) {
            throw new BreakException(Msg);
        }

    }

    public abstract class ScriptBase<TScriptData,TCheckpoint> :ScriptBase<TScriptData, TCheckpoint,ProgressInfo>
    {
        
    }

    public abstract class ScriptBase<TScriptData> : ScriptBase<TScriptData, Checkpoint>
    {

    }


    public class SkipException:Exception
    {
        public int StepId { get; set; }

        public SkipException(int Id) {
            this.StepId = Id;
        }
    }


    public class Checkpoint
    {
        public string Name { get; set; }

        public object ExpectedValue { get; set; }

        public object ActualValue { get; set; }

        public bool IsPass { get; set; }
    }

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
