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
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using FailureNotificationsApp.Interfaces;
using Refit;
using FailureNotificationsApp.models;
using Android.Text;

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

        ListView FailureListView;
        List<Failure> failures;

        private const string apiUrl = "https://projektzespolowyitm-production.up.railway.app/api/awarie/pracownik/";
        IServiceAPI serviceAPI;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.StatusView);
            SetAmbientEnabled();

            username = FindViewById<TextView>(Resource.Id.pracownik_username);
            username.Text = "Pracownik: " + MainActivity.authUsername;

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true
            };
            var httpClient = new HttpClient(handler)
            {
            BaseAddress = new Uri("https://projektzespolowyitm-production-7d0d.up.railway.app/api")
            };
            httpClient.DefaultRequestHeaders.Add("x-access-token", MainActivity.authToken);

            serviceAPI = RestService.For<IServiceAPI>(httpClient);
            failures = await serviceAPI.GetFailures(MainActivity.authUserID);
            List<string> failures_list = new List<string>();
            foreach(var failure in failures)
            {
                failures_list.Add(failure.stanowisko.nazwa + ", priorytet: " + new PriorityHelper(failure.priorytet).getPriority());
            }

            FailureListView = (ListView)FindViewById<ListView>(Resource.Id.listView1);
            FailureListView.Adapter = new ArrayAdapter(this, Resource.Layout.CustomItemList, failures_list);
            FailureListView.ItemClick += FailureListView_ItemClick;

            if (unseenNotification)
            {
                OpenRaportView();
                Finish();
            }

            socket = IO.Socket("https://projektzespolowyitm-production-7d0d.up.railway.app");
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

        private void FailureListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(DetailsActvity));

            intent.PutExtra("notificationDescription", failures[e.Position].opis_awarii);
            intent.PutExtra("notificationWorkstation", failures[e.Position].stanowisko.nazwa);
            intent.PutExtra("notificationPriority", new PriorityHelper(failures[e.Position].priorytet).getPriority());
            intent.PutExtra("notificationPriorityNumber", failures[e.Position].priorytet);
            StartActivity(intent);
            Toast.MakeText(Application.Context, failures[e.Position].opis_awarii, ToastLength.Short).Show();
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
            intent.PutExtra("notificationPriorityNumber", notificationPriority);
            StartActivity(intent);
        }

        private async Task<List<string>> GetFailureList(int employeeId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-access-token", MainActivity.authToken);
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;
                var response = await client.GetAsync(apiUrl + employeeId);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                //var obj = JsonConvert.DeserializeObject(result);
                //dynamic json_response = JObject.Parse(obj.ToString());
                //MainActivity.authUsername = jsonobj.user.imie + " " + jsonobj.user.nazwisko;

                List<string> failures = new List<string>();
                foreach (var failure in result)
                {
                    var obj = JsonConvert.DeserializeObject(result);
                    dynamic json_response = JObject.Parse(obj.ToString());
                    failures.Add(failure.ToString());
                    Console.WriteLine(failure.ToString());
                }
                Console.WriteLine(result.ToString());

                return failures;
            }
        }


    }
}