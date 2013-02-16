using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SolverFoundation;
using ShoNS.Optimizer;
using ShoNS.Numerics;
using ShoNS.Array;
using ShoNS.MathFunc;

namespace GrowthCurveLibrary
{
    /// <summary>
    /// Fits a collection of growth curves, trying to account for a time point dependent residual
    /// </summary>
    [Serializable]
    public class GroupFitter :IAbstractFitter
    {
        public bool FitByDefault { get; set; }
        public string Comment { get; set; }
        QuasiNewton QN = new QuasiNewton();
        List<double> xl = new List<double>();
        List<double> yl = new List<double>();        
        public Dictionary<string, LightWeightGC> NamesToCurves = new Dictionary<string, LightWeightGC>();
        public double Tolerance
        {
            get { return QN.Tolerance; }
            set { QN.Tolerance = value; }
        }
        //Vector of parameters, has a 
        double[] Parameters;
        public LightWeightGC[] data;
        double[] Times;
        DoubleArray daOffSets;
        public GroupFitter(GrowthCurveCollection GCC)
        {
            var tmpHold = new List<LightWeightGC>(); 
            HashSet<double> times = new HashSet<double>();
            Random r = new Random();
              
            foreach (GrowthCurve gc in GCC)
            {
                if (gc.ExpFit == null || gc.ExpFit.SuccessfulFit == false)
                {
                    throw new Exception("Curve " + gc.DataSetName + " has not been fit with an exponential yet");
                }
                LightWeightGC tmp = new LightWeightGC() { Xvalues = gc.FittedXValues, YValues = gc.FittedYValues, Name = gc.DataSetName };
                tmpHold.Add(tmp);
                NamesToCurves[gc.DataSetName] = tmp;
                xl.AddRange(gc.FittedXValues);
                yl.AddRange(gc.FittedYValues);
                gc.FittedXValues.ToList().ForEach(x => times.Add(x));
                gc.GroupFit = this;
               
            }
            //Now to make the data array
            int ParameterArraySize = times.Count + 2 * tmpHold.Count;

            var t2 = times.ToList();
            data = tmpHold.ToArray();
            t2.Sort();
            Times = t2.ToArray();
            //First items 1-n are the time point offsets, next are the GrowthRate,InitPop for the different guys
            Parameters = new double[ParameterArraySize];
            int ArrayStart = times.Count;
            for(int i=0;i<data.Length;i++)
            {
                if (i < Times.Length)
                {
                    //Parameters[i] = 1e-3;
                }
              
                data[i].GrowthParameterStart = ArrayStart + (i * 2);
                double v=  r.NextDouble()<.5 ? -1.0:1.0;
                Parameters[data[i].GrowthParameterStart] = GCC[i].ExpFit.GrowthRate;// +GCC[i].ExpFit.GrowthRate * (r.NextDouble() * .005) * v;
                v=  r.NextDouble()<.5 ? -1.0:1.0;
                Parameters[data[i].GrowthParameterStart + 1] = GCC[i].ExpFit.InitialPopSize;// +GCC[i].ExpFit.InitialPopSize * (r.NextDouble() * .005) * v;
                double minTime = data[i].Xvalues[0];
                data[i].timeParameterStart = t2.IndexOf(minTime);
            }
            double val=ShoNS.Optimizer.GradTester.Test(new DiffFunc(GetDerivatives),Parameters);
            double val3 = val + .01;
            val3++; 
            FitModel();
           
            
        }
        public double GetDerivatives(IList<double> parameters, IList<double> grad)
        {
            double ss = 0.0;                
            //First the R Gradient, think it should be zero
            for (int i = 0; i < grad.Count; i++)
            {
                grad[i] = 0;
            }
            foreach (LightWeightGC gc in data)
            {
                int TimeStart = gc.timeParameterStart;
                double GR = parameters[gc.GrowthParameterStart];
                double A = parameters[gc.GrowthParameterStart + 1];
                double[] x = gc.Xvalues;
                double[] y = gc.YValues;
                double rGradient=0;
                double AGradient = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    //ah, I accidentally swithched the x,y this is confusing.
                    //Could also be much more efficient, just going to make sure it works now
                    double cx = x[i];
                    double cy = y[i];
                    double curOffSet = parameters[TimeStart + i];
                    double ercx = Math.Exp(GR * cx);
                    double tmp = -0.2e1 * (cy - A * ercx - curOffSet) * ercx;
                    ss += Math.Pow(cy - A * ercx - curOffSet, 2);
                    rGradient += tmp * A * cx ;
                    AGradient += tmp;
                    grad[TimeStart+i] += -2.0 * cy + 0.2e1 * A * ercx + 2.0 * curOffSet;
                }
                grad[gc.GrowthParameterStart] = rGradient;
                grad[gc.GrowthParameterStart+1] = AGradient;
                
            }
            return ss*10000;
        }
       
        public QuasiNewtonSolution results;
        protected void FitModel()
        {
            QN.MaxIterations = 50000;
            QN.Tolerance = 1e-10;
            results = QN.MinimizeDetail(new DiffFunc(GetDerivatives), Parameters);
            Parameters = results.solution;
            SetParameterDictionary();
            if (results.quality != Microsoft.SolverFoundation.Solvers.CompactQuasiNewtonSolutionQuality.LocalOptima)
            {
                throw new Exception("Optimization failed\n" + results.quality.ToString());
            }
            else
            {
                SuccessfulFit = true;
            }
            
        }
        public Dictionary<string, ExpParameters> WellParameters=new Dictionary<string,ExpParameters>();
        private void SetParameterDictionary()
        {
            foreach (LightWeightGC gc in data)
            {
                double r = Parameters[gc.GrowthParameterStart];
                double a = Parameters[gc.GrowthParameterStart + 1];
                WellParameters[gc.Name] = new ExpParameters() { InitPop = a, GrowthRate = r };
            }
            daOffSets = DoubleArray.From(OffSetsAtTimes);
        }
        public bool SuccessfulFit { get; set; }
        public double[] TimeValues
        {
            get { return Times; }
        }
        public double CalculateRMSE(LightWeightGC gc)
        {
            var mse = CalculateResiduals(gc);
            double MSE = mse.ElementMultiply(mse).Sum();    
            double DataPoints = gc.Xvalues.Length;
            double NumParmeters = Parameters.Length;
            double df = DataPoints - NumParmeters;
            MSE = MSE/df;
            double RMSE = Math.Sqrt(MSE);
            return RMSE;
        }
        public DoubleArray CalculateResiduals(LightWeightGC gc)
        {
            return DoubleArray.From(gc.YValues).Subtract(Predict(gc));
        }
        public DoubleArray MakePredictionsWithoutOffSet(LightWeightGC gc)
        {
            ExpParameters ep =WellParameters[gc.Name];
            var b=gc.Xvalues.Select(x => Math.Exp(x * ep.GrowthRate));
            DoubleArray da = DoubleArray.From(b);
            da=da.Multiply(ep.InitPop);
            return da;

        }
        private DoubleArray Predict(LightWeightGC gc)
        {
            var x = MakePredictionsWithoutOffSet(gc);
            
            x=x.Add(daOffSets.GetSlice(gc.timeParameterStart,(gc.timeParameterStart+gc.Xvalues.Length-1)));
            return x;
        }
        public double[] OffSetsAtTimes
        {
            get
            {
                return Parameters.Take(Times.Length).ToArray();
                
            }
        }
        public class ExpParameters
        {
            public double InitPop, GrowthRate;
        }
        #region IAbstractFitter Members

        public double AbsError
        {
            get 
            {
                return calculateAbsError();
            }
        }

        private double pAbsError=1;
        public double calculateAbsError()
        {
            if (pAbsError < 0)
            {

                DoubleArray d = DoubleArray.From(Residuals);
                d = ShoNS.MathFunc.ArrayMath.Abs(d);
                pAbsError = d.Sum();
            }
            return pAbsError;
        }

        public double calculateResidualSumofSquares()
        {
            DoubleArray d = DoubleArray.From(Residuals);
            d= d.Multiply(d.T);
            return d.First();
        }

        double[] IAbstractFitter.Parameters
        {
            get { return Parameters.ToArray(); }
        }

        public double[] PredictedValues
        {
            get 
            {
                List<double> toRet = new List<double>(500);
                data.ToList().ForEach(x=>toRet.AddRange(Predict(x)));
                return toRet.ToArray();
            }
        }

        public double R2
        {
            get { return calculateR2(); }
        }
        private double pR2=Double.NaN;
        private double calculateR2()
        {
            if (!SuccessfulFit)
                return double.NaN;
            if (Double.IsNaN(pR2))
            {
                double SST, SSR;
                SSR = calculateResidualSumofSquares();
                double Ymean = yl.Average();
                SST = 0;
                for (int i = 0; i < xl.Count; i++)
                {
                    SST += Math.Pow((yl[i] - Ymean), 2.0);
                }
                pR2= (1.0 - (SSR / SST));
            }
            return pR2;
        }
        public double[] Residuals
        {
            get
            {
                List<double> toRet = new List<double>(500);
                data.ToList().ForEach(x => toRet.AddRange(CalculateResiduals(x)));
                return toRet.ToArray();
            }
        }

        public double RMSE
        {
            get
            {
                if (!SuccessfulFit)
                {
                    return double.NaN;
                }
                double MSE = calculateResidualSumofSquares();
                double DataPoints = xl.Count;
                double NumParmeters = Parameters.Length;
                double df = DataPoints - NumParmeters;
                MSE = MSE / df;
                double RMSE = Math.Sqrt(MSE);
                return RMSE;
            }
        }

        public double[] X
        {
            get { return xl.ToArray(); }
        }

        public double[] Y
        {
            get { return yl.ToArray(); }
        }

        #endregion
    }
    public class LightWeightGC
    {
        public double[] Xvalues;
        public string Name;
        public double[] YValues;
        public int timeParameterStart;
        public int GrowthParameterStart;
        
    }
}
