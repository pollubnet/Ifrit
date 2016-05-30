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
            // AnalogInput przetwarza różnicę poziomów pomiędzy 0 a 3,3V na 1024 poziomy odczytu, lub liczbę niecałkowitą z zakresu 0;1
            AnalogInput lux = new AnalogInput(AnalogChannels.ANALOG_PIN_A0);

            // SerialPort połączony jest na COM2 (piny 2 i 3) do modułu Bluetooth na parametrach 38400, 8, N, 1
            SerialPort sp = new SerialPort(SerialPorts.COM2, 38400, Parity.None, 8, StopBits.One);
            sp.Open();

            // na wieczność
            while (true)
            {
                // wykonaj odczyt, przetwórz na string, dodaj znak końca linii
                var data = lux.Read().ToString() + "\r\n";
                Debug.Print(data);

                // wyślij przez Bluetooth
                var buf = Encoding.UTF8.GetBytes(data);
                sp.Write(buf, 0, buf.Length);

                // czekaj 200 milisekund do następnego pomiaru
                Thread.Sleep(200);
            }

            sp.Close();

        }

    }
}
