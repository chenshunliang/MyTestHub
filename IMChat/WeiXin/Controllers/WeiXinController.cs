using System.Configuration;
using System.Web.Mvc;
using WeiXin.Common;

namespace WeiXin.Controllers
{
    public class WeiXinController : Controller
    {
        //
        // GET: /WeiXin/

        //token  A1A8887793ACFC199182A649E905DAAB
        private readonly string _appId = ConfigurationManager.AppSettings["weixin_appid"];
        private readonly string _appSecret = ConfigurationManager.AppSettings["weixn_appsecret"];

        public ActionResult Test()
        {
            return View();
        }

        /// <summary>
        /// 校验token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckToken()
        {
            return Content(WeixinHelper.WAuth());
        }

        /// <summary>
        /// 接受微 信消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMessage()
        {
            return null;
        }
    }
}
