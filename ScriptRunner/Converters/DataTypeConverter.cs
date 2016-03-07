using ScriptRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ScriptRunner.Converters
{
    class DataTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sc = value as Script;
            if(sc!=null)
            {
                if(sc.Datas==null)
                {
                    sc.Datas = new DataTable();
                    foreach (var item in sc.Types)
                    {
                        sc.Datas.Columns.Add(item.Name, Type.GetType(item.Type));
                    }
                }


                return sc.Datas;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
