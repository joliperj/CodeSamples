using Microsoft.Maker.RemoteWiring;

namespace Arduino.Sensors
{
    public abstract class SensorBase
    {
        protected string AnalogPin { get; private set; }
        protected RemoteDevice Device { get; private set; }
        public decimal Value { get { return Device.analogRead(AnalogPin); } }

        public event ValueChanged OnValueChanged;
        public delegate void ValueChanged(decimal value);

        public SensorBase(RemoteDevice device, string analogPin)
        {
            Device = device;
            AnalogPin = analogPin;

            Device.pinMode(AnalogPin, PinMode.INPUT);
            Device.AnalogPinUpdated += Device_AnalogPinUpdated;
            Device.analogRead(AnalogPin);
        }

        private void Device_AnalogPinUpdated(string pin, ushort value)
        {
            var processedValue = ProcessValue(value);
            OnValueChanged?.Invoke(processedValue);
        }

        protected virtual decimal ProcessValue(ushort value)
        {
            return value;
        }
    }
}
