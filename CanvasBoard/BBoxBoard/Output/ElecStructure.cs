using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBoxBoard.Output
{
    public class ElecStructure
    {
        public int LeftFoot;
        public int RightFoot;
        public double rC;
        public int GroupIndex;

        public bool IsLinkToFoot(int x)
        {
            return LeftFoot == x || RightFoot == x;
        }

        public int GetOtherFoot(int x)
        {
            if (x == LeftFoot) return RightFoot;
            if (x == RightFoot) return LeftFoot;
            return -1;
        }

        public ElecStructure(int Left, int Right, double RC)
        {
            LeftFoot = Left;
            RightFoot = Right;
            rC = RC;
            GroupIndex = -1;
        }

        public override string ToString()
        {
            return "Structure(" + LeftFoot + "," + RightFoot + 
                "," + rC + ")"; 
        }

        public int IsBetweenFoot(int Left, int Right)
        {
            if (Left == LeftFoot && Right == RightFoot) return 1;
            if (Left == RightFoot && Right == LeftFoot) return -1;
            return 0;
        }

        public int GetFoot(int Foot)
        {
            if (Foot == LeftFoot) return 1;
            if (Foot == RightFoot) return -1;
            return 0;
        }
    }
}
