using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using RaspberryPi.API.Infrastructure.Interfaces;
using RaspberryPi.API.Model;
using System;

namespace RaspberryPi.API.Controllers
{
    [RestController(InstanceCreationType.Singleton)]
    public sealed class SensorController
    {
        private II2CBus _bus { get; set; }

        public SensorController(II2CBus bus)
        {
            _bus = bus;
            _bus.InitializeI2CBus(0x40);
        }

        [UriFormat("/sensor/{id}")]
        public IGetResponse GetSensor(int id)
        {
            return new GetResponse(
                GetResponse.ResponseStatus.OK,
                new TempAndHumidity
                {
                    Type = "DHT11",
                    Humidity = 84.23,
                    TemperatureInCelcius = 22.39
                });
        }
    }
}
