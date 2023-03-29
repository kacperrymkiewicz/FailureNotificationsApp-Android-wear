using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FailureNotificationsApp
{
    public class WebsocketData
    {
        public bool mozna_pracowac;
        public string opis_awarii;
        public int priorytet;
        public int stanowisko;

        public static WebsocketData FromData(Java.Lang.Object[] data)
        {
            if (data != null && data.Length == 1)
            {
                var json = data[0] as JSONObject;
                return JsonConvert.DeserializeObject<WebsocketData>(json.ToString());
            }
            return null;
        }
    }
}