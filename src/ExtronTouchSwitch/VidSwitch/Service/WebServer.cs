using Devkoes.Restup.WebServer.File;
using Devkoes.Restup.WebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidSwitch.Service
{
    class WebServer
    {
        private const int LISTEN_PORT = 7777;
        private const string STATIC_ASSETS_FOLDER = @"Web";

        private HttpServer httpServer;

        public WebServer()
        {
            httpServer = new HttpServer(LISTEN_PORT);
            httpServer.RegisterRoute(new StaticFileRouteHandler(STATIC_ASSETS_FOLDER));
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
