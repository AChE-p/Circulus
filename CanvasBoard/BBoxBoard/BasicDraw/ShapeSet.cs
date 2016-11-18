using BBoxBoard.BasicDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
    }
}
