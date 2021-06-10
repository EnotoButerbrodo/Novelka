using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NovelkaLib.ViewModels;

namespace NovelkaLib.Models
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

        public HashSet<string> UsingImages = new();
        public ObservableCollection<Slide> Slides { get; set; }
        public ObservableCollection<string> Images = new();
        public ProjectSettings Settings { get; set; }
        public Project()
        {
            Slides = new();
        }
    }
    [Serializable]
    public class ProjectSettings:ViewModelBase
    {
        public int Version;
        int width, height;
        public int Width
        {
            get => width;
            set => Set(ref width, value);
        }
        public int Height
        {
            get => height;
            set => Set(ref height, value);
        }
    }
}
