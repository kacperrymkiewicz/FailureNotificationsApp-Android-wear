using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FailureNotificationsApp.models
{
    public class Workstation
    {
        public int id { get; set; }
        public string kod { get; set; }
        public string nazwa { get; set; }
        public string opis { get; set; }
    }
}