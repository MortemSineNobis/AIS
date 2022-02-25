using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.IO;
using Microsoft.Win32;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        string FileName = string.Empty;
        string ExeString = string.Empty;
        string Server = string.Empty;
        string PathToIdentify = string.Empty;
        int LocalPort = 0;
        int RemotePort = 0;
        public Settings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
                PathToIdentify = ofd.FileName;
            file.Text = Path.GetFileName(PathToIdentify);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetValues();
            if (string.IsNullOrEmpty(FileName) &&
                string.IsNullOrEmpty(PathToIdentify) &&
                string.IsNullOrEmpty(ExeString) &&
                RemotePort == 0 && LocalPort == 0)
            {
                MessageBox.Show("Необходимо настроить ПО");
                return;
            }
            try
            {
                var ais = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AIS",true);
                ais.SetValue("FileName", FileName, RegistryValueKind.String);
                ais.SetValue("Exes", ExeString, RegistryValueKind.String);
                ais.SetValue("Server", Server, RegistryValueKind.String);
                ais.SetValue("Path", PathToIdentify, RegistryValueKind.String);
                ais.SetValue("Local", LocalPort, RegistryValueKind.DWord);
                ais.SetValue("Remote", RemotePort, RegistryValueKind.DWord);
                ais.Flush();
                ais.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadSettings()
        {
            var ais = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AIS");
            FileName = ais.GetValue("FileName", string.Empty).ToString();
            ExeString = ais.GetValue("Exes", string.Empty).ToString();
            Server = ais.GetValue("Server", string.Empty).ToString();
            PathToIdentify = ais.GetValue("Path", string.Empty).ToString();
            LocalPort = Convert.ToInt32(ais.GetValue("Local", 5432));
            RemotePort = Convert.ToInt32(ais.GetValue("Remote", 5432));
            ais.Close();
            ServerBox.Text = Server;
            LocalPortBox.Text = LocalPort.ToString();
            RemotePortBox.Text = RemotePort.ToString();
            Exes.Text = ExeString;
            exefile.Text = FileName;
            file.Text = Path.GetFileName(PathToIdentify);


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
                FileName = ofd.FileName;
            exefile.Text = FileName;
        }

        private void GetValues()
        {
            try
            {
                Server = ServerBox.Text;
                ExeString = Exes.Text;
                LocalPort = Convert.ToInt32(LocalPortBox.Text);
                RemotePort = Convert.ToInt32(RemotePortBox.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
