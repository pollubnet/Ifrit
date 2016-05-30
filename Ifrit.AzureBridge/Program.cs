using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifrit.AzureBridge
{
    class IoTData
    {
        public double Data { get; set; }
        public string Device { get; set; }

    }

    class Program
    {
        static SerialPort sp;
        static DeviceClient deviceClient;

        // ConnectionString dla IotHuba, zmień na swój
        private const string DeviceConnectionString = "HostName=MqttTest.azure-devices.net;DeviceId=simulator;SharedAccessSignature=SharedAccessSignature sr=MqttTest.azure-devices.net%2fdevices%2fsimulator&sig=Xnn36%2fX4sNNXtHhu%2bn0bDlF2MJsNJdPrF0dsUonutpU%3d&se=1464457147";

        static void Main(string[] args)
        {
            try
            {
                // połącz się z Azure IoT Hub

                deviceClient = DeviceClient.CreateFromConnectionString(DeviceConnectionString, TransportType.Mqtt);

                deviceClient.OpenAsync().Wait();

                // połącz się z Bluetoothem (zmień port na odpowiedni)
                sp = new SerialPort("COM7", 9600);
                sp.DataReceived += Sp_DataReceived;
                sp.Open();
                Console.WriteLine("Działam.");
                Console.ReadLine();
                sp.Close();
            }
            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error in sample: {0}", exception);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Error in sample: {0}", ex.Message);
            }

            Console.ReadLine();
        }

        private static void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // kiedy odebrano dane z Bluetootha, przetwórz je na liczbę, ale zignoruj fakt, że
            // separatorem dziesiętnym jest tam kropka
            var d = sp.ReadLine();
            double data = double.Parse(d, System.Globalization.CultureInfo.InvariantCulture);

            // i wyślij dalej do Azure
            SendEvent(data);
        }

        static async Task SendEvent(double data)
        {
            // stwórz wiadomość i ją wyślij, serializując obiekt do JSONa
            string dataBuffer = JsonConvert.SerializeObject(new IoTData { Data = data, Device = "netmf" });
            
            Message eventMessage = new Message(Encoding.UTF8.GetBytes(dataBuffer));
            Console.WriteLine("{0}", dataBuffer);

            await deviceClient.SendEventAsync(eventMessage);

        }
    }
}
