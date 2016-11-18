using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BBoxBoard.BasicDraw
{
    public class MyShape
    {
        public const int Shape_Line = 1;
        public const int Shape_Rectangle = 2;
        public const int Shape_Ellipse = 3;

        Shape shape;
        int WHAT;

        public MyShape(int WHAT_)
        {
            WHAT = WHAT_;
            switch (WHAT)
            {
                case Shape_Line:
                    shape = new Line();
                    break;
                case Shape_Rectangle:
                    shape = new Rectangle();
                    break;
                case Shape_Ellipse:
                    shape = new Ellipse();
                    break;
            }
        }

        public Line GetLine()
        {
            if (WHAT == Shape_Line) return (Line)shape;
            return null;
        }

        public Rectangle GetRectangle()
        {
            if (WHAT == Shape_Rectangle) return (Rectangle)shape;
            return null;
        }

        public Ellipse GetEllipse()
        {
            if (WHAT == Shape_Ellipse) return (Ellipse)shape;
            return null;
        }

        public void ShowAt(Canvas canvas, IntPoint point)
        {
            switch (WHAT)
            {
                case Shape_Line:
                    Line line = (Line)shape;
                    line.X1 += point.X;
                    line.X2 += point.X;
                    line.Y1 += point.Y;
                    line.Y2 += point.Y;
                    canvas.Children.Add(line);
                    break;
                case Shape_Rectangle:
                    Rectangle rectangle = (Rectangle)shape;
                    double X0 = Canvas.GetLeft(rectangle);
                    double Y0 = Canvas.GetTop(rectangle);
                    Canvas.SetLeft(rectangle, point.X + X0);
                    Canvas.SetTop(rectangle, point.Y + Y0);
                    canvas.Children.Add(rectangle);
                    break;
                case Shape_Ellipse:
                    Ellipse ellipse = (Ellipse)shape;
                    double X1 = Canvas.GetLeft(ellipse);
                    double Y1 = Canvas.GetTop(ellipse);
                    Canvas.SetLeft(ellipse, point.X + X1);
                    Canvas.SetTop(ellipse, point.Y + Y1);
                    canvas.Children.Add(ellipse);
                    break;
            }
        }

        public void Move(int deltaX, int deltaY)
        {
            switch (WHAT)
            {
                case Shape_Line:
                    Line line = (Line)shape;
                    line.X1 += deltaX;
                    line.X2 += deltaX;
                    line.Y1 += deltaY;
                    line.Y2 += deltaY;
                    break;
                case Shape_Rectangle:
                    Rectangle rectangle = (Rectangle)shape;
                    double X = Canvas.GetLeft(rectangle);
                    double Y = Canvas.GetTop(rectangle);
                    //MessageBox.Show("Rect X:" + X + " Y:" + Y);
                    Canvas.SetLeft(rectangle, X + deltaX);
                    Canvas.SetTop(rectangle, Y + deltaY);
                    break;
                case Shape_Ellipse:
                    Ellipse ellipse = (Ellipse)shape;
                    double X1 = Canvas.GetLeft(ellipse);
                    double Y1 = Canvas.GetTop(ellipse);
                    Canvas.SetLeft(ellipse, X1 + deltaX);
                    Canvas.SetTop(ellipse, Y1 + deltaY);
                    break;
            }
        }

        public void DeleteFrom(Canvas canvas)
        {
            canvas.Children.Remove(shape);
        }

        public void RotateLeftAround(IntPoint center)
        {
            switch (WHAT)
            {
                case Shape_Line:
                    Line line = (Line)shape;
                    double deltaX1 = line.X1 - center.X;
                    double deltaY1 = line.Y1 - center.Y;
                    line.X1 = center.X + deltaY1;
                    line.Y1 = center.Y - deltaX1;
                    double deltaX2 = line.X2 - center.X;
                    double deltaY2 = line.Y2 - center.Y;
                    line.X2 = center.X + deltaY2;
                    line.Y2 = center.Y - deltaX2;
                    break;
                case Shape_Rectangle:
                    Rectangle rectangle = (Rectangle)shape;
                    double CX = Canvas.GetLeft(rectangle) + rectangle.Width/2;
                    double CY = Canvas.GetTop(rectangle) + rectangle.Height/2;
                    double deltaX_Rect = CX - center.X;
                    double deltaY_Rect = CY - center.Y;
                    CX = center.X + deltaY_Rect;
                    CY = center.Y - deltaX_Rect;
                    double RectWidth = rectangle.Width;
                    double RectHeight = rectangle.Height;
                    rectangle.Width = RectHeight;
                    rectangle.Height = RectWidth;
                    Canvas.SetLeft(rectangle, CX - RectHeight / 2);
                    Canvas.SetTop(rectangle, CY - RectWidth / 2);
                    break;
                case Shape_Ellipse:
                    Ellipse ellipse = (Ellipse)shape;
                    double CX2 = Canvas.GetLeft(ellipse) + ellipse.Width / 2;
                    double CY2 = Canvas.GetTop(ellipse) + ellipse.Height / 2;
                    double deltaX_Elli = CX2 - center.X;
                    double deltaY_Elli = CY2 - center.Y;
                    CX2 = center.X + deltaY_Elli;
                    CY2 = center.Y - deltaX_Elli;
                    double ElliWidth = ellipse.Width;
                    double ElliHeight = ellipse.Height;
                    ellipse.Width = ElliHeight;
                    ellipse.Height = ElliWidth;
                    Canvas.SetLeft(ellipse, CX2 - ElliHeight / 2);
                    Canvas.SetTop(ellipse, CY2 - ElliWidth / 2);
                    break;
            }
        }
    }
}
