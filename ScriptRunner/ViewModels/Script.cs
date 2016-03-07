using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptRunner.Interface;
using System.Collections.ObjectModel;
using System.Data;

namespace ScriptRunner.ViewModels
{
   
    public class Script:WPFNotify
    {
        public Script() { }

        public Script(ScriptMarshal script)
        {
            this.Name = script.Name;
            this.Description = script.Description;
            this.HelpLink = script.HelpLink;
            this.Steps = new List<Step>();
            this.Types = new List<InputData>();
            foreach(var s in script.Steps)
            {
                this.Steps.Add(new Step(s));
            }
            foreach(var t in script.Types)
            {
                this.Types.Add(new InputData(t));
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public string Description { get; set; }


        public string HelpLink { get; set; }


        public List<Step> Steps { get; set; }

        public List<InputData> Types { get; set; }

        private bool _isChoose;
        public bool IsChoose
        {
            get { return _isChoose; }
            set { SetProperty(ref _isChoose, value); }
        }

        public DataTable Datas { get; set; }
    }

    public class ScriptMarshal : MarshalByRefObject
    {
        public string Name { get; set; }


        public string Description { get; set; }


        public string HelpLink { get; set; }


        public List<StepMarshal> Steps { get; set; }

        public List<InputDataMarshal> Types { get; set; }
    }

}
