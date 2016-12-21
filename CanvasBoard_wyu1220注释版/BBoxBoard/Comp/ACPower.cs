using BBoxBoard.BasicDraw;
using BBoxBoard.Comp;
using BBoxBoard.Equipment;
using BBoxBoard.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

public enum Powermode { sine_wave,cosine_wave, square_wave, stw}
namespace BBoxBoard.Comp
{
    public class ACPower : ElecComp
    {
        public double  frequency;
        public double  pp_value;
        public Powermode  powermode;

        public ACPower(Powermode _powermode,double _frequency, double _pp_value ) :this()
        {
           frequency = _frequency;
           pp_value = _pp_value;
           powermode = _powermode;

        }
        public ACPower() : base()
        {
           
            pp_value = 0;
            frequency = 0;
            powermode =0;
        }
       
        public override void AddShapes()
        {
            //设置类型
            Comp = Comp_ACPower;
            /*//必须重新设置元件大小（已废弃）
            size.X = 100;
            size.Y = 20;*/
            //定义外部接口的位置
            RelativeInterface.Add(new IntPoint(0, 10)); //左端口
            RelativeInterface.Add(new IntPoint(100, 10)); //右端口
             //@d 这部分为交流电表的形状，到本函数结尾可重写
            //左边的导线
            MyShape line1 = new MyShape(MyShape.Shape_Line);
            line1.GetLine().Stroke = System.Windows.Media.Brushes.BlueViolet;
            line1.GetLine().X1 = 0;
            line1.GetLine().Y1 = 10;
            line1.GetLine().X2 = 30;
            line1.GetLine().Y2 = 10;
            line1.GetLine().StrokeThickness = 5;
            shapeSet.AddShape(line1);
            //中间的长方形
            MyShape rectangle = new MyShape(MyShape.Shape_Rectangle);
            rectangle.GetRectangle().Fill = System.Windows.Media.Brushes.Bisque;
            rectangle.GetRectangle().Width = 40;
            rectangle.GetRectangle().Height = 20;
            Canvas.SetLeft(rectangle.GetRectangle(), 30);
            Canvas.SetTop(rectangle.GetRectangle(), 0);
            shapeSet.AddShape(rectangle);
            //右边的导线
            MyShape line2 = new MyShape(MyShape.Shape_Line);
            line2.GetLine().Stroke = System.Windows.Media.Brushes.Navy;
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
            circle1.GetEllipse().Stroke = System.Windows.Media.Brushes.Navy;
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

        public override ElecFeature GetElecFeature()
        {
            ACPowerElecFeature acpowerelecfeature = new ACPowerElecFeature();
            acpowerelecfeature.frequency = frequency;
            acpowerelecfeature.pp_value = pp_value;
            acpowerelecfeature.powermode = powermode;
            acpowerelecfeature.CommitAttr();
            return acpowerelecfeature;

        }
        

        class ACPowerElecFeature : ElecFeature
        {
            public double frequency;
            public double pp_value;
            public Powermode powermode;

            double period;
            double Tsum;
            public ACPowerElecFeature() : base()
            {
                rC = Default_rC * 1e5;
            }
            public void CommitAttr()
            {
                Tsum = 0;
                period = 1 / frequency;
            }

            public override double GetNext(double deltaT)
            {
                Tsum += deltaT;
                if (Tsum >= period) Tsum -= period;
                double U = 0;
                switch (powermode)
                {
                    case Powermode.cosine_wave:
                        U = pp_value * Math.Cos(2 * Math.PI * frequency * Tsum);
                        break;

                    case Powermode.sine_wave:
                        U = pp_value * Math.Sin(2 * Math.PI * frequency * Tsum);
                        break;

                    case Powermode.square_wave:

                        if (Tsum <= 0.5 * period) U = pp_value;
                        else U = -pp_value;
                        break;
                    case Powermode.stw:
                        if (Tsum <= 0.25 * period)
                            U = 4 * Tsum * pp_value / period;
                        else if (Tsum > 0.25 * period && Tsum <= 0.75 * period)
                            U = -(4 * (Tsum - 0.5 * period) * pp_value) / period;
                        else
                            U = 4 * (Tsum - period) * pp_value / period;
                        break;
                }
                rQ = rC * U;
                return rQ;
            }

        }

       
    }
}

