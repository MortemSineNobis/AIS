using System;
using CAIS;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO.Ports;
using CommandLine;

namespace test3
{
    class Program
    {

        //const int BufferSize = 8192;
        //static SerialPort _serialport = new SerialPort();
        //[STAThread]
        static void Main(string[] args)
        {
            string f = "file",
                u = "user",
                h = "host",
                d = "database";
            int p = 1234;
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(
                o =>
                {
                    f = o.FileName;
                    u = o.UserName;
                    h = o.Host;
                    d = o.DataBase;
                    p = o.Port;
                    Console.WriteLine(o.LoadAllLines);
                });
            Console.WriteLine(f);
            Console.WriteLine(u);
            Console.WriteLine(h);
            Console.WriteLine(d);
            Console.WriteLine(p);
            //    _serialport.BaudRate = 115200;
            //    _serialport.DataBits = 8;
            //    _serialport.Parity = Parity.None;
            //    _serialport.StopBits = StopBits.One;
            //    _serialport.Handshake = Handshake.None;
            //    _serialport.PortName = "/dev/ttyACM0";
            //    _serialport.Encoding = Encoding.ASCII;
            //    _serialport.DataReceived += _serialport_DataReceived;
            //    //_serialport.NewLine = 0x0A;
            //    Console.ReadLine();
            //    _serialport.Open();
            //    _serialport.DtrEnable = true;
            //    //while (true)
            //    //{
            //    //    if (_serialport.BytesToRead > 0)
            //    //    {

            //    //        //byte[] buf = new byte[_serialport.BytesToRead];
            //    //        //_serialport.Read(buf, 0, buf.Length);
            //    //        //Console.WriteLine(Encoding.ASCII.GetString(buf));
            //    //    }
            //    //}
            //    Console.ReadLine();
            //    _serialport.Close();
        }

        //private static void _serialport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    Console.WriteLine(_serialport.ReadLine());
        //}
    }
}