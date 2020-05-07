using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp.Converter
{
    public class PopupVeticalOffsetConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null)
            {
                object obj = values[0];
                if (obj is double)
                {
                    double num = (double)obj;
                    obj = values[1];
                    if (obj is double)
                    {
                        double num2 = (double)obj;
                        return num / 2.0 + num2 / 2.0 + 9.0;
                    }
                }
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
