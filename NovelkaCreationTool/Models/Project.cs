using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovelkaCreationTool.ViewModels.Base;

namespace NovelkaCreationTool.Models
{
    public class Project:ViewModelBase
    {
        string path, name;
        public string Path
        {
            get => path;
            set => Set(ref path, value);
        }
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        public ObservableCollection<Slide> Slides = new();
        public ObservableCollection<string> Images = new();
    }
}
