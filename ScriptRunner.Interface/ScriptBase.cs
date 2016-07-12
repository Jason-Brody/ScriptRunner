using ScriptRunner.Interface.Attributes;
using ScriptRunner.Interface.Exceptions;
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





   

   

}
