using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Enum
{
    /// <summary>
    /// 回复类型枚举
    /// </summary>
    public enum ResponseMsgType
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// 图文
        /// </summary>
        News,
        /// <summary>
        /// 事件
        /// </summary>
        Event
    }
}