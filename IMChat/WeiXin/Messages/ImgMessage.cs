using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Messages
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImgMessage : BaseMessage
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicUrl { get; set; }
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