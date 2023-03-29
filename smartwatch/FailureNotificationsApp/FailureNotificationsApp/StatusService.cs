using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Content;
using Android.Media;
using SocketIOClient;
using System.Security.AccessControl;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name")]
    public class StatusService : WearableActivity
    {

        Button logout_button;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.StatusView);
            SetAmbientEnabled();

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
        }

    }
}