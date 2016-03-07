using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptRunner.Interface;
using TestScript.Case6;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace SignalRClientTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var port = args[0];
            //System.Diagnostics.ProcessStartInfo pi = new ProcessStartInfo();
            //pi.FileName = @"E:\GitHub\ScriptRunner\ScriptRunner\ScriptAgent\bin\Debug\ScriptAgent.exe";
            //pi.Arguments = port.ToString();
            //Process.Start(pi);

            //var method = typeof(JsonConvert).GetMethods().Where(m => m.IsGenericMethod && m.Name == nameof(JsonConvert.DeserializeObject) && m.GetParameters()[0].ParameterType == typeof(string)).First();

            string _server = $"http://localhost:{port}/signalr";
            var _connection = new HubConnection(_server, useDefaultUrl: false);
            var _proxy = _connection.CreateHubProxy("ScriptHub");
            _proxy.On("Connect", () => { Console.WriteLine("Connected"); });
            _connection.Start().Wait();

            //RunScript(string Location, string TypeName, string Jsondata, IProgress < ProcessInfo > progress)
            //Case6DataModel data = new Case6DataModel();
            //data.UserName = "Zhou Yang";
            //data.Password = "123456";
            //data.Client = "100";
            //data.Address = "www.baidu.com";
            //data.Language = "EN";
            //string jsonData = JsonConvert.SerializeObject(data);

            //var t = _proxy.Invoke<Case6DataModel>("RunScript", new object[] { @"E:\GitHub\GLMEC\TestScript\bin\Debug\TestScript.dll", "TestScript.Case6.Case6_Workflow", jsonData });
            //if (t.Status == TaskStatus.WaitingToRun)
            //    t.Start();
            //t.Wait();
            //Console.WriteLine(t.Result.UserName);
            Console.ReadLine();
        }

        static int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
    }
}
