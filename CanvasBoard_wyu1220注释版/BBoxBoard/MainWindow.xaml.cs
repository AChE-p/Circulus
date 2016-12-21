using BBoxBoard.AdvancedDraw;
using BBoxBoard.BasicDraw;
using BBoxBoard.Comp;
using BBoxBoard.Equipment;
using BBoxBoard.Output;
using BBoxBoard.PicAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using BBoxBoard.Data;
using System.IO;
using Microsoft.Win32;
using BBoxBoard.Record;

namespace BBoxBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Core core;
        public MainWindow()
        {
            InitializeComponent();
            /*
             * 将UI层和算法曾分离，所有的算法在Core里实现，但是有三个东西必须要提供
             * 一个是Canvas用来用户操作，一个是ProgressBar用来显示当前的进度，一个是TextBlock用于显示进度相关信息
             * 除了必须要提供的三个控件，还有几个函数是可以操作Core的
             *     1.try_AddElecComp(int Index)用来向面板添加一个原件
             *     2.try_Start()用来开始计算
             *     3.ChangeVMode()用来改变计算精度
             *     4.OnClosing()用来关闭所有窗口
             *     5.来加函数啊！！！！！！我就写到这里了
             */
            core = new Core(Mycanvas, progress, progressTextBlock);
            this.elecCompList.ItemsSource = core.StringArr;
            this.elecCompList.MouseDoubleClick += ElecCompList_MouseDoubleClick;
            this.start_button.Click += Start_button_Click;
        }

        private void ElecCompList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (elecCompList.SelectedItems.Count == 1)
            {
                core.try_AddElecComp(elecCompList.SelectedIndex);
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            core.OnClosing();
            base.OnClosing(e);
        }
        private void Start_button_Click(object sender, RoutedEventArgs e) => core.try_Start();
        private void MenuItem_Click_1(object sender, RoutedEventArgs e) => core.ChangeVMode(1);
        private void MenuItem_Click_2(object sender, RoutedEventArgs e) => core.ChangeVMode(2);
        private void MenuItem_Click_3(object sender, RoutedEventArgs e) => core.ChangeVMode(3);
    }
}
