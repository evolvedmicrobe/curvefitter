using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace GrowthCurveLibrary
{

    public static class ExportDataClasses
    {
        public sealed class OutputColumn
        {
            public readonly string Name;
            public readonly Func<GrowthCurve, object> outFunc;
            public OutputColumn(string Name, Func<GrowthCurve, object> OutputFunction)
            {
                this.Name = Name;
                this.outFunc = OutputFunction;
            }
        }
        public static string SafeGet(Func<GrowthCurve,object> func, GrowthCurve GC)
        {
            try
            {
                object o= func(GC).ToString();
                if(o is double)
                {
                    double n = (double)o;
                    return n.ToString("n5");
                }
                else
                {
                    return o.ToString();
                }
            }
            catch
            {
                return "NA";
            }
        }
        /// <summary>
        /// Collection of columns to output with various summary statistics.
        /// </summary>
        public static List<OutputColumn> OutputColumnCollection = new List<OutputColumn>() {
                new OutputColumn("Name", (x) => x.DataSetName),
                new OutputColumn("Doubling Time(Hrs)", (x) => x.GrowthRate.DoublingTime),              
                new OutputColumn("How Determined?", (x) => x.GrowthRate.FitterUsed),
                new OutputColumn("GrowthRateExp",(x)=>x.ExpFit.GrowthRate),
                new OutputColumn("GrowthRateIntercept",x=>x.ExpFit.InitialPopSize),
                new OutputColumn("NumPoints", (x) => x.GrowthRate.NumPoints),
                new OutputColumn("R2", (x) => x.GrowthRate.R2),
                new OutputColumn("RMSE", (x) => x.GrowthRate.RMSE),
                new OutputColumn("MaxOD", (x) => x.HighestODValue),
                new OutputColumn("LastOD", (x) => x.Last().ODValue),
                new OutputColumn("LinearSlope", (x) => x.LinFit.Slope),
                new OutputColumn("LinearIntercept", (x) => x.LinFit.Intercept),
                new OutputColumn("OffSetSlope", (x) => x.OffSetExp.GrowthRate),
                new OutputColumn("OffSetIntercept", (x) => x.OffSetExp.InitialPopSize),
                new OutputColumn("OffSetOffSet", (x) => x.OffSetExp.OffSet),
                new OutputColumn("OffSetRMSE", (x) => x.OffSetExp.RMSE),
                new OutputColumn("HoursUntilOD0.02", (x) => x.HoursTillODReached(.02)),
                new OutputColumn("DifinL1ErrorLinvExp",(x)=>x.LinFit.CalculateAbsErrorAfterExpTransform()-x.ExpFit.AbsError),
                new OutputColumn("DifinL2ErrorLinvExp",(x)=>x.LinFit.CalculateL2ErrorAfterExpTransform()-x.ExpFit.calculateResidualSumofSquares()),
                new OutputColumn("TreatmentGroup",(x)=>x.Population),
#if !MONO
                new OutputColumn("GroupFitGrowth",x=>x.GroupFitGrowthRate),
                new OutputColumn("GroupFitIntercept",x=>x.GroupFitInitPop),
                new OutputColumn("RobustGR",(x)=>x.MixtureErrorModel.GrowthRate),
                new OutputColumn("RobustIntercept",(x)=>x.MixtureErrorModel.InitialPopSize)
#endif
                };
       /// <summary>
       /// Exports data as a CSV file with the first column being the date/time encoded as a double.
       /// Useful for loading data in Matlab.
       /// </summary>
       /// <param name="FullFileName"></param>
       /// <param name="GCC"></param>
        public static void ExportMatlabData(string FullFileName, GrowthCurveCollection GCC)
        {
            StreamWriter SW = new StreamWriter(FullFileName);
             string TitleLine = "Time,";//this will hold the titles for everything below
            HashSet<double> Times = new HashSet<double>();

            foreach (GrowthCurve GC in GCC)
            {
                foreach (double d in GC.Select((x)=>x.time_as_double)){ Times.Add(d); }
                TitleLine += GC.ToString() + "," ;                
            }
            //Below assumes the time is the same for all of them
            SW.WriteLine(TitleLine);
            List<double> times =Times.ToList();
            times.Sort();
            foreach (double time in times)
                {
                    string line = time.ToString() + ",";
                    foreach (GrowthCurve GR in GCC)
                    {
                        int indexPos = -1;
                        double[] timesGC = GR.TimeValues_As_Double;
                        
                        if (SimpleFunctions.ValueInArray(timesGC, time, ref indexPos))
                        {
                            //decide if this timepoint was included
                            line += GR.ODValues[indexPos].ToString() + ",";
                            double DateX = GR[indexPos].time_as_double;
                        }
                        else { line += "-999,"; }
                    }
                    SW.WriteLine(line);
            }
                SW.Close();

            }
        /// <summary>
        /// Export the raw data as well as the results of fitting the data.
        /// </summary>
        /// <param name="FullFileName">Name of file to write.</param>
        /// <param name="GCC">Collection of GrowthCurves to export</param>
        public static void ExportData(string FullFileName, GrowthCurveCollection GCC)
        {
            StreamWriter SW = new StreamWriter(FullFileName);
            SW.WriteLine(String.Join(",", OutputColumnCollection.Select((x) => x.Name)));
            foreach (GrowthCurve GR in GCC)
            {
                SW.WriteLine(String.Join(",", OutputColumnCollection.Select((x) => SafeGet(x.outFunc, GR))));
            }     
            //Below assumes the time is the same for all of them
            //Below assumes the time is the same for all of them

            SW.WriteLine(Intermissionline);
            SW.Write("DateTime,");
            SW.WriteLine(String.Join(",", GCC.Select((x) => x.DataSetName + " OD,Flag")));
            HashSet<DateTime> dtimes = new HashSet<DateTime>();
            foreach (GrowthCurve gc in GCC)
            {
                foreach (DateTime dt in gc.Select((x) => x.time))
                {
                    dtimes.Add(dt);
                }
            }
            List<DateTime> DateTimesinFile = dtimes.Select((x) => x).ToList();
            DateTimesinFile.Sort();
            foreach (DateTime DT in DateTimesinFile)
            {
                string line = DT.ToString() + ",";
                foreach (GrowthCurve GR in GCC)
                {
                    int indexPos = -1;
                    DateTime[] timeValues = GR.Times;
                    if (SimpleFunctions.ValueInArray(timeValues, DT, ref indexPos))
                    {
                        //decide if this timepoint was included
                        line += GR.ODValues[indexPos].ToString() + ",";
                        double DateX = GR[indexPos].time_as_double;
                        if (GR.FittedXValues != null && SimpleFunctions.ValueInArray(GR.FittedXValues, DateX))//now decide if it made it into the fit
                        {
                            line += "0,";
                        }
                        else { line += "1,"; }
                    }
                    else { line += "-999,-999,"; }
                }
                SW.WriteLine(line);
            }
            SW.Close();
        }
        public const string Intermissionline = "Complete Data Listing Below";
     
    /// <summary>
    /// A deprecated method.
    /// </summary>
    /// <param name="FullFileName"></param>
    /// <param name="GCC"></param>
        [Obsolete]
        public static void ExportDataDEPRECATED(string FullFileName, GrowthCurveCollection GCC)
        {
            bool LagData = false;
            //FullFileName = "C:\\FullName.csv";
            StreamWriter SW = new StreamWriter(FullFileName);
            SW.WriteLine("Fitted Data Results");
            SW.WriteLine("Name, Doubling Time(Hrs),Growth Rate, How Determined?,NumPoints,R2,RMSE, Maximum GrowthRate,MaxOD,Notes,Linear-Fit Slope,Reduction in absolute error from ExpFit,LagTime,Reduction in Sum of Squares from Exp Fit,TimeTill_OD_0.02,EndOD,TreatmentGroup");
            string TitleLine = "Time,";//this will hold the titles for everything below
            foreach (GrowthCurve GR in GCC)
            {
                    LagData = false;
                
                TitleLine += GR.ToString() + " OD," + "Flag,";                
                string newline=GR.ToString()+",";
                double ActualGrowth = Math.Log(2) / GR.GrowthRate.GrowthRate;
                if (GR.ValidDataSet)
                {
                    newline += ActualGrowth.ToString("n5") + "," + GR.GrowthRate.GrowthRate.ToString() + "," + GR.GrowthRate.FittingUsed + "," + GR.GrowthRate.NumPoints + "," + GR.GrowthRate.R2.ToString("n4") + ","
                    + GR.GrowthRate.RMSE.ToString("n5") + "," + GR.MaxGrowthRate.MaxGrowthRate.ToString("n5") + "," +GR.ODValues.Max().ToString("n4")+","+ GR.GrowthRate.Notes;
                    if (GR.LinearModelFitted && GR.ExpModelFitted)
                    {
                        double dif = GR.LinFit.AbsError - GR.ExpFit.AbsError;
                        double dif2 = GR.LinFit.calculateResidualSumofSquares() - GR.ExpFit.calculateResidualSumofSquares();
                        newline += "," + GR.LinFit.Parameters[1].ToString("n5") + "," + dif.ToString("n5")+",DEPRECATED,"+dif2.ToString("n5")+",";
                    }//+","+RMSEdiff.ToString("n5")+","+GR.LinFit.RMSE.ToString()+","+GR.ExpFit.RMSE.ToString(); }//report the linear fitted slope if possible
                    else { newline += ",No Exp Fit to Compare Against,"; }
                    newline += GR.HoursTillODReached(.02).ToString() + ",";
                    newline += GR.ODValues.Last().ToString()+",";
                    
                }
                else { newline += ",,Weird Data:Blank??,,,,,,,,,,,,,"; }
                SW.WriteLine(newline);
            }
            //Below assumes the time is the same for all of them
            string Intermissionline = "Complete Data Listing Below";
            if (LagData) { Intermissionline += "-Initial OD Present"; };
            SW.WriteLine(Intermissionline);
            SW.WriteLine(TitleLine);
            HashSet<DateTime> dtimes = new HashSet<DateTime>();
            foreach(GrowthCurve gc in GCC)
            {
                foreach(DateTime dt in gc.Select((x)=>x.time))
                {
                    dtimes.Add(dt);
                }
            }
            List<DateTime> DateTimesinFile = dtimes.Select((x) => x).ToList();
            DateTimesinFile.Sort();
                foreach (DateTime DT in DateTimesinFile)
                {
                    string line = DT.ToString() + ",";
                    foreach (GrowthCurve GR in GCC)
                    {
                        int indexPos = -1;
                        DateTime[] timeValues = GR.Times;
                        if (SimpleFunctions.ValueInArray(timeValues, DT, ref indexPos))
                        {
                            //decide if this timepoint was included
                            line += GR.ODValues[indexPos].ToString() + ",";
                            double DateX = GR[indexPos].time_as_double;
                            if (GR.FittedXValues != null && SimpleFunctions.ValueInArray(GR.FittedXValues, DateX))//now decide if it made it into the fit
                            {
                                line += "0,";
                            }
                            else { line += "1,"; }
                        }
                        else { line += "-999,-999,"; }
                    }
                    SW.WriteLine(line);
                }
                SW.Close();
            
        }
    }
}