using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Rest;
using RaspberryPi.API.Controllers;
using System.Threading.Tasks;

namespace RaspberryPi.API.Infrastructure
{
    internal static class WebServer
    {
        internal static async Task InitializeWebServer()
        {
            var httpServer = new HttpServer(8800);
            var restRouteHandler = new RestRouteHandler();

            restRouteHandler.RegisterController<SensorController>();

            httpServer.RegisterRoute("api", restRouteHandler);

            await httpServer.StartServerAsync();
        }
    }
}
