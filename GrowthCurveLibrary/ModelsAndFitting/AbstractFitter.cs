using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;


namespace GrowthCurveLibrary
{

    //ABSTRACT CLASSES TO BE INHERITED FROM
    #region AbstractFitter
    /// <summary>
    /// This abstract fitter implements a lot of basic fitter functions.
    /// </summary>
    [Serializable]
    public abstract class AbstractFitter : GrowthCurveLibrary.IAbstractFitter
    {
        public string Comment { get; set; }
        public bool FitByDefault { get; set; }
        public double CalculateLogLikelihoodAssumingNormallyDistributedError(IEnumerable<double> xPoints, IEnumerable<double> yPoints)
        {
            double[] xs = xPoints.ToArray();
            double[] ys = yPoints.ToArray();
            int n = xs.Length; ;
            if (n != ys.Length)
                throw new Exception("Arrays to get likelihood for don't match!");
            double ll = 0.0;
            double EstSE = this.RMSE;
            double var = Math.Pow(EstSE, 2);
            double tau = Math.PI * 2.0;
            for (int i = 0; i < n; i++)
            {
                double ypr = FunctiontoFit(xs[i]);
                ll += Math.Exp(-Math.Pow((ypr - ys[i]), 2) / (2 * var)) - .5 * Math.Log(tau * var);
            }
            return ll;
        }
        public int NumberOfParameters
        {
            get { return pParameters.Length; }
        }
        public string name = "";
       
        protected double[] x;
        public double[] X
        {
            get { return x.ToArray(); }
        }
        public double[] Y
        {
            get { return y.ToArray(); }
        }
        protected double[] y;
        protected double[] ypred;
        public double[] PredictedValues
        {
            get
            {
                if (ypred == null && SuccessfulFit)
                {
                    makeYHAT();
                }
                if (ypred != null)
                {
                    return ypred.ToArray();
                }
                else { return null; }
            }

        }
        public double[] CalculateResidualsAtNewPoints(double[] xVals, double[] yvals)
        {
            if (xVals.Length != yvals.Length)
                throw new Exception("Points are unmatched");

            return Enumerable.Zip(this.MakePredictionsAtPoints(xVals), yvals, (x, y) => y - x).ToArray();
        }
        protected void VerifyInput(double[] XDATA, double[] YDATA)
        {
            if ((XDATA.Length < 2 || YDATA.Length < 2) || (XDATA.Length != YDATA.Length))
            { throw new ArgumentOutOfRangeException(this.name+"Can't work with less then 2 points or unequal matrices"); }
            if (XDATA.Length != YDATA.Length)
                throw new ArgumentException("Arrays of unequal size were passed to the non-linear fitter");
            //deep copy the data to protect its integrity
            y = YDATA.ToArray();
            x = XDATA.ToArray();
        }
        protected double[] pParameters;//parameters in the model
        public double[] Parameters { get { return pParameters.ToArray(); } }
        public bool SuccessfulFit { get; set; }//determines if the model is fitted and has parameters
        public double RMSE
        {
            get
            {
                if (SuccessfulFit)
                {
                    return calculateRMSE();
                }
                else
                {
                    return -999;
                }
            }
        }
        public double R2
        {
            get
            {
                if (SuccessfulFit)
                {
                    return calculateR2();
                }
                else
                {
                    return -999;
                }
            }
        }
        public double AbsError
        {
            //This calculates teh absolute error |yhat-y|
            get
            {
                if (SuccessfulFit)
                {
                    return calculateAbsError();
                }
                else
                {
                    return -999;
                }
            }
        }
        public double[] Residuals
        {
            get
            {
                return Enumerable.Zip(y, PredictedValues, (ac, pr) => ac - pr).ToArray();
            }
        }
        public AbstractFitter()
        {

        }
        protected void makeYHAT()
        {
            ypred = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                ypred[i] = FunctiontoFit(x[i]);
            }
        }
        protected double calculateRMSE()
        {
            if (!SuccessfulFit)
                return double.NaN;
            double MSE = calculateResidualSumofSquares();
            double DataPoints = x.Length;
            double NumParmeters = pParameters.Length;
            double df = DataPoints - NumParmeters;
            MSE = MSE / df;
            double RMSE = Math.Sqrt(MSE);
            return RMSE;
        }
        protected double calculateR2()
        {
            if (!SuccessfulFit)
                return double.NaN;
            double SST, SSR;
            SSR = calculateResidualSumofSquares();
            double Ymean = y.Average();
            SST = 0;
            for (int i = 0; i < x.Length; i++)
            {
                SST += Math.Pow((y[i] - Ymean), 2.0);
            }
            return (1.0 - (SSR / SST));
        }
        /// <summary>
        /// Only should be called by external classes following a fit
        /// </summary>
        /// <returns>NaN if not successful fit, otherwise the sum of squares</returns>
        public double calculateResidualSumofSquares()
        {
            if (!SuccessfulFit)
                return double.NaN;
            if (ypred == null)
                makeYHAT();
            double SSR = 0;
            for (int i = 0; i < y.Length; i++)
            { SSR += Math.Pow((ypred[i] - y[i]), 2.0); }
            return SSR;
        }
        public virtual double calculateAbsError()
        {
            if (!SuccessfulFit)
                return double.NaN;
            if (ypred == null)
                makeYHAT();
            double Error = 0;
            for (int i = 0; i < ypred.Length; i++)
            {
                Error += Math.Abs(ypred[i] - y[i]);
            }
            return Error;
        }
        public abstract double FunctiontoFit(double x);
        protected abstract void FitModel();
        public virtual void GenerateFitLine(double LowX, double interval, double HighX, out double[] xvalues, out double[] yvalues)
        {
            double curVal = LowX;
            //now to determine the number of values in the interval
            int ArraySize = (int)((HighX - LowX) / interval);
            xvalues = new double[ArraySize + 1];
            yvalues = new double[ArraySize + 1];
            for (int i = 0; i < ArraySize; i++)
            {
                xvalues[i] = curVal;
                yvalues[i] = FunctiontoFit(curVal);
                curVal += interval;
            }
            xvalues[ArraySize] = HighX;
            yvalues[ArraySize] = FunctiontoFit(HighX);
        }
        public virtual IEnumerable<double> MakePredictionsAtPoints(IEnumerable<double> XValues)
        {
            List<double> predictions = new List<double>();
            foreach (double curVal in XValues)
            {
                predictions.Add(FunctiontoFit(curVal));
            }
            return predictions;
        }
    }
    #endregion
  
}
