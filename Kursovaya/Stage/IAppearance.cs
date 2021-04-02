using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace Kursovaya.Stage
{
    //Отвечает за возможность добавлять, удалять и устанавливать изображение как свой внешний вид
    interface IAppearance
    {
        void AddSprite(string spriteName, BitmapImage image);
        void RemoveSprite(string spriteName);
        bool IsSpriteInCollection(string spriteName);
        void SetAppearance(string spriteName);
    }
}
