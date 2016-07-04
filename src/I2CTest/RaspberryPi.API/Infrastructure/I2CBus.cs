using RaspberryPi.API.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace RaspberryPi.API.Infrastructure
{
    internal class I2CBus: II2CBus
    {
        private Dictionary<int, I2cDevice> _devices;

        public async Task InitializeI2CBus(int deviceAddress)
        {
            var settings = new I2cConnectionSettings(deviceAddress);
            settings.BusSpeed = I2cBusSpeed.StandardMode;
            string aqs = I2cDevice.GetDeviceSelector("I2C1");
            var dis = await DeviceInformation.FindAllAsync(aqs);
            var device = await I2cDevice.FromIdAsync(dis[0].Id, settings);

            if (!_devices.ContainsKey(deviceAddress))
            {
                _devices.Add(deviceAddress, device);
            }
            else
            {
                _devices[deviceAddress] = device;
            }
        }
    }
}
