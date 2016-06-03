using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Connect.Arduino;
using Microsoft.Maker.RemoteWiring;

namespace Arduino.Sensors.Tests
{
    [TestClass]
    public class UnitTest1
    {
        RemoteDevice arduino;

        [TestMethod]
        public void TestMethod1()
        {
            arduino = AutoDiscover.ConnectDevice().Result;
            arduino.DeviceReady += OnDeviceReady;
        }

        private void OnDeviceReady()
        {
            SoilMoisture.SoilMoistureHygrometer sensor = new SoilMoisture.SoilMoistureHygrometer(arduino, "A0");
            while (true)
            {
                var value = sensor.Value;
            }
        }
    }
}
