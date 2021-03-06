﻿using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Java.IO;
using System;

namespace GeoGeometry.Activity.Cameraa
{
    public class CameraPreview : SurfaceView, ISurfaceHolderCallback
    {
        Android.Hardware.Camera camera;
        static bool _stopped;

        public CameraPreview(Context context, Android.Hardware.Camera cameraa) : base(context)
        {
            camera = cameraa;
            camera.SetDisplayOrientation(90);// Вылетает на этой строчке !!! Если закоментировать - переходит к строке номер 68 и выдаёт такую же ошибку !!!

            //Surface holder callback is set so theat SurfaceChanged, Created, destroy... 
            //Could be called from here.
            Holder.AddCallback(this);
            // deprecated but required on Android versions less than 3.0
            Holder.SetType(SurfaceType.PushBuffers);
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            if (Holder.Surface == null)
            {
                return;
            }

            try
            {
                camera.StopPreview();
            }
            catch (Exception)
            {
                // ignore: tried to stop a non-existent preview
            }

            try
            {
                // start preview with new settings
                camera.SetPreviewDisplay(Holder);
                camera.StartPreview();

            }
            catch (Exception e)
            {
                Log.Debug("", "Error starting camera preview: " + e.Message);
            }
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            try
            {
                camera.SetPreviewDisplay(holder);
                camera.StartPreview();
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            //You could handle release of camera and holder here, but I did it already in the CameraFragment.
        }
    }
}