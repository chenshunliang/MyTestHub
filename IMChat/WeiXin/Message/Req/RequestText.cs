using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Enum;
using WeiXin.Message.Base;

namespace WeiXin.Message.Req
{
    /// <summary>
    /// 文本消息请求
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class RequestText : BaseMessage
    {
        public RequestText()
        {
            this.MsgType = ResponseMsgType.Text.ToString().ToLower();
        }

        public RequestText(BaseMessage info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        /// <summary>
        /// 内容
        /// </summary>        
        public string Content { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public string MsgId { get; set; }
    }
}