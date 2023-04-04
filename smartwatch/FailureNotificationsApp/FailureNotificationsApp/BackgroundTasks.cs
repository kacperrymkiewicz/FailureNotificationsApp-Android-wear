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
using Newtonsoft.Json;
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
            socket = IO.Socket("https://projektzespolowyitm-production.up.railway.app/");
            socket.On("newAwaria", (data) => {
                var notification = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                .SetContentTitle("Nowa awaria")
                .SetContentText("test")
                .SetSmallIcon(Resource.Drawable.notification_icon_background).Build();

                var manager = NotificationManagerCompat.From(this);
                manager.Notify(MainActivity.NOTIFICATION_ID, notification);

                var json = data[0] as JSONObject;
                Console.WriteLine(JsonConvert.DeserializeObject(json.ToString()));
                //socket.Emit("add user", "Xamarin Android");
                Intent statusService = new Intent(this, typeof(RaportActivity));
                StartActivity(statusService);
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
