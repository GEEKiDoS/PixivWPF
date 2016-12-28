using Pixeez.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

namespace WPF_UI_Fucker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        XunleiDownloadTask XLEngine;
        string CurrectMode = null;

        public MainWindow()
        {
            GC.Collect();
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            XLEngine = new XunleiDownloadTask();
            ResetSidebarButtons();
            File.Delete("avatar.png");
            XLEngine.AddToTask(AT.ME.User.ProfileImageUrls.Px170x170, Path.Combine(Environment.CurrentDirectory, "avatar.png"));
            Username.Text = AT.ME.User.Name;
            LoadAvat();
            GetRankingAll();
        }

        private async void LoadAvat()
        {
            string filename = "avatar.png";
            await Task.Run(() =>
            {
                while (true)
                {
                    if (File.Exists(filename))
                    {
                        Avat.Dispatcher.Invoke(() =>
                        {
                            var ib = new ImageBrush();
                            BinaryReader binReader = new BinaryReader(File.Open(filename, FileMode.Open));
                            FileInfo fileInfo = new FileInfo(filename);
                            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                            binReader.Close();
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = new MemoryStream(bytes);
                            bi.EndInit();
                            ib.ImageSource = bi;
                            ib.TileMode = TileMode.None;
                            ib.Stretch = Stretch.UniformToFill;
                            Avat.Fill = ib;
                        });
                        break;
                    }
                    Thread.Sleep(500);
                }
            });

        }

        private async void GetRankingAll()
        {
            if (CallSideBar.IsChecked != false)
            {
                CallSideBar.IsChecked = false;
                Toggle(this, new RoutedEventArgs());
            }
            CurrectMode = "Ranking";
            ModeText.Text = "今日排行榜";
            Brush Highlightcolor = new SolidColorBrush(Color.FromArgb(255, 0, 134, 245));
            iconRanking.Foreground = Highlightcolor;
            textRanking.Foreground = Highlightcolor;
            // 老夫要性能优化
            ItemCollection temp = listbox.Items;
            listbox.ItemsSource = null;
            Loading.Start();
            await Task.Run(async () =>
            {
                for (int i = 0; i < temp.Count; i++)
                {
#if DEBUG
                    Console.WriteLine(GC.GetGeneration(temp[i]));
#endif
                    ((WorkShow)(temp[i])).Dispose();
                    ((List<WorkShow>)(listbox.ItemsSource)).Remove((WorkShow)(temp[i]));
                }
                GC.SuppressFinalize(temp);
                FlushMemory.Flush();
                Paginated<Rank> shit = await AT.T.GetRankingAllAsync("daily", 1, 20);
                listbox.Dispatcher.Invoke(() =>
                {
                    List<WorkShow> l = new List<WorkShow>();

                    foreach (Rank r in shit)
                    {
                        foreach (RankWork w in r.Works)
                        {
                            long workid = 0;
                            long userid = 0;
                            bool isliked = false;
                            if (w.Work.Id != null)
                            {
                                workid = (long)w.Work.Id;
                            }
                            if (w.Work.User.Id != null)
                            {
                                userid = (long)w.Work.User.Id;
                            }
                            if (w.Work.FavoriteId != null)
                            {
                                isliked = true;
                            }
                            if (!File.Exists(string.Format("{0}\\cache\\{1}_preview.png", Environment.CurrentDirectory, workid)))
                            {
                                XLEngine.AddToTask(w.Work.ImageUrls.Medium, string.Format("{0}\\cache\\{1}_preview.png", Environment.CurrentDirectory, workid));
                            }
                            l.Add(new WorkShow(w.Work.Title, workid, w.Work.User.Name, userid, isliked, w.Work.ImageUrls.Medium));
                        }
                    }

                    listbox.ItemsSource = l;
                });
            });
            await Task.Delay(1000);
            Loading.Stop();
        }

        private async void GetMyFavoriteWorks()
        {
            if (CallSideBar.IsChecked != false)
            {
                CallSideBar.IsChecked = false;
                Toggle(this, new RoutedEventArgs());
            }
            CurrectMode = "MyFavoriteWorks";
            ModeText.Text = "我的收藏";
            Brush Highlightcolor = new SolidColorBrush(Color.FromArgb(255, 255, 67, 67));
            iconMyFavoriteWorks.Foreground = Highlightcolor;
            textMyFavoriteWorks.Foreground = Highlightcolor;
            ItemCollection temp = listbox.Items;
            listbox.ItemsSource = null;
            Loading.Start();
            await Task.Run(async () =>
            {
                // 看上面
                for (int i = 0; i < temp.Count; i++)
                {
#if DEBUG
                    Console.WriteLine(string.Format("[DEBUG] GC GEN for Work Obj :{0}", GC.GetGeneration(temp[i])));
#endif
                    ((WorkShow)(temp[i])).Dispose();
                    ((List<WorkShow>)(listbox.ItemsSource)).Remove((WorkShow)(temp[i]));

                }
                GC.SuppressFinalize(temp);
                FlushMemory.Flush();
                Pixeez.Objects.Paginated<Pixeez.Objects.UsersFavoriteWork> shit = await AT.T.GetMyFavoriteWorksAsync();
                listbox.Dispatcher.Invoke(() => {
                    List<WorkShow> l = new List<WorkShow>();

                    foreach (Pixeez.Objects.UsersFavoriteWork w in shit)
                    {
                        long workid = 0;
                        long userid = 0;
                        bool isliked = false;
                        if (w.Work.Id != null)
                        {
                            workid = (long)w.Work.Id;
                        }
                        if (w.Work.User.Id != null)
                        {
                            userid = (long)w.Work.User.Id;
                        }
                        isliked = true;
                        if (!File.Exists(string.Format("{0}\\cache\\{1}_preview.png", Environment.CurrentDirectory, workid)))
                        {
                            XLEngine.AddToTask(w.Work.ImageUrls.Medium, string.Format("{0}\\cache\\{1}_preview.png", Environment.CurrentDirectory, workid));
                        }
                        l.Add(new WorkShow(w.Work.Title, workid, w.Work.User.Name, userid, isliked, w.Work.ImageUrls.Medium));
                    }

                    listbox.ItemsSource = l;
                    });
            });
            await Task.Delay(1000);
            Loading.Stop();
        }

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Toggle(object sender, RoutedEventArgs e)
        {
            if (CallSideBar.IsChecked == true)
            {
                BeginStoryboard((Storyboard)FindResource("sbon"));
            }
            else
            {
                BeginStoryboard((Storyboard)FindResource("sboff"));
            }

        }

        private void shit(object sender, MouseButtonEventArgs e)
        {
            CallSideBar.IsChecked = false;
            Toggle(sender, new RoutedEventArgs());
        }

        private void UninitXunlei(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XLEngine.UnInitEngine();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            XLEngine.UnInitEngine();
            try
            {
                string[] files = Directory.GetFiles("cache");
                foreach (string file in files)
                {
                    string exname = file.Substring(file.LastIndexOf(".") + 1);
                    if (exname == "td" | exname == "cfg" | exname == "td.cfg")
                    {
                        File.Delete(file);
                    }
                }
            }
            catch
            {
            }
            Close();
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            File.Delete("avatar.png");
            File.Delete("config.ini");
            (new LoginWindow()).Show();
            Exit(sender, e);
        }

        private void ResetSidebarButtons()
        {
            Brush DefColor = new SolidColorBrush(Color.FromArgb(255, 48, 48, 48));
            iconRanking.Foreground = DefColor;
            textRanking.Foreground = DefColor;
            iconMyFavoriteWorks.Foreground = DefColor;
            textMyFavoriteWorks.Foreground = DefColor;
        }

        private void ChangeMode(object sender, RoutedEventArgs e)
        {
            string TargetMode = ((Button)sender).Name;
            if (TargetMode != CurrectMode)
            {
                ResetSidebarButtons();
                switch (TargetMode)
                {
                    case "Ranking":
                        GetRankingAll();
                        break;
                    case "MyFavoriteWorks":
                        GetMyFavoriteWorks();
                        break;
                    case default(string):
                        break;
                }
            }
        }
    }
}
