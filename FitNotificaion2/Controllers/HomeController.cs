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
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Baiviet()
        {

            List<Post> posts = db.Posts.OrderBy(t => t.NgayTao).ToList();
            return View(posts);
        }

        public ActionResult DaDangNhap()
        {
            return View();
        }

        public ActionResult ChinhSachBaoMat()
        {
            return View();
        }

        public ActionResult DieuKhoan()
        {
            return View();
        }

        public ActionResult GioiThieu()
        {
            return View();            
        }
    }

}