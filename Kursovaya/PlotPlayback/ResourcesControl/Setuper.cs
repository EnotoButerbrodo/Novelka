using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Novelka.Stage;
using Novelka.Plot;
using Frame = Novelka.Plot.Frame;
using Novelka.Converters;
using System.Windows.Media.Imaging;
using Novelka.FileLoader;

namespace Novelka.Playback.ResourcesControl
{
    /*
     * Цели - подготовить загруженные ресурсы, правильно их сохранить, управление доступом к ним
     * */
    public class Setuper
    {
        protected IFileLoader loader;
        protected IFormatConverter converter;
        Canvas stage;
        public Setuper(IFileLoader loader, IFormatConverter converter, Canvas stage)
        {
            this.loader = loader;
            this.converter = converter;
            this.stage = stage;
        }

        const string ResourcesPath = "../../Resources.zip";

        public virtual void SetupSceneResources(Scene scene)
        {
            //Загрузить недостающие картинки
            //Добавить нужные обьекты на сцену и сделать их невидимыми
            

        }
        //На вход словарь - ключ - название обьекта, значение - массив с названиями картинок
        void LoadUsedResources(Dictionary<string, string[]> usedReources)
        {
            StageObject stageObject;
            
            //Проходим по каждому обьекту
            foreach (var stObj in usedReources)
            {
                //Добавляем обьект в ресурсы, если его еще не было
                if (!Resources.IsStageObjectInList(stObj.Key))
                {
                    var newStageObject = new StageObject();
                    newStageObject.AddToStage(stage);
                    Resources.AddStageObject(stObj.Key, newStageObject);
                }
                //Получаем нужный обьект из ресурсов
                stageObject = Resources.GetStageObject(stObj.Key);
                stageObject.Hide();
                //Проходимся по всем спрайтам
                foreach (var image in stObj.Value)
                {
                    //Если нужного спрайта нету добавляем его
                    if (!stageObject.IsSpriteInCollection(image))
                    {
                        var loadedData = loader.LoadFile(ResourcesPath,
                            $"{stObj.Key}\\{image}");
                        stageObject.AddSprite(image,
                            converter.ToBitmapImage(loadedData));
                    }
                }
            }
        }
        

        void SetupAppearance(ImageInfo[] imageInfos)
        {
            foreach(ImageInfo info in imageInfos)
            {
                var stObj = Resources.GetStageObject(info.objectName);
                stObj.SetAppearance(info.spriteName);
                stObj.Move(info.position);
            }
        }
        void SetupFrame(Frame frame)
        {
            //Расставить персонажей на нужные позиции с нужной анимацией.
            //Выставить нужный внешний вид
            //Выставить текст на текст бокс
            foreach(var imageInfo in frame.imagesInfo)
            {
                var stageObject = Resources.GetStageObject(imageInfo.objectName);
                stageObject.SetAppearance(imageInfo.spriteName);
                stageObject.Move(imageInfo.position);
            }
            
            
        }
        
    }
}
