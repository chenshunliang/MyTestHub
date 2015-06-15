using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WeiXin.Common;

namespace WeiXin.Message.Base
{
    /// <summary>
    /// 基础消息内容
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class BaseMessage
    {
        /// <summary>
        /// 初始化一些内容，如创建时间为整形，
        /// </summary>
        public BaseMessage()
        {
            this.CreateTime = DateTime.Now.DateTimeToInt();
        }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

        public virtual string ToXml()
        {
            this.CreateTime = DateTime.Now.DateTimeToInt();//重新更新
            return XMLUtil.Serializer(typeof(BaseMessage), this);
        }
    }
}