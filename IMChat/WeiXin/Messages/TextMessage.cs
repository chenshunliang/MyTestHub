using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Messages
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage : BaseMessage
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }
    }
}