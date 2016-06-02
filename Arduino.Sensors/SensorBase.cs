using Microsoft.Maker.RemoteWiring;

namespace Arduino.Sensors
{
    public abstract class SensorBase
    {
        protected string AnalogPin { get; private set; }
        protected RemoteDevice Device { get; private set; }
        public decimal Value { get { return ReadValue(); } }

        public SensorBase(RemoteDevice device, string analogPin)
        {
            Device = device;
            AnalogPin = analogPin;
            Device.pinMode(analogPin, PinMode.OUTPUT);
        }

        protected virtual decimal ReadValue()
        {
            return Device.analogRead(AnalogPin);
        }
    }
}
