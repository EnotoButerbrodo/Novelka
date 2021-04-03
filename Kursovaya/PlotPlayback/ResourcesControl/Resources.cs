using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Kursovaya.Stage;

namespace Kursovaya.Playback.ResourcesControl
{
    public static class Resources
    {

        static Dictionary<string, StageObject> stageObjects = new Dictionary<string, StageObject>();
        static public void AddStageObject(string name, StageObject stageObject)
        {
             stageObjects.Add(name, stageObject);
        }

        static public bool IsStageObjectInList(string name)
        {
            return stageObjects.ContainsKey(name);
        }

        static public StageObject GetStageObject(string name)
        {
            return stageObjects[name];
        }

        static public void RemoveStageObject(string name)
        {
            stageObjects.Remove(name);
        }




    }
}
