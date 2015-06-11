using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace WeiXin.Controllers
{
    public class WeiXinController : Controller
    {
        //
        // GET: /WeiXin/

        //token  A1A8887793ACFC199182A649E905DAAB
        private readonly string _appId = ConfigurationManager.AppSettings["weixin_appid"];
        private readonly string _appSecret = ConfigurationManager.AppSettings["weixn_appsecret"];

        /// <summary>
        /// 校验token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckToken()
        {
            string signature = Request["signature"];
            string timestamp = Request["timestamp"];
            string nonce = Request["nonce"];
            string echostr = Request["echostr"];

            string token = WeixinHelper.GetToken(_appId, _appSecret);
            string[] para = new string[] { token, timestamp, nonce };
            var str = "";
            Array.Sort(para);
            Array.ForEach(para, a => str += a);
            var value = Encoding.Default.GetBytes(str);
            HashAlgorithm sha1 = new SHA1CryptoServiceProvider();
            var hashData = sha1.ComputeHash(value);
            StringBuilder sb = new StringBuilder();
            foreach (byte t in hashData)
            {
                sb.Append(t.ToString("X2"));
            }
            if (echostr == sb.ToString())
                return Content(echostr);
            return Content("");
        }

        /// <summary>
        /// 获取微信token
        /// </summary>
        /// <returns></returns>
        public ActionResult GetToken()
        {
            string token = WeixinHelper.GetToken(_appId, _appSecret);
            return Content(token);
        }

        /// <summary>
        /// 接受微信消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMessage()
        {

        }
    }
}
