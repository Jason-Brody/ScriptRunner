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

namespace ScriptRobot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            try
            {
                connectToServer();
            }
            catch (Exception ex)
            {
                rtb_Message.AppendText(ex.Message);
            }

        }


        private IHubProxy proxy;
        private async void connectToServer()
        {

            string server = $"http://localhost:{(Application.Current as App).Port}/signalr";
            var connection = new HubConnection(server, useDefaultUrl: false);
            proxy = connection.CreateHubProxy("ScriptHub");
            proxy.On<ScriptTaskInfo>("RunScript", (s) =>
            {

                

                rtb_Message.Dispatcher.Invoke(() =>
                {
                    rtb_Message.AppendText(s.Location + "\r");
                    rtb_Message.AppendText(s.ScriptType + "\r");
                    rtb_Message.AppendText(s.JsonData + "\r");


                });

                RobotWorker worker = new RobotWorker(s);
                worker.Run();
                
            });
            proxy.On("Complete", () =>
            {

                rtb_Message.Dispatcher.Invoke(() =>
                {
                    rtb_Message.AppendText("Complete" + "\r");
                    Application.Current.Shutdown();

                });



            });
            connection.Start().Wait();
            await proxy.Invoke("ReadyForTask", new object[] { (Application.Current as App).Id });
            
            await proxy.Invoke("Complete", new object[] { (Application.Current as App).Id });



        }


    }
}
