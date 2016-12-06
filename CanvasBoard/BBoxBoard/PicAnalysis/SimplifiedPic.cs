using BBoxBoard.BasicDraw;
using BBoxBoard.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MathNet;
using MathNet.Numerics.LinearAlgebra.Complex;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace BBoxBoard.PicAnalysis
{
    public class SimplifiedPic
    {
        List<IntPoint> PointArr;
        public List<ElecFeature> FeatureArr;
        List<ElecStructure> StructureArr;
        public double[,] A;

        public SimplifiedPic(List<BriefElecComp> Arr, MainWindow mainWindow)
        {
            mainWindow.SyncProgess(0, "处理图像");
            PointArr = new List<IntPoint>();
            FeatureArr = new List<ElecFeature>();
            StructureArr = new List<ElecStructure>();
            //第一步：只要找到导线，则把所有的第二节点变成第一节点
            mainWindow.SyncProgess(5, "处理导线");
            for (int i=0; i<Arr.Count; i++)
            {
                if (Arr[i].Comp == BriefElecComp.Comp_Wire)
                {
                    //把所有的A替换成B
                    IntPoint A = Arr[i].Interfaces[0];
                    IntPoint B = new IntPoint(Arr[i].Interfaces[1]);//一定要新建
                    for (int j=0; j<Arr.Count; j++)
                    {
                        Arr[j].ReplaceWith(B, A);
                    }
                }
            }
            mainWindow.SyncProgess(10, "处理坐标");
            //第二步：找到所有不同的坐标，存在PointArr里
            for (int i=0; i<Arr.Count; i++)
            {
                for (int j=0; j<Arr[i].Interfaces.Count; j++)
                {
                    if (!PointArr.Contains(Arr[i].Interfaces[j]))
                    {
                        PointArr.Add(new IntPoint(Arr[i].Interfaces[j]));
                    }
                }
            }
            mainWindow.SyncProgess(15, "整理元件");
            for (int i=0; i<Arr.Count; i++)
            {
                if (Arr[i].Comp != BriefElecComp.Comp_Wire)
                {
                    int Left = PointArr.IndexOf(Arr[i].Interfaces[0]);
                    int Right = PointArr.IndexOf(Arr[i].Interfaces[1]);
                    ElecFeature elecFeature = Arr[i].elecComp.GetElecFeature();
                    elecFeature.SetFoot(Left, Right);
                    elecFeature.SetFather(Arr[i]);
                    //生成了elecFeature的序列，和对应的Structure序列
                    FeatureArr.Add(elecFeature);
                    StructureArr.Add(elecFeature.GetStructure());
                }
            }
            //显示所有的Feature信息
            /*String str1 = "";
            for (int i=0; i< FeatureArr.Count; i++)
            {
                str1 += FeatureArr[i] + "\r\n";
            }
            MessageBox.Show(str1);*/
            //显示所有的节点信息
            String str = "";
            for (int i=0; i<PointArr.Count; i++)
            {
                str += "Point " + i + ":" + PointArr[i].X + 
                    "," + PointArr[i].Y + "\n";
            }
            MessageBox.Show(str);
            //显示所有的结构信息
            /*String str2 = "";
            for (int i=0; i<StructureArr.Count; i++)
            {
                str2 += StructureArr[i] + "\n";
            }
            MessageBox.Show(str2);*/
            //下面开始处理结构，获得结构矩阵
            mainWindow.SyncProgess(20, "处理结构");
            ComputeA(mainWindow);
        }
        private void ComputeA(MainWindow mainWindow)
        {
            //初始化矩阵
            A = new double[StructureArr.Count, StructureArr.Count];
            //MessageBox.Show("" + StructureArr.Count);
            for (int i=0; i < StructureArr.Count; i++)
            {
                for (int j=0; j< StructureArr.Count; j++)
                {
                    A[i, j] = 0;
                }
            }
            //分组前先剪死枝
            mainWindow.SyncProgess(25, "处理元件");
            List<ElecStructure> NoDeadArr = new List<ElecStructure>();
            List<int> NoDeadToOMap = new List<int>();
            for (int i=0; i<StructureArr.Count; i++)
            {
                if (StructureArr[i].LeftFoot == StructureArr[i].RightFoot)
                {
                    //如果被短路的话，设为0，Bug修复
                    A[i, i] = 0;
                }
                else if (IsDeadin(i, StructureArr))
                {
                    A[i, i] = 1; //直接映射就可以
                }
                else
                {
                    NoDeadArr.Add(StructureArr[i]);
                    NoDeadToOMap.Add(i);
                }
            }
            //对没有死枝的电路分组，之前写错了
            mainWindow.SyncProgess(30, "分组");
            int TeamCount = 0;
            for (int i=0; i< NoDeadArr.Count; i++)
            {
                if (NoDeadArr[i].GroupIndex == -1)
                {
                    //找到没有分组的
                    //MessageBox.Show("Founding:" + i);
                    List<int> RelatedComp = GetGroupRelated(i, NoDeadArr);
                    for (int j=0; j<RelatedComp.Count; j++)
                    {
                        NoDeadArr[RelatedComp[j]].GroupIndex = TeamCount;
                    }
                    TeamCount++;
                }
            }
            //已经分好组了，对于每一个组内部做操作
            for (int i=0; i<TeamCount; i++)
            {
                mainWindow.SyncProgess(30 + 70 * i / TeamCount, "处理分组：" + (i+1));
                HandleGroup(i, NoDeadToOMap, NoDeadArr);
            }
            mainWindow.SyncProgess(100, "图像处理完成");
            /*MessageBox.Show("0:" + IsDeadin(0, StructureArr));
            MessageBox.Show("1:" + IsDeadin(1, StructureArr));
            MessageBox.Show("2:" + IsDeadin(2, StructureArr));
            MessageBox.Show("3:" + IsDeadin(3, StructureArr));
            MessageBox.Show("4:" + IsDeadin(4, StructureArr));*/
            Console.WriteLine("A=");
            for (int i=0; i< StructureArr.Count; i++)
            {
                for (int j=0; j< StructureArr.Count; j++)
                {
                    Console.Write(" " + A[i, j]);
                }
                Console.Write("\r\n");
            }
        }

        private void HandleGroup(int groupIndex, List<int> NoDeadToOMap, 
            List<ElecStructure> NoDeadArr)
        {
            //MessageBox.Show("HandleGroup:" + groupIndex);
            List<int> MapToOIndex = new List<int>();
            List<ElecStructure> GroupArr = new List<ElecStructure>();
            for (int i=0; i< NoDeadArr.Count; i++)
            {
                if (NoDeadArr[i].GroupIndex == groupIndex)
                {
                    //发现了属于这个组的一个元件
                    MapToOIndex.Add(NoDeadToOMap[i]); //复合映射
                    GroupArr.Add(NoDeadArr[i]);
                }
            }
            //已经得到了一个Structure数组和一个角标映射数组
            double[,] Equation = new double[GroupArr.Count, GroupArr.Count];
            double[,] BMatrix = new double[GroupArr.Count, GroupArr.Count];
            //double[,] SolveResult = new double[GroupArr.Count, GroupArr.Count];
            //归零
            for (int i = 0; i < GroupArr.Count; i++)
            {
                for (int j = 0; j < GroupArr.Count; j++)
                {
                    Equation[i, j] = 0;
                    BMatrix[i, j] = 0;
                }
            }
            //MessageBox.Show("GetNextLoopEquation");
            List<int> EquationMap = new List<int>();
            for (int i=0; i< GroupArr.Count; i++)
            {
                EquationMap.Add(i);
            }
            //获得回路方程
            int endIndex = GetNextLoopEquation(0, Equation, GroupArr, EquationMap);
            
            //寻找所有的节点
            List<int> Joint = new List<int>();
            for (int i=0; i<GroupArr.Count; i++)
            {
                if (!Joint.Contains(GroupArr[i].LeftFoot))
                {
                    Joint.Add(GroupArr[i].LeftFoot);
                }
                if (!Joint.Contains(GroupArr[i].RightFoot))
                {
                    Joint.Add(GroupArr[i].RightFoot);
                }
            }
            //写上节点方程
            for (int i=1; i<Joint.Count; i++)
            {
                for (int j=0; j<GroupArr.Count; j++)
                {
                    //修复Bug，节点方程与电容值无关
                    Equation[endIndex, j] = GroupArr[j].GetFoot(Joint[i]);
                    BMatrix[endIndex, j] = GroupArr[j].GetFoot(Joint[i]);
                }

                endIndex++;
            }
            /*Console.WriteLine("Equation=");
            for (int i = 0; i < GroupArr.Count; i++)
            {
                for (int j = 0; j < GroupArr.Count; j++)
                {
                    Console.Write(" " + Equation[i, j]);
                }
                Console.Write("\r\n");
            }
            Console.WriteLine("BMatrix=");
            for (int i = 0; i < GroupArr.Count; i++)
            {
                for (int j = 0; j < GroupArr.Count; j++)
                {
                    Console.Write(" " + BMatrix[i, j]);
                }
                Console.Write("\r\n");
            }*/
            //开始解方程的列向量
            Complex[,] complexEquation = new Complex[GroupArr.Count, GroupArr.Count];
            Complex[] complexB = new Complex[GroupArr.Count];
            Complex[,] complexBMatrix = new Complex[GroupArr.Count, GroupArr.Count];
            for (int i = 0; i < GroupArr.Count; i++)
            {
                for (int j = 0; j < GroupArr.Count; j++)
                {
                    complexEquation[i, j] = Equation[i, j];
                    complexBMatrix[i, j] = BMatrix[i, j];
                }
            }
            DenseMatrix matrixA = DenseMatrix.OfArray(complexEquation);
            DenseMatrix matrixB = DenseMatrix.OfArray(complexBMatrix);
            /*Console.WriteLine("Traditional:");
            for (int i=0; i < GroupArr.Count; i++)
            {
                for (int j=0; j<GroupArr.Count; j++)
                {
                    complexB[j] = BMatrix[j, i];
                }
                DenseVector vectorB = new DenseVector(complexB);
                Vector<Complex> result = matrixA.LU().Solve(vectorB);
                for (int j = 0; j < result.Count; j++)
                {
                    SolveResult[j, i] = result[j].Real;
                    Console.Write(" " + result[j].Real);
                }
                Console.Write("\r\n");
            }*/
            //尝试另一种求法，经过证实这个方法没有问题且更快，采用这一种了
            Matrix<Complex> QResult = matrixA.Inverse() * matrixB;
            Console.WriteLine("New Method:");
            for (int i=0; i<GroupArr.Count; i++)
            {
                for (int j=0; j<GroupArr.Count; j++)
                {
                    Console.Write(" " + QResult[i, j].Real);
                }
                Console.Write("\r\n");
            }
            //尝试完毕
            for (int i = 0; i < GroupArr.Count; i++)
            {
                for (int j = 0; j < GroupArr.Count; j++)
                {
                    A[MapToOIndex[i], MapToOIndex[j]] =
                        //SolveResult[i, j];
                        QResult[i, j].Real;
                }
            }
        }

        private int GetNextLoopEquation(int startIndex, double[,] Equation, List<ElecStructure> Arr, List<int> MapEquation)
        {
            //剪死枝
            List<ElecStructure> RoundArr = new List<ElecStructure>();
            List<int> MapEquation2 = new List<int>();
            for (int i=0; i<Arr.Count; i++)
            {
                if (!IsDeadin(i, Arr))
                {
                    RoundArr.Add(Arr[i]);
                    MapEquation2.Add(MapEquation[i]);
                }
            }
            //判断是不是没有了，如果没有了就没有回路，直接返回就可以
            if (RoundArr.Count == 0) return startIndex;
            //现在一定有回路，获得一个可行的路径
            //MessageBox.Show("Getting AvalPath:remain:" + RoundArr.Count);
            List<IntPoint> path = GetAvalPath(RoundArr); //卡在这里了
            //输出
            /*String str = "Path:";
            for (int i=0; i<path.Count; i++)
            {
                str += "(" + path[i].X + "," + path[i].Y + ")";
            }
            str += "\r\nMap:";
            for (int i=0; i< MapEquation2.Count; i++)
            {
                str += "(" + MapEquation2[i] + ")";
            }
            MessageBox.Show(str);*/
            //获得一个回路，填充到方程中去
            //对于每一个步骤，寻找一个原件匹配
            //debug：不能找相同的元件，需要把找没找过用List记录下来
            bool[] canBeSelect = new bool[RoundArr.Count];
            for (int i=0; i < canBeSelect.Count(); i++)
            {
                canBeSelect[i] = true;
            }
            for (int i=0; i< Arr.Count; i++)
            {
                //归零
                Equation[startIndex, i] = 0;
            }
            for (int i=0; i<path.Count; i++)
            {
                for (int j=0; j< RoundArr.Count; j++)
                {
                    if (canBeSelect[j])
                    {
                        int IsBetweenFoot = RoundArr[j].IsBetweenFoot
                        (path[i].X, path[i].Y);
                        if (IsBetweenFoot != 0)
                        {
                            Equation[startIndex, MapEquation2[j]] = IsBetweenFoot /
                                RoundArr[j].rC;
                            canBeSelect[j] = false;
                            break;
                        }
                    }
                }
            }
            //下一层递归
            RoundArr.RemoveAt(0);
            MapEquation2.RemoveAt(0); //修复这个严重的Bug了！
            return GetNextLoopEquation(startIndex + 1, Equation, RoundArr, MapEquation2);
        }

        private List<IntPoint> GetAvalPath(List<ElecStructure> Arr)
        {
            //假定这里面没有死枝，即所有元件都构成回路的一部分
            //返回值是一个0->1,1->3,3->2类似的东西
            List<IntPoint> path = new List<IntPoint>();
            List<KeyValuePair<int, IntPoint>> avalPath =
                new List<KeyValuePair<int, IntPoint>>();
            bool FoundOther = true;
            bool FoundFinal = false;
            avalPath.Add(new KeyValuePair<int, IntPoint>(Arr[0].LeftFoot,
                new IntPoint(Arr[0].LeftFoot, Arr[0].LeftFoot)));
            while (FoundOther && FoundFinal == false)
            {
                FoundOther = false;
                //开始用蔓延的算法求去掉这个元件以后还能不能连通
                for (int i = 0; i < avalPath.Count && FoundFinal == false; i++)
                {
                    //查找所有位于FoundFoot里的点能联通的位置
                    //MessageBox.Show("i=" + i);
                    for (int j = 1; j < Arr.Count; j++) //j从1开始，跳过0元件!!!!!!
                    {
                        //MessageBox.Show("j=" + j);
                        if (Arr[j].IsLinkToFoot(avalPath[i].Key))
                        {
                            int OtherFootIndex = Arr[j].
                                GetOtherFoot(avalPath[i].Key);
                            bool HasBeenReached = false;
                            for (int k = 0; k < avalPath.Count; k++)
                            {
                                //MessageBox.Show("k=" + k);
                                if (OtherFootIndex == avalPath[k].Key)
                                {
                                    HasBeenReached = true;
                                    break;
                                }
                            }
                            if (HasBeenReached == false)
                            {
                                //找到新的Foot了
                                if (OtherFootIndex == Arr[0].RightFoot)
                                {
                                    avalPath.Add(new KeyValuePair<int, IntPoint>(OtherFootIndex,
                                        new IntPoint(avalPath[i].Key, OtherFootIndex)));
                                    FoundFinal = true;
                                    break;
                                }
                                FoundOther = true;
                                avalPath.Add(new KeyValuePair<int, IntPoint>(OtherFootIndex,
                                        new IntPoint(avalPath[i].Key, OtherFootIndex)));
                            }
                        }
                    }
                }
            }
            if (!FoundFinal)
            {
                MessageBox.Show("寻找回路出错！请联系管理员");
                return null;
            }
            int NextFinding = Arr[0].RightFoot;
            while (NextFinding != Arr[0].LeftFoot)
            {
                for (int i = 0; i < avalPath.Count; i++)
                {
                    if (NextFinding == avalPath[i].Key)
                    {
                        //找到一个可行的步骤
                        path.Insert(0, avalPath[i].Value);
                        NextFinding = avalPath[i].Value.X;
                        break;
                    }
                }
            }
            path.Insert(0, new IntPoint(Arr[0].RightFoot, Arr[0].LeftFoot));
            return path;
        }

        private List<int> GetGroupRelated(int Index, List<ElecStructure> Arr)
        {
            List<int> RelatedComp = new List<int>();
            bool FoundOther = true;
            List<int> FoundFoots = new List<int>();
            FoundFoots.Add(Arr[Index].LeftFoot);
            FoundFoots.Add(Arr[Index].RightFoot);
            while (FoundOther)
            {
                FoundOther = false;
                //开始用蔓延的算法求去掉这个元件以后还能不能连通
                for (int i = 0; i < FoundFoots.Count; i++)
                {
                    //查找所有位于FoundFoot里的点能联通的位置
                    for (int j = 0; j < Arr.Count; j++)
                    {
                        if (j != Index) //不能使原来的元件
                        {
                            if (Arr[j].IsLinkToFoot(FoundFoots[i]))
                            {
                                int OtherFootIndex = Arr[j].
                                    GetOtherFoot(FoundFoots[i]);
                                if (!FoundFoots.Contains(OtherFootIndex))
                                {
                                    //找到新的Foot了
                                    FoundOther = true;
                                    FoundFoots.Add(OtherFootIndex);
                                }
                            }
                        }
                    }
                }
            }
            for (int i=0; i<Arr.Count; i++)
            {
                for (int j=0; j<FoundFoots.Count; j++)
                {
                    if (Arr[i].IsLinkToFoot(FoundFoots[j]))
                    {
                        RelatedComp.Add(i);
                        break;
                    }
                }
            }
            /*String str = "Related:";
            for (int i=0; i< RelatedComp.Count; i++)
            {
                str += RelatedComp[i] + ",";
            }
            MessageBox.Show(str);*/
            return RelatedComp;
        }

        private bool IsDeadin(int Index, List<ElecStructure> Arr)
        {
            //MessageBox.Show("Index:" + Index);
            bool FoundOther = true;
            List<int> FoundFoots = new List<int>();
            FoundFoots.Add(Arr[Index].LeftFoot);
            while (FoundOther)
            {
                FoundOther = false;
                //开始用蔓延的算法求去掉这个元件以后还能不能连通
                for (int i=0; i< FoundFoots.Count; i++)
                {
                    //查找所有位于FoundFoot里的点能联通的位置
                    for (int j=0; j<Arr.Count; j++)
                    {
                        if (j != Index) //不能使原来的元件
                        {
                            if (Arr[j].IsLinkToFoot(FoundFoots[i]))
                            {
                                int OtherFootIndex = Arr[j].
                                    GetOtherFoot(FoundFoots[i]);
                                if (!FoundFoots.Contains(OtherFootIndex))
                                {
                                    //找到新的Foot了
                                    if (OtherFootIndex == Arr[Index].RightFoot) return false;
                                    FoundOther = true;
                                    FoundFoots.Add(OtherFootIndex);
                                }
                            }
                        }
                    }
                }
                /*String str = "";
                for (int i=0; i<FoundFoots.Count; i++)
                {
                    str += FoundFoots[i] + ",";
                }
                MessageBox.Show(str);*/
            }
            return true;
        }
    }
}
