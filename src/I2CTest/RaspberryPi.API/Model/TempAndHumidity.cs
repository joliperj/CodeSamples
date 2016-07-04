using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryPi.API.Model
{
    public class TempAndHumidity: ISensor
    {
        public string Type { get; set; }
        public double TemperatureInCelcius { get; set; }
        public double Humidity { get; set; }
    }
}
