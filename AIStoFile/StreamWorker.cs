using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIStoFile
{
    static class StreamWorker
    {
        static SerialPort serialPort;
        public static bool _continue;
        static Thread readThread;
        public static Form1 form;
        public static void  Start(string name, int bitr,Form1 f)
        {
            form = f;
            _continue = true;
            readThread = new Thread(Read);
            serialPort = new SerialPort(name, bitr);
            serialPort.ReadTimeout = 100; //10 минут
            if (!serialPort.IsOpen)
                serialPort.Open();
            readThread.Start();
        }
        public static void Stop() 
        {
            _continue = false;
            Task.Delay(100);
            if (readThread != null)
                if (readThread.IsAlive)
                    readThread.Join();
            if (serialPort!=null)
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            serialPort = null;
        }

        private static void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = serialPort.ReadLine();
                    form.BeginInvoke((MethodInvoker)(() =>
                    {
                        form.updatelist(message);
                    }));
                    FileWriter.ToWrite.Enqueue(message);
                    FileWriter.Write();
                }
                catch (TimeoutException) { }
            }
        }

    }
}
