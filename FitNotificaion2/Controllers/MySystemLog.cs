using FitNotificaion2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitNotificaion2.Controllers
{
    public static class MySystemLog
    {
        private static FitNotificationDBEntities db = new FitNotificationDBEntities();

        public static void Calllog(string message)
        {
            SystemLog log = new SystemLog();
            log.DateCreate = DateTime.Now;
            log.Detail = message;


            db.SystemLogs.Add(log);
            db.SaveChanges();
        }
    }
}