using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NovelkaCreator.Slide
{
    class AddNewSlide : BasicSlide
    {
        public AddNewSlide() : base("")
        {
            BitmapImage bitImage = new BitmapImage();
            bitImage.BeginInit();
            bitImage.UriSource = new Uri(@"Resources/AddNewSlideIcon.png", UriKind.Relative);
            bitImage.EndInit();
            image.Source = bitImage;
        }

        
    }
}
