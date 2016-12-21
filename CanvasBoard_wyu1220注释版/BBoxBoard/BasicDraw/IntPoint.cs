using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BBoxBoard.BasicDraw
{
    public class IntPoint
    {
        public int X;
        public int Y;
        public IntPoint()
        {
            X = 0;
            Y = 0;
        }
        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        public IntPoint(Point point)
        {
            X = (int)point.X;
            Y = (int)point.Y;
        }
        public IntPoint(IntPoint point)
        {
            X = point.X;
            Y = point.Y;
        }
        public override bool Equals(object obj)
        {
            IntPoint A = (IntPoint)obj;
            return (A.X == X && A.Y == Y);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public IntPoint GetGROUD()
        {
            return new IntPoint(-1000, -1000);
        }
    }
}
