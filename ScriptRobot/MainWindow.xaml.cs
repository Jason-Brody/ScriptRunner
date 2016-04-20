using Microsoft.AspNet.SignalR.Client;
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
using ScriptRunner.Interface;
using ScriptRunner.Interface.Attributes;
using Newtonsoft.Json;

namespace ScriptRobot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _Id;

        public MainWindow()
        {
            InitializeComponent();
            _Id = (Application.Current as App).Id;

            connectToServer();
        }

        private StepAttribute _current;

        private IHubProxy proxy;
        private async void connectToServer()
        {
            var queryData = new Dictionary<string, string>();
            queryData.Add("guid", _Id);
            string server = $"http://localhost:{(Application.Current as App).Port}/signalr";
            var connection = new HubConnection(server, queryData, useDefaultUrl: false);
            proxy = connection.CreateHubProxy("ScriptHub");
            await connection.Start();


            //ScriptTaskInfo t = new ScriptTaskInfo();
            //t.Location = @"E:\GitHub\ScriptRunner\ScriptRunner\ScriptSample\bin\Debug\ScriptSample.dll";
            //t.ScriptType = "ScriptSample.MyTestScript1";
            //t.JsonData = "{\"0\":{\"UserName\":\"2321\"}}";
            var t = await proxy.Invoke<ScriptTaskInfo>("ReadyForTask");
            runTask(t);
        }

        private async void runTask(ScriptTaskInfo s)
        {
            var result = await Task.Run(() =>
            {
                //Dictionary<int, System.Dynamic.ExpandoObject> testObj = new Dictionary<int, System.Dynamic.ExpandoObject>();
                //var item = new System.Dynamic.ExpandoObject();
                //((IDictionary<string, object>)item).Add("UserName", "Zhou Yang");
                //((IDictionary<string, object>)item).Add("Id", 123);

                //testObj.Add(0, item);
                ////testObj.Add(1, item);
                //s.JsonData = JsonConvert.SerializeObject(testObj);

                RobotWorker worker = new RobotWorker(s);
                return worker.Run(beforeStep, afterStep, detailProgress);
            });

            await proxy.Invoke("Complete",JsonConvert.SerializeObject(result));
            this.Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });
        }

        private void beforeStep(StepAttribute step)
        {
            _current = step;
            rtb_Message.Dispatcher.Invoke(() =>
            {
                rtb_Message.AppendText($"Current Running step:{step.Name} \r");
            });
            
            proxy.Invoke("BeforeStep", step.Id);
        }

        private void afterStep(StepAttribute step)
        {
            
            rtb_Message.Dispatcher.Invoke(() =>
            {
                rtb_Message.AppendText($"step:{step.Name} is complete \r");
            });
            proxy.Invoke("AfterStep", step.Id);
        }

        private void detailProgress(ProgressInfo p)
        {

            rtb_Message.Dispatcher.Invoke(() =>
            {
                rtb_Message.AppendText(p.Current.ToString() + "\r");
            });
            proxy.Invoke("ReportProgress", _current.Id, p);
        }




    }
}
