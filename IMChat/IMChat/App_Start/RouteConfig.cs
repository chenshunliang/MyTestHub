using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IMChat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //忽略路由规则
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //    name: "Manager",
            //    url: "Manager/Manage/{id}",
            //    defaults: new { controller = "Manager", action = "Default", id = UrlParameter.Optional }
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Chat", action = "Default", id = UrlParameter.Optional }
            );
        }
    }
}