using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using RemoteControl.RaspBerryPi.Models;
using System.Collections.Generic;
using Windows.Devices.Gpio;

namespace RemoteControl.RaspBerryPi.Controllers
{
    [RestController(InstanceCreationType.Singleton)]
    public sealed class GpioController
    {
        private readonly Windows.Devices.Gpio.GpioController _gpio;
        private readonly Dictionary<byte, GpioPin> _pins;

        public GpioController()
        {
            _gpio = Windows.Devices.Gpio.GpioController.GetDefault();
            _pins = new Dictionary<byte, GpioPin>();

            byte[] pins = { 4, 17, 27, 22, 5, 6, 13, 19, 26, 21, 20, 16, 12, 25, 24, 23, 18 };
            for(byte i = 0;i<pins.Length;i++)
            {
                _pins.Add(pins[i], _gpio.OpenPin(pins[i], GpioSharingMode.Exclusive));
            }
        }

        [UriFormat("/gpio")]
        public IGetResponse GetAllGpioPins()
        {
            return new GetResponse(
                GetResponse.ResponseStatus.OK,
                _pins.Keys);
        }

        [UriFormat("/gpio/{id}/value")]
        public IGetResponse GetGpioValue(byte id)
        {
            if (!_pins.ContainsKey(id))
                return new GetResponse(GetResponse.ResponseStatus.NotFound);

            return new GetResponse(
                GetResponse.ResponseStatus.OK,
                new GpioPinResponse { IsStateHigh = _pins[id].Read() == GpioPinValue.High }
            );
        }

        [UriFormat("/gpio/{id}/value/{isHigh}")]
        public IPutResponse SetGpioValue(byte id, bool isHigh)
        {
            if (!_pins.ContainsKey(id))
                return new PutResponse(PutResponse.ResponseStatus.NotFound);

            _pins[id].SetDriveMode(GpioPinDriveMode.Output);
            _pins[id].Write(isHigh ? GpioPinValue.High : GpioPinValue.Low);
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }
    }
}
