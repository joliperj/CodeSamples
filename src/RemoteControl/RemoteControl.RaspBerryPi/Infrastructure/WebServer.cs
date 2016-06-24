using Devkoes.Restup.WebServer.Rest;
using RemoteControl.RaspBerryPi.Controllers;
using Devkoes.Restup.WebServer.Http;
using System.Threading.Tasks;

namespace RemoteControl.RaspBerryPi.Infrastructure
{
    public sealed class WebServer
    {
        private HttpServer _httpServer;

        public async Task<HttpServer> Run()
        {
            var restRouteHandler = new RestRouteHandler();
            MapControllers(restRouteHandler);

            _httpServer = new HttpServer(8800);
            _httpServer.RegisterRoute("api", restRouteHandler);
            await _httpServer.StartServerAsync();

            return _httpServer;
        }

        private void MapControllers(RestRouteHandler restRouteHandler)
        {
            restRouteHandler.RegisterController<GpioController>();
        }
    }
}
