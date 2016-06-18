using Devkoes.Restup.WebServer.File;
using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidSwitch.Model;

namespace VidSwitch.Service
{
    class WebServer
    {
        public const int LISTEN_PORT = 7777;
        private const string STATIC_ASSETS_FOLDER = @"Web";

        private HttpServer httpServer;

        public WebServer(Settings settings)
        {
            PresetsRESTController.Settings = settings;

            httpServer = new HttpServer(LISTEN_PORT);

            httpServer.RegisterRoute(new StaticFileRouteHandler(STATIC_ASSETS_FOLDER));

            var restRouteHandler = new RestRouteHandler();

            restRouteHandler.RegisterController<PresetsRESTController>();
            httpServer.RegisterRoute("api", restRouteHandler);
        }

        public async void Start()
        {
            await this.httpServer.StartServerAsync();
        }

        public void Stop()
        {
            this.httpServer.StopServer();
        }
    }
}
