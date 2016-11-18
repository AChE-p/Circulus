using BBoxBoard.BasicDraw;
using BBoxBoard.Comp;
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

namespace BBoxBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int CanvasWidth = 985;
        public const int CanvasHeight = 715;
        public const int GridLen = 10;

        private ElecCompSet elecCompSet;
        private IntPoint PushDownPoint;
        private IntPoint HasMoved;
        private List<Image> ImageArr;

        public MainWindow()
        {
            InitializeComponent();
            ImageArr = new List<Image>();
            //MessageBox.Show("" + Environment.CurrentDirectory);
            for (int i=0; i<2; i++)
            {
                Image image = new Image();
                image.Width = 200;
                image.Height = 150;
                /*image.Source = new BitmapImage(new Uri("C:\\Users" +
                    "\\37754\\Pictures\\doge.jpg"));*/
                image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory
                    + "\\doge.jpg"));
                ImageArr.Add(image);
            }
            this.elecCompList.ItemsSource = ImageArr;
            this.elecCompList.MouseDoubleClick += ElecCompList_MouseDoubleClick;
            //UpdateList();
            this.Mycanvas.MouseDown += Mycanvas_MouseDown;
            this.Mycanvas.MouseUp += Mycanvas_MouseUp;
            this.Mycanvas.MouseMove += Mycanvas_MouseMove;
            elecCompSet = new ElecCompSet();
            //elecCompSet.AddCompAndShow(new Resistance(), Mycanvas);
            //elecCompSet.AddCompAndShow(new Capacity(), Mycanvas);
            //resistance2.Move(100, 200);
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R && elecCompSet.pressedElecComp != null)
            {
                //MessageBox.Show("Rotating!");
                elecCompSet.pressedElecComp.RotateLeft();
            }
        }

        private void ElecCompList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
                }
            }
        }

        private void Mycanvas_MouseMove(object sender, MouseEventArgs e)
        {
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
            Mouse.Capture(null);
            //取消正在移动的东西，并刷新电路网格
            Point p = e.GetPosition(Mycanvas);
            if (p.X >= 10 && p.X <= 70 && p.Y >= 10 && p.Y <= 90)
            {
                elecCompSet.DeleteNowPressed(Mycanvas);
            }
            elecCompSet.ReleaseElecComp();
        }

        private void Mycanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
                if (elecCompSet.FoundPressedElecComp(point))
                {
                    //MessageBox.Show("Found!");
                    textBox.Text = "Found";
                    PushDownPoint = point;
                    HasMoved = new IntPoint(0, 0);
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
    }
}
