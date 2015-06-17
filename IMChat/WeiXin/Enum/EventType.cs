using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Enum
{
    /// <summary>
    /// 事件类型枚举
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 订阅
        /// </summary>
        subscribe,
        /// <summary>
        /// 取消订阅
        /// </summary>
        unsubscribe,
        /// <summary>
        /// 已关注
        /// </summary>
        SCAN,

    }
}