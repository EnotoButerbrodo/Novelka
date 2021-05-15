using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;
using NovelkaCreationTool.Models;
using NovelkaCreationTool.ViewModels.Base;

namespace NovelkaCreationTool.ViewModels
{
    public class SlidesViewModel : ViewModelBase
    {
        
        public ObservableCollection<Slide> Slides { get; set; }
        Slide selectedSlide;
        public Slide SelectedSlide
        {
            get => selectedSlide;
            set => Set(ref selectedSlide, value);
        }

        public SlidesViewModel()
        {
            Slides = new ObservableCollection<Slide>();
            Slide slide = new Slide
            {
                Image = new BitmapImage(new Uri("Resources\\Default\\SlideDefaultImage.png", UriKind.Relative)),
                Id = Slides.Count + 1
            };
            Slides.Add(slide); 
        }
    }
}
