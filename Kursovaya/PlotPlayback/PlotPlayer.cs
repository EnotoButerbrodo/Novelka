using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Kursovaya.Playback
{
    /*
     * Цели - управление этапом воспроизведения сюжета, полностью.
     * Управление процессом загрузки всех ресурсов, подготовки их к хранению,
     * Подготовка нудного фрейма и его отображение
      */

    public class PlotPlayer
    {
        Canvas stage;
        public PlotPlayer(Canvas stage)
        {
            this.stage = stage;
        }

    }
}
