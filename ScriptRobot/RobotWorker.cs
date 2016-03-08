using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptRunner.Interface;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace ScriptRobot
{
    class RobotWorker
    {
        private ScriptTaskInfo _task;

        public RobotWorker(ScriptTaskInfo taskInfo)
        {
            _task = taskInfo;
        }

        public void Run()
        {
            run();
        }

        private void run()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            FileInfo fi = new FileInfo(_task.Location);
            var asmName = AssemblyName.GetAssemblyName(fi.FullName);
            var asm = Assembly.Load(asmName);
            var scriptType = asm.GetType(_task.ScriptType);
            var dataType = scriptType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IScriptRunner<>)).First().GetGenericArguments().First();


            var method = typeof(JsonConvert).GetMethods().Where(m => m.IsGenericMethod && m.Name == nameof(JsonConvert.DeserializeObject) && m.GetParameters()[0].ParameterType == typeof(string)).First();
            var genericMethod = method.MakeGenericMethod(dataType);
            //object data = genericMethod.Invoke(null, new object[] { _task.JsonData });

           
            var targetInstance = Activator.CreateInstance(scriptType);

            Type engineType = typeof(ScriptEngine<>).MakeGenericType(new Type[] { dataType });
            dynamic engineInstance = Activator.CreateInstance(engineType,new object[] { targetInstance});
            engineInstance.Run(null);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.Load(args.Name);
        }
    }

}
