using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public class Checkpoint
    {
        public string Name { get; set; }

        public object ExpectedValue { get; set; }

        public object ActualValue { get; set; }

        public bool IsPass { get; set; }
    }
}
