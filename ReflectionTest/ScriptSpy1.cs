using ScriptRunner.Interface.Attributes;
using ScriptRunner.Models;
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
    class ScriptSpy1
    {
        private List<Assembly> _assemblyList;
        private List<Type> _scriptTypes;

        public ScriptSpy1()
        {
            _assemblyList = new List<Assembly>();
            _scriptTypes = new List<Type>();
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
        }



        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                return Assembly.ReflectionOnlyLoad(args.Name);
            }
            catch
            {
                return _assemblyList.Where(c => c.FullName == args.Name).First();
            }

        }



        //private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //    return _assemblyList.Where(a => a.FullName == args.Name).First();
        //}

        public List<ScriptMarshal> GetScripts(string folder)
        {
            List<ScriptMarshal> scripts = new List<ScriptMarshal>();
            loadAllAssembly(folder);
            //foreach (var asm in _assemblyList)
            //{
            //    getCaseInfo(asm);
            //}
            var apsms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var t in _scriptTypes)
            {
                var myType = t;
                apsms = AppDomain.CurrentDomain.GetAssemblies();
                var script = getScripts(myType);
                scripts.Add(script);
            }


            return scripts;
        }

        private void loadAllAssembly(string folder)
        {
            var existedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            DirectoryInfo di = new DirectoryInfo(folder);

            foreach (var f in di.GetFiles().Where(f => f.Extension.ToLower() == ".dll" || f.Extension.ToLower() == ".exe"))
            {


                try
                {

                    _assemblyList.Add(Assembly.ReflectionOnlyLoadFrom(f.FullName));

                    //var asmName = AssemblyName.GetAssemblyName(f.FullName);
                    ////if(asmName.Name != "ScriptRunner.Interface")
                    //if (existedAssemblies.Where(a => a.GetName().Name == asmName.Name).FirstOrDefault() == null)
                    //{
                    //    var asm = Assembly.ReflectionOnlyLoadFrom(f.FullName);
                    //    getCaseInfo(asm);
                    //    //_assemblyList.Add(Assembly.LoadFrom(f.FullName));
                    //}


                }
                catch (Exception ex)
                {

                }


            }

            foreach (var asm in _assemblyList)
                getCaseInfo(asm);
            existedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        private ScriptMarshal getScripts(Type t)
        {

            ScriptMarshal s = null;

            var attrs = t.GetCustomAttributes();
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

                var dataType = Type.GetType(sattr.InputDataType);

                s.Types = new List<InputDataMarshal>();

                foreach (var prop in dataType.GetProperties().Where(p => p.PropertyType == typeof(string) || p.PropertyType.IsPrimitive))
                {
                    s.Types.Add(new InputDataMarshal() { Name = prop.Name, Type = prop.PropertyType.FullName });
                }
            }



            return s;
        }

        private void getCaseInfo(Assembly asm)
        {
            var existedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var t in asm.GetTypes().Where(t => t.IsClass))
            {
                if (t.GetInterface("ScriptRunner.Interface.IScriptRunner") != null)
                {
                    if (t.GetConstructor(Type.EmptyTypes) != null)
                        _scriptTypes.Add(t);
                }
                //if (t.GetInterface(typeof(IScriptRunner).FullName) != null)
                //{

                //}
            }
            existedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            //catch (ReflectionTypeLoadException ex1)
            //{
            //    var interfaces = ex1.Types.Where(x => x != null && x.GetInterface(typeof(IScriptRunner).FullName) != null).ToList();
            //    return null;
            //}



        }
    }
}
