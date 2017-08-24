using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ThrottleApp
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

            //register our Throttle Handler globally
            //config.MessageHandlers.Add(new ThrottleHandler());
        }
    }
}
