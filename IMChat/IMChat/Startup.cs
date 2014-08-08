using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
[assembly: OwinStartup(typeof(IMChat.Startup))]

namespace IMChat
{
    //初始配置，SignalR初始配置
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);//允许跨域
            app.MapSignalR();//路由映射
        }
    }
}