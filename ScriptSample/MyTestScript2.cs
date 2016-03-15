using ScriptRunner.Interface;
using ScriptRunner.Interface.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptSample
{
    public class Script2Data
    {
        [Parameter(Direction = Direction.Output)]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string AccountList { get; set; }
    }

    [Script("Test Case2")]
    public class MyTestScript2 : IScriptRunner<Script2Data>
    {
        public Script2Data GetResult()
        {
            throw new NotImplementedException();
        }

        public Script2Data GetSampleData()
        {
            return new Script2Data() { AccountList = "abc", Id = 1, UserName = "Zhou Yang" };
        }

        public void SetInputData(Script2Data data, IProgress<ProgressInfo> MyProgress)
        {

        }

        [Step(Id = 1, Name = "Test Step1")]
        public void Step1()
        {

        }

        [Step(Id = 2, Name = "Test Step2")]
        public void Step2()
        {

        }

        [Step(Id = 3, Name = "Test Step3")]
        public void Step3()
        { }

        [Step(Id = 4, Name = "Test Step4")]
        public void Step4()
        { }
    }
}
