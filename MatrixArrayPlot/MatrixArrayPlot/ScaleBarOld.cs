using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixArrayPlot
{
    class ScaleBarOld : ArrayPlot
    {
        private int pBinNumber = 10;
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
            for (int i = 1; i < (BinNumber - 1); i++)
            {
                newMat[i,0] = min + interval * i;
            }
            base.SetMatrixForPlotting(newMat,null,null);

        }
        public ScaleBarOld()
            : base()
            {
                this.DrawRowLabels = false;
                this.DrawColLabels = false;
                SetMatrixForPlotting(new double[,] { { 0.0 }, { 1.0 } });
                this.ShowValues = true;
                
            }
        public override void SetMatrixForPlotting(double[,] newMat, string[] WontBeUsed = null, string[] WontBeUsed2 = null)
        {
            double min = newMat.Cast<double>().Min();
            double max = newMat.Cast<double>().Max();
            CreateNewMat(min,max);

        }

       
    }
}
