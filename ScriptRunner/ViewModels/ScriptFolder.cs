using ScriptRunner.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ScriptRunner.ViewModels
{
    class ScriptFolder : WPFNotify
    {

        private static object _lockObj = new object();

        public static int Count = 0;

        private string _name;

        private List<Script> _scripts;

        public ScriptFolder(string FolderName)
        {
            _name = FolderName;
            this.PropertyChanged += ScriptFolder_PropertyChanged;
        }

        private void ScriptFolder_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var folder = sender as ScriptFolder;
            Scripts.ForEach(s => s.IsChoose = folder.IsChoose);
        }

      

        public string Name { get { return _name; } }

        public List<Script> Scripts { get { return _scripts; } }

        private bool _isChoose;

        public bool IsChoose
        {
            get { return _isChoose; }
            set
            {
                SetProperty(ref _isChoose, value);
            }
        }

        public void FindScripts()
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.PrivateBinPath = _name;
            AppDomain domain = AppDomain.CreateDomain("Test", null, setup);
            ScriptSpy finder = domain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().Location, typeof(ScriptSpy).FullName) as ScriptSpy;
            var scripts = finder.GetScripts(_name);

            _scripts = new List<Script>();
            foreach (var s in scripts)
            {
                AddId();
                var script = new Script(s);
                script.Id = Count;
                _scripts.Add(new Script(s));
            }

            AppDomain.Unload(domain);
        }

        private void AddId()
        {
            System.Threading.Monitor.Enter(_lockObj);
            try
            {
                Count++;
            }
            finally
            {
                System.Threading.Monitor.Exit(_lockObj);
            }
        }
    }
}
