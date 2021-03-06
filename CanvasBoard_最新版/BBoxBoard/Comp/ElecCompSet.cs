﻿using BBoxBoard.BasicDraw;
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
    public class ElecCompSet
    {
        List<ElecComp> elecSet;
        public ElecComp pressedElecComp;
        private int pressedIndex;

        public ElecCompSet()
        {
            elecSet = new List<ElecComp>();
            pressedElecComp = null;
        }

        public bool FoundPressedElecComp(IntPoint point)
        {
            for (int i=0; i<elecSet.Count; i++)
            {
                if (elecSet[i].IfInRegion(point))
                {
                    pressedElecComp = elecSet[i];
                    pressedIndex = i;
                    return true;
                }
            }
            return false;
        }

        public bool FoundPressedElecComp(IInputElement targetElement)
        {
            if (targetElement is Shape)
            {
                Shape shape = (Shape)targetElement;
                for (int i = 0; i < elecSet.Count; i++)
                {
                    if (elecSet[i].HasShape(shape))
                    {
                        pressedElecComp = elecSet[i];
                        pressedIndex = i;
                        return true;
                    }
                }
            }
            return false;
        }

        public ElecComp GetPressedElecComp(IInputElement targetElement)
        {
            Shape shape = (Shape)targetElement;
            for (int i = 0; i < elecSet.Count; i++)
            {
                if (elecSet[i].HasShape(shape))
                {
                    return elecSet[i];
                }
            }
            return null;
        }

        public void ReleaseElecComp()
        {
            pressedElecComp = null;
        }
        
        public void AddCompAndShow(ElecComp elecComp, Canvas canvas)
        {
            elecSet.Add(elecComp);
            elecComp.ShowIn(canvas);
        }
        

        public void DeleteNowPressed(Canvas canvas)
        {
            if (pressedElecComp != null && pressedElecComp.DeletingCmd(false))
            {
                pressedElecComp.RemoveAllFrom(canvas);
                elecSet.Remove(pressedElecComp);
            }
        }

        public override String ToString()
        {
            String A = "组件数：" + elecSet.Count + '\n';
            for (int i=0; i<elecSet.Count; i++)
            {
                A += i + ":" + elecSet[i].GetInfo() + '\n';
            }
            return A;
        }

        public void OutputInto(List<BriefElecComp> A)
        {
            foreach (ElecComp elecComp in elecSet)
            {
                A.Add(elecComp.GetBriefElecComp());
            }
        }

        public void CloseAll(Canvas canvas)
        {
            foreach (ElecComp x in elecSet)
            {
                if (x.DeletingCmd(true))
                {
                    x.RemoveAllFrom(canvas);
                    //elecSet.Remove(x);
                }
            }
        }
        //@m这里是存档的函数
        public String PrintSavingAll()
        {
            String Str = "";
            foreach (ElecComp x in elecSet)
            {
                Str += x.PrintSaving() + "\r\n";
            }
            return Str;
        }
        //@m 这里是存档的调取
        public ElecComp AddFromRecord(int Comp, Canvas Mycanvas)
        {
            ElecComp elecComp = null;
            switch (Comp)
            {
                case ElecComp.Comp_Wire:
                    Wire w = new Wire();
                    AddCompAndShow(w, Mycanvas);
                    elecComp = w;
                    break;
                case ElecComp.Comp_Resistance:
                    Resistance r = new Resistance();
                    AddCompAndShow(r, Mycanvas);
                    elecComp = r;
                    break;
                case ElecComp.Comp_Capacity:
                    Capacity c = new Capacity();
                    AddCompAndShow(c, Mycanvas);
                    elecComp = c;
                    break;
                case ElecComp.Comp_Inductance:
                    Inductance i = new Inductance();
                    AddCompAndShow(i, Mycanvas);
                    elecComp = i;
                    break;
                case ElecComp.Comp_Power:
                    Power p = new Power();
                    AddCompAndShow(p, Mycanvas);
                    elecComp = p;
                    break;
                case ElecComp.Comp_ACPower:
                    ACPower acp = new ACPower();
                    AddCompAndShow(acp, Mycanvas);
                    elecComp = acp;
                    break;
                    //@m这里需要补充其他元件的存档调取
                    //@m在补充完所有元件的attribute和 handleattri 之后 记得调取的时候要一并对应调取
            }
            return elecComp;
        }
        
        public void ShowAllMeter()
        {
            foreach (ElecComp x in elecSet)
            {
                x.ShowMeter();
            }
        }
    }
}
