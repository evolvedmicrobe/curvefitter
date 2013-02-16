using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrowthCurveLibrary;

namespace MatrixArrayPlot
{
    public partial class PlateHeatMap : ArrayPlot
    {
        public delegate double GrowthCurveDoubleValueGetter(GrowthCurve GC);
        public enum PlateType {WELL48,WELL96,None};
        public PlateHeatMap() :base()
        {
            this.LabelFormat = "n4";
            this.RoomForText = 25;
            this.LabelTextColor = Color.Black;
            InitializeComponent();
            this.CF = new ColorFunction(ColorMaps.SpringColorFn);
            this.Matrix = new MatrixForPlotting();
            this.PT = pt96;
            double[,] dataToPlot = new double[PT.NumRows, PT.NumCols];
            SetMatrixForPlotting(dataToPlot, PT.RowNames, PT.ColNames);
            this.ShowValues = true;            
        }
        private class ToPlot
        {
            public string Name; public double Value;
        }
        /// <summary>
        /// Should be dynamic object with properties name and value
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public void SetValuesDynamic(dynamic list)
        {
            try
            {
                List<string> Names = new List<string>();
                List<ToPlot> data = new List<ToPlot>();
                foreach (dynamic q in list)
                {
                    var v = new ToPlot{ Name = q.name, Value = q.value };
                    data.Add(v);
                    Names.Add(q.name);
                }
                AssignPlateType(Names);

                double[,] dataToPlot = new double[PT.NumRows, PT.NumCols];
                var cleanedData = from x in data where SimpleFunctions.IsARealNumber(x.Value) & x.Value != 0 select x;
                double max = cleanedData.Max((x) => x.Value);
                double min = cleanedData.Min((x) => x.Value);
                foreach (var d in cleanedData)
                {
                    int index = -1;
                    if (PT.CellNameToInts.TryGetValue(d.Name, out index))
                    {
                        int row = PT.RowColArray[index, 0];
                        int col = PT.RowColArray[index, 1];
                        dataToPlot[row, col] = d.Value;
                    }
                }
                this.SetMatrixForPlotting(dataToPlot, PT.RowNames, PT.ColNames);
            }
            catch (Exception thrown)
            {
                Console.WriteLine("Could not Make Plot\n" + thrown.Message);
            }
        }
        public PlateType AssignPlateType(IEnumerable<string> Names)
        {
            pcurPlateType = PlateType.None;
            this.curGCC = null;
            var match96 = from x in Names where pt96.CellNameToInts.ContainsKey(x) & !pt48.CellNameToInts.ContainsKey(x) select x;
            if (match96.Count() == 0)
            {
                var match48 = from x in Names where pt48.CellNameToInts.ContainsKey(x) select x;
                if (match48.Count() > 0)
                {
                    PT = pt48;
                    this.FontForLabels = new System.Drawing.Font(this.FontForLabels.FontFamily, 12);
                    pcurPlateType = PlateType.WELL48;
                    return PlateType.WELL48;
                }
                else
                {
                    pcurPlateType = PlateType.None;
                    return PlateType.None;
                }
            }
            else if (Names.Contains("A1"))
            {
                PT = pt96;
                this.FontForLabels = new System.Drawing.Font(this.FontForLabels.FontFamily, 8);
                pcurPlateType = PlateType.WELL96;
                return PlateType.WELL96;
            }
            else
            {
                pcurPlateType = PlateType.None;
                return PlateType.None;
            }
        }
        public void SetLabeFormat(string format)
        {
            this.LabelFormat = format;
        }
        private double SafeGet(GrowthCurveDoubleValueGetter dataFunction,GrowthCurve gc)
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
        public void SetValue(GrowthCurveDoubleValueGetter dataFunction)
        {
            if (pcurPlateType != PlateType.None)
            {
                double[,] dataToPlot = new double[PT.NumRows, PT.NumCols];
                var data = from x in curGCC select new { Name = x.DataSetName, Value = SafeGet(dataFunction,x) };
                var cleanedData = (from x in data where SimpleFunctions.IsARealNumber(x.Value) & x.Value!=0 select x).ToList();
               
                double max = cleanedData.Max((x) => x.Value);
                double min = cleanedData.Min((x) => x.Value);
               
                foreach (var d in cleanedData)
                {
                    int index=-1;
                    if (PT.CellNameToInts.TryGetValue(d.Name, out index))
                    {
                        int row = PT.RowColArray[index, 0];
                        int col = PT.RowColArray[index, 1];
                        dataToPlot[row, col] = d.Value;
                    }
                }
                this.SetMatrixForPlotting(dataToPlot, PT.RowNames, PT.ColNames);
            }
        }
        private PlateType pcurPlateType;
        public PlateType CurPlateType
        {
            get
            { return pcurPlateType; }
        }
        PlateTools PT;
        PlateTools48 pt48= new PlateTools48();
        PlateTools96 pt96=new PlateTools96();
        private GrowthCurveCollection curGCC;
        public void SetGrowthCurveCollection(GrowthCurveCollection GCC)
        {
            pcurPlateType = PlateType.None;   
            this.curGCC = GCC;
            var match96 = from x in GCC where pt96.CellNameToInts.ContainsKey(x.DataSetName) & !pt48.CellNameToInts.ContainsKey(x.DataSetName) select x;
            if (match96.Count() == 0)
            {                
                var match48 = from x in GCC where pt48.CellNameToInts.ContainsKey(x.DataSetName) select x;
                if (match48.Count() > 0)
                {
                    PT = pt48;
                    this.FontForLabels = new System.Drawing.Font(this.FontForLabels.FontFamily, 12);
                    pcurPlateType = PlateType.WELL48;
                }
            }
            else
            {
                PT = pt96;
                this.FontForLabels = new System.Drawing.Font(this.FontForLabels.FontFamily, 8);
                pcurPlateType = PlateType.WELL96;
            }
        }
         public void SaveImage(string Filename)
        {
             this.TheColoredSquaresBM.Save( Filename,System.Drawing.Imaging.ImageFormat.Png);
 
        }
    }
     public class PlateTools
    {
        /// This will be class of tools for interacting with Plate Data
        /// It will mostly be designed for grabbing data or not

        //A bitmap to store the colored squares
        public int NumRows;
        public int NumCols;
        public int NumWells;
        public string[] ColNames
        {
            get
            {
                List<string> toReturn = new List<string>();
                for (int i = 0; i < NumCols; i++)
                {
                    toReturn.Add((i+1).ToString());
                }
                return toReturn.ToArray();
            }
        }
        public string[] RowNames
        {
            get
            {
                string RowNames = "ABCDEFGH";
                return RowNames.Substring(0, NumRows).Select(x=>x.ToString()).ToArray();
            }
        }
        public PlateTools(int numCols,int numRows)
        {
            this.NumCols = numCols;
            this.NumRows = numRows;
            this.NumWells = numCols * NumRows;
            MakeRowColArray();
            MakeIntsToCellName();
        }
        public Dictionary<int, string> IntsToCellName = new Dictionary<int, string>();
        public Dictionary<string, int> CellNameToInts = new Dictionary<string, int>();
        public int[,] RowColArray;
        private void MakeRowColArray()
        {
            ///This method will create a row column array, the first element will be the row and the second the col
            ///Thus rowcolarray[i,0]=row of index 1 and rowcolarray[i,1]=col of index 1, zero order
            
            RowColArray = new int[NumWells, 2];//first element row, second column, indexed by row position
            int index = 0;
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    RowColArray[index, 0] = row;
                    RowColArray[index, 1] = col;
                    index++;
                }
            }
        }
        private void MakeIntsToCellName()
        {
            string RowNames = "ABCDEFGH";
            int count = 0;
            RowNames = RowNames.Substring(0, NumRows);
            foreach (char c in RowNames)
            {
                for (int i = 1; i <= NumCols; i++)
                {
                    IntsToCellName.Add(count, c.ToString()  + i.ToString());
                    CellNameToInts.Add(c.ToString() + i.ToString(), count);
                    count++;
                }
            }
        }
        public int[] ReturnIndexesInRectangle(int TopLeftIndex, int BottomRightIndex)
        {
            //This method will return the indexes in a rectangle defined by the inputs
            int startrow = RowColArray[TopLeftIndex, (int)0];
            int startcol = RowColArray[TopLeftIndex, (int)1];
            int endrow = RowColArray[BottomRightIndex, (int)0];
            int endcol = RowColArray[BottomRightIndex, (int)1];
            int sizeofArray = (endrow - startrow + 1) * (endcol - startcol + 1);
            int[] IndexesToReturn = new int[sizeofArray];
            int spot = 0;
            for (int i = startrow; i < endrow + 1; i++)
            {
                for (int j = startcol; j < endcol + 1; j++)
                {
                    IndexesToReturn[spot] = i * NumCols + j;
                    spot++;
                }
            }
            return IndexesToReturn;

        }
        private int[] ReturnIndexesInRows(int[] rows)
        {
            //This method will return the indexes for a particular set of rows
            //So for example if given rows 2-4, it will return the indexes of all the cells there
            int[] indexes = new int[NumCols * rows.Length];
            int spot = 0;
            foreach (int i in rows)
            {
                for (int j = 0; j < NumCols; j++)
                {
                    indexes[spot] = NumCols * i + j;
                    spot++;
                }
            }
            return indexes;
        }
        public int[] GetInverseSquares(int[] WellsCurrent)
        {
            //This will take a list of well locations, and return a new list that is the opposite of it
            //for example if the old list had well B1, the new list would have everything but B1
            List<int> OldWells = new List<int>(WellsCurrent);
            List<int> NewWells = new List<int>();
            for (int i = 0; i < NumWells; i++)
            {
                if (!OldWells.Contains(i))
                {
                    NewWells.Add(i);
                }
            }
            int[] toReturn = new int[NewWells.Count];
            NewWells.CopyTo(toReturn);
            return toReturn;
        }
       
    }
     public class PlateTools96 : PlateTools
     {
         public PlateTools96():base(12,8)
         {
             this.NumCols = 12;
             this.NumRows = 8;
         }
     }
     public class PlateTools48 : PlateTools
     {
         public PlateTools48()
             : base(8, 6)
         {
         }
     }
}
