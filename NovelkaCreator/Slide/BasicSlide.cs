using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NovelkaCreator.Slide
{
    [Serializable]
    public class BasicSlide
    {
        public Image image { get; protected set; }
        public int id { get; protected set; }
        protected Grid appearance;
        protected TextBlock idTextBlock;
        protected Border selectBorder;
        protected MouseButtonEventHandler SlideClickEventHandler;
        public Novelka.Plot.Frame frame = new Novelka.Plot.Frame();
        public BasicSlide()
        {
            GridSetup();
            ImageSetup();
            SelectBorderSetup();
            DefaultImageSetup();
            IdTextBlockSetup();
        }
        public BasicSlide(int id) : this() {
            this.id = id;
        }
        void ImageSetup()
        {
            image = new Image();
            image.Stretch = Stretch.Uniform;
            Panel.SetZIndex(image, 1);

            Grid.SetColumn(image, 1);
            appearance.Children.Add(image);

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
            appearance.PreviewMouseLeftButtonUp += SlideClick;

        }
        protected virtual void DefaultImageSetup()
        {
            BitmapImage bitImage = new BitmapImage();
            bitImage.BeginInit();
            bitImage.UriSource = new Uri(@"Resources/BlankTemplate.png", UriKind.Relative);
            bitImage.EndInit();
            image.Source = bitImage;
        }
        protected virtual void IdTextBlockSetup()
        {
            idTextBlock = new TextBlock();
            idTextBlock.Text = id.ToString();
            idTextBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            idTextBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            idTextBlock.FontSize = 20;
            idTextBlock.Foreground = Brushes.Black;
            Grid.SetColumn(idTextBlock, 0);
            appearance.Children.Add(idTextBlock);


        }
        protected virtual void SelectBorderSetup()
        {
            selectBorder = new Border();
            selectBorder.Background = Brushes.Transparent;
            selectBorder.BorderBrush = Brushes.Red;
            selectBorder.BorderThickness = new Thickness(0);
            Panel.SetZIndex(selectBorder,0);

            Grid.SetColumn(selectBorder, 1);
            appearance.Children.Add(selectBorder);
        }
        public Grid GetAppearance() => appearance;
        public int GetNumber() => id;
        public void SetId(int id)
        {
            this.id = id;
            idTextBlock.Text = id.ToString();
        }
        public void SetSlideClickEventHandler(MouseButtonEventHandler handler)
        {
            SlideClickEventHandler += handler;
        }
        protected void SlideClick(object sender, MouseButtonEventArgs e)
        {
            SlideClickEventHandler?.Invoke(this, e);
        }
        public void SetImage(ImageSource source, string imageName)
        {
            image.Source = source;
            image.Tag = imageName;
            frame.backgroundImage = imageName;
        }
        public void ActiveteSlide()
        {
            idTextBlock.Foreground = Brushes.Red;
            image.Margin = new Thickness(3);
            selectBorder.BorderThickness = new Thickness(3);
        }

        public void DeactivateSlide()
        {
            idTextBlock.Foreground = Brushes.Black;
            image.Margin = new Thickness(0);
            selectBorder.BorderThickness = new Thickness(0);
        }
        
        public BasicSlide DeepClone()
        {
            BasicSlide slide = new BasicSlide();
            slide.frame = this.frame.DeepClone();
            slide.image.Source = this.image.Source.CloneCurrentValue();
            return slide;
        }

    }
}
