using System;
#if !MONO
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.SolverFoundation;
using ShoNS.Optimizer;
using ShoNS.Numerics;
using ShoNS.Array;
using ShoNS.MathFunc;
using ShoNS.Stats;
//using MicrosoftResearch.Infer.Models;
//using MicrosoftResearch.Infer;
//using MicrosoftResearch.Infer.Distributions;
//using MicrosoftResearch.Infer.Maths;

namespace GrowthCurveLibrary
{
    [Serializable]
    public static class OutlierDetector
    {
        public static double measError = 0.0015;        
        public static double Cutoff = .995;
        public static void LinearModelOutlierDetector(GrowthCurve GC)
        {
            if (!GC.ExpModelFitted)
            {
                foreach (GrowthDataPoint gdp in GC)
                {
                    if (gdp.UsedInFit) gdp.OutlierFlag = true;
                }
            }
            List<double> XvaluesToDrop = new List<double>() ;
            while (true)
            {
                bool goAgain = false;
                double[] xfittedStart = GC.FittedXValues;
                double[] yfittedStart = GC.FittedYValues;
                //Flag it all
                if (xfittedStart.Length < 4) 
                {
                    foreach (GrowthDataPoint gdp in GC)
                    {
                        if (gdp.UsedInFit) gdp.OutlierFlag = true;
                    }
                }
                for (int i = 0; i < xfittedStart.Length; i++)
                {
                    var xfit = xfittedStart.ToList();
                    var yfit = yfittedStart.ToList();
                    xfit.RemoveAt(i);
                    yfit.RemoveAt(i);
                    double curX = xfittedStart[i];
                    ExponentialFit EXP = new ExponentialFit(xfit.ToArray(), yfit.ToArray());
                    double xmean = xfit.Average();
                    double n = (double)xfit.Count;
                    //Equations from Sleuth p. 190
                    DoubleArray xs = DoubleArray.From(xfit);
                    DoubleArray ys = DoubleArray.From(yfit);
                    double denom = (n - 1) * xs.Std();
                    double denom2 = 1 / n;
                    double Difs = xfittedStart[i] - xmean;
                    Difs = Difs * Difs;
                    Difs = Difs / denom;
                    Difs = Difs + denom2 + 1.0;
                    Difs = Math.Sqrt(Difs) * measError;
                    double SEPred = Math.Sqrt(Math.Pow(measError, 2.0) + Difs);
                    ////assume mean is 0 so just divide
                    double predict = EXP.MakePredictionsAtPoints(new double[] { xfittedStart[i] }).First();
                    ////Leading to the following estimated differences
                    double Error = (predict - yfittedStart[i]) / SEPred;
                    //ArrayMathInPlace.Abs(Error);
                    ////Giving the probability 
                    Error = ShoNS.MathFunc.ArrayMath.CdfNorm(DoubleArray.From(new List<double>() { Error }))[0];

                    if (Error > .99)
                    {
                        var q = from xx in GC where xx.ODValue == curX select xx;
                        q.First().OutlierFlag = true;
                        goAgain = true;
                        break;
                    }
                }
                if (!goAgain)
                {
                    break;
                }
            }
            GC.FitData();
        }
    }
    //public class BayesianOutlierDetector
    //{
        
    //    public BayesianOutlierDetector()
    //    {
    //        Variable<int> R = Variable.New<int>().Named("R");
    //        Range r = new Range(R);
    //        Variable<double> A = Variable.GaussianFromMeanAndPrecision(0, 1).Named("A");
    //        Variable<double> b = Variable.GaussianFromMeanAndPrecision(0, 1).Named("r");
    //        VariableArray<double> x = Variable.Array<double>(r).Named("x");
    //        VariableArray<double> y = Variable.Array<double>(r).Named("y");

    //        using (Variable.ForEach(r))
    //        {
    //            var exp = Variable.Exp(b * x[r]).Named("exp");
    //            var Aexp = (A * exp).Named("aexp");
    //            y[r] = Variable.GaussianFromMeanAndPrecision(Aexp, 1.0);
    //        }

    //        double[] Xvals = new double[] { 18.3333, 19.1667, 20.0000, 20.8333, 21.6667, 22.5000, 23.3333, 24.1667, 25.0000, 25.8333, 26.6667, 27.5000, 28.3333, 29.1667, 30.0000 };
    //        double[] Yvals = new double[] { 0.0113, 0.0136, 0.0163, 0.0196, 0.0235, 0.0282, 0.0339, 0.0407, 0.0489, 0.0588, 0.0706, 0.0848, 0.1019, 0.1224, 0.1470 };
    //        R.ObservedValue = Xvals.Length;
    //        x.ObservedValue = Xvals;
    //        y.ObservedValue = Yvals;

    //        InferenceEngine engine = new InferenceEngine(new VariationalMessagePassing());
    //        Gaussian postA = engine.Infer<Gaussian>(A);
    //        Gaussian postb = engine.Infer<Gaussian>(b);
    //        Console.WriteLine("Posterior over A = {0}", postA);
    //        Console.WriteLine("Posterior over b = {0}", postb);
    //        Console.ReadLine();
       
            
    //    }
    //    private void oldV2()
    //    {
    //        Variable<int> R = Variable.New<int>().Named("R");
    //        Range r = new Range(R);
    //        Variable<double> A = Variable.GaussianFromMeanAndPrecision(0, 1).Named("A");
    //        Variable.ConstrainPositive(A);
    //        Variable<double> b = Variable.GaussianFromMeanAndPrecision(0, 1).Named("r");
    //        Variable.ConstrainPositive(b);
    //        VariableArray<double> x = Variable.Array<double>(r).Named("x");
    //        VariableArray<double> y = Variable.Array<double>(r).Named("y");

    //        // Range k = new Range(2);
    //        // VariableArray<bool> z = Variable.Array<bool>(r);
    //        using (Variable.ForEach(r))
    //        {
    //            //z[r]=Variable.Bernoulli(.95);
    //            var exp = Variable.Exp(b * x[r]).Named("exp");
    //            var Aexp = (A * exp).Named("aexp");
    //            y[r] = Variable.GaussianFromMeanAndVariance(Aexp, 1e-6);
    //            //using(Variable.If(z[r]))
    //            //{
    //            //    y[r] = Variable.GaussianFromMeanAndVariance(Aexp, 1e-6);
    //            //}
    //            //using(Variable.IfNot(z[r]))
    //            //{
    //            //    y[r] = Variable.GaussianFromMeanAndVariance(Aexp, 4e-4);
    //            //}
    //        }
    //        double[] Xvals = new double[] { 18.3333, 19.1667, 20.0000, 20.8333, 21.6667, 22.5000, 23.3333, 24.1667, 25.0000, 25.8333, 26.6667, 27.5000, 28.3333, 29.1667, 30.0000 };
    //        double[] Yvals = new double[] { 0.0113, 0.0136, 0.0163, 0.0196, 0.0235, 0.0282, 0.0339, 0.0407, 0.0489, 0.0588, 0.0706, 0.0848, 0.1019, 0.1224, 0.1470 };

    //        R.ObservedValue = Xvals.Length;
    //        x.ObservedValue = Xvals;
    //        y.ObservedValue = Yvals;
    //        InferenceEngine engine = new InferenceEngine(new VariationalMessagePassing());

    //        Gaussian postA = engine.Infer<Gaussian>(A);
    //        Gaussian postb = engine.Infer<Gaussian>(b);
    //        //var q = engine.Infer(z);

    //        Console.WriteLine("Posterior over A = {0}", postA);
    //        Console.WriteLine("Posterior over b = {0}", postb);
    //        //Console.WriteLine("Posterior over z = {0}", q);

    //        Console.ReadLine();
    //    }
    //    private void old()
    //    {
    //        //Vector[] data = new Vector[] { new Vector(1.0, -3), new Vector(1.0, -2.1), new Vector(1.0, -1.3), new Vector(1.0, 0.5), new Vector(1.0, 1.2), new Vector(1.0, 3.3), new Vector(1.0, 4.4), new Vector(1.0, 5.5) };
    //        //Range rows = new Range(data.Length);
    //        //VariableArray<Vector> x = Variable.Constant(data, rows).Named("x");
    //        //Variable<Vector> w = Variable.VectorGaussianFromMeanAndPrecision(new Vector(new double[] { 0, 0 }), PositiveDefiniteMatrix.Identity(2)).Named("w");
    //        //VariableArray<double> y = Variable.Array<double>(rows);
    //        //y[rows] = Variable.GaussianFromMeanAndVariance(Variable.InnerProduct(x[rows], w), 1.0);
    //        //y.ObservedValue = new double[] { 30, 45, 40, 80, 70, 100, 130, 110 };
    //        //InferenceEngine engine = new InferenceEngine(new VariationalMessagePassing());
    //        //VectorGaussian postW = engine.Infer<VectorGaussian>(w);
    //        //Console.WriteLine("Posterior over the weights: " + Environment.NewLine + postW);

    //        //double[] Xvals = new double[] { 1.2, 2.0, 3.0, 4.0, 5.0, 6.0 };
    //        //double[] Yvals = new double[] { 0.2604256, 0.3105414, 0.3869585, 0.4821799, 0.6008332, 0.7486843 };
    //        //Variable<double> A = Variable.GaussianFromMeanAndVariance(0, 10).Named("A");
    //        //Variable<double> r = Variable.GaussianFromMeanAndVariance(0, 10).Named("r");
    //        ////Variable<double> A=Variable.Constant<double>(.2);
    //        ////Variable<double> r=Variable.Constant<double>(.22);

    //        //List<Vector> dataList = new List<Vector>();
    //        //dataList.Add(Vector.FromArray(Xvals));
    //        //Vector[] data = dataList.ToArray();
    //        //Range rows = new Range(Xvals.Length);

    //        //Variable<Vector> w = Variable.VectorGaussianFromMeanAndPrecision(Vector.FromArray(new double[] { 0 }), PositiveDefiniteMatrix.Identity(1)).Named("w");
    //        //VariableArray<double> y = Variable.Array<double>(rows);
    //        //VariableArray<Vector> x = Variable.Constant(data, rows).Named("x");
    //        //y[rows] = Variable.GaussianFromMeanAndVariance(Variable.Exp(Variable.InnerProduct(x[rows], w)), 1.0);



    //        // VariableArray<double> x = Variable.Constant(Xvals,rows).Named("x");
    //        //VariableArray<double> power = Variable.Array<double>(rows).Named("power");
    //        //VariableArray<double> power2 = Variable.Array<double>(rows).Named("Power2)");
    //        //power[rows] = x[rows] * r;
    //        //power2[rows] = Variable<double>.Exp(power[rows]);

    //        // VariableArray<double> y =Variable.Array<double>(rows);
    //        //y[rows]=power2[rows]*A;
    //        y.ObservedValue = Yvals;
    //        InferenceEngine engine = new InferenceEngine();
    //        VectorGaussian postW = engine.Infer<VectorGaussian>(w);
    //        //var postW = engine.Infer<double>(r);
            
    //    }
    //}
    //public static class MyFactor
    //{
    //    public static double ExpND(double[] array)
    //    {
    //        double Exp = 0.0;
    //        var arr=array.ToList();
    //        arr.ForEach(x => Math.Exp(x));
    //        return arr.Sum();
    //    }
    //}
}

#endif