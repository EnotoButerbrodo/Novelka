using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Frame = Novelka.Plot.Frame;
using NovelkaCreator.Slide;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Windows.Media;

namespace NovelkaCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Создать новый проект
        //Создать директорию под хранение файлов
        //Вначале создается временная директория 
        static DirectoryInfo tempDirectoryInfo = new DirectoryInfo("temp");
        static DirectoryInfo currentProjectPath = tempDirectoryInfo;
        LinkedList<BasicSlide> Slides = new LinkedList<BasicSlide>();
        static BasicSlide selectedSlide;
        static BasicSlide prevSelectedSlide;

        static FileSystemWatcher ImagesFileWathcer;
        public MainWindow()
        {
            InitializeComponent();
            ImagesFileWathcerSetup();
        }

        void ImagesFileWathcerSetup()
        {
            ImagesFileWathcer = new FileSystemWatcher(currentProjectPath.FullName)
            {
                NotifyFilter = NotifyFilters.DirectoryName
                               | NotifyFilters.FileName

            };
            ImagesFileWathcer.EnableRaisingEvents = true;
            ImagesFileWathcer.Created += new FileSystemEventHandler(ImagesDirectoryChanged);
        }

        void ImagesDirectoryChanged(object sender, FileSystemEventArgs e)
        {
            
        }
        void AddSlide(BasicSlide slide)
        {
            Slides.AddFirst(slide);
            SlidesPanel.Children.Add(slide.GetAppearance());
        }
        void DeleteSlide()
        {
            SlidesPanel.Children.Remove(selectedSlide.GetAppearance());
            Slides.Remove(selectedSlide);
            SlidesScrollBar.Maximum = Slides.Count - 1;
        }
        void CreateTempDirectory()
        {
            if (!tempDirectoryInfo.Exists)
                tempDirectoryInfo.Create();
        }
        void UpdateTempDirectory()
        {
            Directory.Delete(tempDirectoryInfo.FullName);
            CreateTempDirectory();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                MessageBox.Show(sfd.FileName);
            }

        }


        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.png;*.jpeg)|*.png;*.jpeg";
            if (ofd.ShowDialog() == true)
            {
                File.Copy(ofd.FileName,
                    $"{currentProjectPath.FullName}\\{Path.GetFileName(ofd.FileName)}");
            }
        }

        private void AddTestSlide_Click(object sender, RoutedEventArgs e)
        {
            var newSlide = new BasicSlide(Slides.Count+1);
            newSlide.SetSlideClickEventHandler(ChangeSelectedSlide);
            AddSlide(newSlide);
            SlidesScrollBar.Maximum = Slides.Count - 1;
        }

        private void DeleteTestSlide_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSlide == null) return;
            DeleteSlide();
            ResetSlidesId();

            
        }

        private void ResetSlidesId()
        {
            int id = Slides.Count;
            foreach(var slide in Slides)
            {
                slide.SetId(id--);
            }
        }

        public void ChangeSelectedSlide(object sender, MouseButtonEventArgs e)
        {
            prevSelectedSlide = selectedSlide ;
            selectedSlide = sender as BasicSlide;

            prevSelectedSlide?.DeactivateSlide();
            selectedSlide.ActiveteSlide();
            
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        async void LoadBackgoundImagesNames()
        {
            await Task.Factory.StartNew(async () =>
            {
                var files = currentProjectPath.GetFiles();
                foreach (var file in files)
                {
                    await BackgroundImageListBox.Dispatcher.InvokeAsync(() =>
                    {
                        BackgroundImageListBox.Items.Add(file.Name);
                    });
                }
            });
        }

        async private void BackgroundImageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MainPreviewImage.Source = new BitmapImage(new Uri("Resources//template.png", UriKind.Relative));
            string uri = $"{currentProjectPath.FullName}\\{e.AddedItems[0]}";

            MainPreviewImage.Source = await LoadImage(uri);

          
        }

        async Task<BitmapImage> LoadImage(string path)
        {
            return await Task.Run(() =>
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

            });
        }




        private void Background_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBackgoundImagesNames();
        }
    }
}
