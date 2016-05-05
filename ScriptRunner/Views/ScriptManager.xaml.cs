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
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ScriptRunner.ViewModels;

namespace ScriptRunner.Views
{
    /// <summary>
    /// Interaction logic for ScriptManager.xaml
    /// </summary>
    public partial class ScriptManager : UserControl
    {
        public ScriptManager()
        {
            InitializeComponent();
            this.DataContext = ScriptRunnerManager.ScriptFolders;
        }

        private async void btn_AddFolder_Click(object sender, RoutedEventArgs e)
        {
            //var result = await this.ShowInputAsync("Folder Info", "Please type folder address here");
            //if(result!=null)
            {
                var result = @"E:\GitHub\ATT\ATT.Scripts\bin\Debug";
                //var result = @"E:\GitHub\ScriptRunner\ScriptRunner\ScriptSample\bin\Debug";

                if (ScriptRunnerManager.ScriptFolders.Where(f => f.Name.ToLower().Trim() == result.ToLower().Trim()).FirstOrDefault() != null)
                {
                    await (App.Current.MainWindow as MetroWindow).ShowMessageAsync("ERROR", $"The folder:{result} is existed", MessageDialogStyle.Affirmative);
                }
                else
                {
                    ScriptFolder folder = new ScriptFolder(result);
                    await Task.Run(() => { folder.FindScripts(); });
                    ScriptRunnerManager.ScriptFolders.Add(folder);
                }
            }
        }

        private void btn_AddScript_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ScriptRunnerManager.ScriptFolders)
            {
                foreach(var subItem in item.Scripts)
                {
                    if(!ScriptRunnerManager.Scripts.Any(s=>s.Id == subItem.Id) && subItem.IsChoose)
                    {
                        ScriptRunnerManager.Scripts.Add(subItem);
                    }
                }
            }
        }
    }
}
