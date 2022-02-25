using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Renci.SshNet;

namespace TEST
{
    public partial class Form1 : Form
    {
        Process SSH;
        string UserName = "root";
        string Server = "89.223.84.21";
        string PathToIdentify = @"C:\Users\bes-s\Desktop\AIS\SSH\private.ppk";
        string PasswordString = "alfabetatestforhanibal369";
        int LocalPort = 5433;
        int RemotePort = 5432;
        bool good = true;
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //PrivateKeyFile file = new PrivateKeyFile(PathToIdentify);
            //using (var client = new SshClient(Server, UserName, file))
            //{

            //    client.Connect();
            //    var port = new ForwardedPortLocal("localhost", 5433, "localhost", 5432);
            //    client.AddForwardedPort(port);
            //    port.Start();

            //    while(true)
            //    { }

            //    client.Disconnect();
            //}
            //var ais = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AIS");
            //if (ais == null)
            //    ais = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIS");
            //UserName = ais.GetValue("UserName", string.Empty).ToString();
            //Server = ais.GetValue("Server", string.Empty).ToString();
            //PathToIdentify = ais.GetValue("Path", string.Empty).ToString();
            //PasswordString = ais.GetValue("Passwd", string.Empty).ToString();
            //LocalPort = Convert.ToInt32(ais.GetValue("Locat", 5432));
            //RemotePort = Convert.ToInt32(ais.GetValue("Remote", 5432));

            SSH = Process.Start(new ProcessStartInfo
            {
                FileName = @"cmd",
                    Arguments = $"/c netstat /na | /find /\"{LocalPort}\" & pause",
                CreateNoWindow = false,
                //WindowStyle = ProcessWindowStyle.Hidden
                //UseShellExecute = false,
                //RedirectStandardOutput = true
            });
            MessageBox.Show("");
            //SSH.BeginOutputReadLine();
            //SSH.OutputDataReceived += (s, a) => { label1.BeginInvoke((Action)(() => label1.Text += a.Data + Environment.NewLine)); };
            //label1.Text += SSH.StandardOutput.ReadToEnd();
            Console.WriteLine();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            good = !good;
            button2.Text = good.ToString();
        }
    }
}
