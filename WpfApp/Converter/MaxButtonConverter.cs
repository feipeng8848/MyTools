using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfApp.Converter
{
    /// <summary>
    /// 切换最大化按钮
    /// </summary>
	public class MaxButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString() == "Max")
            {
                if ((WindowState)value != WindowState.Maximized)
                {
                    return "Visible";
                }
                return "Collapsed";
            }
            if ((WindowState)value != WindowState.Maximized)
            {
                return "Collapsed";
            }
            return "Visible";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
