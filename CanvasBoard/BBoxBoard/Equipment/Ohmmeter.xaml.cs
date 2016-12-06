using BBoxBoard.Comp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BBoxBoard.Equipment
{
    /// <summary>
    /// Interaction logic for Ohmmeter.xaml
    /// </summary>
    public partial class Ohmmeter : Window
    {
        private OhmMeter ohmmterClass;
        public ShowingData showingData;
        Line line;

        public Ohmmeter(OhmMeter ohmmterClass_)
        {
            InitializeComponent();
            ohmmterClass = ohmmterClass_;
            showingData = ohmmterClass.showingData;
            this.textBlock.DataContext = showingData;

            //line = new Line();
            //line.Stroke = Brushes.Green;
            //line.StrokeThickness = 10;
            //line.X1 = 0;
            //line.Y1 = 0;
            //line.X2 = 100;
            //line.Y2 = 100;
            //ohmCanvas.Children.Add(line);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //line.X1++;
            //必须通过拖动来关闭窗口
            if (ohmmterClass.CanbeClosed)
            {
                base.OnClosing(e);
            }
            else
            {
                showingData.SimpleData = "啦啦啦";
                e.Cancel = true;
            }
        }
    }

}
