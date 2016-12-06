using BBoxBoard.BasicDraw;
using BBoxBoard.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BBoxBoard.Comp
{
    public abstract class ElecComp
    {
        public const int State_Move = 0;
        public const int State_AdjRight = 1;
        public const int Comp_NULL = -1;
        public const int Comp_Wire = 0;
        public const int Comp_Resistance = 1;
        public const int Comp_Capacity = 2;
        public const int Comp_Inductance = 3;
        public const int Comp_OhmMeter = 4;
        public const int Comp_VoltMeter = 5;
        public const int Comp_IMeter = 6;
        public const int Comp_Ground = 7;
        public const int Comp_Probe = 8;

        protected ShapeSet shapeSet;
        protected IntPoint XYPoint;
        protected IntPoint size;
        protected Canvas canvas;
        protected List<IntPoint> RelativeInterface;
        protected int RotatedState = 0;
        public int Comp;
        public bool IsWire;
        public int State;

        public ElecComp()
        {
            Init();
        }

        protected void Init()
        {
            Comp = -1;
            shapeSet = new ShapeSet();
            XYPoint = new IntPoint(0, 0);
            size = new IntPoint(MainWindow.GridLen, MainWindow.GridLen);
            RelativeInterface = new List<IntPoint>();
            State = State_Move;
            IsWire = false;
            AddShapes();
        }

        public virtual void AddShapes() { }
        public virtual ElecFeature GetElecFeature() { return new ElecFeature(); }
        public virtual bool DeletingCmd(bool IsFinal) { return true; }

        public void ShowIn(Canvas canvas_)
        {
            canvas = canvas_;
            foreach (MyShape myShape in shapeSet.arr) {
                myShape.ShowAt(canvas, XYPoint);
            }
        }
        public void SetPosition(int X, int Y)
        {
            XYPoint.X = X;
            XYPoint.Y = Y;
        }
        public void Move(int deltaX, int deltaY)
        {
            switch (State)
            {
                case State_Move:
                    XYPoint.X += deltaX;
                    XYPoint.Y += deltaY;
                    MoveShapeSet(deltaX, deltaY);
                    break;
                case State_AdjRight:
                    AdjRight(deltaX, deltaY);
                    break;
            }
        }
        private void MoveShapeSet(int deltaX, int deltaY)
        {
            foreach (MyShape myShape in shapeSet.arr)
            {
                myShape.Move(deltaX, deltaY);
            }
        }
        public virtual void AdjRight(int deltaX, int deltaY)
        {
            if (shapeSet.IsPossibleWire())
            {
                Line line1 = shapeSet.arr[0].GetLine();
                Line line2 = shapeSet.arr[1].GetLine();
                DrawBetween(new IntPoint((int)line1.X1, (int)line1.Y1),
                    new IntPoint((int)line2.X2 + deltaX, (int)line2.Y2 + deltaY));
            }
        }
        public bool IfInRegion(IntPoint P0)
        {
            if (size.X > 0)
            {
                if (P0.X < XYPoint.X || P0.X > XYPoint.X + size.X) return false;
            }
            else
            {
                if (P0.X > XYPoint.X || P0.X < XYPoint.X + size.X) return false;
            }
            if (size.Y > 0)
            {
                if (P0.Y < XYPoint.Y || P0.Y > XYPoint.Y + size.Y) return false;
            }
            else
            {
                if (P0.Y > XYPoint.Y || P0.Y < XYPoint.Y + size.Y) return false;
            }
            return true;
        }
        public bool HasShape(Shape shape)
        {
            return shapeSet.HasShape(shape);
        }
        public void RemoveAllFrom(Canvas canvas)
        {
            shapeSet.RemoveAllFrom(canvas);
        }
        public void RotateLeft()
        {
            if (IsWire) return;
            RotatedState++;
            RotatedState %= 4;
            shapeSet.RotateLeftAround(XYPoint);
            foreach (IntPoint intPoint in RelativeInterface)
            {
                RotatePointAround(intPoint);
            }
            int sizeX = size.X;
            int sizeY = size.Y;
            size.X = sizeY;
            size.Y = -sizeX;
        }
        private void RotatePointAround(IntPoint itface)
        {
            int deltaX = itface.X;
            int deltaY = itface.Y;
            itface.X = deltaY;
            itface.Y = -deltaX;
        }
        protected void InitiateWireBetween(IntPoint A, IntPoint B)
        {
            //左边的导线
            MyShape line1 = new MyShape(MyShape.Shape_Line);
            line1.GetLine().Stroke = System.Windows.Media.Brushes.Blue;
            line1.GetLine().X1 = A.X;
            line1.GetLine().Y1 = A.Y;
            line1.GetLine().X2 = 0;
            line1.GetLine().Y2 = 0;
            line1.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(line1);
            //右边的导线
            MyShape line2 = new MyShape(MyShape.Shape_Line);
            line2.GetLine().Stroke = System.Windows.Media.Brushes.Blue;
            line2.GetLine().X1 = 0;
            line2.GetLine().Y1 = 0;
            line2.GetLine().X2 = B.X;
            line2.GetLine().Y2 = B.Y;
            line2.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(line2);
            //左边的定位圆圈
            MyShape circle1 = new MyShape(MyShape.Shape_Ellipse);
            circle1.GetEllipse().Fill = System.Windows.Media.Brushes.Red;
            circle1.GetEllipse().StrokeThickness = 3;
            circle1.GetEllipse().Stroke = System.Windows.Media.Brushes.Yellow;
            circle1.GetEllipse().Width = 10;
            circle1.GetEllipse().Height = 10;
            Canvas.SetLeft(circle1.GetEllipse(), A.X - 5);
            Canvas.SetTop(circle1.GetEllipse(), A.Y - 5);
            shapeSet.AddShape(circle1);
            //右边的定位圆圈
            MyShape circle2 = new MyShape(MyShape.Shape_Ellipse);
            circle2.GetEllipse().Fill = System.Windows.Media.Brushes.Red;
            circle2.GetEllipse().StrokeThickness = 3;
            circle2.GetEllipse().Stroke = System.Windows.Media.Brushes.Yellow;
            circle2.GetEllipse().Width = 10;
            circle2.GetEllipse().Height = 10;
            Canvas.SetLeft(circle2.GetEllipse(), B.X - 5);
            Canvas.SetTop(circle2.GetEllipse(), B.Y - 5);
            shapeSet.AddShape(circle2);
            //按照折线标准绘图
            DrawBetween(A, B);
        }
        private void DrawBetween(IntPoint A, IntPoint B)
        {
            if (shapeSet.IsPossibleWire())
            {
                RelativeInterface[0] = new IntPoint(0, 0);
                RelativeInterface[1] = new IntPoint(B.X - A.X, B.Y - A.Y);
                double sizeX = B.X - A.X;
                double sizeY = B.Y - A.Y;
                size.X = (int)((sizeX == 0) ? 10 : sizeX);
                size.Y = (int)((sizeY == 0) ? 10 : sizeY);
                Line line1 = shapeSet.arr[0].GetLine();
                Line line2 = shapeSet.arr[1].GetLine();
                Ellipse point1 = shapeSet.arr[2].GetEllipse();
                Ellipse point2 = shapeSet.arr[3].GetEllipse();
                IntPoint C = new IntPoint(); //用来记录中间转折点的信息
                double deltaX = B.X - A.X;
                double deltaY = B.Y - A.Y;
                Canvas.SetLeft(point1, A.X - point1.Width / 2);
                Canvas.SetTop(point1, A.Y - point1.Height / 2);
                Canvas.SetLeft(point2, B.X - point2.Width / 2);
                Canvas.SetTop(point2, B.Y - point2.Height / 2);
                if (Math.Abs(deltaX) == Math.Abs(deltaY)) //这里面包含了均为0的情况
                {
                    //在一条斜线上，不需要中间转折
                    line1.X1 = A.X;
                    line1.Y1 = A.Y;
                    line1.X2 = A.X;
                    line1.Y2 = A.Y;
                    line2.X1 = A.X;
                    line2.Y1 = A.Y;
                    line2.X2 = B.X;
                    line2.Y2 = B.Y;
                    return;
                }
                //不在一条斜线上，需要中间转折
                if (deltaX == 0 || Math.Abs(deltaY/deltaX) > 1)
                {
                    //先竖线，再折线
                    C.X = A.X;
                    if (deltaY > 0)
                    {
                        C.Y = (int)(A.Y + deltaY - Math.Abs(deltaX));
                    }
                    else
                    {
                        C.Y = (int)(A.Y + deltaY + Math.Abs(deltaX));
                    }
                }
                else
                {
                    //先横线，再折线
                    C.Y = A.Y;
                    if (deltaX > 0)
                    {
                        C.X = (int)(A.X + deltaX - Math.Abs(deltaY));
                    }
                    else
                    {
                        C.X = (int)(A.X + deltaX + Math.Abs(deltaY));
                    }
                }
                //绘制两条直线
                line1.X1 = A.X;
                line1.Y1 = A.Y;
                line1.X2 = C.X;
                line1.Y2 = C.Y;
                line2.X1 = C.X;
                line2.Y1 = C.Y;
                line2.X2 = B.X;
                line2.Y2 = B.Y;
            }
        }

        public String GetInfo()
        {
            String A = "";
            switch(Comp)
            {
                case Comp_Wire:
                    A += "Wire";
                    break;
                case Comp_Resistance:
                    A += "Resistance";
                    break;
                case Comp_Capacity:
                    A += "Capacity";
                    break;
                default:
                    return "NULL";
            }
            A += ",center=(" + XYPoint.X + "," + XYPoint.Y + ")";
            for (int i=0; i<RelativeInterface.Count; i++)
            {
                A += ",itfc(" + RelativeInterface[i].X + "," 
                    + RelativeInterface[i].Y + ")";
            }
            A += shapeSet;
            return A;
        }

        public virtual BriefElecComp GetBriefElecComp()
        {
            List<IntPoint> A = new List<IntPoint>();
            foreach (IntPoint p in RelativeInterface)
            {
                A.Add(new IntPoint(p.X + XYPoint.X, p.Y + XYPoint.Y));
            }
            return new BriefElecComp(Comp, A, this);
        }
    }
}
