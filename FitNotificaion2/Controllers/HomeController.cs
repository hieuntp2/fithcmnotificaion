using FitNotificaion2.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitNotificaion2.Controllers
{
    public class HomeController : Controller
    {
        FitNotificationDBEntities db = new FitNotificationDBEntities();        

        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DaDangNhap");
            }
            return View();
        }

        public ActionResult DaDangNhap()
        {
            return View();
        }

        public ActionResult AddUser(string userid)
        {
            FBUser fbuser = db.FBUsers.SingleOrDefault(t => t.FBID == userid);
            if (fbuser == null)
            {
                fbuser = new FBUser();
                fbuser.FBID = userid;
                db.FBUsers.Add(fbuser);
                db.SaveChanges();
            }
            return RedirectToAction("Login","Account");
        }
    }
}