using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_UI_Fucker
{
    /// <summary>
    /// WorkShow.xaml 的交互逻辑
    /// </summary>
    public partial class WorkShow : Grid,IDisposable
    {
        public WorkShow(string workname, long workid, string artistname, long artistid, bool stared, string previewpiclink)
        {
            InitializeComponent();
            isStarted = stared;
            if(stared)
            {
                star.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
            }
            textBlock.Text = workname;
            textBlock_Copy.Text = artistname;
            ArtistId = artistid;
            WorkId = workid;
            LoadPreview(workid.ToString());
        }

        private async void LoadPreview(string workID)
        {
            string filename = string.Format("cache\\{0}_preview.png", workID);
            await Task.Run(() =>
            {
                while (true)
                {
                    if (File.Exists(filename))
                    {
                        image.Dispatcher.Invoke(() =>
                        {
                            using (BinaryReader binReader = new BinaryReader(File.Open(filename, FileMode.Open)))
                            {
                                FileInfo fileInfo = new FileInfo(filename);
                                byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                                binReader.Close();
                                BitmapImage bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = new MemoryStream(bytes);
                                bi.DecodePixelWidth = 800;
                                bi.EndInit();
                                image.Source = bi;
                                GC.SuppressFinalize(bi);
                                GC.SuppressFinalize(fileInfo);
                                GC.SuppressFinalize(bytes);
                            }
                        });
                        break;
                    }
                    Thread.Sleep(2000);
                }
            });

        }

        public long WorkId { get; private set; }
        public long ArtistId { get; private set; }

        public bool isStarted;

        private async void Star(object sender, RoutedEventArgs e)
        {
            if (isStarted)
            {

                try { await AT.T.DeleteMyFavoriteWorksAsync(WorkId); } catch { }
                    
                star.Foreground = new SolidColorBrush(Color.FromArgb(255, 96, 96, 96));
                isStarted = false;
            }
            else
            {
                try { await AT.T.AddMyFavoriteWorksAsync(WorkId); } catch { }
                star.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
                isStarted = true;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            WorkDetailWindow wdw = new WorkDetailWindow(WorkId);
            wdw.Show();
        }

        private new void Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GC.SuppressFinalize(image);
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~WorkShow() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
