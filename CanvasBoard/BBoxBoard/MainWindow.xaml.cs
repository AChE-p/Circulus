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
        public const int CanvasWidth = 985;
        public const int CanvasHeight = 715;//面板大小
        public const int GridLen = 10;//格点间距
        public bool IsRuning;
       
        SynchronizationContext m_SyncContext;//控制线程用
        Thread mThread;
        oscilloscope myOscilloscope;//示波器
        SuspensionWindow suspensionWindow;//右键设置元件参数

        private ElecCompSet elecCompSet;//存放所有元件
        private IntPoint PushDownPoint;//鼠标点击位置
        private IntPoint HasMoved;//相对移动距离
        //private List<Image> ImageArr;
        private List<String> StringArr;//用于添加元件
        condition mycondition;


        public MainWindow()
        {
            
            IsRuning = false;
            InitializeComponent();
            m_SyncContext = SynchronizationContext.Current;
            suspensionWindow = new SuspensionWindow(this);
            
            StringArr = new List<string>();
            //@d 这里是出现在页面上的元件列表
            StringArr.Add("电阻");
            StringArr.Add("电容");
            StringArr.Add("导线");
            StringArr.Add("电感");
            StringArr.Add("电阻表");
            StringArr.Add("电压表");
            StringArr.Add("地");
            StringArr.Add("红色探针");
            StringArr.Add("蓝色探针");
            StringArr.Add("直流电源");
            StringArr.Add("交流电源");
            //@@d
            this.elecCompList.ItemsSource = StringArr;
            this.elecCompList.MouseDoubleClick += ElecCompList_MouseDoubleClick;
            //UpdateList();
            this.Mycanvas.MouseLeftButtonDown += Mycanvas_MouseLeftButtonDown;
            this.Mycanvas.MouseUp += Mycanvas_MouseUp;
            this.Mycanvas.MouseMove += Mycanvas_MouseMove;
            this.Mycanvas.MouseRightButtonUp += Mycanvas_MouseRightButtonUp;
            //定义面板上的鼠标操作
            elecCompSet = new ElecCompSet();
            //elecCompSet.AddCompAndShow(new Resistance(), Mycanvas);
            //elecCompSet.AddCompAndShow(new Capacity(), Mycanvas);
            //resistance2.Move(100, 200);
            this.KeyDown += MainWindow_KeyDown;          
            this.start_button.Click += Start_button_Click;
            SyncProgess(100, "无任务"); //用这个函数异步更新ProgressBar的值
            mycondition = new condition();
            myOscilloscope = new oscilloscope();//示波器，只有一个实例

        }

        private void Start_button_Click(object sender, RoutedEventArgs e)
        {
            if (IsRuning)
            {
                mThread.Abort();//异常终止
                MessageBox.Show("强行中断模拟！");
                IsRuning = false;
            }
            else
            {
                elecCompSet.ShowAllMeter();
                myOscilloscope.ClearAll();
                mThread = new Thread(Elec_Run);//新开线程执行任务，函数名作为参数，结束之后关闭该线程。
                mThread.Start();
            }
        }

        private void Elec_Run()
        {
            this.IsRuning = true;
            //MessageBox.Show("模拟中！");
            //for (int i=0; i<100; i++)
            //{
            //    Thread.Sleep(2000);
            //    MessageBox.Show("模拟:" + i);
                
            //}

            Processing processing = new Processing(GetAllComp(), mycondition.precision_time, 100, this);
            this.IsRuning = false;
            MessageBox.Show("模拟结束！");
        }
        //@d 定义主页面上的键盘操作，建议卸载帮助文档里
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.T) //Transform
            {
                List<BriefElecComp> A = GetAllComp();
                SimplifiedPic simplifiedPic = new SimplifiedPic(A, this);
            }
            else if (e.Key == Key.I)
            {
                MessageBox.Show(elecCompSet.ToString());
            }
            else if (e.Key == Key.R && elecCompSet.pressedElecComp != null)
            {
                //MessageBox.Show("Rotating!");
                elecCompSet.pressedElecComp.RotateLeft();
            }
            else if (e.Key == Key.D && elecCompSet.pressedElecComp != null)
            {
                if (elecCompSet.pressedElecComp.IsWire)
                {
                    elecCompSet.pressedElecComp.State = ElecComp.State_AdjRight;
                }
            }
            else if (e.Key == Key.S)
            {
                LayoutRecord.Save(elecCompSet);
            }
            if (e.Key == Key.O)
            {
                /*List<BriefElecComp> A = GetAllComp();
                String str = "";
                foreach (BriefElecComp b in A)
                {
                    str += b + "\n";
                }
                MessageBox.Show(str); //之前写的演示用的，注释掉，现在是打开文件*/
                LayoutRecord.Read(elecCompSet, Mycanvas);
            }
        }
        private void ElecCompList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsRuning) return;
            if (elecCompList.SelectedItems.Count == 1)
            {
                //MessageBox.Show("Select: " + elecCompList.SelectedIndex);
                switch (elecCompList.SelectedIndex)
                {
                    case 0:
                        Resistance r = new Resistance();
                        elecCompSet.AddCompAndShow(r, Mycanvas);
                        r.Move(100, 100);
                        break;
                    case 1:
                        Capacity c = new Capacity();
                        elecCompSet.AddCompAndShow(c, Mycanvas);
                        c.Move(100, 100);
                        break;
                    case 2:
                        Wire w = new Wire();
                        elecCompSet.AddCompAndShow(w, Mycanvas);
                        w.Move(100, 100);
                        break;
                    case 3:
                        Inductance i = new Inductance();
                        elecCompSet.AddCompAndShow(i, Mycanvas);
                        i.Move(100, 100);
                        break;
                    case 4:
                        OhmMeter o = new OhmMeter();
                        elecCompSet.AddCompAndShow(o, Mycanvas);
                        o.Move(100, 100);
                        break;
                    case 5:
                        VoltMeter ee = new VoltMeter();
                        elecCompSet.AddCompAndShow(ee, Mycanvas);
                        ee.Move(100, 100);
                        break;
                    case 6:
                        ElecGround eg = new ElecGround();
                        elecCompSet.AddCompAndShow(eg, Mycanvas);
                        eg.Move(100, 100);
                        break;
                    case 7:
                        oscilloscopeData myOscilloscopeData = new
                            oscilloscopeData(Brushes.Red, oscilloscopeData.Volt_Index,
                                myOscilloscope.m_SyncContext);
                        Probe pb = new Probe(Brushes.Red, myOscilloscopeData);
                        if (!myOscilloscope.IsVisible)
                        {
                            myOscilloscope.Show();
                        }
                        myOscilloscope.AddData(myOscilloscopeData);
                        myOscilloscope.SyncSettings();
                        elecCompSet.AddCompAndShow(pb, Mycanvas);
                        pb.Move(100, 100);
                        break;
                    case 8:
                        myOscilloscopeData = new
                            oscilloscopeData(Brushes.Blue, oscilloscopeData.Volt_Index,
                                myOscilloscope.m_SyncContext);
                        pb = new Probe(Brushes.Blue, myOscilloscopeData);
                        if (!myOscilloscope.IsVisible)
                        {
                            myOscilloscope.Show();
                        }
                        myOscilloscope.AddData(myOscilloscopeData);
                        myOscilloscope.SyncSettings();
                        elecCompSet.AddCompAndShow(pb, Mycanvas);
                        pb.Move(100, 100);
                        break;
                    case 9:
                        Power p = new Power();
                        elecCompSet.AddCompAndShow(p, Mycanvas);
                        p.Move(100, 100);
                        break;
                    case 10:
                        ACPower ap = new ACPower(0, 0.1, 10);
                        elecCompSet.AddCompAndShow(ap, Mycanvas);
                        ap.Move(100, 100);
                        break;
                }
            }
        }

        private void Mycanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsRuning) return;
            if (e.LeftButton == MouseButtonState.Pressed && 
                elecCompSet.pressedElecComp != null)
            {
                IntPoint pCanvas = new IntPoint(e.GetPosition(Mycanvas));
                IntPoint p = ToGrid(pCanvas);
                textBox.Text = "X:" + p.X + "\nY:" + p.Y;
                if (p.X - PushDownPoint.X - HasMoved.X != 0 || 
                    p.Y - PushDownPoint.Y - HasMoved.Y != 0)
                {
                    elecCompSet.pressedElecComp.Move(p.X - PushDownPoint.X - HasMoved.X,
                        p.Y - PushDownPoint.Y - HasMoved.Y);
                    HasMoved.X = p.X - PushDownPoint.X;
                    HasMoved.Y = p.Y - PushDownPoint.Y;
                }
                
            }
        }

        private void Mycanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsRuning) return;
            Mouse.Capture(null);
            //取消正在移动的东西，并刷新电路网格
            Point p = e.GetPosition(Mycanvas);
            if (p.X >= 10 && p.X <= 70 && p.Y >= 10 && p.Y <= 90)
            {
                elecCompSet.DeleteNowPressed(Mycanvas);
            }
            elecCompSet.ReleaseElecComp();
        }

        private void Mycanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsRuning) return;
            if (suspensionWindow.IsNowShown)
            {
                suspensionWindow.ReleaseChooses();
            }
            var targetElement = e.Source as IInputElement;
            IntPoint point = ToGrid(new IntPoint(e.GetPosition(Mycanvas)));
            if (targetElement != null)
            {
                targetElement.CaptureMouse();
                /*
                 * 点下的时候判断有没有选中某一个电子元件
                 * 如果选中了，记录状态，记录初始位置和初始光标位置
                 * Move的时候一直跟着动，但是位置不是鼠标的位置，而是贴合位置
                 * 这和第一次放上去元件是一样的，只能放在格点上
                 */
                /*if (elecCompSet.FoundPressedElecComp(point))
                {
                    //MessageBox.Show("Found!");
                    textBox.Text = "Found";
                    PushDownPoint = point;
                    HasMoved = new IntPoint(0, 0);
                }*/
                //更新为根据所选的Shape决定谁被选中
                if (elecCompSet.FoundPressedElecComp(targetElement))
                {
                    //MessageBox.Show("Found!");
                    textBox.Text = "Found";
                    PushDownPoint = point;
                    HasMoved = new IntPoint(0, 0);
                }
            }
        }

        private void Mycanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsRuning) return;
            if (suspensionWindow.IsNowShown)
            {
                suspensionWindow.ReleaseChooses();
            }
            var targetElement = e.Source as IInputElement;
            IntPoint point = ToGrid(new IntPoint(e.GetPosition(Mycanvas)));
            if (targetElement != null)
            {
                targetElement.CaptureMouse();
                if (elecCompSet.FoundPressedElecComp(targetElement))
                {
                    //MessageBox.Show("Right!");
                    textBox.Text = "Right";
                    ElecComp elecComp = elecCompSet.GetPressedElecComp(targetElement);
                    suspensionWindow.ShowChooses(elecComp, point);
                }
            }
        }

        private void UpdateList()
        {
            this.elecCompList.Items.Clear();
        }

        private IntPoint ToGrid(IntPoint point0)
        {
            IntPoint p = new IntPoint();
            p.X = point0.X - (point0.X % GridLen) + GridLen / 2;
            p.Y = point0.Y - (point0.Y % GridLen) + GridLen / 2;
            return p;
        }

        public List<BriefElecComp> GetAllComp()
        {
            List<BriefElecComp> A = new List<BriefElecComp>();
            elecCompSet.OutputInto(A);
            return A;
        }

        public void SyncProgess(int p, String Text)
        {
            m_SyncContext.Post(SetProgress, new KeyValuePair<int, String>
                (p, Text));//某些线程往队列里增加命令，用此函数来更新绑定ui的线程的ui。
        }

        private void SetProgress(Object x)
        {
            KeyValuePair<int, String> kvp = (KeyValuePair<int, String>)x;
            int p = kvp.Key;
            String Text = kvp.Value;
            this.progress.Value = p;
            this.progressTextBlock.Text = Text;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            elecCompSet.CloseAll(Mycanvas);
            myOscilloscope.Close();
            base.OnClosing(e);
        }

        //@d 这部分是用来调节计算模式的，相关参数在data里的condition 定义了，用的是menuitem实现的，
        
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            mycondition.presion_condition = condition.presecion_condition_enum.fast_mode;
            mycondition.precision_time = 1e-3;
            ElecFeature.Default_rC = 1e-3;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            mycondition.presion_condition = condition.presecion_condition_enum.general_mode;
            mycondition.precision_time = 1e-6;
            ElecFeature.Default_rC = 1e-6;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            mycondition.presion_condition = condition.presecion_condition_enum.hquality_mode;
            mycondition.precision_time = 1e-8;
            ElecFeature.Default_rC = 1e-8;
        }
    }
}
