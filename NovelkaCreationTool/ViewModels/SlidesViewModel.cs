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
using System.Collections;
using System.Windows.Controls;
using GongSolutions.Wpf.DragDrop;
using System.Windows.Data;
using System;

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

        double mouseX, mouseY;
        double mousePreviewX, mousePreviewY;
        public double MouseX
        {
            get => mouseX;
            set => Set(ref mouseX, value);
        }
        public double MouseY
        {
            get => mouseY;
            set => Set(ref mouseY, value);
        }
        public double MousePreviewX
        {
            get => mousePreviewX;
            set => Set(ref mousePreviewX, value);
        }
        public double MousePreviewY
        {
            get => mousePreviewY;
            set => Set(ref mousePreviewY, value);
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

        #region Commands

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
                Z = SelectedSlide.Images.Count
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
            SwapImagesZPosition(SelectedSlide.Images.IndexOf(SelectedSlideImage), 0);
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
        #region IncreaseImageZCommand
        public ICommand IncreaseImageZCommand { get; }
        void OnIncreaseImageZCommandEx(object p)
        {
            int index = SelectedSlide.Images.IndexOf(SelectedSlideImage);
            int newIndex = index + 1;
            SwapImagesZPosition(index, newIndex);
        }
        bool CanIncreaseImageZCommandEx(object p)
        {
            if (SelectedSlide != null)
                if (SelectedSlide.Images.IndexOf(SelectedSlideImage) < SelectedSlide.Images.Count - 1)
                    return true;
            return false;
        }
        #endregion
        #region DecreaseImageZCommand
        public ICommand DecreaseImageZCommand { get; }
        void OnDecreaseImageZCommandEx(object p)
        {
            int index = SelectedSlide.Images.IndexOf(SelectedSlideImage);
            int newIndex = index - 1;
            SwapImagesZPosition(index, newIndex);
        }
        bool CanDecreaseImageZCommandEx(object p)
        {
            if (SelectedSlide != null)
                if (SelectedSlide.Images.IndexOf(SelectedSlideImage) > 0)
                    return true;
            return false;
        }

        #endregion
        public ICommand StartDrag { get; }
        void OnStartDragEx(object p)
        {
            SelectedSlideImage.IsDrag = true;
        }

        public ICommand Drag { get; }
        void OnDragEx(object p)
        {
            if (SelectedSlideImage.IsDrag)
            {
                SelectedSlideImage.X = Convert.ToInt32(MousePreviewX) - SelectedSlideImage.Width/2;
                SelectedSlideImage.Y = Convert.ToInt32(MousePreviewY) - SelectedSlideImage.Height / 2;
            }
        }
        public ICommand StopDrag { get; }
        void OnStopDragEx(object p)
        {
            SelectedSlideImage.IsDrag = false;
        }
        #endregion

        

        void SwapImagesZPosition(int firstIndex, int secondIndex)
        {
            SelectedSlide.Images[firstIndex].Z = secondIndex;
            SelectedSlide.Images[secondIndex].Z = firstIndex;
            SelectedSlide.Images.Move(firstIndex, secondIndex);
        }


        public SlidesViewModel()
        {
            #region Commands

            AddSlideCommand = new RelayCommand(OnAddSlideCommandExecuted, CanAddSlideCommandExecute);
            DeleteSlideCommand = new RelayCommand(OnDeleteSlideCommandExecuted, CanDeleteSlideCommandExecute);
            LoadBackgroundsListCommand = new RelayCommand(OnLoadBackgroundsListExecuted, CanLoadBackgroundsListExecute);
            AddImageToSlideCommand = new RelayCommand(OnAddImageToSlideCommandEx, CanAddImageToSlideCommandEx);
            SetAsBackgroundImageCommand = new RelayCommand(OnSetAsBackgroundImageCommandEx, CanSetAsBackgroundImageCommandEx);
            LoadImagesListAsyncCommand = new AsyncLambdaCommand(OnLoadImagesListAsyncCommandExecuted, CanLoadImagesListAsyncCommandExecute);
            IncreaseImageZCommand = new RelayCommand(OnIncreaseImageZCommandEx, CanIncreaseImageZCommandEx);
            DecreaseImageZCommand = new RelayCommand(OnDecreaseImageZCommandEx, CanDecreaseImageZCommandEx);
            StartDrag = new RelayCommand(OnStartDragEx, (obj) => { return SelectedSlideImage != null; });
            Drag = new RelayCommand(OnDragEx, (obj) => { return SelectedSlideImage != null; });
            StopDrag = new RelayCommand(OnStopDragEx, (obj) => { return SelectedSlideImage != null; });
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
                Z = Slides[0].Images.Count,
                Height = 200,
                Width = 200

            });
            Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 2",
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                X = 300,
                Y = 0,
                Z = Slides[0].Images.Count,
                Height = 100,
                Width = 100

            });
            Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 4",
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                X = 200,
                Y = 200,
                Z = Slides[0].Images.Count,
                Height = 200,
                Width = 200

            });
            Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 3",
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                X = 300,
                Y = 0,
                Z = Slides[0].Images.Count,
                Height = 100,
                Width = 100

            });

        }
    }
}
