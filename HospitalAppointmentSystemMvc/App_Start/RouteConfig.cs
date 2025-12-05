using System.Web.Mvc;
using System.Web.Routing;

namespace HospitalAppointmentSystemMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignore default requests for resources like .axd (trace, webresource, etc.)
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Default Route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
