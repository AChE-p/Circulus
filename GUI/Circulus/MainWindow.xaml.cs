using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Timers;

namespace Circulus
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// UI字段及方法
        /// </summary>
        public delegate void ShutSubWindows();
        private ShutSubWindows shutWinHandler; //用于在主窗体关闭时关闭子窗体的委托

        /// <summary>
        /// 主窗体事件处理方法和构造器
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.shutWinHandler != null) shutWinHandler();     
        }
    }

}

