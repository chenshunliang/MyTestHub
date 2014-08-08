using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMChat.Common
{
    public class MyExceptionFileAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //处理错误消息，将其跳转到一个页面
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(MyExceptionFileAttribute));
            log.Error(filterContext.Exception.Message + filterContext.Exception.Source);
            //页面跳转到错误页面
            filterContext.HttpContext.Response.Redirect("../Error.html");
        }
    }
}