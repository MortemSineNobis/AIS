using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace AIStoFile
{
    static class FileWriter
    {

        static string path = Properties.Settings.Default.path + "\\";
        static string name = Properties.Settings.Default.name;
        static string ex = Properties.Settings.Default.extension;
        static System.Timers.Timer timer = new System.Timers.Timer(1000);
        public static Queue<string> ToWrite = new Queue<string>();
        public static bool usingLine = Properties.Settings.Default.usingLines;
        
        public static void Write()
        {
            CheckFile();

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path + name, true))
                {
                    string temp;
                    if (usingLine)
                        streamWriter.WriteLine(temp = ToWrite.Peek());
                    else
                        streamWriter.Write(temp = ToWrite.Peek());
                    ToWrite.Dequeue();
                }
            }
            catch
            {

            }
        }

        private static void CheckFile()
        {
            CheckName();
            if (File.Exists(path + name))
                if (new FileInfo(path + name).Length >= 4194304)
                {
                    int i = 0;
                    string tempname =name.Replace(ex, "") + " - " + DateTime.Now.ToString("d");
                    while (File.Exists(path + tempname + (i == 0 ? "" : $" ({i})") + ex))
                        i++;
                    tempname += (i == 0 ? "" : $" ({i})");
                    File.Move(path + name, path + tempname + ex);
                    name = Properties.Settings.Default.name = string.Empty;
                    Properties.Settings.Default.Save();
                    CheckName();
                }
        }

        private static void CheckName()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.name))
            {
                int i = 0;
                string tempname = DateTime.Now.ToString("d");
                while (File.Exists(path + tempname + (i == 0 ? "" : $" ({i})")+ex))
                    i++;
                tempname += (i == 0 ? "" : $" ({i})") + ex;
                name = Properties.Settings.Default.name = tempname;
                Properties.Settings.Default.Save();
                MessageBox.Show($"ВАХ, кажется я создал новый файл :/. И что мне теперь с ним делать?! Опять работать????\nИмя файла: {path + name}");
            }
        }

        public static void CloseWrite()
        {
            timer.Close();
            CheckFile();
            if (ToWrite.Count>0)
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path + name, true))
                {
                    while (ToWrite.Count > 0)
                        if (usingLine)
                            streamWriter.WriteLine(ToWrite.Dequeue());
                        else
                            streamWriter.Write(ToWrite.Dequeue());
                }
            }
            catch
            {
                using (StreamWriter streamWriter = new StreamWriter(path + "Queue.tmp", true))
                {
                    while(ToWrite.Count>0)
                    {
                        if (usingLine)
                            streamWriter.WriteLine(ToWrite.Dequeue());
                        else
                            streamWriter.Write(ToWrite.Dequeue());
                    }
                    
                }
            }
        }
        
        public static void OpenRead()
        {
            List<string> list;
            if (File.Exists(path + "Queue.tmp"))
            {
                list = new List<string>(File.ReadAllLines(path + "Queue.tmp"));
                list.ForEach(l => ToWrite.Enqueue(l));
                File.Delete(path + "Queue.tmp");
            }
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (ToWrite.Count>0)
                for (int i = 0; i < ToWrite.Count; i++)
                {
                    Write();
                }
            
        }
    }
}
