using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.ViewModels
{
    public class InputData
    {
        public InputData() { }
        public InputData(InputDataMarshal data)
        {
            this.Name = data.Name;
            this.Type = data.Type;
            this.Value = data.Value;
        }
        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }
    }

    public class InputDataMarshal:MarshalByRefObject
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }
    }
}
