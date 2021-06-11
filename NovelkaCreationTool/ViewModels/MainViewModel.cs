using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using NovelkaCreationTool.Commands;
using NovelkaCreationTool.Infrastructure.Commands;
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
                Width = 720,
                Height = 480
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
        

        #region Variables
        Slide selectedSlide;
        SlideImage selectedSlideImage;
        string selectedImage;

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

        double mousePreviewX, mousePreviewY;
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
        #region AddImageToSlideCommand
        public ICommand AddImageToSlideCommand { get; }
        void OnAddImageToSlideCommandEx(object p)
        {
            SelectedSlide.Images.Add(new SlideImage
            {
                Name = Path.GetFileName(SelectedImage),
                ImageName = SelectedImage,
                Width = 300,
                Height = 300,
                X = 0,
                Y = 0,
                Z = SelectedSlide.Images.Count
            });
            CurrentProject.UsingImages.Add(SelectedImage);
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
            SelectedSlideImage.Width = CurrentProject.Settings.Width;
            SelectedSlideImage.Height = CurrentProject.Settings.Height;
            SelectedSlideImage.X = -1;
            SelectedSlideImage.Y = -1;
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
                catch (Exception)
                {
                    MessageBox.Show("Не удалось загрузить проект");
                }
            }
        }
        #endregion
        #region ExsportCommand

        #endregion

        #endregion


        void SwapImagesZPosition(int firstIndex, int secondIndex)
        {
            SelectedSlide.Images[firstIndex].Z = secondIndex;
            SelectedSlide.Images[secondIndex].Z = firstIndex;
            SelectedSlide.Images.Move(firstIndex, secondIndex);
        }


        public MainViewModel()
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
            SaveCommand = new RelayCommand(OnSaveCommandEx, (obj) => true);
            OpenProjectCommand = new RelayCommand(OnOpenProjectCommandEx, (obj) => true);
            #endregion
            string imageName;
            CurrentProject.Slides.Add(new Slide
            {
                Id = 1
            });
            imageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg";
            CurrentProject.UsingImages.Add(imageName);
            CurrentProject.Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 1",
                ImageName = imageName,
                X = 200,
                Y = 200,
                Z = CurrentProject.Slides[0].Images.Count,
                Height = 200,
                Width = 200

            });
            imageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg";
            CurrentProject.UsingImages.Add(imageName);
            CurrentProject.Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 2",
                ImageName = imageName,
                X = 300,
                Y = 0,
                Z = CurrentProject.Slides[0].Images.Count,
                Height = 100,
                Width = 100

            });
            

        }
    }
}
