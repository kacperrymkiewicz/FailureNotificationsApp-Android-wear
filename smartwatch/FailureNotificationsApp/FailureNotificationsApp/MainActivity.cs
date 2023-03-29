﻿using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        TextView textView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginView);

            textView = FindViewById<TextView>(Resource.Id.text);
            SetAmbientEnabled();
        }
    }
}


