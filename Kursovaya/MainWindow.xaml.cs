using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Novelka.Plot;
using Frame = Novelka.Plot.Frame;
using Novelka.Converters;
using Novelka.FileLoader;
using Novelka.Playback;
using Novelka.Playback.ResourcesControl;

namespace Novelka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Canvas mainStage;
        Setuper setuper = new Setuper(new FileLoader.FileLoader(), new FormatConverter(), mainStage);
        public MainWindow()
        {
            InitializeComponent();
            //StageConfig();
        }
        //Игра загружается 
        //Создаются необходимые для работы классы
        //После выбора пунка начать игру
        //Определяется, какая сцена должна загрузиться
        //Выбранная сцена загружается
        //--Загрузка используемых ресурсов
        //--Расстановка используемых ресурсов на сцену и скрытие их до необходимости
        //Выбирается какой фрейм поставить
        //После выбора фрейма, он загружается
        //--Устанавливаем спрайты, используемые в кадре
        //--Распологаем в нужной позиции
        //--Делаем видимыми
        //--Выводим текст
        //Ждем окончания всех анимаций
        //Готовы по команде вычислить следующий фрейм

        //Нужно придумать главный файл, который бы указывал, какую
        //нужно загрузить сцену
        /*
        Frame[] CreateTestFrames()
        {
            Frame[] frames;
            frames = new Frame[3];
            ImageInfo[] imagesInfo = new ImageInfo[3];
            
            imagesInfo[0] = new ImageInfo("Monika", "Default.png", new Position(0, 0, 0));
            imagesInfo[1] = new ImageInfo("lilly", "lilly_back_devious.png", new Position(100, 0, 0));
            imagesInfo[2] = new ImageInfo("Background", "Class1.png", new Position(0, 0, -1));
            frames[0] = new Frame(imagesInfo, "", "This is the first");

            imagesInfo[0] = new ImageInfo("Monika", "Default.png", new Position(0, 0, 0));
            imagesInfo[1] = new ImageInfo("lilly", "lilly_back_devious.png", new Position(300, 0, 0));
            imagesInfo[2] = new ImageInfo("Background", "Class1.png", new Position(0, 0, -1));
            frames[1] = new Frame(imagesInfo, "", "This is the second");

            imagesInfo[0] = new ImageInfo("Monika", "Flirty.png", new Position(-300, 0, 0));
            imagesInfo[1] = new ImageInfo("lilly", "lilly_back_devious.png", new Position(0, 0, 0));
            imagesInfo[2] = new ImageInfo("Background", "Class1.png", new Position(0, 0, -1));
            frames[2] = new Frame(imagesInfo, "", "This is the third");

            return frames;

        }

        Scene CreateTestScene()
        {
            Dictionary<string, string[]> usedSprites = new Dictionary<string, string[]>();
            usedSprites.Add("Monika", new string[] { "Default.png", "Flirty.png" });
            usedSprites.Add("lilly", new string[] { "lilly_back_devious.png" });
            usedSprites.Add("Background", new string[] { "Class1.png", "Class2.png"});

            Scene scene = new Scene("First Scene",
                CreateTestFrames(),
                usedSprites
                );
            return scene;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Scene scene = CreateTestScene();
            setuper.SetupSceneResources(scene);

            //Я хочу загрузить сцену
            //Загрузить юзаные ресурсы


        }
        private void StageConfig()
        {
            mainStage = new Canvas();
            MainGrid.Children.Add(mainStage);
            mainStage.Width = MainGrid.Width;
            mainStage.Height = MainGrid.Height;
        }
        */
    }
}
