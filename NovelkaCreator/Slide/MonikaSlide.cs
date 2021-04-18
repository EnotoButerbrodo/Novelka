using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NovelkaCreator.Slide
{
    class MonikaSlide : BasicSlide
    {
        
        protected override void DefaultImageSetup()
        {
            BitmapImage bitImage = new BitmapImage();
            bitImage.BeginInit();
            bitImage.UriSource = new Uri(@"Resources/MonikaTemplate.png", UriKind.Relative);
            bitImage.EndInit();
            image.Source = bitImage;
        }
    }
}
