using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Content;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using FailureNotificationsApp.helpers;
using Newtonsoft.Json.Linq;

namespace FailureNotificationsApp
{
    [Activity(Label = "@string/app_name", NoHistory = true)]
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

        private async Task<string> GetAuthToken(string login, string pass)
        {
            using (var client = new HttpClient())
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;
                //var credentials = new { username, password };
                var content = new StringContent(JsonConvert.SerializeObject(new { username = login, password = pass }), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                var obj = JsonConvert.DeserializeObject(result);
                dynamic jsonobj = JObject.Parse(obj.ToString());
                MainActivity.authUsername = jsonobj.user.imie + " " + jsonobj.user.nazwisko;
                return result;
            }
        }


        private async void LoginSubmit(object sender, EventArgs e)
        {
            try
            {
                string token = await GetAuthToken(login.Text.ToString(), password.Text.ToString());
                MainActivity.authToken = token;
                MainActivity.isLoggedIn = true;
                Toast.MakeText(Application.Context, "Pomyślnie zalogowano", ToastLength.Short).Show();
                Intent statusService = new Intent(this, typeof(StatusService));
                StartActivity(statusService);
            }
            catch(Exception ex) 
            {
                Toast.MakeText(Application.Context, "Nieprawidłowy login lub hasło", ToastLength.Short).Show();
            }
        }
    }
}