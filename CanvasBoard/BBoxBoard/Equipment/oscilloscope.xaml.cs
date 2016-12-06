using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for oscilloscope.xaml
    /// </summary>
    public partial class oscilloscope : Window
    {
        public bool CanbeClosed = false;
        public List<oscilloscopeData> Datas;
        public const int ZeroTop = 100;
        public const int ZeroLeft = 0;
        public const int OsiWidth = 750;
        public const int OsiHeight = 450;
        public double FullTime = 100;
        //0：电压，1：电流
        public double[] FullValue = new double[2] { 20, 1 };
        public SynchronizationContext m_SyncContext;

        public oscilloscope()
        {
            InitializeComponent();
            Datas = new List<oscilloscopeData>();
            m_SyncContext = SynchronizationContext.Current;
        }

        public void SyncSettings()
        {
            foreach (oscilloscopeData data in Datas)
            {
                data.FullTime = FullTime;
                data.FullValue = FullValue;
                data.OsiWidth = OsiWidth;
                data.OsiHeight = OsiHeight;
            }
        }

        public void AddData(oscilloscopeData data)
        {
            Datas.Add(data);
            Path path = data.path;
            canvas.Children.Add(path);
            Canvas.SetLeft(path, ZeroLeft);
            Canvas.SetTop(path, ZeroTop);
        }

        public void DeleteData(oscilloscopeData data)
        {
            canvas.Children.Remove(data.path);
            Datas.Remove(data);
        }

        public void ClearAll()
        {
            foreach (oscilloscopeData data in Datas)
            {
                data.ClearAllPoint();
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (CanbeClosed)
            {
                base.OnClosing(e);
            }
            else this.Hide();
        }
    }
}
