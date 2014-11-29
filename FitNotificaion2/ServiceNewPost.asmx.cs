using Facebook;
using FitNotificaion2.Controllers;
using FitNotificaion2.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;

namespace FitNotificaion2
{
    /// <summary>
    /// Summary description for ServiceNewPost
    /// </summary>
    [WebService(Namespace = "http://localhost:10768/ServiceNewPost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceNewPost : System.Web.Services.WebService
    {
        FitNotificationDBEntities db = new FitNotificationDBEntities();
        FacebookClient client;
        Unities unit = new Unities();

        private static DateTime _lastTimeUpdate = new DateTime(2013, 01, 01);

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string TimPostMoi()
        {
            if ((DateTime.Now - _lastTimeUpdate).TotalHours < 2)
            {
                return "FAIL because last time update < 15m: " + _lastTimeUpdate.ToString();
            }
            else
            {
                _lastTimeUpdate = DateTime.Now;
            }
            DeleteOldPost();
            
            try
            {
                List<NewPost> listpost = new List<NewPost>();
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDocument = htmlWeb.Load("http://www.fit.hcmus.edu.vn/vn/");
                HtmlNodeCollection nodelist = htmlDocument.DocumentNode.SelectNodes("//*[@id='dnn_ctr989_ModuleContent']//table");

                foreach (HtmlNode node in nodelist)
                {
                    NewPost item = new NewPost();

                    HtmlNode tagA_lable = node.ChildNodes[1].ChildNodes[5].ChildNodes[1];
                    item.TieuDe = tagA_lable.InnerText.Trim();
                    //item.href = "http://www.fit.hcmus.edu.vn/vn/" + node.ChildNodes[1].ChildNodes[5].ChildNodes[1].Attributes["href"].Value;
                    item.href = "";
                    int day = Int16.Parse(node.ChildNodes[1].ChildNodes[1].InnerText.Trim());
                    int year = Int16.Parse(node.ChildNodes[1].ChildNodes[3].InnerText.Trim());
                    int month = Int16.Parse(node.ChildNodes[3].ChildNodes[1].InnerText.Trim());
                    item.NgayPost = new DateTime(year, month, day);

                    // Neu item co tag la <img> la post moi.

                    if (node.ChildNodes[1].ChildNodes[5].ChildNodes.Count() < 4)
                    {
                        item.LaPostMoi = false;
                    }
                    else
                    {
                        // Neu post duoc danh dau la moi (tren website khoa) thi kiem tra du lieu xem da duoc post notification chua
                        AddItemToDB(item);
                    }

                    listpost.Add(item);
                }

                callFBNotification(listpost);

            }
            catch (Exception e)
            {
                Calllog("Lỗi khi tìm post " + e.ToString());
            }
            Calllog("RUN SERVICE DONE!");

            return "OK";
        }

        // Xoa cac post co trong du lieu duoc danh dau voi ngay dang > 1 thang
        private void DeleteOldPost()
        {
            DateTime _now = DateTime.Now;

            List<Post> list = db.Posts.ToList();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if ((((DateTime.Now.Year - list[i].NgayTao.Value.Year) * 12) + _now.Month - list[i].NgayTao.Value.Month) >= 1)
                    {
                        Calllog("Xóa post " + list[i].Tieude);
                        db.Posts.Remove(list[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Calllog("Lỗi khi xóa post " + e.ToString());
            }

            db.SaveChanges();
        }

        private void AddItemToDB(NewPost item)
        {
            Post checkitem = db.Posts.SingleOrDefault(t => t.ID == item.href);
            if (checkitem == null)
            {
                checkitem = new Post();
                checkitem.ID = item.href;
                checkitem.NgayTao = DateTime.Now;
                checkitem.Tieude = item.TieuDe;

                try
                {
                    db.Posts.Add(checkitem);
                    db.SaveChanges();
                    Calllog("Thêm post " + checkitem.Tieude);
                }
                catch
                {
                    Calllog("Lỗi khi thêm Post " + checkitem.Tieude);
                }

                item.LaPostMoi = true;
                return;
            }
            else
            {
                item.LaPostMoi = false;
            }

        }

        private void callFBNotification(List<NewPost> listsends)
        {
            string st_acc = unit.getAccessToken();
            client = new FacebookClient(st_acc);

            dynamic nparams = new ExpandoObject();
            nparams.access_token = st_acc;

            List<NewPost> posts = new List<NewPost>();
            foreach (NewPost item in listsends)
            {
                if (item.LaPostMoi)
                {
                    posts.Add(item);
                }
            }

            if (posts.Count() == 0)
            {
                return;
            }
            if (posts.Count() == 1)
            {
                nparams.template = "Fit.hcmus.edu.vn đăng bài " + posts[0].TieuDe;
                nparams.href = posts[0].href;
            }
            else
            {
                nparams.template = "Fit.hcmus.edu.vn đăng " + posts.Count() + " bài mới!";
                nparams.href = "";
               
            }

            List<FBUser> users = db.FBUsers.ToList();
            foreach (FBUser user in users)
            {
                try
                {
                    string post = user.FBID.Trim() + "/notifications";
                    var response = client.Post(post, nparams);
                }
                catch
                {
                    Calllog("User " + user.FBID + " không send được notificaion!");
                }
                finally
                {

                }
            }

        }

        private void Calllog(string message)
        {
            SystemLog log = new SystemLog();
            log.DateCreate = DateTime.Now;
            log.Detail = message;

            db.SystemLogs.Add(log);
            db.SaveChanges();
        }
    }
}
