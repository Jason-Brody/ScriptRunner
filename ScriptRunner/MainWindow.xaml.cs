using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using System.Collections.ObjectModel;
using ScriptRunner.ViewModels;
using Microsoft.Owin.Hosting;
using ScriptRunner.Interface.Utils;
using Microsoft.AspNet.SignalR;
using ScriptRunner.Interface;
using System.Diagnostics;
using ScriptRunner.Models;
using Newtonsoft.Json;
using System.Data;

namespace ScriptRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IDisposable _signalR;
        private int port;
        public MainWindow()
        {
            InitializeComponent();
            startServer();
            
        }

        
        private void startServer()
        {
            StartOptions options = new StartOptions();
            port = 5000;
                //TcpPort.FreeTcpPort();
            string server = $"http://localhost:{port}";
            options.Urls.Add($"http://127.0.0.1:{port}");
            options.Urls.Add(server);

             _signalR = WebApp.Start<Startup>(options);
            item_Port.Content = port;
        }

        private void btn_Temp_Click(object sender, RoutedEventArgs e)
        {
            var dynamicObj = new System.Dynamic.ExpandoObject();
            foreach(DataColumn dc in ScriptRunnerManager.CurrentScript.Datas.Columns)
            {
                if(!dc.ReadOnly)
                {
                    DataRow dr = ScriptRunnerManager.CurrentScript.Datas.Rows[0];
                    (dynamicObj as IDictionary<string, object>).Add(dc.ColumnName, dr[dc]);
                }
               
            }
            Dictionary<int, System.Dynamic.ExpandoObject> objs = new Dictionary<int, System.Dynamic.ExpandoObject>();
            objs.Add(0, dynamicObj);

            ScriptTaskInfo sti = new ScriptTaskInfo()
            {
                Location = ScriptRunnerManager.CurrentScript.Location,
                ScriptType = ScriptRunnerManager.CurrentScript.TargetClass,
                JsonData = JsonConvert.SerializeObject(objs)
            };

            Robot robot = new Robot();
            robot.Guid = Guid.NewGuid().ToString();
            robot.TaskData = sti;
            ScriptRunnerManager.Robots.Add(robot);
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = @"E:\GitHub\ScriptRunner\ScriptRunner\ScriptRobot\bin\Debug\ScriptRobot.exe";
            pi.Arguments = $"{port} {robot.Guid}";
            Task.Run(()=>robot.ProcessId = Process.Start(pi).Id);





            IHubContext<IScriptClient> hubContext = GlobalHost.ConnectionManager.GetHubContext<IScriptClient>("ScriptHub");
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fo_Setting.IsOpen = !fo_Setting.IsOpen;
        }
    }
}
