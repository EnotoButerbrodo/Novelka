using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using NovelkaCreationTool.ViewModels.Base;

namespace NovelkaCreationTool.Models
{
    [Serializable]
    public class SlideImage : ViewModelBase
    {
        int x, y, z, width, height;
        string name, imageName;

        public int X
        {
            get => x;
            set => Set(ref x, value);
        }

        public int Y
        {
            get => y;
            set => Set(ref y, value);
        }

        public int Z
        {
            get => z;
            set => Set(ref z, value);
        }

        public int Width
        {
            get => width;
            set => Set(ref width, value);
        }

        public int Height
        {
            get => height;
            set => Set(ref height, value);
        }
  
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public string ImageName
        {
            get => imageName;
            set => Set(ref imageName, value);
        }
    }
}
