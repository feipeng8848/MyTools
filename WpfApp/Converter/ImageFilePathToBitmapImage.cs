using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace WpfApp.Converter
{
    class ImageFilePathToBitmapImage : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var imgPaths = values[0] as List<string>;
            var ipAddr = values[1] as string;

            if (imgPaths == null || ipAddr == null)
            {
                return null;
            }

            foreach (var item in imgPaths)
            {
                if (item.Contains(ipAddr))
                {
                    var x = new BitmapImage();
                    x.BeginInit();
                    x.UriSource = new Uri(item);
                    x.EndInit();
                    return x;
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
