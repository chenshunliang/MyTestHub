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
    /// 图片消息请求
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class RequestImage : BaseMessage
    {
        public RequestImage()
        {
            this.MsgType = MsgTypeEnum.Image.ToString().ToLower();
        }

        public RequestImage(BaseMessage info)
            : this()
        {
            this.FromUserName = info.FromUserName;
            this.ToUserName = info.ToUserName;
        }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public long MsgId { get; set; }
    }
}