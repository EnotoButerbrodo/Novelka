using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Newtonsoft.Json;
using NovelkaCreationTool.Commands;
using NovelkaCreationTool.Infrastructure.Commands;
using NovelkaLib;
using NovelkaLib.Infrastructure;
using NovelkaLib.Models;
using NovelkaLib.ViewModels;

#pragma warning disable SYSLIB0011 // Тип или член устарел

namespace NovelkaCreationTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        Project currentProject;
        readonly Project defaultProject = new()
        {
            Name = "Unnamed",
            Settings = new()
            {
                Width = 1920,
                Height = 1080
            },
            TextBox = new()
            {
                SpeakerWidth = 300,
                SpeakerHeight = 50,
                TextHeight = 300,
                TextWidth = 1800,
                FontSize = 30
            }
        };
        public Project CurrentProject
        {
            get
            {
                if(currentProject == null)
                {
                    currentProject = defaultProject;
                }
                return currentProject;
            }
            set
            {
                currentProject = value;
                OnPropertyChanged(nameof(CurrentProject));
            }
        }
        public ObservableCollection<string> Images
        {
            get => CurrentProject.Images;
            set => Set(ref CurrentProject.Images, value);
        }
        public DirectoryInfo FolderPath = new("temp");
      

        public static Storage<BitmapImage> SlideObjectsStorage = new();

        #region Variables
        Slide selectedSlide;
        SlideObject selectedSlideObject;
        string selectedImage;

        public Slide SelectedSlide
        {
            get => selectedSlide;
            set => Set(ref selectedSlide, value);
        }
        public SlideObject SelectedSlideObject
        {
            get => selectedSlideObject;
            set => Set(ref selectedSlideObject, value);
        }
        public string SelectedImage
        {
            get => selectedImage;
            set => Set(ref selectedImage, value);
        }

        UIElement previewViewbox;
        public UIElement PreviewViewbox
        {
            get => previewViewbox;
            set
            {
                if(value != null)
                Set(ref previewViewbox, value);
            }
        }
        #endregion
        #region Commands

        #region AddSlideCommand

        public ICommand AddSlideCommand { get; }

        private void OnAddSlideCommandExecuted(object p)
        {
            var slide = new Slide
            {
                Id = CurrentProject.Slides.Count + 1
            };
            CurrentProject.Slides.Add(slide);
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
            CurrentProject.Slides.Remove(SelectedSlide);
            if (CurrentProject.Slides.Count > 0)
            {
                for (int i = 0; i < CurrentProject.Slides.Count; i++)
                {
                    CurrentProject.Slides[i].Id = i + 1;
                }
                SelectedSlide = CurrentProject.Slides.Last();
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
        #region AddImageToSlideAtNewObjectCommand
        public ICommand AddImageToSlideAtNewObjectCommand { get; }
        void OnAddImageToSlideAtNewObjectCommandEx(object p)
        {
            SlideObject newSlideImage = new()
            {
                Name = $"Object{SelectedSlide.Images.Count}",
                ImageName = selectedImage,
                X = 0, 
                Y = 0,
                Z = SelectedSlide.Images.Count
            };
            BitmapImage loadedImage;
            if (!SlideObjectsStorage.ContainsItem(newSlideImage.Name))
            {
                var imageTask = FileLoader.LoadBitmapImageAsync(newSlideImage.ImageName);
                Task.WaitAll(imageTask);
                loadedImage = imageTask.Result;
                SlideObjectsStorage.Add(newSlideImage.Name, loadedImage);
            }
            else
                loadedImage = SlideObjectsStorage.GetItem(newSlideImage.Name);

            newSlideImage.Height = loadedImage.Height;
            newSlideImage.Width = loadedImage.Width;


            SelectedSlide.Images.Add(newSlideImage);
            CurrentProject.UsingImages.Add(SelectedImage);
        }
        bool CanAddImageToSlideAtNewObjectCommandEx(object p)
        {
            return (SelectedImage != null && SelectedSlide != null);
        }
        #endregion
        #region AddImageToCurrentSlideObjectCommand
        public ICommand AddImageToCurrentSlideObjectCommand { get; }
        void OnAddImageToCurrentSlideObjectCommandEx(object p)
        {
            SelectedSlideObject.ImageName = SelectedImage;
        }
        bool CanAddImageToCurrentSlideObjectCommandEx(object p)
        {
            return (SelectedImage != null && SelectedSlideObject != null);
        }
        #endregion
        #region DeleteSlideImageCommand
        public ICommand DeleteSlideImageCommand { get; }

        void OnDeleteSlideImageCommandEx(object p)
        {
            SelectedSlide.Images.Remove(SelectedSlideObject);
        }

        bool CanDeleteSlideImageCommandEx(object p)
        {
            if (SelectedSlideObject == null || SelectedSlide == null) return false;
            return true;
        }
        #endregion
        #region SetAsBackgroundImageCommand
        public ICommand SetAsBackgroundImageCommand { get; }
        void OnSetAsBackgroundImageCommandEx(object p)
        {
            SelectedSlideObject.Width = CurrentProject.Settings.Width;
            SelectedSlideObject.Height = CurrentProject.Settings.Height;
            SelectedSlideObject.X = -1;
            SelectedSlideObject.Y = -1;
            SelectedSlideObject.Name = "Background";
            SwapImagesZPosition(SelectedSlide.Images.IndexOf(SelectedSlideObject), 0);
        }
        bool CanSetAsBackgroundImageCommandEx(object p)
        {
            return (SelectedSlideObject != null && SelectedSlide != null);
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
            int index = SelectedSlide.Images.IndexOf(SelectedSlideObject);
            int newIndex = index + 1;
            SwapImagesZPosition(index, newIndex);
        }
        bool CanIncreaseImageZCommandEx(object p)
        {
            if (SelectedSlide != null && SelectedSlideObject != null)
                if (SelectedSlide.Images.IndexOf(SelectedSlideObject) < SelectedSlide.Images.Count - 1)
                    return true;
            return false;
        }
        #endregion
        #region DecreaseImageZCommand
        public ICommand DecreaseImageZCommand { get; }
        void OnDecreaseImageZCommandEx(object p)
        {
            int index = SelectedSlide.Images.IndexOf(SelectedSlideObject);
            int newIndex = index - 1;
            SwapImagesZPosition(index, newIndex);
        }
        bool CanDecreaseImageZCommandEx(object p)
        {
            if (SelectedSlide != null)
                if (SelectedSlide.Images.IndexOf(SelectedSlideObject) > 0)
                    return true;
            return false;
        }

        #endregion
        #region SaveCommand
        public ICommand SaveCommand { get; }
        void OnSaveCommandEx(object p)
        {
            BinaryFormatter formater = new();
            SaveFileDialog sfd = new()
            {
                DefaultExt = ".nct"
            };

            if (sfd.ShowDialog() == true)
            {

                 CurrentProject.Name = sfd.FileName;
                 using StreamWriter file = new(sfd.FileName);
                 string json = JsonConvert.SerializeObject(CurrentProject);
                 file.Write(json);
            }
        }
        #endregion
        #region OpenProjectCommand
        public ICommand OpenProjectCommand { get; }
        void OnOpenProjectCommandEx(object p)
        {
            OpenFileDialog ofd = new()
            {
                Filter = "Проект Novelka|*.nct"
            };
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    using StreamReader fs = new(ofd.FileName);
                    string json = fs.ReadToEnd();
                    CurrentProject = JsonConvert.DeserializeObject<Project>(json);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Не удалось загрузить проект. {e.Message}");
                }
            }
        }
        #endregion
        #region ExsportCommand

        #endregion
        #region CopySlideCommand
        public ICommand CopySlideCommand { get; }
        void OnCopySlideCommandEx(object p)
        {
            var copyedSlide = CopyObject<Slide>(SelectedSlide);
            CurrentProject.Slides.Add(copyedSlide);
            SelectedSlide = copyedSlide;
        }
        bool CanCopySlideCommandEx(object p)
        {
            return (SelectedSlide != null);
        }
        #endregion

        public static object GetImageFromPath(object value, object parameter)
        {
            string imagePath = value as string;
            string imageName = Path.GetFileNameWithoutExtension(imagePath);
            if (SlideObjectsStorage.ContainsItem(imageName))
                return SlideObjectsStorage.GetItem(imageName);


            var loadedImage = FileLoader.LoadBitmapImage(imagePath);
            SlideObjectsStorage.Add(imageName, loadedImage);
            return loadedImage;

        }

        public RenderTargetBitmap CreateBitmapFromVisual(Visual target)
        {
            if (target == null)
            {
                throw new Exception("Target = null");
            }

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(target);
                context.DrawRectangle(visualBrush, null, new Rect(new System.Windows.Point(), bounds.Size));
            }

            renderTarget.Render(visual);
            return renderTarget;
        }

        #endregion

        void SwapImagesZPosition(int firstIndex, int secondIndex)
        {
            SelectedSlide.Images[firstIndex].Z = secondIndex;
            SelectedSlide.Images[secondIndex].Z = firstIndex;
            SelectedSlide.Images.Move(firstIndex, secondIndex);
        }

        static T CopyObject<T>(T copyedObject)
        {
            string json = JsonConvert.SerializeObject(copyedObject);
            return JsonConvert.DeserializeObject<T>(json);
        }


        public MainViewModel()
        {
            #region Commands

            AddSlideCommand = new RelayCommand(OnAddSlideCommandExecuted, CanAddSlideCommandExecute);
            DeleteSlideCommand = new RelayCommand(OnDeleteSlideCommandExecuted, CanDeleteSlideCommandExecute);
            LoadBackgroundsListCommand = new RelayCommand(OnLoadBackgroundsListExecuted, CanLoadBackgroundsListExecute);
            AddImageToSlideAtNewObjectCommand = new RelayCommand(OnAddImageToSlideAtNewObjectCommandEx, CanAddImageToSlideAtNewObjectCommandEx);
            SetAsBackgroundImageCommand = new RelayCommand(OnSetAsBackgroundImageCommandEx, CanSetAsBackgroundImageCommandEx);
            LoadImagesListAsyncCommand = new AsyncLambdaCommand(OnLoadImagesListAsyncCommandExecuted, CanLoadImagesListAsyncCommandExecute);
            IncreaseImageZCommand = new RelayCommand(OnIncreaseImageZCommandEx, CanIncreaseImageZCommandEx);
            DecreaseImageZCommand = new RelayCommand(OnDecreaseImageZCommandEx, CanDecreaseImageZCommandEx);
            SaveCommand = new RelayCommand(OnSaveCommandEx, (obj) => true);
            OpenProjectCommand = new RelayCommand(OnOpenProjectCommandEx, (obj) => true);
            DeleteSlideImageCommand = new RelayCommand(OnDeleteSlideImageCommandEx, CanDeleteSlideImageCommandEx);
            CopySlideCommand = new RelayCommand(OnCopySlideCommandEx, CanCopySlideCommandEx);
            AddImageToCurrentSlideObjectCommand = new RelayCommand(OnAddImageToCurrentSlideObjectCommandEx, CanAddImageToCurrentSlideObjectCommandEx);
            #endregion
        }
    }
}
