using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace Arduino.Connect
{
    public class AutoDiscover
    {
        private ManualResetEvent _deviceReady;

        public AutoDiscover()
        {
            _deviceReady = new ManualResetEvent(false);
        }

        public async Task<RemoteDevice> ConnectDevice()
        {
            var usbDevices = await UsbSerial.listAvailableDevicesAsync();
            DeviceInformation info = null;

            for (int i = 0; i < usbDevices.Count; i++)
            {
                var props = usbDevices[i].Properties.Values.ToList();
                for (int y = 0; y < props.Count(); y++)
                {
                    if (props[y] == null)
                        continue;

                    if (props[y].ToString().Contains("USB-SERIAL"))
                    {
                        info = usbDevices[i];
                        break;
                    }
                }

                if (info != null)
                    break;
            }

            if (info == null)
                return null;

            var connection = new UsbSerial(info);
            connection.begin(57600, SerialConfig.SERIAL_8N1);

            var arduino = new RemoteDevice(connection);
            arduino.DeviceReady += Arduino_DeviceReady;

            _deviceReady.WaitOne(2000);
            return arduino;
        }

        private void Arduino_DeviceReady()
        {
            _deviceReady.Set();
        }
    }
}
