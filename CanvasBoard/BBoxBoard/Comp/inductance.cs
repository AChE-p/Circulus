using BBoxBoard.BasicDraw;
using BBoxBoard.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BBoxBoard.Comp
{
    public class Inductance : ElecComp
    {
        public Inductance() : base() { }

        public override void AddShapes()
        {
            //设置类型
            Comp = Comp_Inductance;
            /*//必须重新设置元件大小（已废弃）
            size.X = 100;
            size.Y = 20;*/
            //定义外部接口的位置
            RelativeInterface.Add(new IntPoint(0, 10)); //左端口
            RelativeInterface.Add(new IntPoint(100, 10)); //右端口
            //左边的导线
            MyShape Shape_line1 = new MyShape(MyShape.Shape_Line);
            Line line1 = Shape_line1.GetLine();
            line1.Stroke = System.Windows.Media.Brushes.Red;
            line1.X1 = 0;
            line1.Y1 = 10;
            line1.X2 = 20;
            line1.Y2 = 10;
            line1.StrokeThickness = 5;
            shapeSet.AddShape(Shape_line1);
            //中间的不封闭折线段
            MyShape Shape_polyline = new MyShape(MyShape.Shape_Ployline);
            Polyline polyline = Shape_polyline.GetPolyline();
            polyline.StrokeThickness = 5;
            polyline.Stroke = System.Windows.Media.Brushes.Black;
            PointCollection pointC = new PointCollection();
            AddPoints(pointC);
            polyline.Points = pointC;
            shapeSet.AddShape(Shape_polyline);
            //右边的导线
            MyShape Shape_line2 = new MyShape(MyShape.Shape_Line);
            Line line2 = Shape_line2.GetLine();
            line2.Stroke = System.Windows.Media.Brushes.Red;
            line2.X1 = 80;
            line2.Y1 = 10;
            line2.X2 = 100;
            line2.Y2 = 10;
            line2.StrokeThickness = 5;
            shapeSet.AddShape(Shape_line2);
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
        }
        private void AddPoints(PointCollection pointC)
        {
            pointC.Add(new Point(20, 10.0));
            pointC.Add(new Point(21, 8.43566));
            pointC.Add(new Point(22, 6.90983));
            pointC.Add(new Point(23, 5.4601));
            pointC.Add(new Point(24, 4.12215));
            pointC.Add(new Point(25, 2.92893));
            pointC.Add(new Point(26, 1.90983));
            pointC.Add(new Point(27, 1.08993));
            pointC.Add(new Point(28, 0.489435));
            pointC.Add(new Point(29, 0.123117));
            pointC.Add(new Point(30, 0.0));
            pointC.Add(new Point(31, 0.123117));
            pointC.Add(new Point(32, 0.489435));
            pointC.Add(new Point(33, 1.08993));
            pointC.Add(new Point(34, 1.90983));
            pointC.Add(new Point(35, 2.92893));
            pointC.Add(new Point(36, 4.12215));
            pointC.Add(new Point(37, 5.4601));
            pointC.Add(new Point(38, 6.90983));
            pointC.Add(new Point(39, 8.43566));
            pointC.Add(new Point(40, 10.0));
            pointC.Add(new Point(41, 8.43566));
            pointC.Add(new Point(42, 6.90983));
            pointC.Add(new Point(43, 5.4601));
            pointC.Add(new Point(44, 4.12215));
            pointC.Add(new Point(45, 2.92893));
            pointC.Add(new Point(46, 1.90983));
            pointC.Add(new Point(47, 1.08993));
            pointC.Add(new Point(48, 0.489435));
            pointC.Add(new Point(49, 0.123117));
            pointC.Add(new Point(50, 0.0));
            pointC.Add(new Point(51, 0.123117));
            pointC.Add(new Point(52, 0.489435));
            pointC.Add(new Point(53, 1.08993));
            pointC.Add(new Point(54, 1.90983));
            pointC.Add(new Point(55, 2.92893));
            pointC.Add(new Point(56, 4.12215));
            pointC.Add(new Point(57, 5.4601));
            pointC.Add(new Point(58, 6.90983));
            pointC.Add(new Point(59, 8.43566));
            pointC.Add(new Point(60, 10.0));
            pointC.Add(new Point(61, 8.43566));
            pointC.Add(new Point(62, 6.90983));
            pointC.Add(new Point(63, 5.4601));
            pointC.Add(new Point(64, 4.12215));
            pointC.Add(new Point(65, 2.92893));
            pointC.Add(new Point(66, 1.90983));
            pointC.Add(new Point(67, 1.08993));
            pointC.Add(new Point(68, 0.489435));
            pointC.Add(new Point(69, 0.123117));
            pointC.Add(new Point(70, 0.0));
            pointC.Add(new Point(71, 0.123117));
            pointC.Add(new Point(72, 0.489435));
            pointC.Add(new Point(73, 1.08993));
            pointC.Add(new Point(74, 1.90983));
            pointC.Add(new Point(75, 2.92893));
            pointC.Add(new Point(76, 4.12215));
            pointC.Add(new Point(77, 5.4601));
            pointC.Add(new Point(78, 6.90983));
            pointC.Add(new Point(79, 8.43566));
            pointC.Add(new Point(80, 10.0));
        }

        public override ElecFeature GetElecFeature()
        {
            return new InductanceElecFeature(25);
        }

        class InductanceElecFeature : ElecFeature
        {
            private double I;
            private double L;

            public InductanceElecFeature(double L_) : base()
            {
                I = 0;
                L = L_;
            }
            public override double GetNext(double deltaT)
            {
                double U = rQ / rC;
                I += U / L * deltaT;
                rQ -= I * deltaT;
                return rQ;
            }
        }
    }
}
