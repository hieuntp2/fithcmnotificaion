using Facebook;
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DaDangNhap");
            }

            return View();
        }

        public ActionResult DaDangNhap()
        {
            return View();
        }

        //[HttpPost]
        //public string addFBUser(string accesstoken)
        //{
        //    // Add User to database
        //    Unities uni = new Unities();
        //    var facebookClient = new FacebookClient(accesstoken);
        //    var me = facebookClient.Get("me") as JsonObject;
        //    string uid = me["id"].ToString();

        //    FBUser fbuser = db.FBUsers.SingleOrDefault(t => t.FBID == uid);

        //    addAspNetUser(uid);
        //    if (fbuser == null)
        //    {

        //        fbuser = new FBUser();
        //        fbuser.FBID = uid;
        //        db.FBUsers.Add(fbuser);
        //        db.SaveChanges();
        //        return "ADD";
        //    }

        //    return "ALR";
        //}

        //private string addAspNetUser(string userid)
        //{
        //    AccountController controler = new AccountController();
        //    return controler.createaAccount(userid,"123");
        //}

    }

}