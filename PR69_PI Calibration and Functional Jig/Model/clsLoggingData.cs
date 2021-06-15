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
        public double AnalogInputCurrent1 { get; set; }
        [NotNull]
        public double AnalogInputCurrent2 { get; set; }
        [NotNull]
        public double AnalogInputCurrent3 { get; set; }
        [NotNull]
        public double OutputVoltage1 { get; set; }
        [NotNull]
        public double OutputVoltage2 { get; set; }
        [NotNull]
        public double OutputVoltage3 { get; set; }
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
        public double PT100SensTemp1 { get; set; }
        [NotNull]
        public double PT100SensTemp2 { get; set; }
        [NotNull]
        public double PT100SensTemp3 { get; set; }
        [NotNull]
        public double RSensTemp1 { get; set; }
        [NotNull]
        public double RSensTemp2 { get; set; }
        [NotNull]
        public double RSensTemp3 { get; set; }
        [NotNull]
        public double JSensTemp1 { get; set; }
        [NotNull]
        public double JSensTemp2 { get; set; }
        [NotNull]
        public double JSensTemp3 { get; set; }

        //public clsAnalogOPCurrrent clsAnalogOPCurrrent { get; set; }
        //public clsAnalogOPVoltage clsAnalogOPVoltage { get; set; }       
        //public clsAnalogIPCurrrent clsAnalogIPCurrrent { get; set; }
        //public clsAnalogIPVoltage clsAnalogIPVoltage { get; set; }
        //public PT100Sensor PT100Sensor { get; set; }
        //public RSensor RSensor { get; set; }
        //public JSensor JSensor { get; set; }




        public static clsGlobalVariables.DataLogStatus addDataLog(clsLoggingData Datalogging)
        {
            Datalogging.BatchNumber = "123";
            
            Datalogging.CatlogId= "123";
            
            DateTime FromDate = new DateTime(2021, 6, 14);
            DateTime ToDate = new DateTime(2021, 6, 24);
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
            catch (Exception ex)
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

    public class CalibrationPoints
    {
        public string BatchNumber { get; set; }
        public string CatlogId { get; set; }
        public string Date { get; set; }
        public string IPAddress { get; set; }
        public string Current4mA { get; set; }
        public string Current12mA { get; set; }
        public string Current20mA { get; set; }
        public string Voltage1V { get; set; }
        public string Voltage5V { get; set; }
        public string Voltage10V { get; set; }
    }

}
