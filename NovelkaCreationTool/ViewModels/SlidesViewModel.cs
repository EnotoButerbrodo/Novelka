using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;
using NovelkaCreationTool.Models;
using NovelkaCreationTool.ViewModels.Base;
using NovelkaCreationTool.Commands.Base;
using System.Windows.Input;
using NovelkaCreationTool.Commands;
using System.Diagnostics;
using System.Linq;

namespace NovelkaCreationTool.ViewModels
{
    public class SlidesViewModel : ViewModelBase
    {

        public ObservableCollection<Slide> Slides { get; set; } = new ObservableCollection<Slide>();
        Slide selectedSlide;
        public Slide SelectedSlide
        {
            get => selectedSlide;
            set => Set(ref selectedSlide, value);
        }

        #region AddSlideCommand

        public ICommand AddSlideCommand { get; }

        private void OnAddSlideCommandExecuted(object p)
        {
            Slides.Add(new Slide
            {
                Id = Slides.Count + 1
            });
            Debug.WriteLine(Slides.Count);
        }
        private bool CanAddSlideCommandExecute(object p)
        {
            return true;
        }

        #endregion
        #region DeleteSlideCommand

        public ICommand DeleteSlideCommand { get; }

        private void OnDeleteSlideCommandExecuted(object p)
        {
            Slides.Remove(SelectedSlide);
            if (Slides.Count > 0)
            {
                SelectedSlide = Slides.Last();
            }
        }
        private bool CanDeleteSlideCommandExecute(object p)
        {
            if (SelectedSlide == null) return false;
            return true;
        }

        #endregion
        public SlidesViewModel()
        {
            #region Commands

            AddSlideCommand = new LambdaCommand(OnAddSlideCommandExecuted, CanAddSlideCommandExecute);
            DeleteSlideCommand = new LambdaCommand(OnDeleteSlideCommandExecuted, CanDeleteSlideCommandExecute);
            #endregion
        }
    }
}
