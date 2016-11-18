using BBoxBoard.BasicDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BBoxBoard.Comp
{
    public class Capacity : ElecComp
    {
        public Capacity() : base() { }

        public override void AddShapes()
        {
            //必须重新设置元件大小
            size.X = 60;
            size.Y = 40;
            //左边的导线
            MyShape line1 = new MyShape(MyShape.Shape_Line);
            line1.GetLine().Stroke = System.Windows.Media.Brushes.Red;
            line1.GetLine().X1 = 0;
            line1.GetLine().Y1 = 20;
            line1.GetLine().X2 = 30;
            line1.GetLine().Y2 = 20;
            line1.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(line1);
            //右边的导线
            MyShape line2 = new MyShape(MyShape.Shape_Line);
            line2.GetLine().Stroke = System.Windows.Media.Brushes.Red;
            line2.GetLine().X1 = 40;
            line2.GetLine().Y1 = 20;
            line2.GetLine().X2 = 70;
            line2.GetLine().Y2 = 20;
            line2.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(line2);
            //左极板
            MyShape lineleft = new MyShape(MyShape.Shape_Line);
            lineleft.GetLine().Stroke = System.Windows.Media.Brushes.Black;
            lineleft.GetLine().X1 = 30;
            lineleft.GetLine().Y1 = 0;
            lineleft.GetLine().X2 = 30;
            lineleft.GetLine().Y2 = 40;
            lineleft.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(lineleft);
            //右极板
            MyShape lineRight = new MyShape(MyShape.Shape_Line);
            lineRight.GetLine().Stroke = System.Windows.Media.Brushes.Black;
            lineRight.GetLine().X1 = 40;
            lineRight.GetLine().Y1 = 0;
            lineRight.GetLine().X2 = 40;
            lineRight.GetLine().Y2 = 40;
            lineRight.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(lineRight);
            //左边的定位圆圈
            MyShape circle1 = new MyShape(MyShape.Shape_Ellipse);
            circle1.GetEllipse().Fill = System.Windows.Media.Brushes.Red;
            circle1.GetEllipse().StrokeThickness = 3;
            circle1.GetEllipse().Stroke = System.Windows.Media.Brushes.Yellow;
            circle1.GetEllipse().Width = 10;
            circle1.GetEllipse().Height = 10;
            Canvas.SetLeft(circle1.GetEllipse(), -5);
            Canvas.SetTop(circle1.GetEllipse(), 15);
            shapeSet.AddShape(circle1);
            //右边的定位圆圈
            MyShape circle2 = new MyShape(MyShape.Shape_Ellipse);
            circle2.GetEllipse().Fill = System.Windows.Media.Brushes.Red;
            circle2.GetEllipse().StrokeThickness = 3;
            circle2.GetEllipse().Stroke = System.Windows.Media.Brushes.Yellow;
            circle2.GetEllipse().Width = 10;
            circle2.GetEllipse().Height = 10;
            Canvas.SetLeft(circle2.GetEllipse(), 65);
            Canvas.SetTop(circle2.GetEllipse(), 15);
            shapeSet.AddShape(circle2);
        }
    }
}
