using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using NovelkaCreationTool.Models;
using NovelkaCreationTool.ViewModels.Base;
using System.Windows.Input;
using NovelkaCreationTool.Commands;
using System.Linq;
using System.IO;
using NovelkaCreationTool.Views;
using System.Windows;
using NovelkaCreationTool.Infrastructure.Commands;

namespace NovelkaCreationTool.ViewModels
{
    public class SlidesViewModel : ViewModelBase
    {
        DirectoryInfo FolderPath = new DirectoryInfo("temp");
        public ObservableCollection<Slide> Slides { get; set; } = new ObservableCollection<Slide>();
        public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<SlideImage> SlideImages { get; set; } = new ObservableCollection<SlideImage>();
        Slide selectedSlide;
        SlideImage selectedSlideImage;
        string selectedImage;

        int previewWidth;
        int previewHeight;

        public Slide SelectedSlide
        {
            get => selectedSlide;
            set => Set(ref selectedSlide, value);
        }
        public SlideImage SelectedSlideImage
        {
            get => selectedSlideImage;
            set => Set(ref selectedSlideImage, value);
        }
        public string SelectedImage
        {
            get => selectedImage;
            set => Set(ref selectedImage, value);
        }

        public int PreviewWidth
        {
            get => previewWidth;
            set => Set(ref previewWidth, value);
        }
        public int PreviewHeight
        {
            get => previewHeight;
            set => Set(ref previewHeight, value);
        }

        AddSlideImageDialogWindow addSlideImageDialogWindow;


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
                    Slides[i].Id = i + 1;
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
                Images.Add(file.FullName);
            }

        }
        private bool CanLoadBackgroundsListExecute(object p)
        {
            return true;
        }

        #endregion
        #region AddImageToSlideCommand
        public ICommand AddImageToSlideCommand { get; }
        void OnAddImageToSlideCommandEx(object p)
        {
            SelectedSlide.Images.Add(new SlideImage
            {
                Name = Path.GetFileName(SelectedImage),
                ImageName = SelectedImage,
                Height = 200,
                Width = 200,
                X = 0,
                Y = 0,
                Z = 0
            });
        }
        bool CanAddImageToSlideCommandEx(object p)
        {
            return (SelectedImage != null && SelectedSlide != null);
        }
        #endregion
        #region SetAsBackgroundImageCommand
        public ICommand SetAsBackgroundImageCommand { get; }
        void OnSetAsBackgroundImageCommandEx(object p)
        {
            SelectedSlideImage.Width = PreviewWidth;
            SelectedSlideImage.Height = previewHeight;
            SelectedSlideImage.X = 0;
            SelectedSlideImage.Y = 0;
            OnPropertyChanged(nameof(selectedSlideImage));
            //OnPropertyChanged(nameof(SlideImages));
        }
        bool CanSetAsBackgroundImageCommandEx(object p)
        {
            return (SelectedSlideImage != null && SelectedSlide != null);
        }
        #endregion
        #region LoadImagesListAsyncCommand

        public ICommand LoadImagesListAsyncCommand { get; }

        private void OnLoadImagesListAsyncCommandExecuted(object p)
        {
            var files = FolderPath.GetFiles();
            foreach (var file in files)
            {
                Images.Add(file.FullName);
            }

        }
        private bool CanLoadImagesListAsyncCommandExecute(object p)
        {
            return true;
        }

        #endregion

        public ICommand AddNewSlideImageCommand { get; }
        private void OnAddNewSlideImageCommandEx(object p)
        {
            var window = new AddSlideImageDialogWindow
            {
                Owner = Application.Current.MainWindow
            };
            addSlideImageDialogWindow = window;
            addSlideImageDialogWindow.ShowDialog();
        }
        public SlidesViewModel()
        {
            #region Commands

            AddSlideCommand = new LambdaCommand(OnAddSlideCommandExecuted, CanAddSlideCommandExecute);
            DeleteSlideCommand = new LambdaCommand(OnDeleteSlideCommandExecuted, CanDeleteSlideCommandExecute);
            LoadBackgroundsListCommand = new LambdaCommand(OnLoadBackgroundsListExecuted, CanLoadBackgroundsListExecute);
            AddNewSlideImageCommand = new LambdaCommand(OnAddNewSlideImageCommandEx, (obj)=> { return true; });
            AddImageToSlideCommand = new LambdaCommand(OnAddImageToSlideCommandEx, CanAddImageToSlideCommandEx);
            SetAsBackgroundImageCommand = new LambdaCommand(OnSetAsBackgroundImageCommandEx, CanSetAsBackgroundImageCommandEx);
            LoadImagesListAsyncCommand = new AsyncLambdaCommand(OnLoadImagesListAsyncCommandExecuted, CanLoadImagesListAsyncCommandExecute);
            #endregion
            PreviewHeight = 480;
            PreviewWidth = 720;
            Slides.Add(new Slide
            {
                Id = 1
            });
            Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 1",
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                X = 200,
                Y = 200,
                Height = 200,
                Width = 200

            });
            Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 2",
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                X = 300,
                Y = 0,
                Height = 100,
                Width = 100

            });
            Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 1",
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                X = 200,
                Y = 200,
                Height = 200,
                Width = 200

            });
            Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 2",
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                X = 300,
                Y = 0,
                Height = 100,
                Width = 100

            });

        }
    }
}
