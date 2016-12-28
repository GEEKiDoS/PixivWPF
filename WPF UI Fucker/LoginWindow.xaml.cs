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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Pixeez;
using Pixeez.Objects;
using System.IO;
using System.Net.Cache;
using System.Diagnostics;

namespace WPF_UI_Fucker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            closebtn.shit.Click += (object sender, RoutedEventArgs e) => Close();
            ErrorCode.Text = string.Empty;
            Init();
        }

        protected bool Logging = false;

        private void Init()
        {
            if (File.Exists("avatar.png"))
            {
                var ib = new ImageBrush();
                BinaryReader binReader = new BinaryReader(File.Open("avatar.png", FileMode.Open));
                FileInfo fileInfo = new FileInfo("avatar.png");
                byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                binReader.Close();
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(bytes);
                bi.EndInit();
                ib.ImageSource = bi;
                ib.TileMode = TileMode.None;
                ib.Stretch = Stretch.UniformToFill;
                avat.Fill = ib;
            }
            INIClass ini = new INIClass(".\\config.ini");
            if (ini.ExistINIFile())
            {
                string username = ini.IniReadValue("Pixiv", "Username");
                string passwd = ini.IniReadValue("Pixiv", "Passwd");
                if (!string.IsNullOrEmpty(username))
                {
                    textBox_Copy.Text = username;
                    if (!string.IsNullOrEmpty(username))
                        Passwordbox.Password = passwd;
                }
                Login(this, new RoutedEventArgs());
            }
        }

        private void DragMov(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            prog.IsIndeterminate = true;
            if (Logging != true)
            {
                Logging = true;
                AT.T = await Auth.AuthorizeAsync(textBox_Copy.Text, Passwordbox.Password);
                if (Auth.statuscode == "OK")
                {
                    INIClass ini = new INIClass(".\\config.ini");
                    ini.IniWriteValue("Pixiv", "AccessToken", AT.T.AccessToken);
                    ini.IniWriteValue("Pixiv", "Username", textBox_Copy.Text);
                    ini.IniWriteValue("Pixiv", "Passwd", Passwordbox.Password);
                    (new MainWindow()).Show();
                    Close();
                }
                else
                {
                    ErrorCode.Text = string.Format("登陆失败 原因: {0}", Auth.statuscode);
                }
                Logging = false;
            }
            prog.IsIndeterminate = false;
        }

        private void ChangeUserName(object sender, TextChangedEventArgs e)
        {
            textBlock2.Text = textBox_Copy.Text;
        }

        private void Enter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Login(sender, new RoutedEventArgs());
            }
        }

        private void ShowSetting(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("点你个哈嘛批，还没做");
        }

        private void ForgetPW(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.pixiv.net/reminder.php");
        }

        private void SignUP(object sender, RoutedEventArgs e)
        {
            Process.Start("https://accounts.pixiv.net/signup");
        }
    }
}
