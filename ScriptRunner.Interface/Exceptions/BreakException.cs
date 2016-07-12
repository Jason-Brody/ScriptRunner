using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface.Exceptions
{
    public sealed class BreakException : Exception
    {
        public BreakException(string Msg) : base(Msg) {

        }
    }
}
