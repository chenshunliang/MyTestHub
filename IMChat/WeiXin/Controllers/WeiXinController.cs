using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using WeiXin.Handle;

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
        public ActionResult CheckToken()
        {
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                string postString;
                using (Stream stream = Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }

                if (!string.IsNullOrEmpty(postString))
                {
                    return Content(Execute(postString));
                }
                return Content("");
            }
            return Content(Auth());
        }

        /// <summary>
        /// 身份验证
        /// </summary>
        /// <returns></returns>
        private string Auth()
        {
            string token = ConfigurationManager.AppSettings["weixin_token"];//从配置文件获取Token


            string echoString = Request.QueryString["echoStr"];
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            if (CheckSignature(token, signature, timestamp, nonce))
                if (!string.IsNullOrEmpty(echoString))
                {
                    return echoString;
                }
            return "";
        }

        /// <summary>
        /// 进行排序加密
        /// </summary>
        /// <param name="token"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        private bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] arrTmp = { token, timestamp, nonce };

            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);

#pragma warning disable 618
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
#pragma warning restore 618
            if (tmpStr != null)
            {
                tmpStr = tmpStr.ToLower();

                if (tmpStr == signature)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 处理各种请求信息并应答（通过POST的请求）
        /// </summary>
        /// <param name="postStr">POST方式提交的数据</param>
        private string Execute(string postStr)
        {
            WeixinApiDispatch dispatch = new WeixinApiDispatch();
            string responseContent = dispatch.Execute(postStr);

            Response.ContentEncoding = Encoding.UTF8;
            return responseContent;
        }
    }
}
