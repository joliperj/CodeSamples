using System;
using System.Diagnostics;
using System.Threading;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.UI.Xaml.Controls;

namespace RaspberryPi
{
    public sealed partial class MainPage : Page
    {
        private I2cDevice _device;
        private Timer _periodicTimer;

        public MainPage()
        {
            this.InitializeComponent();

            InitI2C();
        }

        private async void InitI2C()
        {
            var settings = new I2cConnectionSettings(0x40); // Arduino address
            settings.BusSpeed = I2cBusSpeed.StandardMode;
            string aqs = I2cDevice.GetDeviceSelector("I2C1");
            var dis = await DeviceInformation.FindAllAsync(aqs);
            _device = await I2cDevice.FromIdAsync(dis[0].Id, settings);
            _periodicTimer = new Timer(this.TimerCallback, null, 0, 100); // Create a timer
        }

        private void TimerCallback(object state)
        {
            byte[] RegAddrBuf = new byte[] { 0x40 };
            byte[] ReadBuf = new byte[6];
            try
            {
                _device.Read(ReadBuf); // read the data
            }
            catch (Exception f)
            {
                Debug.WriteLine(f.Message);
            }
            char[] cArray = System.Text.Encoding.UTF8.GetString(ReadBuf, 0, 6).ToCharArray();  // Converte  Byte to Char
            String c = new String(cArray);
            Debug.WriteLine(c);
            // refresh the screen, note Im using a textbock @ UI
            var task = this.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    txtTemperature.Text = c;
                });
        }
    }
}
