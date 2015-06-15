using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Message.Req;

namespace WeiXin.Handle
{
    /// <summary>
    /// 微信请求处理应答类
    /// </summary>
    public class WeixinApiDispatch
    {
        /// <summary>
        /// 处理消息handle
        /// </summary>
        private static readonly WeixinAction ActionHandle = new WeixinAction();

        /// <summary>
        /// 对外处理
        /// </summary>
        /// <param name="postStr"></param>
        /// <returns></returns>
        public string Execute(string postStr)
        {
            string msgType = "";
            string responseTest = "";
            switch (msgType)
            {
                case "text":
                    responseTest = ActionHandle.HandleText(new RequestText());
                    break;
            }
            return responseTest;
        }
    }
}