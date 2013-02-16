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
            private static int alphaValue = 255;
            
            public int[,] Spring()
            {
                int[,] cmap = new int[colormapLength, 4];
                float[] spring = new float[colormapLength];
                for (int i = 0; i < colormapLength; i++)
                {
                    spring[i] = 1.0f * i / (colormapLength - 1);
                    cmap[i, 0] = alphaValue;
                    cmap[i, 1] = 255;
                    cmap[i, 2] = (int)(255 * spring[i]);
                    cmap[i, 3] = 255 - cmap[i, 1];
                }
                return cmap;
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
                int[] cmap = new int[4];
                double range =((value - min) / (max - min)); 
                cmap[0] = 255;
                cmap[1] = 255;
                cmap[2] = (int)(255 * range);
                cmap[3] = 0;
                Color ColorToReturn = Color.FromArgb(cmap[0], cmap[1], cmap[2], cmap[3]);
                return ColorToReturn;
            }

        public static Color pseudorainbowFn(double val, double minVal, double maxVal)
        {
            if (Double.IsNaN(val))
            {
                return Color.White;
            }
            double r = 0.0;
            double g = 0.0;
            double b = 0.0;
            double num4 = maxVal - minVal;
            if (num4 == 0.0)
            {
                num4 = 1.0;
            }
            double num5 = Math.Min(Math.Max((double)((val - minVal) / num4), (double)0.0), 1.0);
            if (num5 < 0.125)
            {
                r = 0.0;
                g = 0.0;
                b = 0.5 + (4.0 * num5);
            }
            else if (num5 < 0.375)
            {
                num5 -= 0.125;
                r = 0.0;
                g = 4.0 * num5;
                b = 1.0;
            }
            else if (num5 < 0.625)
            {
                num5 -= 0.375;
                r = 4.0 * num5;
                g = 1.0;
                b = 1.0 - (4.0 * num5);
            }
            else if (num5 < 0.875)
            {
                num5 -= 0.625;
                r = 1.0;
                g = 1.0 - (4.0 * num5);
                b = 0.0;
            }
            else
            {
                num5 -= 0.875;
                r = 1.0 - (4.0 * num5);
                g = 0.0;
                b = 0.0;
            }
            return RGBToColor(r, g, b);
        }
        private static int ClampByte(double frac)
        {
            int num = (int)(frac * 255.0);
            if (num < 0)
            {
                return 0;
            }
            if (num <= 0xff)
            {
                return num;
            }
            return 0xff;
        }
        private static Color RGBToColor(double r, double g, double b)
        {
            int red = ClampByte(r);
            int green = ClampByte(g);
            int blue = ClampByte(b);
            return Color.FromArgb(red, green, blue);
        }
        public static Color BlueToRedMap(double val, double minVal, double maxVal)
        {
            double num = maxVal - minVal;
            if (num == 0.0)
            {
                num = 1.0;
            }
            double r = (val - minVal) / num;
            return RGBToColor(r, 0.0, 1.0 - r);
        }
        public static Color GrayMap(double val, double minVal, double maxVal)
        {
            double num = maxVal - minVal;
            if (num == 0.0)
            {
                num = 1.0;
            }
            double r = (val - minVal) / num;
            return RGBToColor(r, r, r);
        }


    }
}
