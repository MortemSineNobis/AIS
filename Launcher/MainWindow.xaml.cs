using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;


namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Process SSH;
        string Exes = string.Empty;
        string FileName = string.Empty;
        string UserName = string.Empty;
        string Server = string.Empty;
        string PathToIdentify = string.Empty;
        string PasswordString = string.Empty;
        int LocalPort = 0;
        int RemotePort = 0;
        bool ReadPassword = false;
        Timer timer = new Timer();
        List<Key> SettingsPassword = new List<Key>();

        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
            timer.Interval = 1000;
            timer.Elapsed += Check;
        }

        private void Check(object sender, ElapsedEventArgs e)
        {
            int i = 0;
            foreach (var item in Exes.Split(';'))
            {
                if (isHaveProcess(System.IO.Path.GetFileNameWithoutExtension(item)))
                    i++;
            }
            if (i == 0)
            {
                Process p = Process.GetProcesses().First(
                    x => 
                    x.ProcessName == System.IO.Path.GetFileNameWithoutExtension(FileName));
                p.Kill();
                Environment.Exit(0);
            }
            
        }

        private void TechHelp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText("bes-sasha09@live.ru");
        }
        private void Close(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowPasswordCharsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Password.Visibility = Visibility.Collapsed;
            PasswordText.Visibility = Visibility.Visible;
            PasswordText.Focus();
            SendKey(Key.End);
           // SendKeys.SendWait("{END}");
        }

        private void ShowPasswordCharsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Password.Visibility = System.Windows.Visibility.Visible;
            PasswordText.Visibility = System.Windows.Visibility.Collapsed;

            Password.Focus();
            SendKey(Key.End);
            //System.Windows.Forms.SendKeys.SendWait("{END}");
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password.Visibility == Visibility.Visible)
                PasswordText.Text = Password.Password;
        }

        private void PasswordText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordText.Visibility == Visibility.Visible)
                Password.Password = PasswordText.Text;
        }
        private async void Auth_ClickAsync(object sender, RoutedEventArgs e)
        {
            GetValues();
            if (string.IsNullOrEmpty(FileName) &&
                string.IsNullOrEmpty(UserName) &&
                string.IsNullOrEmpty(PasswordString) &&
                string.IsNullOrEmpty(PathToIdentify) &&
                RemotePort == 0 && LocalPort == 0)
            {
                System.Windows.MessageBox.Show("Необходимо настроить ПО");
                return;
            }
            SSH = Process.Start(new ProcessStartInfo
            {
                FileName = this.FileName,
                Arguments = $"{UserName}@{Server} -L {LocalPort}:localhost:{RemotePort} -i {PathToIdentify} -pw {PasswordString}",
                CreateNoWindow = false,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            //await Task.Delay(1000);
            //var test = Process.Start(new ProcessStartInfo
            //{
            //    FileName = @"cmd",
            //    Arguments = $"/c netstat /na | find /\"{LocalPort}\"",
            //    CreateNoWindow = true,
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true
            //});
            //var t = test.StandardOutput.ReadToEnd();
            //var tt = t.Split('\n');
            bool good = true;
            //foreach (var item in tt)
            //{
            //    var index = item.IndexOf($"{LocalPort}");
            //    if (item.Contains($"{LocalPort}"))
            //        if (item[index - 1] == ':' && item[index + LocalPort.ToString().Length] == ' ')
            //            good = true;
            //}
            if (good && Saving.IsChecked == true)
            {
                try
                {
                    var ais = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AIS",true);
                    if (ais.GetValue("UserName", string.Empty).ToString() != UserName)
                        ais.SetValue("UserName", UserName, RegistryValueKind.String);
                    if (ais.GetValue("Passwd", string.Empty).ToString() != PasswordString)
                        ais.SetValue("Passwd", PasswordString, RegistryValueKind.String);
                    ais.Flush();
                    ais.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (good)
            {
                foreach (var item in Exes.Split(';'))
                {
                    try
                    {
                        Process.Start(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                timer.Start();
                Hide();
            }

        }

        /// <summary>
        ///   Sends the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        private static void SendKey(Key key)
        {
            if (Keyboard.PrimaryDevice != null)
            {
                if (Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                    {
                        RoutedEvent = Keyboard.KeyDownEvent
                    };
                    InputManager.Current.ProcessInput(e);

                    // Note: Based on your requirements you may also need to fire events for:
                    // RoutedEvent = Keyboard.PreviewKeyDownEvent
                    // RoutedEvent = Keyboard.KeyUpEvent
                    // RoutedEvent = Keyboard.PreviewKeyUpEvent
                }
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                ReadPassword = true;
            }
            else
            {
                if (ReadPassword)
                {
                    if (e.Key == Key.Enter) return;
                    SettingsPassword.Add(e.Key);
                    if (SettingsPassword.Count == 5) CheckSettingsPassword();
                }
            }
        }

        private void CheckSettingsPassword()
        {
            string password = string.Empty;
            SettingsPassword.ForEach((a) => password += ((int)(a)).ToString());
            if (Convert.ToInt64(password) == 5044565644)
                if (!Application.Current.Windows.OfType<Settings>().Any())
                    new Settings().Show();
            SettingsPassword.Clear();
            ReadPassword = false;
        }

        private void LoadSettings()
        {
            try
            {
                var ais = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AIS");
                if (ais == null)
                    ais = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIS");
                FileName = ais.GetValue("FileName", string.Empty).ToString();
                Exes = ais.GetValue("Exes", string.Empty).ToString();
                UserName = ais.GetValue("UserName", string.Empty).ToString();
                Server = ais.GetValue("Server", string.Empty).ToString();
                PathToIdentify = ais.GetValue("Path", string.Empty).ToString();
                PasswordString = ais.GetValue("Passwd", string.Empty).ToString();
                LocalPort = Convert.ToInt32(ais.GetValue("Local", 5432));
                RemotePort = Convert.ToInt32(ais.GetValue("Remote", 5432));
                ais.Close();
                Login.Text = UserName;
                Password.Password = PasswordString;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void GetValues()
        {
            PasswordString = Password.Password;
            UserName = Login.Text;
        }

        public bool isHaveProcess(string pName)
        {
            Process[] pList = Process.GetProcesses();
            foreach (Process myProcess in pList)
            {
                if (myProcess.ProcessName == pName)
                    return true;
            }
            return false;
        }
    }
}