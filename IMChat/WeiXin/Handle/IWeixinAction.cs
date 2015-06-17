using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Message;
using WeiXin.Message.Req;

namespace WeiXin.Handle
{
    public interface IWeixinAction
    {
        /// <summary>
        /// 对文本请求信息进行处理
        /// </summary>
        /// <param name="info">文本信息实体</param>
        /// <returns></returns>
        string HandleText(RequestText info);

        /// <summary>
        /// 对图片请求信息进行处理
        /// </summary>
        /// <param name="info">图片信息实体</param>
        /// <returns></returns>
        string HandleImage(RequestImage info);

        //......

        /// <summary>
        /// 对订阅请求事件进行处理
        /// </summary>
        /// <param name="info">订阅请求事件信息实体</param>
        /// <returns></returns>
        string HandleEventSubscribe(RequestEventSubscribe info);

        /// <summary>
        /// 对菜单单击请求事件进行处理
        /// </summary>
        /// <param name="info">菜单单击请求事件信息实体</param>
        /// <returns></returns>
        //string HandleEventClick(RequestEventClick info);

        //......
    }
}