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
        
        public double CalibrationPoint1mV { get; set; }
        
        public double CalibrationPoint5mV { get; set; }
        
        public double SSROutput { get; set; }
       
        public double CJCOutput { get; set; }
       
        public double VDC24Volt { get; set; }
        
               
        public double InputCurrent4mA { get; set; }
        
        public double InputCurrent12mA { get; set; }
        
        public double InputCurrent20mA { get; set; }
        
        public double InputVoltage1V { get; set; }
        
        public double InputVoltage5V { get; set; }
        
        public double InputVoltage10V { get; set; }

        public double AnalogInputCurrent1 { get; set; }

        public double AnalogInputCurrent2 { get; set; }

        public double AnalogInputCurrent3 { get; set; }

        public double OutputVoltage1 { get; set; }

        public double OutputVoltage2 { get; set; }

        public double OutputVoltage3 { get; set; }

        public double PT100SensTemp1 { get; set; }
        
        public double PT100SensTemp2 { get; set; }
       
        public double PT100SensTemp3 { get; set; }
       
        public double RSensTemp1 { get; set; }
       
        public double RSensTemp2 { get; set; }
       
        public double RSensTemp3 { get; set; }
    
        public double JSensTemp1 { get; set; }
    
        public double JSensTemp2 { get; set; }
        
        public double JSensTemp3 { get; set; }

        public static clsGlobalVariables.DataLogStatus addDataLog(clsLoggingData Datalogging)
        {
            Datalogging.BatchNumber = clsGlobalVariables.mainWindowVM.BatchNumber;

            Datalogging.CatlogId = clsGlobalVariables.Selectedcatid.DeviceName;

            Datalogging.Date = DateTime.Now;
            
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
                        var res = from s in conn.Table<clsLoggingData>().ToList()
                                  where s.Date >= date1 && s.Date <= date2
                                  select s;
                        if (res.Count<clsLoggingData>() > 0)
                        {
                            //List<clsLoggingData> obj = res;

                            List<clsLoggingData> resu = res.ToList<clsLoggingData>();

                            return clsGlobalVariables.DataLogStatus.Valid;
                        }
                        
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
    
}
