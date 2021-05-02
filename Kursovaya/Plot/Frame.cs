using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;




namespace Novelka.Plot
{
    [Serializable]
    public class Frame
    {
        public ImageInfo[] imagesInfo;
        public string backgroundImage;
        public string text;
        public string speaker;
        public Frame(ImageInfo[] imagesInfo, string backgroundImage, string speaker, string text)
        {
            this.imagesInfo = imagesInfo;
            this.backgroundImage = backgroundImage;
            this.speaker = speaker;
            this.text = text;
        }
        public Frame() :this(null, null, null, null)
        {
        }
        public Frame DeepClone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (Frame)formatter.Deserialize(ms);
            }
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
