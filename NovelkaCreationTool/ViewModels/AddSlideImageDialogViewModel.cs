using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovelkaCreationTool.ViewModels.Base;
using NovelkaCreationTool.Commands.Base;
using System.Windows.Input;
using System.Windows;
using NovelkaCreationTool.Views;
using NovelkaCreationTool.Commands;

namespace NovelkaCreationTool.ViewModels
{
    public class AddSlideImageDialogViewModel : ViewModelBase
    {
        public ICommand AddButtonCommand { get; }
        void AddButtonEx(object p)
        {
            Application.Current.Shutdown();
        }

        public AddSlideImageDialogViewModel()
        {
            AddButtonCommand = new LambdaCommand(AddButtonEx, (obj) => { return true; });
        }
    }
}
