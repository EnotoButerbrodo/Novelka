using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using NovelkaCreator.Slide;

namespace NovelkaCreator
{
    public partial class MainWindow
    {
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
        async public void ChangeSelectedSlide(object sender, MouseButtonEventArgs e)
        {
            prevSelectedSlide = selectedSlide;
            selectedSlide = sender as BasicSlide;

            prevSelectedSlide?.DeactivateSlide();
            selectedSlide.ActiveteSlide();
            
            if (selectedSlide.frame.backgroundImage == null)
            {
                MainPreviewImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/BlankTemplate.png"));
            }
            else
            {
                string uri = $"{currentProjectPath}//{selectedSlide.frame.backgroundImage}";
                MainPreviewImage.Source = await LoadImageAsync(uri);
            }
        }

        //Когда в списке задних фонов выбираем элемент
        async private void BackgroundImageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BackgroundImageListBox.IsEnabled = false;
            string uri = $"{currentProjectPath.FullName}\\{BackgroundImageListBox.SelectedItem}";

            MainPreviewImage.Source = await LoadImageAsync(uri);
            BackgroundImageListBox.IsEnabled = true;
        }
        private void SetAsBackgroundImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSlide == null) return;

            selectedSlide.SetImage(CreateBitmapFromVisual(MainPreviewArea),
                BackgroundImageListBox.SelectedItem.ToString());
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
        private void Background_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBackgoundImagesNames();
        }

    }
}
