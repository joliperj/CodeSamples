using Microsoft.Maker.RemoteWiring;

namespace Arduino.Sensors.SoilMoisture
{
    public class SoilMoistureHygrometer : SensorBase
    {
        public SoilMoistureHygrometer(RemoteDevice device, string analogPin): base(device, analogPin)
        {
        }

        protected override decimal ReadValue()
        {
            return base.ReadValue();
        }
    }
}
