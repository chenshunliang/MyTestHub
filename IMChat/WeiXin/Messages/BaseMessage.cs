using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Enum;

namespace WeiXin.Messages
{
    /// <summary>
    /// 消息体基类
    /// </summary>
    public abstract class BaseMessage
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号（openid）
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间（整型）
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType { get; set; }

        public virtual void ResponseNull()
        {
            
        }
        public virtual void ResText(WeixinParams param, string content)
        {

        }
        /// <summary>
        /// 回复消息(音乐)
        /// </summary>
        public void ResMusic(WeixinParams param, Music mu)
        {

        }
        public void ResVideo(WeixinParams param, Video v)
        {
        }

        /// <summary>
        /// 回复消息(图片)
        /// </summary>
        public void ResPicture(WeixinParams param, Picture pic, string domain)
        {
        }

        /// <summary>
        /// 回复消息（图文列表）
        /// </summary>
        /// <param name="param"></param>
        /// <param name="art"></param>
        public void ResArticles(WeixinParams param, List<Articles> art)
        {
        }
        /// <summary>
        /// 多客服转发
        /// </summary>
        /// <param name="param"></param>
        public void ResDKF(WeixinParams param)
        {
        }
        /// <summary>
        /// 多客服转发如果指定的客服没有接入能力(不在线、没有开启自动接入或者自动接入已满)，该用户会一直等待指定客服有接入能力后才会被接入，而不会被其他客服接待。建议在指定客服时，先查询客服的接入能力指定到有能力接入的客服，保证客户能够及时得到服务。
        /// </summary>
        /// <param name="param">用户发送的消息体</param>
        /// <param name="KfAccount">多客服账号</param>
        public void ResDKF(WeixinParams param, string KfAccount)
        {
        }
        private void Response(WeixinParams param, string data)
        {

        }
    }
}