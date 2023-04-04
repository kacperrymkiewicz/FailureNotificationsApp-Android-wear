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
    public class RaportActivity : WearableActivity
    {

        Button btn;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RaportView);
            SetAmbientEnabled();
        }
    }
}