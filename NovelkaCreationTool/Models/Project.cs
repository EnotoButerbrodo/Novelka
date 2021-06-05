using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NovelkaCreationTool.ViewModels.Base;

namespace NovelkaCreationTool.Models
{
    [Serializable]
    public class Project:ViewModelBase
    {
        string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public ObservableCollection<Slide> Slides = new();
        public ObservableCollection<string> Images = new();
    }
}
