using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace NovelkaCreationTool.Models
{
    public class Slide
    {
        public int Id { get; set; }
        public BitmapImage Image { get; set; }
        SlideData Data { get; set; } = new SlideData();
        public string Speaker
        {
            get => Data.Speaker;
            set => Data.Speaker = value;
        }
        public string Text
        {
            get => Data.Text;
            set => Data.Text = value;
        }
        public string BackgroundImageName
        {
            get => Data.BackgroundImageName;
            set => Data.BackgroundImageName = value;
        }
        

    }

    public class SlideData
    {
        public string Speaker { get; set; } = "";
        public string Text { get; set; } = "";
        public string BackgroundImageName { get; set; } = "";
    }
}
