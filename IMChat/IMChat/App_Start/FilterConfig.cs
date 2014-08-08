using System.Web;
using System.Web.Mvc;
using IMChat.Common;

namespace IMChat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            //注册全局过滤
            filters.Add(new MyExceptionFileAttribute());
        }
    }
}