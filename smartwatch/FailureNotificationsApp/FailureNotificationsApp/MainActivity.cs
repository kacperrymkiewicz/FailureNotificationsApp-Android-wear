using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Content;
using SocketIO.Client;
using Android.Nfc;
using Android.Util;
using Newtonsoft.Json;
using Org.Json;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        public static bool isLoggedIn;
        public static string authToken;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Intent backgroundService = new Intent(base.ApplicationContext, typeof(BackgroundTasks));
            StartService(backgroundService);

            isLoggedIn = checkAuthentication();

            if(!isLoggedIn)
            {
                Intent loginService = new Intent(this, typeof(LoginController));
                StartActivity(loginService);
            }

            SetContentView(Resource.Layout.activity_main);
            SetAmbientEnabled();

        }

        private bool checkAuthentication()
        {
            return false;
        }
    }
}


