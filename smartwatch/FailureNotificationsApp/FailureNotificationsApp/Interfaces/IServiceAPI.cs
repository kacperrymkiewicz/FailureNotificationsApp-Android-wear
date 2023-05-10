using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FailureNotificationsApp.models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FailureNotificationsApp.Interfaces
{
    public interface IServiceAPI
    {
        [Get("/awarie/pracownik/{employeeId}")]
        Task<List<Failure>> GetFailures(int employeeId);
    }
}