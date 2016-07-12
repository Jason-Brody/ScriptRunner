using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptRunner.Interface;
using ScriptRunner.Interface.Attributes;

namespace ScriptRunner.Test
{
    public class ScriptData
    {
        public string Data { get; set; }
    }

    [Script(nameof(MyScript))]
    public class MyScript:ScriptBase<ScriptData>
    {
        [Step(Id =1)]
        public void Step1() {
            _data.Data += "Step1";
        }

        [Step(Id =2)]
        public void Step2() {
            //Return("Error Found");
            //SkipTo(5);
        }

        [Step(Id=3)]
        public void Step3() {
            _data.Data += "Step3";
            throw new Exception("Test Ex");
        }

        [Step(Id = 4)]
        public void Step4() {
            _data.Data += "Step4";
        }

        [Step(Id = 5)]
        public void Step5() {
            _data.Data += "Step5";
        }
    }
}
