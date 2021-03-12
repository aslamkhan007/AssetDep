using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AssetDeprciation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
"ItActReport",
"ITActDepreciationReport/{controller}/{action}",
new { controller = "YearlyReport", action = "YearlyReport" },
new[] { "AssetDeprciation.Controllers.ReportController.ITActDepreciationReportController" }
);
                        routes.MapRoute(
"CompanyActReport",
"CompanyActDepreciationReport/{controller}/{action}",
new { controller = "YearlyReport", action = "YearlyReport" },
new[] { "AssetDeprciation.Controllers.ReportController.CompanyActDepreciationReportController" }
);
            
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Authenticate", action = "Login", id = UrlParameter.Optional }
            );



        }
    }
}
