using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Messages
{
    /// <summary>
    /// 微信公共参数
    /// </summary>
    public class WeixinParams
    {
        /// <summary>
        /// 是否加密
        /// </summary>
        public bool IsAes { get; set; }
        /// <summary>
        /// 接入token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        ///微信appid
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncodingAESKey { get; set; }
    }
}