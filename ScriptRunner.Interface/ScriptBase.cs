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

        protected TScriptModel _data;

        public void SetStepReport(IProgress<TStepProgress> Reporter) {
            this._stepReporter = Reporter;
        }

        public void SetInputData(TScriptModel data) {
            this._data = data;
        }
    }

    public abstract class ScriptBase<TScriptModel>:ScriptBase<TScriptModel,ProgressInfo>
    {
        
    }




}
