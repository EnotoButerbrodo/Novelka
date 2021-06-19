using NovelkaLib.ViewModels;

namespace NovelkaLib.Models
{
    public class SlideTextBox : ViewModelBase
    {
        double speakerWidth, speakerHeight,
               textWidth, textHeight;
        string imageName;
        int fontSize;

        public double SpeakerWidth
        {
            get => speakerWidth;
            set => Set(ref speakerWidth, value);
        }
        public double SpeakerHeight
        {
            get => speakerHeight;
            set => Set(ref speakerHeight, value);
        }
        public double TextWidth
        {
            get => textWidth;
            set => Set(ref textWidth, value);
        }

        public double TextHeight
        {
            get => textHeight;
            set => Set(ref textHeight, value);
        }
        public string ImageName
        {
            get => imageName;
            set => Set(ref imageName, value);
        }

        public int FontSize
        {
            get => fontSize;
            set => Set(ref fontSize, value);
        }

    }
}
