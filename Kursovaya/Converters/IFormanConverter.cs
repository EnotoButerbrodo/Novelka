using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace Kursovaya.Converters
{
    public interface IFormanConverter
    {
        BitmapImage ToBitmapImage(MemoryStream data);
        
    }
}