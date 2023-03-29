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
    public class LoginController : WearableActivity
    {

        EditText login, password;
        Button login_button;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginView);

            SetAmbientEnabled();

            login = FindViewById<EditText>(Resource.Id.login_input);
            password = FindViewById<EditText>(Resource.Id.password_input);

            login_button = FindViewById<Button>(Resource.Id.login_button);
            login_button.Click += LoginSubmit;
        }


        private void LoginSubmit(object sender, EventArgs e)
        {
            if(login.Text.ToString() == "koc")
            {
                Toast.MakeText(Application.Context, "Pomyślnie zalogowano", ToastLength.Short).Show();
                Intent statusService = new Intent(this, typeof(StatusService));
                StartActivity(statusService);
            }
            Toast.MakeText(Application.Context, "Nieprawidłowy login lub hasło", ToastLength.Short).Show();
        }
    }
}