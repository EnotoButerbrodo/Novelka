using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace Novelka.Converters
{
    public class FormatConverter
    {
        public virtual BitmapImage ToBitmapImage(MemoryStream data)
        {
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = data;
            src.EndInit();
            return src;
        }
    }
}
