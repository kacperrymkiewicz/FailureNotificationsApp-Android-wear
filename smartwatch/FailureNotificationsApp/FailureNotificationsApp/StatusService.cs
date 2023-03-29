using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Content;
using Android.Media;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name")]
    public class StatusActivity : WearableActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.StatusView);
            SetAmbientEnabled();
        }

    }
}