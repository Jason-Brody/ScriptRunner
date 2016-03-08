using ScriptRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScriptRunner.Views
{
    /// <summary>
    /// Interaction logic for ScriptExecution.xaml
    /// </summary>
    public partial class ScriptExecution : UserControl
    {
        public ScriptExecution()
        {
            InitializeComponent();
            this.DataContext = ScriptRunnerManager.Scripts;
        }

        private void dg_Data_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var style = new Style(typeof(DataGridColumnHeader));
            style.BasedOn = (Style)FindResource("MetroDataGridColumnHeader");


            style.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            if (e.Column.IsReadOnly)
            {
                style.Setters.Add(new Setter(DataGridColumnHeader.ForegroundProperty, new SolidColorBrush(Colors.Red)));
            }
            //e.Column.Header = e.PropertyName.ToUpper();
            e.Column.HeaderStyle = style;
            e.Column.MinWidth = 50;

            
        }

        private void lv_Scripts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            ScriptRunnerManager.CurrentScript = lv_Scripts.SelectedItem as Script;
            (Application.Current.MainWindow as MainWindow).sbi_Current.DataContext = ScriptRunnerManager.CurrentScript;
        }
    }
}
