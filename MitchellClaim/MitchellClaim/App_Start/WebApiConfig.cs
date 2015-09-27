using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MitchellClaim
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "GetClaimData",
               routeTemplate: "api/{controller}/{action}/{start}/{end}"
           );
            config.Routes.MapHttpRoute(
              name: "GetVehicle",
              routeTemplate: "api/{controller}/{claimID}/{vehicleID}"
          );
        }
    }
}
