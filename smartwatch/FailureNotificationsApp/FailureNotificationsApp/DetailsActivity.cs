using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Wearable.Activity;
using Android.Views;
using Android.Widget;
using FailureNotificationsApp.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name", NoHistory = true)]
    public class DetailsActvity : WearableActivity
    {

        Button back_button;
        Button finish_button;
        TextView description;
        TextView workstation;
        TextView priority;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DetailView);
            SetAmbientEnabled();

            description = FindViewById<TextView>(Resource.Id.opis_string_details);
            description.Text = Intent.GetStringExtra("notificationDescription");

            workstation = FindViewById<TextView>(Resource.Id.stanowisko_string_details);
            workstation.Text = "Stanowisko: " + Intent.GetStringExtra("notificationWorkstation");

            priority = FindViewById<TextView>(Resource.Id.priorytet_string_details);
            priority.Text = Intent.GetStringExtra("notificationPriority");
            priority.SetTextColor(new PriorityHelper(Intent.GetIntExtra("notificationPriorityNumber", 1)).getPriorityColor());

            back_button = FindViewById<Button>(Resource.Id.back_button);
            back_button.Click += Back_button_Click;

            finish_button = FindViewById<Button>(Resource.Id.finish_button);
            finish_button.Click += Finish_button_Click;
        }

        private void Finish_button_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Back_button_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}