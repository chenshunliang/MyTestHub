using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Enum
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        ///文本类型
        /// </summary>
        TEXT = 1,
        /// <summary>
        /// 图片类型
        /// </summary>
        IMAGE = 2,
        /// <summary>
        /// 语音类型
        /// </summary>
        VOICE = 4,
        /// <summary>
        /// 视频类型
        /// </summary>
        VIDEO = 8,
        /// <summary>
        /// 地理位置类型
        /// </summary>
        LOCATION = 16,
        /// <summary>
        /// 链接类型
        /// </summary>
        LINK = 32,
        /// <summary>
        /// 事件类型
        /// </summary>
        EVENT = 64
    }
}