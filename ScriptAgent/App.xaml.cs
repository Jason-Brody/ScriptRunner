using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ScriptAgent
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public int Port { get; set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if(e.Args!=null && e.Args.Count()>0)
            {
                try
                {
                    Port = int.Parse(e.Args[0]);
                }
                catch
                {
                    this.Shutdown();
                }
                
            }
        }
    }
}
