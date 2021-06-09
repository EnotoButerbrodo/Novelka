using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novelka.Models;
using NovelkaCreationTool.Models;
using NovelkaCreationTool.ViewModels.Base;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
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
            BinaryFormatter formatter = new();
            using FileStream fs = File.OpenRead(config.StartupProjectPath);
            CurrentProject = (Project)formatter.Deserialize(fs);

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
