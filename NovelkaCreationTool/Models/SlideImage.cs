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
    [DataContract]
    public class SlideImage : ViewModelBase
    {
        int x, y, z, width, height;
        string name, imageName;
        [DataMember]
        public int X
        {
            get => x;
            set => Set(ref x, value);
        }
        [DataMember]
        public int Y
        {
            get => y;
            set => Set(ref y, value);
        }
        [DataMember]
        public int Z
        {
            get => z;
            set => Set(ref z, value);
        }
        [DataMember]
        public int Width
        {
            get => width;
            set => Set(ref width, value);
        }
        [DataMember]
        public int Height
        {
            get => height;
            set => Set(ref height, value);
        }
        [DataMember]
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        [DataMember]
        public string ImageName
        {
            get => imageName;
            set => Set(ref imageName, value);
        }
    }
}
