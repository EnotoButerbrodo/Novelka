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
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NovelkaCreationTool.ViewModels
{
    public class SlidesViewModel : ViewModelBase
    {
        DirectoryInfo FolderPath = new DirectoryInfo("temp");
        public ObservableCollection<Slide> Slides { get; set; } = new ObservableCollection<Slide>();
        public ObservableCollection<Background> Backgrounds { get; set; } = new ObservableCollection<Background>();
        public ObservableCollection<SlideImage> SlideImages { get; set; } = new ObservableCollection<SlideImage>();
        Slide selectedSlide;
        Background selectedBackground;
        BitmapImage previewImage;

        public Slide SelectedSlide
        {
            get => selectedSlide;
            set => Set(ref selectedSlide, value);
        }  
        public Background SelectedBackground
        {
            get => selectedBackground;
            set => Set(ref selectedBackground, value);
        }
        public BitmapImage PreviewImage
        {
            get => previewImage;
            set => Set(ref previewImage, value);
        }

        

        #region AddSlideCommand

        public ICommand AddSlideCommand { get; }

        private void OnAddSlideCommandExecuted(object p)
        {
            var slide = new Slide
            {
                Id = Slides.Count + 1
            };
            Slides.Add(slide);
            SelectedSlide = slide;
            
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
                for (int i = 0; i < Slides.Count; i++)
                {
                    Slides[i].Id = i+1;
                }
                SelectedSlide = Slides.Last();
            }
        }
        private bool CanDeleteSlideCommandExecute(object p)
        {
            if (SelectedSlide == null) return false;
            return true;
        }

        #endregion
        #region LoadBackgroundsList

        public ICommand LoadBackgroundsListCommand { get; }

        private void OnLoadBackgroundsListExecuted(object p)
        {
             var files = FolderPath.GetFiles();
             foreach (var file in files)
             {
                Background background = new Background
                {
                    Name = file.Name,
                    FullName = file.FullName
                };
                Backgrounds.Add(background);
             }

        }
        private bool CanLoadBackgroundsListExecute(object p)
        {
            return true;
        }

        #endregion
        #region SetImageAsBackgroundCommand

        public ICommand SetImageAsBackgroundCommand { get; }

        private void OnSetImageAsBackgroundExecuted(object p)
        {
            SelectedSlide.BackgroundImageName = SelectedBackground.FullName;

        }
        private bool CanSetImageAsBackgroundExecute(object p)
        {
            return (SelectedBackground != null && SelectedSlide != null);
        }

        #endregion
        public SlidesViewModel()
        {
            #region Commands

            AddSlideCommand = new LambdaCommand(OnAddSlideCommandExecuted, CanAddSlideCommandExecute);
            DeleteSlideCommand = new LambdaCommand(OnDeleteSlideCommandExecuted, CanDeleteSlideCommandExecute);
            LoadBackgroundsListCommand = new LambdaCommand(OnLoadBackgroundsListExecuted, CanLoadBackgroundsListExecute);
            SetImageAsBackgroundCommand = new LambdaCommand(OnSetImageAsBackgroundExecuted, CanSetImageAsBackgroundExecute);
            #endregion
            SlideImages.Add(new SlideImage
            {
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                Width = 100,
                Height = 100,
                X = 0,
                Y = 0,
                Z = 0
            });
            SlideImages.Add(new SlideImage
            {
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                Width = 200,
                Height = 100,
                X = 300,
                Y = 0,
                Z = 0
            });
        }
    }
}
