using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SolverFoundation;
using ShoNS.Optimizer;
using ShoNS.Numerics;

namespace GrowthCurveLibrary
{
    [Serializable]
    public class LogisticModel : AbstractFitter, IAbstractFitter
    {
        enum ParametersIndex : int { P0Index = 0, rIndex = 1,Carrying=2 };
        public double GrowthRate
        {
            get { return pParameters[(int)ParametersIndex.rIndex]; }
        }
        public double InitialPopSize
        {
            get { return pParameters[(int)ParametersIndex.P0Index]; }
        }

        public double CarryingCapacity
        {
            get { return pParameters[(int)ParametersIndex.Carrying]; }
        }
       
        
      
        public LogisticModel(double[] XDATA, double[] YDATA)
        {
            this.name = "Logistic";
            base.VerifyInput(XDATA, YDATA);
            this.FitByDefault = false;
            FitModel();
        }
        public double GetGrowthRateAtODValue(double x=0.15)
        {
        if (!SuccessfulFit)
            return double.NaN;
        return  this.GrowthRate * (1 - (1.0 / this.CarryingCapacity) * x);
         }
        public override double FunctiontoFit(double x)
        {
            double A = pParameters[(int)ParametersIndex.P0Index];
            double r = pParameters[(int)ParametersIndex.rIndex];
            double K = pParameters[(int)ParametersIndex.Carrying];
            double val = K * A * Math.Exp(r * x) / (K + A * (Math.Exp(r *x) - 0.1e1));
            return val;
        }

        public QuasiNewtonSolution results;
        public QuasiNewton QN;
        protected void FitModelAlgLib()
        {
            double epsf = 0;
            double epsx = 1e-10;
            int maxits = 0;
            int info;
            alglib.lsfitstate state;
            alglib.lsfitreport rep;
            double[,] nx=new double[x.Length,1];
            Enumerable.Range(0, x.Length).ToList().ForEach(b => nx[b, 0] = x[b]);
            //
            // Fitting without weights
            //
            double[] param = CreateInitialParameterGuess();
            alglib.lsfitcreatefg(nx, y, param, true, out state);
            alglib.lsfitsetcond(state, epsf, epsx, maxits);
            alglib.lsfitfit(state, function_cx_1_func, function_cx_1_grad, null, null);
            alglib.lsfitresults(state, out info, out param, out rep);
            AlgLibFind = param;
            pParameters = param;
            SuccessfulFit = true;
        }
        public static void function_cx_1_func(double[] c, double[] x, ref double func, object obj)
        {

            double A = c[(int)ParametersIndex.P0Index];
            double r = c[(int)ParametersIndex.rIndex];
            double K = c[(int)ParametersIndex.Carrying];
            double cx = x[0];
            // this callback calculates f(c,x)=exp(-c0*sqr(x0))
            // where x is a position on X-axis and c is adjustable parameter
            double ercx = Math.Exp(r * cx);
            func = K * A * ercx / (K + A * (ercx - 0.1e1));
        }
        public static void function_cx_1_grad(double[] c, double[] x, ref double func, double[] grad, object obj)
        {
            // this callback calculates f(c,x)=exp(-c0*sqr(x0)) and gradient G={df/dc[i]}
            // where x is a position on X-axis and c is adjustable parameter.
            // IMPORTANT: gradient is calculated with respect to C, not to X
            double A = c[(int)ParametersIndex.P0Index];
            double r = c[(int)ParametersIndex.rIndex];
            double K = c[(int)ParametersIndex.Carrying];
            double cx = x[0];
            double ercx = Math.Exp(r * cx);
            func = K * A * ercx / (K + A * (ercx - 0.1e1));

            grad[(int)ParametersIndex.Carrying] = A * A * ercx * (ercx - 0.1e1) * Math.Pow(K + A * ercx - A, -0.2e1);
            grad[(int)ParametersIndex.rIndex] = K * A * cx * ercx * (K - A) * Math.Pow(K + A *ercx - A, -0.2e1);
            grad[(int)ParametersIndex.P0Index]= K * K * ercx * Math.Pow(K + A * ercx - A, -0.2e1);

        }

        public double[] AlgLibFind = new double[3];
        protected override void FitModel()
        {
            FitModelAlgLib();
            //double r2 = calculateR2();
            ////TestGradient();
            ////makeYHAT();
            //r2 = calculateR2();
            //QN = new QuasiNewton();
            //QN.MaxIterations = 1000;
            //QN.Tolerance = 1e-10;
            //r2 = r2 - 0.0;
            //results = QN.MinimizeDetail(new DiffFunc(GetDerivatives), CreateInitialParameterGuess());
            //if (results.quality != Microsoft.SolverFoundation.Solvers.CompactQuasiNewtonSolutionQuality.LocalOptima)
            //{
            //    Console.WriteLine(results.quality.ToString());
            //    this.SuccessfulFit = false;
            //    pParameters = new double[] { Double.NaN, Double.NaN, Double.NaN };
            //    this.Comment = results.quality.ToString();
            //    //throw new Exception("Problem in Data Fitting!");
            //}
            //else
            //{
             
            //    pParameters = results.solution;
            //    makeYHAT();
            //    r2 = calculateR2();
            //    SuccessfulFit = true;
            //    this.Comment = results.quality.ToString();
            //}
        }
        private void TestGradient()
        {
            
            List<double> grad = new List<double>(3);
            pParameters = CreateInitialParameterGuess();
            grad.Add(0.0);
            grad.Add(0.0);
            grad.Add(0.0);
            double ss = GetDerivatives(pParameters, grad);
            double[] op = pParameters.ToArray();
            double[] difs = new double[3];
            double[] obsGrad = grad.ToArray();
            for (int i = 0; i < 3; i++)
            {
                double eps = 1e-6;
                pParameters = op.ToArray();
                pParameters[i] = op[i] + eps;

                double predict1 = GetDerivatives(pParameters, grad);
                pParameters[i] = op[i] - eps;

                double predict2 = GetDerivatives(pParameters, grad);
                double est = (predict1 - predict2) / (2 * eps);
                double dif = est - obsGrad[i];
                difs[i] = dif;
            }
            double q = difs.Sum();
            q = q / 1.0;

        }

        public double GetDerivatives(IList<double> value, IList<double> grad)
        {
            double A = value[(int)ParametersIndex.P0Index];
            double r = value[(int)ParametersIndex.rIndex];
            double K = value[(int)ParametersIndex.Carrying];
            //First the R Gradient
            double rGradient = 0.0;
            double AGradient = 0.0;
            double KGradient = 0.0;
            double ss = 0.0;
            for (int i = 0; i < x.Length; i++)
            {
                //ah, I accidentally swithched the x,y this is confusing.
                //Could also be much more efficient, just going to make sure it works now
                double cx = x[i];
                double cy = y[i];
                double ercx = Math.Exp(r * cx);
                ss+=  Math.Pow(K * A * ercx / (K + A * (ercx- 0.1e1)) - cy, 0.2e1);
                rGradient += 0.2e1 * (K * A * ercx - cy * K - cy * A * ercx + cy * A) * K * A * cx * ercx * (K - A) * Math.Pow(K + A * ercx - A, -0.3e1);
                AGradient += 0.2e1 * (K * A *ercx - cy * K - cy * A *ercx + cy * A) * K * K *ercx * Math.Pow(K + A * ercx - A, -0.3e1);
                double temp= 0.2e1 * (K * A *ercx - cy * K - cy * A * ercx + cy * A) * K * K * ercx * Math.Pow(K + A * ercx - A, -0.3e1);

                KGradient += 0.2e1 * (K * A * ercx - cy * K - cy * A * ercx + cy * A) * A * A * ercx * (ercx - 0.1e1) * Math.Pow(K + A *ercx - A, -0.3e1); ;

            }
            grad[(int)ParametersIndex.P0Index] = AGradient;
            grad[(int)ParametersIndex.rIndex] = rGradient;
            grad[(int)ParametersIndex.Carrying] = KGradient;
            return ss;
        }
        double[] CreateInitialParameterGuess()
        {
            pParameters = new double[3];
            LinearFit LF = new LinearFit(x, (from b in y select Math.Log(b)).ToArray());
            pParameters[(int)ParametersIndex.P0Index]=Math.Exp(LF.Intercept);
            pParameters[(int)ParametersIndex.rIndex] = LF.Slope;
            pParameters[(int)ParametersIndex.Carrying] = 5;
            return pParameters;
        }
    }
}
