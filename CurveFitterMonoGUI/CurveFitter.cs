using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ZedGraph;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Data.OleDb;
using GrowthCurveLibrary;
using MatrixArrayPlot;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Fit_Growth_Curves
{

    public partial class CurveFitter : Form
    {
        public const double BAD_DATA_VALUE = -999;
        GroupFitter GroupFit;
        double ChartNLastFitXValue= BAD_DATA_VALUE;
        double ChartPickDataLastFitXValue = BAD_DATA_VALUE;
        PlateHeatMap.PlateType CurrentPlateType = PlateHeatMap.PlateType.None;
        private GrowthCurveCollection GCC=new GrowthCurveCollection();
        private GrowthCurveCollection GroupModelGCC = new GrowthCurveCollection();
        //individual growth curves might
        //not have data everywhere
        public CurveFitter()
        {
            InitializeComponent();
            plateMap.SetGrowthCurveCollection(GCC);
            selectablePlateMap1.GroupsChanged += new SelectablePlateMap.ChangedEventHandler(selectablePlateMap1_GroupsChanged);
            TreatmentTextBoxes[1] = txtTreatment1;
            TreatmentTextBoxes[2] = txtTreatment2;
            TreatmentTextBoxes[3] = txtTreatment3;
            TreatmentTextBoxes[4] = txtTreatment4;
            TreatmentTextBoxes[5] = txtTreatment5;
            TreatmentTextBoxes[6] = txtTreatment6;
            ChartN.ZoomEvent += new ZedGraphControl.ZoomEventHandler(ChartN_ZoomEvent);
            ChartPickData.ZoomEvent+=new ZedGraphControl.ZoomEventHandler(ChartPickData_ZoomEvent);
            toDeletePlateMap.IndividualWellChanged += new SelectablePlateMap.ChangedEventHandler(toDeletePlateMap_IndividualWellChanged);
            toDeletePlateMap.SHOW_GROUP_NUMBER = false;
        }
        void  ChartPickData_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
{
 	if (ChartPickDataLastFitXValue != BAD_DATA_VALUE)
            {
                RefitCurve(ChartPickDataLastFitXValue);
                ChartPickDataLastFitXValue = BAD_DATA_VALUE;
            }
}
        //really backwards way to undo a point fit change due to a zoom event
        void ChartN_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            if (ChartNLastFitXValue != BAD_DATA_VALUE)
            {
                RefitCurve(ChartNLastFitXValue);
                ChartNLastFitXValue = BAD_DATA_VALUE;
            }
        }

        #region UPDATE/CHANGE EVENTS

        /// <summary>
        /// Occurs when data is added or removed
        /// </summary>
        public void GCCChangeEvent()
        {
            ClearLists();
            this.CurrentPlateType = plateMap.AssignPlateType((from x in GCC select x.DataSetName));
            selectablePlateMap1.SwitchToNewPlateType(this.CurrentPlateType);
            
            if ((GCC.Count == 48 && this.CurrentPlateType == PlateHeatMap.PlateType.WELL48) || (GCC.Count == 96 && this.CurrentPlateType == PlateHeatMap.PlateType.WELL96))
            {
                toDeletePlateMap.AssignAllWellsToGroup(0);
                toDeletePlateMap.SwitchToNewPlateType(this.CurrentPlateType);
            
            }
           if (GCC.Count > 0)
            {

                foreach (var GD in GCC)
                {
                    toDeletePlateMap.AssignWellToGroup(GD.DataSetName, 1);
                    AddGrowthRateDataToBoxes(GD);                    
                }
                if (CurrentPlateType != PlateHeatMap.PlateType.None)
                {
                    foreach (double value in GCC[0].TimeValues_As_Double)
                    {
                        lstTimePoints.Items.Add(value.ToString("n3"));
                    }
                }
                lstGrowthCurves.SelectedIndex = 0;
                plateMap.SetGrowthCurveCollection(GCC);
                toDeletePlateMap.RecreateImage();
                toDeletePlateMap.Refresh();
            }
        }
        private void ResetChart()
        {
            ChartN.GraphPane.Title.Text = "Log[OD600]";
            ChartN.GraphPane.XAxis.Title.Text = "Hours";
            ChartStandard.GraphPane.Title.Text = "OD[600]";
            txtMaxRange.Text = "";
            txtMaxRange.Text = "";
            lstData.Items.Clear();
            ChartN.GraphPane.CurveList.Clear();
            ChartSlopeN.GraphPane.CurveList.Clear();
            ChartSlopeN.GraphPane.YAxis.Scale.MagAuto = true;
            ChartStandard.GraphPane.CurveList.Clear();
            ChartPickData.GraphPane.CurveList.Clear();

        }
        private void ClearLists()
        {
            //GCC.Clear();
            lstGrowthCurves.Items.Clear();
            lstGrowthCurvesMirror.Items.Clear();
            lstTimePoints.Items.Clear();
            lstGrowthRatesSensitivity.Items.Clear();
        }
        private void RefreshGraphs()
        {

            if (lstGrowthCurves.SelectedIndex != -1)
            {
                lstGrowthCurvesMirror.SelectedIndex = lstGrowthCurves.SelectedIndex;
                try
                {
                    ResetChart();
                    RemakeTreatmentGraph();
                    ChartNLastFitXValue = BAD_DATA_VALUE;
                    ChartNLastFitXValue = BAD_DATA_VALUE;
                    GrowthCurve toPlot = GCC[lstGrowthCurves.SelectedIndex];
                    plotGrowthData(toPlot, true);
                    lstallData(toPlot);

                }
                catch (Exception thrown)
                {
                    MessageBox.Show("Could not plot this data, check the input file\n Exact error: " + thrown.Message);
                }
            }
        }
        private void AddGrowthRateDataToBoxes(GrowthCurve GR)
        {
            lstGrowthCurves.Items.Add(GR.ToString());
            lstGrowthCurvesMirror.Items.Add(GR.ToString());
            lstGrowthRatesSensitivity.Items.Add(GR.ToString());
        }
        #endregion
        private void tempLoad()
        {
            string fname = @"C:\Users\Nigel\Documents\My Dropbox\Media_Paper\Fitter_Manuscript\Experiments\ReproducibilityAndPlateType\ND_CoStar_17\\" + ImportRobotData.NameofTempFile ;
            ImportDateTimeCSV.FillCurveCollectionFromCSV(fname, GCC);
            GCCChangeEvent();
            btnDeleteFirstBlank_Click(null, null);
            btnDeleteFirstBlank.PerformClick();
            //btnCallOutliers.PerformClick();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo DI=new DirectoryInfo(System.Environment.CurrentDirectory);
            System.Environment.SetEnvironmentVariable("SHODIR",DI.Parent.FullName);

            //tempLoad();
            selectablePlateMap1.AssignAllWellsToGroup(1);
            toDeletePlateMap.CurGroupToSelect = 0;
            //txtDeleteBlanksTab.Rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fcharset0 Arial;}}{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\qc\b\f0\fs20 About This Tab\par\par\pard\tab\b0 Often times when we collect data from plates used with the robots, many of the wells are left blank as controls.  The particular layout any individual uses can vary though.  After loading 96 well plate data, you can come to this tab to delete the blanks if the layout you have used in your plates matches one of the layouts shown here, simply click the appropriate button under the layout of choice.  Red indicates sample wells, while white indicates blanks.\par}";
            sensitivityArray.SwitchToRainbow();
            sensitivityArray.LabelTextColor = Color.Black;
            Font forLabels = new System.Drawing.Font(this.Font.FontFamily,6.0F, FontStyle.Regular);
            sensitivityArray.FontForLabels = forLabels;
            ChartN.GraphPane.Title.Text = "";
            ChartN.GraphPane.XAxis.Title.Text = "";
            ChartN.GraphPane.YAxis.Title.Text = "";
            toDeletePlateMap.AssignAllWellsToGroup(2);
            ChartSlopeN.GraphPane.YAxis.Title.Text = "";
            ChartSlopeN.GraphPane.XAxis.Title.Text = "";
            ChartStandard.GraphPane.YAxis.Title.Text = "";
            ChartStandard.GraphPane.XAxis.Title.Text = "";
            ChartStandard.GraphPane.Title.Text = "";
            ChartSlopeN.GraphPane.Title.Text = "";
            scaleBarSensitivity.AttachArrayToProvideScaleFor(sensitivityArray);
            ChartN.ZoomEvent+=new ZedGraphControl.ZoomEventHandler(ChartN_ZoomEvent);
            ChartPickData.ZoomEvent+=new ZedGraphControl.ZoomEventHandler(ChartPickData_ZoomEvent);
        }
        private void GetData(string FullFileName)
        {
            ImportDateTimeCSV.FillCurveCollectionFromCSV(FullFileName, GCC);
            //first to create an array of values, I know there will be 48 columns in the second one,
            //and for now I am going to assume we will have 200 datapoints, which we will not!
            GCCChangeEvent();
        }
     
        private void lstGrowthCurves_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGraphs();
        }
        private void lstallData(GrowthCurve toList)
        {
            lstData.Items.Clear();
            lstDataMirror.Items.Clear();
            if (toList.ValidDataSet)
            {
                double Rate = Math.Log(2)/toList.GrowthRate.GrowthRate;
                lstData.Items.Add("Growth Rate: " + toList.GrowthRate.GrowthRate.ToString("n3"));
                lstData.Items.Add("Linear Fit: " + toList.LinFit.Slope.ToString("g4"));
                #if !MONO
                if ((bool)(toList.MixtureErrorModel != null))
                {
                    lstData.Items.Add("Robust GR: "+toList.MixtureErrorModel.GrowthRate.ToString("n3"));
                }
#endif
                lstData.Items.Add("Fitted Method: " + toList.GrowthRate.FitterUsed.ToString());
                lstData.Items.Add("R2= " + toList.GrowthRate.R2.ToString("n4"));
                lstData.Items.Add("RMSE = " + toList.GrowthRate.RMSE.ToString("e3"));
                lstData.Items.Add("Num Points = " + toList.GrowthRate.NumPoints.ToString());
                lstData.Items.Add("Fitted Doubling Time: " + Rate.ToString("n4"));
                if (toList.OffSetExp != null)
                {
                    lstData.Items.Add("OffSet: " + toList.OffSetExp.OffSet.ToString("g4"));
                    lstData.Items.Add("OffSet Rate: " + toList.OffSetExp.GrowthRate.ToString("g4"));
                    lstData.Items.Add("OffSet Init: " + toList.OffSetExp.InitialPopSize.ToString("e3"));

                }

            }
            lstData.Items.Add("Note: " + toList.GrowthRate.Notes);
            foreach(object o in lstData.Items)
            {
                lstDataMirror.Items.Add(o);
            }
        }
        private void SetLineValues(LineItem ToChange, Color ColorToUse)
        {
            ToChange.Symbol.Fill.IsVisible = true;
            ToChange.Symbol.Fill.Color = ColorToUse;
            ToChange.Symbol.Size = 10;
            ToChange.Line.Width = 3;
            
        }
        //TODO: This is a damn mess
        private void plotGrowthData(GrowthCurve toPlot, bool ShowPredicted)
        {
            //ChartN is the semi log plot on the main page
            GraphPane ChartNGraph = ChartN.GraphPane;
            ChartNGraph.Title.IsVisible = false;
            ChartNGraph.XAxis.Title.Text = "Hours";
            ChartNGraph.YAxis.Title.Text = "Log OD[600]";
            ChartNGraph.Legend.IsVisible = true;
            ChartStandard.GraphPane.XAxis.Title.Text = "Hours";
            ChartStandard.GraphPane.YAxis.Title.Text = "OD600";

            double[] times = toPlot.TimeValues_As_Double;
            double[] logs = toPlot.LogODValues;
            double[] FittedXValues = toPlot.FittedXValues;
            double[] FittedYValues = toPlot.FittedYValues;
            SetLineValues(ChartN.GraphPane.AddCurve("Values", times, logs, Color.Green, SymbolType.Square),Color.Green);
            List<double> yOutleirs=toPlot.OutlierYValues.ToList();
          
            var youtl=yOutleirs.Select(x=>Math.Log(x)).ToArray();
            var v = ChartN.GraphPane.AddCurve("Outliers", toPlot.OutlierXValues,youtl , Color.Red, SymbolType.Star);
            v.Line.IsVisible = false;
            bool ShouldPlot;
            if (GrowthCurve.USE_EXPONENTIAL_FITTING) ShouldPlot = toPlot.ExpModelFitted;
            else
            {
                ShouldPlot = (toPlot.LinearModelFitted && toPlot.MaxGrowthRate.MaxGrowthRate != GrowthCurve.DEFAULT_MAX_GROWTH_RATE);
            }
                //Now The Data THAT WAS USED TO MAKE THE FIT
            if (toPlot.ValidDataSet && toPlot.FittedXValues.Length > 1)
            {
                PointPairList PL = new PointPairList(toPlot.FittedXValues, toPlot.FittedLogYValues);
                SetLineValues(ChartN.GraphPane.AddCurve("Fitted Values", PL, Color.DarkBlue, SymbolType.Square), Color.DarkBlue);
            }
            //NOW THE FIT ITSELF
            if (toPlot.ExpModelFitted && GrowthCurve.USE_EXPONENTIAL_FITTING && ShowPredicted)
            {
                //IF THE DATA HAS BEEN FIT, THIS FIRST ROUTINE WILL PLOT THE FITTED line on the top log graph
                //not this fit is from the exponential
                double[] x, y;
                toPlot.ExpFit.GenerateFitLine(0, .1, FittedXValues.Max(), out x, out y);
                for (int i = 0; i < y.Length; i++)
                {
                    if (y[i] == 0) { continue; }
                    y[i] = Math.Log(y[i]);
                }
                PointPairList ExpFitted = new PointPairList(x, y);
                LineItem ExpFitLineNormal= ChartN.GraphPane.AddCurve("Exp Fit", ExpFitted, Color.Plum, SymbolType.Plus);
                ExpFitLineNormal.Symbol.IsVisible = false;
                ExpFitLineNormal.Line.Width = 3;   
                
            }
            else if (toPlot.LinearModelFitted && !GrowthCurve.USE_EXPONENTIAL_FITTING && ShowPredicted)
            {
                //IF THE DATA HAS BEEN FIT, THIS FIRST ROUTINE WILL PLOT THE FITTED line on the top log graph
                //not this fit is from the exponential
                double[] x, y;
                toPlot.LinFit.GenerateFitLine(0, .1, FittedXValues.Max(), out x, out y);
                PointPairList LinFitted = new PointPairList(x, y);
                LineItem LinFitLineNormal = ChartN.GraphPane.AddCurve("Lin Fit", LinFitted, Color.Plum, SymbolType.Plus);
                LinFitLineNormal.Symbol.IsVisible = false;
                LinFitLineNormal.Line.Width = 3;

            }
           
            //NOW FOR THE NORMAL SCALE
            try
            {
                bool ExtendFit = false;
                double xmax;
                if (ExtendFit)
                    xmax = (from x in toPlot where x.ODValue == toPlot.HighestODValue select x.time_as_double).First();
                else
                    xmax = toPlot.FittedXValues.Max();

                if ((chkShowLin.Checked && toPlot.LinearModelFitted) || (!GrowthCurve.USE_EXPONENTIAL_FITTING && toPlot.LinearModelFitted))
                {
                    double[] x, y;
                    //Now the predicted line from the linear fit plotted NORMALLY
                    toPlot.LinFit.GenerateFitLine(FittedXValues.Min(), .1, xmax, out x, out y);
                    double[] y3 = new double[y.Length];
                    for (int i = 0; i < y.Length; i++)
                    {
                        y3[i] = Math.Exp(y[i]);
                    }
                    SimpleFunctions.CleanNonRealNumbersFromYvaluesInXYPair(ref x, ref y);
                    SetLineValues(ChartStandard.GraphPane.AddCurve("Lin Fit", x, y3, Color.Green, SymbolType.None), Color.Green);
                    ChartStandard.GraphPane.Legend.IsVisible = true;
                }
                //NormalScal-inuse
                if (toPlot.LinearModelFitted && ShowPredicted)
                {
                    double[] toPlotX = FittedXValues.ToArray();
                    double[] toPlotY = FittedYValues.ToArray();
                    SimpleFunctions.CleanNonRealNumbersFromYvaluesInXYPair(ref toPlotX, ref toPlotY);
                    ChartStandard.GraphPane.AddCurve("Fitted Data", toPlotX, toPlotY, Color.BlueViolet, SymbolType.Square);
                }
                //Now the predictedLine
                ChartStandard.GraphPane.AddCurve("Data",toPlot.TimeValues_As_Double,toPlot.ODValues,Color.Blue,SymbolType.Square);
                //now any outliers
                var vvv = ChartStandard.GraphPane.AddCurve("Outliers", toPlot.OutlierXValues, toPlot.OutlierYValues, Color.Red, SymbolType.Star);
                vvv.Line.IsVisible = false;
                //Now to add the lag time
                if (toPlot.ExpModelFitted)
                {
                    //First the actual exponential fit
                    double[] x1, y1;
                    
                 
                    toPlot.ExpFit.GenerateFitLine(0, .1, xmax, out x1, out y1);
                    SimpleFunctions.CleanNonRealNumbersFromYvaluesInXYPair(ref x1, ref y1);
                    if (x1.Length > 0)
                    {
                        ChartStandard.GraphPane.AddCurve("Exp Fit", x1, y1, Color.Plum, SymbolType.None);
                   }
                    if (toPlot.OffSetExp != null && toPlot.OffSetExp.SuccessfulFit)
                    {
                        double[] x2, y2;
                        toPlot.OffSetExp.GenerateFitLine(0, .1, xmax, out x2, out y2);
                        SimpleFunctions.CleanNonRealNumbersFromYvaluesInXYPair(ref x2, ref y2);
                        if (x2.Length > 0)
                        {
                           LineItem li= ChartStandard.GraphPane.AddCurve("Offset Fit", x2, y2, Color.Brown, SymbolType.None);
                           li.Line.Width = (float)3.0;
                        }
                    }
                    #if !MONO
                    
                    if (toPlot.MixtureErrorModel != null && chkShowRobustFit.Checked)
                    {
                        double[] x2, y2;
                        toPlot.MixtureErrorModel.GenerateFitLine(0, .1,xmax, out x2, out y2);
                        SimpleFunctions.CleanNonRealNumbersFromYvaluesInXYPair(ref x2, ref y2);
                        if (x2.Length > 0)
                        {
                            LineItem li = ChartStandard.GraphPane.AddCurve("Robust Fit", x2, y2, Color.Black, SymbolType.None);
                            li.Line.Width = (float)2.0;
                        }
                    }
#endif
                    if ((toPlot.LogisticModel != null) && toPlot.LogisticModel.SuccessfulFit)
                    {
                        double[] x2, y2;
                        toPlot.LogisticModel.GenerateFitLine(0, .1, xmax, out x2, out y2);
                        SimpleFunctions.CleanNonRealNumbersFromYvaluesInXYPair(ref x2, ref y2);
                        if (x2.Length > 0)
                        {
                            LineItem li = ChartStandard.GraphPane.AddCurve("Logistic Fit", x2, y2, Color.MidnightBlue, SymbolType.None);
                            li.Line.Width = (float)2.0;
                        }
                    }
                }
                CreatePickDataPlot(toPlot);
            }
            catch
            {
                ChartPickData.GraphPane.Title.Text = "This was too weird to plot";
                ChartPickData.GraphPane.CurveList.Clear();

                ChartStandard.GraphPane.Title.Text = "This was too weird to plot";
                ChartStandard.GraphPane.CurveList.Clear();
            }
            //NOW THE SLOPE GRAPH
            //FIRST FOR AN INFINITY CHECK
            ChartSlopeN.GraphPane.AddCurve("DoublingTime", toPlot.XvaluesForSlope, toPlot.SlopeChange, Color.Black, SymbolType.Square);
            ChartSlopeN.GraphPane.Legend.IsVisible = false;
            ChartSlopeN.GraphPane.YAxis.Title.Text = "Hours";
            ChartSlopeN.GraphPane.XAxis.Title.Text = "Center Time OD Value";
            ChartSlopeN.GraphPane.Title.Text = "Doubling Time Between Reads";
            txtMaxRange.Text = ChartSlopeN.GraphPane.YAxis.Scale.Max.ToString();
            txtMinRange.Text = ChartSlopeN.GraphPane.YAxis.Scale.Min.ToString();

            //Now to make sure the order is right, reverse everything
           ChartN.GraphPane.CurveList.Reverse();
           ChartSlopeN.GraphPane.CurveList.Reverse();
           ChartPickData.GraphPane.CurveList.Reverse();

            ChartN.GraphPane.AxisChange();
            ChartSlopeN.GraphPane.AxisChange();
            ChartStandard.GraphPane.AxisChange();
            ChartPickData.GraphPane.AxisChange();
            
            ChartN.Refresh();
            ChartSlopeN.Refresh();
            ChartPickData.Refresh();
            ChartStandard.Refresh();
        }
        private void CreatePickDataPlot(GrowthCurve toPlot)
        {

            ChartPickData.GraphPane.Title.Text = "Pick a Value to Change it";
            ChartPickData.GraphPane.XAxis.Title.Text = "Time";
            ChartPickData.GraphPane.YAxis.Title.Text = "Log[OD600]";
            SetLineValues(ChartPickData.GraphPane.AddCurve("", toPlot.TimeValues_As_Double, toPlot.LogODValues, Color.Blue, SymbolType.Square), Color.Blue);
            //Now The Data in the fit
            if (toPlot.ValidDataSet && toPlot.GrowthRate.FitterUsed!=null  && chkShowFittedPick.Checked)
            {
                SetLineValues(ChartPickData.GraphPane.AddCurve("Fitted", toPlot.FittedXValues, toPlot.FittedLogYValues, Color.BlueViolet, SymbolType.Square), Color.BlueViolet);
                double[] x, y;
                
                toPlot.GrowthRate.FitterUsed.GenerateFitLine(0, .1, SimpleFunctions.Max(toPlot.FittedXValues), out x, out y);
                if (toPlot.GrowthRate.FitterUsed is ExponentialFit)
                {
                    y = y.Select((q) => Math.Log(q)).ToArray();
                }
                PointPairList Fitted = new PointPairList(x, y);
                LineItem FitLineNormal = ChartPickData.GraphPane.AddCurve(toPlot.GrowthRate.FittingUsed, Fitted, Color.Plum, SymbolType.Plus);
                FitLineNormal.Symbol.IsVisible = false;
                FitLineNormal.Line.Width = 2;
                
            }
        }
        private void SelectFileToOpen()
        {
            
            ClearLists();
            GCC.Clear();
            openFileDialog1.Title = "Select the file with Growth Curve Data";
            openFileDialog1.Filter = "CSV Files | *.csv|All Files | *.csv";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Length > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                this.Text = "Curve Fitter - " + openFileDialog1.FileName;
                try { GetData(openFileDialog1.FileName); }
                catch (Exception thrown) { MessageBox.Show("Could not open the file, please check the format \n Also, make sure it is not open by another program\n\n  Error: " + thrown.Message); }
            }
            else { MessageBox.Show("You must pick a file"); }//this.Close(); }
            this.Cursor = Cursors.Default;
            

        }
        private void MenuOpenFile_Click(object sender, EventArgs e)
        {
            SelectFileToOpen();
        }
        private void btnChangeAxis_Click(object sender, EventArgs e)
        {
            try
            {
                ChartSlopeN.GraphPane.YAxis.Scale.Min=Convert.ToDouble(txtMinRange.Text);
                ChartSlopeN.GraphPane.YAxis.Scale.Max = Convert.ToDouble(txtMaxRange.Text);
                ChartSlopeN.Refresh();
            }
            catch
            {
                MessageBox.Show("Could not change Y axis, please check your inputs");
            }
        }
        private void exportDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".csv";
            saveFileDialog1.Filter = "CSV FILE(*.csv) | *.csv";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName.Length > 0)
            {
                try
                {
                    foreach (GrowthCurve gc in GCC)
                    {
                        gc.Population = selectablePlateMap1.GetGroupAssignmentForWell(gc.DataSetName).ToString();
                    }
                }
                catch { }
                try { ExportDataClasses.ExportData(saveFileDialog1.FileName,GCC); }
                catch(Exception thrown)
                { MessageBox.Show("Could not save the file, please check the format\nActual Error is:  "+thrown.Message); }
            }
            else { MessageBox.Show("You must pick a file"); }
        }
        private void lstGrowthCurvesMirror_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstGrowthCurves.SelectedIndex = lstGrowthCurvesMirror.SelectedIndex;
        }
        private void importPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Filename = "";
            try
            {
                DialogResult DR = openFileDialog1.ShowDialog();
                openFileDialog1.Filter = "CSV FILE(*.csv) | *.csv"; 
                if (DR == DialogResult.OK)
                {
                    Filename = openFileDialog1.FileName;
                    importPreviousDataFile(Filename);
                    this.Text = "Growth Curve " + openFileDialog1.FileName;
                }
            }
            catch (Exception thrown)
            {
                MessageBox.Show("Unable to import this file, this could have corrupted the program and it is recommended that you save "
                + "and restart.  Check your file to insure that you did not alter the formatting after it was exported from the curve fitter, excel will do "
                + "this without telling you.  Talk to Nigel to correct these issues\n\n\n" + thrown.Message + "\n\n\n" + Filename, "File Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        private void importPreviousDataFile(string file)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GrowthCurveCollection gcnew = ImportPreviousFile.importPreviousDataFile(file);
                GCC.Clear();
                GCC.AddRange(gcnew);
                GCCChangeEvent();
            }
            catch (Exception thrown)
            {
                MessageBox.Show("Could not load file.\n" + thrown.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private string runSaveFileDialog(string filtername, string ext)
        {
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ext;
            saveFileDialog1.Filter = filtername;
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName.Length > 0)
            {
                return saveFileDialog1.FileName;
            }
            else
            {
                MessageBox.Show("You must pick a file");
                return "Failed";
            }
        }
       
        private void btnDeleteCurve_Click(object sender, EventArgs e)
        {
            if (lstGrowthCurves.SelectedIndex != -1)
            {   
                int toRemove=lstGrowthCurves.SelectedIndex;
                DeleteCurve(toRemove);
            }
        }
        private void DeleteCurve(int IndexToDelete)
        {
            
            GCC.RemoveAt(IndexToDelete);
            GCCChangeEvent();
            
        }
        private void chkShowLin_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGraphs();
        }


     
        private void saveMultipleChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Right Click On The Graph To Do This","Wrong Spot",MessageBoxButtons.OK,MessageBoxIcon.Information);
      
        }
        private void btnTemp_Click(object sender, EventArgs e)
        {
            try
            {

                int startindex = Convert.ToInt32(txtStartPoint.Text)-1;
                int endindex = Convert.ToInt32(txtEndPoint.Text)-1;
                foreach (GrowthCurve GR in GCC)
                {
                    try
                    {
                        if (GR.Count < endindex)
                        {
                            MessageBox.Show(GR.DataSetName + " only has " + GR.Count.ToString() + " points");
                            break;
                        }
                        //GrowthCurve GR = (GrowthCurve)GRs;
                        GR.SetFittedRange(startindex, endindex);
                    }
                    catch (Exception thrown)
                    {
                        MessageBox.Show("Failed to change range for : " + GR.ToString() + "\n" + thrown.Message);
                        break;
                    }
                }

                RefreshGraphs();
                RemakeTreatmentGraph();
                RefreshPlateHeatMap();
            }
            catch(Exception thrown) {MessageBox.Show("Error setting points\n\n"+thrown.Message); }
        }
    

        /// <summary>
        /// Subtracts the first reading from each position as the blank
        /// </summary>
        private void SubtractFirstReadingFromAll()
        {
            this.Cursor = Cursors.WaitCursor;
            GCC.RemoveDataPointsAsBlanks(true);
            
            RefreshGraphs();
            this.Cursor = Cursors.Default;
        }
        private void SubtractSecondReadingFromAll()
        {
            this.Cursor = Cursors.WaitCursor;
            GCC.RemoveDataPointsAsBlanks(false);
            RefreshGraphs();
            this.Cursor = Cursors.Default;
        }
        private void SubtractAverageOfFirstThreeReadingsFromAll()
        {
            this.Cursor = Cursors.WaitCursor;
            foreach (GrowthCurve GC in GCC)
            {
                GC.RemoveAverageOfFirstThreePointsAsBlank();
            }
            RefreshGraphs();
            this.Cursor = Cursors.Default;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int[] rows ={0, 1, 7, 8};
            int[] BlankIndexes=new int[(rows.Length*12)];//this is the index of the blank values in the listbox
            int indexer=0;
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    BlankIndexes[indexer] = 12 * rows[i] + j;
                    indexer++;
                }                
            }
            //now to grab these blank values
            double BlankSum=0;
            double BlankCount=0;
            for (int i = 0; i < BlankIndexes.Length; i++)
            {
                GrowthCurve GD = (GrowthCurve)lstGrowthCurves.Items[i];
                foreach (double dbl in GD.ODValues)
                {
                    BlankSum += dbl;
                    BlankCount++;
                }
            }
            double Blank = BlankSum / BlankCount;
            SubtractBlankFromAll(Blank);   
        }
      
        private void btnDeleteBlanks_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            int[] rows ={ 0, 1, 6, 7 };
            int[] BlankIndexes = new int[(rows.Length * 12)];//this is the index of the blank values in the listbox
            int indexer = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    BlankIndexes[indexer] = 12 * rows[i] + j;
                    indexer++;
                }
            }
            for (int i = BlankIndexes.Length - 1; i > -1; i--)
            {
                //MessageBox.Show(lstGrowthCurves.Items.Count.ToString()+" "+BlankIndexes[i].ToString());
                lstGrowthCurves.Items.RemoveAt(BlankIndexes[i]);
                lstGrowthCurvesMirror.Items.RemoveAt(BlankIndexes[i]);
               
            }
            this.Cursor = Cursors.Default;            
        }
        //Plate_Tools PT;
        //This is embarrassing, really need to add a control
        PlateHeatMap.GrowthCurveDoubleValueGetter curPlateValueFunction;
        private void RefreshPlateHeatMap()
        {
            try
            {
                lblRobo.Text="";
            plateMap.SetValue(curPlateValueFunction);
               
            List<double> Data= (GCC.Select((X)=>SafeGet(new GetValueForTreatment(curPlateValueFunction),X)).Where(x=>SimpleFunctions.IsARealNumber(x))).ToList();
            HistogramData(Data, (Data.Count() / 3), graphHistogram.GraphPane);
            graphHistogram.GraphPane.Title.Text = "Histogram";
            graphHistogram.GraphPane.XAxis.Title.Text = "";
            graphHistogram.GraphPane.YAxis.Title.Text = "";
            graphHistogram.AxisChange();
            graphHistogram.Refresh();
            }
            catch
            {
                 lblRobo.Text = "NO ROBOT DATA TO DISPLAY OR ERROR IN DISPLAYING IT";
            }
        }
        private void rbtnPlateOptions_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;
            if (!rbtn.Checked)
            {
                return;
            }
            //Probably should have used dictionary
            if (sender == rbtnGrowthRate)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.GrowthRate.GrowthRate);
            }
             
            else if (sender == rbtnOffSetGR)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.OffSetExp.GrowthRate);
            }
            else if (sender == rbtnMaxOD)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.HighestODValue);
            }
            else if (sender == rbtnRMSE)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.GrowthRate.RMSE);
            }
            else if (sender == rbtnPlatePlotLogistic15)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.LogisticModel.GetGrowthRateAtODValue(0.15));
            }
            else if (sender == rbtnPlatesLogisticR2)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.LogisticModel.R2);
            }
           
            else if (sender == rbtnDoubleTime)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.GrowthRate.DoublingTime);
            }
            else if (sender == rbtnTimePoint)
            {
                if (lstTimePoints.Items.Count > 0)
                {
                    if (lstTimePoints.SelectedIndex == -1)
                    {
                        lstTimePoints.SelectedIndex = 0;
                    }

                    curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x[lstTimePoints.SelectedIndex].ODValue);
                }
            }
            else if (sender == rbtnIOD)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x[0].ODValue);
            }
            else if (sender == rbtnOffSet)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.OffSetExp.OffSet);
            }
            else if (sender == rbtnIntercept)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.ExpFit.InitialPopSize);
            }
            else if (sender == rbtnPlateOffSetIDMinusStart)
            {
                curPlateValueFunction = new PlateHeatMap.GrowthCurveDoubleValueGetter((x) => x.OffSetExp.OffSet - x[0].ODValue);
            }

            RefreshPlateHeatMap();

        }
        

        public void HistogramData(List<double> toPlot, int binNumber, ZedGraph.GraphPane Graph)
        {
            Graph.CurveList.Clear();
            double max = toPlot.Max();
            double min = toPlot.Min();
            max = max * 1.0001;
            min = min * .9999;
            double[] counts = new double[binNumber + 1];
            double[] midpoints = new double[binNumber + 1];
            double interval = (max - min) / Convert.ToDouble(binNumber);
            PointPairList ppl = new PointPairList();
            for (int i = 0; i <= binNumber; i++)
            {
                double cmin = min + i * interval;
                double cmax = cmin + interval;
                midpoints[i] = cmin + (cmax - cmin) / 2;
                int count = toPlot.Count(x => x >= cmin && x < cmax);
                counts[i] = Convert.ToDouble(count);
                ppl.Add(midpoints[i], counts[i]);
            }
            Graph.AddBar("Distribtuion", ppl, System.Drawing.Color.Blue);

        }
       
        
        private void btnDeleteFirstBlank_Click(object sender, EventArgs e)
        {
            try
            {
                SubtractFirstReadingFromAll();
            }
            catch (Exception thrown)
            {
                MessageBox.Show("Could not subtract blank, error is: " + thrown.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lstGrowthCurves_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete || e.KeyCode==Keys.Back) 
            { btnDeleteCurve.PerformClick(); }
        }
        private void btnFitODRange_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                double startOD = Convert.ToDouble(txtMinOD.Text);
                double endOD = Convert.ToDouble(txtMaxOD.Text);
                double endPercent = Convert.ToDouble(txtMaxODPercent.Text);
                //Parallel.ForEach(GrowthCurve GR in GCC)
                Parallel.ForEach(GCC, (GR) =>
                {
                    try
                    {
                        if (!chkUsePercent.Checked)
                        {
                            GR.SetFittedODRange(startOD, endOD, chkODMustIncrease.Checked);
                        }
                        else
                        {
                            GR.SetFittedODRangeFromPercent(startOD, endPercent);
                        }
                    }
                    catch (Exception thrown)
                    {
                        MessageBox.Show("Failed to change range for : " + GR.ToString() + "\nRecommend you shut down\n\n" + thrown.Message);
                    }
                });

                RefreshGraphs();
            }
            catch (Exception thrown) { MessageBox.Show("Error setting points\n\n" + thrown.Message); }

            finally { this.Cursor = Cursors.Default; }
        }
        private void ChartPickData_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    CurveItem CI;
                    int outVal;
                    PointF location = (PointF)e.Location;

                    GrowthCurve GR = GCC[lstGrowthCurves.SelectedIndex];
                    //Somethimes this returns a value from the fitted line instead of
                    //from the data points, this work around will check for this, and remove it.
                    var q = ChartPickData.GraphPane.FindNearestPoint(location, out CI, out outVal);
                    bool IsCorrect = true;
                    
                    if (CI.Points.Count == GR.TimeValues_As_Double.Length)
                    {
                        for( int pIndex=0;pIndex<CI.Points.Count;pIndex++)
                        {
                            if (CI.Points[pIndex].X != GR.TimeValues_As_Double[pIndex])
                            { IsCorrect = false; break; }
                            pIndex++;
                        }
                    }
                    else
                    { IsCorrect = false; }
                     PointPair selected = CI.Points[outVal];

                    //if not the right curve, find the point closest in the curve
                    //for the actual data
                    double XValue=selected.X;
                    if (!IsCorrect)
                    {
                        var Difs=from x in GR.TimeValues_As_Double select new {val=x,dif=Math.Abs(x-XValue)};
                        double smallestDif=(double)Difs.Min((a) => a.dif);
                        var values = from x in Difs where x.dif == smallestDif select x.val;
                        XValue = values.First();
                    }
                    RefitCurve(XValue);

                   GR.SetHandPicked();
                    ChartPickDataLastFitXValue=XValue;
                    
                }
            }
            catch { }
        }

      

        private void lstTimePoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPlateHeatMap();
        }

        private void chkShowFittedPick_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGraphs();
        }
        void selectablePlateMap1_GroupsChanged(object sender, EventArgs e)
        {
            RemakeTreatmentGraph();

        }
        private void openDirectoryWithExcelDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if !MONO
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ClearLists();
                GCC.Clear();
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.Description = "Select the directory that contains ONLY the data files";
                folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowserDialog1.SelectedPath = Properties.Settings.Default.LastSelectedPath;
                DialogResult DR = folderBrowserDialog1.ShowDialog();
                if (DR == DialogResult.OK)
                {
                    ImportRobotData.GetExcelData(folderBrowserDialog1.SelectedPath);
                    ImportDateTimeCSV.FillCurveCollectionFromCSV(folderBrowserDialog1.SelectedPath + "\\" + ImportRobotData.NameofTempFile,GCC);
                   
                    this.Text = "Growth Curve " + folderBrowserDialog1.SelectedPath;
                    Properties.Settings.Default.LastSelectedPath = folderBrowserDialog1.SelectedPath;
                }
                GCCChangeEvent();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Could not load the data, the error was:\n\n" + thrown.Message);
            }
#else
            MessageBox.Show("Excel files can only be accessed from the windows version");
#endif
        }
        private void btnDelete2ndPoint_Click(object sender, EventArgs e)
        {
            try
            {
                SubtractSecondReadingFromAll();
                RefreshGraphs();
            }
            catch (Exception thrown)
            {
                MessageBox.Show("Could not subtract blank, error is: " + thrown.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeleteAvg3asBlank_Click(object sender, EventArgs e)
        {
            try
            {
                SubtractAverageOfFirstThreeReadingsFromAll();
                RefreshGraphs();
            }
            catch (Exception thrown)
            {
                MessageBox.Show("Could not subtract blank, error is: " + thrown.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lstTreatmentSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTreatmentSelection.SelectedIndex > -1)
            {
                selectablePlateMap1.CurGroupToSelect = lstTreatmentSelection.SelectedIndex;
            }
        }
        private void chkTreatShowLog_CheckedChanged(object sender, EventArgs e)
        {
            RemakeTreatmentGraph();
        }
        private void btnClearTreatments_Click(object sender, EventArgs e)
        {
            selectablePlateMap1.ClearAllAssignments();
            RemakeTreatmentGraph();
        }
        private void RefitCurve(double XValueToChange)
        {
            GrowthCurve GR = GCC[lstGrowthCurves.SelectedIndex];
            var q = from x in GR where x.time_as_double==XValueToChange select x;
            if (q.Count() == 1)
            {
                GrowthDataPoint gdp = q.First();
                gdp.UIUsedInFit= !gdp.UsedInFit;
                GR.SetHandPicked();
                RefreshGraphs();
                //bool worked = GR.ChangePoint(XValueToChange);//, selected.Y);
                //int IndexToReplace = lstGrowthCurves.SelectedIndex;
                //if (worked)
                //{
                //    lstGrowthCurves.Items[IndexToReplace] = GR;
                //    lstGrowthCurves.SelectedIndex = IndexToReplace;
                //}
            }
        }
        private void ChartN_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
               
                if (e.Button == MouseButtons.Left)
                {
                    CurveItem CI;
                    int outVal;
                    PointF location = (PointF)e.Location;
                    GrowthCurve GR = GCC[lstGrowthCurves.SelectedIndex];
                    //Somethimes this returns a value from the fitted line instead of
                    //from the data points, this work around will check for this, and remove it.
                    var q = ChartN.GraphPane.FindNearestPoint(location, out CI, out outVal);
                    bool IsCorrect = true;
                    if (CI.Points.Count == GR.TimeValues_As_Double.Length)
                    {
                        for (int pIndex = 0; pIndex < CI.Points.Count; pIndex++)
                        {
                            if (CI.Points[pIndex].X != GR.TimeValues_As_Double[pIndex])
                            { IsCorrect = false; break; }
                            
                        }
                    }
                    else
                    { IsCorrect = false; }
                    PointPair selected = CI.Points[outVal];

                    //if not the right curve, find the point closest in the curve
                    //for the actual data
                    double XValue = selected.X;
                    if (!IsCorrect)
                    {
                        var Difs = from x in GR.TimeValues_As_Double select new { val = x, dif = Math.Abs(x - XValue) };
                        double smallestDif = (double)Difs.Min((a) => a.dif);
                        var values = from x in Difs where x.dif == smallestDif select x.val;
                        XValue = values.First();
                    }
                    RefitCurve(XValue);
                    GR.SetHandPicked();
                    ChartNLastFitXValue = XValue;
  
                }
            }
            catch { }
        }

        private void importPlateKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                 int Assigned=0;
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.Title = "Select Plate Map File";
                OFD.Filter = "CSV FILE(*.csv) | *.csv";
                DialogResult DR = OFD.ShowDialog();
                if (DR == DialogResult.OK)
                {
                    selectablePlateMap1.ClearAllAssignments();
                    StreamReader SR = new StreamReader(OFD.OpenFile());
                    string line;
                    int curRow=0;
                   
                    while ((line = SR.ReadLine()) != null)
                    {
                        string[] sp = line.Split(',');
                        string rows="ABCDEFGH";
                        string cols="12345678";
                        int max= sp.Length== 8 ? 8 : sp.Length;
                        char well = rows[curRow];
                        for (int i = 0; i < max; i++)
                        {
                            try
                            {
                                int group = Convert.ToInt32(sp[i]);
                                char col = cols[i];
                                string name = well.ToString() + col.ToString();
                                selectablePlateMap1.AssignWellToGroup(name, group);
                                Assigned++;
                            }
                            catch (Exception thrown)
                            { }
                            
                        }
                        curRow++;
                        if (curRow > 6)
                            break;
                    }
                    SR.Close();
                    selectablePlateMap1.RecreateImage();
                }
                this.Cursor = Cursors.Default;
                MessageBox.Show(Assigned.ToString()+" Wells Assigned");
            }
            catch (Exception thrown)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Could not load the data, the error was:\n\n" + thrown.Message);
            }
        }


        private void btnMakeEvoGroups_Click(object sender, EventArgs e)
        {
            int max = 48;
            if (CurrentPlateType == PlateHeatMap.PlateType.WELL96)
                max = 96;
            for (int i = 0; i < max; i++)
            {
              selectablePlateMap1.AssignWellToGroup(selectablePlateMap1.IntsToCellName[i], 1);
            }
            //selectablePlateMap1.AssignWellToGroup("A1", 1);
            //selectablePlateMap1.AssignWellToGroup("F8", 1);
            //selectablePlateMap1.AssignWellToGroup("F1", 1);
            //selectablePlateMap1.AssignWellToGroup("A8", 1);
            selectablePlateMap1.RecreateImage();
            selectablePlateMap1.Refresh();
            RemakeTreatmentGraph();
            
        }  

        public bool AssignRows = true;
        private void btnRowGroups_Click(object sender, EventArgs e)
        {
            int rowCount = 6;
            int colCount = 8;
            if (this.CurrentPlateType == PlateHeatMap.PlateType.WELL96)
            {
                rowCount = 8; colCount = 12;
            }
            string Rows = "ABCDEFGH";
            
           
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 1; j < (colCount+1); j++)
                    {

                        string Name = Rows[i].ToString() + j.ToString();
                        if (AssignRows)
                        {selectablePlateMap1.AssignWellToGroup(Name, i+1);}
                        else
                        {selectablePlateMap1.AssignWellToGroup(Name, j );}
                    }
                }
            //Now to toggle
            if(AssignRows)
            {
                AssignRows=false;
                btnRowGroups.Text="Assign Cols to Groups";
            }
            else
            {
                AssignRows=true;
                btnRowGroups.Text = "Assign Rows to Groups";
            }
                selectablePlateMap1.RecreateImage();
                selectablePlateMap1.Refresh();
                RemakeTreatmentGraph();
            }

        private void btnDeleteLikelyBlanks_Click(object sender, EventArgs e)
        {
            List<GrowthCurve> toRemove = new List<GrowthCurve>();

            foreach (var g in GCC)
            {
                if (g.HighestODValue < 0.05)
                {
                    
                    toDeletePlateMap.AssignWellToGroup(g.DataSetName, 0);
                    toRemove.Add(g);
                }
            }
            toRemove.ForEach(x => GCC.Remove(x));
            GCCChangeEvent();          
        }
        private void btnDeleteEvolution_Click(object sender, EventArgs e)
        {
            List<string> NamedRemove = new List<string>();
            string RowNames = "ABCDEF";
            int CurRowType = 1;//indicates odd row
            foreach (char c in RowNames)
            {
                for (int i = 1; i <= 8; i++)
                {
                    if((i%2)==CurRowType)
                        NamedRemove.Add(c.ToString() + i.ToString());                    
                }
                CurRowType = CurRowType == 1 ? 0 : 1;//switch row type
            }
            NamedRemove.Remove("A1");
            NamedRemove.Remove("F8");
            foreach (string name in NamedRemove)
            {
                for (int i = 0; i < lstGrowthCurves.Items.Count; i++)
                {
                    if (lstGrowthCurves.Items[i].ToString() == name)
                    {
                        lstGrowthCurves.Items.RemoveAt(i);
                        lstGrowthCurvesMirror.Items.RemoveAt(i);
                        lstGrowthRatesSensitivity.Items.RemoveAt(i);
                    }
                }
            }
        }
        private void lstGrowthRatesSensitivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSensitivityAnalysis();           
        }
        private void DoSensitivityAnalysis()
        {
            if (lstGrowthRatesSensitivity.SelectedIndex != -1)
            {
                GrowthCurve GD=GCC[lstGrowthRatesSensitivity.SelectedIndex];
                S.DoSensitivityAnalysis(GD, sensitivityArray);
            }
        }

        private Sensitivities S=new StartEndODRange();
        private void SensitivityCriteriaChanged(object sender, EventArgs e)
        {
                if (sender == rbtnSensRange)
                {
                    S = new StartEndODRange();
                }
                else if (sender == rbtnSensStartEndLog)
                {
                    S = new StartEndODRangeLogFit();
                }
                else if (sender == rbtnSensBiasError)
                {
                    S = new StartEndODRangeWithBias();
                }
                else if (sender == rbtnSensBiasLinear)
                {
                    S = new StartEndODRangeWithBiasLinear();
                }
                else { throw new Exception("No button checked when one was called for."); }
                
                this.lblXAxis.Text=S.xAxis;
                this.lblYAxis.Text=S.yAxis;
            DoSensitivityAnalysis();
        
        }

        private void exportRawDataForMatlabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".csv";
            saveFileDialog1.Filter = "CSV FILE(*.csv) | *.csv";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName.Length > 0)
            {
                try { ExportDataClasses.ExportMatlabData(saveFileDialog1.FileName,GCC); }
                catch (Exception thrown)
                { MessageBox.Show("Could not save the file, please check the format\nActual Error is:  " + thrown.Message); }
            }
            else { MessageBox.Show("You must pick a file"); }
        }
        private void ResetPlateDeletionProtocol()
        {
            toDeletePlateMap.ClearAllAssignments(false);
            foreach (GrowthCurve gd in GCC)
            {
                string Name = gd.ToString();
                if (toDeletePlateMap.isValidWellName(Name))
                {
                    toDeletePlateMap.AssignWellToGroup(Name, 1);
                }
            }
            toDeletePlateMap.RecreateImage();
        }
        void toDeletePlateMap_IndividualWellChanged(object sender, EventArgs e)
        {
            SelectablePlateEventArgs ev = (SelectablePlateEventArgs)e;
            if (ev.GroupAssignedTo == 0)
            {
                string WellID = ev.WellID;
                var b = from x in GCC where x.DataSetName == WellID select x;
                if (b.Count() > 0)
                {
                    GCC.Remove(b.First());
                    GCCChangeEvent();
                    toDeletePlateMap.RecreateImage();
                }
            }
        }
        private void SubtractBlankFromAll(double Value)
        {
            
            foreach (GrowthCurve o in GCC)
            {
                o.RemoveBlank(Value);
            }
            RefreshGraphs();
        }

        private void btnSubtractWrittenBlank_Click_1(object sender, EventArgs e)
        {

            try
            {
                double initOD = Convert.ToDouble(txtBlankValue.Text);
                SubtractBlankFromAll(-initOD);
                
            }
            catch { MessageBox.Show("Error, could not add Initial OD, did you write a number in the box?"); }
        }

     
        private void chkTreat_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            if (btn.Checked)
            {
                RemakeTreatmentGraph();
            }
        }
        private void btnCallOutliers_Click(object sender, EventArgs e)
        {
            #if !MONO
                    
            //BayesianOutlierDetector bod = new BayesianOutlierDetector();

            //MixtureErrorModel mem = new MixtureErrorModel(GCC[0]);
            this.Cursor = Cursors.WaitCursor;
            try
            {
                foreach (GrowthCurve gc in GCC)
                {
                    gc.CallOutliers();
                }
                RefreshGraphs();
            }
            catch (Exception thrown)
            {
                MessageBox.Show("Error Calling Outliers:\n"+thrown.Message);
            }
            finally { Cursor = Cursors.Default; }
            #else
            MessageBox.Show("Not available in Mono Version");
            #endif
                    
        }
        private void PickleGCC()
        {
            GCC.PickleData();
        }
        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if !MONO
            PickleGCC();
            FileInfo finfo = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
            string start = finfo.Directory.FullName + @"\ShoConsole32.exe";
            Process notePad = new Process();
            notePad.StartInfo.FileName = start;
            notePad.Start();
#endif
            }

        private void open16MinuteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearLists();
            GCC.Clear();
            openFileDialog1.Title = "Select the file with Growth Curve Data";
            openFileDialog1.Filter = "CSV Files | *.csv|All Files | *.csv";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Length > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                this.Text = "Curve Fitter - " + openFileDialog1.FileName;
                try 
                {
                    
                    var GCC2 = Import16MinuteFile.ImportFile(openFileDialog1.FileName);
                    GCC.Clear();
                    GCC.AddRange(GCC2);
                    GCCChangeEvent();
                }
                catch (Exception thrown) { MessageBox.Show("Could not open the file, please check the format \n Also, make sure it is not open by another program\n\n  Error: " + thrown.Message); }
            }
            else { MessageBox.Show("You must pick a file"); }//this.Close(); }
            this.Cursor = Cursors.Default;
        }

        private void openFileWithNumberedHoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearLists();
            GCC.Clear();
            openFileDialog1.Title = "Select the file with Growth Curve Data";
            openFileDialog1.Filter = "CSV Files | *.csv|All Files | *.csv";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Length > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                this.Text = "Curve Fitter - " + openFileDialog1.FileName;
                try 
                {

                    ImportNumericTimeCSV.FillCurveCollectionFromCSV(openFileDialog1.FileName, GCC);
                    GCCChangeEvent();
                     
                }
                catch (Exception thrown) { MessageBox.Show("Could not open the file, please check the format \n Also, make sure it is not open by another program\n\n  Error: " + thrown.Message); }
            }
            else { MessageBox.Show("You must pick a file"); }//this.Close(); }
            this.Cursor = Cursors.Default;
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

       





     




       



        


    }
}

