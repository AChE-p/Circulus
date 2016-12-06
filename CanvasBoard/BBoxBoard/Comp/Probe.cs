using BBoxBoard.BasicDraw;
using BBoxBoard.Equipment;
using BBoxBoard.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace BBoxBoard.Comp
{
    public class Probe : ElecComp
    {
        private SolidColorBrush probeColor;
        private oscilloscopeData myOscilloscopeData;

        public Probe(SolidColorBrush x, oscilloscopeData y) {
            probeColor = x;
            myOscilloscopeData = y;
            Init();
        }

        public override void AddShapes()
        {
            Comp = Comp_Probe;
            //（已废弃）size.X = 40;
            //size.Y = 30;
            RelativeInterface.Add(new IntPoint(-1024, -1024)); //Ground
            RelativeInterface.Add(new IntPoint(20, 0)); //右端口
            //直线
            MyShape line0 = new MyShape(MyShape.Shape_Line);
            line0.GetLine().Stroke = System.Windows.Media.Brushes.Black;
            line0.GetLine().X1 = 20;
            line0.GetLine().Y1 = 5;
            line0.GetLine().X2 = 20;
            line0.GetLine().Y2 = 15;
            line0.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(line0);
            //定位圆圈
            MyShape circle1 = new MyShape(MyShape.Shape_Ellipse);
            circle1.GetEllipse().Fill = System.Windows.Media.Brushes.Red;
            circle1.GetEllipse().StrokeThickness = 3;
            circle1.GetEllipse().Stroke = System.Windows.Media.Brushes.Yellow;
            circle1.GetEllipse().Width = 10;
            circle1.GetEllipse().Height = 10;
            Canvas.SetLeft(circle1.GetEllipse(), 15);
            Canvas.SetTop(circle1.GetEllipse(), -5);
            shapeSet.AddShape(circle1);
            //直线
            MyShape line1 = new MyShape(MyShape.Shape_Line);
            line1.GetLine().Stroke = probeColor;
            line1.GetLine().X1 = 0;
            line1.GetLine().Y1 = 15;
            line1.GetLine().X2 = 40;
            line1.GetLine().Y2 = 15;
            line1.GetLine().StrokeThickness = 3;
            shapeSet.AddShape(line1);
            //直线
            MyShape line2 = new MyShape(MyShape.Shape_Line);
            line2.GetLine().Stroke = probeColor;
            line2.GetLine().X1 = 7;
            line2.GetLine().Y1 = 21;
            line2.GetLine().X2 = 33;
            line2.GetLine().Y2 = 21;
            line2.GetLine().StrokeThickness = 3;
            shapeSet.AddShape(line2);
            //直线
            MyShape line3 = new MyShape(MyShape.Shape_Line);
            line3.GetLine().Stroke = probeColor;
            line3.GetLine().X1 = 14;
            line3.GetLine().Y1 = 27;
            line3.GetLine().X2 = 26;
            line3.GetLine().Y2 = 27;
            line3.GetLine().StrokeThickness = 3;
            shapeSet.AddShape(line3);
        }

        public override BriefElecComp GetBriefElecComp()
        {
            List<IntPoint> A = new List<IntPoint>();
            A.Add(new IntPoint(RelativeInterface[1].X + XYPoint.X,
                RelativeInterface[1].Y + XYPoint.Y)); //正常的连接点
            A.Add(new IntPoint(RelativeInterface[0].X, RelativeInterface[0].Y));
            //地
            return new BriefElecComp(Comp, A, this);
        }

        public override ElecFeature GetElecFeature()
        {
            ProbeElecFeature probeElecFeature =
                new ProbeElecFeature(myOscilloscopeData.FullTime / 10000,
                    myOscilloscopeData);
            return probeElecFeature;
        }
        class ProbeElecFeature : ElecFeature
        {
            double Tsum = 0;
            double period;
            double periodSum = 0;
            oscilloscopeData myOscilloscopeData;

            public ProbeElecFeature(double period_, oscilloscopeData 
                myOscilloscopeData_) : base()
            {
                period = period_;
                myOscilloscopeData = myOscilloscopeData_;
            }

            public override double GetNext(double deltaT)
            {
                Tsum += deltaT;
                periodSum += deltaT;
                if (periodSum > period)
                {
                    myOscilloscopeData.SyncAddData(Tsum, rQ / rC);
                    periodSum = 0;
                }
                return rQ;
            }
        }


    }
}
