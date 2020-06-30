using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tester
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "",
                new { controller = "Test", action = "Index" });

            routes.MapRoute(
                null,
                "AllResults",
                new { controller = "Results", action = "AllResults", page = 1 }
                );

            routes.MapRoute(
                null,
                "AllResults/Page{page}",
                new { controller = "Results", action = "AllResults" },
                new { page = @"\d+" }
                );

            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}