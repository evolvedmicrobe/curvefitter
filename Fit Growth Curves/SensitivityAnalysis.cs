using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoNS.Visualization;
using ShoNS.Array;
using ShoNS.MathFunc;
using GrowthCurveLibrary;

namespace Fit_Growth_Curves
{
    class SensitivityAnalysis
    {

        
        List<Measurement> Measurements;
        GrowthCurve GD;
        HashSet<double> AllFitValues = new HashSet<double>();
        public const double MinOD = 0.003;
        public const double MaxOD = .21;
        private void TrimCurve()
        {
            double MaxOd = Measurements.Max(x => x.ODValue);
            Measurements = (from x in Measurements where x.ODValue > MinOD select x).ToList();
            var maxTime = (from x in Measurements where x.ODValue == MaxOd select x.Time).First();
            Measurements = (from x in Measurements where (x.Time - maxTime) < 2 select x).ToList();

        }

        public SensitivityAnalysis(GrowthCurve GD,MatrixArrayPlot.ArrayPlot AP)
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
                double lastValue = Double.NaN;
                for (int i = 0; i < Measurements.Count; i++)
                {
                    Array[i, i] = Double.NaN;
                    for (int j = (i + 1); j < Measurements.Count; j++)
                    {
                        double value = lastValue;
                            var Fit = from b in Enumerable.Range(i,(j-i+1)) select Measurements[b];
                            double[] times = (from b in Fit select b.Time).ToArray();
                            double[] ods = (from b in Fit select b.ODValue).ToArray();
                            int count = times.Length;
                            value = Double.NaN;
                            double[] LogValues = ods.Select(z => Math.Log(z)).ToArray();
                            if (count > 2)
                            {
                                ExponentialFit EF = new ExponentialFit(times, ods);
                                //LinearFit EF = new LinearFit(times, LogValues);
                                value = EF.GrowthRate;
                                AllFitValues.Add(value);
                            }
                            if (count == 2)
                            {
                                AllFitValues.Add(value);
                                LinearFit LF = new LinearFit(times, LogValues);
                                value = LF.Slope;
                            }
                        if (value < MinAllowed | value > MaxAllowed)
                        { value = Double.NaN; }
                        Array[i, j] = value;
                        Array[j, i] = value;
                        lastValue = value;
                    }
                }
                string[] rowNames=(from x in Measurements select x.ODValue.ToString("g2")).ToArray();
                string[] colNames=Enumerable.Range(0,rowNames.Length).Select(x=> x%2==0?rowNames[x]:"").ToArray();
                AP.SetMatrixForPlotting(Array, rowNames, colNames);
            }
            else
            {
                
                //throw new Exception("Your maximum OD is below 0.2 or your curve has not been fit.  Obtain higher quality data before fitting");
            }


        }

        /// <summary>
        /// Returns true if the range has changed
        /// </summary>
        /// <param name="startOD"></param>
        /// <param name="endOD"></param>
        /// <param name="ODMustIncrease"></param>
        /// <returns></returns>
        public bool SetFittedODRange(double startOD, double endOD, bool ODMustIncrease = true)
        {

            bool FittedPointsDif = false;
            foreach (Measurement meas in Measurements)
            {
                if (meas.ODValue >= startOD && meas.ODValue <= endOD)
                {
                    if (meas.Fitted != true)
                    { FittedPointsDif = true; }
                    meas.Fitted = true;
                }
                else
                {
                    if (meas.Fitted != false)
                    { FittedPointsDif = true; }
                    meas.Fitted = false;

                }
            }
            return FittedPointsDif;
        }
        public class Measurement
        {
            public double ODValue;
            public double Time;
            public bool Fitted;

        }



    }
}
