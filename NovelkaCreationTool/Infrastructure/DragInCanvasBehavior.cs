using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace NovelkaCreationTool.Infrastructure
{
    public class DragInCanvasBehavior : Behavior<UIElement>
    {
        private Canvas canvas;
        ListBoxItem FixedAssociatedObject;

        protected override void OnAttached()
        {
            base.OnAttached();
            FixedAssociatedObject = FindParent<ListBoxItem>(AssociatedObject);
            canvas = FindParent<Canvas>(this.AssociatedObject);
            // Присоединение обработчиков событий            
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            
            // Удаление обработчиков событий
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }

        // Отслеживание перетаскивания элемента
        private bool isDragging = false;

        // Запись точной позиции, в которой нажата кнопка
        private Point mouseOffset;

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Поиск canvas
           // if (canvas == null) canvas = LogicalTreeHelper.GetParent(this.AssociatedObject) as Canvas;
            // Режим перетаскивания
            isDragging = true;

            // Получение позиции нажатия относительно элемента
            mouseOffset = e.GetPosition(AssociatedObject);

            // Захват мыши
            AssociatedObject.CaptureMouse();
        }

        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null) return null;
            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Получение позиции элемента относительно Canvas
                Point point = e.GetPosition(canvas);

                // Move the element.

                FixedAssociatedObject.SetValue(Canvas.TopProperty, point.Y - mouseOffset.Y);
                FixedAssociatedObject.SetValue(Canvas.LeftProperty, point.X - mouseOffset.X);
            }
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                FixedAssociatedObject.ReleaseMouseCapture();
                isDragging = false;
            }
        }
    }
}
