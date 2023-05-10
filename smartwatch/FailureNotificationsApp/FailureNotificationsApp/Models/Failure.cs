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
    public class Failure
    {
        public int id { get; set; }
        public int mozna_pracowac { get; set; }
        public string opis_awarii { get; set; }
        public int status { get; set; }
        public int priorytet { get; set; }
        public DateTime data_zgloszenia { get; set; }
        public object data_podjecia { get; set; }
        public object data_naprawy { get; set; }
        public Workstation stanowisko { get; set; }
    }
}