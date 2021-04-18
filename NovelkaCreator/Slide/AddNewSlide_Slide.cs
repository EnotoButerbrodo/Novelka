using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NovelkaCreator.Slide
{
    class AddNewSlide_Slide : BasicSlide
    {
        protected override void DefaultImageSetup()
        {
            BitmapImage bitImage = new BitmapImage();
            bitImage.BeginInit();
            bitImage.UriSource = new Uri(@"Resources/AddNewSlideIcon.png", UriKind.Relative);
            bitImage.EndInit();
            image.Source = bitImage;
        }
    }
}
