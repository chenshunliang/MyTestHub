using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Messages
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMessage : BaseMessage
    {
        /// <summary>
        /// 缩略图ID
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音识别结果
        /// </summary>
        public string Recognition { get; set; }
    }
}