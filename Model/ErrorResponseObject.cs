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
using GeoGeometry.Model.Auth;

namespace GeoGeometry.Model
{
    public class ErrorResponseObject: BaseResponseObject
    {
        public List<string> Errors { get; set; }
    }
}