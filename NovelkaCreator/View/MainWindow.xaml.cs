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
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using NovelkaCreator.ViewModel;

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
        public static DirectoryInfo tempDirectoryInfo = new DirectoryInfo("temp");
        public static DirectoryInfo currentProjectPath = tempDirectoryInfo;
        Novelka.TextBlock.TextBlock textBlock = new Novelka.TextBlock.TextBlock();
        public Action PreviewAreaChangedEvent;

        public MainViewModel model = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            PreviewAreaChangedEvent += PreviewAreaChangedHandler;
            DataContext = model;
            //MainPreviewCanvas.Children.Add(textBlock.GetBase());
        }

        void PreviewAreaChangedHandler()
        {
            selectedSlide.SetSlideImage(CreateBitmapFromVisual(MainPreviewArea));
        }
        async Task<BitmapImage> LoadImageAsync(string path)
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

        bool ShowOpenFileDialog(string filter, out string selectedFileName)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            if (ofd.ShowDialog() == true)
            {
                selectedFileName = ofd.FileName;
                return true;
            }
            else
                selectedFileName = null;
                return false;
        }
        void CopyFile(string oldFilePath, string newFilePath)
        {
            if (String.IsNullOrEmpty(oldFilePath) || String.IsNullOrEmpty(newFilePath))
                throw new Exception("Path is null or empty");
            File.Copy(oldFilePath, newFilePath, true);
        }

        private void SpeakerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selectedSlide == null) return;

            selectedSlide.frame.text = (sender as TextBox).Text;
            //MessageBox.Show(TextTextbox.MaxLength.ToString());
            PreviewAreaChangedEvent?.Invoke();
        }
    }
}
