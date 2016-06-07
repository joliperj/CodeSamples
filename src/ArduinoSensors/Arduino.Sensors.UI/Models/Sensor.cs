using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;

namespace Arduino.Sensors.UI.Models
{
    public class Sensor: INotifyPropertyChanged
    {
        private string _value;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        public string Value {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value");  }
        }
        public string Unit { get; set; }

        public Sensor(string name, string unit)
        {
            Name = name;
            Unit = unit;
            Value = "...";
        }

        public Sensor(string name, string unit, string value)
        {
            Name = name;
            Unit = unit;
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        protected async virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }
    }
}
