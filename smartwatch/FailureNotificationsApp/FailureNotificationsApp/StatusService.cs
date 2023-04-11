using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Content;
using Android.Media;
using SocketIOClient;
using System.Security.AccessControl;
using static Android.Icu.Text.CaseMap;
using SocketIO.Client;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.Json;
using FailureNotificationsApp.helpers;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name")]
    public class StatusService : WearableActivity
    {
        public static bool unseenNotification;
        public static string notificationDescription;
        public static string notificationWorkstation;
        public static int notificationPriority;
        Button logout_button;
        TextView username;
        SocketIO.Client.Socket socket;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.StatusView);
            SetAmbientEnabled();

            username = FindViewById<TextView>(Resource.Id.pracownik_username);
            username.Text = "Pracownik: " + MainActivity.authUsername;

            if (unseenNotification)
            {
                OpenRaportView();
                Finish();
            }

            socket = IO.Socket("https://projektzespolowyitm-production.up.railway.app/");
            socket.On("newAwaria", (data) => {
                Console.WriteLine("poszlo");
                if(MainActivity.isLoggedIn)
                {
                    var json = data[0] as JSONObject;
                    var obj = JsonConvert.DeserializeObject(json.ToString());
                    dynamic jsonobj = JObject.Parse(obj.ToString());

                    unseenNotification = true;
                    notificationDescription = jsonobj.newAwaria.opis_awarii;
                    notificationWorkstation = jsonobj.newAwaria.stanowisko.nazwa;
                    notificationPriority = jsonobj.newAwaria.priorytet;
                    OpenRaportView();
                }
            });

            socket.Connect();

            logout_button = FindViewById<Button>(Resource.Id.logout_button);
            logout_button.Click += LogoutSubmit;
        }

        private void LogoutSubmit(object sender, EventArgs e)
        {
            MainActivity.isLoggedIn = false;
            MainActivity.authToken = null;

            Toast.MakeText(Application.Context, "Pomyślnie wylogowano", ToastLength.Short).Show();
            Intent logoutIntent = new Intent(this, typeof(MainActivity));
            StartActivity(logoutIntent);
            Finish();
        }

        private void OpenRaportView()
        {
            var intent = new Intent(this, typeof(RaportActivity));

            intent.PutExtra("notificationDescription", notificationDescription);
            intent.PutExtra("notificationWorkstation", notificationWorkstation);
            intent.PutExtra("notificationPriority", new PriorityHelper(notificationPriority).getPriority());
            StartActivity(intent);
        }


    }
}