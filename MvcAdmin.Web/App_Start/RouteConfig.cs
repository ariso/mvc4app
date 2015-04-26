using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcAdmin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //首页伪静态 IIS7测试通过
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}.html/{id}",
                defaults: new { controller = "Home", action = "Index.html", id = UrlParameter.Optional },
                namespaces: new string[] { "MvcAdmin.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default01",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "MvcAdmin.Web.Controllers" }
            );
            
        }
    }
}