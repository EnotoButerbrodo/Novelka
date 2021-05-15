using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovelkaCreator.Model;

namespace NovelkaCreator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Slide> Slides { get; set; } = new ObservableCollection<Slide>();
        Slide selectedSlide;
        public Slide SelectedSlide
        {
            get => selectedSlide;
            set
            {
                selectedSlide = value;
                OnPropertyChanged(nameof(SelectedSlide));
            }
        }
    }
}
