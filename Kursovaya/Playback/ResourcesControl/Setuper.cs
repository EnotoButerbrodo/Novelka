using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Kursovaya.Stage;
using Kursovaya.Plot;
using Frame = Kursovaya.Plot.Frame;
using Kursovaya.Converters;
using System.Windows.Media.Imaging;
using Kursovaya.FileLoader;

namespace Kursovaya.Playback.ResourcesControl
{
    /*
     * Цели - подготовить загруженные ресурсы, правильно их сохранить, управление доступом к ним
     * */
    public class Setuper
    {
        IFileLoader loader;
        IFormanConverter converter;
        public Setuper(IFileLoader loader, IFormanConverter converter)
        {
            this.loader = loader;
            this.converter = converter;
        }

        const string ResourcesPath = "../../Resources.zip";
        public void SetupUsedImages(Dictionary<string, string[]> usedImages, Canvas stage)
        {
            StageObject stageObject;
            foreach(var stObj in usedImages)
            {
                if (!Resources.IsStageObjectInList(stObj.Key))
                {
                    var newStageObject = new StageObject();
                    newStageObject.AddToStage(stage);
                    Resources.AddStageObject(stObj.Key, newStageObject);
                }

                stageObject = Resources.GetStageObject(stObj.Key);

                foreach(var image in stObj.Value)
                {
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

        public void SetupFrameImages(Frame frame)
        {
            SetupAppearance(frame.imagesInfo);
        }
        void LoadSceneResources(Dictionary<string, string[]> usedImages)
        {
            foreach(var stageObject in usedImages)
            {
                if (!Resources.IsStageObjectInList(stageObject.Key))
                {

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
        
    }
}
