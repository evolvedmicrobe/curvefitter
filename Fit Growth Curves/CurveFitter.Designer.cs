namespace Fit_Growth_Curves
{
    partial class CurveFitter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurveFitter));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryWithExcelDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPlateKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.open16MinuteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alternativeExportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportRawDataForMatlabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabSensitivityAnalysis = new System.Windows.Forms.TabPage();
            this.rbtnSensBiasLinear = new System.Windows.Forms.RadioButton();
            this.lblYAxis = new System.Windows.Forms.Label();
            this.rbtnSensBiasError = new System.Windows.Forms.RadioButton();
            this.rbtnSensStartEndLog = new System.Windows.Forms.RadioButton();
            this.rbtnSensRange = new System.Windows.Forms.RadioButton();
            this.lblXAxis = new System.Windows.Forms.Label();
            this.lstGrowthRatesSensitivity = new System.Windows.Forms.ListBox();
            this.tabPlotGraphic = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.rbtnGroupsQuadraticResiduals = new System.Windows.Forms.RadioButton();
            this.rbtnGroupQuadraticDoubling = new System.Windows.Forms.RadioButton();
            this.rbtnGroupsSlopeDoublingsLogOD = new System.Windows.Forms.RadioButton();
            this.rbtnGroupDifFromCenter = new System.Windows.Forms.RadioButton();
            this.rbtnGroupsDeviance = new System.Windows.Forms.RadioButton();
            this.rbtnGroupsQQLinear = new System.Windows.Forms.RadioButton();
            this.rbtnGroupsOffSetGrowthRate = new System.Windows.Forms.RadioButton();
            this.rbtnGroupsLinFit = new System.Windows.Forms.RadioButton();
            this.rbtnRobustGRvEXPGR = new System.Windows.Forms.RadioButton();
            this.rbtnMixtureModelGR = new System.Windows.Forms.RadioButton();
            this.rbtnEndODvMax = new System.Windows.Forms.RadioButton();
            this.rbtnMaxvGrowthRate = new System.Windows.Forms.RadioButton();
            this.rbtnInitialPopvGrowthRate = new System.Windows.Forms.RadioButton();
            this.rbtnMakeQQPlot = new System.Windows.Forms.RadioButton();
            this.rbtnGroupOffSetMinusStart = new System.Windows.Forms.RadioButton();
            this.btnMakeEvoGroups = new System.Windows.Forms.Button();
            this.btnClearTreatments = new System.Windows.Forms.Button();
            this.rbtnPlotLinearResiduals = new System.Windows.Forms.RadioButton();
            this.rbtnMaxMinusEnd = new System.Windows.Forms.RadioButton();
            this.rbtnFittedResiduals = new System.Windows.Forms.RadioButton();
            this.rbtnPlotAllResiduals = new System.Windows.Forms.RadioButton();
            this.btnRowGroups = new System.Windows.Forms.Button();
            this.rbtnPlotSlope = new System.Windows.Forms.RadioButton();
            this.rbtnPlotLagTime = new System.Windows.Forms.RadioButton();
            this.rbtnPlotLastOD = new System.Windows.Forms.RadioButton();
            this.rbtnTimeToOD = new System.Windows.Forms.RadioButton();
            this.chkTreatShowLog = new System.Windows.Forms.CheckBox();
            this.chkTreatLegend = new System.Windows.Forms.CheckBox();
            this.rbtnTreatNumPoints = new System.Windows.Forms.RadioButton();
            this.rbtnTreatTimevOD = new System.Windows.Forms.RadioButton();
            this.rbtnTreatDoublingTime = new System.Windows.Forms.RadioButton();
            this.rbtnTreatInitialOD = new System.Windows.Forms.RadioButton();
            this.label21 = new System.Windows.Forms.Label();
            this.rbtnTreatRSq = new System.Windows.Forms.RadioButton();
            this.rbtnTreatGrowthRate = new System.Windows.Forms.RadioButton();
            this.rbtnTreatMaxOd = new System.Windows.Forms.RadioButton();
            this.label20 = new System.Windows.Forms.Label();
            this.txtTreatment6 = new System.Windows.Forms.TextBox();
            this.txtTreatment5 = new System.Windows.Forms.TextBox();
            this.txtTreatment4 = new System.Windows.Forms.TextBox();
            this.txtTreatment3 = new System.Windows.Forms.TextBox();
            this.txtTreatment2 = new System.Windows.Forms.TextBox();
            this.txtTreatment1 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lstTreatmentSelection = new System.Windows.Forms.ListBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.plotTreatments = new ZedGraph.ZedGraphControl();
            this.tabBlankRemoval = new System.Windows.Forms.TabPage();
            this.btnSubtractWrittenBlank = new System.Windows.Forms.Button();
            this.txtBlankValue = new System.Windows.Forms.TextBox();
            this.btnDeleteLikelyBlanks = new System.Windows.Forms.Button();
            this.lblDeletePlate = new System.Windows.Forms.Label();
            this.btnDeleteAvg3asBlank = new System.Windows.Forms.Button();
            this.btnDelete2ndPoint = new System.Windows.Forms.Button();
            this.btnDeleteFirstBlank = new System.Windows.Forms.Button();
            this.tabRobo = new System.Windows.Forms.TabPage();
            this.rbtnPlatesLogisticR2 = new System.Windows.Forms.RadioButton();
            this.rbtnPlateQuadR2 = new System.Windows.Forms.RadioButton();
            this.rbtnPlotQuadraticRate = new System.Windows.Forms.RadioButton();
            this.rbtnPlatePlotLogistic15 = new System.Windows.Forms.RadioButton();
            this.rbtnOutlierNumber = new System.Windows.Forms.RadioButton();
            this.rbtnMixtureGrowthRate = new System.Windows.Forms.RadioButton();
            this.rbtnGroupGrowthRate = new System.Windows.Forms.RadioButton();
            this.rbtnPlateOffSetIDMinusStart = new System.Windows.Forms.RadioButton();
            this.rbtnOffSetGR = new System.Windows.Forms.RadioButton();
            this.rbtnOffSet = new System.Windows.Forms.RadioButton();
            this.rbtnIntercept = new System.Windows.Forms.RadioButton();
            this.rbtnRMSE = new System.Windows.Forms.RadioButton();
            this.lblRobo = new System.Windows.Forms.Label();
            this.graphHistogram = new ZedGraph.ZedGraphControl();
            this.label12 = new System.Windows.Forms.Label();
            this.lstTimePoints = new System.Windows.Forms.ListBox();
            this.rbtnTimePoint = new System.Windows.Forms.RadioButton();
            this.rbtnDoubleTime = new System.Windows.Forms.RadioButton();
            this.rbtnIOD = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.rbtnRS = new System.Windows.Forms.RadioButton();
            this.rbtnGrowthRate = new System.Windows.Forms.RadioButton();
            this.rbtnMaxOD = new System.Windows.Forms.RadioButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tblRawData = new System.Windows.Forms.DataGridView();
            this.tabPageChangeFitted = new System.Windows.Forms.TabPage();
            this.chkShowFittedPick = new System.Windows.Forms.CheckBox();
            this.ChartPickData = new ZedGraph.ZedGraphControl();
            this.label4 = new System.Windows.Forms.Label();
            this.lstGrowthCurvesMirror = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lstDataMirror = new System.Windows.Forms.ListBox();
            this.tabMainTab = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ChartSlopeN = new ZedGraph.ZedGraphControl();
            this.ChartStandard = new ZedGraph.ZedGraphControl();
            this.topRightPanel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ChartN = new ZedGraph.ZedGraphControl();
            this.label7 = new System.Windows.Forms.Label();
            this.l = new System.Windows.Forms.Panel();
            this.chkShowRobustFit = new System.Windows.Forms.CheckBox();
            this.btnCallOutliers = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEndPoint = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSetBounds = new System.Windows.Forms.Button();
            this.txtStartPoint = new System.Windows.Forms.TextBox();
            this.lstData = new System.Windows.Forms.ListBox();
            this.chkShowLin = new System.Windows.Forms.CheckBox();
            this.btnDeleteCurve = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.leftsubbottompanel = new System.Windows.Forms.Panel();
            this.chkODMustIncrease = new System.Windows.Forms.CheckBox();
            this.chkUsePercent = new System.Windows.Forms.CheckBox();
            this.txtMaxODPercent = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnFitODRange = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMaxOD = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMinOD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangeAxis = new System.Windows.Forms.Button();
            this.txtMaxRange = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinRange = new System.Windows.Forms.TextBox();
            this.lstGrowthCurves = new System.Windows.Forms.ListBox();
            this.tabGroup = new System.Windows.Forms.TabPage();
            this.rbtnGMRMSEDif = new System.Windows.Forms.RadioButton();
            this.btnFitGroup = new System.Windows.Forms.Button();
            this.btnCopyGroupAssignments = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.rbtnGMResidualQQPlot = new System.Windows.Forms.RadioButton();
            this.rbtnGMTimevOffSet = new System.Windows.Forms.RadioButton();
            this.rbtnGMTimeVOD = new System.Windows.Forms.RadioButton();
            this.rbtnGMDoublings = new System.Windows.Forms.RadioButton();
            this.rbtnGMResiduals = new System.Windows.Forms.RadioButton();
            this.lblBlah = new System.Windows.Forms.Label();
            this.plotGroupModeling = new ZedGraph.ZedGraphControl();
            this.lstGroupModelGroups = new System.Windows.Forms.ListBox();
            this.openFileWithNumberedHoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plateMap = new MatrixArrayPlot.PlateHeatMap();
            this.toDeletePlateMap = new Fit_Growth_Curves.SelectablePlateMap();
            this.selectablePlateMap1 = new Fit_Growth_Curves.SelectablePlateMap();
            this.scaleBarSensitivity = new MatrixArrayPlot.ScaleBar();
            this.sensitivityArray = new MatrixArrayPlot.ArrayPlot();
            this.selectPlatesForGroupModel = new Fit_Growth_Curves.SelectablePlateMap();
            this.label22 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabSensitivityAnalysis.SuspendLayout();
            this.tabPlotGraphic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabBlankRemoval.SuspendLayout();
            this.tabRobo.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblRawData)).BeginInit();
            this.tabPageChangeFitted.SuspendLayout();
            this.tabMainTab.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.topRightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.l.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.leftsubbottompanel.SuspendLayout();
            this.tabGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.alternativeExportsToolStripMenuItem,
            this.launchToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1173, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOpenFile,
            this.openFileWithNumberedHoursToolStripMenuItem,
            this.exportDataToolStripMenuItem1,
            this.importPreviousToolStripMenuItem,
            this.openDirectoryWithExcelDataToolStripMenuItem,
            this.importPlateKeyToolStripMenuItem,
            this.open16MinuteFileToolStripMenuItem});
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.openFileToolStripMenuItem.Text = "File";
            // 
            // MenuOpenFile
            // 
            this.MenuOpenFile.Name = "MenuOpenFile";
            this.MenuOpenFile.Size = new System.Drawing.Size(245, 22);
            this.MenuOpenFile.Text = "Open File with Date/Times";
            this.MenuOpenFile.Click += new System.EventHandler(this.MenuOpenFile_Click);
            // 
            // exportDataToolStripMenuItem1
            // 
            this.exportDataToolStripMenuItem1.Name = "exportDataToolStripMenuItem1";
            this.exportDataToolStripMenuItem1.Size = new System.Drawing.Size(245, 22);
            this.exportDataToolStripMenuItem1.Text = "Export Data";
            this.exportDataToolStripMenuItem1.Click += new System.EventHandler(this.exportDataToolStripMenuItem1_Click);
            // 
            // importPreviousToolStripMenuItem
            // 
            this.importPreviousToolStripMenuItem.Name = "importPreviousToolStripMenuItem";
            this.importPreviousToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.importPreviousToolStripMenuItem.Text = "Import Previous";
            this.importPreviousToolStripMenuItem.Click += new System.EventHandler(this.importPreviousToolStripMenuItem_Click);
            // 
            // openDirectoryWithExcelDataToolStripMenuItem
            // 
            this.openDirectoryWithExcelDataToolStripMenuItem.Name = "openDirectoryWithExcelDataToolStripMenuItem";
            this.openDirectoryWithExcelDataToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.openDirectoryWithExcelDataToolStripMenuItem.Text = "Open Directory with Excel Data";
            this.openDirectoryWithExcelDataToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryWithExcelDataToolStripMenuItem_Click);
            // 
            // importPlateKeyToolStripMenuItem
            // 
            this.importPlateKeyToolStripMenuItem.Name = "importPlateKeyToolStripMenuItem";
            this.importPlateKeyToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.importPlateKeyToolStripMenuItem.Text = "Import Plate Key";
            this.importPlateKeyToolStripMenuItem.Click += new System.EventHandler(this.importPlateKeyToolStripMenuItem_Click);
            // 
            // open16MinuteFileToolStripMenuItem
            // 
            this.open16MinuteFileToolStripMenuItem.Name = "open16MinuteFileToolStripMenuItem";
            this.open16MinuteFileToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.open16MinuteFileToolStripMenuItem.Text = "Open 16 Minute File";
            this.open16MinuteFileToolStripMenuItem.Click += new System.EventHandler(this.open16MinuteFileToolStripMenuItem_Click);
            // 
            // alternativeExportsToolStripMenuItem
            // 
            this.alternativeExportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportRawDataForMatlabToolStripMenuItem});
            this.alternativeExportsToolStripMenuItem.Name = "alternativeExportsToolStripMenuItem";
            this.alternativeExportsToolStripMenuItem.Size = new System.Drawing.Size(117, 20);
            this.alternativeExportsToolStripMenuItem.Text = "Alternative Exports";
            // 
            // exportRawDataForMatlabToolStripMenuItem
            // 
            this.exportRawDataForMatlabToolStripMenuItem.Name = "exportRawDataForMatlabToolStripMenuItem";
            this.exportRawDataForMatlabToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.exportRawDataForMatlabToolStripMenuItem.Text = "Export Raw Data For Matlab";
            this.exportRawDataForMatlabToolStripMenuItem.Click += new System.EventHandler(this.exportRawDataForMatlabToolStripMenuItem_Click);
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            this.launchToolStripMenuItem.Size = new System.Drawing.Size(127, 20);
            this.launchToolStripMenuItem.Text = "Launch Sho Console";
            this.launchToolStripMenuItem.Click += new System.EventHandler(this.launchToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "\".txt\",\".csv\",\".gcd\"";
            // 
            // tabSensitivityAnalysis
            // 
            this.tabSensitivityAnalysis.Controls.Add(this.rbtnSensBiasLinear);
            this.tabSensitivityAnalysis.Controls.Add(this.lblYAxis);
            this.tabSensitivityAnalysis.Controls.Add(this.rbtnSensBiasError);
            this.tabSensitivityAnalysis.Controls.Add(this.rbtnSensStartEndLog);
            this.tabSensitivityAnalysis.Controls.Add(this.rbtnSensRange);
            this.tabSensitivityAnalysis.Controls.Add(this.lblXAxis);
            this.tabSensitivityAnalysis.Controls.Add(this.scaleBarSensitivity);
            this.tabSensitivityAnalysis.Controls.Add(this.sensitivityArray);
            this.tabSensitivityAnalysis.Controls.Add(this.lstGrowthRatesSensitivity);
            this.tabSensitivityAnalysis.Location = new System.Drawing.Point(4, 22);
            this.tabSensitivityAnalysis.Name = "tabSensitivityAnalysis";
            this.tabSensitivityAnalysis.Size = new System.Drawing.Size(1165, 695);
            this.tabSensitivityAnalysis.TabIndex = 7;
            this.tabSensitivityAnalysis.Text = "Sensitivity Analysis";
            this.tabSensitivityAnalysis.UseVisualStyleBackColor = true;
            // 
            // rbtnSensBiasLinear
            // 
            this.rbtnSensBiasLinear.AutoSize = true;
            this.rbtnSensBiasLinear.Location = new System.Drawing.Point(5, 553);
            this.rbtnSensBiasLinear.Name = "rbtnSensBiasLinear";
            this.rbtnSensBiasLinear.Size = new System.Drawing.Size(116, 17);
            this.rbtnSensBiasLinear.TabIndex = 8;
            this.rbtnSensBiasLinear.Text = "Add Bias, Linear Fit";
            this.rbtnSensBiasLinear.UseVisualStyleBackColor = true;
            this.rbtnSensBiasLinear.CheckedChanged += new System.EventHandler(this.SensitivityCriteriaChanged);
            // 
            // lblYAxis
            // 
            this.lblYAxis.AutoSize = true;
            this.lblYAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYAxis.Location = new System.Drawing.Point(550, 14);
            this.lblYAxis.Name = "lblYAxis";
            this.lblYAxis.Size = new System.Drawing.Size(122, 13);
            this.lblYAxis.TabIndex = 6;
            this.lblYAxis.Text = "End of OD Range To Fit";
            // 
            // rbtnSensBiasError
            // 
            this.rbtnSensBiasError.AutoSize = true;
            this.rbtnSensBiasError.Location = new System.Drawing.Point(5, 504);
            this.rbtnSensBiasError.Name = "rbtnSensBiasError";
            this.rbtnSensBiasError.Size = new System.Drawing.Size(174, 43);
            this.rbtnSensBiasError.TabIndex = 5;
            this.rbtnSensBiasError.Text = "Add a 0.001 bias and  plot \r\ndifferences from original number\r\n(Exponential Fit)";
            this.rbtnSensBiasError.UseVisualStyleBackColor = true;
            this.rbtnSensBiasError.CheckedChanged += new System.EventHandler(this.SensitivityCriteriaChanged);
            // 
            // rbtnSensStartEndLog
            // 
            this.rbtnSensStartEndLog.AutoSize = true;
            this.rbtnSensStartEndLog.Location = new System.Drawing.Point(6, 481);
            this.rbtnSensStartEndLog.Name = "rbtnSensStartEndLog";
            this.rbtnSensStartEndLog.Size = new System.Drawing.Size(146, 17);
            this.rbtnSensStartEndLog.TabIndex = 4;
            this.rbtnSensStartEndLog.Text = "Start/End Range, Lin. Fit.";
            this.rbtnSensStartEndLog.UseVisualStyleBackColor = true;
            this.rbtnSensStartEndLog.CheckedChanged += new System.EventHandler(this.SensitivityCriteriaChanged);
            // 
            // rbtnSensRange
            // 
            this.rbtnSensRange.AutoSize = true;
            this.rbtnSensRange.Checked = true;
            this.rbtnSensRange.Location = new System.Drawing.Point(5, 448);
            this.rbtnSensRange.Name = "rbtnSensRange";
            this.rbtnSensRange.Size = new System.Drawing.Size(147, 17);
            this.rbtnSensRange.TabIndex = 3;
            this.rbtnSensRange.TabStop = true;
            this.rbtnSensRange.Text = "Start/End Range, Exp. Fit";
            this.rbtnSensRange.UseVisualStyleBackColor = true;
            this.rbtnSensRange.CheckedChanged += new System.EventHandler(this.SensitivityCriteriaChanged);
            // 
            // lblXAxis
            // 
            this.lblXAxis.AutoSize = true;
            this.lblXAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXAxis.Location = new System.Drawing.Point(46, 618);
            this.lblXAxis.Name = "lblXAxis";
            this.lblXAxis.Size = new System.Drawing.Size(121, 13);
            this.lblXAxis.TabIndex = 2;
            this.lblXAxis.Text = "Start of OD Range to Fit";
            // 
            // lstGrowthRatesSensitivity
            // 
            this.lstGrowthRatesSensitivity.FormattingEnabled = true;
            this.lstGrowthRatesSensitivity.Location = new System.Drawing.Point(9, 22);
            this.lstGrowthRatesSensitivity.Name = "lstGrowthRatesSensitivity";
            this.lstGrowthRatesSensitivity.Size = new System.Drawing.Size(158, 420);
            this.lstGrowthRatesSensitivity.TabIndex = 0;
            this.lstGrowthRatesSensitivity.SelectedIndexChanged += new System.EventHandler(this.lstGrowthRatesSensitivity_SelectedIndexChanged);
            // 
            // tabPlotGraphic
            // 
            this.tabPlotGraphic.Controls.Add(this.splitContainer3);
            this.tabPlotGraphic.Location = new System.Drawing.Point(4, 22);
            this.tabPlotGraphic.Name = "tabPlotGraphic";
            this.tabPlotGraphic.Size = new System.Drawing.Size(1165, 695);
            this.tabPlotGraphic.TabIndex = 6;
            this.tabPlotGraphic.Text = "Plot Different Groups";
            this.tabPlotGraphic.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.rbtnGroupsQuadraticResiduals);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnGroupQuadraticDoubling);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnGroupsSlopeDoublingsLogOD);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnGroupDifFromCenter);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnGroupsDeviance);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnGroupsQQLinear);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnGroupsOffSetGrowthRate);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnGroupsLinFit);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnRobustGRvEXPGR);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnMixtureModelGR);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnEndODvMax);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnMaxvGrowthRate);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnInitialPopvGrowthRate);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnMakeQQPlot);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnGroupOffSetMinusStart);
            this.splitContainer3.Panel1.Controls.Add(this.btnMakeEvoGroups);
            this.splitContainer3.Panel1.Controls.Add(this.btnClearTreatments);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnPlotLinearResiduals);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnMaxMinusEnd);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnFittedResiduals);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnPlotAllResiduals);
            this.splitContainer3.Panel1.Controls.Add(this.btnRowGroups);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnPlotSlope);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnPlotLagTime);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnPlotLastOD);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnTimeToOD);
            this.splitContainer3.Panel1.Controls.Add(this.chkTreatShowLog);
            this.splitContainer3.Panel1.Controls.Add(this.chkTreatLegend);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnTreatNumPoints);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnTreatTimevOD);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnTreatDoublingTime);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnTreatInitialOD);
            this.splitContainer3.Panel1.Controls.Add(this.label21);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnTreatRSq);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnTreatGrowthRate);
            this.splitContainer3.Panel1.Controls.Add(this.rbtnTreatMaxOd);
            this.splitContainer3.Panel1.Controls.Add(this.label20);
            this.splitContainer3.Panel1.Controls.Add(this.txtTreatment6);
            this.splitContainer3.Panel1.Controls.Add(this.txtTreatment5);
            this.splitContainer3.Panel1.Controls.Add(this.txtTreatment4);
            this.splitContainer3.Panel1.Controls.Add(this.txtTreatment3);
            this.splitContainer3.Panel1.Controls.Add(this.txtTreatment2);
            this.splitContainer3.Panel1.Controls.Add(this.txtTreatment1);
            this.splitContainer3.Panel1.Controls.Add(this.label19);
            this.splitContainer3.Panel1.Controls.Add(this.lstTreatmentSelection);
            this.splitContainer3.Panel1.Controls.Add(this.label18);
            this.splitContainer3.Panel1.Controls.Add(this.label17);
            this.splitContainer3.Panel1.Controls.Add(this.label16);
            this.splitContainer3.Panel1.Controls.Add(this.label15);
            this.splitContainer3.Panel1.Controls.Add(this.label14);
            this.splitContainer3.Panel1.Controls.Add(this.selectablePlateMap1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.plotTreatments);
            this.splitContainer3.Size = new System.Drawing.Size(1165, 695);
            this.splitContainer3.SplitterDistance = 388;
            this.splitContainer3.TabIndex = 0;
            // 
            // rbtnGroupsQuadraticResiduals
            // 
            this.rbtnGroupsQuadraticResiduals.AutoSize = true;
            this.rbtnGroupsQuadraticResiduals.Location = new System.Drawing.Point(232, 618);
            this.rbtnGroupsQuadraticResiduals.Name = "rbtnGroupsQuadraticResiduals";
            this.rbtnGroupsQuadraticResiduals.Size = new System.Drawing.Size(130, 17);
            this.rbtnGroupsQuadraticResiduals.TabIndex = 127;
            this.rbtnGroupsQuadraticResiduals.Text = "Show Quad Residuals";
            this.rbtnGroupsQuadraticResiduals.UseVisualStyleBackColor = true;
            this.rbtnGroupsQuadraticResiduals.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnGroupQuadraticDoubling
            // 
            this.rbtnGroupQuadraticDoubling.AutoSize = true;
            this.rbtnGroupQuadraticDoubling.Location = new System.Drawing.Point(232, 595);
            this.rbtnGroupQuadraticDoubling.Name = "rbtnGroupQuadraticDoubling";
            this.rbtnGroupQuadraticDoubling.Size = new System.Drawing.Size(152, 17);
            this.rbtnGroupQuadraticDoubling.TabIndex = 126;
            this.rbtnGroupQuadraticDoubling.Text = "Quadratic Doubling at 0.12";
            this.rbtnGroupQuadraticDoubling.UseVisualStyleBackColor = true;
            this.rbtnGroupQuadraticDoubling.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnGroupsSlopeDoublingsLogOD
            // 
            this.rbtnGroupsSlopeDoublingsLogOD.AutoSize = true;
            this.rbtnGroupsSlopeDoublingsLogOD.Location = new System.Drawing.Point(243, 545);
            this.rbtnGroupsSlopeDoublingsLogOD.Name = "rbtnGroupsSlopeDoublingsLogOD";
            this.rbtnGroupsSlopeDoublingsLogOD.Size = new System.Drawing.Size(118, 17);
            this.rbtnGroupsSlopeDoublingsLogOD.TabIndex = 125;
            this.rbtnGroupsSlopeDoublingsLogOD.Text = "Doublings -Log Axis";
            this.rbtnGroupsSlopeDoublingsLogOD.UseVisualStyleBackColor = true;
            this.rbtnGroupsSlopeDoublingsLogOD.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnGroupDifFromCenter
            // 
            this.rbtnGroupDifFromCenter.AutoSize = true;
            this.rbtnGroupDifFromCenter.Location = new System.Drawing.Point(243, 523);
            this.rbtnGroupDifFromCenter.Name = "rbtnGroupDifFromCenter";
            this.rbtnGroupDifFromCenter.Size = new System.Drawing.Size(116, 17);
            this.rbtnGroupDifFromCenter.TabIndex = 124;
            this.rbtnGroupDifFromCenter.Text = "Difference From C4";
            this.rbtnGroupDifFromCenter.UseVisualStyleBackColor = true;
            this.rbtnGroupDifFromCenter.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnGroupsDeviance
            // 
            this.rbtnGroupsDeviance.AutoSize = true;
            this.rbtnGroupsDeviance.Location = new System.Drawing.Point(232, 384);
            this.rbtnGroupsDeviance.Name = "rbtnGroupsDeviance";
            this.rbtnGroupsDeviance.Size = new System.Drawing.Size(115, 17);
            this.rbtnGroupsDeviance.TabIndex = 123;
            this.rbtnGroupsDeviance.Text = "Grouped Deviance";
            this.rbtnGroupsDeviance.UseVisualStyleBackColor = true;
            this.rbtnGroupsDeviance.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnGroupsQQLinear
            // 
            this.rbtnGroupsQQLinear.AutoSize = true;
            this.rbtnGroupsQQLinear.Location = new System.Drawing.Point(283, 645);
            this.rbtnGroupsQQLinear.Name = "rbtnGroupsQQLinear";
            this.rbtnGroupsQQLinear.Size = new System.Drawing.Size(93, 17);
            this.rbtnGroupsQQLinear.TabIndex = 122;
            this.rbtnGroupsQQLinear.Text = "QQ Plot Lin Fit";
            this.rbtnGroupsQQLinear.UseVisualStyleBackColor = true;
            this.rbtnGroupsQQLinear.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnGroupsOffSetGrowthRate
            // 
            this.rbtnGroupsOffSetGrowthRate.AutoSize = true;
            this.rbtnGroupsOffSetGrowthRate.Location = new System.Drawing.Point(232, 339);
            this.rbtnGroupsOffSetGrowthRate.Name = "rbtnGroupsOffSetGrowthRate";
            this.rbtnGroupsOffSetGrowthRate.Size = new System.Drawing.Size(72, 17);
            this.rbtnGroupsOffSetGrowthRate.TabIndex = 121;
            this.rbtnGroupsOffSetGrowthRate.Text = "Offset GR";
            this.rbtnGroupsOffSetGrowthRate.UseVisualStyleBackColor = true;
            this.rbtnGroupsOffSetGrowthRate.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnGroupsLinFit
            // 
            this.rbtnGroupsLinFit.AutoSize = true;
            this.rbtnGroupsLinFit.Location = new System.Drawing.Point(232, 293);
            this.rbtnGroupsLinFit.Name = "rbtnGroupsLinFit";
            this.rbtnGroupsLinFit.Size = new System.Drawing.Size(72, 17);
            this.rbtnGroupsLinFit.TabIndex = 120;
            this.rbtnGroupsLinFit.Text = "Lin Fit GR";
            this.rbtnGroupsLinFit.UseVisualStyleBackColor = true;
            this.rbtnGroupsLinFit.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnRobustGRvEXPGR
            // 
            this.rbtnRobustGRvEXPGR.AutoSize = true;
            this.rbtnRobustGRvEXPGR.Location = new System.Drawing.Point(232, 360);
            this.rbtnRobustGRvEXPGR.Name = "rbtnRobustGRvEXPGR";
            this.rbtnRobustGRvEXPGR.Size = new System.Drawing.Size(127, 17);
            this.rbtnRobustGRvEXPGR.TabIndex = 119;
            this.rbtnRobustGRvEXPGR.Text = "Robust GR v Exp GR";
            this.rbtnRobustGRvEXPGR.UseVisualStyleBackColor = true;
            this.rbtnRobustGRvEXPGR.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnMixtureModelGR
            // 
            this.rbtnMixtureModelGR.AutoSize = true;
            this.rbtnMixtureModelGR.Location = new System.Drawing.Point(232, 318);
            this.rbtnMixtureModelGR.Name = "rbtnMixtureModelGR";
            this.rbtnMixtureModelGR.Size = new System.Drawing.Size(122, 17);
            this.rbtnMixtureModelGR.TabIndex = 118;
            this.rbtnMixtureModelGR.Text = "Robust Growth Rate";
            this.rbtnMixtureModelGR.UseVisualStyleBackColor = true;
            this.rbtnMixtureModelGR.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnEndODvMax
            // 
            this.rbtnEndODvMax.AutoSize = true;
            this.rbtnEndODvMax.Location = new System.Drawing.Point(137, 670);
            this.rbtnEndODvMax.Name = "rbtnEndODvMax";
            this.rbtnEndODvMax.Size = new System.Drawing.Size(135, 17);
            this.rbtnEndODvMax.TabIndex = 117;
            this.rbtnEndODvMax.Text = "End OD v Growth Rate";
            this.rbtnEndODvMax.UseVisualStyleBackColor = true;
            this.rbtnEndODvMax.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnMaxvGrowthRate
            // 
            this.rbtnMaxvGrowthRate.AutoSize = true;
            this.rbtnMaxvGrowthRate.Location = new System.Drawing.Point(134, 445);
            this.rbtnMaxvGrowthRate.Name = "rbtnMaxvGrowthRate";
            this.rbtnMaxvGrowthRate.Size = new System.Drawing.Size(157, 17);
            this.rbtnMaxvGrowthRate.TabIndex = 116;
            this.rbtnMaxvGrowthRate.Text = "Plot Max OD v Growth Rate";
            this.rbtnMaxvGrowthRate.UseVisualStyleBackColor = true;
            this.rbtnMaxvGrowthRate.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnInitialPopvGrowthRate
            // 
            this.rbtnInitialPopvGrowthRate.AutoSize = true;
            this.rbtnInitialPopvGrowthRate.Location = new System.Drawing.Point(135, 570);
            this.rbtnInitialPopvGrowthRate.Name = "rbtnInitialPopvGrowthRate";
            this.rbtnInitialPopvGrowthRate.Size = new System.Drawing.Size(143, 17);
            this.rbtnInitialPopvGrowthRate.TabIndex = 115;
            this.rbtnInitialPopvGrowthRate.Text = "Initial Pop v Growth Rate";
            this.rbtnInitialPopvGrowthRate.UseVisualStyleBackColor = true;
            this.rbtnInitialPopvGrowthRate.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnMakeQQPlot
            // 
            this.rbtnMakeQQPlot.AutoSize = true;
            this.rbtnMakeQQPlot.Location = new System.Drawing.Point(135, 297);
            this.rbtnMakeQQPlot.Name = "rbtnMakeQQPlot";
            this.rbtnMakeQQPlot.Size = new System.Drawing.Size(92, 17);
            this.rbtnMakeQQPlot.TabIndex = 114;
            this.rbtnMakeQQPlot.Text = "Make QQ Plot";
            this.rbtnMakeQQPlot.UseVisualStyleBackColor = true;
            this.rbtnMakeQQPlot.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnGroupOffSetMinusStart
            // 
            this.rbtnGroupOffSetMinusStart.AutoSize = true;
            this.rbtnGroupOffSetMinusStart.Location = new System.Drawing.Point(245, 499);
            this.rbtnGroupOffSetMinusStart.Name = "rbtnGroupOffSetMinusStart";
            this.rbtnGroupOffSetMinusStart.Size = new System.Drawing.Size(94, 17);
            this.rbtnGroupOffSetMinusStart.TabIndex = 113;
            this.rbtnGroupOffSetMinusStart.Text = "OffSet - InitOD";
            this.rbtnGroupOffSetMinusStart.UseVisualStyleBackColor = true;
            this.rbtnGroupOffSetMinusStart.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // btnMakeEvoGroups
            // 
            this.btnMakeEvoGroups.Location = new System.Drawing.Point(245, 36);
            this.btnMakeEvoGroups.Name = "btnMakeEvoGroups";
            this.btnMakeEvoGroups.Size = new System.Drawing.Size(111, 45);
            this.btnMakeEvoGroups.TabIndex = 112;
            this.btnMakeEvoGroups.Text = "Assign Everyone";
            this.btnMakeEvoGroups.UseVisualStyleBackColor = true;
            this.btnMakeEvoGroups.Click += new System.EventHandler(this.btnMakeEvoGroups_Click);
            // 
            // btnClearTreatments
            // 
            this.btnClearTreatments.Location = new System.Drawing.Point(245, 87);
            this.btnClearTreatments.Name = "btnClearTreatments";
            this.btnClearTreatments.Size = new System.Drawing.Size(111, 45);
            this.btnClearTreatments.TabIndex = 111;
            this.btnClearTreatments.Text = "Clear All Group Assignments";
            this.btnClearTreatments.UseVisualStyleBackColor = true;
            this.btnClearTreatments.Click += new System.EventHandler(this.btnClearTreatments_Click);
            // 
            // rbtnPlotLinearResiduals
            // 
            this.rbtnPlotLinearResiduals.AutoSize = true;
            this.rbtnPlotLinearResiduals.Location = new System.Drawing.Point(137, 645);
            this.rbtnPlotLinearResiduals.Name = "rbtnPlotLinearResiduals";
            this.rbtnPlotLinearResiduals.Size = new System.Drawing.Size(140, 17);
            this.rbtnPlotLinearResiduals.TabIndex = 110;
            this.rbtnPlotLinearResiduals.Text = "Residuals from Linear Fit";
            this.rbtnPlotLinearResiduals.UseVisualStyleBackColor = true;
            this.rbtnPlotLinearResiduals.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnMaxMinusEnd
            // 
            this.rbtnMaxMinusEnd.AutoSize = true;
            this.rbtnMaxMinusEnd.Location = new System.Drawing.Point(135, 245);
            this.rbtnMaxMinusEnd.Name = "rbtnMaxMinusEnd";
            this.rbtnMaxMinusEnd.Size = new System.Drawing.Size(156, 17);
            this.rbtnMaxMinusEnd.TabIndex = 109;
            this.rbtnMaxMinusEnd.Text = "Plot Maximum OD - End OD";
            this.rbtnMaxMinusEnd.UseVisualStyleBackColor = true;
            this.rbtnMaxMinusEnd.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnFittedResiduals
            // 
            this.rbtnFittedResiduals.AutoSize = true;
            this.rbtnFittedResiduals.Location = new System.Drawing.Point(136, 620);
            this.rbtnFittedResiduals.Name = "rbtnFittedResiduals";
            this.rbtnFittedResiduals.Size = new System.Drawing.Size(100, 17);
            this.rbtnFittedResiduals.TabIndex = 108;
            this.rbtnFittedResiduals.Text = "Fitted Residuals";
            this.rbtnFittedResiduals.UseVisualStyleBackColor = true;
            this.rbtnFittedResiduals.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnPlotAllResiduals
            // 
            this.rbtnPlotAllResiduals.AutoSize = true;
            this.rbtnPlotAllResiduals.Location = new System.Drawing.Point(136, 595);
            this.rbtnPlotAllResiduals.Name = "rbtnPlotAllResiduals";
            this.rbtnPlotAllResiduals.Size = new System.Drawing.Size(85, 17);
            this.rbtnPlotAllResiduals.TabIndex = 107;
            this.rbtnPlotAllResiduals.Text = "All Residuals";
            this.rbtnPlotAllResiduals.UseVisualStyleBackColor = true;
            this.rbtnPlotAllResiduals.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // btnRowGroups
            // 
            this.btnRowGroups.Location = new System.Drawing.Point(245, 135);
            this.btnRowGroups.Name = "btnRowGroups";
            this.btnRowGroups.Size = new System.Drawing.Size(111, 45);
            this.btnRowGroups.TabIndex = 106;
            this.btnRowGroups.Text = "Assign by Rows";
            this.btnRowGroups.UseVisualStyleBackColor = true;
            this.btnRowGroups.Click += new System.EventHandler(this.btnRowGroups_Click);
            // 
            // rbtnPlotSlope
            // 
            this.rbtnPlotSlope.AutoSize = true;
            this.rbtnPlotSlope.Location = new System.Drawing.Point(135, 545);
            this.rbtnPlotSlope.Name = "rbtnPlotSlope";
            this.rbtnPlotSlope.Size = new System.Drawing.Size(102, 17);
            this.rbtnPlotSlope.TabIndex = 104;
            this.rbtnPlotSlope.Text = "Slope Doublings";
            this.rbtnPlotSlope.UseVisualStyleBackColor = true;
            this.rbtnPlotSlope.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnPlotLagTime
            // 
            this.rbtnPlotLagTime.AutoSize = true;
            this.rbtnPlotLagTime.Location = new System.Drawing.Point(135, 520);
            this.rbtnPlotLagTime.Name = "rbtnPlotLagTime";
            this.rbtnPlotLagTime.Size = new System.Drawing.Size(69, 17);
            this.rbtnPlotLagTime.TabIndex = 103;
            this.rbtnPlotLagTime.Text = "Lag Time";
            this.rbtnPlotLagTime.UseVisualStyleBackColor = true;
            this.rbtnPlotLagTime.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnPlotLastOD
            // 
            this.rbtnPlotLastOD.AutoSize = true;
            this.rbtnPlotLastOD.Location = new System.Drawing.Point(136, 495);
            this.rbtnPlotLastOD.Name = "rbtnPlotLastOD";
            this.rbtnPlotLastOD.Size = new System.Drawing.Size(64, 17);
            this.rbtnPlotLastOD.TabIndex = 102;
            this.rbtnPlotLastOD.Text = "Last OD";
            this.rbtnPlotLastOD.UseVisualStyleBackColor = true;
            this.rbtnPlotLastOD.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnTimeToOD
            // 
            this.rbtnTimeToOD.AutoSize = true;
            this.rbtnTimeToOD.Location = new System.Drawing.Point(135, 470);
            this.rbtnTimeToOD.Name = "rbtnTimeToOD";
            this.rbtnTimeToOD.Size = new System.Drawing.Size(109, 17);
            this.rbtnTimeToOD.TabIndex = 101;
            this.rbtnTimeToOD.Text = "Time Until OD .02";
            this.rbtnTimeToOD.UseVisualStyleBackColor = true;
            this.rbtnTimeToOD.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // chkTreatShowLog
            // 
            this.chkTreatShowLog.AutoSize = true;
            this.chkTreatShowLog.Location = new System.Drawing.Point(264, 198);
            this.chkTreatShowLog.Name = "chkTreatShowLog";
            this.chkTreatShowLog.Size = new System.Drawing.Size(109, 17);
            this.chkTreatShowLog.TabIndex = 76;
            this.chkTreatShowLog.Text = "Show Log Values";
            this.chkTreatShowLog.UseVisualStyleBackColor = true;
            this.chkTreatShowLog.CheckedChanged += new System.EventHandler(this.chkTreatShowLog_CheckedChanged);
            // 
            // chkTreatLegend
            // 
            this.chkTreatLegend.AutoSize = true;
            this.chkTreatLegend.Location = new System.Drawing.Point(156, 199);
            this.chkTreatLegend.Name = "chkTreatLegend";
            this.chkTreatLegend.Size = new System.Drawing.Size(92, 17);
            this.chkTreatLegend.TabIndex = 79;
            this.chkTreatLegend.Text = "Show Legend";
            this.chkTreatLegend.UseVisualStyleBackColor = true;
            this.chkTreatLegend.CheckedChanged += new System.EventHandler(this.chkTreatShowLog_CheckedChanged);
            // 
            // rbtnTreatNumPoints
            // 
            this.rbtnTreatNumPoints.AutoSize = true;
            this.rbtnTreatNumPoints.Location = new System.Drawing.Point(135, 420);
            this.rbtnTreatNumPoints.Name = "rbtnTreatNumPoints";
            this.rbtnTreatNumPoints.Size = new System.Drawing.Size(120, 17);
            this.rbtnTreatNumPoints.TabIndex = 100;
            this.rbtnTreatNumPoints.Text = "Number of Points Fit";
            this.rbtnTreatNumPoints.UseVisualStyleBackColor = true;
            this.rbtnTreatNumPoints.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnTreatTimevOD
            // 
            this.rbtnTreatTimevOD.AutoSize = true;
            this.rbtnTreatTimevOD.Checked = true;
            this.rbtnTreatTimevOD.Location = new System.Drawing.Point(135, 395);
            this.rbtnTreatTimevOD.Name = "rbtnTreatTimevOD";
            this.rbtnTreatTimevOD.Size = new System.Drawing.Size(81, 17);
            this.rbtnTreatTimevOD.TabIndex = 99;
            this.rbtnTreatTimevOD.TabStop = true;
            this.rbtnTreatTimevOD.Text = "Time vs OD";
            this.rbtnTreatTimevOD.UseVisualStyleBackColor = true;
            this.rbtnTreatTimevOD.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnTreatDoublingTime
            // 
            this.rbtnTreatDoublingTime.AutoSize = true;
            this.rbtnTreatDoublingTime.Location = new System.Drawing.Point(135, 370);
            this.rbtnTreatDoublingTime.Name = "rbtnTreatDoublingTime";
            this.rbtnTreatDoublingTime.Size = new System.Drawing.Size(93, 17);
            this.rbtnTreatDoublingTime.TabIndex = 98;
            this.rbtnTreatDoublingTime.Text = "Doubling Time";
            this.rbtnTreatDoublingTime.UseVisualStyleBackColor = true;
            this.rbtnTreatDoublingTime.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnTreatInitialOD
            // 
            this.rbtnTreatInitialOD.AutoSize = true;
            this.rbtnTreatInitialOD.Location = new System.Drawing.Point(135, 345);
            this.rbtnTreatInitialOD.Name = "rbtnTreatInitialOD";
            this.rbtnTreatInitialOD.Size = new System.Drawing.Size(66, 17);
            this.rbtnTreatInitialOD.TabIndex = 97;
            this.rbtnTreatInitialOD.Text = "Intial OD";
            this.rbtnTreatInitialOD.UseVisualStyleBackColor = true;
            this.rbtnTreatInitialOD.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(122, 223);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(159, 20);
            this.label21.TabIndex = 96;
            this.label21.Text = "Select Data To Show";
            // 
            // rbtnTreatRSq
            // 
            this.rbtnTreatRSq.AutoSize = true;
            this.rbtnTreatRSq.Location = new System.Drawing.Point(135, 320);
            this.rbtnTreatRSq.Name = "rbtnTreatRSq";
            this.rbtnTreatRSq.Size = new System.Drawing.Size(74, 17);
            this.rbtnTreatRSq.TabIndex = 95;
            this.rbtnTreatRSq.Text = "R squared";
            this.rbtnTreatRSq.UseVisualStyleBackColor = true;
            this.rbtnTreatRSq.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnTreatGrowthRate
            // 
            this.rbtnTreatGrowthRate.AutoSize = true;
            this.rbtnTreatGrowthRate.Location = new System.Drawing.Point(232, 270);
            this.rbtnTreatGrowthRate.Name = "rbtnTreatGrowthRate";
            this.rbtnTreatGrowthRate.Size = new System.Drawing.Size(85, 17);
            this.rbtnTreatGrowthRate.TabIndex = 94;
            this.rbtnTreatGrowthRate.Text = "Growth Rate";
            this.rbtnTreatGrowthRate.UseVisualStyleBackColor = true;
            this.rbtnTreatGrowthRate.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // rbtnTreatMaxOd
            // 
            this.rbtnTreatMaxOd.AutoSize = true;
            this.rbtnTreatMaxOd.Location = new System.Drawing.Point(135, 270);
            this.rbtnTreatMaxOd.Name = "rbtnTreatMaxOd";
            this.rbtnTreatMaxOd.Size = new System.Drawing.Size(88, 17);
            this.rbtnTreatMaxOd.TabIndex = 93;
            this.rbtnTreatMaxOd.Text = "Maximum OD";
            this.rbtnTreatMaxOd.UseVisualStyleBackColor = true;
            this.rbtnTreatMaxOd.CheckedChanged += new System.EventHandler(this.chkTreat_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(5, 636);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(95, 13);
            this.label20.TabIndex = 92;
            this.label20.Text = "Name Treatment 6";
            // 
            // txtTreatment6
            // 
            this.txtTreatment6.Location = new System.Drawing.Point(5, 655);
            this.txtTreatment6.Name = "txtTreatment6";
            this.txtTreatment6.Size = new System.Drawing.Size(100, 20);
            this.txtTreatment6.TabIndex = 91;
            // 
            // txtTreatment5
            // 
            this.txtTreatment5.Location = new System.Drawing.Point(5, 610);
            this.txtTreatment5.Name = "txtTreatment5";
            this.txtTreatment5.Size = new System.Drawing.Size(100, 20);
            this.txtTreatment5.TabIndex = 87;
            // 
            // txtTreatment4
            // 
            this.txtTreatment4.Location = new System.Drawing.Point(5, 564);
            this.txtTreatment4.Name = "txtTreatment4";
            this.txtTreatment4.Size = new System.Drawing.Size(100, 20);
            this.txtTreatment4.TabIndex = 85;
            // 
            // txtTreatment3
            // 
            this.txtTreatment3.Location = new System.Drawing.Point(5, 520);
            this.txtTreatment3.Name = "txtTreatment3";
            this.txtTreatment3.Size = new System.Drawing.Size(100, 20);
            this.txtTreatment3.TabIndex = 83;
            // 
            // txtTreatment2
            // 
            this.txtTreatment2.Location = new System.Drawing.Point(5, 476);
            this.txtTreatment2.Name = "txtTreatment2";
            this.txtTreatment2.Size = new System.Drawing.Size(100, 20);
            this.txtTreatment2.TabIndex = 81;
            // 
            // txtTreatment1
            // 
            this.txtTreatment1.Location = new System.Drawing.Point(5, 434);
            this.txtTreatment1.Name = "txtTreatment1";
            this.txtTreatment1.Size = new System.Drawing.Size(100, 20);
            this.txtTreatment1.TabIndex = 78;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(5, 210);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(132, 26);
            this.label19.TabIndex = 90;
            this.label19.Text = "Select Treatment Group to\r\nAssign by Clicking";
            // 
            // lstTreatmentSelection
            // 
            this.lstTreatmentSelection.FormattingEnabled = true;
            this.lstTreatmentSelection.Items.AddRange(new object[] {
            "Unassigned",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.lstTreatmentSelection.Location = new System.Drawing.Point(5, 239);
            this.lstTreatmentSelection.Name = "lstTreatmentSelection";
            this.lstTreatmentSelection.Size = new System.Drawing.Size(120, 173);
            this.lstTreatmentSelection.TabIndex = 89;
            this.lstTreatmentSelection.SelectedIndexChanged += new System.EventHandler(this.lstTreatmentSelection_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 591);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(95, 13);
            this.label18.TabIndex = 88;
            this.label18.Text = "Name Treatment 5";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(5, 545);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(95, 13);
            this.label17.TabIndex = 86;
            this.label17.Text = "Name Treatment 4";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 501);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 13);
            this.label16.TabIndex = 84;
            this.label16.Text = "Name Treatment 3";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 457);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 13);
            this.label15.TabIndex = 82;
            this.label15.Text = "Name Treatment 2";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 415);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 13);
            this.label14.TabIndex = 80;
            this.label14.Text = "Name Treatment 1";
            // 
            // plotTreatments
            // 
            this.plotTreatments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotTreatments.Location = new System.Drawing.Point(0, 0);
            this.plotTreatments.Name = "plotTreatments";
            this.plotTreatments.ScrollGrace = 0D;
            this.plotTreatments.ScrollMaxX = 0D;
            this.plotTreatments.ScrollMaxY = 0D;
            this.plotTreatments.ScrollMaxY2 = 0D;
            this.plotTreatments.ScrollMinX = 0D;
            this.plotTreatments.ScrollMinY = 0D;
            this.plotTreatments.ScrollMinY2 = 0D;
            this.plotTreatments.Size = new System.Drawing.Size(773, 695);
            this.plotTreatments.TabIndex = 4;
            // 
            // tabBlankRemoval
            // 
            this.tabBlankRemoval.Controls.Add(this.btnSubtractWrittenBlank);
            this.tabBlankRemoval.Controls.Add(this.txtBlankValue);
            this.tabBlankRemoval.Controls.Add(this.btnDeleteLikelyBlanks);
            this.tabBlankRemoval.Controls.Add(this.lblDeletePlate);
            this.tabBlankRemoval.Controls.Add(this.btnDeleteAvg3asBlank);
            this.tabBlankRemoval.Controls.Add(this.btnDelete2ndPoint);
            this.tabBlankRemoval.Controls.Add(this.btnDeleteFirstBlank);
            this.tabBlankRemoval.Controls.Add(this.toDeletePlateMap);
            this.tabBlankRemoval.Location = new System.Drawing.Point(4, 22);
            this.tabBlankRemoval.Name = "tabBlankRemoval";
            this.tabBlankRemoval.Padding = new System.Windows.Forms.Padding(3);
            this.tabBlankRemoval.Size = new System.Drawing.Size(1165, 695);
            this.tabBlankRemoval.TabIndex = 5;
            this.tabBlankRemoval.Text = "Remove Blanks From Plate Data";
            this.tabBlankRemoval.UseVisualStyleBackColor = true;
            // 
            // btnSubtractWrittenBlank
            // 
            this.btnSubtractWrittenBlank.Location = new System.Drawing.Point(64, 250);
            this.btnSubtractWrittenBlank.Name = "btnSubtractWrittenBlank";
            this.btnSubtractWrittenBlank.Size = new System.Drawing.Size(298, 42);
            this.btnSubtractWrittenBlank.TabIndex = 17;
            this.btnSubtractWrittenBlank.Text = "Subtract Value Below As Blank";
            this.btnSubtractWrittenBlank.UseVisualStyleBackColor = true;
            this.btnSubtractWrittenBlank.Click += new System.EventHandler(this.btnSubtractWrittenBlank_Click_1);
            // 
            // txtBlankValue
            // 
            this.txtBlankValue.Location = new System.Drawing.Point(155, 298);
            this.txtBlankValue.Name = "txtBlankValue";
            this.txtBlankValue.Size = new System.Drawing.Size(100, 20);
            this.txtBlankValue.TabIndex = 16;
            this.txtBlankValue.Text = ".034";
            // 
            // btnDeleteLikelyBlanks
            // 
            this.btnDeleteLikelyBlanks.Location = new System.Drawing.Point(574, 502);
            this.btnDeleteLikelyBlanks.Name = "btnDeleteLikelyBlanks";
            this.btnDeleteLikelyBlanks.Size = new System.Drawing.Size(302, 37);
            this.btnDeleteLikelyBlanks.TabIndex = 15;
            this.btnDeleteLikelyBlanks.Text = "Delete Wells with OD below 0.05";
            this.btnDeleteLikelyBlanks.UseVisualStyleBackColor = true;
            this.btnDeleteLikelyBlanks.Click += new System.EventHandler(this.btnDeleteLikelyBlanks_Click);
            // 
            // lblDeletePlate
            // 
            this.lblDeletePlate.AutoSize = true;
            this.lblDeletePlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeletePlate.Location = new System.Drawing.Point(642, 465);
            this.lblDeletePlate.Name = "lblDeletePlate";
            this.lblDeletePlate.Size = new System.Drawing.Size(184, 24);
            this.lblDeletePlate.TabIndex = 14;
            this.lblDeletePlate.Text = "Click Well to Delete It";
            // 
            // btnDeleteAvg3asBlank
            // 
            this.btnDeleteAvg3asBlank.Location = new System.Drawing.Point(64, 174);
            this.btnDeleteAvg3asBlank.Name = "btnDeleteAvg3asBlank";
            this.btnDeleteAvg3asBlank.Size = new System.Drawing.Size(298, 37);
            this.btnDeleteAvg3asBlank.TabIndex = 12;
            this.btnDeleteAvg3asBlank.Text = "Delete Average of First Three Points as Blank";
            this.btnDeleteAvg3asBlank.UseVisualStyleBackColor = true;
            this.btnDeleteAvg3asBlank.Click += new System.EventHandler(this.btnDeleteAvg3asBlank_Click);
            // 
            // btnDelete2ndPoint
            // 
            this.btnDelete2ndPoint.Location = new System.Drawing.Point(64, 118);
            this.btnDelete2ndPoint.Name = "btnDelete2ndPoint";
            this.btnDelete2ndPoint.Size = new System.Drawing.Size(298, 37);
            this.btnDelete2ndPoint.TabIndex = 11;
            this.btnDelete2ndPoint.Text = "Delete Second Data Point in Each Well As Blank";
            this.btnDelete2ndPoint.UseVisualStyleBackColor = true;
            this.btnDelete2ndPoint.Click += new System.EventHandler(this.btnDelete2ndPoint_Click);
            // 
            // btnDeleteFirstBlank
            // 
            this.btnDeleteFirstBlank.Location = new System.Drawing.Point(64, 60);
            this.btnDeleteFirstBlank.Name = "btnDeleteFirstBlank";
            this.btnDeleteFirstBlank.Size = new System.Drawing.Size(298, 37);
            this.btnDeleteFirstBlank.TabIndex = 10;
            this.btnDeleteFirstBlank.Text = "Delete First Data Point in Each Well As Blank";
            this.btnDeleteFirstBlank.UseVisualStyleBackColor = true;
            this.btnDeleteFirstBlank.Click += new System.EventHandler(this.btnDeleteFirstBlank_Click);
            // 
            // tabRobo
            // 
            this.tabRobo.Controls.Add(this.rbtnPlatesLogisticR2);
            this.tabRobo.Controls.Add(this.rbtnPlateQuadR2);
            this.tabRobo.Controls.Add(this.rbtnPlotQuadraticRate);
            this.tabRobo.Controls.Add(this.rbtnPlatePlotLogistic15);
            this.tabRobo.Controls.Add(this.rbtnOutlierNumber);
            this.tabRobo.Controls.Add(this.rbtnMixtureGrowthRate);
            this.tabRobo.Controls.Add(this.rbtnGroupGrowthRate);
            this.tabRobo.Controls.Add(this.rbtnPlateOffSetIDMinusStart);
            this.tabRobo.Controls.Add(this.rbtnOffSetGR);
            this.tabRobo.Controls.Add(this.rbtnOffSet);
            this.tabRobo.Controls.Add(this.rbtnIntercept);
            this.tabRobo.Controls.Add(this.rbtnRMSE);
            this.tabRobo.Controls.Add(this.lblRobo);
            this.tabRobo.Controls.Add(this.graphHistogram);
            this.tabRobo.Controls.Add(this.label12);
            this.tabRobo.Controls.Add(this.plateMap);
            this.tabRobo.Controls.Add(this.lstTimePoints);
            this.tabRobo.Controls.Add(this.rbtnTimePoint);
            this.tabRobo.Controls.Add(this.rbtnDoubleTime);
            this.tabRobo.Controls.Add(this.rbtnIOD);
            this.tabRobo.Controls.Add(this.label9);
            this.tabRobo.Controls.Add(this.rbtnRS);
            this.tabRobo.Controls.Add(this.rbtnGrowthRate);
            this.tabRobo.Controls.Add(this.rbtnMaxOD);
            this.tabRobo.Location = new System.Drawing.Point(4, 22);
            this.tabRobo.Name = "tabRobo";
            this.tabRobo.Size = new System.Drawing.Size(1165, 695);
            this.tabRobo.TabIndex = 0;
            this.tabRobo.Text = "View Microtiter Plate Data";
            this.tabRobo.UseVisualStyleBackColor = true;
            // 
            // rbtnPlatesLogisticR2
            // 
            this.rbtnPlatesLogisticR2.AutoSize = true;
            this.rbtnPlatesLogisticR2.Location = new System.Drawing.Point(486, 660);
            this.rbtnPlatesLogisticR2.Name = "rbtnPlatesLogisticR2";
            this.rbtnPlatesLogisticR2.Size = new System.Drawing.Size(99, 17);
            this.rbtnPlatesLogisticR2.TabIndex = 31;
            this.rbtnPlatesLogisticR2.Text = "Plot Logistic R2";
            this.rbtnPlatesLogisticR2.UseVisualStyleBackColor = true;
            this.rbtnPlatesLogisticR2.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnPlateQuadR2
            // 
            this.rbtnPlateQuadR2.AutoSize = true;
            this.rbtnPlateQuadR2.Location = new System.Drawing.Point(380, 660);
            this.rbtnPlateQuadR2.Name = "rbtnPlateQuadR2";
            this.rbtnPlateQuadR2.Size = new System.Drawing.Size(109, 17);
            this.rbtnPlateQuadR2.TabIndex = 30;
            this.rbtnPlateQuadR2.Text = "Plot Quadratic R2";
            this.rbtnPlateQuadR2.UseVisualStyleBackColor = true;
            this.rbtnPlateQuadR2.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnPlotQuadraticRate
            // 
            this.rbtnPlotQuadraticRate.AutoSize = true;
            this.rbtnPlotQuadraticRate.Location = new System.Drawing.Point(177, 660);
            this.rbtnPlotQuadraticRate.Name = "rbtnPlotQuadraticRate";
            this.rbtnPlotQuadraticRate.Size = new System.Drawing.Size(179, 17);
            this.rbtnPlotQuadraticRate.TabIndex = 29;
            this.rbtnPlotQuadraticRate.Text = "Plot Quadratic Rate at OD 0.015";
            this.rbtnPlotQuadraticRate.UseVisualStyleBackColor = true;
            this.rbtnPlotQuadraticRate.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnPlatePlotLogistic15
            // 
            this.rbtnPlatePlotLogistic15.AutoSize = true;
            this.rbtnPlatePlotLogistic15.Location = new System.Drawing.Point(8, 660);
            this.rbtnPlatePlotLogistic15.Name = "rbtnPlatePlotLogistic15";
            this.rbtnPlatePlotLogistic15.Size = new System.Drawing.Size(163, 17);
            this.rbtnPlatePlotLogistic15.TabIndex = 28;
            this.rbtnPlatePlotLogistic15.Text = "Plot Logistic Rate at OD 0.15";
            this.rbtnPlatePlotLogistic15.UseVisualStyleBackColor = true;
            this.rbtnPlatePlotLogistic15.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnOutlierNumber
            // 
            this.rbtnOutlierNumber.AutoSize = true;
            this.rbtnOutlierNumber.Location = new System.Drawing.Point(774, 628);
            this.rbtnOutlierNumber.Name = "rbtnOutlierNumber";
            this.rbtnOutlierNumber.Size = new System.Drawing.Size(133, 17);
            this.rbtnOutlierNumber.TabIndex = 27;
            this.rbtnOutlierNumber.Text = "Plot Number of Outliers";
            this.rbtnOutlierNumber.UseVisualStyleBackColor = true;
            this.rbtnOutlierNumber.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnMixtureGrowthRate
            // 
            this.rbtnMixtureGrowthRate.AutoSize = true;
            this.rbtnMixtureGrowthRate.Location = new System.Drawing.Point(632, 594);
            this.rbtnMixtureGrowthRate.Name = "rbtnMixtureGrowthRate";
            this.rbtnMixtureGrowthRate.Size = new System.Drawing.Size(143, 17);
            this.rbtnMixtureGrowthRate.TabIndex = 26;
            this.rbtnMixtureGrowthRate.Text = "Plot Robust Growth Rate";
            this.rbtnMixtureGrowthRate.UseVisualStyleBackColor = true;
            this.rbtnMixtureGrowthRate.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnGroupGrowthRate
            // 
            this.rbtnGroupGrowthRate.AutoSize = true;
            this.rbtnGroupGrowthRate.Location = new System.Drawing.Point(632, 628);
            this.rbtnGroupGrowthRate.Name = "rbtnGroupGrowthRate";
            this.rbtnGroupGrowthRate.Size = new System.Drawing.Size(138, 17);
            this.rbtnGroupGrowthRate.TabIndex = 25;
            this.rbtnGroupGrowthRate.Text = "Plot Group Growth Rate";
            this.rbtnGroupGrowthRate.UseVisualStyleBackColor = true;
            this.rbtnGroupGrowthRate.Click += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnPlateOffSetIDMinusStart
            // 
            this.rbtnPlateOffSetIDMinusStart.AutoSize = true;
            this.rbtnPlateOffSetIDMinusStart.Location = new System.Drawing.Point(377, 594);
            this.rbtnPlateOffSetIDMinusStart.Name = "rbtnPlateOffSetIDMinusStart";
            this.rbtnPlateOffSetIDMinusStart.Size = new System.Drawing.Size(112, 17);
            this.rbtnPlateOffSetIDMinusStart.TabIndex = 24;
            this.rbtnPlateOffSetIDMinusStart.Text = "Plot Off Set - Initial";
            this.rbtnPlateOffSetIDMinusStart.UseVisualStyleBackColor = true;
            this.rbtnPlateOffSetIDMinusStart.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnOffSetGR
            // 
            this.rbtnOffSetGR.AutoSize = true;
            this.rbtnOffSetGR.Location = new System.Drawing.Point(484, 628);
            this.rbtnOffSetGR.Name = "rbtnOffSetGR";
            this.rbtnOffSetGR.Size = new System.Drawing.Size(142, 17);
            this.rbtnOffSetGR.TabIndex = 23;
            this.rbtnOffSetGR.Text = "Plot Off Set Growth Rate";
            this.rbtnOffSetGR.UseVisualStyleBackColor = true;
            this.rbtnOffSetGR.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnOffSet
            // 
            this.rbtnOffSet.AutoSize = true;
            this.rbtnOffSet.Location = new System.Drawing.Point(263, 628);
            this.rbtnOffSet.Name = "rbtnOffSet";
            this.rbtnOffSet.Size = new System.Drawing.Size(79, 17);
            this.rbtnOffSet.TabIndex = 22;
            this.rbtnOffSet.Text = "Plot Off Set";
            this.rbtnOffSet.UseVisualStyleBackColor = true;
            this.rbtnOffSet.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnIntercept
            // 
            this.rbtnIntercept.AutoSize = true;
            this.rbtnIntercept.Location = new System.Drawing.Point(495, 594);
            this.rbtnIntercept.Name = "rbtnIntercept";
            this.rbtnIntercept.Size = new System.Drawing.Size(88, 17);
            this.rbtnIntercept.TabIndex = 21;
            this.rbtnIntercept.Text = "Plot Intercept";
            this.rbtnIntercept.UseVisualStyleBackColor = true;
            this.rbtnIntercept.Click += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnRMSE
            // 
            this.rbtnRMSE.AutoSize = true;
            this.rbtnRMSE.Location = new System.Drawing.Point(380, 628);
            this.rbtnRMSE.Name = "rbtnRMSE";
            this.rbtnRMSE.Size = new System.Drawing.Size(77, 17);
            this.rbtnRMSE.TabIndex = 19;
            this.rbtnRMSE.Text = "Plot RMSE";
            this.rbtnRMSE.UseVisualStyleBackColor = true;
            this.rbtnRMSE.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // lblRobo
            // 
            this.lblRobo.AutoSize = true;
            this.lblRobo.Location = new System.Drawing.Point(26, 497);
            this.lblRobo.Name = "lblRobo";
            this.lblRobo.Size = new System.Drawing.Size(0, 13);
            this.lblRobo.TabIndex = 18;
            // 
            // graphHistogram
            // 
            this.graphHistogram.Location = new System.Drawing.Point(660, 3);
            this.graphHistogram.Name = "graphHistogram";
            this.graphHistogram.ScrollGrace = 0D;
            this.graphHistogram.ScrollMaxX = 0D;
            this.graphHistogram.ScrollMaxY = 0D;
            this.graphHistogram.ScrollMaxY2 = 0D;
            this.graphHistogram.ScrollMinX = 0D;
            this.graphHistogram.ScrollMinY = 0D;
            this.graphHistogram.ScrollMinY2 = 0D;
            this.graphHistogram.Size = new System.Drawing.Size(497, 341);
            this.graphHistogram.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(998, 347);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Sample Times";
            // 
            // lstTimePoints
            // 
            this.lstTimePoints.FormattingEnabled = true;
            this.lstTimePoints.Location = new System.Drawing.Point(949, 363);
            this.lstTimePoints.Name = "lstTimePoints";
            this.lstTimePoints.Size = new System.Drawing.Size(199, 264);
            this.lstTimePoints.TabIndex = 12;
            this.lstTimePoints.SelectedIndexChanged += new System.EventHandler(this.lstTimePoints_SelectedIndexChanged);
            // 
            // rbtnTimePoint
            // 
            this.rbtnTimePoint.AutoSize = true;
            this.rbtnTimePoint.Location = new System.Drawing.Point(935, 660);
            this.rbtnTimePoint.Name = "rbtnTimePoint";
            this.rbtnTimePoint.Size = new System.Drawing.Size(114, 17);
            this.rbtnTimePoint.TabIndex = 11;
            this.rbtnTimePoint.Text = "Plot Selected Time";
            this.rbtnTimePoint.UseVisualStyleBackColor = true;
            this.rbtnTimePoint.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnDoubleTime
            // 
            this.rbtnDoubleTime.AutoSize = true;
            this.rbtnDoubleTime.Location = new System.Drawing.Point(143, 628);
            this.rbtnDoubleTime.Name = "rbtnDoubleTime";
            this.rbtnDoubleTime.Size = new System.Drawing.Size(114, 17);
            this.rbtnDoubleTime.TabIndex = 10;
            this.rbtnDoubleTime.Text = "Plot Doubling Time";
            this.rbtnDoubleTime.UseVisualStyleBackColor = true;
            this.rbtnDoubleTime.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnIOD
            // 
            this.rbtnIOD.AutoSize = true;
            this.rbtnIOD.Location = new System.Drawing.Point(263, 594);
            this.rbtnIOD.Name = "rbtnIOD";
            this.rbtnIOD.Size = new System.Drawing.Size(87, 17);
            this.rbtnIOD.TabIndex = 9;
            this.rbtnIOD.Text = "Plot Intial OD";
            this.rbtnIOD.UseVisualStyleBackColor = true;
            this.rbtnIOD.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 571);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "Select Data To Show";
            // 
            // rbtnRS
            // 
            this.rbtnRS.AutoSize = true;
            this.rbtnRS.Location = new System.Drawing.Point(143, 594);
            this.rbtnRS.Name = "rbtnRS";
            this.rbtnRS.Size = new System.Drawing.Size(95, 17);
            this.rbtnRS.TabIndex = 7;
            this.rbtnRS.Text = "Plot R squared";
            this.rbtnRS.UseVisualStyleBackColor = true;
            this.rbtnRS.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnGrowthRate
            // 
            this.rbtnGrowthRate.AutoSize = true;
            this.rbtnGrowthRate.Checked = true;
            this.rbtnGrowthRate.Location = new System.Drawing.Point(8, 628);
            this.rbtnGrowthRate.Name = "rbtnGrowthRate";
            this.rbtnGrowthRate.Size = new System.Drawing.Size(106, 17);
            this.rbtnGrowthRate.TabIndex = 6;
            this.rbtnGrowthRate.TabStop = true;
            this.rbtnGrowthRate.Text = "Plot Growth Rate";
            this.rbtnGrowthRate.UseVisualStyleBackColor = true;
            this.rbtnGrowthRate.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // rbtnMaxOD
            // 
            this.rbtnMaxOD.AutoSize = true;
            this.rbtnMaxOD.Location = new System.Drawing.Point(8, 594);
            this.rbtnMaxOD.Name = "rbtnMaxOD";
            this.rbtnMaxOD.Size = new System.Drawing.Size(109, 17);
            this.rbtnMaxOD.TabIndex = 5;
            this.rbtnMaxOD.Text = "Plot Maximum OD";
            this.rbtnMaxOD.UseVisualStyleBackColor = true;
            this.rbtnMaxOD.CheckedChanged += new System.EventHandler(this.rbtnPlateOptions_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tblRawData);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1165, 695);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "View Raw Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tblRawData
            // 
            this.tblRawData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tblRawData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tblRawData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tblRawData.DefaultCellStyle = dataGridViewCellStyle2;
            this.tblRawData.Location = new System.Drawing.Point(356, 25);
            this.tblRawData.Name = "tblRawData";
            this.tblRawData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tblRawData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tblRawData.Size = new System.Drawing.Size(428, 580);
            this.tblRawData.TabIndex = 0;
            // 
            // tabPageChangeFitted
            // 
            this.tabPageChangeFitted.Controls.Add(this.chkShowFittedPick);
            this.tabPageChangeFitted.Controls.Add(this.ChartPickData);
            this.tabPageChangeFitted.Controls.Add(this.label4);
            this.tabPageChangeFitted.Controls.Add(this.lstGrowthCurvesMirror);
            this.tabPageChangeFitted.Controls.Add(this.label3);
            this.tabPageChangeFitted.Controls.Add(this.lstDataMirror);
            this.tabPageChangeFitted.Location = new System.Drawing.Point(4, 22);
            this.tabPageChangeFitted.Name = "tabPageChangeFitted";
            this.tabPageChangeFitted.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChangeFitted.Size = new System.Drawing.Size(1165, 695);
            this.tabPageChangeFitted.TabIndex = 1;
            this.tabPageChangeFitted.Text = "Change Data To Fit";
            this.tabPageChangeFitted.UseVisualStyleBackColor = true;
            // 
            // chkShowFittedPick
            // 
            this.chkShowFittedPick.AutoSize = true;
            this.chkShowFittedPick.Checked = true;
            this.chkShowFittedPick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowFittedPick.Location = new System.Drawing.Point(15, 608);
            this.chkShowFittedPick.Name = "chkShowFittedPick";
            this.chkShowFittedPick.Size = new System.Drawing.Size(105, 17);
            this.chkShowFittedPick.TabIndex = 8;
            this.chkShowFittedPick.Text = "Show Fitted Line";
            this.chkShowFittedPick.UseVisualStyleBackColor = true;
            this.chkShowFittedPick.CheckedChanged += new System.EventHandler(this.chkShowFittedPick_CheckedChanged);
            // 
            // ChartPickData
            // 
            this.ChartPickData.Location = new System.Drawing.Point(351, 77);
            this.ChartPickData.Name = "ChartPickData";
            this.ChartPickData.ScrollGrace = 0D;
            this.ChartPickData.ScrollMaxX = 0D;
            this.ChartPickData.ScrollMaxY = 0D;
            this.ChartPickData.ScrollMaxY2 = 0D;
            this.ChartPickData.ScrollMinX = 0D;
            this.ChartPickData.ScrollMinY = 0D;
            this.ChartPickData.ScrollMinY2 = 0D;
            this.ChartPickData.Size = new System.Drawing.Size(756, 528);
            this.ChartPickData.TabIndex = 7;
            this.ChartPickData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChartPickData_MouseClick_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(491, 623);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Two Points Must Always Be Selected";
            // 
            // lstGrowthCurvesMirror
            // 
            this.lstGrowthCurvesMirror.FormattingEnabled = true;
            this.lstGrowthCurvesMirror.Location = new System.Drawing.Point(15, 239);
            this.lstGrowthCurvesMirror.Name = "lstGrowthCurvesMirror";
            this.lstGrowthCurvesMirror.Size = new System.Drawing.Size(282, 355);
            this.lstGrowthCurvesMirror.TabIndex = 5;
            this.lstGrowthCurvesMirror.SelectedIndexChanged += new System.EventHandler(this.lstGrowthCurvesMirror_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(517, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(356, 60);
            this.label3.TabIndex = 4;
            this.label3.Text = "Click on a point to add or remove it from the fit.\r\nPoints can also be selected o" +
    "n the log plot for the\r\nview and fit data tab.";
            // 
            // lstDataMirror
            // 
            this.lstDataMirror.FormattingEnabled = true;
            this.lstDataMirror.Location = new System.Drawing.Point(6, 44);
            this.lstDataMirror.Name = "lstDataMirror";
            this.lstDataMirror.Size = new System.Drawing.Size(282, 173);
            this.lstDataMirror.TabIndex = 3;
            // 
            // tabMainTab
            // 
            this.tabMainTab.Controls.Add(this.tabPageMain);
            this.tabMainTab.Controls.Add(this.tabPageChangeFitted);
            this.tabMainTab.Controls.Add(this.tabPage1);
            this.tabMainTab.Controls.Add(this.tabRobo);
            this.tabMainTab.Controls.Add(this.tabBlankRemoval);
            this.tabMainTab.Controls.Add(this.tabPlotGraphic);
            this.tabMainTab.Controls.Add(this.tabSensitivityAnalysis);
            this.tabMainTab.Controls.Add(this.tabGroup);
            this.tabMainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMainTab.Location = new System.Drawing.Point(0, 24);
            this.tabMainTab.Name = "tabMainTab";
            this.tabMainTab.SelectedIndex = 0;
            this.tabMainTab.Size = new System.Drawing.Size(1173, 721);
            this.tabMainTab.TabIndex = 9;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.splitContainer2);
            this.tabPageMain.Controls.Add(this.topRightPanel);
            this.tabPageMain.Controls.Add(this.panel1);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(1165, 695);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "View and Fit Data";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(238, 323);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ChartSlopeN);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ChartStandard);
            this.splitContainer2.Size = new System.Drawing.Size(924, 369);
            this.splitContainer2.SplitterDistance = 400;
            this.splitContainer2.TabIndex = 16;
            // 
            // ChartSlopeN
            // 
            this.ChartSlopeN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartSlopeN.Location = new System.Drawing.Point(0, 0);
            this.ChartSlopeN.Name = "ChartSlopeN";
            this.ChartSlopeN.ScrollGrace = 0D;
            this.ChartSlopeN.ScrollMaxX = 0D;
            this.ChartSlopeN.ScrollMaxY = 0D;
            this.ChartSlopeN.ScrollMaxY2 = 0D;
            this.ChartSlopeN.ScrollMinX = 0D;
            this.ChartSlopeN.ScrollMinY = 0D;
            this.ChartSlopeN.ScrollMinY2 = 0D;
            this.ChartSlopeN.Size = new System.Drawing.Size(400, 369);
            this.ChartSlopeN.TabIndex = 14;
            // 
            // ChartStandard
            // 
            this.ChartStandard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartStandard.Location = new System.Drawing.Point(0, 0);
            this.ChartStandard.Name = "ChartStandard";
            this.ChartStandard.ScrollGrace = 0D;
            this.ChartStandard.ScrollMaxX = 0D;
            this.ChartStandard.ScrollMaxY = 0D;
            this.ChartStandard.ScrollMaxY2 = 0D;
            this.ChartStandard.ScrollMinX = 0D;
            this.ChartStandard.ScrollMinY = 0D;
            this.ChartStandard.ScrollMinY2 = 0D;
            this.ChartStandard.Size = new System.Drawing.Size(520, 369);
            this.ChartStandard.TabIndex = 4;
            // 
            // topRightPanel
            // 
            this.topRightPanel.Controls.Add(this.splitContainer1);
            this.topRightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topRightPanel.Location = new System.Drawing.Point(238, 3);
            this.topRightPanel.Name = "topRightPanel";
            this.topRightPanel.Size = new System.Drawing.Size(924, 320);
            this.topRightPanel.TabIndex = 15;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ChartN);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.l);
            this.splitContainer1.Size = new System.Drawing.Size(924, 320);
            this.splitContainer1.SplitterDistance = 632;
            this.splitContainer1.TabIndex = 15;
            // 
            // ChartN
            // 
            this.ChartN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartN.Location = new System.Drawing.Point(0, 0);
            this.ChartN.Name = "ChartN";
            this.ChartN.ScrollGrace = 0D;
            this.ChartN.ScrollMaxX = 0D;
            this.ChartN.ScrollMaxY = 0D;
            this.ChartN.ScrollMaxY2 = 0D;
            this.ChartN.ScrollMinX = 0D;
            this.ChartN.ScrollMinY = 0D;
            this.ChartN.ScrollMinY2 = 0D;
            this.ChartN.Size = new System.Drawing.Size(632, 320);
            this.ChartN.TabIndex = 17;
            this.ChartN.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChartN_MouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(739, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "End Point For Data";
            // 
            // l
            // 
            this.l.Controls.Add(this.chkShowRobustFit);
            this.l.Controls.Add(this.btnCallOutliers);
            this.l.Controls.Add(this.panel2);
            this.l.Controls.Add(this.lstData);
            this.l.Controls.Add(this.chkShowLin);
            this.l.Controls.Add(this.btnDeleteCurve);
            this.l.Dock = System.Windows.Forms.DockStyle.Fill;
            this.l.Location = new System.Drawing.Point(0, 0);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(288, 320);
            this.l.TabIndex = 14;
            // 
            // chkShowRobustFit
            // 
            this.chkShowRobustFit.AutoSize = true;
            this.chkShowRobustFit.Checked = true;
            this.chkShowRobustFit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowRobustFit.Location = new System.Drawing.Point(3, 236);
            this.chkShowRobustFit.Name = "chkShowRobustFit";
            this.chkShowRobustFit.Size = new System.Drawing.Size(167, 17);
            this.chkShowRobustFit.TabIndex = 19;
            this.chkShowRobustFit.Text = "Show Robust Fit Below Curve";
            this.chkShowRobustFit.UseVisualStyleBackColor = true;
            this.chkShowRobustFit.CheckedChanged += new System.EventHandler(this.chkShowLin_CheckedChanged);
            // 
            // btnCallOutliers
            // 
            this.btnCallOutliers.Location = new System.Drawing.Point(157, 188);
            this.btnCallOutliers.Name = "btnCallOutliers";
            this.btnCallOutliers.Size = new System.Drawing.Size(122, 23);
            this.btnCallOutliers.TabIndex = 18;
            this.btnCallOutliers.Text = "Call Outliers";
            this.btnCallOutliers.UseVisualStyleBackColor = true;
            this.btnCallOutliers.Click += new System.EventHandler(this.btnCallOutliers_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtEndPoint);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.btnSetBounds);
            this.panel2.Controls.Add(this.txtStartPoint);
            this.panel2.Location = new System.Drawing.Point(2, 250);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(283, 67);
            this.panel2.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(156, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "End Point";
            // 
            // txtEndPoint
            // 
            this.txtEndPoint.Location = new System.Drawing.Point(215, 16);
            this.txtEndPoint.Name = "txtEndPoint";
            this.txtEndPoint.Size = new System.Drawing.Size(66, 20);
            this.txtEndPoint.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Start Point";
            // 
            // btnSetBounds
            // 
            this.btnSetBounds.Location = new System.Drawing.Point(14, 42);
            this.btnSetBounds.Name = "btnSetBounds";
            this.btnSetBounds.Size = new System.Drawing.Size(232, 23);
            this.btnSetBounds.TabIndex = 12;
            this.btnSetBounds.Text = "Fit All Data In Range";
            this.btnSetBounds.UseVisualStyleBackColor = true;
            this.btnSetBounds.Click += new System.EventHandler(this.btnTemp_Click);
            // 
            // txtStartPoint
            // 
            this.txtStartPoint.Location = new System.Drawing.Point(84, 16);
            this.txtStartPoint.Name = "txtStartPoint";
            this.txtStartPoint.Size = new System.Drawing.Size(66, 20);
            this.txtStartPoint.TabIndex = 13;
            // 
            // lstData
            // 
            this.lstData.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lstData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstData.FormattingEnabled = true;
            this.lstData.Location = new System.Drawing.Point(15, 10);
            this.lstData.Name = "lstData";
            this.lstData.Size = new System.Drawing.Size(251, 169);
            this.lstData.TabIndex = 3;
            // 
            // chkShowLin
            // 
            this.chkShowLin.AutoSize = true;
            this.chkShowLin.Checked = true;
            this.chkShowLin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLin.Location = new System.Drawing.Point(3, 217);
            this.chkShowLin.Name = "chkShowLin";
            this.chkShowLin.Size = new System.Drawing.Size(162, 17);
            this.chkShowLin.TabIndex = 11;
            this.chkShowLin.Text = "Show Linear Fit Below Curve";
            this.chkShowLin.UseVisualStyleBackColor = true;
            this.chkShowLin.CheckedChanged += new System.EventHandler(this.chkShowLin_CheckedChanged);
            // 
            // btnDeleteCurve
            // 
            this.btnDeleteCurve.Location = new System.Drawing.Point(0, 188);
            this.btnDeleteCurve.Name = "btnDeleteCurve";
            this.btnDeleteCurve.Size = new System.Drawing.Size(132, 23);
            this.btnDeleteCurve.TabIndex = 10;
            this.btnDeleteCurve.Text = "Delete This Curve";
            this.btnDeleteCurve.UseVisualStyleBackColor = true;
            this.btnDeleteCurve.Click += new System.EventHandler(this.btnDeleteCurve_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.leftsubbottompanel);
            this.panel1.Controls.Add(this.lstGrowthCurves);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 689);
            this.panel1.TabIndex = 14;
            // 
            // leftsubbottompanel
            // 
            this.leftsubbottompanel.Controls.Add(this.chkODMustIncrease);
            this.leftsubbottompanel.Controls.Add(this.chkUsePercent);
            this.leftsubbottompanel.Controls.Add(this.txtMaxODPercent);
            this.leftsubbottompanel.Controls.Add(this.label13);
            this.leftsubbottompanel.Controls.Add(this.btnFitODRange);
            this.leftsubbottompanel.Controls.Add(this.label10);
            this.leftsubbottompanel.Controls.Add(this.txtMaxOD);
            this.leftsubbottompanel.Controls.Add(this.label11);
            this.leftsubbottompanel.Controls.Add(this.txtMinOD);
            this.leftsubbottompanel.Controls.Add(this.label1);
            this.leftsubbottompanel.Controls.Add(this.btnChangeAxis);
            this.leftsubbottompanel.Controls.Add(this.txtMaxRange);
            this.leftsubbottompanel.Controls.Add(this.label2);
            this.leftsubbottompanel.Controls.Add(this.txtMinRange);
            this.leftsubbottompanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.leftsubbottompanel.Location = new System.Drawing.Point(0, 503);
            this.leftsubbottompanel.Name = "leftsubbottompanel";
            this.leftsubbottompanel.Size = new System.Drawing.Size(235, 186);
            this.leftsubbottompanel.TabIndex = 10;
            // 
            // chkODMustIncrease
            // 
            this.chkODMustIncrease.AutoSize = true;
            this.chkODMustIncrease.Location = new System.Drawing.Point(8, 154);
            this.chkODMustIncrease.Name = "chkODMustIncrease";
            this.chkODMustIncrease.Size = new System.Drawing.Size(167, 17);
            this.chkODMustIncrease.TabIndex = 18;
            this.chkODMustIncrease.Text = "Enforce OD Always Increases";
            this.chkODMustIncrease.UseVisualStyleBackColor = true;
            // 
            // chkUsePercent
            // 
            this.chkUsePercent.AutoSize = true;
            this.chkUsePercent.Location = new System.Drawing.Point(8, 131);
            this.chkUsePercent.Name = "chkUsePercent";
            this.chkUsePercent.Size = new System.Drawing.Size(115, 17);
            this.chkUsePercent.TabIndex = 17;
            this.chkUsePercent.Text = "Use Percent Value";
            this.chkUsePercent.UseVisualStyleBackColor = true;
            // 
            // txtMaxODPercent
            // 
            this.txtMaxODPercent.Location = new System.Drawing.Point(164, 76);
            this.txtMaxODPercent.Name = "txtMaxODPercent";
            this.txtMaxODPercent.Size = new System.Drawing.Size(45, 20);
            this.txtMaxODPercent.TabIndex = 15;
            this.txtMaxODPercent.Text = ".9";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(159, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Max OD %";
            // 
            // btnFitODRange
            // 
            this.btnFitODRange.Location = new System.Drawing.Point(8, 102);
            this.btnFitODRange.Name = "btnFitODRange";
            this.btnFitODRange.Size = new System.Drawing.Size(146, 23);
            this.btnFitODRange.TabIndex = 14;
            this.btnFitODRange.Text = "Fit Values in Range";
            this.btnFitODRange.UseVisualStyleBackColor = true;
            this.btnFitODRange.Click += new System.EventHandler(this.btnFitODRange_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Min OD Value";
            // 
            // txtMaxOD
            // 
            this.txtMaxOD.Location = new System.Drawing.Point(89, 76);
            this.txtMaxOD.Name = "txtMaxOD";
            this.txtMaxOD.Size = new System.Drawing.Size(45, 20);
            this.txtMaxOD.TabIndex = 11;
            this.txtMaxOD.Text = ".18";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(84, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Max OD ";
            // 
            // txtMinOD
            // 
            this.txtMinOD.Location = new System.Drawing.Point(8, 76);
            this.txtMinOD.Name = "txtMinOD";
            this.txtMinOD.Size = new System.Drawing.Size(45, 20);
            this.txtMinOD.TabIndex = 10;
            this.txtMinOD.Text = "0.02";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Min Value";
            // 
            // btnChangeAxis
            // 
            this.btnChangeAxis.Location = new System.Drawing.Point(8, 28);
            this.btnChangeAxis.Name = "btnChangeAxis";
            this.btnChangeAxis.Size = new System.Drawing.Size(146, 23);
            this.btnChangeAxis.TabIndex = 9;
            this.btnChangeAxis.Text = "Change Doubling Y Axis";
            this.btnChangeAxis.UseVisualStyleBackColor = true;
            this.btnChangeAxis.Click += new System.EventHandler(this.btnChangeAxis_Click);
            // 
            // txtMaxRange
            // 
            this.txtMaxRange.Location = new System.Drawing.Point(180, 3);
            this.txtMaxRange.Name = "txtMaxRange";
            this.txtMaxRange.Size = new System.Drawing.Size(45, 20);
            this.txtMaxRange.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Max Value";
            // 
            // txtMinRange
            // 
            this.txtMinRange.Location = new System.Drawing.Point(65, 2);
            this.txtMinRange.Name = "txtMinRange";
            this.txtMinRange.Size = new System.Drawing.Size(45, 20);
            this.txtMinRange.TabIndex = 5;
            // 
            // lstGrowthCurves
            // 
            this.lstGrowthCurves.FormattingEnabled = true;
            this.lstGrowthCurves.Location = new System.Drawing.Point(0, -1);
            this.lstGrowthCurves.Name = "lstGrowthCurves";
            this.lstGrowthCurves.Size = new System.Drawing.Size(235, 498);
            this.lstGrowthCurves.TabIndex = 0;
            this.lstGrowthCurves.SelectedIndexChanged += new System.EventHandler(this.lstGrowthCurves_SelectedIndexChanged);
            this.lstGrowthCurves.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstGrowthCurves_KeyDown);
            // 
            // tabGroup
            // 
            this.tabGroup.BackColor = System.Drawing.Color.Transparent;
            this.tabGroup.Controls.Add(this.label22);
            this.tabGroup.Controls.Add(this.rbtnGMRMSEDif);
            this.tabGroup.Controls.Add(this.btnFitGroup);
            this.tabGroup.Controls.Add(this.btnCopyGroupAssignments);
            this.tabGroup.Controls.Add(this.label5);
            this.tabGroup.Controls.Add(this.rbtnGMResidualQQPlot);
            this.tabGroup.Controls.Add(this.rbtnGMTimevOffSet);
            this.tabGroup.Controls.Add(this.rbtnGMTimeVOD);
            this.tabGroup.Controls.Add(this.rbtnGMDoublings);
            this.tabGroup.Controls.Add(this.rbtnGMResiduals);
            this.tabGroup.Controls.Add(this.lblBlah);
            this.tabGroup.Controls.Add(this.plotGroupModeling);
            this.tabGroup.Controls.Add(this.lstGroupModelGroups);
            this.tabGroup.Controls.Add(this.selectPlatesForGroupModel);
            this.tabGroup.Location = new System.Drawing.Point(4, 22);
            this.tabGroup.Name = "tabGroup";
            this.tabGroup.Size = new System.Drawing.Size(1165, 695);
            this.tabGroup.TabIndex = 8;
            this.tabGroup.Text = "Grouped Modeling";
            // 
            // rbtnGMRMSEDif
            // 
            this.rbtnGMRMSEDif.AutoSize = true;
            this.rbtnGMRMSEDif.Location = new System.Drawing.Point(177, 564);
            this.rbtnGMRMSEDif.Name = "rbtnGMRMSEDif";
            this.rbtnGMRMSEDif.Size = new System.Drawing.Size(182, 17);
            this.rbtnGMRMSEDif.TabIndex = 117;
            this.rbtnGMRMSEDif.Text = "Plot Grouped RMSE - Exp RMSE";
            this.rbtnGMRMSEDif.UseVisualStyleBackColor = true;
            this.rbtnGMRMSEDif.CheckedChanged += new System.EventHandler(this.rbtnGM_CheckedChanged);
            // 
            // btnFitGroup
            // 
            this.btnFitGroup.Location = new System.Drawing.Point(51, 587);
            this.btnFitGroup.Name = "btnFitGroup";
            this.btnFitGroup.Size = new System.Drawing.Size(239, 47);
            this.btnFitGroup.TabIndex = 116;
            this.btnFitGroup.Text = "Perform Joint Fitting";
            this.btnFitGroup.UseVisualStyleBackColor = true;
            this.btnFitGroup.Click += new System.EventHandler(this.btnFitGroup_Click);
            // 
            // btnCopyGroupAssignments
            // 
            this.btnCopyGroupAssignments.Location = new System.Drawing.Point(51, 640);
            this.btnCopyGroupAssignments.Name = "btnCopyGroupAssignments";
            this.btnCopyGroupAssignments.Size = new System.Drawing.Size(239, 47);
            this.btnCopyGroupAssignments.TabIndex = 115;
            this.btnCopyGroupAssignments.Text = "Copy Groups From Other Tab";
            this.btnCopyGroupAssignments.UseVisualStyleBackColor = true;
            this.btnCopyGroupAssignments.Click += new System.EventHandler(this.btnCopyGroupAssignments_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 352);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 26);
            this.label5.TabIndex = 114;
            this.label5.Text = "Select Treatment Group to\r\nAssign by Clicking";
            // 
            // rbtnGMResidualQQPlot
            // 
            this.rbtnGMResidualQQPlot.AutoSize = true;
            this.rbtnGMResidualQQPlot.Checked = true;
            this.rbtnGMResidualQQPlot.Location = new System.Drawing.Point(42, 564);
            this.rbtnGMResidualQQPlot.Name = "rbtnGMResidualQQPlot";
            this.rbtnGMResidualQQPlot.Size = new System.Drawing.Size(111, 17);
            this.rbtnGMResidualQQPlot.TabIndex = 113;
            this.rbtnGMResidualQQPlot.TabStop = true;
            this.rbtnGMResidualQQPlot.Text = "QQ Plot Residuals";
            this.rbtnGMResidualQQPlot.UseVisualStyleBackColor = true;
            this.rbtnGMResidualQQPlot.CheckedChanged += new System.EventHandler(this.rbtnGM_CheckedChanged);
            // 
            // rbtnGMTimevOffSet
            // 
            this.rbtnGMTimevOffSet.AutoSize = true;
            this.rbtnGMTimevOffSet.Location = new System.Drawing.Point(177, 541);
            this.rbtnGMTimevOffSet.Name = "rbtnGMTimevOffSet";
            this.rbtnGMTimevOffSet.Size = new System.Drawing.Size(114, 17);
            this.rbtnGMTimevOffSet.TabIndex = 112;
            this.rbtnGMTimevOffSet.Text = "Plot Time vs Offset";
            this.rbtnGMTimevOffSet.UseVisualStyleBackColor = true;
            this.rbtnGMTimevOffSet.CheckedChanged += new System.EventHandler(this.rbtnGM_CheckedChanged);
            // 
            // rbtnGMTimeVOD
            // 
            this.rbtnGMTimeVOD.AutoSize = true;
            this.rbtnGMTimeVOD.Location = new System.Drawing.Point(42, 541);
            this.rbtnGMTimeVOD.Name = "rbtnGMTimeVOD";
            this.rbtnGMTimeVOD.Size = new System.Drawing.Size(102, 17);
            this.rbtnGMTimeVOD.TabIndex = 111;
            this.rbtnGMTimeVOD.Text = "Plot Time vs OD";
            this.rbtnGMTimeVOD.UseVisualStyleBackColor = true;
            this.rbtnGMTimeVOD.CheckedChanged += new System.EventHandler(this.rbtnGM_CheckedChanged);
            // 
            // rbtnGMDoublings
            // 
            this.rbtnGMDoublings.AutoSize = true;
            this.rbtnGMDoublings.Location = new System.Drawing.Point(177, 518);
            this.rbtnGMDoublings.Name = "rbtnGMDoublings";
            this.rbtnGMDoublings.Size = new System.Drawing.Size(114, 17);
            this.rbtnGMDoublings.TabIndex = 110;
            this.rbtnGMDoublings.Text = "Plot Doubling Time";
            this.rbtnGMDoublings.UseVisualStyleBackColor = true;
            this.rbtnGMDoublings.CheckedChanged += new System.EventHandler(this.rbtnGM_CheckedChanged);
            // 
            // rbtnGMResiduals
            // 
            this.rbtnGMResiduals.AutoSize = true;
            this.rbtnGMResiduals.Location = new System.Drawing.Point(42, 518);
            this.rbtnGMResiduals.Name = "rbtnGMResiduals";
            this.rbtnGMResiduals.Size = new System.Drawing.Size(121, 17);
            this.rbtnGMResiduals.TabIndex = 109;
            this.rbtnGMResiduals.Text = "Plot Fitted Residuals";
            this.rbtnGMResiduals.UseVisualStyleBackColor = true;
            this.rbtnGMResiduals.CheckedChanged += new System.EventHandler(this.rbtnGM_CheckedChanged);
            // 
            // lblBlah
            // 
            this.lblBlah.AutoSize = true;
            this.lblBlah.Location = new System.Drawing.Point(71, 54);
            this.lblBlah.Name = "lblBlah";
            this.lblBlah.Size = new System.Drawing.Size(191, 13);
            this.lblBlah.TabIndex = 92;
            this.lblBlah.Text = "All Wells in Any Group Will Be Modeled";
            // 
            // plotGroupModeling
            // 
            this.plotGroupModeling.Location = new System.Drawing.Point(392, 0);
            this.plotGroupModeling.Name = "plotGroupModeling";
            this.plotGroupModeling.ScrollGrace = 0D;
            this.plotGroupModeling.ScrollMaxX = 0D;
            this.plotGroupModeling.ScrollMaxY = 0D;
            this.plotGroupModeling.ScrollMaxY2 = 0D;
            this.plotGroupModeling.ScrollMinX = 0D;
            this.plotGroupModeling.ScrollMinY = 0D;
            this.plotGroupModeling.ScrollMinY2 = 0D;
            this.plotGroupModeling.Size = new System.Drawing.Size(773, 695);
            this.plotGroupModeling.TabIndex = 91;
            // 
            // lstGroupModelGroups
            // 
            this.lstGroupModelGroups.FormattingEnabled = true;
            this.lstGroupModelGroups.Items.AddRange(new object[] {
            "Unassigned",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.lstGroupModelGroups.Location = new System.Drawing.Point(68, 381);
            this.lstGroupModelGroups.Name = "lstGroupModelGroups";
            this.lstGroupModelGroups.Size = new System.Drawing.Size(211, 121);
            this.lstGroupModelGroups.TabIndex = 90;
            this.lstGroupModelGroups.SelectedIndexChanged += new System.EventHandler(this.lstGroupModelGroups_SelectedIndexChanged);
            // 
            // openFileWithNumberedHoursToolStripMenuItem
            // 
            this.openFileWithNumberedHoursToolStripMenuItem.Name = "openFileWithNumberedHoursToolStripMenuItem";
            this.openFileWithNumberedHoursToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.openFileWithNumberedHoursToolStripMenuItem.Text = "Open File with Numbered Hours";
            this.openFileWithNumberedHoursToolStripMenuItem.Click += new System.EventHandler(this.openFileWithNumberedHoursToolStripMenuItem_Click);
            // 
            // plateMap
            // 
            this.plateMap.BackColor = System.Drawing.SystemColors.Control;
            this.plateMap.Location = new System.Drawing.Point(8, 23);
            this.plateMap.Name = "plateMap";
            this.plateMap.RoomForText = 25;
            this.plateMap.ShowValues = true;
            this.plateMap.Size = new System.Drawing.Size(646, 456);
            this.plateMap.TabIndex = 20;
            // 
            // toDeletePlateMap
            // 
            this.toDeletePlateMap.BackColor = System.Drawing.Color.White;
            this.toDeletePlateMap.CurGroupToSelect = 1;
            this.toDeletePlateMap.Location = new System.Drawing.Point(452, 27);
            this.toDeletePlateMap.Name = "toDeletePlateMap";
            this.toDeletePlateMap.Size = new System.Drawing.Size(524, 397);
            this.toDeletePlateMap.TabIndex = 13;
            // 
            // selectablePlateMap1
            // 
            this.selectablePlateMap1.CurGroupToSelect = 1;
            this.selectablePlateMap1.Location = new System.Drawing.Point(3, 5);
            this.selectablePlateMap1.Name = "selectablePlateMap1";
            this.selectablePlateMap1.Size = new System.Drawing.Size(278, 238);
            this.selectablePlateMap1.TabIndex = 77;
            this.selectablePlateMap1.GroupsChanged += new Fit_Growth_Curves.SelectablePlateMap.ChangedEventHandler(this.selectablePlateMap1_GroupsChanged_1);
            // 
            // scaleBarSensitivity
            // 
            this.scaleBarSensitivity.BackColor = System.Drawing.SystemColors.Control;
            this.scaleBarSensitivity.BinNumber = 10;
            this.scaleBarSensitivity.Location = new System.Drawing.Point(1021, 16);
            this.scaleBarSensitivity.Name = "scaleBarSensitivity";
            this.scaleBarSensitivity.RoomForText = 55;
            this.scaleBarSensitivity.ShowValues = true;
            this.scaleBarSensitivity.Size = new System.Drawing.Size(118, 541);
            this.scaleBarSensitivity.TabIndex = 7;
            // 
            // sensitivityArray
            // 
            this.sensitivityArray.BackColor = System.Drawing.SystemColors.Control;
            this.sensitivityArray.Location = new System.Drawing.Point(180, 30);
            this.sensitivityArray.Name = "sensitivityArray";
            this.sensitivityArray.RoomForText = 40;
            this.sensitivityArray.ShowValues = true;
            this.sensitivityArray.Size = new System.Drawing.Size(825, 626);
            this.sensitivityArray.TabIndex = 1;
            // 
            // selectPlatesForGroupModel
            // 
            this.selectPlatesForGroupModel.CurGroupToSelect = 1;
            this.selectPlatesForGroupModel.Location = new System.Drawing.Point(42, 154);
            this.selectPlatesForGroupModel.Name = "selectPlatesForGroupModel";
            this.selectPlatesForGroupModel.Size = new System.Drawing.Size(294, 181);
            this.selectPlatesForGroupModel.TabIndex = 78;
            this.selectPlatesForGroupModel.GroupsChanged += new Fit_Growth_Curves.SelectablePlateMap.ChangedEventHandler(this.selectPlatesForGroupModel_GroupsChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(9, 19);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(311, 90);
            this.label22.TabIndex = 118;
            this.label22.Text = "Warning, this tab should only be used by \r\nadvanced users.  The more complicated\r" +
    "\nmodels it implements can usually avoided\r\nby altering the instrument settings o" +
    "r replacing \r\nthe lamp.";
            this.label22.Click += new System.EventHandler(this.label22_Click);
            // 
            // CurveFitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1173, 745);
            this.Controls.Add(this.tabMainTab);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CurveFitter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Curve Fitter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabSensitivityAnalysis.ResumeLayout(false);
            this.tabSensitivityAnalysis.PerformLayout();
            this.tabPlotGraphic.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabBlankRemoval.ResumeLayout(false);
            this.tabBlankRemoval.PerformLayout();
            this.tabRobo.ResumeLayout(false);
            this.tabRobo.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblRawData)).EndInit();
            this.tabPageChangeFitted.ResumeLayout(false);
            this.tabPageChangeFitted.PerformLayout();
            this.tabMainTab.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.topRightPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.l.ResumeLayout(false);
            this.l.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.leftsubbottompanel.ResumeLayout(false);
            this.leftsubbottompanel.PerformLayout();
            this.tabGroup.ResumeLayout(false);
            this.tabGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenFile;
        private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem importPreviousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDirectoryWithExcelDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPlateKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alternativeExportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportRawDataForMatlabToolStripMenuItem;
        private System.Windows.Forms.TabPage tabSensitivityAnalysis;
        private System.Windows.Forms.RadioButton rbtnSensBiasLinear;
        private MatrixArrayPlot.ScaleBar scaleBarSensitivity;
        private System.Windows.Forms.Label lblYAxis;
        private System.Windows.Forms.RadioButton rbtnSensBiasError;
        private System.Windows.Forms.RadioButton rbtnSensStartEndLog;
        private System.Windows.Forms.RadioButton rbtnSensRange;
        private System.Windows.Forms.Label lblXAxis;
        private MatrixArrayPlot.ArrayPlot sensitivityArray;
        private System.Windows.Forms.ListBox lstGrowthRatesSensitivity;
        private System.Windows.Forms.TabPage tabPlotGraphic;
        private System.Windows.Forms.TabPage tabBlankRemoval;
        private System.Windows.Forms.Button btnSubtractWrittenBlank;
        private System.Windows.Forms.TextBox txtBlankValue;
        private System.Windows.Forms.Button btnDeleteLikelyBlanks;
        private System.Windows.Forms.Label lblDeletePlate;
        private System.Windows.Forms.Button btnDeleteAvg3asBlank;
        private System.Windows.Forms.Button btnDelete2ndPoint;
        private System.Windows.Forms.Button btnDeleteFirstBlank;
        private SelectablePlateMap toDeletePlateMap;
        private System.Windows.Forms.TabPage tabRobo;
        private System.Windows.Forms.RadioButton rbtnRMSE;
        private System.Windows.Forms.Label lblRobo;
        private ZedGraph.ZedGraphControl graphHistogram;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox lstTimePoints;
        private System.Windows.Forms.RadioButton rbtnTimePoint;
        private System.Windows.Forms.RadioButton rbtnDoubleTime;
        private System.Windows.Forms.RadioButton rbtnIOD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbtnRS;
        private System.Windows.Forms.RadioButton rbtnGrowthRate;
        private System.Windows.Forms.RadioButton rbtnMaxOD;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView tblRawData;
        private System.Windows.Forms.TabPage tabPageChangeFitted;
        private System.Windows.Forms.CheckBox chkShowFittedPick;
        private ZedGraph.ZedGraphControl ChartPickData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstGrowthCurvesMirror;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstDataMirror;
        private System.Windows.Forms.TabControl tabMainTab;
        private MatrixArrayPlot.PlateHeatMap plateMap;
        private System.Windows.Forms.RadioButton rbtnOffSet;
        private System.Windows.Forms.RadioButton rbtnIntercept;
        private System.Windows.Forms.TabPage tabPageMain;
       
        private System.Windows.Forms.Panel topRightPanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel l;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEndPoint;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSetBounds;
        private System.Windows.Forms.TextBox txtStartPoint;
        public System.Windows.Forms.ListBox lstData;
        private System.Windows.Forms.CheckBox chkShowLin;
        private System.Windows.Forms.Button btnDeleteCurve;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel leftsubbottompanel;
        private System.Windows.Forms.CheckBox chkODMustIncrease;
        private System.Windows.Forms.CheckBox chkUsePercent;
        private System.Windows.Forms.TextBox txtMaxODPercent;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnFitODRange;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMaxOD;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMinOD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChangeAxis;
        private System.Windows.Forms.TextBox txtMaxRange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMinRange;
        private System.Windows.Forms.ListBox lstGrowthCurves;
        private ZedGraph.ZedGraphControl ChartN;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ZedGraph.ZedGraphControl ChartSlopeN;
        private ZedGraph.ZedGraphControl ChartStandard;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btnMakeEvoGroups;
        private System.Windows.Forms.Button btnClearTreatments;
        private System.Windows.Forms.RadioButton rbtnPlotLinearResiduals;
        private System.Windows.Forms.RadioButton rbtnMaxMinusEnd;
        private System.Windows.Forms.RadioButton rbtnFittedResiduals;
        private System.Windows.Forms.RadioButton rbtnPlotAllResiduals;
        private System.Windows.Forms.Button btnRowGroups;
        private System.Windows.Forms.RadioButton rbtnPlotSlope;
        private System.Windows.Forms.RadioButton rbtnPlotLagTime;
        private System.Windows.Forms.RadioButton rbtnPlotLastOD;
        private System.Windows.Forms.RadioButton rbtnTimeToOD;
        private System.Windows.Forms.CheckBox chkTreatShowLog;
        private System.Windows.Forms.CheckBox chkTreatLegend;
        private System.Windows.Forms.RadioButton rbtnTreatNumPoints;
        private System.Windows.Forms.RadioButton rbtnTreatTimevOD;
        private System.Windows.Forms.RadioButton rbtnTreatDoublingTime;
        private System.Windows.Forms.RadioButton rbtnTreatInitialOD;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RadioButton rbtnTreatRSq;
        private System.Windows.Forms.RadioButton rbtnTreatGrowthRate;
        private System.Windows.Forms.RadioButton rbtnTreatMaxOd;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtTreatment6;
        private System.Windows.Forms.TextBox txtTreatment5;
        private System.Windows.Forms.TextBox txtTreatment4;
        private System.Windows.Forms.TextBox txtTreatment3;
        private System.Windows.Forms.TextBox txtTreatment2;
        private System.Windows.Forms.TextBox txtTreatment1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ListBox lstTreatmentSelection;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private SelectablePlateMap selectablePlateMap1;
        private ZedGraph.ZedGraphControl plotTreatments;
        private System.Windows.Forms.TabPage tabGroup;
        private System.Windows.Forms.RadioButton rbtnGroupOffSetMinusStart;
        private System.Windows.Forms.RadioButton rbtnOffSetGR;
        private System.Windows.Forms.RadioButton rbtnPlateOffSetIDMinusStart;
        private System.Windows.Forms.Label lblBlah;
        private ZedGraph.ZedGraphControl plotGroupModeling;
        private System.Windows.Forms.ListBox lstGroupModelGroups;
        private SelectablePlateMap selectPlatesForGroupModel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtnGMResidualQQPlot;
        private System.Windows.Forms.RadioButton rbtnGMTimevOffSet;
        private System.Windows.Forms.RadioButton rbtnGMTimeVOD;
        private System.Windows.Forms.RadioButton rbtnGMDoublings;
        private System.Windows.Forms.RadioButton rbtnGMResiduals;
        private System.Windows.Forms.Button btnCopyGroupAssignments;
        private System.Windows.Forms.Button btnFitGroup;
        private System.Windows.Forms.RadioButton rbtnGMRMSEDif;
        private System.Windows.Forms.RadioButton rbtnGroupGrowthRate;
        private System.Windows.Forms.RadioButton rbtnMakeQQPlot;
        private System.Windows.Forms.RadioButton rbtnInitialPopvGrowthRate;
        private System.Windows.Forms.RadioButton rbtnMaxvGrowthRate;
        private System.Windows.Forms.RadioButton rbtnEndODvMax;
        private System.Windows.Forms.Button btnCallOutliers;
        private System.Windows.Forms.RadioButton rbtnMixtureModelGR;
        private System.Windows.Forms.RadioButton rbtnRobustGRvEXPGR;
        private System.Windows.Forms.RadioButton rbtnOutlierNumber;
        private System.Windows.Forms.RadioButton rbtnMixtureGrowthRate;
        private System.Windows.Forms.CheckBox chkShowRobustFit;
        private System.Windows.Forms.RadioButton rbtnGroupsLinFit;
        private System.Windows.Forms.RadioButton rbtnGroupsOffSetGrowthRate;
        private System.Windows.Forms.RadioButton rbtnGroupsQQLinear;
        private System.Windows.Forms.RadioButton rbtnGroupsDeviance;
        private System.Windows.Forms.RadioButton rbtnGroupDifFromCenter;
        private System.Windows.Forms.RadioButton rbtnGroupsSlopeDoublingsLogOD;
        private System.Windows.Forms.RadioButton rbtnGroupQuadraticDoubling;
        private System.Windows.Forms.RadioButton rbtnGroupsQuadraticResiduals;
        private System.Windows.Forms.RadioButton rbtnPlatesLogisticR2;
        private System.Windows.Forms.RadioButton rbtnPlateQuadR2;
        private System.Windows.Forms.RadioButton rbtnPlotQuadraticRate;
        private System.Windows.Forms.RadioButton rbtnPlatePlotLogistic15;
        private System.Windows.Forms.ToolStripMenuItem launchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem open16MinuteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileWithNumberedHoursToolStripMenuItem;
        private System.Windows.Forms.Label label22;
    }
}

