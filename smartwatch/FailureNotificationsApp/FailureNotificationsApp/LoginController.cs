﻿using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Content;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name")]
    public class LoginController : WearableActivity
    {

        private const string apiUrl = "https://projektzespolowyitm-production.up.railway.app/api/auth/login";


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

        private async Task<string> GetAuthToken(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;
                var credentials = new { username, password };
                var content = new StringContent(@"{""username"" : ""test123"", ""password"" : ""test""}", Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }


        private async void LoginSubmit(object sender, EventArgs e)
        {
            if(login.Text.ToString() == "koc")
            {
                string token = await GetAuthToken("test123", "test");
                Console.WriteLine(token);
                Toast.MakeText(Application.Context, "Pomyślnie zalogowano", ToastLength.Short).Show();
                Intent statusService = new Intent(this, typeof(StatusService));
                StartActivity(statusService);
            }
            Toast.MakeText(Application.Context, "Nieprawidłowy login lub hasło", ToastLength.Short).Show();
        }
    }
}