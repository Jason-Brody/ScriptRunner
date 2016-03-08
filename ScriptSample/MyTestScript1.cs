using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptRunner.Interface;
using ScriptRunner.Interface.Attributes;

namespace ScriptSample
{
    
    public class Script1Data
    {
        public int Id { get; set; }

        public string UserName { get; set; }
    }

    [Script("Test Case1")]
    public class MyTestScript1 : IScriptRunner<Script1Data>
    {
        public Script1Data GetSampleData()
        {
            throw new NotImplementedException();
        }

        public void SetInputData(Script1Data data, IProgress<ProcessInfo> MyProgress)
        {
            
        }

        [Step(Id =1,Name ="Test Step1")]
        public void Step1()
        {
            Task.Delay(5000).Wait();
        }

        [Step(Id =2,Name ="Test Step2")]
        public void Step2()
        {

        }

        [Step(Id =3,Name ="Test Step3")]
        public void Step3()
        { }
    }
}
