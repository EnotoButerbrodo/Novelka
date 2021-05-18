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

namespace NovelkaCreationTool.ViewModels
{
    public class SlidesViewModel : ViewModelBase
    {
        DirectoryInfo FolderPath = new DirectoryInfo("temp");
        public ObservableCollection<Slide> Slides { get; set; } = new ObservableCollection<Slide>();
        public ObservableCollection<string> Backgrounds { get; set; } = new ObservableCollection<string>();
        Slide selectedSlide;
        string selectedBackground;
        BitmapImage previewImage;

        public Slide SelectedSlide
        {
            get => selectedSlide;
            set => Set(ref selectedSlide, value);
        }  
        public string SelectedBackground
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
                Backgrounds.Add(file.Name);
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
            SelectedSlide.BackgroundImageName = SelectedBackground;
            PreviewImage = LoadImage($"{FolderPath.FullName}\\{SelectedBackground}");

        }
        private bool CanSetImageAsBackgroundExecute(object p)
        {
            return (SelectedBackground != null && SelectedSlide != null);
        }

        BitmapImage LoadImage(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                BitmapImage image = new BitmapImage();
                image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = fs;
                image.EndInit();
                image.Freeze();
                return image;
            }

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
        }
    }
}
