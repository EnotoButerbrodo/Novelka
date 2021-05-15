using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
    }
}
