using Arduino.Sensors.SoilMoisture;
using Arduino.Sensors.UI.Models;
using Connect.Arduino;
using Microsoft.Maker.RemoteWiring;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Arduino.Sensors.UI
{
    public sealed partial class MainPage : Page
    {
        private RemoteDevice _arduino;
        private Sensor MoistureSensor;
        private const int PUMP_PIN = 2;

        public MainPage()
        {
            this.InitializeComponent();

            MoistureSensor = new Sensor("Soil moisture level", "%");

            lblSensorValue.DataContext = MoistureSensor;
            lblSensor.DataContext = MoistureSensor;

            Connect();
        }

        private async void Connect()
        {
            _arduino = await AutoDiscover.ConnectDevice();
            _arduino.DeviceReady += Arduino_DeviceReady;
        }

        private void Arduino_DeviceReady()
        {
            var sensor = new SoilMoistureHygrometer(_arduino, "A0");
            sensor.OnValueChanged += SensorValueChanged;
        }

        private void SensorValueChanged(decimal value)
        {
            MoistureSensor.Value = $"{value}{MoistureSensor.Unit}";
            if (value < 10) // soil humidity percentage less than 10%
            {
                _arduino.digitalWrite(PUMP_PIN, PinState.HIGH);
            }
            else
            {
                _arduino.digitalWrite(PUMP_PIN, PinState.LOW);
            }
        }
    }
}
