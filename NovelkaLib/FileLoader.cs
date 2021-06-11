using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NovelkaLib
{
    public static class FileLoader
    {
        public static Task<BitmapImage> LoadBitmapImageAsync(string path)
        {
            return Task.Run(() =>
            {
                return LoadBitmapImage(path);
            });
        }
        public static BitmapImage LoadBitmapImage(string path)
        {
            if (String.IsNullOrEmpty(path)) throw new Exception("Wrong path");
            using FileStream fs = File.OpenRead(path);
            BitmapImage image = new();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = fs;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}
