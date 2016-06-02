using Microsoft.Azure.Devices.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using Windows.Devices.I2c;
using Windows.Devices.Enumeration;

namespace ConnectToAzureIoTHub
{
    public sealed partial class MainPage : Page
    {
        private const string deviceConnectionString = "HostName=TheProgrammatorCodeSamples.azure-devices.net;DeviceId=RaspberryPi2;SharedAccessKey=TVYTCB6HxK9XI7HAGrpxrzdFY5BrUspexld8Db8cg1A=";
        private DeviceClient _raspberry;
        private I2cDevice _arduino;

        public MainPage()
        {
            this.InitializeComponent();

            InitArduino().Wait();
            InitRaspberry();

            ListenForIncomingMessages();
        }

        private async Task InitArduino()
        {
            var settings = new I2cConnectionSettings(0x40); // Arduino address
            settings.BusSpeed = I2cBusSpeed.StandardMode;
            string aqs = I2cDevice.GetDeviceSelector("I2C1");
            var dis = await DeviceInformation.FindAllAsync(aqs);
            _arduino = await I2cDevice.FromIdAsync(dis[0].Id, settings);
        }

        private void InitRaspberry()
        {
            _raspberry = DeviceClient.CreateFromConnectionString(deviceConnectionString, TransportType.Amqp);
        }

        private void InitGPIO()
        {
            //var gpio = GpioController.GetDefault();

            //// Show an error if there is no GPIO controller
            //if (gpio == null)
            //{
            //    pin = null;
            //    txtReceived.Text = "There is no GPIO controller on this device.";
            //    return;
            //}

            //pin = gpio.OpenPin(LED_PIN);
            //pinValue = GpioPinValue.High;
            //pin.Write(pinValue);
            //pin.SetDriveMode(GpioPinDriveMode.Output);

            //txtReceived.Text = "GPIO pin initialized correctly.";

            //pin.Write(GpioPinValue.Low);

        }

        private async Task ListenForIncomingMessages()
        {
            while (true)
            {
                var receivedMessage = await _raspberry.ReceiveAsync();

                if (receivedMessage != null)
                {
                    var messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    await _raspberry.CompleteAsync(receivedMessage);
                    if(messageData == "LED_ON")
                    {
                        //pin.Write(GpioPinValue.High);
                    }
                    if(messageData == "LED_OFF")
                    {
                        //pin.Write(GpioPinValue.Low);
                    }
                    txtReceived.Text += messageData + Environment.NewLine;
                }
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var message = new Message(Encoding.ASCII.GetBytes(txtSend.Text));
            await _raspberry.SendEventAsync(message);
            txtSend.Text = "";
        }
    }
}
