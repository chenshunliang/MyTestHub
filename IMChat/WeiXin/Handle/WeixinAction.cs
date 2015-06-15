using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Message.Req;

namespace WeiXin.Handle
{
    public class WeixinAction : IWeixinAction
    {
        /// <summary>
        /// 文本消息处理
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string HandleText(RequestText info)
        {
            return "";
        }
    }
}