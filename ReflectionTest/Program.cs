using ScriptRunner.Interface.Attributes;
using ScriptRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;

namespace ReflectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var tp = typeof(ScriptSample.MyTestScript1);
            Console.WriteLine(tp.IsSubclassOf(typeof(ScriptRunner.Interface.ScriptBase<ScriptSample.Script1Data>)));
            var tp1 = tp.BaseType;
            Console.WriteLine(tp.IsSubclassOf(typeof(ScriptRunner.Interface.ScriptBase<>)));
            Console.WriteLine(tp1.GetGenericTypeDefinition() == typeof(ScriptRunner.Interface.ScriptBase<>).GetGenericTypeDefinition());
            Console.WriteLine(tp1.IsSubclassOf(typeof(ScriptRunner.Interface.ScriptBase<>)));

            HttpClient client = new HttpClient();

            client.BaseAddress =  new Uri("http://sh.lianjia.com/ershoufang");
            var response = client.SendAsync(new HttpRequestMessage()).Result;
           var result =  response.Content.ReadAsStringAsync().Result;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            var node = doc.DocumentNode.SelectSingleNode(@"//*[@class='option-list']");
            foreach(var n in node.ChildNodes.Where(c=>c.Name!="#text"))
            {
                Console.WriteLine(n.Attributes["href"].Value);
            }
        }

        static List<Task> ThreadLockTest()
        {
            var lockObj = new object();
            List<Task> taskList = new List<Task>();
            for(int i =0;i<500000;i++)
            {
                taskList.Add( Task.Run(() => {
                    System.Threading.Monitor.Enter(lockObj);
                    try
                    {
                        Id++;
                    }
                    finally
                    {
                        System.Threading.Monitor.Exit(lockObj);
                    }
                }));
            }
            return taskList;
        }

        public static int Id = 0;
    }


    public interface ITools<T>
    {

    }

    public class Test
    {

    }

    public class Tools:ITools<Test>
    {

    }
    
}
