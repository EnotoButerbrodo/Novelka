using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace NovelkaCreationTool.Infrastructure
{
    public class SetterAction : TargetedTriggerAction<FrameworkElement>
    {
        public DependencyProperty Property { get; set; }
        public Object Value { get; set; }


        protected override void Invoke(object parameter)
        {
            AssociatedObject.SetValue(Property, Value);
        }
    }
}
