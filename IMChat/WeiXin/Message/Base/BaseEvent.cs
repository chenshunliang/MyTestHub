using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Enum;

namespace WeiXin.Message.Base
{
    /// <summary>
    /// 事件基类
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class BaseEvent : BaseMessage
    {
        public BaseEvent()
        {
            this.MsgType = MsgTypeEnum.Event.ToString().ToLower();
        }

        public BaseEvent(BaseMessage info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        /// <summary>
        /// 事件内容
        /// </summary>
        public string Event { get; set; }
    }
}