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
    public class OffSetExponentialFit_OLD:AbstractFitter
    {
        QuasiNewton QN = new QuasiNewton();
        private double CParamGuess = -0.0002;
        public double Tolerance
        {
            get { return QN.Tolerance; }
            set { QN.Tolerance = value; }
        }
        enum ParametersIndex : int { P0Index = 0, rIndex = 1,OffSetIndex=2 };
        public double GrowthRate
        {
            get { return pParameters[(int)ParametersIndex.rIndex]; }
        }
        public double InitialPopSize
        {
            get { return pParameters[(int)ParametersIndex.P0Index]; }
        }
        public double OffSet
        {
            get { return pParameters[(int)ParametersIndex.OffSetIndex]; }
        }
        public OffSetExponentialFit_OLD(double[] XDATA, double[] YDATA,double CGuess=0.0)
        {
            this.name = "OffSetExponential";
            if ((XDATA.Length < 3 || YDATA.Length < 3) || (XDATA.Length != YDATA.Length))
            { throw new ArgumentOutOfRangeException("Offset exponential fit can't work with less then 3 points or unequal matrices"); }
              //deep copy the data to protect its integrity
            y = YDATA.ToArray();
            x = XDATA.ToArray();
            CParamGuess = CGuess;
            pParameters = new double[3] { GrowthCurve.BAD_DATA_VALUE, GrowthCurve.BAD_DATA_VALUE,GrowthCurve.BAD_DATA_VALUE };
            FitModel();
        }
        public override double FunctiontoFit(double x)
        {
            return InitialPopSize * Math.Exp(GrowthRate * x) + OffSet;
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
                double eps = 1e-4;
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
                double A=value[(int)ParametersIndex.P0Index];
                double r=value[(int)ParametersIndex.rIndex];
                double C=value[(int)ParametersIndex.OffSetIndex];
                //First the R Gradient
            double rGradient=0.0;
            double AGradient=0.0;
            double CGradient=0.0;
            double ss = 0.0;
            for(int i=0;i<x.Length;i++)
            {
                //ah, I accidentally swithched the x,y this is confusing.
                //Could also be much more efficient, just going to make sure it works now
                double cx=y[i];
                double cy=x[i];
                double ercy = Math.Exp(r * cy);
                ss += Math.Pow(cx - A * ercy - C, 2);
                rGradient += -0.2e1 * (cx - A * ercy - C) * A * cy * ercy;
                AGradient += -0.2e1 * (cx - A * ercy - C) * ercy;
                CGradient += -2.0 * cx + 0.2e1 * A * ercy + 2.0 * C;
               
            }
            grad[(int)ParametersIndex.P0Index]=AGradient;
            grad[(int)ParametersIndex.rIndex]=rGradient;
            grad[(int)ParametersIndex.OffSetIndex]=CGradient;
            return ss;
        }
        protected double[] CreateInitialParameterGuess()
        {
            double[] logdata = y.ToArray();
            logdata = Array.ConvertAll(logdata, z => Math.Log(z));
            LinearFit LF = new LinearFit(x, logdata);
            double[] ParamGuess = new double[3];
            ParamGuess[(int)ParametersIndex.rIndex] = LF.Slope; ;
            ParamGuess[(int)ParametersIndex.P0Index] = Math.Exp(LF.Intercept);
            ParamGuess[(int)ParametersIndex.OffSetIndex] = CParamGuess;
            return ParamGuess;
        }
        public QuasiNewtonSolution results;
        protected override void FitModel()
        {
            ///TestGradient();
            QN.MaxIterations = 500;
            QN.Tolerance = 1e-8;
            results=QN.MinimizeDetail(new DiffFunc(GetDerivatives), CreateInitialParameterGuess());
            if (results.quality != Microsoft.SolverFoundation.Solvers.CompactQuasiNewtonSolutionQuality.LocalOptima)
            {
                Console.WriteLine(results.quality.ToString());
                this.SuccessfulFit = false;
                pParameters = new double[] { Double.NaN, Double.NaN, Double.NaN };
                
                //throw new Exception("Problem in Data Fitting!");
            }
            else
            {
                pParameters = results.solution;
                SuccessfulFit = true;
            }
        }
    }
}
