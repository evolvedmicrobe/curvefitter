using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GrowthCurveLibrary
{
    /// <summary>
    /// A collection of Growth Curves, 
    /// </summary>
    [Serializable]
    public class GrowthCurveCollection :List<GrowthCurve> 
    {
        public string Name = "";
        public const string PICKLEDFILENAME = "DataPickled.csv";
        public bool IsMicroPlateData=false;
        public static GrowthCurveCollection UnpickleData()
        {
            try
            {
                string path = System.IO.Path.GetTempPath();
                string fname = path + "\\" + PICKLEDFILENAME;
                if (File.Exists(fname))
                {
                    // FileStream f = null;
                    //f = new FileStream(fname, FileMode.Open);
                        //BinaryFormatter b = new BinaryFormatter();
                        ////This next bit seems odd to me, got it off a forum as a way to correct an end of stream error
                        //f.Seek(0, 0);
                        //GrowthCurveCollection gcc = (GrowthCurveCollection)b.Deserialize(f);
                        //f.Close();
                        //f.Dispose();
                        //File.Delete(fname);
                        var gcc=ImportPreviousFile.importPreviousDataFile(fname);
                        return gcc;
                 }
            }
            catch (Exception thrown)
            {
                Console.WriteLine("Could Not Unpickle a Data File\n\nError is:" + thrown.Message);

            }
            return null;
        }
        public void PickleData()
        {
            try
            {
                string path = System.IO.Path.GetTempPath();
                string fname=path+"\\"+PICKLEDFILENAME;
                if(File.Exists(fname))
                {
                    File.Delete(fname);
                }
                ExportDataClasses.ExportData(fname, this);
                //FileStream f = new FileStream(fname, FileMode.Create);
                //    BinaryFormatter b = new BinaryFormatter();
                //    b.Serialize(f, this);
                //    f.Close();
                }
            
            catch (Exception thrown)
            {
                Console.WriteLine("Could Not Write Recovery Data File\n\nError is:" + thrown.Message);
                
            }
        }
        public void SendDataToGrowthCurve(List<double[]> absDATA, List<DateTime> acTimeValues, List<string> titles)
        {
            for (int i = 0; i < titles.Count; i++)
            {

                double[] ODDATA = absDATA.Select(x => x[i]).ToArray();
                GrowthCurve GC = new GrowthCurve(titles[i], acTimeValues.ToArray(), ODDATA);
                Add(GC);               
            }          
        }
        public void RemoveDataPointsAsBlanks(bool useFirst=true)
        {
            List<GrowthCurve> data = this.ToList();
            foreach (GrowthCurve GC in data)
            {
                if (useFirst)
                    GC.RemoveFirstPointAsBlank();
                else { GC.RemoveSecondPointAsBlank(); }               
            }    
        }
#if !MONO
        public void CallOutliers()
        {
            foreach (var gc in this)
            {
                OutlierDetector.LinearModelOutlierDetector(gc);
            }
        }
#endif
        public string __repr__()
        {
            if (Name != "")
                return Name;
            else
                return "GrowthCurveCollection";
        }
        
    }
}
