using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NovelkaCreator.Slide;

namespace NovelkaCreator
{
    public partial class MainWindow
    {
        public LinkedList<BasicSlide> Slides = new LinkedList<BasicSlide>();
        public static BasicSlide selectedSlide;
        public static BasicSlide prevSelectedSlide;

        void AddSlide(BasicSlide slide)
        {
            Slides.AddLast(slide);
          
        }
        void DeleteSlide()
        {
            Slides.Remove(selectedSlide);
            SlidesScrollBar.Maximum = Slides.Count - 1;
        }
        void CreateTempDirectory()
        {
            if (!tempDirectoryInfo.Exists)
                tempDirectoryInfo.Create();
        }

        private void AddTestSlide_Click(object sender, RoutedEventArgs e)
        {
            var newSlide = Slides.Last?.Value.DeepClone() ?? new BasicSlide();
            newSlide.SetId(Slides.Count + 1);
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
            int id = Slides.Count;
            foreach (var slide in Slides)
            {
                slide.SetId(id--);
            }
        }

    }
}
