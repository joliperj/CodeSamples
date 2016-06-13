using RaspberryPi.Model;
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
            _periodicTimer = new Timer(TimerCallback, null, 0, 1000); // Create a timer
        }

        private void TimerCallback(object state)
        {
            try
            {
                byte[] ReadBuf = new byte[14];
                _device.Read(ReadBuf);

                var humid = (int)ReadBuf[0];
                var temp = (int)ReadBuf[1];
                var soil = (float)ReadBuf[2];

                if (temp < 30 && humid < 50 && soil < 50)
                {
                    _device.Write(new byte[] { I2cConstants.SET_PINSTATE, 3, I2cConstants.PINSTATE_HIGH });
                }
                else
                {
                    _device.Write(new byte[] { I2cConstants.SET_PINSTATE, 3, I2cConstants.PINSTATE_LOW });
                }

                var task = Dispatcher.RunAsync(
                    Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                        txtTemperature.Text = temp.ToString();
                        txtHumidity.Text = humid.ToString();
                        txtSoil.Text = soil.ToString();
                    });

            }
            catch (Exception f)
            {
                Debug.WriteLine(f.Message);
            }

        }
    }
}
