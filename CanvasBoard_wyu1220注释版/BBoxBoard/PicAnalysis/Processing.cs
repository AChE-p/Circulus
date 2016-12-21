using BBoxBoard.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BBoxBoard.PicAnalysis
{
    public class Processing
    {
        double[,] A;
        List<BriefElecComp> Arr;
        List<ElecFeature> ElecArr;
        double deltaT;
        double lastingT;

        public Processing(List<BriefElecComp> Arr_, double deltaT_, 
            double lastingT_, Core core)
        {
            deltaT = deltaT_;
            lastingT = lastingT_;
            ElecArr = new List<ElecFeature>();
            Arr = Arr_;
            SimplifiedPic simplifiedPic = new SimplifiedPic(Arr, core);
            ElecArr = simplifiedPic.FeatureArr;
            A = simplifiedPic.A; //获得变换矩阵
            double T = 0;
            //int Count = 0;
            double ProgdeltaT = lastingT / 100;

            core.SyncProgess(0, "模拟中...(s)0");
            while (T < /*3e-4*/ lastingT)
            {
                if (T > ProgdeltaT) //更新进度条
                {
                    core.SyncProgess((int)(100*T / lastingT), "模拟中...(s)" + 
                        T);
                    ProgdeltaT += lastingT / 100;
                }

                double[] QpArr = new double[ElecArr.Count];
                for (int i = 0; i < ElecArr.Count; i++)
                {
                    QpArr[i] = ElecArr[i].GetNext(deltaT);
                    //MessageBox.Show("QpArr[" + i + "]=" + QpArr[i]);
                }
                for (int i = 0; i < ElecArr.Count; i++)
                {
                    double rQ = 0;
                    for (int j = 0; j < ElecArr.Count; j++)
                    {
                        rQ += A[i, j] * QpArr[j];
                    }
                    ElecArr[i].rQ = rQ;
                }
                /*
                if (Count == 1000)
                {
                    Count = 0;
                    for (int i = 0; i < ElecArr.Count; i++)
                    {
                        //MessageBox.Show("Point:" + 1024 * T / 1e-2 + ", " +
                        //    ElecArr[i].GetrQ());
                        /*pfArr[i].Segments.Add(new LineSegment(
                            new Point(1024 * T / 3e-4,
                            -ElecArr[i].GetrQ() / ElecArr[i].GetrC() * 50), true));
                    }
                    //MessageBox.Show("I=" + inductor.I + ", U=" +
                    //    ElecArr[2].GetrQ() / ElecArr[2].GetrC());
                    //MessageBox.Show("Usum=" + (ElecArr[1].GetrQ() / ElecArr[1].GetrC() +
                    //    ElecArr[2].GetrQ() / ElecArr[2].GetrC()));
                }
                Count++;*/
                T += deltaT;
                String str = "";
                for (int i = 0; i < ElecArr.Count; i++)
                {
                    str += "rQ=" + ElecArr[i].rQ + " U=" +
                        ElecArr[i].rQ / ElecArr[i].rC;
                    str += "\r\n";
                }
                //MessageBox.Show(str);
            }
            core.SyncProgess(100, "模拟结束");
        }
    }
}
