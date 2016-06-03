using Microsoft.Maker.RemoteWiring;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Connect.Arduino;
using Windows.UI.Xaml.Controls;

namespace AutoDiscoverArduino.App
{
    public sealed partial class MainPage : Page
    {
        private RemoteDevice arduino;
        private const int relayPin = 2;
        private CoreDispatcher UiDispatcher
        {
            get { return Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher; }
        }

        public MainPage()
        {
            this.InitializeComponent();

            Log("Start");

            Connect();
        }

        private async Task Connect()
        {
            await Log("Connecting to device...");

            arduino = await AutoDiscover.ConnectDevice();
            if (arduino == null)
                await Log("Device not found.");

            arduino.DeviceReady += Arduino_DeviceReady;
            arduino.DeviceConnectionFailed += Arduino_DeviceConnectionFailed;
            arduino.DeviceConnectionLost += Arduino_DeviceConnectionLost;
            arduino.DigitalPinUpdated += Arduino_DigitalPinUpdated;
            arduino.AnalogPinUpdated += Arduino_AnalogPinUpdated;
        }

        private async void Arduino_AnalogPinUpdated(string pin, ushort value)
        {
            await Log($"Analogpin {pin} reported value {value}");
        }

        private async void Arduino_DigitalPinUpdated(byte pin, PinState state)
        {
            await Log($"Digitalpin {pin} updated to state {state.ToString()}");
        }

        private async void Arduino_DeviceConnectionLost(string message)
        {
            await ToggleUI(false);
            await Log("Device connection lost");
        }

        private async void Arduino_DeviceConnectionFailed(string message)
        {
            await ToggleUI(false);
            await Log("Device connection failed");
        }

        private async void Arduino_DeviceReady()
        {
            arduino.pinMode(relayPin, PinMode.OUTPUT);
            await ToggleUI(true);
            await Log("Device ready");
        }

        private async void Connection_ConnectionFailed(string message)
        {
            await Log("Connection failed");
        }

        private async void Connection_ConnectionEstablished()
        {
            await Log("Connection established");
        }

        private async void btnOn_Click(object sender, RoutedEventArgs e)
        {
            arduino.digitalWrite(relayPin, PinState.HIGH);
            await Log("Clicked ON");
        }

        private async void btnOff_Click(object sender, RoutedEventArgs e)
        {
            arduino.digitalWrite(relayPin, PinState.LOW);
            await Log("Clicked Off");
        }

        private async Task ToggleUI(bool isEnabled)
        {
            await UiDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                btnOff.IsEnabled = isEnabled;
                btnOn.IsEnabled = isEnabled;
            });
        }

        private async Task Log(string text)
        {
            await UiDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtLog.Text += text + Environment.NewLine;
                scroller.UpdateLayout();
                scroller.ScrollToVerticalOffset(scroller.ScrollableHeight);
            });
        }
    }
}
