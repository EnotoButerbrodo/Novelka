using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NovelkaCreationTool.Infrastructure.Converters
{
    public class RelayValueConverter: IValueConverter
    {
        Func<object, Type, object, object> ConvertOperation;
        public RelayValueConverter(Func<object, Type, object, object> operation)
        {
            this.ConvertOperation = operation;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertOperation(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
