using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrowthCurveLibrary;
using MatrixArrayPlot;

namespace Fit_Growth_Curves
{
    public abstract class Sensitivities
    {
        public string xAxis, yAxis;
        public const double MinOD = 0.003;
        public const double MaxOD = .21;
        
        public abstract void DoSensitivityAnalysis(GrowthCurve GD,ArrayPlot AP);
    }
    public class StartEndODRange : Sensitivities
    {
        public StartEndODRange()
        {
            this.xAxis = "Start OD to Fit";
            this.yAxis = "End OD to Fit";
        }
        protected internal virtual double CalculateGrowthRate(double[] times, double[] ods)
        {
            double value = Double.NaN;
            int count = times.Length;
            if (count > 2)
            {
                ExponentialFit EF = new ExponentialFit(times, ods);
                //LinearFit EF = new LinearFit(times, LogValues);
                value = EF.GrowthRate;
            }
            if (count == 2)
            {
                double[] LogValues = ods.Select(z => Math.Log(z)).ToArray();
                LinearFit LF = new LinearFit(times, LogValues);
                value = LF.Slope;
            }
            return value;
        }
        public override void DoSensitivityAnalysis(GrowthCurve GD, ArrayPlot AP)
        {
            this.GD = GD;
            
            if (GD.ExpModelFitted && GD.ODValues.Max() > .08)
            {
                double current = GD.GrowthRate.GrowthRate;
                double MaxAllowed = 1.2 * current;
                double MinAllowed = .8 * current;
                double BaseGrowthRate = GD.ExpFit.GrowthRate;
                Measurements = (GD.TimeValues_As_Double.Zip(GD.ODValues, (time, od) => new Measurement() { ODValue = od, Time = time })).ToList();
                TrimCurve();
                double[,] Array = new double[Measurements.Count,Measurements.Count];
                double value = Double.NaN;
                for (int i = 0; i < Measurements.Count; i++)
                {
                    Array[i, i] = Double.NaN;
                    for (int j = (i + 1); j < Measurements.Count; j++)
                    {
                        var Fit = from b in Enumerable.Range(i,(j-i+1)) select Measurements[b];
                        double[] times = (from b in Fit select b.Time).ToArray();
                        double[] ods = (from b in Fit select b.ODValue).ToArray();
                        value = CalculateGrowthRate(times, ods);
                        if (value < MinAllowed | value > MaxAllowed)
                        { value = Double.NaN; }
                        Array[i, j] = value;
                        Array[j, i] = Double.NaN ;
                    }
                }
                string[] rowNames=(from x in Measurements select x.ODValue.ToString("g2")).ToArray();
                string[] colNames=Enumerable.Range(0,rowNames.Length).Select(x=> x%2==0?rowNames[x]:"").ToArray();
                AP.SetMatrixForPlotting(Array, rowNames, colNames);
            }
            else
            {
                 AP.SetMatrixForPlotting(new double[10,10]);
                
                //throw new Exception("Your maximum OD is below 0.2 or your curve has not been fit.  Obtain higher quality data before fitting");
            }


        }
        
        protected List<Measurement> Measurements;
        protected GrowthCurve GD;
        protected void TrimCurve()
        {
            double MaxOd = Measurements.Max(x => x.ODValue);
            Measurements = (from x in Measurements where x.ODValue > MinOD select x).ToList();
            var maxTime = (from x in Measurements where x.ODValue == MaxOd select x.Time).First();
            Measurements = (from x in Measurements where (x.Time - maxTime) < .05 select x).ToList();

        }


        public class Measurement
        {
            public double ODValue;
            public double Time;
            public bool Fitted;

        }

    }
    public class StartEndODRangeLogFit : StartEndODRange
    {
        protected internal override double CalculateGrowthRate(double[] times, double[] ods)
        {
            double value = Double.NaN;
            int count = times.Length;

            if (count >= 2)
            {
                double[] LogValues = ods.Select(z => Math.Log(z)).ToArray();
                LinearFit LF = new LinearFit(times, LogValues);
                value = LF.Slope;
            }
            return value;
        }
    }
    public class StartEndODRangeWithBias : StartEndODRange
    {
        public const double BiasToAdd=0.001;
        public StartEndODRangeWithBias() 
        {
            this.xAxis = "Start OD to Fit";
            this.yAxis = "End OD to Fit";
        }
        public override void DoSensitivityAnalysis(GrowthCurve GD, ArrayPlot AP)
        {
            this.GD = GD;

            if (GD.ExpModelFitted && GD.ODValues.Max() > .08)
            {
                double current = GD.GrowthRate.GrowthRate;
                double MaxAllowed = 1.2 * current;
                double MinAllowed = .8 * current;
                double BaseGrowthRate = GD.ExpFit.GrowthRate;
                Measurements = (GD.TimeValues_As_Double.Zip(GD.ODValues, (time, od) => new Measurement() { ODValue = od, Time = time })).ToList();
                TrimCurve();
                double[,] Array = new double[Measurements.Count, Measurements.Count];
                double value = Double.NaN;
                for (int i = 0; i < Measurements.Count; i++)
                {
                    Array[i, i] = Double.NaN;
                    for (int j = (i + 1); j < Measurements.Count; j++)
                    {
                        var Fit = from b in Enumerable.Range(i, (j - i + 1)) select Measurements[b];
                        double[] times = (from b in Fit select b.Time).ToArray();
                        double[] ods = (from b in Fit select b.ODValue).ToArray();
                        value = CalculateGrowthRate(times, ods);
                        if (value < MinAllowed | value > MaxAllowed)
                        { value = Double.NaN; }
                        else
                        {
                            ods = ods.Select(x => x + BiasToAdd).ToArray();
                            double newFit = CalculateGrowthRate(times, ods);
                            value = newFit-value;
                        }
                        Array[i, j] = value;
                        Array[j, i] = Double.NaN;
                    }
                }
                string[] rowNames = (from x in Measurements select x.ODValue.ToString("g2")).ToArray();
                string[] colNames = Enumerable.Range(0, rowNames.Length).Select(x => x % 2 == 0 ? rowNames[x] : "").ToArray();
                AP.SetMatrixForPlotting(Array, rowNames, colNames);
            }
            else
            {
                AP.SetMatrixForPlotting(new double[10, 10]);
           }

        }


    }
    public class StartEndODRangeWithBiasLinear : StartEndODRangeWithBias
    {
        protected internal override double CalculateGrowthRate(double[] times, double[] ods)
        {
            double value = Double.NaN;
            int count = times.Length;

            if (count >= 2)
            {
                double[] LogValues = ods.Select(z => Math.Log(z)).ToArray();
                LinearFit LF = new LinearFit(times, LogValues);
                value = LF.Slope;
            }
            return value;
        }
    }
}
