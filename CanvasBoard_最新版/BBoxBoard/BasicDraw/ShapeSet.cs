using BBoxBoard.BasicDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BBoxBoard.BasicDraw
{
    public class ShapeSet
    {
        public List<MyShape> arr;

        public ShapeSet()
        {
            arr = new List<MyShape>();
        }

        public void AddShape(MyShape myShape)
        {
            arr.Add(myShape);
        }

        public void RemoveAllFrom(Canvas canvas)
        {
            foreach (MyShape myShape in arr)
            {
                myShape.DeleteFrom(canvas);
            }
        }
        
        public void RotateLeftAround(IntPoint center)
        {
            foreach (MyShape myShape in arr)
            {
                myShape.RotateLeftAround(center);
            }
        }

        public bool IsPossibleWire()
        {
            if (arr.Count != 4) return false;
            if (arr[0].GetLine() == null) return false;
            if (arr[1].GetLine() == null) return false;
            if (arr[2].GetEllipse() == null) return false;
            if (arr[3].GetEllipse() == null) return false;
            return true;
        }

        public override string ToString()
        {
            String A = "";
            foreach (MyShape myShape in arr)
            {
                A += "," + myShape;
            }
            return A;
        }

        public bool HasShape(Shape shape)
        {
            foreach (MyShape myShape in arr)
            {
                if (myShape.shape == shape) return true;
            }
            return false;
        }
    }
}
