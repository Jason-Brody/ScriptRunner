using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public class ScriptTaskInfo
    {
        public string Location { get; set; }

        public string ScriptType { get; set; }

        public string JsonData { get; set; }

        public int Index { get; set; }

        public DataMode Mode { get; set; }
    }

    public enum DataMode
    {
        Signal = 0,
        All = 1
    }
}
