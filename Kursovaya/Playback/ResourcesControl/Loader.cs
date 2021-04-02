using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
using Ionic.Zip;

namespace Kursovaya.Playback.ResourcesControl
{
    //Цель - загрузить необходимые данные из игровых ресурсов
    public class Loader
    {
        //public BitmapImage GetImage(string zipPath, string fileName)
        //{
        //    var buff = LoadFile(zipPath, fileName);
        //    return ConvertToBitmapImage(buff);
        //}

        public MemoryStream LoadFile(string zipPath, string fileFullName)
        {//Чтение любого файла из архива в виде MemoryStream
            using (ZipFile zip = ZipFile.Read(zipPath))
            {
                MemoryStream stream = new MemoryStream();
                zip[fileFullName].Extract(stream);
                return stream;
            }
            throw new Exception("Файл не найден");
        }

        //public MemoryStream[] LoadFiles(string zipPath, string[] filesName)
        //{
        //    List<MemoryStream> loadedData = new List<MemoryStream>(filesName.Length);
        //    MemoryStream readedData;
        //    using (ZipFile zip = ZipFile.Read(zipPath))
        //    {
        //        for(int i = 0; i<filesName.Length; i++)
        //        {
        //            readedData = new MemoryStream();
        //            zip[filesName[i]].Extract(readedData);
        //            loadedData.Add(readedData);
        //        }
        //    }
        //    return loadedData.ToArray();
        //}

        //public BitmapImage ConvertToBitmapImage(MemoryStream stream)
        //{
        //    BitmapImage src = new BitmapImage();
        //    src.BeginInit();
        //    src.StreamSource = stream;
        //    src.EndInit();
        //    return src;
        //}
    }
}
