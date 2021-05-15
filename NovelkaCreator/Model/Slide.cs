using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using NovelkaCreator.ViewModel;

namespace NovelkaCreator.Model
{
    public class Slide : ViewModelBase
    {
        string id;
        string speaker;
        string text;
        BitmapImage image;

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Speaker
        {
            get => speaker;
            set
            {
                speaker = value;
                OnPropertyChanged(nameof(Speaker));
            }
        }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public BitmapImage Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
    }
}
