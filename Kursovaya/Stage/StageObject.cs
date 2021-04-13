using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Novelka.Plot;

namespace Novelka.Stage
{
    public class StageObject
    {
        protected Dictionary<string, BitmapImage> sprites;
        protected Image appearance;

        public StageObject()
        {
            sprites = new Dictionary<string, BitmapImage>();
            appearance = new Image();
            appearance.RenderTransformOrigin = new System.Windows.Point(0.5, 1);
            appearance.Stretch = Stretch.Uniform;
            Canvas.SetLeft(appearance, 0);
            Canvas.SetBottom(appearance, 0);
        }
        public void AddSprite(string spriteName, BitmapImage image)
        {
            sprites.Add(spriteName, image);
        }
        public void RemoveSprite(string spriteName)
        {
            if(IsSpriteInCollection(spriteName))
                sprites.Remove(spriteName);
        }
        public virtual void AddToStage(Canvas stage)
        {
            stage.Children.Add(appearance);
        }
        public void RemoveFromStage(Canvas stage)
        {
            stage.Children.Remove(appearance);
        }
        public void SetAppearance(string spriteName)
        {
            appearance.Source = sprites[spriteName];
        }
        public void Move(Position position)
        {
            Canvas.SetLeft(appearance, position.x);
            Canvas.SetBottom(appearance, position.y);
            Canvas.SetZIndex(appearance, position.z);
        }
        public bool IsSpriteInCollection(string spriteName)
        {
            return sprites.ContainsKey(spriteName);
        }
        public void Show()
        {
            appearance.Opacity = 1;
        }
        public void Hide()
        {
            appearance.Opacity = 0;
        }
    }
}
