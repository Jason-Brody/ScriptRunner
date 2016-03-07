using ScriptRunner.Interface.Attributes;
using ScriptRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = typeof(Tools).GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ITools<>)).FirstOrDefault();
            //var b = a.GetGenericArguments().FirstOrDefault();


            //Task.WaitAll(ThreadLockTest().ToArray());
           // Console.WriteLine(Id);

            string folder = @"E:\GitHub\GLMEC\TestScript\bin\Debug";
            ScriptSpy spy = new ScriptSpy();
            var sc =  spy.GetScripts(folder);
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
