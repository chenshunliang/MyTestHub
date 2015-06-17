using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WeiXin.Enum;
using WeiXin.Message.Base;

namespace WeiXin.Message.Res
{
    /// <summary>
    /// 图片消息响应
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class ResponseImage : BaseMessage
    {
        public ResponseImage()
        {
            this.MsgType = MsgTypeEnum.Image.ToString().ToLower();
        }

        public ResponseImage(BaseMessage info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        public string MediaId { get; set; }
    }
}