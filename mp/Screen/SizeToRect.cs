using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace mp.Screen
{
    class SizeToRect : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = (Size)value;
            Console.WriteLine("{0} {1}", size.Width, size.Height);
            return new Rect(0, 0, size.Width, size.Height);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rect = (Rect)value;
            return new Size(rect.Width, rect.Height);
        }
    }
}
