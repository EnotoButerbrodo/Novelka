using System.Windows;
using System.Windows.Input;
using NovelkaCreationTool.Commands;
using NovelkaLib.ViewModels;

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
            AddButtonCommand = new RelayCommand(AddButtonEx, (obj) => { return true; });
        }
    }
}
