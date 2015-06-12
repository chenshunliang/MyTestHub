using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Messages
{
    /// <summary>
    /// 链接消息
    /// </summary>
    public class LinkMessage : BaseMessage
    {
        /// <summary>
        /// 缩略图ID
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }

    }
}