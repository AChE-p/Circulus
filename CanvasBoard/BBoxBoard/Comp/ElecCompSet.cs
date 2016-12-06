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
    }
}
