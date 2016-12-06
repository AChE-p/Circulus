using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BBoxBoard.Equipment
{
    public class oscilloscopeData
    {
        public const int strokeThickness = 3;
        public Path path;
        PathFigure pf;
        public int OsiWidth;
        public int OsiHeight;
        public double FullTime;
        //0：电压，1：电流
        public double[] FullValue;
        public const int Volt_Index = 0;
        public const int I_Index = 1;
        private int Index;
        SynchronizationContext m_SyncContext;

        public oscilloscopeData(SolidColorBrush solidColorBrush, int value,
            SynchronizationContext m_SyncContext_)
        {
            m_SyncContext = SynchronizationContext.Current;
            Index = value;
            PathGeometry pg = new PathGeometry();
            path = new Path();
            pf = new PathFigure();
            path.Stroke = solidColorBrush;
            path.StrokeThickness = strokeThickness;
            pf.StartPoint = new Point(0, 0);
            pf.Segments.Add(new LineSegment(new Point(0, 0), true));
            pg.Figures.Add(pf);
            path.Data = pg;
        }

        public void SyncAddData(double time, double value, bool IsStroken = true)
        {
            //MessageBox.Show("" + m_SyncContext);
            //MessageBox.Show("" + time + " " + value);
            m_SyncContext.Post(AddData2, new KeyValuePair<bool ,Point>(IsStroken, 
                new Point(time / FullTime * OsiWidth,
            -value / FullValue[Index] * OsiHeight)));
        }

        public void ClearAllPoint()
        {
            pf.Segments.Clear();
        }

        private void AddData2(Object x)
        {
            KeyValuePair<bool, Point> kvp = (KeyValuePair<bool, Point>)x;
            pf.Segments.Add(new LineSegment(kvp.Value, kvp.Key));
        }
    }
}
