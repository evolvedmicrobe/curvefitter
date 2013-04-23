
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
#if !MONO
using Microsoft.Office.Interop.Excel;
#else
//using ExcelLibrary;
//using ExcelLibrary.SpreadSheet;
#endif
using System.Collections;
using System.Linq;


namespace GrowthCurveLibrary
{
    public class ImportRobotData
    {
        static List<double[]> absDATA;
        static int plateSize = 96;
        static int ColNumber = 12;
        static List<string> IntToWell;//96 well version of int to name
        static List<double> timeValues;//time values as a double
        static List<DateTime> acTimeValues;//time values as a datetime
        static string Directory;
        /// <summary>
        /// Name of flat file exported by excel
        /// </summary>
        public static string NameofTempFile = "CondensedForCurveFitter.csv";
        public static void ChangeTo48WellPlates()
        {
            plateSize=48;
            ColNumber=8;
        }
        public static void GetTextData(string FileLocations)
        {
            //ChangeTo48WellPlates();
            //SetIntToWell();
            string error = "";
            //first to create an array of values, I know there will be 48 columns in the second one,
            //and for now I am going to assume we will have 200 datapoints, which we will not!
            Directory = FileLocations;
            timeValues = new List<double>();
            acTimeValues = new List<DateTime>();
            absDATA = new List<double[]>();//must initialize this to the plate size
            DirectoryInfo DI = new DirectoryInfo(FileLocations);
            foreach (FileInfo FI in DI.GetFiles())
            {
                if (FI.Extension == ".txt")
                {
                    try
                    {
                        StreamReader SR = new StreamReader(FI.FullName);
                        SR.ReadLine();//skip the first line
                        string line;
                        double[] newData = new double[plateSize];
                        for (int i = 0; i < plateSize; i++)
                        {
                            line = SR.ReadLine();
                            string[] splitit = line.Split('\t');
                            
                            newData[i] = Convert.ToDouble(splitit[5]);
                        }
                        absDATA.Add(newData);
                        //Now we should be on row 49, but the date/time data is on row 101 in the form below
                        //Measured on ....................... 3/26/2007 6:20:39 PM
                        bool Timefound = false;
                        string timeline = "";
                        while (!Timefound)
                        {
                            line = SR.ReadLine();
                            if (line.StartsWith("Measured on"))
                            {
                                timeline = line.Remove(0, 36);
                                Timefound = true;
                            }
                        }
                        error = timeline;
                        DateTime TIME = Convert.ToDateTime(timeline);
                        error = TIME.ToString();
                        acTimeValues.Add(TIME);
                    }
                    catch (Exception thrown)
                    {
                        Exception ex = new Exception("File " + FI.Name + " is screwed" + thrown.Message);
                        throw ex;
                    }
                }
            }
            exportData();
        }
#if !MONO
        /// <summary>
        /// Converts a directory of excel data into a flat text csv file
        /// </summary>
        /// <param name="FileLocation"></param>
        public static void GetExcelData(string FileLocation)
        {

            //ChangeTo48WellPlates();
           // SetIntToWell();
            //first to create an array of values, I know there will be 48 columns in the second one,
            //and for now I am going to assume we will have 200 datapoints, which we will not!
            Directory = FileLocation;
            acTimeValues = new List<DateTime>();
            absDATA = new List<double[]>();//must initialize this to the plate size
            DirectoryInfo DI = new DirectoryInfo(FileLocation);
            ApplicationClass app = null;
            try
            {
                bool SizeSetYet = false;
                app = new ApplicationClass();
                foreach (FileInfo FI in DI.GetFiles())
                {
                    if (FI.Extension == ".xls")
                    {
                        Workbook workBook = app.Workbooks.Open(FI.FullName,
                            0,
                            true,
                            5,
                            "",
                            "",
                            true,
                            XlPlatform.xlWindows, "", false, false, 0, true, 1, 0);
                        
                        Worksheet workSheet = (Worksheet)workBook.Worksheets[1];
                        Range excelRange = workSheet.UsedRange;
                        object[,] valueArray = (object[,])excelRange.get_Value(
                            XlRangeValueDataType.xlRangeValueDefault);
                        int numCols = valueArray.GetUpperBound(1);
                        int numRows = valueArray.GetUpperBound(0);
                        if (!SizeSetYet)
                        {
                            plateSize = numRows - 1;
                            if (plateSize == 96)
                            {
                                ColNumber = 12;
                            }
                            else { ColNumber = 8; }
                            SetIntToWell();
                            SizeSetYet = true;
                        }
                        numRows++;
                        double[] newData = new double[plateSize];
                        for (int i = 0; i < 48; i++)
                        {
                            var Cell = valueArray[i + 2, 6];
                            double value = (double)Cell;
                            newData[i] = value;
                        }
                        absDATA.Add(newData);
                        ///NEW CODE ADDED BELOW
                        string timeline = "";
                        workSheet = (Worksheet)workBook.Worksheets[3];
                        Range excelRange2 = workSheet.UsedRange;
                        object[,] DescriptArray = (object[,])excelRange2.get_Value(XlRangeValueDataType.xlRangeValueDefault);
                        int TotalSize = DescriptArray.GetUpperBound(0);
                        DateTime testTime = DateTime.Now;
                        DateTime TIME = testTime;
                        for (int i = 0; i < TotalSize; i++)
                        {
                            timeline = (string)DescriptArray[i + 1, 1];
                            if (timeline != null && timeline.StartsWith("Measured on ..."))
                            {
                                timeline = timeline.Remove(0, 36);
                                TIME = Convert.ToDateTime(timeline);
                                break;
                            }
                        }
                        if (TIME == testTime) throw new Exception("No time found in file");

                        acTimeValues.Add(TIME);
                        app.Workbooks.Close();
                    }
                }
                exportData();
            }
            catch (Exception thrown)
            {
                //Exception ex = new Exception("File " + FI.Name + " is screwed" ,thrown);
                //throw ex;
                throw thrown;
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                    app = null;
                }
            }
        }
        /// <summary>
        /// Converts a directory of excel data into a flat text csv file
        /// </summary>
        /// <param name="FileLocation"></param>
        public static void GetExcelVenusData(string FileLocation)
        {
            //ChangeTo48WellPlates();
            // SetIntToWell();
            //first to create an array of values, I know there will be 48 columns in the second one,
            //and for now I am going to assume we will have 200 datapoints, which we will not!
            Directory = FileLocation;
            acTimeValues = new List<DateTime>();
            absDATA = new List<double[]>();//must initialize this to the plate size
            DirectoryInfo DI = new DirectoryInfo(FileLocation);
            ApplicationClass app = null;
            try
            {
                bool SizeSetYet = false;
                app = new ApplicationClass();
                foreach (FileInfo FI in DI.GetFiles())
                {
                    if (FI.Extension == ".xls")
                    {
                        Workbook workBook = app.Workbooks.Open(FI.FullName,
                            0,
                            true,
                            5,
                            "",
                            "",
                            true,
                            XlPlatform.xlWindows, "", false, false, 0, true, 1, 0);

                        Worksheet workSheet = (Worksheet)workBook.Worksheets[1];
                        Range excelRange = workSheet.UsedRange;
                        object[,] valueArray = (object[,])excelRange.get_Value(
                            XlRangeValueDataType.xlRangeValueDefault);
                        int numCols = valueArray.GetUpperBound(1);
                        int numRows = valueArray.GetUpperBound(0);
                        if (!SizeSetYet)
                        {
                            plateSize = numRows - 1;
                            if (plateSize == 96)
                            {
                                ColNumber = 12;
                            }
                            else { ColNumber = 8; }
                            SetIntToWell();
                            SizeSetYet = true;
                        }
                        numRows++;
                        double[] newData = new double[plateSize];
                        for (int i = 0; i < 48; i++)
                        {
                            var Cell = valueArray[i + 2, 8];
                            double value = (double)Cell;
                            newData[i] = value;
                        }
                        absDATA.Add(newData);
                        ///NEW CODE ADDED BELOW
                        string timeline = "";
                        workSheet = (Worksheet)workBook.Worksheets[3];
                        Range excelRange2 = workSheet.UsedRange;
                        object[,] DescriptArray = (object[,])excelRange2.get_Value(XlRangeValueDataType.xlRangeValueDefault);
                        int TotalSize = DescriptArray.GetUpperBound(0);
                        DateTime testTime = DateTime.Now;
                        DateTime TIME = testTime;
                        for (int i = 0; i < TotalSize; i++)
                        {
                            timeline = (string)DescriptArray[i + 1, 1];
                            if (timeline != null && timeline.StartsWith("Measured on ..."))
                            {
                                timeline = timeline.Remove(0, 36);
                                TIME = Convert.ToDateTime(timeline);
                                break;
                            }
                        }
                        if (TIME == testTime) throw new Exception("No time found in file");

                        acTimeValues.Add(TIME);
                        app.Workbooks.Close();
                    }
                }
                exportVenusData();
            }
            catch (Exception thrown)
            {
                //Exception ex = new Exception("File " + FI.Name + " is screwed" ,thrown);
                //throw ex;
                throw thrown;
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                    app = null;
                }
            }
        }
#else
        //public static void GetExcelDataNoCom(string FileLocation)
        //{
        //    //ChangeTo48WellPlates();
        //    // SetIntToWell();
        //    //first to create an array of values, I know there will be 48 columns in the second one,
        //    //and for now I am going to assume we will have 200 datapoints, which we will not!
        //    Directory = FileLocation;
        //    acTimeValues = new List<DateTime>();
        //    absDATA = new List<double[]>();//must initialize this to the plate size
        //    DirectoryInfo DI = new DirectoryInfo(FileLocation);
        //    try
        //    {
        //        foreach (FileInfo FI in DI.GetFiles())
        //        {
        //            if (FI.Extension == ".xls")
        //            {
                        
        //                var FO=File.OpenRead(FI.FullName);
        //                Workbook workBook = Workbook.Load(FO);
        //                Worksheet sheet = (Worksheet)workBook.Worksheets[1];
        //                int numRows = sheet.Cells.LastRowIndex;
        //                int plateSize = numRows;
        //                double[] newData = new double[plateSize];
        //                for (int rowIndex = sheet.Cells.FirstRowIndex; rowIndex < numRows; rowIndex++)
        //                {
        //                    Row r = sheet.Cells.GetRow(rowIndex);
        //                    var Cell = r.GetCell(5);
        //                    double value = (double)Cell.Value;
        //                    newData[rowIndex] = value;
        //                }
        //                absDATA.Add(newData);
        //                ///NEW CODE ADDED BELOW
        //                string timeline = "";
        //                sheet = (Worksheet)workBook.Worksheets[3];
        //                numRows = sheet.Cells.LastRowIndex;
        //                DateTime testTime = DateTime.Now;
        //                DateTime TIME = testTime;
        //                for (int rowIndex = sheet.Cells.FirstRowIndex; rowIndex < numRows; rowIndex++)
        //                {
        //                    Row r = sheet.Cells.GetRow(rowIndex);

        //                    timeline = (string)r.GetCell(0).Value.ToString();
        //                    if (timeline != null && timeline.StartsWith("Measured on ..."))
        //                    {
        //                        timeline = timeline.Remove(0, 36);
        //                        TIME = Convert.ToDateTime(timeline);
        //                        break;
        //                    }
        //                }
        //                if (TIME == testTime) throw new Exception("No time found in file");
        //                acTimeValues.Add(TIME);

        //            }
        //        }
        //        exportData();
        //    }
        //    catch (Exception thrown)
        //    {
        //        //Exception ex = new Exception("File " + FI.Name + " is screwed" ,thrown);
        //        //throw ex;
        //        throw thrown;
        //    }

        //}
#endif
        private static void SetIntToWell()
        {
            IntToWell = new List<string>(plateSize);
            string Rows = "ABCDEFGH";
            for (int i = 0; i < plateSize; i++)
            {
                int ColPos = (i % ColNumber) + 1;
                int RowPos = Convert.ToInt32((i / ColNumber));
                string toSet = Rows[RowPos] + ColPos.ToString();
                IntToWell.Add(toSet);
            }
        }
        private static void exportData()
        {
            StreamWriter SW = new StreamWriter(Directory+"\\" + NameofTempFile);
            SW.Write("Time,");
            string nextline = "";
            for (int i = 0; i < plateSize; i++)
            {
                nextline += IntToWell[i] + ",";
            }
            nextline = nextline.Trim(',');
            SW.Write(nextline + "\n");
            for (int i = 0; i < acTimeValues.Count; i++)
            {
                SW.Write(acTimeValues[i].ToString() + ",");
                string lastline = "";
                for (int j = 0; j < plateSize; j++)
                {
                    lastline += absDATA[i][j].ToString() + ",";
                }
                lastline = lastline.TrimEnd(',');
                SW.Write(lastline + "\n");
            }
            SW.Close();
            absDATA = null;
            acTimeValues = null;
        }
        private static void exportVenusData()
        {
            StreamWriter SW = new StreamWriter(Directory + "\\" +"Venus_"+ NameofTempFile);
            SW.Write("Time,");
            string nextline = "";
            for (int i = 0; i < plateSize; i++)
            {
                nextline += IntToWell[i] + ",";
            }
            nextline = nextline.Trim(',');
            SW.Write(nextline + "\n");
            for (int i = 0; i < acTimeValues.Count; i++)
            {
                SW.Write(acTimeValues[i].ToString() + ",");
                string lastline = "";
                for (int j = 0; j < plateSize; j++)
                {
                    lastline += absDATA[i][j].ToString() + ",";
                }
                lastline = lastline.TrimEnd(',');
                SW.Write(lastline + "\n");
            }
            SW.Close();
            absDATA = null;
            acTimeValues = null;
        }
    }
    /// <summary>
    /// Loads a file previously unloaded by the curve fitter
    /// </summary>
    public class ImportPreviousFile
    {
        public static GrowthCurveCollection importPreviousDataFile(string file)
        {
            StreamReader SR = new StreamReader(file);
            GrowthCurveCollection GCC = new GrowthCurveCollection();

            //drop two header lines
            SR.ReadLine();
            //SR.ReadLine();
            string line = "";
            List<string> FitSummaryLines = new List<string>();
            while ((line = SR.ReadLine()) != null)
            {
                if (!line.StartsWith("Complete Data Listing Below"))
                { FitSummaryLines.Add(line); }
                else { break; }
            }
            //now to grab the more important data
            List<string[]> CompleteDataListing = new List<string[]>();
            SR.ReadLine();//blow through headerline
            while ((line = SR.ReadLine()) != null)
            {
                if (line.Length > 3)
                {
                    CompleteDataListing.Add(line.Split(','));
                }
            }
            //now to add the datetimes to the file
            DateTime[] AllDateTimes = new DateTime[CompleteDataListing.Count];
            for (int i = 0; i < CompleteDataListing.Count; i++)
            {
                AllDateTimes[i] = (Convert.ToDateTime(CompleteDataListing[i][0]));
            }
            //now to convert this into something useful 
            for (int i = 0; i < FitSummaryLines.Count; i++)
            {
                List<DateTime> DateTimeArray = new List<DateTime>();
                List<double> ODValuesArray = new List<double>();

                string name = (string)FitSummaryLines[i];
                name = name.Split(',')[0];
                string notes = (string)FitSummaryLines[i];
                notes = notes.Split(',')[9];
                int dataposition = 1 + i * 2;
                int indexPos = 0;
                List<int> IndexesToFit = new List<int>();
                for (int j = 0; j < CompleteDataListing.Count; j++)
                {
                    if ((CompleteDataListing[j] as string[])[dataposition] != "-999")
                    {
                        DateTimeArray.Add(Convert.ToDateTime(CompleteDataListing[j][0]));
                        ODValuesArray.Add(Convert.ToDouble(CompleteDataListing[j][dataposition]));
                        int flag = Convert.ToInt32(CompleteDataListing[j][dataposition + 1]);
                        if (flag == 0)
                        {
                            IndexesToFit.Add(indexPos);
                        }
                        indexPos++;
                    }
                }
                DateTime[] Times = DateTimeArray.ToArray();
                double[] ODvalues = ODValuesArray.ToArray();
                GrowthCurve GD = new GrowthCurve(name, Times, ODvalues);
                GD.SetFittedRangeFromIndexes(IndexesToFit);
                GCC.Add(GD);
            }
            return GCC;
        }
    }
    public class Import16MinuteFile
    {
        public static GrowthCurveCollection ImportFile(string file)
        {
            //first to create an array of values, I know there will be 48 columns in the second one,
            //and for now I am going to assume we will have 200 datapoints, which we will not!
            GrowthCurveCollection GCC = new GrowthCurveCollection();
            StreamReader SR = new StreamReader(file);
            DateTime[] times = null ;
            string line;
            //reset values for matrices
            while ((line = SR.ReadLine()) != null && line.Length > 0 && !(line.StartsWith(",,")))
            {
                string[] splitit = line.Split(',');
                int N = splitit.Length;
                if (splitit[N - 1] == "")
                    N = N - 1;
                if (times == null)
                {
                    times = new DateTime[N-1];
                    DateTime ctime = DateTime.Now;
                    for (int i = 0; i < (N-1); i++)
                    {
                        ctime = ctime.AddMinutes(16);
                        times[i] = ctime;
                    }
                }
                string name = splitit[0];
                double[] data=new double[N-1];
                for (int i = 1; i < splitit.Length; i++)//was -1
                { data[i - 1] = Convert.ToDouble(splitit[i]); }//add datetime
                GrowthCurve gc = new GrowthCurve(name, times, data);
                GCC.Add(gc);
            }
            return GCC;
        }
    }

    public class ImportDateTimeCSV
    {
        /// <summary>
        /// Fills a growth curve collection with the data in a CSV file
        /// </summary>
        /// <param name="FullFileName"></param>
        /// <param name="toFill"></param>
        public static void FillCurveCollectionFromCSV(string FullFileName,GrowthCurveCollection toFill)
        {
            //first to create an array of values, I know there will be 48 columns in the second one,
            //and for now I am going to assume we will have 200 datapoints, which we will not!
            StreamReader SR = new StreamReader(FullFileName);
            string line;
            List<string> titles = new List<string>();// = new string[1];//holds all the titles
            List<double[]> absDATA = new List<double[]>();// = new double[1, 1];
            List<DateTime> acTimeValues=new List<DateTime>();// = new DateTime[1];//time values as a datetime
            
            //get titles
            line = SR.ReadLine();
            string[] splitit = line.Trim().Split(',');//title should be of the form #Time, Data1,Data2,Data3;
            int NumSamples = splitit.Length;
            for (int i = 1; i < splitit.Length; i++)
            { titles.Add(splitit[i]); }//add title names

            //reset values for matrices
            while ((line = SR.ReadLine()) != null && line.Length > 0 && !(line.StartsWith(",,")))
            {
                double[] data = new double[NumSamples];
                splitit = line.Split(',');
                acTimeValues.Add(Convert.ToDateTime(splitit[0]));//add the time value
                for (int i = 1; i < splitit.Length; i++)//was -1
                { data[i - 1] = Convert.ToDouble(splitit[i]); }//add datetime
                absDATA.Add(data);
            }
            SR.Close();
            toFill.SendDataToGrowthCurve(absDATA, acTimeValues, titles);
        }
        /// <summary>
        /// Returns a list of growth curves from a csv file.
        /// </summary>
        /// <param name="FullFileName"></param>
        /// <returns></returns>
        public static List<GrowthCurve> GetDataFromCSV(string FullFileName)
        {
            List<GrowthCurve> toReturn = new List<GrowthCurve>();
            //first to create an array of values, I know there will be 48 columns in the second one,
            //and for now I am going to assume we will have 200 datapoints, which we will not!
            StreamReader SR = new StreamReader(FullFileName);
            string line;
            List<string> titles = new List<string>();// = new string[1];//holds all the titles
            List<double[]> absDATA = new List<double[]>();// = new double[1, 1];
            List<DateTime> acTimeValues = new List<DateTime>();// = new DateTime[1];//time values as a datetime

            //get titles
            line = SR.ReadLine();
            string[] splitit = line.Trim().Split(',');//title should be of the form #Time, Data1,Data2,Data3;
            int NumSamples = splitit.Length;
            for (int i = 1; i < splitit.Length; i++)
            { titles.Add(splitit[i]); }//add title names

            //reset values for matrices
            while ((line = SR.ReadLine()) != null && line.Length > 0 && !(line.StartsWith(",,")))
            {
                double[] data = new double[NumSamples];
                splitit = line.Split(',');
                acTimeValues.Add(Convert.ToDateTime(splitit[0]));//add the time value
                for (int i = 1; i < splitit.Length; i++)//was -1
                { data[i - 1] = Convert.ToDouble(splitit[i]); }//add datetime
                absDATA.Add(data);
            }
            SR.Close();
            for (int i = 0; i < titles.Count; i++)
            {

                double[] ODDATA = absDATA.Select(x => x[i]).ToArray();
                GrowthCurve GC = new GrowthCurve(titles[i], acTimeValues.ToArray(), ODDATA);
                toReturn.Add(GC);
            }
            return toReturn;
            //toFill.SendDataToGrowthCurve(absDATA, acTimeValues, titles);
        }
    }

    public class ImportNumericTimeCSV
    {
        /// <summary>
        /// Fills a growth curve collection with the data in a CSV file
        /// </summary>
        /// <param name="FullFileName"></param>
        /// <param name="toFill"></param>
        public static void FillCurveCollectionFromCSV(string FullFileName, GrowthCurveCollection toFill)
        {
            List<GrowthCurve> data = GetDataFromCSV(FullFileName);
            toFill.AddRange(data);
        }
        /// <summary>
        /// Returns a list of growth curves from a csv file.
        /// </summary>
        /// <param name="FullFileName"></param>
        /// <returns></returns>
        public static List<GrowthCurve> GetDataFromCSV(string FullFileName)
        {
            DateTime StartTime = new DateTime();
            
            List<GrowthCurve> toReturn = new List<GrowthCurve>();
            //first to create an array of values, I know there will be 48 columns in the second one,
            //and for now I am going to assume we will have 200 datapoints, which we will not!
            StreamReader SR = new StreamReader(FullFileName);
            string line;
            List<string> titles = new List<string>();// = new string[1];//holds all the titles
            List<double[]> absDATA = new List<double[]>();// = new double[1, 1];
            List<DateTime> acTimeValues = new List<DateTime>();// = new DateTime[1];//time values as a datetime

            //get titles
            line = SR.ReadLine();
            string[] splitit = line.Trim().Split(',');//title should be of the form #Time, Data1,Data2,Data3;
            int NumSamples = splitit.Length;
            if (String.IsNullOrEmpty(splitit[splitit.Length - 1]))
            {
                NumSamples -= 1;
            }
            for (int i = 1; i < NumSamples; i++)
            { titles.Add(splitit[i]); }//add title names

            //reset values for matrices
            while ((line = SR.ReadLine()) != null && line.Length > 0 && !(line.StartsWith(",,")))
            {
                double[] data = new double[NumSamples];
                splitit = line.Split(',');

                acTimeValues.Add(StartTime.AddHours(Convert.ToDouble(splitit[0])));//add the time value

                for (int i = 1; i < NumSamples; i++)//was -1
                { data[i - 1] = Convert.ToDouble(splitit[i]); }//add datetime
                absDATA.Add(data);
            }
            SR.Close();
            for (int i = 0; i < titles.Count; i++)
            {

                double[] ODDATA = absDATA.Select(x => x[i]).ToArray();
                GrowthCurve GC = new GrowthCurve(titles[i], acTimeValues.ToArray(), ODDATA);
                toReturn.Add(GC);
            }
            return toReturn;
            //toFill.SendDataToGrowthCurve(absDATA, acTimeValues, titles);
        }
    }

}
