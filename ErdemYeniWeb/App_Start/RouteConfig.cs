using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ErdemYeniWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "BlogFollowingCancel",
                url: "abonelikiptal/{id}",
                defaults: new { controller = "Blog", action = "CancelFollowingBlog", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Index",
                url: "",
                defaults: new { controller = "Index", action = "Index"}
            );
            routes.MapRoute(
                name: "BlogIndex",
                url: "blog/{id}",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "BlogEntry",
                url: "blog/entry/{id}",
                defaults: new { controller = "Blog", action = "Entry", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "giris",
                defaults: new { controller = "Index", action = "Giris"}
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
