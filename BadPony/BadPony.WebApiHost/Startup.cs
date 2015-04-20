using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using System.Net.Http.Formatting;

[assembly: OwinStartup(typeof(BadPony.WebApiHost.Startup))]

namespace BadPony.WebApiHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Routes.MapHttpRoute(
                name: "DefaultWebApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ); 

            config.Routes.MapHttpRoute(
                 name: "MoveController",
                 routeTemplate: "api/Move/{objectId}/{destinationId}",
                 defaults: new { controller = "Move" }
            );
            app.UseWebApi(config);
        }
    }
}
