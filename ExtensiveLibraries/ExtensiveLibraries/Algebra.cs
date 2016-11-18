using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensiveLibraries
{
    namespace Algebra
    {
        interface ILinearSpatial<T, F> //线性空间
        {
            T AddTo(T other);
            T MultiplyBy(F k);
            T Negative();
            T GetZero();
            string ToString(Complex.ComplexRepresentingModeEnum mode);
        }
        interface IInnerProductSpatial<T, F> /*内积空间*/: ILinearSpatial<T, F> 
        {
            F ProductWith(T other);
            double GetNorm();
            
        }
        
        struct Complex /*复数域*/: IInnerProductSpatial<Complex, Complex> 
        {
            public static Complex Zero //零元
            {
                get
                {
                    return new Complex(0, 0);
                }
            }
            public static Complex Identity //幺元
            {
                get
                {
                    return new Complex(1, 0);
                }
            }
            public Complex GetZero() => Complex.Zero;
            public double Real { get; set; }
            public double Imaginary { get; set; }
            public enum ComplexRepresentingModeEnum { Cartesian, Polar }
            public Complex(double _XorR, double _YorTheta, ComplexRepresentingModeEnum _mode = ComplexRepresentingModeEnum.Cartesian)
            {
                if (_mode == ComplexRepresentingModeEnum.Cartesian)
                {
                    this.Real = _XorR;
                    this.Imaginary = _YorTheta;
                }
                else
                {
                    this.Real = _XorR * Math.Cos(_YorTheta);
                    this.Imaginary = _XorR * Math.Sin(_YorTheta);
                }
            }
            public Complex AddTo(Complex other) => this + other;
            public static Complex operator +(Complex lhs, Complex rhs) => new Complex(lhs.Real + rhs.Real, lhs.Imaginary + rhs.Imaginary);
            public static Complex operator -(Complex lhs, Complex rhs) => new Complex(lhs.Real - rhs.Real, lhs.Imaginary - rhs.Imaginary);
            public Complex MultiplyBy(Complex k) => k * this;
            public static Complex operator *(double k, Complex x) => new Complex(k * x.Real, k * x.Imaginary);
            public static Complex operator *(Complex x, double k) => new Complex(k * x.Real, k * x.Imaginary);
            public static Complex operator /(double k, Complex x) => (new Complex(k, 0.0)) / x;
            public static Complex operator /(Complex x, double k) => x / (new Complex(k, 0.0));
            public Complex ProductWith(Complex other) => this * other.GetConjugate();
            public static Complex operator *(Complex lhs, Complex rhs) => new Complex(lhs.Real * rhs.Real - lhs.Imaginary * rhs.Imaginary, lhs.Real * rhs.Imaginary + lhs.Imaginary * rhs.Real);
            public static Complex operator /(Complex lhs, Complex rhs)
            {
                double x = (lhs.Real * rhs.Real + lhs.Imaginary * rhs.Imaginary) / (rhs.Real * rhs.Real + rhs.Imaginary * rhs.Imaginary);
                double y = (-lhs.Real * rhs.Imaginary + lhs.Imaginary * rhs.Real) / (rhs.Real * rhs.Real + rhs.Imaginary * rhs.Imaginary);
                return new Complex(x, y);
            }
            public double GetNorm() => Math.Sqrt(this.Real * this.Real + this.Imaginary * this.Imaginary);
            public static double operator !(Complex x) => x.GetNorm();
            public static double? operator ~(Complex x) //求复数的辐角主值
            {
                if ((x.Imaginary == 0.0) && (x.Real > 0.0))
                {
                    return 0.0;
                }
                else if ((x.Imaginary == 0.0) && (x.Real < 0.0))
                {
                    return Math.PI;
                }
                else if ((x.Real == 0.0) && (x.Imaginary > 0.0))
                {
                    return Math.PI / 2;
                }
                else if ((x.Real == 0.0) && (x.Imaginary < 0.0))
                {
                    return -Math.PI / 2;
                }
                else if ((x.Imaginary == 0.0) && (x.Real == 0.0))
                {
                    return null;
                }
                else
                {
                    double k = x.Imaginary / x.Real;
                    return Math.Atan(k);
                }
            }
            public Complex Negative() => new Complex(-this.Real, -this.Imaginary);
            public Complex GetConjugate() => new Complex(this.Real, -this.Imaginary); //求当前实例的复共轭
            public static bool operator ==(Complex lhs, Complex rhs) => (lhs.Real == rhs.Real) && (lhs.Imaginary == rhs.Imaginary);
            public static bool operator !=(Complex lhs, Complex rhs) => (lhs.Real != rhs.Real) || (lhs.Imaginary != rhs.Imaginary);
            public override bool Equals(object obj)
            {
                if (!(obj is Complex)) return false;
                return this == (Complex)obj;
            }
            public override int GetHashCode()
            {
                return this.GetNorm().GetHashCode();
            }
            public override string ToString()
            {
                string res = "";
                string imRes = "";
                if (this.Imaginary == 1)
                {
                    imRes = "i";
                }
                else if (this.Imaginary == -1)
                {
                    imRes = "-i";
                }
                else
                {
                    imRes = this.Imaginary.ToString().Trim() + "i";
                }
                if (this.Imaginary == 0)
                {
                    res = this.Real.ToString().Trim();
                }
                else if (this.Real == 0 && this.Imaginary != 0)
                {
                    res = imRes;
                }
                else if (this.Imaginary > 0)
                {
                    res = this.Real.ToString().Trim() + "+" + imRes;
                }
                else
                {
                    res = this.Real.ToString().Trim() + imRes;
                }
                return res;
            }
            public string ToString(ComplexRepresentingModeEnum mode)
            {
                if (mode == ComplexRepresentingModeEnum.Cartesian)
                {
                    return this.ToString();
                }
                else
                {
                    string res = "";
                    string reRes = "";
                    if (this.Imaginary == 0)
                    {
                        res = this.Real.ToString().Trim();
                    }
                    else
                    {
                        if (this.Real == 1)
                        {
                            reRes = "";
                        }
                        else if (this.Real == -1)
                        {
                            reRes = "-";
                        }
                        else
                        {
                            reRes = (!this).ToString("f3").Trim();
                        }
                        if ((~this).HasValue)
                        {
                            res = reRes + "exp(" + ((~this).Value / Math.PI).ToString("f3").Trim() + "πi)";
                        }
                    }
                    return res;
                }
            }

        }

        struct Real /*实数域*/:IInnerProductSpatial<Real, Real>
        {
            public static Real Zero //零元
            {
                get
                {
                    return new Real(0);
                }
            }
            public static Real Identity //幺元
            {
                get
                {
                    return new Real(1);
                }
            }
            public Real GetZero() => Real.Zero;
            public double Value { get; set; }
            public Real(double _value)
            {
                this.Value = _value;
            }
            public Real AddTo(Real other) => this + other;
            public Real MultiplyBy(Real other) => this * other;
            public Real ProductWith(Real other) => this * other;
            public double GetNorm() => Math.Abs(this.Value);
            public Real Negative() => new Real(-this.Value);
            public static Real operator +(Real lhs, Real rhs) => new Real(lhs.Value + rhs.Value);
            public static Real operator +(Real lhs, double rhs) => new Real(lhs.Value + rhs);
            public static Real operator +(double lhs, Real rhs) => new Real(lhs + rhs.Value);
            public static Real operator -(Real lhs, Real rhs) => new Real(lhs.Value - rhs.Value);
            public static Real operator -(Real lhs, double rhs) => new Real(lhs.Value - rhs);
            public static Real operator -(double lhs, Real rhs) => new Real(lhs - rhs.Value);
            public static Real operator *(Real lhs, Real rhs) => new Real(lhs.Value * rhs.Value);
            public static Real operator *(Real lhs, double rhs) => new Real(lhs.Value * rhs);
            public static Real operator *(double lhs, Real rhs) => new Real(lhs * rhs.Value);
            public static Real operator /(Real lhs, Real rhs) => new Real(lhs.Value / rhs.Value);
            public static Real operator /(Real lhs, double rhs) => new Real(lhs.Value - rhs);
            public static Real operator /(double lhs, Real rhs) => new Real(lhs - rhs.Value);
            public static bool operator ==(Real lhs, Real rhs) => lhs.Value == rhs.Value;
            public static bool operator ==(double lhs, Real rhs) => lhs == rhs.Value;
            public static bool operator ==(Real lhs, double rhs) => lhs.Value == rhs;
            public static bool operator !=(Real lhs, Real rhs) => lhs.Value != rhs.Value;
            public static bool operator !=(double lhs, Real rhs) => lhs != rhs.Value;
            public static bool operator !=(Real lhs, double rhs) => lhs.Value != rhs;
            public static bool operator >(Real lhs, Real rhs) => lhs.Value > rhs.Value;
            public static bool operator >(double lhs, Real rhs) => lhs > rhs.Value;
            public static bool operator >(Real lhs, double rhs) => lhs.Value > rhs;
            public static bool operator <(Real lhs, Real rhs) => lhs.Value < rhs.Value;
            public static bool operator <(double lhs, Real rhs) => lhs < rhs.Value;
            public static bool operator <(Real lhs, double rhs) => lhs.Value < rhs;
            public static bool operator >=(Real lhs, Real rhs) => lhs.Value >= rhs.Value;
            public static bool operator >=(double lhs, Real rhs) => lhs >= rhs.Value;
            public static bool operator >=(Real lhs, double rhs) => lhs.Value >= rhs;
            public static bool operator <=(Real lhs, Real rhs) => lhs.Value <= rhs.Value;
            public static bool operator <=(double lhs, Real rhs) => lhs <= rhs.Value;
            public static bool operator <=(Real lhs, double rhs) => lhs.Value <= rhs;
            public static Real operator ++(Real x) => new Real(x.Value++);
            public static Real operator --(Real x) => new Real(x.Value--);
            public override bool Equals(object obj)
            {
                if (!(obj is Real)) return false;
                return this == (Real)obj;
            }
            public override int GetHashCode()
            {
                return this.Value.GetHashCode();
            }
            public override string ToString()
            {
                return this.Value.ToString().Trim();
            }
            public string ToString(Complex.ComplexRepresentingModeEnum mode) => this.ToString();
        }
        
        struct Vector<F> /*数域F上的向量*/: IInnerProductSpatial<Vector<F>, F> where F : IInnerProductSpatial<F, F>, new()
        {
            public int Dimension { get; }
            public F[] Components { get; }
            public static Vector<F> Zero(int dim)
            {
                return new Vector<F>(dim, new F[dim]);
            }
            public Vector<F> GetZero() => Vector<F>.Zero(this.Dimension);
            public Vector(int _dimension, F[] _components)
            {
                this.Dimension = _dimension;
                this.Components = new F[_dimension];
                try
                {
                    for (int i = 0; i < _dimension; i++) this.Components[i] = _components[i];
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Dimension and components dismatch.", ex);
                }
            }
            public Vector(params F[] _components)
            {
                this.Dimension = _components.Length;
                this.Components = new F[this.Dimension];
                try
                {
                    for (int i = 0; i < this.Dimension; i++) this.Components[i] = _components[i];
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Invalid components.", ex);
                }
            }
            public Vector<F> AddTo(Vector<F> other) => this + other;
            public Vector<F> MultiplyBy(F k) => k * this;
            public F ProductWith(Vector<F> other) //向量内积
            {
                if (this.Dimension != other.Dimension) throw new RankException("Dimension dismatch.");
                F res = new F();
                res = res.GetZero();
                for (int i = 0; i < this.Dimension; i++)
                {
                    res = res.AddTo(this.Components[i].ProductWith(other.Components[i]));
                }
                return res;
            }
            public double GetNorm() => Math.Sqrt(this.ProductWith(this).GetNorm());
            public Vector<F> Negative()
            {
                F[] res = new F[this.Dimension];
                for (int i = 0; i < this.Dimension; i++) res[i] = this.Components[i].Negative();
                return new Vector<F>(this.Dimension, res);
            }
            public static Vector<F> operator +(Vector<F> lhs, Vector<F> rhs)
            {
                if (lhs.Dimension != rhs.Dimension) throw new RankException("Dimension dismatch.");
                F[] res = new F[lhs.Dimension];
                for (int i = 0; i < lhs.Dimension; i++) res[i] = lhs.Components[i].AddTo(rhs.Components[i]);
                return new Vector<F>(lhs.Dimension, res);
            }
            public static Vector<F> operator -(Vector<F> lhs, Vector<F> rhs) => lhs + rhs.Negative();
            public static Vector<F> operator *(F lhs, Vector<F> rhs)
            {
                F[] res = new F[rhs.Dimension];
                for (int i = 0; i < rhs.Dimension; i++) res[i] = rhs.Components[i].MultiplyBy(lhs);
                return new Vector<F>(rhs.Dimension, res);
            }
            public static Vector<F> operator *(Vector<F> lhs, F rhs)
            {
                F[] res = new F[lhs.Dimension];
                for (int i = 0; i < lhs.Dimension; i++) res[i] = lhs.Components[i].MultiplyBy(rhs);
                return new Vector<F>(lhs.Dimension, res);
            }
            public static F operator *(Vector<F> lhs, Vector<F> rhs) /*内积*/ => lhs.ProductWith(rhs);
            public static Vector<F> operator ^(Vector<F> lhs, Vector<F> rhs) //二元楔积
            {
                if (lhs.Dimension != rhs.Dimension) throw new RankException("Dimension dismatch.");
                int dimRes = lhs.Dimension * (lhs.Dimension - 1) / 2;
                F[] res = new F[dimRes];
                int pin = dimRes - 1;
                for (int i = 0; i < lhs.Dimension - 1; i++)
                    for (int j = i + 1; j < lhs.Dimension; j++)
                    {
                        F t = (lhs.Components[i].ProductWith(rhs.Components[j])).AddTo((lhs.Components[j].ProductWith(rhs.Components[i])).Negative());
                        if (((i + j) & 1) == 0) t = t.Negative();
                        res[pin--] = t; //ei^ej的系数依序从res的最后一维开始倒着储存，若i+j为偶数则取负号
                    }
                return new Vector<F>(dimRes, res);
                //对于三维向量返回它们的叉乘
            }
            public static bool operator ==(Vector<F> lhs, Vector<F> rhs)
            {
                if (lhs.Dimension != rhs.Dimension) return false;
                for (int i = 0; i < lhs.Dimension; i++)
                    if (!(lhs.Components[i].Equals(rhs.Components[i]))) return false;
                return true;
            }
            public static bool operator !=(Vector<F> lhs, Vector<F> rhs) => !(lhs == rhs);
            public override bool Equals(object obj)
            {
                if (!(obj is Vector<F>)) return false;
                return this == (Vector<F>)obj;
            }
            public override int GetHashCode()
            {
                return this.GetNorm().GetHashCode();
            }
            public override string ToString()
            {
                string res = "(";
                for (int i = 0; i < this.Dimension - 1; i++) res += this.Components[i].ToString() + ", ";
                res += this.Components[this.Dimension - 1].ToString() + ")";
                return res;
            }
            public string ToString(Complex.ComplexRepresentingModeEnum mode)
            {
                string res = "(";
                for (int i = 0; i < this.Dimension - 1; i++) res += this.Components[i].ToString(mode) + ", ";
                res += this.Components[this.Dimension - 1].ToString(mode) + ")";
                return res;
            }
        }

        //需要定义张量

        //需要定义矩阵


    }
}
