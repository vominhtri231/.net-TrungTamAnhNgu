using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TrungTamAnhNgu.App_Start;

namespace TrungTamAnhNgu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteValueDictionary routeValue = new RouteValueDictionary();
            routeValue.Add("controller", "Download");
            routes.Add(new Route("Download", routeValue,new RouteHandler()));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
