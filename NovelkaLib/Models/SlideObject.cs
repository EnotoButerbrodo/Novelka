using System;
using System.Text.Json.Serialization;
using System.Windows.Media.Imaging;
using NovelkaLib.ViewModels;

namespace NovelkaLib.Models
{
    [Serializable]
    public class SlideObject : ViewModelBase
    {
        double x, y, z;
        double width, height;
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
        public double Width
        {
            get => width;
            set => Set(ref width, value);
        }
        public double Height
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
