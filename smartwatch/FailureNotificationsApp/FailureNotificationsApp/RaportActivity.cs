using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Content;
using Android.Media;
using SocketIOClient;
using System.Security.AccessControl;
using Android.Graphics;
using FailureNotificationsApp.helpers;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name", NoHistory = true)]
    public class RaportActivity : WearableActivity
    {

        Button btn;
        TextView description;
        TextView workstation;
        TextView priority;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RaportView);
            SetAmbientEnabled();

            description = FindViewById<TextView>(Resource.Id.opis_string);
            description.Text = Intent.GetStringExtra("notificationDescription");

            workstation = FindViewById<TextView>(Resource.Id.stanowisko_string);
            workstation.Text = "Stanowisko: " + Intent.GetStringExtra("notificationWorkstation");

            priority = FindViewById<TextView>(Resource.Id.priorytet_string);
            priority.Text = Intent.GetStringExtra("notificationPriority");
            priority.SetTextColor(new PriorityHelper(Intent.GetIntExtra("notificationPriorityNumber", 1)).getPriorityColor());

            StatusService.unseenNotification = false;
        }
    }
}