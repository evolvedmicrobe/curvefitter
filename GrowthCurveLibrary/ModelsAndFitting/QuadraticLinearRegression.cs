#if !MONO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoNS.Stats;
using ShoNS.Array;

namespace GrowthCurveLibrary
{
    /// <summary>
    /// Transforms y values, calculates value, goes for it you could say
    /// </summary>
    [Serializable]
    public class QuadraticLinearRegression : AbstractFitter
    {
        Regress r;
        new DoubleArray pParameters;

        enum ParametersIndex : int { Intercept = 0, Linear = 1,Quadratic=2 };
        public double Intercept
        {
            get { return pParameters[(int)ParametersIndex.Intercept]; }
        }
        public double Linear
        {
            get { return pParameters[(int)ParametersIndex.Linear]; }
        }
        public double Quadratic
        {
            get { return pParameters[(int)ParametersIndex.Quadratic]; }
        }
        public double CalculateXValueAtOD(double OD)
        {
            double c =  this.Intercept-OD;
            double b = this.Linear;
            double a = this.Quadratic;
            double ans=(-b+Math.Sqrt(b*b-4*a*c))/(2*a);
            if(ans>x.Max() || ans<x.Min())
                ans = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            return ans;
        }
        public double GrowthRateAtValue(double OD=0.15)
        {
            double lod = Math.Log(OD);
            lod = CalculateXValueAtOD(lod);
            return this.Linear + lod * this.Quadratic * 2.0;
           
        }
        public QuadraticLinearRegression(double[] XDATA, double[] YDATA)
        {
            try
            {
                if ((XDATA.Length < 2 || YDATA.Length < 2) || (XDATA.Length != YDATA.Length))
                { throw new ArgumentOutOfRangeException("Exponential fit can't work with less then 2 points or unequal matrices"); }
                if (XDATA.Length != YDATA.Length)
                    throw new ArgumentException("Arrays of unequal size were passed to the quadratic fitter");
                //deep copy the data to protect its integrity
                y = YDATA.ToArray();
                y = (from b in y select Math.Log(b)).ToArray();
                x = XDATA.ToArray();
                DoubleArray dx = DoubleArray.From(XDATA);
                DoubleArray dy = DoubleArray.From(y);
                var x2 = dx.ElementMultiply(dx);
                var xmat = DoubleArray.VertStack(dx, x2);
                xmat = xmat.T;
                r = new Regress(dy, xmat);
                this.name = "ExpDecreasing";
                pParameters = r.Beta;
                this.SuccessfulFit = true;
            }
            catch
            {
                this.SuccessfulFit = false;
            }
            
        }

        #region IAbstractFitter Members

      
        new public double calculateAbsError()
        {
            return (from x in r.Res select Math.Abs(x)).ToArray().Sum();
        }

        new public double calculateResidualSumofSquares()
        {
            return (from x in this.Residuals select x * x).ToArray().Sum();
        }

        

        new public double[] PredictedValues
        {
            get { return r.YPred.ToArray(); }
        }

        new public double R2
        {
            get { return r.Rsq; }
        }

        //TODO: Why did I override this??
        new public double[] Residuals
        {
            get { return (from x in r.Res select x).ToArray(); }
        }
        public double[] ReturnResidualsAfterExpTransform
        {
            get
            {
                return PredictedValues.Zip(y, (z, w) => Math.Exp(z) - Math.Exp(w)).ToArray();
            }
        }

        new public double RMSE
        {
            get { return calculateResidualSumofSquares() / (this.x.Length - 3); }
        }

        

   

        #endregion

        public override double FunctiontoFit(double x)
        {

            return this.Intercept + this.Linear * x + this.Quadratic * x * x;
        }

        protected override void FitModel()
        {
            throw new NotImplementedException();
        }
    }
}
#endif