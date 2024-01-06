using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Gitusme.Net.Extensiones.Core;
using Com.Gitusme.Net.Extensiones.Wpf.IO;

namespace Com.Gitusme.Net.Extensiones.Demos
{
    internal class SerialPortDemo : IExtensionesDemo
    {
        public override void Execute()
        {
            base.Execute();

            SerialPort port1 = new SerialPort();
            port1.Settings = new SerialPortSettings
            {
                BaudRate = 115200,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = 1,
                Synchronizable = false
            };
            port1.Open("COM1");
            System.Console.WriteLine("open COM1");

            SerialPort port2 = new SerialPort();
            port2.Settings = new SerialPortSettings
            {
                BaudRate = 115200,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = 1,
                Synchronizable = false
            };
            port2.Open("COM2");
            System.Console.WriteLine("open COM2");

            string send = "Hello, gitusme!";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(send);
            port1.Write(data);
            System.Console.WriteLine("send data to COM1: " + send);

            byte[] rec = new byte[data.Length + 10000];
            int read = port2.Read(rec);
            string receive = System.Text.Encoding.UTF8.GetString(rec, 0, read);
            System.Console.WriteLine("receive data from COM2: " + receive);

            port1.Close();
            System.Console.WriteLine("close COM1");
            port2.Close();
            System.Console.WriteLine("close COM2");

            bool isEquals = send.Equals(receive);
            System.Console.WriteLine("send=receive: " + isEquals);
        }
    }
}
