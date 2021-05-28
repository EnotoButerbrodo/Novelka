using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NovelkaCreationTool.Infrastructure.Converters
{
    class SetPositionAsOwnOrMousePositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int ownPosition = (int)values[0];
            double mousePosition = (double)values[1];
            bool isDrag = (bool)values[2];
            if (isDrag) return System.Convert.ToInt32(mousePosition);
            else return ownPosition;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
