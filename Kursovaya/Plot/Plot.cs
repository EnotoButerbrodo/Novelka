using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaya.Plot
{
    //Класс Plot содержит информацию о иерархии всех сцен, условия переходов между сценами.
    public class Plot
    {
        SceneInfo[] scenesInfo;
    }

    class JumpTransition
    {
        string varName;
        string value;
        string transitionsSceneName;
        public JumpTransition(string varName, string value, string transitionsSceneName)
        {
            this.varName = varName;
            this.value = value;
            this.transitionsSceneName = transitionsSceneName;
        }
    }

    class SceneInfo
    {
        string name;
        JumpTransition[] jumpTransitions;
        public SceneInfo(string sceneName, JumpTransition[] jumpTransitions)
        {
            this.name = sceneName;
            this.jumpTransitions = jumpTransitions;
        }
    }
}
