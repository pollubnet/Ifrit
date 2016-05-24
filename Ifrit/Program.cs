using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using System.IO.Ports;
using System.Text;

namespace Ifrit
{
    public class Program
    {
        public static void Main()
        {
            AnalogInput lux = new AnalogInput(AnalogChannels.ANALOG_PIN_A0);
            SerialPort sp = new SerialPort(SerialPorts.COM2, 38400, Parity.None, 8, StopBits.One);
            sp.Open();

            while (true)
            {
                var data = lux.Read().ToString() + "\r\n";
                Debug.Print(data);

                var buf = Encoding.UTF8.GetBytes(data);

                sp.Write(buf, 0, buf.Length);

                Thread.Sleep(200);
            }

            sp.Close();

        }

    }
}
