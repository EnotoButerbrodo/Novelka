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
        short slide_number = 1;
        static BasicSlide selectedSlide;
        static BasicSlide prevSelectedSlide;
        public MainWindow()
        {
            InitializeComponent();
        }

        void AddSlide(BasicSlide slide)
        {
            Slides.Add(slide);
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
            var newSlide = new MonikaSlide(Slides.Count+1);
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
            for(int id = 0; id< Slides.Count; id++)
            {
                Slides[id].SetId(id+1);
            }
        }

        public void ChangeSelectedSlide(object sender, MouseButtonEventArgs e)
        {
            prevSelectedSlide = selectedSlide ;
            selectedSlide = sender as BasicSlide;

            prevSelectedSlide?.DeactivateSlide();
            selectedSlide.ActiveteSlide();
            
        }
    }
}
