using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Novelka.TextBlock
{

    public class TextBlock
    {
        System.Windows.Controls.TextBlock text;
        System.Windows.Controls.TextBlock speaker;
        Image mainImage;
        Image speakerImage;
        Grid Base;

        public TextBlock()
        {
            text = new System.Windows.Controls.TextBlock();
            mainImage = new Image()
            {
                Source = new BitmapImage(new Uri("Resources/template.png", UriKind.Relative))
            };
            speakerImage = new Image()
            {
                Source = new BitmapImage(new Uri("Resources/template.png", UriKind.Relative))
            };
            speaker = new System.Windows.Controls.TextBlock();
            GridSetup();
        }
        void GridSetup()
        {
            Base = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(200, 0, 200, 50)
            };
            ColumnDefinition c1 = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            ColumnDefinition c2 = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            ColumnDefinition c3 = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            RowDefinition r1 = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            RowDefinition r2 = new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            };
            RowDefinition r3 = new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            };
            Base.ColumnDefinitions.Add(c1);
            Base.ColumnDefinitions.Add(c2);
            Base.ColumnDefinitions.Add(c3);
            Base.RowDefinitions.Add(r1);
            Base.RowDefinitions.Add(r2);
            Base.RowDefinitions.Add(r3);


            Grid.SetRow(text, 1);
            Grid.SetColumn(text, 0);
            Grid.SetColumnSpan(text, 3);
            Base.Children.Add(text);

            Grid.SetRow(mainImage, 1);
            Grid.SetColumn(mainImage, 0);
            Grid.SetColumnSpan(mainImage, 3);
            Base.Children.Add(mainImage);

            Grid.SetRow(speaker, 0);
            Grid.SetColumn(speaker, 0);
            Base.Children.Add(speaker);

            Grid.SetRow(speaker, 0);
            Grid.SetColumn(speaker, 0);
            Base.Children.Add(speakerImage);

            
            //appearance.PreviewMouseLeftButtonUp += SlideClick;

        }
        public Grid GetBase() => Base;
    }
    public class TextBoxConfig
    {
        public int textWidth;
        public int textHeigh;
        public int speakerWidth;
        public int speakerHeight;
        public ImageSource mainImage;
        public ImageSource speakerImage;

    }


}
