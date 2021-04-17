using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NovelkaCreator.Slide
{
    class BasicSlide
    {
        protected Image image;
        protected short id;
        protected Grid appearance;
        protected TextBlock idTextBlock;
        public BasicSlide(short id)
        {
            this.id = id;
            ImageSetup();
            IdTextBlockSetup();
            GridSetup();
            
        }
        void ImageSetup()
        {
            image = new Image();
            image.Stretch = Stretch.Uniform;
           
        }
        void GridSetup()
        {
            appearance = new Grid();
            ColumnDefinition idColumn = new ColumnDefinition
            {
                Width = new GridLength(20, GridUnitType.Pixel)
            };
            ColumnDefinition MainColumn = new ColumnDefinition
            {
                Width = new GridLength(200, GridUnitType.Star)
            };
            appearance.ColumnDefinitions.Add(idColumn);
            appearance.ColumnDefinitions.Add(MainColumn);
            appearance.Margin = new System.Windows.Thickness(20, 10, 20, 10);

            Grid.SetColumn(image, 1);
            appearance.Children.Add(image);


            Grid.SetColumn(idTextBlock, 0);
            appearance.Children.Add(idTextBlock);
        }
        protected virtual void IdTextBlockSetup()
        {
            idTextBlock = new TextBlock();
            idTextBlock.Text = id.ToString();
            idTextBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            idTextBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            idTextBlock.FontSize = 20;
            idTextBlock.Foreground = Brushes.Black;

            
        }
        public Grid GetAppearance() => appearance;
        public short GetNumber() => id;
        
    }
}
