using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensiveLibraries
{
    namespace Analysis
    {
        public delegate double FunctionHandler(double[] args); //表示一个多元函数的委托
        public delegate double MonoFunctionHandler(double arg); //表示一个一元函数的委托
        static class Calculus //该类未完成
        {
            public enum LimSign { Negative, Positive }; //趋向极限的方向
            public struct LimVariable //包含趋近方向的数值点
            {
                public double Value { get; set; }
                public LimSign Sign { get; set; }
                public LimVariable(double _p, LimSign _s)
                {
                    this.Value = _p;
                    this.Sign = _s;
                }

            }
            public static double? Limit(FunctionHandler f, double precision, params LimVariable[] points) //返回一个多元函数在某点的极限值（为每个分量指定趋近方向）
            {
                if (f == null)
                {
                    throw new ArgumentNullException("Function Null");
                }
                if ((points == null) || (points.Length == 0))
                {
                    throw new ArgumentNullException("Variable Null");
                }
                int pointsCount = points.Length;
                double res = 0;
                precision = Math.Abs(precision);
                try
                {
                    double[] dArgs = new double[pointsCount];
                    for (int i = 0; i < pointsCount; i++)
                    {
                        switch (points[i].Sign)
                        {
                            case LimSign.Negative:
                                dArgs[i] = points[i].Value - precision;
                                break;
                            case LimSign.Positive:
                                dArgs[i] = points[i].Value + precision;
                                break;
                        }
                    }
                    res = f(dArgs);
                }
                catch (ArgumentException) //自变量数量错误
                {
                    //MessageBox
                    return null;
                }
                catch (Exception) //极限不存在或其他错误
                {
                    //MessageBox
                    return null;
                }
                return res;
            }
            public static double? Limit(MonoFunctionHandler f, double precision, LimVariable point) //返回一个一元函数在某点的左右极限值
            {
                if (f == null)
                {
                    throw new ArgumentNullException("Function Null");
                }
                double res = 0;
                precision = Math.Abs(precision);
                try
                {
                    switch (point.Sign)
                    {
                        case LimSign.Negative:
                            res = f(point.Value - precision);
                            break;
                        case LimSign.Positive:
                            res = f(point.Value + precision);
                            break;
                    }
                }
                catch (Exception) //极限不存在或其他错误
                {
                    //MessageBox
                    return null;
                }
                return res;
            }
            public static double? Derivative(MonoFunctionHandler f, LimVariable x, double precision = 1E-3) //返回一个一元函数在某点的左右导数值；precision过大会引入较大误差，precision过小会导致数值结果不稳定
            {
                if (f == null)
                {
                    throw new ArgumentNullException("Function Null");
                }
                double res = 0;
                precision = Math.Abs(precision);
                try
                {
                    switch (x.Sign)
                    {
                        case LimSign.Negative:
                            res = (-25 * f(x.Value) + 48 * f(x.Value - precision) - 36 * f(x.Value - 2 * precision) + 16 * f(x.Value - 3 * precision) - 3 * f(x.Value - 4 * precision)) / (12 * -precision); //五点公式
                            break;
                        case LimSign.Positive:
                            res = (-25 * f(x.Value) + 48 * f(x.Value + precision) - 36 * f(x.Value + 2 * precision) + 16 * f(x.Value + 3 * precision) - 3 * f(x.Value + 4 * precision)) / (12 * precision);
                            break;
                    }
                }
                catch (Exception) //导数不存在或其他错误
                {
                    //MessageBox
                    return null;
                }
                return res;
            }
            public static double? PartialDerivative(FunctionHandler f, LimSign xSign, uint index = 1, double precision = 1E-3, params double[] points) //返回一个多元函数在某点的指定趋向方向的偏导数值；index指示要求偏微分的自变量序号，从1开始；precision过大会引入较大误差，precision过小会导致数值结果不稳定
            {
                if (f == null)
                {
                    throw new ArgumentNullException("Function Null");
                }
                if ((points == null) || (points.Length == 0))
                {
                    throw new ArgumentNullException("Variable Null");
                }
                double res = 0;
                if (index-- == 0) index = 0;
                precision = Math.Abs(precision);
                try
                {
                    if (xSign == LimSign.Negative) precision = -precision;
                    double[][] fi = new double[5][];
                    for (int i = 0; i < 5; i++)
                    {
                        fi[i] = (double[])points.Clone();
                        fi[i][index] += i * precision;
                    }
                    res = (-25 * f(fi[0]) + 48 * f(fi[1]) - 36 * f(fi[2]) + 16 * f(fi[3]) - 3 * f(fi[4])) / (12 * precision); //五点公式
                }
                catch (Exception) //导数不存在或其他错误
                {
                    //MessageBox
                    return null;
                }
                return res;
            }
            public static double? Integrate(MonoFunctionHandler f, double lower, double upper, double precision = 1E-12) //返回一个一元函数在(lower,upper)区间上的定积分
            {
                //采用Romberg积分算法
                if (lower > upper) return -Integrate(f, upper, lower, precision);
                if (lower == upper) return 0;
                double fa, fb;
                try
                {
                    fa = f(lower);
                }
                catch
                {

                    fa = Limit(f, 1E-15, new LimVariable(lower, LimSign.Positive)).Value;
                }
                try
                {
                    fb = f(upper);
                }
                catch
                {

                    fb = Limit(f, 1E-15, new LimVariable(upper, LimSign.Negative)).Value;
                }
                precision = Math.Abs(precision);
                double h = upper - lower;
                int k = 1;
                double delta = 0;
                double[][] arrRbg = new double[2][];
                arrRbg[0] = new double[] { (fa + fb) * h / 2 };
                try
                {
                    do
                    {
                        arrRbg[1] = new double[arrRbg[0].Length + 1];
                        h /= 2;
                        k++;
                        for (UInt64 i = 1; i <= (UInt64)Math.Pow(2, k - 2); i++) arrRbg[1][0] += f(lower + (2 * i - 1) * h);
                        arrRbg[1][0] = (arrRbg[0][0] + 2 * h * arrRbg[1][0]) / 2;
                        for (int i = 1; i < arrRbg[0].Length + 1; i++) arrRbg[1][i] = arrRbg[1][i - 1] + (arrRbg[1][i - 1] - arrRbg[0][i - 1]) / (Math.Pow(4, i) - 1);
                        delta = arrRbg[1][arrRbg[0].Length] - arrRbg[0][arrRbg[0].Length - 1];
                        arrRbg[0] = arrRbg[1];
                    } while (Math.Abs(delta) >= precision);
                    return arrRbg[0][arrRbg[0].Length - 1];
                }
                catch
                {
                    return null;
                }  
            }
        }

    }
}
