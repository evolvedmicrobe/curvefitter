using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;


namespace GrowthCurveLibrary
{

    //ABSTRACT CLASSES TO BE INHERITED FROM
  
  
    #region NonLinearFitter
    /// <summary>
    /// A wrapper class for the alglib functions, used to fit non-linear models
    /// 
    /// Poorly named class, see fitting method for comments as to why, this no longer uses hessian
    /// </summary>
    [Serializable]
    public abstract class NonLinearFitterWithGradientAndHessian : AbstractFitter
    {
        public enum TerminationType : int { NotYetSet = -2, WrongParams = -1, SumOfSquaresChangeConverge = 1, ParameterConverge = 2, MaxIterations = 5 };
        protected Delegates.DerivativePortionCalculator[,] HessianFunctions;
        /// <summary>
        /// stopping criterion. Algorithm stops if
        /// |X(k+1)-X(k)| <= EpsX*(1+|X(k)|)
        /// </summary>
        protected double EPSX;
        /// <summary>
        /// Fill in the Hessian matrix for the sum of squares function
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="Hessian"></param>
        private void EvaluateHessian(double[] Params, ref double func, double[] grad,double[,] Hessian,object obj)
        {
           
            EvaluateSumOfSquaresGradient(Params,ref func,grad,obj);
            int n = Params.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    Delegates.DerivativePortionCalculator calcDeriv = HessianFunctions[i, j];
                    Hessian[i, j] = EvaluateDerivative(Params, calcDeriv);
                    if (i != j)
                    { Hessian[j, i] = Hessian[i, j]; }
                }
            }
        }
        /// <summary>
        /// Since for sum of squares the method is simply a sum over each of the components, this general method performs the loop
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="calc"></param>
        /// <returns></returns>
        private double EvaluateDerivative(double[] Params, Delegates.DerivativePortionCalculator calc)
        {
            double sum = 0;
            for (int i = 0; i < Params.Length; i++)
            {
                sum += calc(Params, x[i], y[i]);
            }
            return sum;
        }
        /// <summary>
        /// Used by the fitting routine to evaluate possible values,
        /// meant to stay compatible with alg lig
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        protected abstract void EvaluateSumofSquaresFunction(double[] Params,ref double func,object obj);
        protected abstract void EvaluateSumofSquaresFunction22(double[] Params, double[] func, object obj);
        
        protected abstract void EvaluateSumOfSquaresGradient(double[] Params, ref double func, double[] GradientVec,object obj);
        protected abstract void EvaluateSumOfSquaresGradient22(double[] Params, double[] fi, double[,] GradientVec, object obj);
        
        protected abstract void CreateHessianDelegates();
        protected abstract double[] CreateInitialParameterGuess();
        /// <summary>
        /// See the example in alglib on levenberg marquadt to understand this code.
        /// Based entirely on their example, somethings kept around for compatibility
        /// </summary>
        /// <returns></returns>
        protected alglib.minlmreport GeneralLMFitting()
        {
            alglib.minlmstate state;
            alglib.minlmreport rep;
            double[] InitValues = CreateInitialParameterGuess();
            double epsg = 0.000000001;
            double epsf = 0;
            double epsx = 0;
            int maxits = 10000;

            //Gradient and hessian - Broken, probably have to rescale variable
            //lglib.minlmcreatefgh(InitValues, out state);
            //alglib.minlmsetcond(state, epsg, epsf, epsx, maxits);
            //alglib.minlmoptimize(state, EvaluateSumofSquaresFunction, EvaluateSumOfSquaresGradient, EvaluateHessian, null, null);
            //alglib.minlmresults(state, out InitValues, out rep);

            /////No gradient of hessian -This also seems to work just fine
            alglib.minlmcreatev(x.Length, InitValues, 0.0001, out state);
            alglib.minlmsetcond(state, epsg, epsf, epsx, maxits);
            alglib.minlmoptimize(state, EvaluateSumofSquaresFunction22, null, null);
            alglib.minlmresults(state, out InitValues, out rep);

           

            //Just gradient
            alglib.minlmcreatevj(x.Length, InitValues, out state);
            alglib.minlmsetcond(state, epsg, epsf, epsx, maxits);
           
            alglib.minlmoptimize(state, EvaluateSumofSquaresFunction22, EvaluateSumOfSquaresGradient22, null, null);
            alglib.minlmresults(state, out InitValues, out rep);

            this.pParameters = InitValues;

            
            return rep;
            
         

        }
    }
    #endregion
    #region ExponentialFit
    [Serializable]
    public class ExponentialFit : NonLinearFitterWithGradientAndHessian
    {
        //the first parameter is the constant, the second is the exponential growth rate
        enum ParametersIndex : int { P0Index = 0, rIndex = 1 };
        public TerminationType ReasonMarquadtEnded = TerminationType.NotYetSet;
        public double GrowthRate
        {
            get { return pParameters[(int)ParametersIndex.rIndex]; }
        }
        public double InitialPopSize
        {
            get { return pParameters[(int)ParametersIndex.P0Index]; }
        }
        public ExponentialFit(double[] XDATA, double[] YDATA)
        {
            this.name = "Exp";
            EPSX = .0000000001;

            if ((XDATA.Length < 2 || YDATA.Length < 2) || (XDATA.Length != YDATA.Length))
            { throw new ArgumentOutOfRangeException("Exponential fit can't work with less then 2 points or unequal matrices"); }
            if (XDATA.Length != YDATA.Length)
                throw new ArgumentException("Arrays of unequal size were passed to the non-linear fitter");
            //deep copy the data to protect its integrity
            y = YDATA.ToArray();
            x = XDATA.ToArray();
            pParameters = new double[2] { GrowthCurve.BAD_DATA_VALUE, GrowthCurve.BAD_DATA_VALUE};
            CreateHessianDelegates();
            FitModel();
        }
        public double CalculateLag( double initOD)
        {
            if (this.SuccessfulFit)
            {
                double x = Math.Log(initOD / this.InitialPopSize) / this.GrowthRate;
                return x;
            }
            else
            {
                return Double.NaN;
            }
        }
        protected override void FitModel()
        {
            //Add a fitting variable


            alglib.minlmreport mla = GeneralLMFitting();
           
            TerminationType term = (TerminationType)Enum.ToObject(typeof(TerminationType), mla.terminationtype);
            ReasonMarquadtEnded = term;
            //bad fit
            if (term == TerminationType.MaxIterations || term == TerminationType.WrongParams)
            {
                SuccessfulFit = false;
                pParameters = null;
            }
            else
            {
                SuccessfulFit = true;
                makeYHAT();
            }
        }
        public override string ToString()
        {
            return "Exponential";
        }
        public override double FunctiontoFit(double x)
        {
            double y = pParameters[(int)ParametersIndex.P0Index] * Math.Exp(pParameters[(int)ParametersIndex.rIndex] * x);
            return y;
        }
        protected override void EvaluateSumofSquaresFunction(double[] Params, ref double func, object obj)
        {
            
            //Calculate the sum  of squares for each point
            double SS = 0;
            double r = Params[(int)ParametersIndex.rIndex];
            double a = Params[(int)ParametersIndex.P0Index];
            for (int i = 0; i < x.Length & i < y.Length; i++)
            {
                double yPred = a * Math.Exp(r * x[i]);
                double yDif = Math.Pow((yPred - y[i]), 2.0);
                SS += yDif;
            }
            func= SS;
        }
        protected override void EvaluateSumofSquaresFunction22(double[] Params, double[] func, object obj)
        {

            //Calculate the sum  of squares for each point
            double r = Params[(int)ParametersIndex.rIndex];
            double a = Params[(int)ParametersIndex.P0Index];
            for (int i = 0; i < x.Length & i < y.Length; i++)
            {
                double yPred = a * Math.Exp(r * x[i]);
                double yDif = yPred - y[i];
                func[i]= yDif;
            }
        }        
        protected override void EvaluateSumOfSquaresGradient(double[] Params, ref double func, double[] GradientVec,object obj)
        {
            EvaluateSumofSquaresFunction(Params,ref func,obj);
            double A, b;
            double sum = 0;

            A = Params[(int)ParametersIndex.P0Index];
            b = Params[(int)ParametersIndex.rIndex];
            //the A gradient
            for (int i = 0; i < x.Length; i++)
            {
                sum += -0.2e1 * (y[i] - A * Math.Exp(b * x[i])) * Math.Exp(b * x[i]);
            }
            GradientVec[(int)ParametersIndex.P0Index] = sum;
            //the B gradient 
            sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += -2.0 * (y[i] - A * Math.Exp(b * x[i])) * A * x[i] * Math.Exp(b * x[i]);
            }
            GradientVec[(int)ParametersIndex.rIndex] = sum;
        }
        protected override void EvaluateSumOfSquaresGradient22(double[] Params, double[] fi, double[,] Gradient, object obj)
        {
            EvaluateSumofSquaresFunction22(Params, fi, obj);
            double A, b;
           

            int Aindex = (int)ParametersIndex.P0Index;
            int Bindex = (int)ParametersIndex.rIndex;
            A = Params[(int)ParametersIndex.P0Index];
            b = Params[(int)ParametersIndex.rIndex];
            //the A gradient
            for (int i = 0; i < x.Length; i++)
            {
                Gradient[i,Aindex] = -0.2e1 * (y[i] - A * Math.Exp(b * x[i])) * Math.Exp(b * x[i]);
            }
            //the B gradient 
            for (int i = 0; i < x.Length; i++)
            {
                Gradient[i, Bindex] = -2.0 * (y[i] - A * Math.Exp(b * x[i])) * A * x[i] * Math.Exp(b * x[i]);
            }
           
        }
        /// <summary>
        /// Creates an upper triangular array of delegates to calculate portions of each element of the hessian matrix
        /// </summary>
        protected override void CreateHessianDelegates()
        {
            HessianFunctions = new Delegates.DerivativePortionCalculator[pParameters.Length, pParameters.Length];
            HessianFunctions[0, 0] = new Delegates.DerivativePortionCalculator(CalculateAADerivateComponent);
            HessianFunctions[0, 1] = new Delegates.DerivativePortionCalculator(CalculateABDerivativeComponent);
            HessianFunctions[1, 1] = new Delegates.DerivativePortionCalculator(CalculateBBDerivativeComponent);
        }
        /// <summary>
        /// Makes an initial guess based on linear regreesion
        /// </summary>
        /// <returns></returns>
        protected override double[] CreateInitialParameterGuess()
        {
            double[] logdata = y.ToArray();
            logdata = Array.ConvertAll(logdata, z => Math.Log(z));
            LinearFit LF = new LinearFit(x, logdata);
            double[] ParamGuess = new double[2];
            ParamGuess[(int)ParametersIndex.rIndex] =  LF.Slope; 
            ParamGuess[(int)ParametersIndex.P0Index] = Math.Exp(LF.Intercept);
            return ParamGuess;
        }
        #region DoubleDerivative Functions
        /// <summary>
        /// dy^2/dA^2
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private double CalculateAADerivateComponent(double[] Params, double x, double y)
        {
            double b = Params[(int)ParametersIndex.rIndex];
            double value = 2.0 * Math.Exp(2.0 * b * x);
            return value;
        }
        /// <summary>
        /// dy^2/dA,dB
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private double CalculateABDerivativeComponent(double[] Params, double x, double y)
        {
            double b = Params[(int)ParametersIndex.rIndex];
            double A = Params[(int)ParametersIndex.P0Index];
            //Old formula below which I think was incorrect
            double nalue = -0.2e1 * (y - A * Math.Exp(b * x)) * A * x * Math.Exp(b * x);
            double value = 0.2e1 * x * Math.Exp(b * x) * (0.2e1 * A * Math.Exp(b * x) - y);
            double q = nalue + 1.0;
            return value;
        }
        /// <summary>
        /// Calculates one portion of dy^2/dBdB
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private double CalculateBBDerivativeComponent(double[] Params, double x, double y)
        {
            double b = Params[(int)ParametersIndex.rIndex];
            double A = Params[(int)ParametersIndex.P0Index];
            //Old formula below, the one beneath it I think is simpler
            //double value = 0.2e1 * A * A * Math.Pow(x, 0.2e1) * Math.Pow(Math.Exp(b * x), 0.2e1) - 0.2e1 * (y - A * Math.Exp(b * x)) * A * Math.Pow(x, 0.2e1) * Math.Exp(b * x);

            double value = 0.2e1 * A * x * x * Math.Exp(b * x) * (0.2e1 * A * Math.Exp(b * x) - y);
            return value;
        }
        #endregion
        public double GetXValueAtYValue(double yValue)
        {
            if (!SuccessfulFit)
            { return GrowthCurve.BAD_DATA_VALUE; }
            else
            {
                double t = Math.Log(yValue / Parameters[(int)ParametersIndex.P0Index]) / Parameters[(int)ParametersIndex.rIndex];
                return t;
            }
        }
    }
    #endregion


    #region LinearFit
    [Serializable]
    public class LinearFit : AbstractFitter
    {
        public double XOFFSET;
        public override string ToString()
        {
            return "Linear Fit";    
        }
        public LinearFit(double[] XGOOD, double[] YGOOD)
        {
            name = "Linear";
            if ((XGOOD.Length < 2 || YGOOD.Length < 2) || (XGOOD.Length != YGOOD.Length))
            { throw new Exception("Linear fit can't work with less then 2 points or unequal matrices"); }
            if (XGOOD.Count(n => !SimpleFunctions.IsARealNumber(n)) > 0 || YGOOD.Count(n => !SimpleFunctions.IsARealNumber(n)) > 0)
            { throw new Exception("Linear fit can't work on data that contains non real numbers"); }
            x = XGOOD;
            y = YGOOD;
            pParameters = new double[2];//linear model
            FitModel();

        }
        public override double calculateAbsError()
        {
            //WARNING DOES NOT CALCULATE THE ABSERROR OF A LINEAR FIT!!!!!
            //TO DO THAT SIMPLY DELETE THIS METHOD!!!!
            //I am using this because before this would calculate the 
            //ABSERROR IN SEMI LOG SPACE for the growth curve fitter, but I wanted it in linear space
            makeYHAT();
            double Error = 0;
            for (int i = 0; i < ypred.Length; i++)
            {
                Error += Math.Abs(Math.Exp(ypred[i]) - Math.Exp(y[i]));
            }
            return Error;
        }
        public double CalculateAbsErrorAfterExpTransform()
        {
            var pred = this.PredictedValues.Select(x => Math.Exp(x));
            var actual = this.y.Select(x => Math.Exp(x));
            return Enumerable.Zip(pred,actual,(x,y)=>Math.Abs(x-y)).Sum();
        }
        public double CalculateL2ErrorAfterExpTransform()
        {
            var pred = this.PredictedValues.Select(x => Math.Exp(x));
            var actual = this.y.Select(x => Math.Exp(x));
            return Enumerable.Zip(pred, actual, (x, y) => Math.Pow((x - y),2.0)).Sum();
        }
        public double[] ReturnResidualsAfterExpTransform()
        {
            var pred = this.PredictedValues.Select(x => Math.Exp(x));
            var actual = this.y.Select(x => Math.Exp(x));
            return Enumerable.Zip(pred, actual, (x, y) => (x - y)).ToArray();
        }
        protected override void FitModel()
        {
            //First to calculate m
            double n = x.Length;
            double SumofSQX = 0, SumofSQY = 0, SumofX = 0, SumofY = 0, SumofXY = 0;
            for (int i = 0; i < n; i++)
            {
                SumofSQX += Math.Pow(x[i], (double)2);
                SumofSQY += Math.Pow(y[i], (double)2);
                SumofX += x[i];
                SumofY += y[i];
                SumofXY += x[i] * y[i];
            }
            //Calculate the slope
            pParameters[1] = (n * SumofXY - (SumofX * SumofY)) / ((n * SumofSQX) - Math.Pow(SumofX, 2));//the slope
            pParameters[0] = (SumofY - pParameters[1] * SumofX) / n;//the constant
            SuccessfulFit = true;
            makeYHAT();
        }
        public override double FunctiontoFit(double x)
        {
            double y = pParameters[0] + pParameters[1] * x;
            return y;
        }
        enum ParameterIndexes { Intercept = 0, Slope = 1 };
        public double Slope
        {
            get { return pParameters[(int)ParameterIndexes.Slope]; }
        }
        public double Intercept
        {
            get { return pParameters[(int)ParameterIndexes.Intercept]; }
        }


    }
    #endregion

    #region Delegates
    [Serializable]
    public class Delegates
    {
        public delegate void FunctionToFit(double x, double[] a, out double y, double[] dyda);
        public delegate double DerivativePortionCalculator(double[] Params, double x, double y);

    }
    #endregion

    #region SimpleFunctions
    public class SimpleFunctions
    {
        public static void CleanNonRealNumbersFromYvaluesInXYPair(ref double[] x, ref double[] y)
        {
            if ((x.Length != y.Length) | x.Rank != 1 | y.Rank != 1) { throw new Exception("This XY pair is sized wrong"); }
            ArrayList NewXValues = new ArrayList(x.Length);
            ArrayList NewYValues = new ArrayList(y.Length);
            //int toRemoveIndex=new int[x.Length];
            int countToRemove = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (Double.IsPositiveInfinity(x[i]) || Double.IsNegativeInfinity(x[i]) || Double.IsNaN(x[i]) || Double.IsPositiveInfinity(y[i]) || Double.IsNegativeInfinity(y[i]) || Double.IsNaN(y[i]))
                {

                    countToRemove++;
                    //toRemoveIndex[countToRemove] = i;
                }
                else
                {
                    NewXValues.Add(x[i]);
                    NewYValues.Add(y[i]);
                }
            }
            x = new double[x.Length - countToRemove];
            y = new double[x.Length - countToRemove];
            NewYValues.CopyTo(y);
            NewXValues.CopyTo(x);
        }
        public static bool IsARealNumber(double value)
        {
            bool toReturn = true;
            if (Double.IsNaN(value) || Double.IsNegativeInfinity(value) || Double.IsPositiveInfinity(value))
            {
                toReturn = false;
            }
            return toReturn;
        }
        public static double Max(double[] values)
        {
            double res = values[0];
            foreach (double x in values) if (x > res) res = x;
            return res;
        }
        public static double Min(double[] values)
        {
            double res = values[0];
            foreach (double x in values) if (x < res) res = x;
            return res;
        }
        public static double MinNotZero(double[] values)
        {
            double res = Max(values);
            foreach (double x in values) if (x < res && x != 0) res = x;
            return res;
        }
        public static bool ValueInArray(double[] array, double val)
        {
            bool IsIT = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == val) { IsIT = true; break; }
            }
            return IsIT;
        }
        public static bool ValueInArray<T>(T[] array, T val, ref int Index) where T:IComparable
        {
            bool IsIT = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].CompareTo(val)==0) { IsIT = true; Index = i; break; }
            }
            return IsIT;
        }
        public static bool ValueInArray(DateTime[] array, DateTime val)
        {
            bool IsIT = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == val) { IsIT = true; break; }
            }
            return IsIT;
        }
        public static bool ValueInArray(DateTime[] array, DateTime val, ref int Index)
        {
            bool IsIT = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == val) { IsIT = true; Index = i; break; }
            }
            return IsIT;
        }


    }
    #endregion
}
