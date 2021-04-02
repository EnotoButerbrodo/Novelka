using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Kursovaya.Stage
{
    //Отвечает за возможноть быть отображенным на сцене 
    interface IStage
    {
        void AddToStage(Canvas stage);
        void RemoveFromStage(Canvas stage);
    }
}
