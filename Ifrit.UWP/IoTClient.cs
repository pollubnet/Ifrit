// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Microsoft.Azure.Devices.Client.Samples
{
    class IoTClient
    {
        private static int MESSAGE_COUNT = 5;

        // String containing Hostname, Device Id & Device Key in one of the following formats:
        //  "HostName=<iothub_host_name>;DeviceId=<device_id>;SharedAccessKey=<device_key>"
        //  "HostName=<iothub_host_name>;CredentialType=SharedAccessSignature;DeviceId=<device_id>;SharedAccessSignature=SharedAccessSignature sr=<iot_host>/devices/<device_id>&sig=<token>&se=<expiry_time>";
        private const string DeviceConnectionString = "HostName=MqttTest.azure-devices.net;DeviceId=uwp;SharedAccessSignature=SharedAccessSignature sr=MqttTest.azure-devices.net%2fdevices%2fuwp&sig=u2NbTACjx3JBChQycSbtDQYejewKJb9%2feGP6gNNQqk0%3d&se=1464284421";

        private static DeviceClient deviceClient;

        public async static Task Start()
        {
            try
            {
                deviceClient = DeviceClient.CreateFromConnectionString(DeviceConnectionString, TransportType.Http1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in sample: {0}", ex.Message);
            }
        }

        public static async Task SendEvent(string data)
        {
            string dataBuffer;

            dataBuffer = JsonConvert.SerializeObject(new { Data = data, Device = "uwp" });
            Message eventMessage = new Message(Encoding.UTF8.GetBytes(dataBuffer));

            await deviceClient.SendEventAsync(eventMessage);

        }
    }
}