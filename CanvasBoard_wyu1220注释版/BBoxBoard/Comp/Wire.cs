using BBoxBoard.BasicDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBoxBoard.Comp
{
    public class Wire : ElecComp
    {
        public Wire() : base() { }

        public override void AddShapes()
        {
            //设置类型
            Comp = Comp_Wire;
            //指定这是电线
            IsWire = true;
            //定义外部接口的位置
            IntPoint A = new IntPoint(0, 0);
            IntPoint B = new IntPoint(50, 0);
            RelativeInterface.Add(A); //左端口
            RelativeInterface.Add(B); //右端口
            InitiateWireBetween(A, B);
            //必须重新设置元件大小
            int sizeX = Math.Abs(A.X - B.X);
            int sizeY = Math.Abs(A.Y - B.Y);
            //（已废弃）size.X = (sizeX==0)?10:sizeX;
            //size.Y = (sizeY==0)?10:sizeY;
        }
    }
}
