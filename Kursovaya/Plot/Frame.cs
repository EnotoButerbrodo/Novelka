using System;
using System.Collections.Generic;
using System.Text;



namespace Novelka.Plot
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
        public Frame() :this(new ImageInfo[3], "", "")
        {

        }
    }

    public class ImageInfo
    {
        public string objectName;
        public string spriteName;
        public Position position;
        public AnimationInfo animationInfo;
        public ImageInfo(string objectName, string spriteName, Position position, AnimationInfo animationInfo = null)
        {
            this.objectName = objectName;
            this.spriteName = spriteName;
            this.position = position;
            this.animationInfo = animationInfo;
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

    public class AnimationInfo
    {
        public string animationScript;
        
    }

}
