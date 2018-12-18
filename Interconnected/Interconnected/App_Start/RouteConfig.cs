using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Interconnected
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Login", action = "Index" },
                namespaces: new[] { "Interconnected.Controllers" }
            );

            routes.MapRoute(
                name: "Regis",
                url: "dang-ky",
                defaults: new { controller = "Login", action = "Regis" },
                namespaces: new[] { "Interconnected.Controllers" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "dang-xuat",
                defaults: new { controller = "Login", action = "Logout" },
                namespaces: new[] { "Interconnected.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "noi-quy",
                defaults: new { controller = "About", action = "Index" },
                namespaces: new[] { "Interconnected.Controllers" }
            );

            routes.MapRoute(
                name: "Category",
                url: "{category}-{id}",
                defaults: new { controller = "Categories", action = "Index", id = UrlParameter.Optional, category = UrlParameter.Optional },
                namespaces: new[] { "Interconnected.Controllers" }
            );

            routes.MapRoute(
                name: "DetailPost",
                url: "{category}-{idCate}/{post}-{id}",
                defaults: new { controller = "Categories", action = "DetailPost", id = UrlParameter.Optional, post = UrlParameter.Optional, idCate = UrlParameter.Optional, category = UrlParameter.Optional },
                namespaces: new[] { "Interconnected.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
