using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBoxBoard.Output
{
    public class ElecFeature
    {
        public static double Default_rC = 1e-6;
        public int LeftFoot;
        public int RightFoot;
        public double rC;
        public double rQ;
        public BriefElecComp father;

        public ElecFeature()
        {
            rC = Default_rC;
            rQ = 0;
        }

        public virtual double GetNext(double deltaT)
        {
            return rQ;
        }

        public void SetFoot(int Left, int Right)
        {
            LeftFoot = Left;
            RightFoot = Right;
        }

        public void SetFather(BriefElecComp Father)
        {
            father = Father;
        }
        public override string ToString()
        {
            return "ElecFeature:(" + LeftFoot + "," + RightFoot + "), father:" + father;
        }
        public ElecStructure GetStructure()
        {
            return new ElecStructure(LeftFoot, RightFoot, rC);
        }
    }
}
