using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SolverFoundation;
using ShoNS.Optimizer;
using ShoNS.Numerics;
using ShoNS.Array;
using ShoNS.MathFunc;
//using MicrosoftResearch.Infer.Maths;
namespace GrowthCurveLibrary
{
    /// <summary>
    /// Mixture model for the error, assumes the mean of the error is zero for both components
    /// </summary>
    [Serializable]
    public class MixtureErrorModel 
    {

        enum ParametersIndex : int { P0Index = 0, rIndex = 1 };
        private double[] pParameters;
        public double GrowthRate
        {
            get { return  pParameters[(int)ParametersIndex.rIndex]; }
        }
        public double InitialPopSize
        {
            get { return pParameters[(int)ParametersIndex.P0Index]/AConditioningScale; }
        }
        public double[] sds=new double[] {Math.Sqrt(5.4775e-7),Math.Sqrt(4.267e-6)};
        public double[] Passignments=new double[] {.8416,.1854};
        public double[] startParams;
        double[] xs;
        double[] ys;
        double[,] zs;
        DoubleArray dxs, dys, predY;
        DoubleArray dpAs;
        DoubleArray dsds;
        
        double AConditioningScale;//Variable used to scale A so it is roughly equivalent with R.
        double curVal=double.MaxValue;
        int MaxIterations = 1000;
        double TerminationTolerance = 1e-6;
        public MixtureErrorModel(GrowthCurve gc, bool CallOutliersAfter=true)
        {
            xs = gc.FittedXValues;
            ys = gc.FittedYValues;
            zs = new double[ys.Length,2];
            dxs = DoubleArray.From(xs);
            dys = DoubleArray.From(ys);
            double curA = gc.ExpFit.InitialPopSize;
            double curR = gc.ExpFit.GrowthRate;
            AConditioningScale = curR / curA;
            pParameters = new double[] { curA*AConditioningScale, curR };
            startParams=pParameters.ToArray();
            startParams[0] = curA*AConditioningScale * .98;
            startParams[1] = curR * 1.02;
            dpAs=DoubleArray.From(Passignments);
            dsds=DoubleArray.From(sds);
            FitModel();
            var oxs = XValuesOfOutliers();
            foreach (var gd in gc)
            {
                if (oxs.Contains(gd.time_as_double))
                    gd.OutlierFlag = true;
            }

        }
        public List<double> XValuesOfOutliers()
        {
            //Probably would have been better to return indexes, but this seemed safer
            List<double> toR = new List<double>();
            //should be up to date, but just in case
            CalculateZExpectations();
            for (int i = 0; i < xs.Length; i++)
            {
                if (zs[i, 1] > .5) toR.Add(xs[i]);
            }
            return toR;
        }
        public double[] Residuals
        {
            get
            {
                CalculatePredScaled();
                return ys.Zip(predY,(x,y)=>x-y).ToArray();
            }
        }
        public double[] FittedXValues
        { get { return xs.ToArray(); } }
        private void FitModel()
        {
            TestGradient();
            double lastVal;
            int counts=-1;
           // List<double> LL = new List<double>();
            //List<double[]> b=new List<double[]>();
            curVal = CalculateZExpectationsScaled();
            do
            {
                counts++;
                lastVal = curVal;
               // LL.Add(lastVal);
              //  b.Add(pParameters);
                MaximizeGivenZExpectationsScaled();
                curVal = CalculateZExpectationsScaled();
            }
            while (Math.Abs(curVal - lastVal) > TerminationTolerance && counts < MaxIterations);

        }
        private void CalculatePredScaled()
        {
            predY = InitialPopSize* ArrayMath.Exp(dxs * GrowthRate);
        }
       
        //The E Step
        private double CalculateZExpectations()
        {
            //OLDER VALIDATION CODE
            //Console.WriteLine(InitialPopSize);
            //Console.WriteLine(GrowthRate);
            //int n = xs.Length;
            //double[] residualsaa = (xs.Zip(ys, (x,y)=>Math.Pow((y-InitialPopSize*Math.Exp(x*GrowthRate)),2.0)).ToArray());
            //double var1 = -2 * sds[0] * sds[0];
            //double var2 = -2 * sds[1] * sds[1];
            //var residuals1a = residualsaa.Select(x =>x/ var1).ToArray();
            //var residuals2a = residualsaa.Select(x => x / var2).ToArray();
            //double w1 = Passignments[0] / sds[0];
            //double w2 = Passignments[1] / sds[1];
            //double loglik=0;
            //for (int i = 0; i < n; i++)
            //{
            //    loglik += Math.Log(w1 * Math.Exp(residuals1a[i]) + w2 * Math.Exp(residuals2a[i]));
            //}

            CalculatePred();
            var residuals = ArrayMath.Pow(dys - predY, 2.0);
            var residuals1=residuals/(-2*dsds[0]*dsds[0]);
            //Console.WriteLine(residuals);
            var residuals2 = residuals / (-2 * dsds[1] * dsds[1]);
            //Console.WriteLine(dsds[1]);
            var weights = dpAs.ElementDivide(dsds);
            var t1=weights[0]*ArrayMath.Exp(residuals1);
            var t2 =weights[1]*ArrayMath.Exp(residuals2);
            var l = t1.Zip(t2, (x, y) => (x+ y));
            var ll = l.Select(x => Math.Log(x)).Sum();
            var denom = t1 + t2;
            t1 = t1.ElementDivide(denom);
            t2 = t2.ElementDivide(denom);
            for(int i=0;i<ys.Length;i++)
            {
                zs[i, 0] = t1[i];
                zs[i, 1] = t2[i];
            }
            //return the log likelihood
            return ll; 
        }
        private double CalculateZExpectationsScaled()
        {   
            CalculatePredScaled();
            var residuals = ArrayMath.Pow(dys - predY, 2.0);
            var residuals1 = residuals / (-2 * dsds[0] * dsds[0]);
            //Console.WriteLine(residuals);
            var residuals2 = residuals / (-2 * dsds[1] * dsds[1]);
            //Console.WriteLine(dsds[1]);
            var weights = dpAs.ElementDivide(dsds);
            var t1 = weights[0] * ArrayMath.Exp(residuals1);
            var t2 = weights[1] * ArrayMath.Exp(residuals2);
            var l = t1.Zip(t2, (x, y) => (x + y));
            var ll = l.Select(x => Math.Log(x)).Sum();
            var denom = t1 + t2;
            t1 = t1.ElementDivide(denom);
            t2 = t2.ElementDivide(denom);
            for (int i = 0; i < ys.Length; i++)
            {
                zs[i, 0] = t1[i];
                zs[i, 1] = t2[i];
            }
            //return the log likelihood
            return ll;
        }
        class CurrentBestSolution
        {
           public double curBestFunction = double.MaxValue;
           public double[] curBestParameters;
           public double GradientL2NormAtBest;
        }
        CurrentBestSolution currentWinner;
        //The M Step - Much better numeric properties if A and R are put on the same scale at the start
        //of the process
        private void MaximizeGivenZExpectationsScaled()
        {
            
            //Recreating as I didn't know if it remembered the Hessian
            QN = new QuasiNewton();
            QN.MaxIterations = MaxIterations;
            QN.Tolerance = TerminationTolerance;
            currentWinner = new CurrentBestSolution() { curBestFunction = double.MaxValue };

            // results = QN.MinimizeDetail(new DiffFunc(GetDerivatives), new double[] {InitialPopSize,GrowthRate});
            results = QN.MinimizeDetail(new DiffFunc(GetDerivativesScaled), startParams);

            pParameters = results.solution;
            curVal = results.funcValue;
            if (results.quality != Microsoft.SolverFoundation.Solvers.CompactQuasiNewtonSolutionQuality.LocalOptima)
            {
                //Sometimes it went to the local optimum, but didn't seem to think so.
                if (results.quality == Microsoft.SolverFoundation.Solvers.CompactQuasiNewtonSolutionQuality.UserCalculationError &&
                   currentWinner.GradientL2NormAtBest < .002)
                {
                    pParameters = currentWinner.curBestParameters;
                    curVal = currentWinner.curBestFunction;
                }
                else { throw new Exception("M Step Failed to Converge during Solving"); }
            }
        }
        QuasiNewton QN;
        public QuasiNewtonSolution results;
        //HashSet<double> Qualities = new HashSet<double>();
        //List<double> Gradients = new List<double>();
        private double GetDerivatives(IList<double> value, IList<double> grad)
        {

            double A = value[(int)ParametersIndex.P0Index];
            double r = value[(int)ParametersIndex.rIndex];
            
            //First the R Gradient
            double rGradient = 0.0;
            double AGradient = 0.0;
            double ss = 0.0;
            for (int i = 0; i < xs.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    double cy = ys[i];
                    double cx = xs[i];
                    double cz = zs[i, j];
                    double csd = sds[j];
                    double cvd = Math.Pow(csd, 2.0);
                    double ercx = Math.Exp(r * cx);
                    ss += cz * Math.Pow(cy - A * ercx, 2) / (2 * cvd);
                    rGradient += (-cz * (cy - A * ercx) * A * cx * ercx ) / cvd;
                    AGradient += (-cz * (cy - A * ercx) *  ercx) / cvd;
                }
            }
            grad[(int)ParametersIndex.P0Index] = AGradient;
            grad[(int)ParametersIndex.rIndex] = rGradient;
            if (currentWinner==null || ss < currentWinner.curBestFunction)
            {
                var b = grad.ToList();
                var qq=from x in b select Math.Pow(x,2);
                double norm = (qq.Sum() / 2);
                currentWinner = new CurrentBestSolution() { curBestFunction = ss, curBestParameters = value.ToArray(), GradientL2NormAtBest = norm };
            }
            return ss;
        }
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
        public double FunctiontoFit(double x)
        {
            double y = InitialPopSize * Math.Exp(GrowthRate * x);
            return y;
        }


        #region OLDCODE

        /// <summary>
        /// OLDER METHODS
        /// </summary>
        /// <param name="value"></param>
        /// <param name="grad"></param>
        /// <returns></returns>
        /// 
        private void CalculatePred()
        {
            predY = InitialPopSize * ArrayMath.Exp(dxs * GrowthRate);
        }
        //The M Step
        private void MaximizeGivenZExpectations()
        {
            //Solver doesn't work well, but is here.
            //LBFGS solver = new LBFGS(2);
            //FunctionEval ff = new FunctionEval(GetDerivativesLBFGS);
            //Vector x0 = Vector.FromArray(new double[] { InitialPopSize, GrowthRate });
            //solver.debug = true;
            //solver.linesearchDebug = true;
            //solver.Run(x0,1, ff);
            //pParameters = x0.ToArray();
            //var b = solver.convergenceCriteria;

            //Recreating as I didn't know if it remembered the Hessian
            QN = new QuasiNewton();
            QN.MaxIterations = MaxIterations;
            QN.Tolerance = TerminationTolerance;
            currentWinner = new CurrentBestSolution() { curBestFunction = double.MaxValue };

            // results = QN.MinimizeDetail(new DiffFunc(GetDerivatives), new double[] {InitialPopSize,GrowthRate});
            results = QN.MinimizeDetail(new DiffFunc(GetDerivatives), startParams);

            pParameters = results.solution;
            curVal = results.funcValue;
            if (results.quality != Microsoft.SolverFoundation.Solvers.CompactQuasiNewtonSolutionQuality.LocalOptima)
            {
                //Sometimes it went to the local optimum, but didn't seem to think so.
                if (results.quality == Microsoft.SolverFoundation.Solvers.CompactQuasiNewtonSolutionQuality.UserCalculationError &&
                   currentWinner.GradientL2NormAtBest < .002)
                {
                    pParameters = currentWinner.curBestParameters;
                    curVal = currentWinner.curBestFunction;
                }
                else { throw new Exception("M Step Failed to Converge during Solving"); }
            }
        }
       
        private double GetDerivativesScaled(IList<double> value, IList<double> grad)
        {

            double A = value[(int)ParametersIndex.P0Index]/AConditioningScale;
            double r = value[(int)ParametersIndex.rIndex];

            //First the R Gradient
            double rGradient = 0.0;
            double AGradient = 0.0;
            double ss = 0.0;
            for (int i = 0; i < xs.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    double cy = ys[i];
                    double cx = xs[i];
                    double cz = zs[i, j];
                    double csd = sds[j];
                    double cvd = Math.Pow(csd, 2.0);
                    double ercx = Math.Exp(r * cx);
                    ss += cz * Math.Pow(cy - A * ercx, 2) / (2 * cvd);
                    cvd *= AConditioningScale;
                    rGradient += (-cz * (cy - A * ercx) * A*AConditioningScale * cx * ercx) / cvd;
                    AGradient += (-cz * (cy - A * ercx) * ercx) / cvd;
                }
            }
            grad[(int)ParametersIndex.P0Index] = AGradient;
            grad[(int)ParametersIndex.rIndex] = rGradient;
            if (currentWinner == null || ss < currentWinner.curBestFunction)
            {
                var b = grad.ToList();
                var qq = from x in b select Math.Pow(x, 2);
                double norm = (qq.Sum() / 2);
                currentWinner = new CurrentBestSolution() { curBestFunction = ss, curBestParameters = value.ToArray(), GradientL2NormAtBest = norm };
            }
            return ss;
        }

        //Log transform forces growth rate to be positive, as well as init pop size
        private double GetDerivativesLOG(IList<double> value, IList<double> grad)
        {

            double origA = value[(int)ParametersIndex.P0Index];
            double A = Math.Exp(origA);
            double origr = value[(int)ParametersIndex.rIndex];
            double r = Math.Exp(origr);

            //First the R Gradient
            double rGradient = 0.0;
            double AGradient = 0.0;
            double ss = 0.0;
            for (int i = 0; i < xs.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    double cy = ys[i];
                    double cx = xs[i];
                    double cz = zs[i,j];
                    double csd =sds[j];
                    double cvd=Math.Pow(csd,2.0);
                    double ercx = Math.Exp(r * cx);
                    //ss += -cz*(Math.Log(Passignments[j])-Math.Log(csd)-.5*Math.Pow(cy - A * ercx, 2)/(2*cvd));
                    ss += cz * Math.Pow(cy - A * ercx, 2) / (2*cvd);
                    double val = (-cz * (cy - A * ercx) * A * cx * ercx * r) / cvd;
                    val += 1;
                    rGradient += (-cz * (cy - A * ercx) * A * cx * ercx *r) / cvd;
                    AGradient += (-cz * (cy - A * ercx ) *A* ercx)/cvd;
                }
            }
            grad[(int)ParametersIndex.P0Index] = AGradient;
            grad[(int)ParametersIndex.rIndex] = rGradient;
            if (ss > currentWinner.curBestFunction)
            {   
                //Qualities.Add(ss);
                var b = grad.ToList();
                b.ForEach(x => Math.Pow(x, 2));
                double norm=(b.Sum() / 2);
                currentWinner = new CurrentBestSolution() { curBestFunction = ss, curBestParameters = value.ToArray(), GradientL2NormAtBest = norm };
            }
            return ss;
        }
        //private double GetDerivativesLBFGS(Vector value, ref Vector grad)
        //{
        //    double A = value[(int)ParametersIndex.P0Index];
        //    double r = value[(int)ParametersIndex.rIndex];
        //    //First the R Gradient
        //    double rGradient = 0.0;
        //    double AGradient = 0.0;
        //    double ss = 0.0;
        //    for (int i = 0; i < xs.Length; i++)
        //    {
        //        for (int j = 0; j < 2; j++)
        //        {
        //            double cy = ys[i];
        //            double cx = xs[i];
        //            double cz = zs[i, j];
        //            double csd = sds[j];
        //            double cvd = Math.Pow(csd, 2.0);
        //            double ercx = Math.Exp(r * cx);
        //            //ss += -cz*(Math.Log(Passignments[j])-Math.Log(csd)-.5*Math.Pow(cy - A * ercx, 2)/(2*cvd));
        //            ss += cz * Math.Pow(cy - A * ercx, 2) / (2 * cvd);

        //            rGradient += (-cz * (cy - A * ercx) * A * cx * ercx) / cvd;
        //            AGradient += (-cz * (cy - A * ercx) * ercx) / cvd;
        //        }
        //    }
        //    grad[(int)ParametersIndex.P0Index] = AGradient;
        //    grad[(int)ParametersIndex.rIndex] = rGradient;
        //   // Qualities.Add(ss);
        //    //var b = grad.ToList();
        //   // b.ForEach(x => Math.Pow(x, 2));
        //   // Gradients.Add(b.Sum() / 2);
        //    return ss;
        //}
        private void CalculatePredLog()
        {
            predY = Math.Exp(InitialPopSize) * ArrayMath.Exp(dxs * Math.Exp(GrowthRate));
        }
        private void TestGradient()
        {
            currentWinner = new CurrentBestSolution() { curBestFunction = double.MaxValue };
            //pParameters = new double[] { .001013, .1628 };
            List<double> grad = new List<double>(2);
            CalculateZExpectationsScaled();
            grad.Add(0.0);
            grad.Add(0.0);

            double ss = GetDerivativesScaled(pParameters, grad);
            double[] op = pParameters.ToArray();
            double[] difs = new double[2];
            double[] obsGrad = grad.ToArray();
            for (int i = 0; i < 2; i++)
            {
                double eps = 1e-8;
                pParameters = op.ToArray();
                pParameters[i] = op[i] + eps;
                double predict1 = GetDerivativesScaled(pParameters, grad);
                pParameters[i] = op[i] - eps;
                double predict2 = GetDerivativesScaled(pParameters, grad);
                double est = (predict1 - predict2) / (2 * eps);
                double dif = est - obsGrad[i];
                difs[i] = dif;
            }
            double q = difs.Sum();
            q = q / 1.0;

        }
        #endregion
    }


}
