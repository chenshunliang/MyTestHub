using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;
using chatNow.Common;
using IMChat.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace IMChat.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/


        //action执行之前
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            List<string> actionNames = new List<string>() { "ToRegister", "Login", "Register", "Notice" };
            if (actionNames.Contains(filterContext.ActionDescriptor.ActionName))
                return;
            if (GetCookie() == null)
            //进行跳转
            {
                //获取当前请求的绝对路经
                string path = filterContext.HttpContext.Request.Url.AbsoluteUri;
                //组建返回路径
                string returnPath = string.Format(FormsAuthentication.LoginUrl, path);
                //初次登录校验是否存在登录凭证
                if (filterContext.ActionDescriptor.ActionName != "Default")
                    filterContext.Result = Redirect(returnPath);
            }
            else
            {
                CheckSuccess(filterContext);
            }
        }

        //校验成功后操作
        public void CheckSuccess(ActionExecutingContext filterContext)
        {
            string sign = Request.Cookies["myLog"].Value;
            LoginUser luser = new LoginUser(sign);
            //获取当前用户
            SSOUserActive currentUser = MongoDBHelper.Get(sign);
            if (currentUser != null)
            {
                //登录成功状态
                HttpContext.Cache.Insert("activeUser", currentUser);
            }
            else
            {
                //登录成功，但不是当前用户
                //同一域下可使
                if (luser.UserID != "")
                {
                    ClearCookie();
                    //跳转到提醒页
                    filterContext.Result = RedirectToAction("Notice", "Chat", new { message = "用户在其他地方登录，本地被迫下线，请重新登录", action = "toLogin" });
                }
                else
                    filterContext.Result = RedirectToAction("Default", "Chat");
            }
        }


        //记录form验证凭据
        public void WriteAuthCookie(Users user)
        {
            string auth = user.UserId.ToString();
            //生成票据
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.NickName, DateTime.Now, DateTime.Now.AddHours(2), false, auth, "/");

            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie loginCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            //有待改进
            loginCookie.Expires = DateTime.Now.AddMinutes(20);
            Response.Cookies.Add(loginCookie);
        }

        //写入mongodb 记录登录sso凭证
        public string WriteSSO(SSOUserActive ssouser)
        {
            MongoCollection mc = MongoDBHelper.GetCollection("Chatting", "SSOUser");
            if (mc.FindOneAs<SSOUserActive>(Query.EQ("UserID", ssouser.UserID)) != null)
            {
                //清除原来的登录凭据
                mc.Remove(Query.EQ("UserID", ssouser.UserID));
            }
            if (mc.Insert<SSOUserActive>(ssouser).Ok)
                return ssouser.SignID.ToString();
            return "";
        }
        //写入sso cookie
        public void WriteSSOCookie(string value, bool croessDomain)
        {
            //myLog  sso凭证是否设置过期时间
            //如果需要自动登录可加入过期时间
            HttpCookie ssoCookie = new HttpCookie("myLog");
            //有待改进
            ssoCookie.Expires = DateTime.Now.AddMinutes(20);
            ssoCookie.Value = value;
            //IE下跨域
            if (croessDomain)
            {
                Response.Headers.Add("P3P", "CP=CAO PSA OUR");
            }
            Response.Cookies.Add(ssoCookie);
        }

        public Users GetCookie()
        {
            Users user = null;
            if (User.Identity.IsAuthenticated)
            {
                string userData;
                FormsIdentity fi = (FormsIdentity)User.Identity;
                FormsAuthenticationTicket ticket = fi.Ticket;
                userData = ticket.UserData;
                if (userData != null)
                {
                    user = SetUserData(userData);
                }
            }
            return user;
        }

        private Users SetUserData(string userData)
        {
            return MongoDBHelper.GetUserByID(userData);
        }

        //清除凭证,伪单点(个人感觉)
        protected void ClearCookie()
        {
            //清除登录凭证
            FormsAuthentication.SignOut();
            //清楚单点凭证
            HttpCookie ccoie = Request.Cookies["myLog"];
            ccoie.Expires = DateTime.Now.AddMinutes(-1);
            Response.Cookies.Add(ccoie);
            HttpContext.Cache.Remove("activeUser");
            //是否清除单点Mongodb记录

        }
        //获取当前用户信息
        public Users GetUserInfo()
        {
            Users user = new Users();
            SSOUserActive sso = HttpContext.Cache["activeUser"] as SSOUserActive;
            if (sso != null)
            {
                user = MongoDBHelper.GetUserByID(sso.UserID);
            }
            return user;
        }

    }
}
