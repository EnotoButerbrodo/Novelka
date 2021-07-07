using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace NovelkaCreationTool.Infrastructure
{
    class UIElementPicker :Behavior<UIElement>
    {
        public static readonly DependencyProperty PicketUIElementProperty = DependencyProperty.Register(
          "PicketUIElement", typeof(UIElement), typeof(UIElementPicker), new PropertyMetadata(default(UIElement)));

        public UIElement PicketUIElement
        {
            get { return (UIElement)GetValue(PicketUIElementProperty); }
            set { SetValue(PicketUIElementProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            PicketUIElement = AssociatedObject;
        }
    }
}
