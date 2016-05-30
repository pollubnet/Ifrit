using Microsoft.Azure.Devices.Client.Samples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace Ifrit.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // wykonaj odczyt z sensora światła
            var ls = Windows.Devices.Sensors.LightSensor.GetDefault();

            // otwórz połączenie i wykonaj 10 razy odczyt i wysłanie danych, co sekundę
            await IoTClient.Start();
            for (int i = 0; i < 10; i++)
            {
                // pobierz odczyt, wyślij go do Azure przez przykładową klasę IoTClient
                reading.Text = ls.GetCurrentReading().IlluminanceInLux.ToString();
                await IoTClient.SendEvent(ls.GetCurrentReading().IlluminanceInLux.ToString());
                await Task.Delay(1000);
            }

            // ten kod powyżej jest znacząco skopany - nie aktualizuje on interfejsu użytkownika
            // odpowiednio, więc wydaje się, że aplikacja się zawiesza
        }
    }
}
