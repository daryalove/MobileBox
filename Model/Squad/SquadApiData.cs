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

namespace GeoGeometry.Model.Squad
{
    class SquadApiData
    {
        /// <summary>
        /// Статус ответа.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Сообщение ответа.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Информация о клиенте.
        /// </summary>
        public SquadResponseData ResponseData { get; set; }
    }
}