using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptRunner.ViewModels;
using System.Reflection;
using ScriptRunner.Interface;
using System.IO;
using ScriptRunner.Interface.Attributes;

namespace ScriptRunner
{
    class ScriptSpy:MarshalByRefObject
    {
        private List<Assembly> _assemblyList;
        private List<ScriptMarshal> _scripts;
        public ScriptSpy()
        {
            _assemblyList = new List<Assembly>();
            _scripts = new List<ScriptMarshal>();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var asm = Assembly.Load(args.Name);
            _assemblyList.Add(asm);
            return asm;
        }


        public List<ScriptMarshal> GetScripts(string folder)
        {
            List<ScriptMarshal> scripts = new List<ScriptMarshal>();
            loadAllAssembly(folder);
            return _scripts;
        }

        private void loadAllAssembly(string folder)
        {
            DirectoryInfo di = new DirectoryInfo(folder);

            foreach (var f in di.GetFiles().Where(f => f.Extension.ToLower() == ".dll" || f.Extension.ToLower() == ".exe"))
            {
                try
                {
                    var asmName = AssemblyName.GetAssemblyName(f.FullName);

                    var asm = Assembly.Load(asmName);
                    _assemblyList.Add(asm);
                    getCaseInfo(asm);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void getCaseInfo(Assembly asm)
        {
            var existedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var t in asm.GetTypes().Where(t => t.IsClass))
            {
                var myInterface = t.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IScriptRunner<>)).FirstOrDefault();
                if (myInterface != null)
                {
                    if (t.GetConstructor(Type.EmptyTypes) != null)
                    {
                        getScripts(t, myInterface);
                    }


                }
            }
        }

        private void getScripts(Type t, Type interfaceType)
        {
            ScriptMarshal s = null;
            var sattr = t.GetCustomAttribute<ScriptAttribute>(true);
            if (sattr != null)
            {
                s = new ScriptMarshal();
                s.Steps = new List<StepMarshal>();

                s.Name = sattr.Name;
                s.Description = sattr.Description;
                s.HelpLink = sattr.HelpLink;

                foreach (var m in t.GetMethods())
                {
                    var stepAttr = m.GetCustomAttribute<StepAttribute>(true);
                    if (stepAttr != null)
                    {
                        StepMarshal step = new StepMarshal();
                        step.Id = stepAttr.Id;
                        step.Name = stepAttr.Name;
                        step.Description = stepAttr.Description;
                        s.Steps.Add(step);
                    }
                }

                var dataType = interfaceType.GetGenericArguments().First();
                s.Types = new List<InputDataMarshal>();


                s.Types.AddRange(getDataTypes(dataType));
                _scripts.Add(s);

            }
        }

        private List<InputDataMarshal> getDataTypes(Type dataType)
        {
            List<InputDataMarshal> types = new List<InputDataMarshal>();
            foreach (var prop in dataType.GetProperties().Where(p => p.PropertyType == typeof(string) || p.PropertyType.IsPrimitive))
            {
                types.Add(new InputDataMarshal() { Name = prop.Name, Type = prop.PropertyType.FullName });
            }
            return types;

        }


    }
}
