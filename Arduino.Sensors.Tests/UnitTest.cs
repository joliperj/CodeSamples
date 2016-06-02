using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Arduino.Sensors.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var arduino = new Connect.AutoDiscover().ConnectDevice().Result;
            SoilMoisture.SoilMoistureHygrometer sensor = new SoilMoisture.SoilMoistureHygrometer(arduino, "A0");

            var value = sensor.Value;
        }
    }
}
