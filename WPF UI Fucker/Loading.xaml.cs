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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_UI_Fucker
{
    /// <summary>
    /// Loading.xaml 的交互逻辑
    /// </summary>
    public partial class Loading : Grid
    {
        public Loading()
        {
            InitializeComponent();
            IsEnabled = false;
        }

        public void Start()
        {
            IsEnabled = true;
            BeginStoryboard((Storyboard)FindResource("Start"));
        }

        public void Stop()
        {
            IsEnabled = false;
            BeginStoryboard((Storyboard)FindResource("Done"));
        }
    }
}
