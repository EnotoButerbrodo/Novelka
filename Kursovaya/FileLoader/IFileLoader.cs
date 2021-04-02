using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kursovaya.FileLoader
{
    public interface IFileLoader
    {
        MemoryStream LoadFile(string zipPath, string fileFullName);
    }
}
