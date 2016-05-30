using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using ScriptRunner.Interface;

namespace ScriptRunner.Test
{
    [TestClass]
    public class UnitTest1
    {
        string asmFile = "ScriptRunner.Interface.dll";
        [TestMethod]
        public void FailDeleteAssembly()
        {


           

            Assert.AreEqual(a, b);
            //Assembly asm = Assembly.LoadFrom(asmFile);
            //Assert.IsTrue(asm != null);
            //dynamic obj = asm.CreateInstance("ScriptRunner.Interface.Test");
            
            //Assert.AreEqual("Hello:1", obj.GetValue("1"));
        }


        [TestMethod]
        public void ScriptTest() {
            ScriptEngine<MyScript, ScriptData> script = new ScriptEngine<MyScript, ScriptData>();
            ScriptData d = new ScriptData() { Data = "" };
            var msg = script.Run(d);

        }

        [TestMethod]
        public void SuccessDeleteAssembly()
        {
            AppDomainSetup appSetup = new AppDomainSetup();
            //appSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            //appSetup.PrivateBinPath = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain testDomain = AppDomain.CreateDomain("Test", null, appSetup);
            var proxy = testDomain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().Location, typeof(Proxy).FullName) as Proxy;
            //var proxy = testDomain.CreateInstanceAndUnwrap(typeof(Proxy).Assembly.FullName, typeof(Proxy).FullName) as Proxy;
            var a = proxy.GetDirectory();
            Assert.AreEqual("Hello:1", proxy.ShowValue("1", asmFile));
            AppDomain.Unload(testDomain);


            File.Delete(asmFile);
        }

        [TestMethod]
        public void UnwrapTest()
        {

           

            dynamic obj = new System.Dynamic.ExpandoObject();
            ((IDictionary<string, object>)obj).Add("Name", "Zhou Yang");
            ((IDictionary<string, object>)obj).Add("Age", "18");
            foreach (var item in ((IDictionary<string, object>)obj))
            {
                var a1 = item.Key;
                var b1 = item.Value.GetType();
            }

            

            string s = JsonConvert.SerializeObject(obj);
            Test1 t1 = JsonConvert.DeserializeObject<Test1>(s);


            Test t = new Test() { };
            t.Test1 = new Test1() { Name = "Zhou Yang", Age = "18" };
            string s1 = JsonConvert.SerializeObject(t);
            obj = JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(s1);
            foreach(var item in ((IDictionary<string, object>)obj))
            {
                var a1 = item.Key;
                var b1 = item.Value.GetType();
            }

            string b = obj.Name;
            Int64 a = obj.Age;
        }


    }

    public static class Ex
    {
        public static string Test123(this Test t)
        {


            return "a";

        }
    }

    public class Test
    {

        public string Name { get; set; }

        public int Age { get; set; }

        public Test1 Test1 { get; set; }
    }

    public class Test1
    {
        public string Name { get; set; }

        public string Age { get; set; }
    }
    public class Proxy : MarshalByRefObject
    {
        public string GetDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public string ShowValue(string Name, string asmFile)
        {
            Assembly asm = Assembly.LoadFrom(asmFile);
            dynamic obj = asm.CreateInstance("ScriptRunner.Interface.Test");
            return obj.GetValue(Name);
        }
    }
}
