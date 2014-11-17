using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitNotificaion2.Controllers
{
    public class Unities
    {
        public string getAccessToken()
        {
            var fb = new FacebookClient();
            dynamic result = fb.Get("oauth/access_token", new
            {
                client_id = "383036875207364",
                client_secret = "36665fb5bc72f8ed2dcfd743a5ec047e",
                grant_type = "client_credentials",
                spore = "user_groups"
            });


            var apptoken = result.access_token;
            return apptoken;
        }
    }


}