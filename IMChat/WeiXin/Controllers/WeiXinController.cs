using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeiXin.Tools;

namespace WeiXin.Controllers
{
    public class WeiXinController : Controller
    {
        //
        // GET: /WeiXin/

        //token  A1A8887793ACFC199182A649E905DAAB

        public ActionResult Index()
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
            string signature = Request["signature"];
            string timestamp = Request["timestamp"];
            string nonce = Request["nonce"];
            string echostr = Request["echostr"];

            string token = "A1A8887793ACFC199182A649E905DAAB";
            string[] para = new string[] { token, timestamp, nonce };
            var str = "";
            Array.Sort(para);
            Array.ForEach(para, a => str += a);
            var value = Encoding.Default.GetBytes(str);
            HashAlgorithm sha1 = new SHA1CryptoServiceProvider();
            var hashData = sha1.ComputeHash(value);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                sb.Append(hashData[i].ToString("X2"));
            }
            if (echostr == sb.ToString())
                return Content(echostr);
            else
                return Content("");
        }

    }
}
