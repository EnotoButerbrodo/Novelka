using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace Novelka.Stage.TextBox
{
    public class TextBox
    {
        protected TextBlock mainTextBlock;
        protected TextBlock speakerTextBlock;
        protected Image mainBackgroundImage;
        protected Image speakerBackgroundImage;
        protected Canvas Base;

        public TextBox(BitmapImage mainImage, BitmapImage speakerImage)
        {
            Base = new Canvas();
            Base.Children.Add(mainBackgroundImage);
            Base.Children.Add(speakerBackgroundImage);
            mainBackgroundImage.Source = mainImage;
            speakerBackgroundImage.Source = speakerImage;
        }

        protected virtual void Setup()
        {
            //Код инициализации внешнего вида текстбокса
        }

        public virtual void SetText(string text) =>
            mainTextBlock.Text = text;
        public virtual void SetSpeaker(string speaker) => 
            speakerTextBlock.Text = speaker;

        public virtual void AddText(string text) =>
            mainTextBlock.Text += text;

        public virtual void Clear()
        {
            speakerTextBlock.Text = "";
            mainTextBlock.Text = "";
        }
    }
}
