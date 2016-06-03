using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace Connect.Arduino
{
    public static class AutoDiscover
    {
        private static UsbSerial connection = null;
        private static RemoteDevice arduino = null;

        public static async Task<RemoteDevice> ConnectDevice()
        {
            await Task.Delay(1000);

            if(connection == null)
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

                connection = new UsbSerial(info);
                connection.begin(57600, SerialConfig.SERIAL_8N1);
            }
            
            if(arduino == null)
            {
                arduino = new RemoteDevice(connection);
            }

            return arduino;
        }
    }
}
