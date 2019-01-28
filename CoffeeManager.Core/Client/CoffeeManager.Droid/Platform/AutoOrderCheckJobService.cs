using System;
using Android.App;
using Android.App.Job;
using CoffeeManager.Core.Extensions;

namespace CoffeeManager.Droid
{
    [Service(Exported = true, Label = "Notification JobService", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class AutoOrderCheckJobService : JobService
    {
        public AutoOrderCheckJobService()
        {
        }

        public override bool OnStartJob(JobParameters @params)
        {
            InventoryExtensions.CheckInventory();

            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            return true;
        }
    }
}
