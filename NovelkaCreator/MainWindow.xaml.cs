using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Frame = Novelka.Plot.Frame;

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
        List<Frame> Frames = new List<Frame>(10);
        List<BitmapImage> Images = new List<BitmapImage>();
        public MainWindow()
        {
            InitializeComponent();
            CreateTempDirectory();

        }

        void CreateTempDirectory()
        {
            if(!tempDirectoryInfo.Exists)
            tempDirectoryInfo.Create();
        }
        void UpdateTempDirectory()
        {
            Directory.Delete(tempDirectoryInfo.FullName);
            CreateTempDirectory();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitImage = new BitmapImage();
            bitImage.BeginInit();
            bitImage.UriSource = new Uri(@"Resources/template.png", UriKind.Relative);
            bitImage.EndInit();
            Image image = new Image();
            image.Source = bitImage;
            image.Margin = new Thickness(20, 0, 20, 0);
            FramesBlock.Children.Add(image);
            FramesBlockScroll.ScrollToHorizontalOffset(FramesBlockScroll.ContentHorizontalOffset);
            //FramesBlock.Children.Add(new Button() { Width = 200, Height = 200, Content = "+" });
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                MessageBox.Show(sfd.FileName);
            }

        }

        void CreateNewFrame()
        {
            var newFrame = new Frame();
            
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.png;*.jpeg)|*.png;*.jpeg";
            if(ofd.ShowDialog() == true)
            {
                File.Copy(ofd.FileName, 
                    $"{currentProjectPath.FullName}\\{Path.GetFileName(ofd.FileName)}");
            }
        }
    }
}
