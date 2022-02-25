using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIStoFile
{
    public partial class Form1 : Form
    {
        List<string> ports = new List<string>();
        List<int> bitrates = new List<int>{ 300, 600, 1200, 1800, 2400, 4800, 7200, 9600, 14400, 19200, 38400, 57600, 115200, 230400, 460800, 921600 };
        
        public Form1()
        {
            InitializeComponent();
            FileWriter.OpenRead();
            foreach (string s in SerialPort.GetPortNames())
            {
                ports.Add(s);
            }
            ports.ForEach(p => comboBox1.Items.Add(p));
            if (string.IsNullOrEmpty(Properties.Settings.Default.COM))
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(Properties.Settings.Default.COM);
            bitrates.ForEach(b => comboBox2.Items.Add(b));
            if (Properties.Settings.Default.bitrate == -1)
                comboBox2.SelectedIndex = 0;
            else
                comboBox2.SelectedIndex = Properties.Settings.Default.bitrate;
            checkBox1.Checked = Properties.Settings.Default.usingLines;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.path = folderBrowserDialog1.SelectedPath;
                //MessageBox.Show(folderBrowserDialog1.SelectedPath);
                Properties.Settings.Default.Save();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.COM = ports[comboBox1.SelectedIndex];
            Properties.Settings.Default.bitrate = comboBox2.SelectedIndex;
            Properties.Settings.Default.usingLines = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SerialPort _serialPort = new SerialPort(comboBox1.SelectedItem.ToString(), bitrates[comboBox2.SelectedIndex]);
            try
            {
                _serialPort.Open();
                File.Create(Properties.Settings.Default.path + "\\test").Close();
                if (_serialPort.IsOpen && File.Exists(Properties.Settings.Default.path + "\\test"))
                {            
                    _serialPort.Close();
                    Thread.Sleep(1000);
                    File.Delete(Properties.Settings.Default.path + "\\test");
                    MessageBox.Show("Все отлично, можно хавать)");
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Settings.Default.path + "\\test");
                MessageBox.Show("Беда, чот не так, опять работать((");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWorker.Start(comboBox1.SelectedItem.ToString(), bitrates[comboBox2.SelectedIndex],this);
            StatusLabel.Text = "Подключено";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamWorker.Stop();
            StatusLabel.Text = "Отключено";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWorker.Stop();
            FileWriter.CloseWrite();
        }
        public void updatelist(string text)
        {
            listView1.Items.Add(new ListViewItem(text));
            while (listView1.Items.Count > 10)
                listView1.Items.RemoveAt(0);

        }
    }
}
