using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;
using CAIS;

namespace FileToAIS
{
    class Program
    {
        static bool DO = true;
        static string path = string.Empty;
        static System.Timers.Timer timer = new System.Timers.Timer();
        static int count = 0;
        static string host, user, database, password;
        static int port, aisId;
        static bool loadAllLines = false;
        //static string path = @"G:\7.txt";
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                if (!File.Exists(o.FileName))
                {
                    Console.WriteLine($"ERROR: File '{o.FileName}' not exist or can't be read. Please check the file. Sorry ;(");
                    Console.ReadLine();
                    return;
                }
                else
                    path = o.FileName;
                user = o.UserName;
                host = o.Host;
                database = o.DataBase;
                port = o.Port;
                aisId = o.AisId;
                loadAllLines = o.LoadAllLines;
            });
            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(database) || port == null || aisId == null) return;
            Console.Write("Password: ");
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;
                if (!char.IsControl(keyInfo.KeyChar))
                    password += keyInfo.KeyChar;
            } while (key != ConsoleKey.Enter);
            Console.Clear();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 60000;
            if (loadAllLines)
            {
                var li = File.ReadAllLines(path);
                DODO(li);
            }
            else
            {
                DODO(path);
            }
            while (true)
            {
                var res = Console.ReadLine();
                if (res == "q") { DO = false; break; }

            }
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (timer)
            {
                Interlocked.Exchange(ref ProgressBar.operationsPerMinute, count);
                count = 0;
            }
        }

        private static async void DODO(string[] li)
        {
            using (var progress = new ProgressBar())
            {
                progress.Report(loadAllLines);
                CAIS.Decoder.InitPgSQL(host, port, user, password, database, aisId);
                progress.Report((double)0 / li.Length);
                Thread.Sleep(10);
                timer.Start();
                for (int i = 0; i < li.Length; i++)
                {
                    progress.Report((double)i / li.Length, i);
                    var item = li[i];
                    if (!DO) break;
                    CAIS.Decoder.GetDecodedMessage(item);
                    count++;
                }
            }
            timer.Stop();
            Console.Clear();
            Console.WriteLine($"Done! Total lines: {li.Length}");
            Console.Beep();
        }

        private static async void DODO(string filename)
        {
            using (var progress = new ProgressBar())
            {

                CAIS.Decoder.InitPgSQL(host, port, user, password, database, aisId);
                Thread.Sleep(10);
                timer.Start();
                long lines = 0;
                using (StreamReader reader = new StreamReader(filename))
                {
                    string item = string.Empty;
                    while ((item = reader.ReadLine()) != null)
                    {
                        if (!DO) break;
                        CAIS.Decoder.GetDecodedMessage(item);
                        count++;
                        progress.Report(0.0, ++lines);
                    }
                }
                Console.Clear();
                Console.WriteLine($"Done! Total lines: {lines}");
            }
            timer.Stop();
            Console.Beep();
        }
    }
}
