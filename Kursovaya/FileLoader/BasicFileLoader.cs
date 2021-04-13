using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Novelka.FileLoader
{
    class BasicFileLoader : IFileLoader
    {
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
    }
}
