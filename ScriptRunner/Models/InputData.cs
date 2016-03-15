using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Models
{
    public class InputData
    {
        public InputData() { }
        public InputData(InputDataMarshal data)
        {
            this.Name = data.Name;
            this.Type = data.Type;
            this.Value = data.Value;
            this.IsOutput = data.IsOutput;
        }
        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }

        public bool IsOutput { get; set; }
    }

    public class InputDataMarshal:MarshalByRefObject
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }

        public bool IsOutput { get; set; }
    }
}
