using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using System.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;

namespace GrowthCurveLibrary
{
    /// <summary>
    /// This class represents growth data for one strain.
    /// </summary>
    [Serializable]
    public class GrowthCurve : List<GrowthDataPoint>, INotifyPropertyChanged
    {
        /// <summary>
        /// Indicates that the curve should not notify of events or attempt
        /// to refit data 
        /// </summary>
        public bool SuspendRefit = false;
        /// <summary>
        /// Some constants related to the values.
        /// </summary>
        public const double BAD_DATA_VALUE = -999;
        public const bool USE_EXPONENTIAL_FITTING = true;
        public const double DEFAULTOD_MAX = 0.18;
        public const double DEFAULT_MAX_GROWTH_RATE = 10000;
        /// <summary>
        /// Just in case it wasn't clear that this class is inherited from a list,
        /// this returns itself
        /// </summary>
        public List<GrowthDataPoint> DataPoints
        {
            get { return this; }
        }
        /// <summary>
        /// The growth rate currently fit for this guy
        /// </summary>
        [Serializable]
        public struct GrowthRateInUse
        {
            public double GrowthRate;//=0;
            public string DisplayGrowthRate
            {
                get { return GrowthRate.ToString("n4"); }
            }
            public string FittingUsed;//="Not Fitted"; 
            public string DisplayFittingUsed
            {
                get { return FittingUsed; }
                set { FittingUsed = value; }
            }
            public int NumPoints {get;set;}// = -999;
            public double R2;
            public string DisplayR2
            {
                get { return R2.ToString("n4"); }
            }
            public double RMSE;
            public string DisplayRMSE
            { get { return RMSE.ToString("n3"); } }
            public string Notes;
            public AbstractFitter FitterUsed;
            public double DoublingTime
            {
                get { return Math.Log(2) / GrowthRate; }
            }
            public string FormattedDoublingTime
            {
                get { return DoublingTime.ToString("n3"); }
            }            
        }
        [Serializable]
        public struct MaximumGrowthRate
        {
            public double MaxGrowthRate;
            public double[] xvals;
            public double[] yvals;
        }
        
        //Different models fit
        public GroupFitter GroupFit;
        private GroupFitter.ExpParameters getGroupFitParameters()
        {
                if(GroupFit!=null && GroupFit.WellParameters.ContainsKey(this.DataSetName))
                {
                    return GroupFit.WellParameters[this.DataSetName];
                }
            else{return new GroupFitter.ExpParameters(){ GrowthRate=Double.NaN,InitPop=Double.NaN};}
        }
        public double GroupFitGrowthRate
        {
            get
            {
                return getGroupFitParameters().GrowthRate;
            }
        }
        public double GroupFitInitPop
        {
            get { return getGroupFitParameters().InitPop; }
        }

        public LinearFit LinFit;
        public ExponentialFit ExpFit;
        public OffSetExponentialFit OffSetExp;
        public MixtureErrorModel MixtureErrorModel;
        public QuadraticLinearRegression QuadModel;
        public LogisticModel LogisticModel;

        public MaximumGrowthRate MaxGrowthRate;
        private GrowthRateInUse pGrowthRate;
        public GrowthRateInUse GrowthRate
        {
            get { return pGrowthRate; }
            set { pGrowthRate = value; }
        }
        public void SetHandPicked()
        {
            if(!String.IsNullOrEmpty(pGrowthRate.Notes))
            {
                pGrowthRate.Notes="Hand Picked Data";
            }
        }
        public DateTime BaseTime;
        public List<FitPoint> FittedValues 
        {
            get
            {
                double[] x, y;
                AbstractFitter toUse=null;
                if (ExpFit != null && ExpFit.SuccessfulFit)
                {
                    toUse = ExpFit;
                }
                else if (LinFit != null && LinFit.SuccessfulFit)
                {
                    toUse = LinFit;
                }
                else { return null; }

                toUse.GenerateFitLine(0, .1, SimpleFunctions.Max(this.FittedXValues), out x, out y);
                return x.Zip(y, (z, w) => new FitPoint { x = z, y = w }).ToList();
            }
        }
        public List<FitPoint> FittedLogValues
        {
            get
            {
                var q = FittedValues;
                if (q != null)
                {
                    foreach (var v in q)
                    {
                        v.y = Math.Log(v.y);
                    }
                }
                return q;
            }
        }
        public List<FitPoint> FittedResidualValues
        {
            get
            {
                var toUse = (from x in this where x.UsedInFit select x);
                List<FitPoint> toReturn = new List<FitPoint>();

                if (this.ExpModelFitted || this.LinearModelFitted)
                {
                    foreach (var q in toUse)
                    {
                        toReturn.Add(new FitPoint {x=q.time_as_double,
                            y=(q.ODValue-this.GrowthRate.FitterUsed.FunctiontoFit(q.time_as_double))});
                        
                    }                   
                }
                return toReturn;
            }
        }
        /// <summary>
        /// Uses the parameters from the exponential fit to determine when a specific Y value is reached. 
        /// </summary>
        /// <param name="ODValue"></param>
        /// <returns></returns>
        public double? HoursTillODReached(double ODValue)
        {
            if (ODValues.Max() < ODValue || ExpFit == null)
            {
                return null;
            }
            else
            {
                return ExpFit.GetXValueAtYValue(ODValue);
            }
        }        
        public double HighestODValue
        {
            get
            {
                return this.Max(x => x.ODValue);
            }
        }

        //Temporary Statistics to be used
        public int TransferNumber;
        public string Population;
        public int ExpID;
        public dynamic FreeSpace;
        public string __repr__()
        {
            return "Growth Curve: " + DataSetName; 
        }

        public string DataSetName { get; set; }
        /// <summary>
        /// Holds the doubling time between each pair of points, poorly named variable 
        /// </summary>
        public double[] SlopeChange;//holds the doubling time between each pair of points, poorly named variable
        public double[] XvaluesForSlope;
        public List<FitPoint> SlopeValues
        {
            get
            {
                if (SlopeChange == null)
                {
                    return new List<FitPoint>();
                }
                    return SlopeChange.Zip(XvaluesForSlope, (z, w) => new FitPoint { x = w, y = z }).ToList();
            }
        }
        /// <summary>
        /// Determines if the data is valid for fitting or not.
        /// </summary>
        public bool ValidDataSet;
        public bool LinearModelFitted
        {
            get
            {
                if (LinFit != null)
                { return LinFit.SuccessfulFit; }
                else { return false; }
            }
        }
        public bool ExpModelFitted
        {
            get
            {
                if (ExpFit != null)
                {
                    return ExpFit.SuccessfulFit;
                }
                else { return false; }
            }
        }
        public override string ToString()
        {
            return DataSetName;
        }
        public void RecreateFitAfterChange()
        {
            try
            {
                double[] ODS = this.ODValues;
                if ((ODS.Max() - ODS.Min()) < 0.05)
                { SetNoFit(); }
                else
                {
                    CreateSlopeData();
                    CalculateMaxObservedGrowthRate();
                    DetermineDataToFit();
                    FitData();
                }
            }
            catch
            {
                SetNoFit("Problem During Fitting");
            }
        }
        public GrowthCurve(string name, DateTime[] timeValues, double[] odvalues)
        {
            this.SuspendRefit = true;
            DataSetName = name;
            CreateGrowthDataList(timeValues, odvalues);
            RecreateFitAfterChange();
            this.SuspendRefit = false;
        }
        /// <summary>
        /// Gets the doubling time between each time interval
        /// </summary>
        private void CreateSlopeData()
        {
            double[] ODs = this.ODValues;
            double min = Math.Max(ODs.Min() * 1.02, .005);
            double max = this.HighestODValue;
            List<GrowthDataPoint> gdp = new List<GrowthDataPoint>();
            foreach (var g in this)
            {
                if (g.ODValue > min)
                {
                    gdp.Add(g);
                }
                if (g.ODValue >= max)
                {
                    break;
                }
            }
            if (gdp.Count >= 3)
            {
                SlopeChange = new double[gdp.Count - 2];
                XvaluesForSlope = new double[gdp.Count - 2];
                //this determines the slope of the data at each point
                double log2 = Math.Log((double)2);
                for (int i = 1; i < gdp.Count - 1; i++)
                {
                    SlopeChange[i - 1] = (gdp[i + 1].LogODValue - gdp[i - 1].LogODValue) / (gdp[i + 1].time_as_double - gdp[i - 1].time_as_double);
                    SlopeChange[i - 1] = log2 / SlopeChange[i - 1];
                    XvaluesForSlope[i - 1] = gdp[i].ODValue;
                }
            }
            else
            {
                SlopeChange = new double[0];
                XvaluesForSlope = new double[0];
            }
        }
        /// <summary>
        /// Calculates the maximum observed growth rate
        /// between any two points
        /// </summary>
        private void CalculateMaxObservedGrowthRate()
        {
            //This method takes every pair of points ij and calculates the growth rate between them
            //then returns a maximum rate
            MaxGrowthRate.MaxGrowthRate = DEFAULT_MAX_GROWTH_RATE;
            MaxGrowthRate.xvals = new double[2] { BAD_DATA_VALUE, BAD_DATA_VALUE };
            MaxGrowthRate.yvals = new double[2] { 0, 0 };
            int startvalue = 0;
            //Make arrays local so it doesn't take forever to remake them each time
            double[] LogODValues=this.LogODValues;
            double[] TimeValues_As_Double = this.TimeValues_As_Double;
            //if the first value is a lag, the maximum growth rate will be between these this point and another, so we count it out.
            for (int i = startvalue; i < this.Count - 1; i++)
            {
                for (int j = i + 1; j < this.Count; j++)
                {
                    double deltaOD = this[j].ODValue - this[i].ODValue;
                    //To small an interval, stuff gets funky!
                    if (deltaOD < .005)
                    {
                        continue;
                    }
                    double growthrate = (LogODValues[j] - LogODValues[i]) / (TimeValues_As_Double[j] - TimeValues_As_Double[i]);
                    growthrate = Math.Log((double)2) / growthrate;
                    if ((MaxGrowthRate.MaxGrowthRate > growthrate) && (growthrate > DEFAULT_MAX_GROWTH_RATE))
                    {
                        MaxGrowthRate.MaxGrowthRate = growthrate;
                        MaxGrowthRate.xvals[0] = TimeValues_As_Double[i];
                        MaxGrowthRate.xvals[1] = TimeValues_As_Double[j];
                        MaxGrowthRate.yvals[0] = LogODValues[i];
                        MaxGrowthRate.yvals[1] = LogODValues[j];
                    }
                }
            }
        }
        private void SetNoFit(string comment="Not Fit")
        {
            this.ForEach(x => x.UsedInFit = false);
            ValidDataSet = false;
            ExpFit = null;
            LinFit = null;
            pGrowthRate.GrowthRate = Double.NaN;
            pGrowthRate.NumPoints = 0;
            pGrowthRate.R2 = Double.NaN;
            pGrowthRate.RMSE = Double.NaN;
            pGrowthRate.DisplayFittingUsed = "Not Fit";
            pGrowthRate.Notes = comment;
            OnPropertyChange(new PropertyChangedEventArgs("FittedValues"));
            OnPropertyChange(new PropertyChangedEventArgs("FittedLogValues"));

        }
        /// <summary>
        /// Function fits data, should be called everytime the fit changes
        /// </summary>
        public void FitData()
        {
            ValidDataSet = true;
            double[] XtoFit, YtoFit;
            XtoFit = FittedXValues;
            YtoFit = FittedYValues;
            //MixtureErrorModel = null;
            ExpFit = null;
            LinFit = null;
            if (XtoFit.Length >= 2)
            {
                //Linear Fit
                LinFit = new LinearFit(XtoFit, FittedLogYValues);
                pGrowthRate.DisplayFittingUsed = "Linear";
                pGrowthRate.GrowthRate = LinFit.Slope;
                pGrowthRate.R2 = LinFit.R2;
                pGrowthRate.RMSE = LinFit.RMSE;
                pGrowthRate.FitterUsed = LinFit;
                //Exponential Fit and others
                if (XtoFit.Length > 2 && USE_EXPONENTIAL_FITTING)
                {
                    ExpFit = new ExponentialFit(XtoFit, YtoFit);
                    if (ExpFit.SuccessfulFit)
                    {
                        pGrowthRate.DisplayFittingUsed = "Exponential";
                        pGrowthRate.GrowthRate = ExpFit.GrowthRate;
                        pGrowthRate.R2 = ExpFit.R2;
                        pGrowthRate.RMSE = ExpFit.RMSE;
                        pGrowthRate.FitterUsed = ExpFit;
                        //(ExpFit.Y.Max() - ExpFit.Y.Min()) > .1 
                        if (ExpFit.Y.Length > 5)
                        {
                           this.OffSetExp = new OffSetExponentialFit(ExpFit.X, ExpFit.Y, this[0].ODValue);
                            this.LogisticModel = new LogisticModel(ExpFit.X, ExpFit.Y);
                        }
                        else
                        {
                            this.OffSetExp = null;
                        }
                    }
                    else
                    {
                        this.OffSetExp = null;
                    }
                    QuadModel = new QuadraticLinearRegression(XtoFit, YtoFit);
                }
                
                //Now Set the Growth Rate
                pGrowthRate.NumPoints = FittedXValues.Length;
                OnPropertyChange(new PropertyChangedEventArgs("FittedValues"));
                OnPropertyChange(new PropertyChangedEventArgs("FittedLogValues"));
            }
            else
            {
                SetNoFit();
            }
        }
        public void CallOutliers()
        {
            if (this.ExpModelFitted)
            {
                try
                {
                    this.MixtureErrorModel = new MixtureErrorModel(this);
                    List<double> outliers = MixtureErrorModel.XValuesOfOutliers();
                    var q = from x in this where outliers.Contains(x.ODValue) select x;
                    q.ToList().ForEach(x => x.OutlierFlag = true);
                    //FitData();
                    if (this.FittedXValues.Length == 0 && this.OutlierXValues.Count()>0)
                    {
                        SetNoFit("All Outlier Data");
                    }
                }
                catch (Exception thrown)
                {
                    SetNoFit(thrown.Message);
                }
            }
            else
            {
                SetNoFit("No Outlier Detection Possible Without Exp Fit");
            }
        }
        public void GetResiduals(bool JustFittedValues, out List<double> Times, out List<double> Residuals)
        {
            var TimesToUse = this.TimeValues_As_Double;
            var Actual = this.ODValues;
            if (JustFittedValues)
            { TimesToUse = this.FittedXValues; Actual = this.FittedYValues; }
            Times = new List<double>();
            Residuals = new List<double>();
            if (this.ExpModelFitted || this.LinearModelFitted)
            {
                for (int i = 0; i < TimesToUse.Length; i++)
                {
                    Times.Add(TimesToUse[i]);
                    double res = Actual[i] - this.pGrowthRate.FitterUsed.FunctiontoFit(TimesToUse[i]);
                    Residuals.Add(res);
                }
            }
        }
        public void SetFittedRangeFromIndexes(List<int> Indexes)
        {
            SetNoFit();
            foreach (int i in Indexes)
            {
                if (SimpleFunctions.IsARealNumber(this[i].LogODValue))
                {
                    this[i].UsedInFit = true;
                }
            }
            FitData();
            pGrowthRate.Notes = "Range Picked Data";
        }
        public void SetFittedRange(int startindex, int endindex)
        {
            if (ODValues.Length < startindex + 1 || ODValues.Length < endindex + 1)
            {
                throw new Exception("You picked a range to fit outside the range of available values");
            }
            else
            {
                List<int> PossibleValues = new List<int>();
                //This seems a bit ridiculous
                for (int i = startindex; i <= endindex; i++)
                {
                    PossibleValues.Add(i);
                }
                SetFittedRangeFromIndexes(PossibleValues);
            }
        }
        /// <summary>
        /// Naive implementation, currently just goes from the value above the OD value and then grabs it while it is increasing
        /// </summary>
        /// <param name="startOD"></param>
        /// <param name="minOD"></param>
        public void SetFittedODRange(double startOD, double endOD, bool ODMustIncrease = true)
        {
            SetNoFit();
            double HighestOD = this.HighestODValue;
            //Readings have to be contiguous
            bool FitStarted = false;
            double lastOD = Double.MinValue;
            foreach (GrowthDataPoint gdp in this)
            {
                if (gdp.ODValue >= startOD && gdp.ODValue <= endOD && (!ODMustIncrease || gdp.ODValue > lastOD))
                {
                    FitStarted = true;
                    gdp.UsedInFit = true;
                }
                else if (FitStarted || gdp.ODValue==HighestOD)
                { break; }//Cut off points that don't finish
                if (ODMustIncrease && FitStarted && gdp.ODValue < lastOD)
                {
                    break;
                }
                lastOD = gdp.ODValue;
            }
            FitData();
        }
        public void SetFittedODRangeFromPercent(double startOD, double PercentOfMaxOD)
        {
            SetNoFit();
            double MaxAllowed = this.HighestODValue * PercentOfMaxOD;
            double MaxOD = this.HighestODValue;
            foreach (GrowthDataPoint gdp in this)
            {
                double ODValue = gdp.ODValue;
                if (ODValue == MaxOD)
                { break; }
                else if (ODValue >= startOD && ODValue <= MaxAllowed)
                {
                    gdp.UsedInFit = true;
                }
            }
            FitData();
        }
        /// <summary>
        /// Attempts to fit or unfit a point that is selected by it's x,y value
        /// </summary>
        /// <param name="xval">Time as Double in Hours</param>
        /// <param name="yval">Log OD</param>
        /// <returns></returns>
        public void ChangeODPoint(double xval, double yval)
        {
            //presumably only one point matches here
            var points = from gdp in this where gdp.time_as_double == xval && gdp.ODValue == yval select gdp;
            if (points.Count() != 1)
            {
                throw new ArgumentException("Too many or too few points match point requested to change");
            }
            GrowthDataPoint toChange = points.First();
            toChange.UIUsedInFit = !toChange.UsedInFit;
            FitData();
            pGrowthRate.Notes = "Hand Picked Data";
        }
        
        /// <summary>
        /// Simple function which by default goes from .02 to .18
        /// </summary>
        private void DetermineDataToFit()
        {
            this.SetFittedODRange(.02, DEFAULTOD_MAX);
            pGrowthRate.Notes = "Automatically picked";
        }
        public void RemoveFirstPointAsBlank()
        {
            double blank = this.First().ODValue;
            RemoveBlank(blank);
        }
        public void RemoveSecondPointAsBlank()
        {
            double blank = this[1].ODValue;
            RemoveBlank(blank);
        }
        public void RemoveAverageOfFirstThreePointsAsBlank()
        {
            double blank = (from x in this.GetRange(0, 3) select x.ODValue).Average();
            RemoveBlank(blank);
        }
        public void RemoveBlank(double Blank)
        {
            this.SuspendRefit = true;
            foreach (GrowthDataPoint GDP in this)
            {
                GDP.ODValue -= Blank;
                GDP.UsedInFit = false;
            }
            this.SuspendRefit = false;
            RecreateFitAfterChange();      
            OnPropertyChange(new PropertyChangedEventArgs("FittedValues"));
            
        }
        public void OutputDebugFile(string Fname)
        {
            StreamWriter SW = new StreamWriter(Fname);
            foreach (var q in this)
            {
                SW.WriteLine(q.time_as_double.ToString() + "," + q.ODValue.ToString());
            }
            SW.Close();
        }

        #region CodeForUnderlyingData
        public double[] ODValues
        {
            get
            {
                var y = from x in this select x.ODValue;
                return y.ToArray();
            }
        }
        /// <summary>
        /// Time in hours since the start of the
        /// collecting
        /// </summary>
        public double[] TimeValues_As_Double
        {
            get
            { return (from x in this select x.time_as_double).ToArray(); }
        }
        public DateTime[] Times
        {
            get{return (from x in this select x.time).ToArray();}
        }
        public double[] LogODValues
        {
            get { return this.Select(x => Math.Log(x.ODValue)).ToArray(); }
        }
        public void CreateGrowthDataList(DateTime[] Times, double[] ODs)
        {
            if (ODs.Length < 2 || (Times.Length != ODs.Length))
            { throw new Exception("Bad Data, less than 2 points or unequal OD and Time lengths"); }
            //First to create a new list of datapoints
            for (int i = 0; i < Times.Length; i++)
            {
                GrowthDataPoint gdp=new GrowthDataPoint(Times[i], ODs[i]);
                Add(gdp);
                gdp.PropertyChanged+=new PropertyChangedEventHandler(gdp_PropertyChanged);
            }
            //Now to sort by time
            this.Sort((x, y) => x.time.CompareTo(y.time));
            //Now to convert date times to hours decimal.
            ConvertTimesToDoubles();
        }

        void gdp_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!SuspendRefit)
            {
                FitData();
            }
        }
        public double[] OutlierXValues
        {
            get
            {
                var q = from x in this where x.OutlierFlag select x.time_as_double;
                return q.ToArray();
            }
        }
        public double[] OutlierYValues
        {
            get
            {
                var q = from x in this where x.OutlierFlag select x.ODValue;
                return q.ToArray();
            }
        }
        /// <summary>
        /// Probably could be cleaner code by using timespans
        /// </summary>
        private void ConvertTimesToDoubles()
        {
            this.BaseTime = this[0].time;
            double BaseTime = Convert.ToDouble(this[0].time.Ticks);
            foreach (GrowthDataPoint gdp in this)
            {
                double timevaluesdecimal = Convert.ToDouble(gdp.time.Ticks);//store it as a double in ticks
                timevaluesdecimal = timevaluesdecimal - BaseTime;
                timevaluesdecimal = (timevaluesdecimal / 3600) * (.0000001); //Convert to hours since basetime
                gdp.time_as_double = timevaluesdecimal;
            }
        }

        public double[] FittedXValues
        {
            get { return (from x in this where x.UsedInFit select x.time_as_double).ToArray(); }
        }
        public double[] FittedLogYValues
        {
            get { return (from x in this where x.UsedInFit select Math.Log(x.ODValue)).ToArray(); }
        }
        public double[] FittedYValues
        {
            get { return (from x in this where x.UsedInFit select x.ODValue).ToArray(); }
        }

        #endregion
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
        #endregion
    }
    [Serializable]
    public class FitPoint
    {
        public double x{get;set;}
        public double y{get;set;}

    }
    [Serializable]
    public class GrowthDataPoint : INotifyPropertyChanged
    {
        /// <summary>
        /// The actual time of the measurement
        /// </summary>
        public DateTime time { get; set; }
        /// <summary>
        /// The time in hours since the start of the experiment
        /// </summary>
        public double time_as_double {get;set;}

        public double ODValue{
            get;set;// get { return pODValue; } set{pODValue=value;OnPropertyChange(new PropertyChangedEventArgs("ODValue"));}
        }
        public double LogODValue
        {
            get { return Math.Log(ODValue); }
        }
        private bool pOutlierFlag;
        public bool OutlierFlag
        {
            get { return pOutlierFlag; }
            set
            {
                if (value) this.UsedInFit = false;
                pOutlierFlag = value;
            }
        }
        public bool UsedInFit;
        //For notifications and changes via user interface
        public bool UIUsedInFit
        {
            get
            {
                return UsedInFit;
            }
            set
            {
                UsedInFit = value;
                OnPropertyChange(new PropertyChangedEventArgs("Color"));
            }
        }
        public GrowthDataPoint(DateTime time, double OD)
        {
            this.time = time;
            this.ODValue = OD;
        }
        public override string ToString()
        {
            return this.time_as_double.ToString("n2")+","+this.ODValue.ToString("n3");
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
        #endregion
    }



}

   

