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
using System.Reflection;
using System.IO;
using System.Data;
using Young.Data;
using Microsoft.Owin.Hosting;
using System.Net.Sockets;
using System.Net;

namespace ScriptAgent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IDisposable _signalR;

        
        public MainWindow()
        {
            InitializeComponent();
            startServer();
            Run(@"E:\GitHub\GLMEC\TestScript\bin\Debug\TestScript.dll", "TestScript.Case6.Case6_Workflow");
        }

        

        private  void startServer()
        {
            StartOptions options = new StartOptions();

            string server = $"http://localhost:{(Application.Current as App).Port}";

            options.Urls.Add(server);

            _signalR = WebApp.Start<Startup>(options);
        }

        public void Run(string Location, string TypeName)
        {
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;


            //loadAssemblies(Location);
            //var asm = Assembly.LoadFile(Location);
            //var types = asm.GetTypes();
            //DataTable dt = ExcelHelper.Current.Open(@"E:\GitHub\GLMEC\TestScript\bin\Debug\Case1_MTD_Analysis.xlsx").Read("Case6_WorkFlow");
            //var m = typeof(DataTableExtension).GetMethod("ToList");
            //var dataType = asm.GetType("TestScript.Case6.Case6DataModel");
            //var m1 = m.MakeGenericMethod(dataType);
            //dynamic dataObj = m1.Invoke(dt, new object[] { dt });

           
            //var t = asm.GetType(TypeName);
            //var obj = (ScriptRunner.Interface.IScriptRunner)Activator.CreateInstance(t);

            // Environment.CurrentDirectory = new FileInfo(Location).Directory.FullName;
            //// 

            //ScriptEngine se = new ScriptEngine(obj as IScriptRunner);






            //se.Run(dataObj[0]);


            //foreach (var d in myDatas)
            //{
            //    Case6_Workflow script = new Case6_Workflow();
            //    var runner = new ScriptRunner<Case6_Workflow>(script);
            //    runner.Run(d, 8);
            //   // runner.Run(d);

            //}
        }



      

      
    }

}
