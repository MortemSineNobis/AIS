using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CAIS
{
    public static class Decoder
    {
        private static Message LongMessage;
        private static string path = @"errors";
        private static PostgreSQLProvider provider;
        public static long SendedMessagesCount { get; set; }
        public static long ErrorsMessagesCount { get; set; }
        public static long MessagesRecivedCount { get; set; }
        public static bool UsingPostgree { get; set; } = false;
        public static bool UsingStatistics { get; set; } = false;
        public static Message GetDecodedMessage(string item)
        {
            if (UsingStatistics)
                MessagesRecivedCount++;
            Message res = null;
            try
            {
                var m = new Message(item);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Console.WriteLine($"Message Type = {m.MessageType} || {m.NMEA.ais_id}");
                //Console.ResetColor();
                if (m.NMEA.message_fragments > 1)
                {
                    if (LongMessage == null)
                        LongMessage = m;
                    else
                    {
                        if (m.NMEA.fragment_number == LongMessage.NMEA.message_fragments)
                        {
                            LongMessage.AddMessage(m);
                            res = GetDecodedMessage(LongMessage);
                            LongMessage = null;
                        }
                        else
                        {
                            LongMessage.AddMessage(m);
                        }
                    }
                }
                else
                {
                    if (LongMessage != null)
                        LongMessage = null;
                    res = GetDecodedMessage(m);
                }
            }
            catch (InvalidNMEAMessageException e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //LastLeft = Console.CursorLeft;
                //Console.SetCursorPosition(0, line += 2);
                //Console.WriteLine($"Message = {e.AISMessage}{Environment.NewLine}\t{e.Message}");
                //Console.SetCursorPosition(LastLeft, 0);
                //Console.ResetColor();
                using (StreamWriter sw = new StreamWriter(path,true))
                {
                    sw.WriteLine(($"{DateTime.Now.ToString()}{e.AISMessage}{Environment.NewLine}\t{e.Message}"));
                    if (LongMessage != null) 
                        sw.WriteLine(($"\tLongMessage = {LongMessage.AISMessage}\n"));
                    sw.WriteLine(($"\tNowMessage = {item}"));
                    if (UsingPostgree) 
                    {
                        if (LongMessage == null)
                            provider.PushError(item, e.Message);
                        else
                            provider.PushError(LongMessage.AISMessage, e.Message);
                    }

                }
                if (UsingStatistics)
                    ErrorsMessagesCount++;
                //Console.Beep();
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.SetCursorPosition(0, line+=2);
                //Console.WriteLine($"Message = {item}{Environment.NewLine}\t{e.Message}");
                //Console.SetCursorPosition(0, 0);
                //Console.ResetColor();
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(($"Message = {e.Message}"));
                    if (LongMessage != null)
                        sw.WriteLine(($"\tLongMessage = {LongMessage.AISMessage}\n"));
                    sw.WriteLine(($"\tNowMessage = {item}"));
                    if (UsingPostgree)
                    {
                        if (LongMessage == null)
                            provider.PushError(item, e.Message);
                        else
                            provider.PushError(LongMessage.AISMessage, e.Message);
                    }
                }
                if (UsingStatistics)
                    ErrorsMessagesCount++;
                //Console.Beep();
            }

            //if (res != null)
            //{
            //    Console.ForegroundColor = ConsoleColor.Yellow;
            //    Console.WriteLine($"DECODED = {res.AISMessage}");
            //    Console.ResetColor();

            //}
            if (UsingPostgree)
                try
                {
                    provider.PushMessage(res);
                    if (UsingStatistics)
                        SendedMessagesCount++;
                }
                catch (Exception e)
                {
                    if (LongMessage == null)
                        provider.PushError(item, e.Message);
                    else
                        provider.PushError(LongMessage.AISMessage, e.Message);
                    if (UsingStatistics)
                        ErrorsMessagesCount++;
                }
                
            return res;
        }

        private static Message GetDecodedMessage(Message item)
        {
            var res = item;
            res.Decoded = Decode.DecodeData(res.NMEA.bit_array, res.MessageType);
            return res;
        }

        public static void InitPgSQL(string host, int port, string username, string password, string database, int? ais = null)
        {
            UsingPostgree = true;
            try
            {
                provider = new PostgreSQLProvider(host, port, username, password, database, ais);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nPress Enter to exit.");
                Console.ReadLine();
                Environment.Exit(-1);
            }
        }

        
    }
}
