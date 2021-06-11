using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NovelkaLib.Infrastructure
{
    public class RelayValueConverter : IValueConverter
    {
        Func<object, object, object> convertHandler;
        Func<object, object, object> convertBackHandler;

        public RelayValueConverter(Func<object, object, object> convertHandler,
            Func<object, object, object> convertBackHandler = null)
        {
            this.convertHandler = convertHandler ?? throw new ArgumentNullException(nameof(convertHandler)); ;
            this.convertBackHandler = convertBackHandler;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return convertHandler(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return convertBackHandler(value, parameter);
        }
    }
}
