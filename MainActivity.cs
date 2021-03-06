﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Content;
using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Single;
using Com.Karumi.Dexter.Listener;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using GeoGeometry.Model.Auth;
using Com.Karumi.Dexter.Listener.Multi;
using System.Collections.Generic;
using System;
using Android.Support.V4.App;
using Plugin.Settings;
using System.Net;
using System.Net.Http;
using GeoGeometry.Model.User;
using GeoGeometry.Model.Box;
using GeoGeometry.Container;
using System.Threading.Tasks;
using GeoGeometry.Service;
using Java.Util;

namespace GeoGeometry.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        /// <summary>
        /// Конпка прехода на форму авторизации.
        /// </summary>
        private Button btn_auth_form;


        private int MY_PERMISSIONS_REQUEST_CAMERA = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.activity_main);
               
                string file_data_remember;

                btn_auth_form = FindViewById<Button>(Resource.Id.btn_auth_form);

                string[] permissions = { Manifest.Permission.AccessFineLocation, Manifest.Permission.WriteExternalStorage, Manifest.Permission.Camera, Manifest.Permission.ReadPhoneState, Manifest.Permission.Vibrate, Manifest.Permission.AccessNetworkState,
                Manifest.Permission.Bluetooth, Manifest.Permission.BluetoothAdmin, Manifest.Permission.Internet};
                
                Dexter.WithActivity(this).WithPermissions(permissions).WithListener(new CompositeMultiplePermissionsListener(new SamplePermissionListener(this))).Check();
                CrossSettings.Current.AddOrUpdateValue("id", "E353DA5A-07C9-4939-97ED-0CD7CF7B2A7A");


                //Android ID  
                //String m_androidId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

                //WLAN MAC Address              
                //Android.Net.Wifi.WifiManager m_wm = (Android.Net.Wifi.WifiManager)GetSystemService(Android.Content.Context.WifiService);
                //String m_wlanMacAdd = m_wm.ConnectionInfo.MacAddress;

                //Blue-tooth Address  
                //Android.Bluetooth.BluetoothAdapter m_BluetoothAdapter = Android.Bluetooth.BluetoothAdapter.DefaultAdapter;
                //String m_bluetoothAdd = m_BluetoothAdapter.Address;

                //if (CrossSettings.Current.GetValueOrDefault("id", "") == "")
                //{
                //    try
                //    {
                //        var box = GetRandomBox();
                //        if (box.Result.Status == "0")
                //        {
                //            CrossSettings.Current.AddOrUpdateValue("id", box.Result.ResponseData.BoxId);
                //            CrossSettings.Current.AddOrUpdateValue("namebox", box.Result.ResponseData.Name);
                //        }
                //        else
                //            Toast.MakeText(this, "" + box.Result.Message, ToastLength.Long).Show();
                //    }
                //    catch(Exception ex)
                //    {
                //        Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                //    }
                //}

                // Переход к форме авторизация
                btn_auth_form.Click += async (s, e) =>
                {
                    Android.Telephony.TelephonyManager mTelephonyMgr;
                    // Android.Telephony.TelephonyManager mTelephonyMgr = (Android.Telephony.TelephonyManager)GetSystemService(Android.Content.Context.TelephonyService);
                    //Telephone Number  
                    mTelephonyMgr = (Android.Telephony.TelephonyManager)GetSystemService(TelephonyService);                 
                    /*var PhoneNumber = mTelephonyMgr.Line1Number*/;

                    if (mTelephonyMgr.DeviceId != null)
                    {
                        //IMEI number 
                        StaticBox.DeviceId = mTelephonyMgr.DeviceId;
                    }
                    else if (Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId) != null)
                    {
                        //Android ID 
                        StaticBox.DeviceId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                    }
                    else
                    {
                        //Hash code
                        //StaticBox.DeviceId = "35" + (Build.Board.Length % 10) + (Build.Brand.Length % 10) + (Build.CpuAbi.Length % 10) + (Build.Device.Length % 10) + (Build.Manufacturer.Length % 10) + (Build.Model.Length % 10) + (Build.Product.Length % 10);

                        //Blue-tooth Address  
                        Android.Bluetooth.BluetoothAdapter m_BluetoothAdapter = Android.Bluetooth.BluetoothAdapter.DefaultAdapter;
                        string m_bluetoothAdd = m_BluetoothAdapter.Address;
                        StaticBox.DeviceId = m_bluetoothAdd;
                    }

                    await RegisterBox();
                    
                };

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
            }
        }

        private async Task RegisterBox()
        {
            /*http://smartboxcity.ru:8003/imitator/create POST создает контейнер
             * http://iot.tmc-centert.ru/api/container/SearchCommandPhoto?name=123
http://smartboxcity.ru:8003/imitator/delete GET удаляет контейнер*/
           

            try
            {
                #region WebRequest Example
                //var formContent = new Dictionary<string, string>
                //    {
                //        { "Id", StaticBox.DeviceId }
                //    };

                //string newData = "";

                //foreach (string key in formContent.Keys)
                //{
                //    newData += key + "="
                //          + formContent[key] + "&";
                //}

                //var postData = newData.Remove(newData.Length - 1, 1);

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://smartboxcity.ru:8003/imitator/create");
                //request.Method = "POST";


                //byte[] data = Encoding.ASCII.GetBytes(postData);

                //request.ContentType = "multipart/form-data";
                //request.ContentLength = data.Length;

                //Stream requestStream = request.GetRequestStream();
                //requestStream.Write(data, 0, data.Length);
                //requestStream.Close();
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //Stream responseStream = response.GetResponseStream();

                //StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                //string s_result = myStreamReader.ReadToEnd();

                //myStreamReader.Close();
                //responseStream.Close();

                //response.Close();
                #endregion

                CreateBoxModel model = new CreateBoxModel
                {
                    id = StaticBox.DeviceId
                };

                var myHttpClient = new HttpClient();
                var uri = new Uri("http://smartboxcity.ru:8003/imitator/create?id=" + model.id);

                // Поучаю ответ об авторизации [успех или нет]
                HttpResponseMessage response = await myHttpClient.GetAsync(uri.ToString() /*new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")*/);

                string s_result;
                using (HttpContent responseContent = response.Content)
                {
                    s_result = await responseContent.ReadAsStringAsync();
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //запуск задания
                    StartUp.StartTracking();

                    Intent Driver = new Intent(this, typeof(Auth.SensorParameters));
                    StartActivity(Driver);
                    this.Finish();
                }
                else
                {
                    Toast.MakeText(this, "" + "Ошибка входа", ToastLength.Long).Show();
                }
                // AuthApiData<AuthResponseData> o_data = JsonConvert.DeserializeObject<AuthApiData<AuthResponseData>>(s_result);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
            }
            

        }

        private async Task<AuthApiData<GetBoxIdResponse>> GetRandomBox()
        {
           
                string dir_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                var myHttpClient = new HttpClient();
            WebRequest request = WebRequest.Create("http://iot-tmc-cen.1gb.ru/api/container/getrandombox");
            WebResponse response = request.GetResponse();
            // HttpWebRequest
            var uri = new Uri("http://iot-tmc-cen.1gb.ru/api/container/getrandombox");
            string s_result = "";
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        s_result += line;
                    }
                }
            }
            response.Close();

            AuthApiData<GetBoxIdResponse> o_data = JsonConvert.DeserializeObject<AuthApiData<GetBoxIdResponse>>(s_result);
            //ClearField();

            return o_data;
        }

        private class SamplePermissionListener : Java.Lang.Object, IMultiplePermissionsListener
        {
            MainActivity activity;
            public SamplePermissionListener(MainActivity activity)
            {
                this.activity = activity;
            }

            public void OnPermissionDenied(PermissionDeniedResponse p0)
            {
                //Snackbar.Make(activity.main_form, "Permission Denied", Snackbar.LengthShort).Show();
            }

            public void OnPermissionGranted(PermissionGrantedResponse p0)
            {
                //Snackbar.Make(activity.main_form, "Permission Granted", Snackbar.LengthShort).Show();
            }

            public void OnPermissionRationaleShouldBeShown(IList<PermissionRequest> p0, IPermissionToken p1)
            {
                p1.ContinuePermissionRequest();
                throw new System.NotImplementedException();
            }

            public void OnPermissionsChecked(MultiplePermissionsReport p0)
            {
                if (p0.AreAllPermissionsGranted())
                {
                    
                }

                if (p0.IsAnyPermissionPermanentlyDenied)
                {
                    // show alert dialog navigating to Settings
                    
                }
            }
        }
    }

    internal class MyDismissListener : Java.Lang.Object, IDialogInterfaceOnDismissListener
    {
        private IPermissionToken token;

        public MyDismissListener(IPermissionToken token)
        {
            this.token = token;
        }

        public void OnDismiss(IDialogInterface dialog)
        {
            token.CancelPermissionRequest();
        }
    }
}