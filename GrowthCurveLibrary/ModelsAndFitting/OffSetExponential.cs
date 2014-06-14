using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GrowthCurveLibrary
{
    [Serializable]
    public class OffSetExponentialFit:AbstractFitter
    {
        private double CParamGuess = -0.0002;

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
        public OffSetExponentialFit(double[] XDATA, double[] YDATA,double CGuess=0.0)
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
            var res = Enumerable.Zip(x, logdata, (z, yy) => new { x = z, yval = yy - CParamGuess });
            var res2=(from xx in res where xx.yval>0 select xx).ToList();
            double[] xToTry=(from yy in res2 select yy.x).ToArray();
            double[] yToTry=(from yy in res2 select yy.yval).ToArray();
            if (xToTry.Length >= 2)
            {
                logdata = Array.ConvertAll(yToTry, z => Math.Log(z));
                LinearFit LF = new LinearFit(xToTry, logdata);
                double[] ParamGuess = new double[3];
                ParamGuess[(int)ParametersIndex.rIndex] = LF.Slope; ;
                ParamGuess[(int)ParametersIndex.P0Index] = Math.Exp(LF.Intercept);
                ParamGuess[(int)ParametersIndex.OffSetIndex] = CParamGuess * 1.01;
                return ParamGuess;
            }
            else
            {
                throw new Exception("Exponential Offset model can't be fit with data that isn't above the parameter guess");
            }
            
        }
       
        protected override void FitModel()
        {

            FitModelAlgLib();
       
        }
        protected void FitModelAlgLib()
        {
            double epsf = 0;
            double epsx = 1e-10;
            int maxits = 0;
            int info;
            alglib.lsfitstate state;
            alglib.lsfitreport rep;
            double[,] nx = new double[x.Length, 1];
            Enumerable.Range(0, x.Length).ToList().ForEach(b => nx[b, 0] = x[b]);
            //
            // Fitting without weights
            //
            double[] param = CreateInitialParameterGuess();
            alglib.lsfitcreatefg(nx, y, param, true, out state);
            alglib.lsfitsetcond(state, epsf, epsx, maxits);
            alglib.lsfitfit(state, function_cx_1_func, function_cx_1_grad, null, null);
            alglib.lsfitresults(state, out info, out param, out rep);
            pParameters = param;
            SuccessfulFit = true;
        }
        public static void function_cx_1_func(double[] c, double[] x, ref double func, object obj)
        {

            double A = c[(int)ParametersIndex.P0Index];
            double r = c[(int)ParametersIndex.rIndex];
            double OffSet = c[(int)ParametersIndex.OffSetIndex];
            double cx = x[0];
            func= A * Math.Exp(r * cx) + OffSet;
           
        }
        public static void function_cx_1_grad(double[] c, double[] x, ref double func, double[] grad, object obj)
        {
            // this callback calculates f(c,x)=exp(-c0*sqr(x0)) and gradient G={df/dc[i]}
            // where x is a position on X-axis and c is adjustable parameter.
            // IMPORTANT: gradient is calculated with respect to C, not to X
            double A = c[(int)ParametersIndex.P0Index];
            double r = c[(int)ParametersIndex.rIndex];
            double C = c[(int)ParametersIndex.OffSetIndex];
            double cx = x[0];
            double ercx = Math.Exp(r * cx);
                
            func = A *ercx + C;

            
            //Actually appears not to be looking for derivatives of the function itself
            //not the sum of squares function, this is very confusing
            grad[(int)ParametersIndex.rIndex] =  A *cx* ercx ;
            grad[(int)ParametersIndex.P0Index] = ercx; 
            grad[(int)ParametersIndex.OffSetIndex] = 1.0;

        }
       

    }
}
