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
using System.Windows.Shapes;

namespace WPF_UI_Fucker
{
    /// <summary>
    /// WorkDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WorkDetailWindow : Window
    {
        private long workId;

        public WorkDetailWindow()
        {
            InitializeComponent();
            MessageBox.Show("点你个哈嘛批，还没做");
        }

        public WorkDetailWindow(long workId)
        {
            this.workId = workId;
            MessageBox.Show("点你个哈嘛批，还没做");
        }
    }
}
