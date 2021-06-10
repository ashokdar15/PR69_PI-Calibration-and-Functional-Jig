using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    [Table("LoggingData")]
    public class clsLoggingData
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int SerialNumber { get; set; }
        [NotNull]
        public DateTime Date { get; set; }
        [NotNull]
        public string BatchNumber { get; set; }
        [NotNull]
        public string CatlogId { get; set; }
        [NotNull]
        public double CalibrationPoint1mV { get; set; }
        [NotNull]
        public double CalibrationPoint5mV { get; set; }
        [NotNull]
        public double SSROutput { get; set; }
        [NotNull]
        public double CJCOutput { get; set; }
        [NotNull]
        public double VDC24Volt { get; set; }
        [NotNull]
        public double OutputCurrent4mA { get; set; }
        [NotNull]
        public double OutputCurrent12mA { get; set; }
        [NotNull]
        public double OutputCurrent20mA { get; set; }
        [NotNull]
        public double OutputVoltage1V { get; set; }
        [NotNull]
        public double OutputVoltage5V { get; set; }
        [NotNull]
        public double OutputVoltage10V { get; set; }
        [NotNull]
        public double InputCurrent4mA { get; set; }
        [NotNull]
        public double InputCurrent12mA { get; set; }
        [NotNull]
        public double InputCurrent20mA { get; set; }
        [NotNull]
        public double InputVoltage1V { get; set; }
        [NotNull]
        public double InputVoltage5V { get; set; }
        [NotNull]
        public double InputVoltage10V { get; set; }
        [NotNull]
        public double PT100SensTemp0Degreecelcius { get; set; }
        [NotNull]
        public double PT100SensTemp400Degreecelcius { get; set; }
        [NotNull]
        public double PT100SensTemp700Degreecelcius { get; set; }
        [NotNull]
        public double RSensTemp0Degreecelcius { get; set; }
        [NotNull]
        public double RSensTemp1750Degreecelcius { get; set; }
        [NotNull]
        public double RSensTemp1000Degreecelcius { get; set; }
        [NotNull]
        public double JSensTemp0Degreecelcius { get; set; }
        [NotNull]
        public double JSensTemp400Degreecelcius { get; set; }
        [NotNull]
        public double JSensTemp700Degreecelcius { get; set; }

        //public clsAnalogOPCurrrent clsAnalogOPCurrrent { get; set; }
        //public clsAnalogOPVoltage clsAnalogOPVoltage { get; set; }       
        //public clsAnalogIPCurrrent clsAnalogIPCurrrent { get; set; }
        //public clsAnalogIPVoltage clsAnalogIPVoltage { get; set; }
        //public PT100Sensor PT100Sensor { get; set; }
        //public RSensor RSensor { get; set; }
        //public JSensor JSensor { get; set; }


        public static clsGlobalVariables.DataLogStatus addDataLog(clsLoggingData Datalogging)
        {
            DateTime FromDate = new DateTime(2021, 6, 14);
            DateTime ToDate = new DateTime(2021, 6, 24);

            // clsLoggingData.getDataLog(FromDate, ToDate);
                        
            //for (int i = 0; i < 30; i++)
            //{

            //    clsLoggingData clsLoggingData = new clsLoggingData()
            //    {

            //        //Date = new DateTime(2021, 06, i),

            //        BatchNumber = Convert.ToString(1000 + i),

            //        SerialNumber = 10,

            //        CatlogId = "",

            //        CalibrationPoint1mV = 12.5,

            //        CalibrationPoint5mV = 12.5,

            //        SSROutput = 12.5,

            //        CJCOutput = 12.5,

            //        VDC24Volt = 12.5,

            //        OutputCurrent4mA = 12.5,

            //        OutputCurrent12mA = 12.5,

            //        OutputCurrent20mA = 12.5,

            //        OutputVoltage1V = 12.5,

            //        OutputVoltage5V = 12.5,

            //        OutputVoltage10V = 12.5,

            //        InputCurrent4mA = 12.5,

            //        InputCurrent12mA = 12.5,

            //        InputCurrent20mA = 12.5,

            //        InputVoltage1V = 12.5,

            //        InputVoltage5V = 12.5,

            //        InputVoltage10V = 12.5,

            //        PT100SensTemp0Degreecelcius = 12.5,

            //        PT100SensTemp400Degreecelcius = 12.5,

            //        PT100SensTemp700Degreecelcius = 12.5,

            //        RSensTemp0Degreecelcius = 12.5,

            //        RSensTemp1750Degreecelcius = 12.5,

            //        RSensTemp1000Degreecelcius = 12.5,

            //        JSensTemp0Degreecelcius = 12.5,

            //        JSensTemp400Degreecelcius = 12.5,

            //        JSensTemp700Degreecelcius = 12.5
            //        //AccuracymAmpTestsDetails[0].

            //    };

            //    clsLoggingData.addDataLog(clsLoggingData);
            //}

            //return;

            try
            {
                clsGlobalVariables.DatabasePath = "temp.db";
                if (clsGlobalVariables.DatabasePath != "")
                {
                    string dabasePath = clsGlobalVariables.DatabasePath;
                    using (SQLiteConnection conn = new SQLiteConnection(dabasePath))
                    {
                        conn.CreateTable<clsLoggingData>();
                        int rows = conn.Insert(Datalogging);

                        if (rows > 0)
                        {
                            return clsGlobalVariables.DataLogStatus.DataLogged;
                        }
                        else
                        {
                            return clsGlobalVariables.DataLogStatus.DataLoggedFailed;
                        }
                    }
                }
                else
                {
                    return clsGlobalVariables.DataLogStatus.DatabaseConectionFailed;
                }
            }
            catch (Exception)
            {
                return clsGlobalVariables.DataLogStatus.ExceptionHandeled;
            }
        }

        public static clsGlobalVariables.DataLogStatus getDataLog(DateTime date1 , DateTime date2 )
        {
            try
            {
                clsGlobalVariables.DatabasePath = "temp.db";
                if (clsGlobalVariables.DatabasePath != "")
                {
                    string dabasePath = clsGlobalVariables.DatabasePath;
                    using (SQLiteConnection conn = new SQLiteConnection(dabasePath))
                    {
                        //conn.CreateTable<clsLoggingData>();
                        //int rows = conn.Insert(Datalogging);
                        //conn.Query()
                        var Get = from s in conn.Table<clsLoggingData>()
                                    where s.Date >= date1 && s.Date <= date2
                                    select s;


                        //InvoiceDate BETWEEN '2010-01-01' AND '2010-01-31'
                    }
                }
                else
                {
                    return clsGlobalVariables.DataLogStatus.DatabaseConectionFailed;
                }
            }
            catch (Exception)
            {
                return clsGlobalVariables.DataLogStatus.ExceptionHandeled;
            }

            return clsGlobalVariables.DataLogStatus.DatabaseConectionFailed;
        }

        public static clsGlobalVariables.DataLogStatus deleteDataLog(DateTime date)
        {
            try
            {
                clsGlobalVariables.DatabasePath = "temp.db";
                if (clsGlobalVariables.DatabasePath != "")
                {
                    string dabasePath = clsGlobalVariables.DatabasePath;
                    using (SQLiteConnection conn = new SQLiteConnection(dabasePath))
                    {   
                        var obj = conn.Table<clsLoggingData>().ToList().Where(u => u.Date <= date).ToList();
                        
                        var res = conn.Delete("");

                        //InvoiceDate BETWEEN '2010-01-01' AND '2010-01-31'
                    }
                }
                else
                {
                    return clsGlobalVariables.DataLogStatus.DatabaseConectionFailed;
                }
            }
            catch (Exception)
            {
                return clsGlobalVariables.DataLogStatus.ExceptionHandeled;
            }

            return clsGlobalVariables.DataLogStatus.DatabaseConectionFailed;
        }

    }
    //[Table("AnalogOPCurrrent")]
    //public class clsAnalogOPCurrrent
    //{
    //    public double OutputCurrent4mA { get; set; }
    //    public double OutputCurrent12mA { get; set; }
    //    public double OutputCurrent20mA { get; set; }
    //}

    //[Table("AnalogOPVoltage")]
    //public class clsAnalogOPVoltage
    //{
    //    public double OutputVoltage1V { get; set; }
    //    public double OutputVoltage5V { get; set; }
    //    public double OutputVoltage10V { get; set; }
    //}

    //[Table("AnalogIPCurrrent")]
    //public class clsAnalogIPCurrrent
    //{
    //    public double InputCurrent4mA { get; set; }
    //    public double InputCurrent12mA { get; set; }
    //    public double InputCurrent20mA { get; set; }
    //}

    //[Table("AnalogIPVoltage")]
    //public class clsAnalogIPVoltage
    //{
    //    public double InputVoltage1V { get; set; }
    //    public double InputVoltage5V { get; set; }
    //    public double InputVoltage10V { get; set; }
    //}

    //[Table("PT100Sensor")]
    //public class PT100Sensor
    //{
    //    public double Temp0Degreecelcius { get; set; }
    //    public double Temp400Degreecelcius { get; set; }
    //    public double Temp700Degreecelcius { get; set; }
        
    //}

    //[Table("RSensor")]
    //public class RSensor
    //{
    //    public double Temp0Degreecelcius { get; set; }
    //    public double Temp1750Degreecelcius { get; set; }
    //    public double Temp1000Degreecelcius { get; set; }
    //}

    //[Table("JSensor")]
    //public class JSensor
    //{
    //    public double Temp0Degreecelcius { get; set; }
    //    public double Temp400Degreecelcius { get; set; }
    //    public double Temp700Degreecelcius { get; set; }
    //}


}
