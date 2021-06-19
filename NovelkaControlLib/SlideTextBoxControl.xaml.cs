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

namespace NovelkaControlLib
{
    /// <summary>
    /// Логика взаимодействия для SlideTextBoxControl.xaml
    /// </summary>
    public partial class SlideTextBoxControl : UserControl
    {
        public SlideTextBoxControl()
        {
            InitializeComponent();
            DataContext = this;
        }


        public double SpeakerWidth
        {
            get { return (double)GetValue(SpeakerWidthProperty); }
            set { SetValue(SpeakerWidthProperty, value); }
        }
        // Using a DependencyProperty as the backing store for SpeakerWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeakerWidthProperty =
            DependencyProperty.Register("SpeakerWidth", typeof(double), typeof(SlideTextBoxControl), new FrameworkPropertyMetadata(0.0));



        public double SpeakerHeight
        {
            get { return (double)GetValue(SpeakerHeightProperty); }
            set { SetValue(SpeakerHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpeakerHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeakerHeightProperty =
            DependencyProperty.Register("SpeakerHeight", typeof(double), typeof(SlideTextBoxControl), new PropertyMetadata(Convert.ToDouble(0)));





    }
}
