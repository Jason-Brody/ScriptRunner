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
            port = TcpPort.FreeTcpPort();
            string server = $"http://localhost:{port}";
            options.Urls.Add($"http://127.0.0.1:{port}");
            options.Urls.Add(server);

             _signalR = WebApp.Start<Startup>(options);
            item_Port.Content = port;
        }

        private void btn_Temp_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = @"E:\GitHub\ScriptRunner\ScriptRunner\SignalRClientTest\bin\Debug\SignalRClientTest.exe";

            ScriptTaskInfo sti = new ScriptTaskInfo()
            {
                Location = @"E:\GitHub\GLMEC\TestScript\bin\Debug\TestScript.dll",
                ScriptType = "TestScript.Case6.Case6_Workflow",
            };
            ScriptTask st = new ScriptTask() { Id = Guid.NewGuid().ToString(), TaskData = sti };

            pi.Arguments = $"{port} {st.Id}";

            Task.Run(() => { Process.Start(pi); });

            ScriptRunnerManager.ScriptTasks.Add(st);
            //IHubContext<IScriptClient> hubContext = GlobalHost.ConnectionManager.GetHubContext<IScriptClient>("ScriptHub");
            
            
        }
    }
}
