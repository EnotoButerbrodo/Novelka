using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaya.Plot
{
    public class Frame
    {
        public ImageInfo[] imagesInfo;
        public string text;
        public string speaker;
        public Frame(ImageInfo[] imagesInfo, string speaker, string text)
        {
            this.imagesInfo = imagesInfo;
            this.speaker = speaker;
            this.text = text;
        }
    }

    public class ImageInfo
    {
        public string objectName;
        public string spriteName;
        public Position position;
        public ImageInfo(string objectName, string spriteName, Position position)
        {
            this.objectName = objectName;
            this.spriteName = spriteName;
            this.position = position;
        }
    }

    public class Position
    {
        public int x;
        public int y;
        public int z;
        public Position(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        } 
    }

}
