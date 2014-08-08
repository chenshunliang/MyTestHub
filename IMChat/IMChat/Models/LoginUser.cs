using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Caching;
using chatNow.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IMChat.Models
{
    //返回的信息
    public class LoginUser
    {
        public LoginUser(string cookieVal)
        {
            Cache ca = new Cache();
            SSOUserActive sso = HttpContext.Current.Cache.Get("activeUser") as SSOUserActive;
            if (sso == null)
                sso = MongoDBHelper.Get(cookieVal);
            if (sso != null)
            {
                UserName = sso.UserName;
                UserID = sso.UserID;
            }
            else
            {
                UserName = "";
                UserID = "";
            }
        }

        public string UserName { get; set; }
        public string UserID { get; set; }
    }
}