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
using Android.Support.V4.App;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true, NoHistory = true)]
    public class MainActivity : WearableActivity
    {
        public static bool isLoggedIn;
        public static string authToken;

        public static readonly int NOTIFICATION_ID = 1000;
        public static readonly string CHANNEL_ID = "location_notification";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Intent backgroundService = new Intent(base.ApplicationContext, typeof(BackgroundTasks));
            StartService(backgroundService);

            CreateNotificationChannel();

            isLoggedIn = checkAuthentication();

            if(!isLoggedIn)
            {
                Intent loginService = new Intent(this, typeof(LoginController));
                StartActivity(loginService);
            }
            else
            {
                Intent statusService = new Intent(this, typeof(StatusService));
                StartActivity(statusService);
            }

            //SetContentView(Resource.Layout.activity_main);
            SetAmbientEnabled();

        }

        private bool checkAuthentication()
        {
            if(isLoggedIn)
            {
                return true;
            }
            return false;
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var name = Resources.GetString(Resource.String.channel_name);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

    }
}


