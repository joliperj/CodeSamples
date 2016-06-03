using Microsoft.Maker.RemoteWiring;
using System;

namespace Arduino.Sensors.SoilMoisture
{
    public class SoilMoistureHygrometer : SensorBase
    {
        public SoilMoistureHygrometer(RemoteDevice device, string analogPin): base(device, analogPin)
        {
        }

        protected override decimal ProcessValue(ushort value)
        {
            // 1023 is maximum resistance => should represent humidity 0%
            value = (ushort)(1023 - value);
            var percentage = Math.Round((decimal)value / 1023 * 100, 2);
            return percentage;
        }
    }
}
