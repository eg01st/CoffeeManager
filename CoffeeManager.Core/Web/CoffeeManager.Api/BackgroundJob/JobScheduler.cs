using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace CoffeeManager.Api.BackgroundJob
{
    public class JobScheduler
    {
        public static async Task Start()
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
 
            IJobDetail job = JobBuilder.Create<AutoOrderJob>().Build();

                       ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s =>
                    s.WithIntervalInHours(1)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                )
                .Build();
 
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}