﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GeoGeometry.Model.Box
{
    public class BoxResponse
    {
        public string id { get; set; }
        public Dictionary<string, string> Sensors { get; set; }
        public BoxResponse()
        {
            this.Sensors = new Dictionary<string, string>()
            {

                {"Температура","" },
                {"Влажность","" },
                {"Освещенность","" },
                {"Вес груза","" },
                {"Уровень заряда аккумулятора","" },
                {"Уровень сигнала","" },
                {"Состояние дверей","" },
                {"Состояние контейнера","" },
                {"Местоположение контейнера","" }
            };
        }
    }
}