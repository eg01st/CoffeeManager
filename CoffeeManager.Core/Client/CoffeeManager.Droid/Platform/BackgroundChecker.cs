using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.App.Job;
using Android.Content;
using CoffeeManager.Core;

namespace CoffeeManager.Droid
{
    public class BackgroundChecker : IBackgroundChecker
    {
        //use the same time as in JobInfo.MinPeriodMillis(api 24)
        private const long PeriodicInterval = 20 * 60 * 1000L;

        private const int JobId = 123456;
        private JobScheduler jobScheduler;
        private Context context;

        public BackgroundChecker()
        {
            context = Application.Context;
            jobScheduler = (JobScheduler)context.GetSystemService(Context.JobSchedulerService);
        }

        public Task StartBackgroundChecks()
        {
            if (!jobScheduler.AllPendingJobs.Any(x => x.Id == JobId))
            {
                var javaClass = Java.Lang.Class.FromType(typeof(AutoOrderCheckJobService));
                var component = new ComponentName(context, javaClass);
                var builder = new JobInfo.Builder(JobId, component)
                    .SetRequiredNetworkType(NetworkType.Unmetered)
                    .SetPeriodic(PeriodicInterval)
                    .SetPersisted(true);

                var jobInfo = builder.Build();
                var result = jobScheduler.Schedule(jobInfo);
                if (result == JobScheduler.ResultSuccess)
                {
                    System.Diagnostics.Debug.WriteLine("JobScheduler created successfully");
                    // The job was scheduled.
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("JobScheduler creation failed");
                    // Couldn't schedule the job.
                }
            }
            return Task.FromResult(true);
        }

        public Task StopBackgroundChecks()
        {
            jobScheduler?.Cancel(JobId);
            return Task.FromResult(true);
        }
    }
}
