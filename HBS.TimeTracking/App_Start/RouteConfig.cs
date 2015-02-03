using System.Web.Mvc;
using System.Web.Routing;

namespace HeartbeatService.TimeTracking
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TimeTrack", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "HeartbeatService.TimeTracking.Controllers" }
            );
        }
    }
}