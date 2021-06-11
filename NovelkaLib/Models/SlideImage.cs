using System;
using NovelkaLib.ViewModels;

namespace NovelkaLib.Models
{
    [Serializable]
    public class SlideImage : ViewModelBase
    {
        double x, y, z;
        int width, height;
        string name, imageName;
        public double X
        {
            get => x;
            set => Set(ref x, value);
        }
        public double Y
        {
            get => y;
            set => Set(ref y, value);
        }
        public double Z
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
