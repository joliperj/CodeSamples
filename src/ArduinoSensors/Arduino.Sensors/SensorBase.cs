using Microsoft.Maker.RemoteWiring;
using System.Threading;

namespace Arduino.Sensors
{
    public abstract class SensorBase
    {
        private RemoteDevice _device;
        private string _analogPin;
        private decimal _sensorValue;
        private Timer _timer;

        public decimal Value { get { return _sensorValue; } }

        public event ValueChanged OnValueChanged;
        public delegate void ValueChanged(decimal value);

        public SensorBase(RemoteDevice device, string analogPin, int interval = 2000)
        {
            _analogPin = analogPin;
            _device = device;

            _device.pinMode(_analogPin, PinMode.INPUT);
            _timer = new Timer(new TimerCallback(ReadValue), null, 0, interval);
        }

        private void ReadValue(object sender)
        {
            var newRead = _device.analogRead(_analogPin);
            if (_sensorValue != newRead)
            {
                var processedValue = ProcessValue(newRead);
                OnValueChanged?.Invoke(processedValue);
            }
            _sensorValue = newRead;
        }

        protected virtual decimal ProcessValue(ushort value)
        {
            return value;
        }
    }
}
