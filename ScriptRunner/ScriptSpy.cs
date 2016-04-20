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
using ScriptRunner.Models;

namespace ScriptRunner
{
    class ScriptSpy : MarshalByRefObject
    {
        private List<ScriptMarshal> _scripts;
        public ScriptSpy() {

            _scripts = new List<ScriptMarshal>();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
            var asm = Assembly.Load(args.Name);

            return asm;
        }


        public List<ScriptMarshal> GetScripts(string folder) {
            List<ScriptMarshal> scripts = new List<ScriptMarshal>();
            loadAllAssembly(folder);
            return _scripts;
        }

        private void loadAllAssembly(string folder) {
            DirectoryInfo di = new DirectoryInfo(folder);

            foreach (var f in di.GetFiles().Where(f => f.Extension.ToLower() == ".dll" || f.Extension.ToLower() == ".exe")) {
                try {
                    var asmName = AssemblyName.GetAssemblyName(f.FullName);
                    var asm = Assembly.Load(asmName);

                    getCaseInfo(asm);
                }
                catch (Exception ex) {

                }
            }
        }


        private void getCaseInfo(Assembly asm) {
            var existedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var t in asm.GetTypes().Where(t => t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(ScriptBase<>))) {

                if (t.GetConstructor(Type.EmptyTypes) != null) {
                    getScripts(t, asm.Location);
                }
            }
        }

        private void getScripts(Type t, string location) {

            ScriptMarshal s = null;
            var sattr = t.GetCustomAttribute<ScriptAttribute>(true);
            if (sattr != null) {
                s = new ScriptMarshal();
                s.Steps = new List<StepMarshal>();

                s.Name = sattr.Name;
                s.Description = sattr.Description;
                s.HelpLink = sattr.HelpLink;
                s.Location = location;
                s.TargetClass = t.FullName;

                foreach (var m in t.GetMethods()) {
                    var stepAttr = m.GetCustomAttribute<StepAttribute>(true);
                    if (stepAttr != null) {
                        StepMarshal step = new StepMarshal();
                        step.Id = stepAttr.Id;
                        step.Name = stepAttr.Name;
                        step.Description = stepAttr.Description;
                        s.Steps.Add(step);
                    }
                }
                var dataType = t.BaseType.GetGenericArguments().First();
                s.Types = new List<InputDataMarshal>();
                s.Types.AddRange(getDataTypes(dataType));
                _scripts.Add(s);

            }
        }

        private List<InputDataMarshal> getDataTypes(Type dataType) {
            List<InputDataMarshal> types = new List<InputDataMarshal>();
            foreach (var prop in dataType.GetProperties().Where(p => p.PropertyType == typeof(string) || p.PropertyType.IsPrimitive)) {
                var paraAttr = prop.GetCustomAttribute<ParameterAttribute>();
                types.Add(new InputDataMarshal() { Name = prop.Name, Type = prop.PropertyType.FullName, IsOutput = paraAttr?.Direction == Direction.Output ? true : false });
            }
            return types;

        }


    }
}
