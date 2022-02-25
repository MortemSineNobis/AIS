using System;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using CAIS;

namespace AISPI
{
    class Program
    {
        static SerialPort serialPort;
        static UdpClient client = new UdpClient();
        static System.Timers.Timer udptimer = new System.Timers.Timer(1000);
        static bool needToCheck = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            udptimer.Elapsed += Udptimer_Elapsed;
            Console.WriteLine("Starting...");
            serialPort = new SerialPort(args[0]);
            serialPort.BaudRate = 115200;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.None;
            serialPort.RtsEnable = true;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            //Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            //receiveThread.Start();
            udptimer.Start();
            Console.WriteLine($"Listening {args[0]}");
            CAIS.Decoder.InitPgSQL("127.0.0.1", 5433, "AIS1", "daemonais666", "AIS", 1);
            CAIS.Decoder.UsingStatistics = true;
            Console.WriteLine("Postgres provider opened");
            serialPort.Open();
            serialPort.DtrEnable = true;
            Console.WriteLine("Serial port opened");
            while (true)
            {
                var c = Console.ReadLine();
                if (c == "quit") break;
            }
            udptimer.Stop();
            serialPort.Close();
            client.Close();
        }

        private static void Udptimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                SendUDP($"{DateTime.Now}|{CAIS.Decoder.MessagesRecivedCount}|{CAIS.Decoder.SendedMessagesCount}|{CAIS.Decoder.ErrorsMessagesCount}");
                if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0 && needToCheck)
                {
                    needToCheck = false;
                    CAIS.Decoder.MessagesRecivedCount = 0;
                    CAIS.Decoder.SendedMessagesCount = 0;
                    CAIS.Decoder.ErrorsMessagesCount = 0;
                }
                else
                    if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 1 && !needToCheck)
                    needToCheck = true;
            }
            catch (Exception)
            {
            }
        }

        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sr = sender as SerialPort;
            string indata = sr.ReadLine();
            Console.Write(indata);
            CAIS.Decoder.GetDecodedMessage(indata);
            // SendUDP(indata);    
        }



        private static void SendUDP(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, "127.0.0.1", 5604);

            ////Console.ForegroundColor = ConsoleColor.Yellow;
            ////Console.WriteLine("sended | ");
            ////Console.ResetColor();
        }

        //private static void ReceiveMessage()
        //{
        //    UdpClient receiver = new UdpClient(5605); // UdpClient для получения данных
        //    IPEndPoint remoteIp = null; // адрес входящего подключения
        //    try
        //    {
        //        while (true)
        //        {
        //            byte[] data = receiver.Receive(ref remoteIp); // получаем данные
        //            string message = Encoding.UTF8.GetString(data);
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            Console.WriteLine("Собеседник: {0}", message);
        //            Console.ResetColor();
        //            Console.Beep();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        receiver.Close();
        //    }
        //}
    }
}
