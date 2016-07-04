using Microsoft.Practices.Unity;
using RaspberryPi.API.Infrastructure;
using RaspberryPi.API.Infrastructure.Interfaces;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RaspberryPi.API
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeContainer(new UnityContainer());

            await WebServer.InitializeWebServer();
        }

        private void InitializeContainer(IUnityContainer container)
        {
            container.RegisterType<II2CBus, I2CBus>(new PerResolveLifetimeManager());
        }
    }
}
