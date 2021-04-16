using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Frame = Novelka.Plot.Frame;
using NovelkaCreator.Slide;
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
        List<BasicSlide> Slides = new List<BasicSlide>(10);
        public MainWindow()
        {
            InitializeComponent();
            AddSlide(new MonikaSlide("1"));
            AddSlide(new MonikaSlide("2"));
            AddSlide(new AddNewSlide());
        }

        void AddSlide(BasicSlide slide)
        {
            Slides.Add(slide);
            SlidesPanel.Children.Add(slide.GetAppearance());
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
