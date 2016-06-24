using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RemoteControl.ClientApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.1.7:8800");
                var response = client.GetAsync("/api/gpio").Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var pins = JsonConvert.DeserializeObject<byte[]>(content);
                Array.Sort(pins);
                cmbPin.ItemsSource = pins;
            }
            
        }

        private async void btnOn_Click(object sender, RoutedEventArgs e)
        {
            await SendCommand(true);
        }

        private async void btnOff_Click(object sender, RoutedEventArgs e)
        {
            await SendCommand(false);
        }

        private async Task SendCommand(bool isOn)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.1.7:8800");
                string uri = $"/api/gpio/{cmbPin.SelectedValue}/value/{isOn}".ToLower();
                await client.PutAsync(uri, new StringContent(""));
            }
        }
    }
}
