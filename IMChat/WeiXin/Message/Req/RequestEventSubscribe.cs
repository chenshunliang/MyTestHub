using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WeiXin.Enum;
using WeiXin.Message.Base;

namespace WeiXin.Message.Req
{
    /// <summary>
    /// 订阅请求消息
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class RequestEventSubscribe : BaseEvent
    {
        public RequestEventSubscribe(EventType eType)
        {
            this.Event = eType.ToString();
        }
    }
}