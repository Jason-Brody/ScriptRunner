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
        [Parameter(Direction = Direction.Output)]
        public int Id { get; set; }

        public string UserName { get; set; }
    }

    [Script("Test Case1")]
    public class MyTestScript1 : ScriptBase<Script1Data>
    {
        

        [Step(Id =1,Name ="Test Step1")]
        public void Step1()
        {
            
            _stepReporter.Report(new ProgressInfo("Waiting for 5 seconds"));
            Task.Delay(5000).Wait();
        }

        [Step(Id =2,Name ="Test Step2")]
        public void Step2()
        {
            for(int i = 1;i<100;i++)
            {
                Task.Delay(10).Wait();
                _stepReporter.Report(new ProgressInfo(i,100, $"Hello {i}"));
            }
        }

        [Step(Id =3,Name ="Test Step3")]
        public void Step3()
        {
            for (int i = 1; i < 100; i++)
            {
                Task.Delay(25).Wait();
                _stepReporter.Report(new ProgressInfo(i,100,""));
            }

            _data.UserName = "alsdje";
            Random rd = new Random();
            _data.Id = rd.Next(1000);
        }

        public Script1Data GetResult()
        {
            _data.UserName = "alsdje";
            Random rd = new Random();
            _data.Id = rd.Next(1000);
            return _data;
        }

      
    }
}
