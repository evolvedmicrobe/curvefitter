using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace Fit_Growth_Curves
{   
    public class SimpleFunctions
    {
        
        public static void CleanNonRealNumbersFromYvaluesInXYPair(ref double[] x, ref double[] y)
        {
            if ((x.Length != y.Length) | x.Rank != 1 | y.Rank != 1) { throw new Exception("This XY pair is sized wrong"); }
            ArrayList NewXValues = new ArrayList(x.Length);
            ArrayList NewYValues = new ArrayList(y.Length);
            //int toRemoveIndex=new int[x.Length];
            int countToRemove = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (Double.IsPositiveInfinity(x[i]) || Double.IsNegativeInfinity(x[i]) || Double.IsNaN(x[i]) || Double.IsPositiveInfinity(y[i]) || Double.IsNegativeInfinity(y[i]) || Double.IsNaN(y[i]))
                {

                    countToRemove++;
                    //toRemoveIndex[countToRemove] = i;
                }
                else
                {
                    NewXValues.Add(x[i]);
                    NewYValues.Add(y[i]);
                }
            }
            x = new double[x.Length - countToRemove];
            y = new double[x.Length - countToRemove];
            NewYValues.CopyTo(y);
            NewXValues.CopyTo(x);
        }
        public static bool IsARealNumber(double value)
        {
            bool toReturn = true;
            if (Double.IsNaN(value) || Double.IsNegativeInfinity(value) || Double.IsPositiveInfinity(value))
            {
                toReturn = false;
            }
            return toReturn;
        }
      
        public static double MinNotZero(double[] values)
        {
            double res = values.Max();
            foreach (double x in values) if (x < res && x != 0) res = x;
            return res;
        }
      

    }
}
>>>>>>> d4b3b6ea42a998d07873e8992656e5b823ef9e11
