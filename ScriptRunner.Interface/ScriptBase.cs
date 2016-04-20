using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    

    public abstract class ScriptBase<TScriptModel,TStepProgress> 
    {
        protected IProgress<TStepProgress> _stepReporter;

        public void SetStepReport(IProgress<TStepProgress> Reporter) {
            this._stepReporter = Reporter;
        }

        public abstract void SetInputData(TScriptModel data);
    }

    public abstract class ScriptBase<TScriptModel>:ScriptBase<TScriptModel,ProgressInfo>
    {
        
    }




}
