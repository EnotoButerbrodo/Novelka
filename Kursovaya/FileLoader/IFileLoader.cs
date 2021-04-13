using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Novelka.FileLoader
{
    public interface IFileLoader
    {
        MemoryStream LoadFile(string zipPath, string fileFullName);
    }
}
