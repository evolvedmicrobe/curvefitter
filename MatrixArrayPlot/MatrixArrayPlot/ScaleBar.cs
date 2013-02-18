using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MatrixArrayPlot
{
 public partial class ScaleBar : ArrayPlot
    {
        private int pBinNumber = 10;
        public void AttachArrayToProvideScaleFor(ArrayPlot AP)
        {
            this.CF = new ColorFunction(AP.GetColor);
            AP.NewGridLoaded+=new newGridLoadedEvent(SetMatrixForPlotting);
        }
        
        public int BinNumber
        {
            get { return pBinNumber; }
            set
            {
                if (value <= 0 | value > 50)
                {
                    throw new Exception(value.ToString() + " is not an acceptable bin number");
                }
                pBinNumber = value;
                CreateNewMat(this.Matrix.MinValue, this.Matrix.MaxValue);

            }

        }
        private void CreateNewMat(double min, double max)
        {
            double[,] newMat = new double[BinNumber, 1];
            newMat[0,0] = min;
            
            newMat[BinNumber- 1,0] = max;
            double interval = (max - min) / (double)BinNumber;
            double median=min+(max-min)/2.0;
            string[] rowLabels=new string[pBinNumber];
            for (int i = 0; i < BinNumber; i++)
            {
                double v=min + interval * i;
                newMat[i,0] = v;
                rowLabels[i]= Math.Abs(1-(v/median)).ToString("p2");
            }
            base.SetMatrixForPlotting(newMat,rowLabels,null);

        }
        public ScaleBar()
            : base()
            {
                this.DrawRowLabels = true;
                this.DrawColLabels = false;
                this.RoomForText = 55;
                SetMatrixForPlotting(new double[,] { { 0.0 }, { 1.0 } });
                this.ShowValues = true;
                
            }
        public void SetMatrixForPlotting(double[,] newMat)
        {
            this.Matrix.Array = newMat;
             double min = this.Matrix.MinValue;
            double max = this.Matrix.MaxValue;

            CreateNewMat(min,max);
        }
        public override void SetMatrixForPlotting(double[,] newMat, string[] WontBeUsed = null, string[] WontBeUsed2 = null)
        {
            SetMatrixForPlotting(newMat);
        }

       

    }
}
