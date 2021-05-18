using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace NovelkaCreationTool.Infrastructure.Converters
{
    public class PathToImageLoaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string path = value as string;
            if (String.IsNullOrEmpty(path)) return new BitmapImage();
            using (FileStream fs = File.OpenRead(path))
            {
                BitmapImage image = new BitmapImage();
                image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = fs;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
