using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Enum;
using WeiXin.Message.Base;

namespace WeiXin.Message.Res
{
    /// <summary>
    /// 回复文本消息
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class ResponseText : BaseMessage
    {
        public ResponseText()
        {
            this.MsgType = ResponseMsgType.Text.ToString().ToLower();
        }

        public ResponseText(BaseMessage info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        /// <summary>
        /// 内容
        /// </summary>        
        public string Content { get; set; }
    }
}