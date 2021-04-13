using System;
using System.Collections.Generic;
using System.Text;

namespace Novelka.Plot
{
    public class Scene
    {
        public string name;
        public Frame[] frames;
        public Dictionary<string, string[]> usedImages;

        public Scene(string sceneName, Frame[] frames, Dictionary<string, string[]> usedImages)
        {
            this.name = sceneName;
            this.frames = frames;
            this.usedImages = usedImages;
        }

    }

}
