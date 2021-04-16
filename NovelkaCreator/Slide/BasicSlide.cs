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
    class BasicSlide
    {
        protected Image image;
        protected string id;
        protected Grid appearance;
        public BasicSlide(string id)
        {
            this.id = id;
            appearance = new Grid();
            image = new Image();
            image.Stretch = Stretch.Uniform;
            image.Margin = new System.Windows.Thickness(5, 0, 5, 0);
            appearance.Children.Add(image);
            appearance.Margin = new System.Windows.Thickness(0, 5, 0, 5);
        }
        public Grid GetAppearance() => appearance;
        public string GetNumber() => id;
        
    }
}
