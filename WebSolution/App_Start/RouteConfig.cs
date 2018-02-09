using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSolution
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Template_company_read",
                url: "{controller}/Company/read/{id}",
                defaults: new { controller = "Home", action = "Companyread", id = UrlParameter.Optional },
                constraints: new { controller = "Home", action = "Companyread" }
            );

            //routes.MapRoute(
            //    name: "templatePOProfile",
            //    url: "{controller}/template/POProfile/{action}",
            //    defaults: new { controller = "Home", action = "templatePOProfile", Action = UrlParameter.Optional },
            //    constraints: new { controller = "Home", action = "templatePOProfile" }
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "templateRoute",
                url: "{controller}/{action}/{templates}/{id}",
                defaults: new { controller = "Home", action = "POProfile", template = "templates", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "LogonRoute",
                url: "Login/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
                );


        }
    }
}
