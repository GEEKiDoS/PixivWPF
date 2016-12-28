using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_UI_Fucker
{
    public class INIClass
    {
        public string inipath;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="INIPath">文件路径</param>
        public INIClass(string INIPath)
        {
            inipath = INIPath;
        }
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            NativeMethods.WritePrivateProfileString(Section, Key, Value, this.inipath);
        }
        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = NativeMethods.GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            return temp.ToString();
        }
        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <returns>布尔值</returns>
        public bool ExistINIFile()
        {
            return File.Exists(inipath);
        }
    }

    public class FlushMemory
    {
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void Flush()
        {

            GC.Collect();

            GC.WaitForPendingFinalizers();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                NativeMethods.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }

        }

    }

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        internal static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        internal static extern int WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        internal static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("XLDownload.dll", EntryPoint = "XLInitDownloadEngine")]
        internal static extern bool XLInitDownloadEngine();
        [DllImport("XLDownload.dll", EntryPoint = "XLUninitDownloadEngine")]
        internal static extern bool XLUninitDownloadEngine();
        [DllImport("XLDownload.dll", EntryPoint = "XLURLDownloadToFile", CharSet = CharSet.Unicode)]
        internal static extern int XLURLDownloadToFile(string pszFileName, string pszUrl, string pszRefUrl, ref int lTaskId);
        [DllImport("XLDownload.dll", EntryPoint = "XLQueryTaskInfo", CharSet = CharSet.Auto)]
        internal static extern int XLQueryTaskInfo(int lTaskId, ref int plStatus, ref ulong pullFileSize, ref ulong pullRecvSize);
        [DllImport("XLDownload.dll", EntryPoint = "XLGetErrorMsg", CharSet = CharSet.Unicode)]
        internal static extern int XLGetErrorMsg(int dwErrorId, string pszBuffer, ref int dwSize);
        [DllImport("XLDownload.dll", EntryPoint = "XLContinueTask", CharSet = CharSet.Auto)]
        internal static extern int XLContinueTask(int lTaskId);
        [DllImport("XLDownload.dll", EntryPoint = "XLContinueTaskFromTdFile", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        internal static extern int XLContinueTaskFromTdFile(string pszTdFileFullPath, ref int lTaskId);
        [DllImport("XLDownload.dll", EntryPoint = "XLPauseTask", CharSet = CharSet.Auto)]
        private static extern int XLPauseTask(int lTaskId, ref int lNewTaskId);
        [DllImport("XLDownload.dll", EntryPoint = "XLStopTask", CharSet = CharSet.Auto)]
        internal static extern int XLStopTask(int lTaskId);

        internal const int TaskStatus_Connect = 0;                // 已经建立连接  
        internal const int TaskStatus_Download = 2;                // 开始下载   
        internal const int TaskStatus_Pause = 10;                  // 暂停  
        internal const int TaskStatus_Success = 11;                // 成功下载  
        internal const int TaskStatus_Fail = 12;                  // 下载失败  


        internal const int XL_SUCCESS = 0;
        internal const int XL_ERROR_FAIL = 0x10000000;

        // 尚未进行初始化  
        internal const int XL_ERROR_UNINITAILIZE = XL_ERROR_FAIL + 1;

        // 不支持的协议，目前只支持HTTP  
        internal const int XL_ERROR_UNSPORTED_PROTOCOL = XL_ERROR_FAIL + 2;

        // 初始化托盘图标失败  
        internal const int XL_ERROR_INIT_TASK_TRAY_ICON_FAIL = XL_ERROR_FAIL + 3;

        // 添加托盘图标失败  
        internal const int XL_ERROR_ADD_TASK_TRAY_ICON_FAIL = XL_ERROR_FAIL + 4;

        // 指针为空  
        internal const int XL_ERROR_POINTER_IS_NULL = XL_ERROR_FAIL + 5;

        // 字符串是空串  
        internal const int XL_ERROR_STRING_IS_EMPTY = XL_ERROR_FAIL + 6;

        // 传入的路径没有包含文件名  
        internal const int XL_ERROR_PATH_DONT_INCLUDE_FILENAME = XL_ERROR_FAIL + 7;

        // 创建目录失败  
        internal const int XL_ERROR_CREATE_DIRECTORY_FAIL = XL_ERROR_FAIL + 8;

        // 内存不足  
        internal const int XL_ERROR_MEMORY_ISNT_ENOUGH = XL_ERROR_FAIL + 9;

        // 参数不合法  
        internal const int XL_ERROR_INVALID_ARG = XL_ERROR_FAIL + 10;

        // 任务不存在  
        internal const int XL_ERROR_TASK_DONT_EXIST = XL_ERROR_FAIL + 11;

        // 文件名不合法  
        internal const int XL_ERROR_FILE_NAME_INVALID = XL_ERROR_FAIL + 12;

        // 没有实现  
        internal const int XL_ERROR_NOTIMPL = XL_ERROR_FAIL + 13;

        // 已经创建的任务数达到最大任务数，无法继续创建任务  
        internal const int XL_ERROR_TASKNUM_EXCEED_MAXNUM = XL_ERROR_FAIL + 14;

        // 任务类型未知  
        internal const int XL_ERROR_INVALID_TASK_TYPE = XL_ERROR_FAIL + 15;

        // 文件已经存在  
        internal const int XL_ERROR_FILE_ALREADY_EXIST = XL_ERROR_FAIL + 16;

        // 文件不存在  
        internal const int XL_ERROR_FILE_DONT_EXIST = XL_ERROR_FAIL + 17;

        // 读取cfg文件失败  
        internal const int XL_ERROR_READ_CFG_FILE_FAIL = XL_ERROR_FAIL + 18;

        // 写入cfg文件失败  
        internal const int XL_ERROR_WRITE_CFG_FILE_FAIL = XL_ERROR_FAIL + 19;

        // 无法继续任务，可能是不支持断点续传，也有可能是任务已经失败  
        // 通过查询任务状态，确定错误原因。  
        internal const int XL_ERROR_CANNOT_CONTINUE_TASK = XL_ERROR_FAIL + 20;

        // 无法暂停任务，可能是不支持断点续传，也有可能是任务已经失败  
        // 通过查询任务状态，确定错误原因。  
        internal const int XL_ERROR_CANNOT_PAUSE_TASK = XL_ERROR_FAIL + 21;

        // 缓冲区太小  
        internal const int XL_ERROR_BUFFER_TOO_SMALL = XL_ERROR_FAIL + 22;

        // 调用XLInitDownloadEngine的线程，在调用XLUninitDownloadEngine之前已经结束。  
        // 初始化下载引擎线程，在调用XLUninitDownloadEngine之前，必须保持执行状态。  
        internal const int XL_ERROR_INIT_THREAD_EXIT_TOO_EARLY = XL_ERROR_FAIL + 23;

        // TP崩溃  
        internal const int XL_ERROR_TP_CRASH = XL_ERROR_FAIL + 24;

        // 任务不合法，调用XLContinueTaskFromTdFile继续任务。内部任务切换失败时，会产生这个错误。  
        internal const int XL_ERROR_TASK_INVALID = XL_ERROR_FAIL + 25;
    }

}
