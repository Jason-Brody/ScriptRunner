using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ScriptRobot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string Port { get; set; } = "5000";

        public string Id { get; set; } = "7860";

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if(e.Args!= null && e.Args.Count()>1)
            {
                Port = e.Args[0];
                Id = e.Args[1];
            }
        }
    }
}
