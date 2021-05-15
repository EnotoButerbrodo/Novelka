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
        }
        private bool CanAddSlideCommandExecute(object p)
        {
            return true;
        }

        #endregion
        public SlidesViewModel()
        {
            #region Commands

            AddSlideCommand = new LambdaCommand(OnAddSlideCommandExecuted, CanAddSlideCommandExecute);

            #endregion
        }
    }
}
