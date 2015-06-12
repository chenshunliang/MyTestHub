using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Messages
{
    /// <summary>
    /// 视频消息
    /// </summary>
    public class VideoMessage : BaseMessage
    {
        /// <summary>
        /// 缩略图ID
        /// </summary>
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }
    }
}