﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace NovelkaLib.Models
{
    [Serializable]
    public class Slide
    {
        public int Id { get; set; }
        [field:NonSerialized]
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
        public ObservableCollection<SlideImage> Images
        {
            get => Data.Images;
            set => Data.Images = value;
        }
    }
    [Serializable]
    public class SlideData
    {
        public string Speaker { get; set; } = "";
        public string Text { get; set; } = "";
        public ObservableCollection<SlideImage> Images = new ObservableCollection<SlideImage>();
    }
}
