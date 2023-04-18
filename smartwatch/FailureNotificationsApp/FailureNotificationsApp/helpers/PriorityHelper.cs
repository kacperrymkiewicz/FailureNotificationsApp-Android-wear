using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FailureNotificationsApp.helpers
{
    class PriorityHelper
    {
        private int priority;

        public PriorityHelper(int priority)
        {
            this.priority = priority;
        }

        public string getPriority()
        {
            if (priority == 1) { return "niski"; }
            if (priority == 2) { return "średni"; }
            if (priority == 3) { return "wysoki"; }
            return "";
        }

        public Color getPriorityColor()
        {
            if (priority == 1) { return Color.Yellow; }
            if (priority == 2) { return Color.Orange; }
            if (priority == 3) { return Color.Red; }
            return Color.Red;
        }
    }
}