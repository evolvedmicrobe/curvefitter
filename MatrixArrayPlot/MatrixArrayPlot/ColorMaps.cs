using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace MatrixArrayPlot
{
    public delegate Color ColorFunction(double val, double minVal, double maxVal);
    /// <summary>
    /// Color map functions
    /// </summary>
    public class ColorMaps
    {
        public class ColorMap
        {
            private static int colormapLength = 64;
            private static int aValue = 255;
            
            public int[,] Spring()
            {
                int[,] colorArray = new int[colormapLength, 4];
                float[] spring = new float[colormapLength];
                for (int i = 0; i < colormapLength; i++)
                {
                    spring[i] = 1.0f * i / (colormapLength - 1);
                    colorArray[i, 0] = aValue;
                    colorArray[i, 1] = 255;
                    colorArray[i, 2] = (int)(255 * spring[i]);
                    colorArray[i, 3] = 255 - colorArray[i, 1];
                }
                return colorArray;
            }
            
        }
        
        public static Color BinaryColorFunGetColor(double value)
        {
            //This class has no nuance, it is designed to return either white or red , for values zero or 1
           
            if (value == 1)
            {
                return Color.Red;
            }
            else
            {
                return Color.GhostWhite;
            }
        }
        public static Color SpringColorFn(double value,double min,double max)
            {
                
                if (value < min || max==min) { return Color.GhostWhite; }
                int[] colorArray = new int[4];
                double range =((value - min) / (max - min)); 
                colorArray[0] = 255;
                colorArray[1] = 255;
                colorArray[2] = (int)(255 * range);
                colorArray[3] = 0;
                Color ColorToReturn = Color.FromArgb(colorArray[0], colorArray[1], colorArray[2], colorArray[3]);
                return ColorToReturn;
            }

        public static Color rainbowScheme(double val, double minVal, double maxVal)
        {
            if (Double.IsNaN(val))
            {
                return Color.White;
            }
            double r = 0.0;
            double g = 0.0;
            double b = 0.0;
            double range = maxVal - minVal;
            if (range == 0.0)
            {
                range = 1.0;
            }
            double colorDecider = Math.Min(Math.Max((double)((val - minVal) / range), (double)0.0), 1.0);
            if (colorDecider < 0.125)
            {
                r = 0.0;
                g = 0.0;
                b = 0.5 + (4.0 * colorDecider);
            }
            else if (colorDecider < 0.375)
            {
                colorDecider -= 0.125;
                r = 0.0;
                g = 4.0 * colorDecider;
                b = 1.0;
            }
            else if (colorDecider < 0.625)
            {
                colorDecider -= 0.375;
                r = 4.0 * colorDecider;
                g = 1.0;
                b = 1.0 - (4.0 * colorDecider);
            }
            else if (colorDecider < 0.875)
            {
                colorDecider -= 0.625;
                r = 1.0;
                g = 1.0 - (4.0 * colorDecider);
                b = 0.0;
            }
            else
            {
                colorDecider -= 0.875;
                r = 1.0 - (4.0 * colorDecider);
                g = 0.0;
                b = 0.0;
            }
            return RGBToC(r, g, b);
        }
        private static int FractionToByte(double frac)
        {
            int byteValue = (int)(frac * 255.0);
            if (byteValue < 0)
            {
                return 0;
            }
            //Check if value is less than 255, max possible byte
            if (byteValue <= 0xff)
            {
                return byteValue;
            }
            return 0xff;
        }
        private static Color RGBToC(double r, double g, double b)
        {
            int red = FractionToByte(r);
            int green = FractionToByte(g);
            int blue = FractionToByte(b);
            return Color.FromArgb(red, green, blue);
        }
        public static Color BlueRedScheme(double val, double minVal, double maxVal)
        {
            double num = maxVal - minVal;
            if (num == 0.0)
            {
                num = 1.0;
            }
            double r = (val - minVal) / num;
            return RGBToC(r, 0.0, 1.0 - r);
        }
        public static Color GrayScheme(double val, double minVal, double maxVal)
        {
            double num = maxVal - minVal;
            if (num == 0.0)
            {
                num = 1.0;
            }
            double r = (val - minVal) / num;
            return RGBToC(r, r, r);
        }


    }
}
