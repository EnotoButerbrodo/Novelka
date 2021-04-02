using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Kursovaya.Plot;

namespace Kursovaya.Stage
{
    public class StageObject : IStage, IAppearance
    {
        Dictionary<string, BitmapImage> sprites = new Dictionary<string, BitmapImage>();
        Image appearance = new Image();

        public StageObject()
        {
            appearance.RenderTransformOrigin = new System.Windows.Point(0.5, 1);
            appearance.Stretch = Stretch.Uniform;
            Canvas.SetLeft(appearance, 0);
            Canvas.SetBottom(appearance, 0);
        }
        public void AddSprite(string spriteName, BitmapImage image)
        {
            sprites.Add(spriteName, image);
        }

        public void AddToStage(Canvas stage)
        {
            stage.Children.Add(appearance);
        }

        public void RemoveFromStage(Canvas stage)
        {
            stage.Children.Remove(appearance);
        }

        public void RemoveSprite(string spriteName)
        {
            if(IsSpriteInCollection(spriteName))
                sprites.Remove(spriteName);
        }

        public void SetAppearance(string spriteName)
        {
            appearance.Source = sprites[spriteName];
        }
        public bool IsSpriteInCollection(string spriteName)
        {
            return sprites.ContainsKey(spriteName);
        }

        public void Move(Position position)
        {
            //Canvas.SetLeft(Appearance, position.x);
            //Canvas.SetBottom(Appearance, position.y);
            //Canvas.SetZIndex(Appearance, position.z);
            Canvas.SetLeft(appearance, position.x);
            Canvas.SetBottom(appearance, position.y);
            Canvas.SetZIndex(appearance, position.z);
            //Panel.SetZIndex(Spot, position.z);
        }
    }
}
