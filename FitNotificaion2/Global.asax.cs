using FitNotificaion2.Controllers.Schedule;
using FitNotificaion2.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FitNotificaion2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //try
            //{
            //    // Start schedule service
            //    IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            //    scheduler.Start();

            //    IJobDetail job = JobBuilder.Create<ScheduleCallWebService>().Build();

            //    ITrigger trigger = TriggerBuilder.Create()
            //        .WithDailyTimeIntervalSchedule
            //          (s =>
            //             s.WithIntervalInHours(24)
            //                 //s.WithIntervalInSeconds(5)
            //            .OnEveryDay()
            //            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
            //          )
            //        .Build();

            //    scheduler.ScheduleJob(job, trigger);
            //    // end start schedule

            //    Calllog("Start Service done!");
            //}
            //catch
            //{
            //    Calllog("Start Service FAIL!");
            //}
            
        }

        private void Calllog(string message)
        {
            FitNotificationDBEntities db = new FitNotificationDBEntities();
            SystemLog log = new SystemLog();
            log.DateCreate = DateTime.Now;
            log.Detail = message;

            db.SystemLogs.Add(log);
            db.SaveChanges();
        }
    }
}
