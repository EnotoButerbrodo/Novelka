using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace Novelka.Converters
{
    public interface IFormatConverter
    {
        BitmapImage ToBitmapImage(MemoryStream data);
        
    }
}