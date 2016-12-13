using BBoxBoard.BasicDraw;
using BBoxBoard.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BBoxBoard.Output;

namespace BBoxBoard.Comp
{
    public class OhmMeter : ElecComp
    {
        private Ohmmeter ohmmeterWindow;
        public bool CanbeClosed = false;
        public ShowingData showingData;

        public OhmMeter() : base() { }

        public override void AddShapes()
        {
            //设置类型
            Comp = Comp_OhmMeter;
            /*//必须重新设置元件大小（已废弃）
            size.X = 100;
            size.Y = 20;*/
            //定义外部接口的位置
            RelativeInterface.Add(new IntPoint(0, 10)); //左端口
            RelativeInterface.Add(new IntPoint(100, 10)); //右端口
            //左边的导线
            MyShape line1 = new MyShape(MyShape.Shape_Line);
            line1.GetLine().Stroke = System.Windows.Media.Brushes.Yellow;
            line1.GetLine().X1 = 0;
            line1.GetLine().Y1 = 10;
            line1.GetLine().X2 = 30;
            line1.GetLine().Y2 = 10;
            line1.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(line1);
            //中间的长方形
            MyShape rectangle = new MyShape(MyShape.Shape_Rectangle);
            rectangle.GetRectangle().Fill = System.Windows.Media.Brushes.Yellow;
            rectangle.GetRectangle().Width = 40;
            rectangle.GetRectangle().Height = 20;
            Canvas.SetLeft(rectangle.GetRectangle(), 30);
            Canvas.SetTop(rectangle.GetRectangle(), 0);
            shapeSet.AddShape(rectangle);
            //右边的导线
            MyShape line2 = new MyShape(MyShape.Shape_Line);
            line2.GetLine().Stroke = System.Windows.Media.Brushes.Yellow;
            line2.GetLine().X1 = 70;
            line2.GetLine().Y1 = 10;
            line2.GetLine().X2 = 100;
            line2.GetLine().Y2 = 10;
            line2.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(line2);
            //左边的定位圆圈
            MyShape circle1 = new MyShape(MyShape.Shape_Ellipse);
            circle1.GetEllipse().Fill = System.Windows.Media.Brushes.Red;
            circle1.GetEllipse().StrokeThickness = 3;
            circle1.GetEllipse().Stroke = System.Windows.Media.Brushes.Yellow;
            circle1.GetEllipse().Width = 10;
            circle1.GetEllipse().Height = 10;
            Canvas.SetLeft(circle1.GetEllipse(), -5);
            Canvas.SetTop(circle1.GetEllipse(), 5);
            shapeSet.AddShape(circle1);
            //右边的定位圆圈
            MyShape circle2 = new MyShape(MyShape.Shape_Ellipse);
            circle2.GetEllipse().Fill = System.Windows.Media.Brushes.Red;
            circle2.GetEllipse().StrokeThickness = 3;
            circle2.GetEllipse().Stroke = System.Windows.Media.Brushes.Yellow;
            circle2.GetEllipse().Width = 10;
            circle2.GetEllipse().Height = 10;
            Canvas.SetLeft(circle2.GetEllipse(), 95);
            Canvas.SetTop(circle2.GetEllipse(), 5);
            shapeSet.AddShape(circle2);

            showingData = new ShowingData("" + 101230 + "ohm");
            ohmmeterWindow = new Ohmmeter(this);
            ohmmeterWindow.Show();
            showingData.SimpleData = "我是一只欧姆表";
        }
        public override bool DeletingCmd(bool IsFinal)
        {
            if (IsFinal)
            {
                CanbeClosed = true;
                ohmmeterWindow.Close();
                return true;
            }
            if (MessageBox.Show("确定要退出欧姆表吗？", 
                "询问", MessageBoxButton.YesNo, 
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CanbeClosed = true;
                ohmmeterWindow.Close();
                return true;
            }
            return false;
        }
        public override void ShowMeter()
        {
            ohmmeterWindow.Activate();
        }
        public override ElecFeature GetElecFeature()
        {
            OhmMeterElecFeature ohmMeterElecFeature
                 = new OhmMeterElecFeature();
            ohmMeterElecFeature.U = 5; //5V的电压
            ohmMeterElecFeature.showingData = showingData;
            return ohmMeterElecFeature;
        }
        class OhmMeterElecFeature : ElecFeature
        {
            public double U;
            private double Tsum = 0;
            private const double ShowT = 1e-3;
            private double Meter_R;
            public ShowingData showingData;
            public OhmMeterElecFeature() : base()
            {
                rC = Default_rC * 1e5;
            }

            public override double GetNext(double deltaT)
            {
                Tsum += deltaT;
                if (Tsum > ShowT)
                {
                    //MessageBox.Show("Show R!");
                    double deltaQ = rC * U - rQ;
                    double I = deltaQ / deltaT;
                    Meter_R = U / I - deltaT / rC;
                    Tsum = 0;
                    showingData.SimpleData = "" + Meter_R;
                }
                rQ = rC * U;
                return rQ;
            }
        }
    }
}
