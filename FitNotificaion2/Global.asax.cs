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

            ServiceNewPost.startservice();            
        }
    }
}
