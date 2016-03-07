using Microsoft.AspNet.SignalR;
using ScriptRunner.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScriptAgent
{
    public class ScriptHub:Hub<IScriptClient>
    {
        public object RunScript(string Location, string TypeName,string Jsondata)
        {
            var data = loadAssemblies(Location, TypeName, Jsondata);
            return data;
        }

        public override Task OnConnected()
        {
            Clients.Caller.Connect();
            return base.OnConnected();
        }

        private object loadAssemblies(string Location, string TypeName,string Jsondata)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            FileInfo fi = new FileInfo(Location);
            var asmName = AssemblyName.GetAssemblyName(fi.FullName);
            var asm = Assembly.Load(asmName);
            var scriptType = asm.GetType(TypeName);
            var dataType = scriptType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IScriptRunner<>)).First().GetGenericArguments().First();


            var method = typeof(JsonConvert).GetMethods().Where(m => m.IsGenericMethod && m.Name == nameof(JsonConvert.DeserializeObject) && m.GetParameters()[0].ParameterType == typeof(string)).First();
            var genericMethod = method.MakeGenericMethod(dataType);
            object data = genericMethod.Invoke(null, new object[] { Jsondata });
            return data;
            

        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.Load(args.Name);
        }
    }
}
