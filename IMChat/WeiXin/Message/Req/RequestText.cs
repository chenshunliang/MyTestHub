using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WeiXin.Common;
using WeiXin.Enum;
using WeiXin.Message.Base;
using WeiXin.Message.Res;

namespace WeiXin.Message.Req
{
    /// <summary>
    /// 文本消息请求
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class RequestText : BaseMessage
    {
        public RequestText()
        {
            this.MsgType = MsgTypeEnum.Text.ToString().ToLower();
        }

        public RequestText(BaseMessage info)
            : this()
        {
            this.FromUserName = info.FromUserName;
            this.ToUserName = info.ToUserName;
        }

        /// <summary>
        /// 内容
        /// </summary>        
        public string Content { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public long MsgId { get; set; }

    }
}