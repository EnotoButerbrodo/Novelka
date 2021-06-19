using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Newtonsoft.Json;
using Novelka.Models;
using NovelkaLib.Models;
using NovelkaLib.ViewModels;

namespace Novelka.ViewModels
{
    class MainViewModel:ViewModelBase
    {
        /*Порядок запуска приложения
        Чтение файла StartupConfig
        Определение файла, который будет запускаться
        Загрузка этого файла
        Загрузка используемых ресурсов 
        Загрузка слайда
        */
        Project currentProject;

        public Project CurrentProject
        {
            get => currentProject;
            set => Set(ref currentProject, value);
        }

        StartupConfig config;
        string configPath = new Uri("startup.ini", UriKind.Relative).ToString();
        //При загрузке приложения
        void LoadStartupConfig()
        {
            using StreamReader file = File.OpenText(configPath);
            JsonSerializer serializer = new();
            config = (StartupConfig)serializer.Deserialize(file, typeof(StartupConfig));
        }
        void SaveStartupConfig()
        {
            string json = JsonConvert.SerializeObject(config);
            using StreamWriter file = new(configPath);
            file.Write(json);
        }
        void LoadProject()
        {
            try
            {
                using StreamReader fs = new(config.StartupProjectPath);
                string json = fs.ReadToEnd();
                CurrentProject = JsonConvert.DeserializeObject<Project>(json);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось загрузить проект");
            }

        }

        public MainViewModel()
        {
            //Чтобы не ругался xaml редактор
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            LoadStartupConfig();
            LoadProject();

        }

    }
}
