using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chatNow.Common;
using IMChat.Common;
using IMChat.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace IMChat.Controllers
{
    public class ChatController : BaseController
    {
        //
        // GET: /Chat/


        public ActionResult Index()
        {
            ViewBag.User = GetUserInfo();
            return View();
        }

        public ActionResult Default()
        {
            Users user = GetUserInfo();
            ViewBag.User = user;
            return View("Login");
        }

        public ActionResult ToRegister()
        {
            return View("Register");
        }

        //防止CSRF攻击
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register()
        {
            string uname = Request["UserName"];
            string pwd = Request["Salt"];
            string nickname = Request["NickName"];
            Users user = new Users();
            user.UserId = ObjectId.GenerateNewId();
            user.UserName = uname;
            user.NickName = nickname;
            //安全密码机制
            user.Salt = PwdSalt.GenerateSalt();
            user.Hash = PwdSalt.EncodePassword(pwd, user.Salt);
            user.Status = 1;
            MongoCollection mc = MongoDBHelper.GetCollection("Chatting", "Users");
            if (mc.FindOneAs<Users>(Query.EQ("UserName", uname)) != null)
                return Content("存在相同的用户名");
            WriteConcernResult wcr = mc.Insert<Users>(user);
            if (wcr.Ok)
            {
                //写入身份验证
                WriteAuthCookie(user);
                SSOUserActive sso = new SSOUserActive { SignID = ObjectId.GenerateNewId(), UserID = user.UserId.ToString(), UserName = user.UserName, ActiveTime = DateTime.Now.AddMinutes(20) };
                string signId = WriteSSO(sso);
                WriteSSOCookie(sso.SignID.ToString(), true);
                HttpContext.Cache.Insert("activeUser", sso);
                LogHelper.WriteLog("用户" + uname + "注册成功");
                return Content("注册成功");
            }
            else
                return Content("注册失败");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            string uname = Request["UserName"];
            string pwd = Request["Salt"];
            string path = Request["PathUrl"];
            MongoCollection mc = MongoDBHelper.GetCollection("Chatting", "Users");
            Users user = mc.AsQueryable<Users>().Where(m => m.UserName == uname).ToList()[0];
            //安全密码验证
            if (user.Hash == PwdSalt.EncodePassword(pwd, user.Salt))
            {
                //设置为在线状态
                //MongoDBHelper.ChangeStatus(1, user.UserId);
                //log4net记录日志
                LogHelper.WriteLog("用户" + uname + "登录成功");
                //存储登录验证cookie
                WriteAuthCookie(user);
                //20分钟过期
                SSOUserActive sso = new SSOUserActive { SignID = ObjectId.GenerateNewId(), UserID = user.UserId.ToString(), UserName = user.UserName, ActiveTime = DateTime.Now.AddMinutes(20) };
                string signId = WriteSSO(sso);
                WriteSSOCookie(sso.SignID.ToString(), true);
                //存储于缓存中
                HttpContext.Cache.Insert("activeUser", sso);
                //登录成功，跳转到之前页面
                if (!path.Contains("ReturnUrl"))
                    //如果访问的就是登录页，成功之后跳转聊天页
                    return Content("http://imchat.chen.com/Chat/Index");
                else
                    //如果其他页，成功之后回跳该页
                    return Content(path.Split(new string[] { "ReturnUrl=" }, StringSplitOptions.None)[1]);
            }
            else
            {
                return Content("登录失败");
            }
        }
        //注销
        public ActionResult LogOff()
        {
            ClearCookie();
            return RedirectToAction("Default", "Chat");
        }
        //用户管理
        public ActionResult Manage()
        {
            return RedirectToAction("Info", "Manager");
        }

        //通知用户
        public ActionResult Notice()
        {
            ViewBag.Message = Request["message"];
            ViewBag.Action = Request["action"];
            return View("Notice");
        }

        //程序简介
        public ActionResult About()
        {
            return View("About");
        }
        //联系方式
        public ActionResult Contact()
        {
            return View("Contact");
        }
    }
}
