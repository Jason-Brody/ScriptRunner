using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface.Exceptions
{
    public class SkipException : Exception
    {
        public int StepId { get; set; }

        public SkipException(int Id) {
            this.StepId = Id;
        }
    }
}
