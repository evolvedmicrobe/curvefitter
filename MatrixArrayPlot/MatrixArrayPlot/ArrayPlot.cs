using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace MatrixArrayPlot
{
    public delegate void newGridLoadedEvent(double[,] newGrid);
    public partial class ArrayPlot : UserControl
    {
        public static bool isReal(double x)
        {
            return !(Double.IsInfinity(x) || Double.IsNaN(x) || Double.IsNegativeInfinity(x) || Double.IsPositiveInfinity(x));
        }
        public event newGridLoadedEvent NewGridLoaded;
        private void OnNewGridLoad()
        {
            if(NewGridLoaded!=null)
        {
            NewGridLoaded(this.Matrix.Array);
        }

        }
        private Font fontToUse;
        public Font FontForLabels;
        public bool DrawRowLabels = true;
        public bool DrawColLabels = true;
        public void SwitchToRainbow()
        {
            CF = new ColorFunction(ColorMaps.rainbowScheme);
            RecreateImage();
            OnNewGridLoad();
        }
        public ArrayPlot()
        {
            InitializeComponent();
            fontToUse = this.Font;
            FontForLabels = this.fontToUse;
            pRoomForText = 40;
           
            this.Matrix = new MatrixForPlotting();
           RecreateImage();
            this.SizeChanged += new EventHandler(ArrayPlot_SizeChanged);
        }
        public bool ShowValues
        {
            get { return pShowValues; }
            set { pShowValues = value; RecreateImage(); }
        }

        private bool pShowValues = false;
        void ArrayPlot_SizeChanged(object sender, EventArgs e)
        {
            RecreateImage();
           /// base.OnSizeChanged(e);
        }
        protected MatrixForPlotting Matrix;
        protected ColorFunction CF = new ColorFunction(ColorMaps.BlueRedScheme);
        public Color GetColor(double value, double min, double max)
        {
            return CF(value, min, max);

        }
        public virtual void SetMatrixForPlotting(double[,] newMat,string[] RowNames=null,string[] Colnames=null)
        {

            this.Matrix.Array = newMat;
            this.Matrix.RowNames = RowNames;
            this.Matrix.ColNames = Colnames;
            //DebugMatrix(newMat);
            RecreateImage();
            OnNewGridLoad();
        }
        private void DebugMatrix(double[,] newMat)
        {
            if (newMat.GetLength(1) > 3)
            {
                StreamWriter SW = new StreamWriter(@"C:\Users\Nigel\Desktop\test.csv");
                for (int i = 0; i < newMat.GetLength(0); i++)
                {
                    for (int j = 0; j < newMat.GetLength(1); j++)
                    {
                        SW.Write(newMat[i, j].ToString() + ",");
                    }
                    SW.Write("\n");
                }
                SW.Close();
            }
        }
        public void SetColNames(string[] names)
        {
            this.Matrix.ColNames = names;
            RecreateImage();
        }
        public void SetRowNames(string[] names)
        {
            this.Matrix.RowNames = names;
            RecreateImage();
        }
        protected System.Drawing.Bitmap TheColoredSquaresBM;
        protected int SideLength,RowSideLength,ColSideLength;
        protected int pRoomForText=20;
        public int RoomForText { get{return pRoomForText;}
            set { pRoomForText = value; RecreateImage(); } }
        protected static Color[] ColorAssignments;
        protected int[] RowPositions, ColumnPositions;
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphicsObj = e.Graphics;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphicsObj.DrawImage(TheColoredSquaresBM,0, 0,this.Width, this.Height);
            base.OnPaint(e);
        } 
        protected void MakeRectanglePostions()
        {
            int totalHeight = this.Height;
            int totalWidth = this.Width;
            SideLength = (int)((totalHeight - 2 - RoomForText) / Convert.ToDouble(this.Matrix.ColCount));
            ColSideLength = (int)((totalWidth - 2 - RoomForText) / Convert.ToDouble(this.Matrix.ColCount));
            RowSideLength = (int)((totalHeight - 2 - RoomForText) / Convert.ToDouble(this.Matrix.RowCount));
            
           //Add two to make it the 
            RowPositions = new int[Matrix.RowCount+1];
            ColumnPositions = new int[Matrix.ColCount + 1];
            RowPositions[0] = ColumnPositions[0] = RoomForText;
            for (int i = 1; i < Matrix.RowCount+1; i++)
            {
                RowPositions[i]= RowPositions[0] + RowSideLength * i;
            }
            for (int i = 1; i < Matrix.ColCount + 1; i++)
            {
                ColumnPositions[i]=ColumnPositions[0]+ ColSideLength * i;
            }
        }
        public Color LabelTextColor=Color.White;
        internal virtual void RecreateImage()
        {
            if (TheColoredSquaresBM != null)
            {
                TheColoredSquaresBM.Dispose();
                TheColoredSquaresBM = null;
            }
            TheColoredSquaresBM = new System.Drawing.Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            MakeRectanglePostions();
            Graphics g = Graphics.FromImage(TheColoredSquaresBM);
            g.Clear(System.Drawing.Color.White);
            SolidBrush BlackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush labelBrush = new SolidBrush(LabelTextColor);
            Pen P = new Pen(Color.Black, 2);            
            try
            {
                if (DrawColLabels)
                {
                    string[] Labels = Matrix.ColNames;
                    if (Labels == null)
                    {
                        Labels = new string[Matrix.ColCount];
                        for (int i = 1; i <= Matrix.ColCount; i++)
                        { Labels[i - 1] = i.ToString(); }

                    }

                    for (int i = 1; i <= Matrix.ColCount; i++)
                    {

                        g.DrawString(Labels[i - 1], this.Font, BlackBrush, ((float)(RoomForText + (ColSideLength * .25) + ColSideLength * (i - 1))), 0);
                    }
                }
                if (DrawRowLabels)
                {
                    string[] Rows = Matrix.RowNames;
                    if (Rows == null)
                    {
                        Rows = new string[Matrix.RowCount];
                        for (int i = 1; i <= Matrix.RowCount; i++)
                        { Rows[i - 1] = i.ToString(); }
                    }
                    for (int i = 0; i < Matrix.RowCount; i++)
                    {
                        g.DrawString(Rows[i].ToString(), this.Font, BlackBrush, 0, ((float)(RoomForText + (RowSideLength * .35) + RowSideLength * i)));
                    }
                }
                int NumWells = Matrix.TotalElements;
                int NumCols=Matrix.ColCount;
                
                int[,] TextPos = new int[NumWells, 2];
                Rectangle[] PlateWells = new Rectangle[NumWells];
                double min=Matrix.MinValue;
                double max=Matrix.MaxValue;
                double[] DataToSquare = new double[NumWells];
                for (int i = 0; i < NumWells; i++)
                {
                    
                    int ColPos = i % NumCols;
                    int rowPos = Convert.ToInt32((i / NumCols));
                    double arrayValue=Matrix.Array[rowPos,ColPos];
                    DataToSquare[i] = arrayValue;
                    Color col=GetColor(arrayValue,min,max);
                    SolidBrush toPaintWith = new SolidBrush(col);
                    int RecYPos = RowPositions[rowPos];
                    int RecXPos = ColumnPositions[ColPos];
                    TextPos[i, 0] = (int)(RecXPos + ColSideLength * .15);
                    TextPos[i, 1] = (int)(RecYPos + (RowSideLength / 2) - (this.Font.Height / 2));
                    PlateWells[i] = new Rectangle(RecXPos, RecYPos, ColSideLength, RowSideLength);
                    g.FillRectangle(toPaintWith, PlateWells[i]);
                    toPaintWith.Dispose();
                }
                g.DrawRectangles(P, PlateWells);
                if (pShowValues)
                {
                    for (int i = 0; i < NumWells; i++)
                    {
                        double val=DataToSquare[i];
                        if (ArrayPlot.isReal(val))
                        {
                            string toWrite = DataToSquare[i].ToString(LabelFormat);
                            g.DrawString(toWrite, FontForLabels, labelBrush, TextPos[i, 0], TextPos[i, 1]);
                        }
                    }
                }
               
            }
            finally { g.Dispose(); BlackBrush.Dispose(); P.Dispose(); labelBrush.Dispose(); whiteBrush.Dispose(); this.Refresh(); }
        }
        protected string LabelFormat = "g3";
        protected class MatrixForPlotting
        {
          
            private double[,] pArray;
            public double[,] Array
            {
                get { return pArray; }
                set
                {
                    this.pArray = value;
                    this.pColNames = null;
                    this.pRowNames = null;
                }
            }
            private string[] pRowNames;
            private string[] pColNames;
            public string[] RowNames
            {
                get { return pRowNames; }
                set
                {
                    if (value!=null && value.Length != RowCount)
                    {
                        throw new Exception("Length of names don't match");
                    }
                    pRowNames = value;
                }
            }
            public string[] ColNames
            {
                get { return pColNames; }
                set
                {
                    if (value !=null &&value.Length != ColCount)
                    {
                        throw new Exception("Length of names don't match");
                    }
                    pColNames = value;
                }
            }
            public int RowCount { get { return pArray.GetLength(0); } }
            public int ColCount { get { return pArray.GetLength(1); } }
            public double MaxValue
            {
                get
                {
                    var t = pArray.Cast<double>();
                    return (from x in t where ArrayPlot.isReal(x) select x).Max();
                }
            }
            public double MinValue
            {
                get
                {
                    var t= pArray.Cast<double>();
                    var ll=(from x in t where ArrayPlot.isReal(x) & x!=0.0 select x);
                    if(ll.Count()==0)
                        return 0.0;
                    else
                        return ll.Min();
                }
            }
            public int TotalElements { get { return this.RowCount * this.ColCount; } }
            public MatrixForPlotting()
            {
                pArray = new double[12, 12];
                Random r = new Random();
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        pArray[i, j] = r.NextDouble();
                    }
                }
            }

        }
    }
}
