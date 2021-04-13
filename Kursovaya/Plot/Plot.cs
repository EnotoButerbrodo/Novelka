using System;
using System.Collections.Generic;
using System.Text;

namespace Novelka.Plot
{
    //Класс Plot содержит информацию о иерархии всех сцен, условия переходов между сценами.
    public class Plot
    {
        public SceneInfo[] scenesInfo;
    }

    public class JumpTransition
    {
        public string varName;
        public string value;
        public string transitionsSceneName;
        public JumpTransition(string varName, string value, string transitionsSceneName)
        {
            this.varName = varName;
            this.value = value;
            this.transitionsSceneName = transitionsSceneName;
        }
    }

    public class SceneInfo
    {
        public string name;
        public JumpTransition[] jumpTransitions;
        public SceneInfo(string sceneName, JumpTransition[] jumpTransitions)
        {
            this.name = sceneName;
            this.jumpTransitions = jumpTransitions;
        }
    }
}
