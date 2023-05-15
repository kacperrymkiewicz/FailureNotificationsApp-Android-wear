using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using FailureNotificationsApp;
using FailureNotificationsApp.helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.Json;
using SocketIO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace FailureNotificationsApp
{

    [Service]
    public class BackgroundTasks : Service
    {
        Timer timer;
        SocketIO.Client.Socket socket;
        public override void OnCreate()
        {
            base.OnCreate();
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            socket = IO.Socket("https://projektzespolowyitm-production.up.railway.app?token=" + MainActivity.authToken);
            socket.On("assignedAwaria", (data) => {
                if(MainActivity.isLoggedIn)
                {
                    var json = data[0] as JSONObject;
                    var obj = JsonConvert.DeserializeObject(json.ToString());
                    dynamic jsonobj = JObject.Parse(obj.ToString());
                    string description = "Priorytet: " + new PriorityHelper((int)jsonobj.updated.priorytet).getPriority();

                    Console.WriteLine(JsonConvert.DeserializeObject(json.ToString()));

                    var notification = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                    .SetContentTitle("Nowa awaria")
                    .SetContentText(description)
                    .SetSmallIcon(Resource.Drawable.notification_icon_background).Build();

                    var manager = NotificationManagerCompat.From(this);
                    manager.Notify(MainActivity.NOTIFICATION_ID, notification);
                }
            });

            socket.Connect();
            //timer = new Timer(HandleTimerCallback, 0, 0, 3000);
            return base.OnStartCommand(intent, flags, startId);
        }

        private void HandleTimerCallback(object state)
        {
            //Console.WriteLine(MainActivity.test);
        }
    }

}
