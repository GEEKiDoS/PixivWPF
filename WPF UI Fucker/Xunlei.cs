using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_UI_Fucker
{
    public class XunleiDownloadTask:IDisposable
    {
        List<AXunleiTask> Downloading;
        List<AXunleiTask> NeedDownload;
        List<AXunleiTask> Done;
        bool Inited;

        class AXunleiTask
        {
            public string Url;
            public string Path;
            public int TaskID;

            public AXunleiTask()
            {
                TaskID = 0;
            }

        }

        public static XunleiDownloadTask Obj
        {
            get
            {
                return _obj;
            }
            private set
            {
                _obj = value;
            }
        }

        private static XunleiDownloadTask _obj = null;

        public XunleiDownloadTask()
        {
            if (Obj != null)
                throw new XunleiDownloadEngineInitedExp();
            else
                Obj = this;

            NativeMethods.XLInitDownloadEngine();
            Inited = true;
            
            Downloading = new List<AXunleiTask>();
            NeedDownload = new List<AXunleiTask>();
            Done = new List<AXunleiTask>();
            CheckTasks();
        }

        /// <summary>
        /// 释放迅雷下载引擎
        /// </summary>
        public void UnInitEngine()
        {
            NativeMethods.XLUninitDownloadEngine();
            Inited = false;
            Obj = null;
        }

        // 检查任务情况
        private async void CheckTasks()
        {
            await Task.Run(() =>
            {
                List<AXunleiTask> temp = null;
                while (true)
                {
                    if (!Inited)
                        break;
                    temp = Downloading;
                    if (temp.Count != 0)
                        for (int i = 0; i < temp.Count; i++)
                        {
                            AXunleiTask item = temp[i];
                            int plStatus = 0;
                            ulong pullFileSize = 0;
                            ulong pullRecvSize = 0;
                            NativeMethods.XLQueryTaskInfo(item.TaskID, ref plStatus, ref pullFileSize, ref pullRecvSize);
#if DEBUG
                            Console.WriteLine(string.Format("[DEBUG] Task Status\n ID:{0} | Status:{1} | FileSize:{2}", item.TaskID, plStatus, pullFileSize));
#endif
                            if (plStatus == NativeMethods.TaskStatus_Success)
                            {
                                Done.Add(item);
                                Downloading.Remove(item);
                            }
                            else if (plStatus == NativeMethods.TaskStatus_Fail)
                            {
                                string[] files = Directory.GetFiles(Path.GetDirectoryName(item.Path));
                                foreach (string file in files)
                                {
                                    if (file.Contains(Path.GetFileNameWithoutExtension(item.Path)))
                                    {
                                        File.Delete(file);
                                    }
                                }
                                item.TaskID = 0;
                                NativeMethods.XLURLDownloadToFile(item.Path, item.Url, "http://www.pixiv.net/", ref item.TaskID);
                            }
                        }
                    temp = NeedDownload;
                    if (temp.Count != 0)
                        for (int i = 0; i < temp.Count; i++)
                        {
                            AXunleiTask item = temp[i];
                            if (Downloading.Count < 5)
                            {
                                NativeMethods.XLURLDownloadToFile(item.Path, item.Url, "http://www.pixiv.net/", ref item.TaskID);
                                NativeMethods.XLContinueTask(item.TaskID);
                                Downloading.Add(item);
                                NeedDownload.Remove(item);
                            }
                        }
#if DEBUG
                    else
                        Console.WriteLine("[DEBUG] There are no any task in the list!");
#endif
                    Thread.Sleep(2000);
                }
                GC.SuppressFinalize(temp);
            });
        }

        /// <summary>
        /// 添加到任务队列
        /// </summary>
        /// <param name="url">链接</param>
        /// <param name="path">目标路径</param>
        public void AddToTask(string url, string path)
        {
            if (Downloading.Count >= 5)
            {

                NeedDownload.Add(new AXunleiTask
                {
                    Path = path,
                    Url = url,
                });
            }
            else
            {
                AXunleiTask DownloadTask = new AXunleiTask
                {
                    Path = path,
                    Url = url,
                };
                DownloadTask.TaskID = NativeMethods.XLURLDownloadToFile(path, url, "http://www.pixiv.net/", ref DownloadTask.TaskID);
                NativeMethods.XLContinueTask(DownloadTask.TaskID);
                Downloading.Add(DownloadTask);
            }
            return;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GC.SuppressFinalize(NeedDownload);
                    GC.SuppressFinalize(Downloading);
                    GC.SuppressFinalize(Done);
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~XunleiDownloadTask() {
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

    [Serializable]
    internal class XunleiDownloadEngineInitedExp : Exception
    {
        public XunleiDownloadEngineInitedExp()
        {
        }

        public XunleiDownloadEngineInitedExp(string message) : base(message)
        {
        }

        public XunleiDownloadEngineInitedExp(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected XunleiDownloadEngineInitedExp(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
