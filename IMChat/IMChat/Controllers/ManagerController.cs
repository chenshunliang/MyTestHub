using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chatNow.Common;
using IMChat.Common;
using IMChat.Models;
using MongoDB.Bson;

namespace IMChat.Controllers
{
    public class ManagerController : BaseController
    {
        //
        // GET: /Manager/

        //完善用户信息界面
        public ActionResult Info()
        {
            Users user = GetUserInfo();
            return View("Manage", user);
        }
        //添加用户详细资料
        public ActionResult AddInfo(Users user)
        {
            string uid = Request["UserId"];
            if (MongoDBHelper.UpdateUser(user, ObjectId.Parse(uid)))
                return Content("true");
            return Content("更新失败");
        }
        //上传头像
        public ActionResult UploadImageView()
        {
            Users user = GetUserInfo();
            return View("UploadImageView", user);
        }
        //图片上传
        public ActionResult UploadImage()
        {
            var file = Request.Files["Filedata"];
            string fileName = file.FileName;
            Users user = GetUserInfo();
            string path = "../HeadImage/" + GetUserInfo().UserId + "/";
            string absloute = Request.MapPath("../HeadImage/" + user.UserId + "/");
            if (file != null)
            {
                if (!Directory.Exists(absloute))
                    Directory.CreateDirectory(absloute);
                file.SaveAs(absloute + fileName);
                user.HeadImage = fileName;
                if (MongoDBHelper.UpdateUser(user, user.UserId))
                {
                    LogHelper.WriteLog("用户" + user.UserId + "成功上传头像" + fileName);
                    return Content(path + fileName);
                }
                else
                    return Content("上传失败");
            }
            return Content("没有要上传的文件");
        }
        //修改密码
        public ActionResult ChangePWDView()
        {
            Users user = GetUserInfo();
            return View("ChangePWD", user);
        }
        public ActionResult ChangePWD()
        {
            string oldpwd = Request["OldPwd"];
            string newpwd = Request["NewPwd"];
            string repeatpwd = Request["RepeatPwd"];
            if (newpwd != repeatpwd)
                return Content("两次新密码不一致");
            if (oldpwd == newpwd)
                return Content("新旧密码相同");
            Users user = MongoDBHelper.GetUserByID(Request["UserId"]);
            if (user.Hash != PwdSalt.EncodePassword(oldpwd, user.Salt))
                return Content("原密码错误");
            if (MongoDBHelper.ChangePwd(newpwd, Request["UserId"], user.Salt))
            {
                //清除登录凭证
                ClearCookie();
                LogHelper.WriteLog("用户" + user.UserId + "密码修改成功");
                return Content("修改成功");
            }
            else
                return Content("修改失败");
        }
        //删除用户档案
        public ActionResult SignOffView()
        {
            return View("SignOff");
        }
        [HttpPost]
        public ActionResult SignOff()
        {
            Users user = GetUserInfo();
            if (MongoDBHelper.RemoveUser(user))
                return Content("true");
            return Content("false");
        }
    }
}
