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
using GrowthCurveLibrary;
using ShoNS;
using ShoNS.Array;
using ShoNS.MathFunc;

namespace Fit_Growth_Curves
{

    public partial class CurveFitter : Form
    {
        TextBox[] TreatmentTextBoxes = new TextBox[SelectablePlateMap.MAX_GROUP_ASSIGNMENTS];
        delegate double GetValueForTreatment(GrowthCurve x);
        delegate PointPair Get2ValuesForTreatment(GrowthCurve x);
        private void ClearTreatmentPlot()
        {
                GraphPane Graph = plotTreatments.GraphPane;
                Graph.CurveList.Clear();
        }
       
        private double SafeGet(GetValueForTreatment dataFunction, GrowthCurve gc)
        {
            try
            {
                return dataFunction(gc);
            }
            catch
            {
                return Double.NaN;
            }
        }
        private PointPair SafeGet(Get2ValuesForTreatment dataFunction, GrowthCurve gc)
        {
            try
            {
                return dataFunction(gc);
            }
            catch
            {
                return null;
            }
        }
        private void RemakeTreatmentGraphWith2(Get2ValuesForTreatment FunctionForData, String Title)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GraphPane Graph = plotTreatments.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = Title;
                Graph.XAxis.Title.Text = "Treatments";
                Graph.YAxis.Title.Text = "";
                Graph.Legend.Position = LegendPos.InsideTopLeft;
                Graph.Legend.FontSpec.Size = 8f;
                Graph.Legend.IsHStack = true;
                SymbolType SymboltoUse = SymbolType.Circle;

                Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                {
                    Color groupColor;
                    List<double> xVals = new List<double>();
                    var curNames = selectablePlateMap1.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                    string GroupName = "";
                    if (TreatmentTextBoxes[i] != null)
                    {
                        GroupName = TreatmentTextBoxes[i].Text;
                    }
                    if (GroupName == "")
                    {
                        GroupName = "Treatment: " + i.ToString();
                    }
                    PointPairList XY = new PointPairList();
                    foreach (string name in curNames)
                    {
                        if (curData.ContainsKey(name))
                        {
                            GrowthCurve GD = curData[name];
                            PointPair xy = SafeGet(FunctionForData, GD);
                            if (xy != null)
                            {
                                XY.Add(xy);
                            }
                        }
                    }
                    if (XY.Count > 0)
                    {
                        LineItem li = Graph.AddCurve(GroupName, XY, groupColor, SymboltoUse);
                        li.Line.IsVisible = false;
                        li.Symbol.Fill = new Fill(groupColor);
                    }
                }
                if (chkTreatLegend.Checked)
                { Graph.Legend.IsVisible = true; }
                else { Graph.Legend.IsVisible = false; }
                Graph.XAxis.Scale.MaxGrace = .05;
                plotTreatments.AxisChange();
                plotTreatments.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void UpdateTreatmentGraphsWithDifference(string BaseLineWell="C4")
        {
            try
            {

                var q = GetDictionaryOfGrowthRateData();
                double[] Baseline = q["C4"].ODValues;
                
                GraphPane Graph = plotTreatments.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = "Growth Plots";
                Graph.XAxis.Title.Text = "Hours";
                Graph.YAxis.Title.Text = "OD Reading-"+BaseLineWell; 
                Graph.Legend.Position = LegendPos.InsideTopLeft;
                Graph.Legend.FontSpec.Size = 8f;
                Graph.Legend.IsHStack = true;
                SymbolType SymboltoUse = SymbolType.Circle;
                Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                {
                    Color groupColor;
                    var curNames = selectablePlateMap1.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                    string GroupName = "";
                    if (TreatmentTextBoxes[i] != null)
                    {
                        GroupName = TreatmentTextBoxes[i].Text;
                    }
                    if (GroupName == "")
                    {
                        GroupName = "Treatment: " + i.ToString();
                    }
                    foreach (string name in curNames)
                    {
                        if (curData.ContainsKey(name))
                        {
                            GrowthCurve GD = curData[name];
                            PointPairList XY;
                            double[] difs=Baseline.Zip(GD.ODValues,(x,y)=>x-y).ToArray();
                            XY = new PointPairList(GD.TimeValues_As_Double,difs);
                            Graph.AddCurve(GroupName, XY, groupColor, SymboltoUse);
                        }
                    }
                }
                if (chkTreatLegend.Checked)
                { Graph.Legend.IsVisible = true; }
                else { Graph.Legend.IsVisible = false; }
                Graph.XAxis.Scale.MaxGrace = .05;
                plotTreatments.AxisChange();
                plotTreatments.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            {
              
             MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            
            }
            finally { this.Cursor = Cursors.Default; }
        }
        private void RemakeTreatmentGraph()
        {
            ClearTreatmentPlot();
            if (rbtnTreatTimevOD.Checked)
            {
                UpdateTreatmentGraphWithGrowthData();
            }
            else if (rbtnPlotSlope.Checked)
            {
                UpdateTreatmentGraphWithSlopeData();
            }
            else if (rbtnGroupsSlopeDoublingsLogOD.Checked)
            {
                UpdateTreatmentGraphWithSlopeData(true);
            }
            else if (rbtnGroupDifFromCenter.Checked)
            {
                UpdateTreatmentGraphsWithDifference();
            }
            else if (rbtnFittedResiduals.Checked) { UpdateTreatmentGraphWithResidualData(true); }
            else if (rbtnPlotLinearResiduals.Checked)
            {
                UpdateTreatmentGraphWithResidualData(true, true);
            }
            else if (rbtnGroupsQuadraticResiduals.Checked)
            {
                UpdateTreatmentGraphWithResidualData(true, false, true);
            }
            else if (rbtnMakeQQPlot.Checked)
            {
                UpdateTreatmentGraphWithQQPlot();
            }
            else if (rbtnGroupsQQLinear.Checked)
            {
                UpdateTreatmentGraphWithQQPlot(true);
            }
            else if (rbtnPlotAllResiduals.Checked) { UpdateTreatmentGraphWithResidualData(false); }
            else if (rbtnInitialPopvGrowthRate.Checked)
            {
                RemakeTreatmentGraphWith2(x => new PointPair(x.ExpFit.InitialPopSize, x.ExpFit.GrowthRate), "Initial Pop Size vs. Growth Rate");
            }
            else if (rbtnMaxvGrowthRate.Checked)
            {
                RemakeTreatmentGraphWith2(x => new PointPair(x.ODValues.Max(), x.ExpFit.GrowthRate), "Max OD vs. Growth Rate");
            }
            else if (rbtnEndODvMax.Checked)
            {
                RemakeTreatmentGraphWith2(x => new PointPair(x.ODValues.Last(), x.ExpFit.GrowthRate), "End OD vs. Growth Rate");
            }
            else if (rbtnRobustGRvEXPGR.Checked)
            {
                RemakeTreatmentGraphWith2(x => new PointPair(x.MixtureErrorModel.GrowthRate, x.ExpFit.GrowthRate), "Robust  vs. L2 Growth Rate");
            }
            else
            {
                GetValueForTreatment valueGetter;
                string Title = "";
                if (rbtnTreatDoublingTime.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.GrowthRate.DoublingTime;
                    Title = "Doubling Time";
                }
                else if (rbtnGroupQuadraticDoubling.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.QuadModel.GrowthRateAtValue(0.12);
                }
                else if (rbtnMixtureModelGR.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.MixtureErrorModel.GrowthRate;
                }
                else if (rbtnGroupsOffSetGrowthRate.Checked)
                {
                    valueGetter = (x) => x.OffSetExp.GrowthRate;
                }
                else if (rbtnMaxMinusEnd.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.ODValues.Max() - x.ODValues.Last();
                }
                else if (rbtnGroupOffSetMinusStart.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.OffSetExp.OffSet - x[0].ODValue;
                }
                else if (rbtnGroupsLinFit.Checked)
                {
                    valueGetter = (x) => x.LinFit.Slope;
                }
                else if (rbtnTreatGrowthRate.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.GrowthRate.GrowthRate;
                    Title = "Growth Rate";
                }
                else if (rbtnTreatMaxOd.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.ODValues.Max();
                    Title = "Max OD";
                }
                else if (rbtnTreatNumPoints.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.GrowthRate.NumPoints;
                    Title = "Number of Points Used";
                }
                else if (rbtnTreatRSq.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.GrowthRate.R2;
                    Title = "R2";
                }
                else if (rbtnTimeToOD.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.HoursTillODReached(.02).GetValueOrDefault(0);
                    Title = "Estimated Time Till OD .02";
                }
                else if (rbtnPlotLastOD.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.ODValues.Last();
                }
                else //(rbtnTreatInitialOD.Checked)
                {
                    valueGetter = (GrowthCurve x) => x.ODValues[0];
                    Title = "Initial OD";
                }
                UpdateTreatmentGraphWithTreatmentData(valueGetter, Title);
            }
        }
        private Dictionary<string,GrowthCurve> GetDictionaryOfGrowthRateData()
        {
            Dictionary<string,GrowthCurve> CurrentDataSets=new Dictionary<string,GrowthCurve>();
            foreach(GrowthCurve gd in GCC)
            {
                CurrentDataSets[gd.ToString()]=gd;
            }
            return CurrentDataSets;
        }
        void UpdateTreatmentGraphWithSlopeData(bool LogScale=false)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GraphPane Graph = plotTreatments.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = "Doubling Times From Neighboring Point Interpolation ";
                Graph.YAxis.Title.Text = "Doubling Time"; 
                Graph.Legend.Position = LegendPos.InsideTopLeft;
                Graph.Legend.FontSpec.Size = 8f;
                Graph.Legend.IsHStack = true;
                //SymbolType SymboltoUse = SymbolType.Circle;
                Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                {
                    Color groupColor;
                    var curNames = selectablePlateMap1.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                    string GroupName = "";
                    if (TreatmentTextBoxes[i] != null)
                    {
                        GroupName = TreatmentTextBoxes[i].Text;
                    }
                    if (GroupName == "")
                    {
                        GroupName = "Treatment: " + i.ToString();
                    }
                    foreach (string name in curNames)
                    {
                        if (curData.ContainsKey(name))
                        {
                            GrowthCurve GD = curData[name];
                            HashSet<double> ht = new HashSet<double>(GD.TimeValues_As_Double);
                            PointPairList XYFit = new PointPairList();
                            PointPairList ZWFit = new PointPairList();
                            if (!LogScale)
                            {
                                GD.SlopeValues.Where(x => ht.Contains(x.x)).ToList().ForEach(x => XYFit.Add(x.x, x.y));
                                GD.SlopeValues.Where(x => !ht.Contains(x.x)).ToList().ForEach(x => XYFit.Add(x.x, x.y));
                            }
                            else
                            {
                                GD.SlopeValues.Where(x => ht.Contains(x.x)).ToList().ForEach(x => XYFit.Add(Math.Log10(x.x), x.y));
                                GD.SlopeValues.Where(x => !ht.Contains(x.x)).ToList().ForEach(x => XYFit.Add(Math.Log10(x.x), x.y));
                            }
                                Graph.AddCurve(GroupName, XYFit, groupColor, SymbolType.Circle);
                            Graph.AddCurve(GroupName, ZWFit, groupColor, SymbolType.Star);
                        }
                    }
                }
                if(!LogScale)
                    Graph.XAxis.Title.Text = "OD Reading at Center Point";
                else
                    Graph.XAxis.Title.Text = "Log OD Reading at Center Point";
                
                if (chkTreatLegend.Checked)
                { Graph.Legend.IsVisible = true; }
                else { Graph.Legend.IsVisible = false; }
                Graph.XAxis.Scale.MaxGrace = .05;
                plotTreatments.AxisChange();
                plotTreatments.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        void UpdateTreatmentGraphWithResidualData(bool OnlyShowFitted = false,bool ShowLinearResiduals=false,bool showQuadResiduals=false)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GraphPane Graph = plotTreatments.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = "Growth Plots";
                Graph.XAxis.Title.Text = "Hours";
                Graph.YAxis.Title.Text = "Residuals";
                Graph.Legend.Position = LegendPos.InsideTopLeft;
                Graph.Legend.FontSpec.Size = 8f;
                Graph.Legend.IsHStack = true;
                SymbolType SymboltoUse = SymbolType.Circle;
                Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                {
                    Color groupColor;
                    var curNames = selectablePlateMap1.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                    string GroupName = "";
                    if (TreatmentTextBoxes[i] != null)
                    {
                        GroupName = TreatmentTextBoxes[i].Text;
                    }
                    if (GroupName == "")
                    {
                        GroupName = "Treatment: " + i.ToString();
                    }
                    foreach (string name in curNames)
                    {
                        if (curData.ContainsKey(name))
                        {
                            GrowthCurve GD = curData[name];
                            PointPairList XY;
                            List<double> Xs;
                            List<double> Ys;
                            if (ShowLinearResiduals)
                            {
                                Xs = new List<double>();
                                Ys = new List<double>();
                                if (GD.LinFit != null)
                                {
                                    Xs.AddRange(GD.LinFit.X);
                                    Ys.AddRange(GD.LinFit.ReturnResidualsAfterExpTransform());
                                }
                                
                            }
                            else if (showQuadResiduals)
                            {
                                Xs = new List<double>();
                                Ys = new List<double>();
                                if(GD.QuadModel!=null && GD.QuadModel.SuccessfulFit)
                                {
                                    Xs.AddRange(GD.QuadModel.X);
                                    Ys.AddRange(GD.QuadModel.ReturnResidualsAfterExpTransform);
                                }                           
                            }
                            else
                            {
                                GD.GetResiduals(OnlyShowFitted, out Xs, out Ys);
                                XY = new PointPairList(Xs.ToArray(), Ys.ToArray());
                                //GD.LinFit.GetResiduals(out Xs, out Ys);
                            }
                            XY = new PointPairList(Xs.ToArray(), Ys.ToArray());
                            Graph.AddCurve(GroupName, XY, groupColor, SymboltoUse);
                        }
                    }
                }
                if (chkTreatLegend.Checked)
                { Graph.Legend.IsVisible = true; }
                else { Graph.Legend.IsVisible = false; }
                Graph.XAxis.Scale.MaxGrace = .05;
                plotTreatments.AxisChange();
                plotTreatments.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        void UpdateTreatmentGraphWithGrowthData()
        {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    GraphPane Graph = plotTreatments.GraphPane;
                    Graph.CurveList.Clear();
                    Graph.Title.Text = "Growth Plots";
                    Graph.XAxis.Title.Text = "Hours";
                    if (chkTreatShowLog.Checked) { Graph.YAxis.Title.Text = "Log [OD600]"; }
                    else { Graph.YAxis.Title.Text = "OD600"; }
                    Graph.Legend.Position = LegendPos.InsideTopLeft;
                    Graph.Legend.FontSpec.Size = 8f;
                    Graph.Legend.IsHStack = true;
                    SymbolType SymboltoUse = SymbolType.Circle;
                    Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                    for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                    {
                        Color groupColor;
                        var curNames = selectablePlateMap1.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                        string GroupName="";
                        if (TreatmentTextBoxes[i] != null)
                        {
                            GroupName = TreatmentTextBoxes[i].Text;
                        }
                        if (GroupName == "")
                        {
                            GroupName = "Treatment: " + i.ToString();
                        }
                        foreach (string name in curNames)
                        {
                            if (curData.ContainsKey(name))
                            {
                                GrowthCurve GD = curData[name];
                                PointPairList XY;
                                if (chkTreatShowLog.Checked)
                                { XY = new PointPairList(GD.TimeValues_As_Double, GD.LogODValues); }
                                else
                                { XY = new PointPairList(GD.TimeValues_As_Double, GD.ODValues); }
                                Graph.AddCurve(GroupName, XY, groupColor, SymboltoUse);
                            }
                        }                       
                    }
                    if (chkTreatLegend.Checked)
                    { Graph.Legend.IsVisible = true; }
                    else { Graph.Legend.IsVisible = false; }
                    Graph.XAxis.Scale.MaxGrace = .05;
                    plotTreatments.AxisChange();
                    plotTreatments.Invalidate();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception thrown)
                { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        void UpdateTreatmentGraphWithTreatmentData(GetValueForTreatment FunctionForData,string Title)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GraphPane Graph = plotTreatments.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = Title;
                Graph.XAxis.Title.Text = "Treatments";
                Graph.YAxis.Title.Text = "";
                Graph.Legend.Position = LegendPos.InsideTopLeft;
                Graph.Legend.FontSpec.Size = 8f;
                Graph.Legend.IsHStack = true;
                SymbolType SymboltoUse = SymbolType.Circle;
               
                Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                {
                    Color groupColor;
                    List<double> xVals = new List<double>();
                    var curNames = selectablePlateMap1.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                    string GroupName = "";
                    if (TreatmentTextBoxes[i] != null)
                    {
                        GroupName = TreatmentTextBoxes[i].Text;
                    }
                    if (GroupName == "")
                    {
                        GroupName = "Treatment: " + i.ToString();
                    }
                    PointPairList XY=new PointPairList();
                    foreach (string name in curNames)
                    {
                        if (curData.ContainsKey(name))
                        {
                            GrowthCurve GD = curData[name];
                            XY.Add((double)i, SafeGet(FunctionForData,GD));
                        }
                    }
                    if (XY.Count > 0)
                    {
                        LineItem li=Graph.AddCurve(GroupName, XY, groupColor, SymboltoUse);
                        li.Line.IsVisible = false;
                        li.Symbol.Fill = new Fill(groupColor);                         
                    }                    
                }
                if (chkTreatLegend.Checked)
                { Graph.Legend.IsVisible = true; }
                else { Graph.Legend.IsVisible = false; }
                Graph.XAxis.Scale.MaxGrace = .05;
                plotTreatments.AxisChange();
                plotTreatments.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        
        void UpdateTreatmentGraphWithQQPlot(bool useLinear=false)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                List<double> residuals = new List<double>();
                
                Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                {
                    Color groupColor;
                    var curNames = selectablePlateMap1.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                    foreach (string name in curNames)
                    {
                        if (curData.ContainsKey(name))
                        {
                            GrowthCurve GD = curData[name];
                            if (!useLinear)
                            {
                                residuals.AddRange(GD.FittedResidualValues.Select(x => x.y));
                            }
                            else
                            {
                                residuals.AddRange(GD.LinFit.Residuals);
                            }
                        }
                    }
                }





                var res = residuals;
                res.Sort();
                var res2 = DoubleArray.From(res);
                res2 = res2 - res2.Mean();
                res2 = res2.Divide(res2.Std());
                double temp = 1.0 / Convert.ToDouble(res.Count);
                DoubleArray d = new DoubleArray(res.Count);
                d[0] = 1 - Math.Pow(0.5, temp);
                d[res.Count - 1] = Math.Pow(0.5, temp);
                int n = res.Count;
                for (int i = 0; i < (res.Count - 1); i++)
                {
                    d[i] = ((i + 1) - .3175) / (n + .365);
                }
                //DoubleArray ll = DoubleArray.From(d);
                ShoNS.MathFunc.ArrayMathInPlace.CdfNormInv(d);
                //now to graph
                GraphPane Graph = plotTreatments.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = "QQ Plot of Residuals";
                Graph.XAxis.Title.Text = "Theoretical Quantiles";
                Graph.YAxis.Title.Text = "Sample Quantiles";
                Graph.Legend.FontSpec.Size = 10f;
                Graph.Legend.IsHStack = true;
                SymbolType SymboltoUse = SymbolType.Circle;
                Graph.Legend.IsVisible = false;
                PointPairList XY = new PointPairList(d.ToArray(), res2.ToArray());
                var b = Graph.AddCurve("Points", XY, Color.Green, SymboltoUse);
                b.Line.IsVisible = false;
                double minm = d.Min();
                double maxm = d.Max();
                XY = new PointPairList(new double[] { minm, maxm }, new double[] { minm, maxm });
                Graph.AddCurve("1 to 1", XY, Color.Blue, SymbolType.None);

                Graph.XAxis.Scale.MaxGrace = .05;
                plotTreatments.AxisChange();
                plotTreatments.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }

        }

        void UpdateGroupGraphWithQQPlot()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
           var res= GroupFit.Residuals.ToList();
           res.Sort();
           var res2 = DoubleArray.From(res);
           res2 = res2 - res2.Mean();
           res2=res2.Divide(res2.Std());
           double temp=1.0/Convert.ToDouble(res.Count);
           DoubleArray d = new DoubleArray(res.Count);
           d[0] = 1 - Math.Pow(0.5, temp);
           d[res.Count - 1] = Math.Pow(0.5, temp);
            int n=res.Count;
            for(int i=0;i<(res.Count-1);i++)
            {
                d[i] = ((i + 1) - .3175) / (n + .365);
            }
            //DoubleArray ll = DoubleArray.From(d);
            ShoNS.MathFunc.ArrayMathInPlace.CdfNormInv(d);
            //now to graph
                GraphPane Graph = plotGroupModeling.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = "QQ Plot of Residuals";
                Graph.XAxis.Title.Text = "Theoretical Quantiles";
                Graph.YAxis.Title.Text = "Sample Quantiles"; 
                Graph.Legend.FontSpec.Size = 10f;
                Graph.Legend.IsHStack = true;
                SymbolType SymboltoUse = SymbolType.Circle;
                Graph.Legend.IsVisible=false;
                PointPairList XY=new PointPairList(d.ToArray(),res2.ToArray());
                var b=Graph.AddCurve("Points",XY,Color.Green,SymboltoUse);
                b.Line.IsVisible=false;
                double minm=d.Min();
                double maxm=d.Max();
                XY=new PointPairList(new double[]{minm,maxm},new double[]{minm,maxm});
                Graph.AddCurve("1 to 1", XY, Color.Blue, SymbolType.None);
               
                Graph.XAxis.Scale.MaxGrace = .05;
                plotGroupModeling.AxisChange();
                plotGroupModeling.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
           
        }
        private void RemakeTreatmentGraphGroupedData()
        {
            if (GroupFit != null && GroupFit.SuccessfulFit)
            {
                plotGroupModeling.GraphPane.CurveList.Clear();
                if (rbtnGMTimeVOD.Checked)
                {UpdateGroupedGraphWithGrowthData();}
                else if (rbtnGMResiduals.Checked)
                { UpdateGroupedGraphWithResidualData(false); }
                else if (rbtnGMResidualQQPlot.Checked)
                { UpdateGroupGraphWithQQPlot(); }
                else if (rbtnGMTimevOffSet.Checked)
                {
                    UpdateGroupedGraphWithTimevOffset();
                }
                else
                {
                    GetValueForTreatment valueGetter;
                    string Title = "";
                    if (rbtnGMDoublings.Checked)
                    {
                        valueGetter = (GrowthCurve x) => Math.Log(2) / GroupFit.WellParameters[x.DataSetName].GrowthRate;
                        Title = "Doubling Time";
                    }
                    else
                    {
                        valueGetter = (GrowthCurve x) => GroupFit.RMSE - x.ExpFit.RMSE;
                    }
                    UpdateGroupedGraphWithTreatmentData(valueGetter, Title);
                }
            }
        }
        void UpdateGroupedGraphWithGrowthData()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GraphPane Graph = plotGroupModeling.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = "Growth Plots";
                Graph.XAxis.Title.Text = "Hours";
                if (chkTreatShowLog.Checked) { Graph.YAxis.Title.Text = "Log [OD600]"; }
                else { Graph.YAxis.Title.Text = "OD600"; }
                Graph.Legend.Position = LegendPos.InsideTopLeft;
                Graph.Legend.FontSpec.Size = 8f;
                Graph.Legend.IsHStack = true;
                SymbolType SymboltoUse = SymbolType.Circle;
                Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                {
                    Color groupColor;
                    var curNames = selectPlatesForGroupModel.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                    string GroupName = "";
                    if (TreatmentTextBoxes[i] != null)
                    {
                        GroupName = TreatmentTextBoxes[i].Text;
                    }
                    if (GroupName == "")
                    {
                        GroupName = "Treatment: " + i.ToString();
                    }
                    foreach (string name in curNames)
                    {
                        if (curData.ContainsKey(name))
                        {
                            GrowthCurve GD = curData[name];
                            PointPairList XY;
                            if (chkTreatShowLog.Checked)
                            { XY = new PointPairList(GD.TimeValues_As_Double, GD.LogODValues); }
                            else
                            { XY = new PointPairList(GD.TimeValues_As_Double, GD.ODValues); }
                            Graph.AddCurve(GroupName, XY, groupColor, SymboltoUse);
                        }
                    }
                }
                if (chkTreatLegend.Checked)
                { Graph.Legend.IsVisible = true; }
                else { Graph.Legend.IsVisible = false; }
                Graph.XAxis.Scale.MaxGrace = .05;
                plotGroupModeling.AxisChange();
                plotGroupModeling.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        void UpdateGroupedGraphWithResidualData(bool ShowByCurve=true)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GraphPane Graph = plotGroupModeling.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = "Growth Plots";
                Graph.XAxis.Title.Text = "Hours";
                Graph.YAxis.Title.Text = "Residuals";
                Graph.Legend.Position = LegendPos.InsideTopLeft;
                Graph.Legend.FontSpec.Size = 8f;
                Graph.Legend.IsHStack = true;
                SymbolType SymboltoUse = SymbolType.Circle;
                Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                {
                    Color groupColor;
                    var curNames = selectPlatesForGroupModel.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                    string GroupName = "";
                   
                    foreach (string name in curNames)
                    {
                        LightWeightGC gc;
                        if (GroupFit.NamesToCurves.TryGetValue(name, out gc) && curData.ContainsKey(name))
                        {
                            GrowthCurve GD = curData[name];
                            PointPairList XY;
                            XY = new PointPairList(gc.Xvalues,GroupFit.CalculateResiduals(gc).ToArray());
                            Graph.AddCurve(GroupName, XY, groupColor, SymboltoUse);
                        }
                    }
                }
                if (chkTreatLegend.Checked)
                { Graph.Legend.IsVisible = true; }
                else { Graph.Legend.IsVisible = false; }
                Graph.XAxis.Scale.MaxGrace = .05;
                plotGroupModeling.AxisChange();
                plotGroupModeling.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        void UpdateGroupedGraphWithTimevOffset()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GraphPane Graph = plotGroupModeling.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = "Growth Plots";
                Graph.XAxis.Title.Text = "Hours";
                Graph.YAxis.Title.Text = "OffSet";
                Graph.Legend.Position = LegendPos.InsideTopLeft;
                Graph.Legend.FontSpec.Size = 8f;
                Graph.Legend.IsHStack = true;
               
                            PointPairList XY;
                            XY = new PointPairList(GroupFit.TimeValues,GroupFit.OffSetsAtTimes);
                            Graph.AddCurve("OffSets", XY,Color.Blue);
                        
                Graph.XAxis.Scale.MaxGrace = .05;
                plotGroupModeling.AxisChange();
                plotGroupModeling.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        void UpdateGroupedGraphWithTreatmentData(GetValueForTreatment FunctionForData, string Title)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GraphPane Graph = plotGroupModeling.GraphPane;
                Graph.CurveList.Clear();
                Graph.Title.Text = Title;
                Graph.XAxis.Title.Text = "Treatments";
                Graph.YAxis.Title.Text = "";
                Graph.Legend.Position = LegendPos.InsideTopLeft;
                Graph.Legend.FontSpec.Size = 8f;
                Graph.Legend.IsHStack = true;
                SymbolType SymboltoUse = SymbolType.Circle;

                Dictionary<string, GrowthCurve> curData = GetDictionaryOfGrowthRateData();
                for (int i = 1; i < SelectablePlateMap.MAX_GROUP_ASSIGNMENTS; i++)
                {
                    Color groupColor;
                    List<double> xVals = new List<double>();
                    var curNames = selectablePlateMap1.GetNamesOfWellsAssignedToGroup(i, out groupColor);
                    string GroupName = "";
                    if (TreatmentTextBoxes[i] != null)
                    {
                        GroupName = TreatmentTextBoxes[i].Text;
                    }
                    if (GroupName == "")
                    {
                        GroupName = "Treatment: " + i.ToString();
                    }
                    PointPairList XY = new PointPairList();
                    foreach (string name in curNames)
                    {
                        if (curData.ContainsKey(name))
                        {
                            GrowthCurve GD = curData[name];
                            XY.Add((double)i, SafeGet(FunctionForData, GD));
                        }
                    }
                    if (XY.Count > 0)
                    {
                        LineItem li = Graph.AddCurve(GroupName, XY, groupColor, SymboltoUse);
                        li.Line.IsVisible = false;
                        li.Symbol.Fill = new Fill(groupColor);
                    }
                }
                if (chkTreatLegend.Checked)
                { Graph.Legend.IsVisible = true; }
                else { Graph.Legend.IsVisible = false; }
                Graph.XAxis.Scale.MaxGrace = .05;
                plotGroupModeling.AxisChange();
                plotGroupModeling.Invalidate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception thrown)
            { MessageBox.Show("Could not make graph, talk to nigel.\n\nError is:\n" + thrown.Message, "Graph Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }

        
    }

}