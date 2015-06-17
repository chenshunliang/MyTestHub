using WeiXin.Common;
using WeiXin.Message.Req;
using WeiXin.Message.Res;

namespace WeiXin.Handle
{
    /// <summary>
    /// 微信消息实现
    /// </summary>
    public class WeixinAction : IWeixinAction
    {
        /// <summary>
        /// 文本消息处理
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string HandleText(RequestText info)
        {
            ResponseText resText = new ResponseText(info);
            if (info.Content == "")
            {
                resText.Content = "aaa";
            }
            return XMLUtil.Serializer(typeof(ResponseText), resText);
        }


        public string HandleImage(RequestImage info)
        {
            throw new System.NotImplementedException();
        }

        public string HandleEventSubscribe(RequestEventSubscribe info)
        {
            throw new System.NotImplementedException();
        }
    }
}