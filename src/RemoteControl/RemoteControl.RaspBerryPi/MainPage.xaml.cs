using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Rest;
using RemoteControl.RaspBerryPi.Controllers;
using RemoteControl.RaspBerryPi.Infrastructure;
using RemoteControl.RaspBerryPi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RemoteControl.RaspBerryPi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HttpServer _httpServer;

        public MainPage()
        {
            this.InitializeComponent();

            DataContext = new HeadedDemoViewModel();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeWebServer();
        }

        private async Task InitializeWebServer()
        {
            var httpServer = new HttpServer(8800);
            _httpServer = httpServer;

            var restRouteHandler = new RestRouteHandler();

            restRouteHandler.RegisterController<GpioController>();

            httpServer.RegisterRoute("api", restRouteHandler);

            await httpServer.StartServerAsync();
        }
    }
}
