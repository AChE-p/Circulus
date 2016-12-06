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
    /// Interaction logic for Voltmeter.xaml
    /// </summary>
    public partial class Voltmeter : Window
    {
        private VoltMeter voltmeterClass;
        public ShowingData showingData;

        public Voltmeter(VoltMeter voltmeterClass_)
        {
            InitializeComponent();
            voltmeterClass = voltmeterClass_;
            showingData = voltmeterClass.showingData;
            this.textBlock.DataContext = showingData;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //必须通过拖动来关闭窗口
            if (voltmeterClass.CanbeClosed)
            {
                base.OnClosing(e);
            }
            else
            {
                showingData.SimpleData = "huhuhu";
                e.Cancel = true;
            }
        }
    }
}
