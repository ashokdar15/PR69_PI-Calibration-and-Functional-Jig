using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    /********************************************************************************************
              Class Name        : clsDataLog Class
              Purpose           : This class is used for data(Accuracy Testing) logging in the "*.csv" File.
              Date              : 5/01/2018
              Written By        : Shubham
              CopyRight         : General Industrial Controls Pvt. Ltd. Pune
              Modified          : Date                
              Released Version  :  V15
              Changed By        :  NA
              Decription Of Change:  NA
    ********************************************************************************************/
    public class clsDataLog
    {
        double dblgChkSum = 0;

        //This variable is used in the split method. 
        char[] arrmSeparator = { ',' };
        //-------Changed By Shubham
        //Date:- 24-02-2018
        //Version:- V16
        //"strmRef_Vtg" variable will store the VREF value read from the device.
        //Variable declaration
        //-------Changed By Shubham
        //Date:- 28-04-2018
        //Version:- V17
        //Statement:- To store timings of Calibration and accuracy, new variables are declared.
        private string strmPT100_ZERO, strmPT100_400, strmPT100_700, strmR_1750, strmR_1000, strmR_ZERO, strmAnlogIP_mA_4, strmAnlogIP_mA_12,
        strmAnlogIP_mA_20, strmAnlogIP_Volt_1, strmAnlogIP_Volt_5, strmAnlogIP_Volt_10, strmAnalogOP_mA_4, strmAnalogOP_mA_20, strmAnalogOP_mA_12,
        strmAnalogOP_Volt_1, strmAnalogOP_Volt_10, strmAnalogOP_Volt_5, strmJ_ZERO, strmJ_400, strmJ_700, strmRef_Vtg,
        strmCalibrationTime, strmAccuracyTime, strmAnalogOP_mA_1;
        //------------Changes End.

        //Default constructor of the class.
        public clsDataLog()
        {
            Clear();
        }
        //Property of Accuracy time stopwatch.
        public string StrmAccuracyTime
        {
            get { return strmAccuracyTime; }
            set { strmAccuracyTime = value; }
        }

        //Property of Calibration time stopwatch.
        public string StrmCalibrationTime
        {
            get { return strmCalibrationTime; }
            set { strmCalibrationTime = value; }
        }

        //Property for J sensor 700 value
        public string StrmJ_700
        {
            get { return strmJ_700; }
            set { strmJ_700 = value; }
        }
               

        //Property for J sensor 400 value
        public string StrmJ_400
        {
            get { return strmJ_400; }
            set { strmJ_400 = value; }
        }

        //Property for J sensor Zero value
        public string StrmJ_ZERO
        {
            get { return strmJ_ZERO; }
            set { strmJ_ZERO = value; }
        }
        //Property for Analog op Volt sensor 5V
        public string StrmAnalogOP_Volt_5
        {
            get { return strmAnalogOP_Volt_5; }
            set { strmAnalogOP_Volt_5 = value; }
        }
        //Property for Analog op Volt sensor 10V
        public string StrmAnalogOP_Volt_10
        {
            get { return strmAnalogOP_Volt_10; }
            set { strmAnalogOP_Volt_10 = value; }
        }

        //Property for Analog op Volt sensor 1V
        public string StrmAnalogOP_Volt_1
        {
            get { return strmAnalogOP_Volt_1; }
            set { strmAnalogOP_Volt_1 = value; }
        }

        //Property for Analog op mA sensor 12mA
        public string StrmAnalogOP_mA_12
        {
            get { return strmAnalogOP_mA_12; }
            set { strmAnalogOP_mA_12 = value; }
        }

        //Property for Analog op mA sensor 20mA
        public string StrmAnalogOP_mA_20
        {
            get { return strmAnalogOP_mA_20; }
            set { strmAnalogOP_mA_20 = value; }
        }

        //Property for Analog op mA sensor 4mA
        public string StrmAnalogOP_mA_4
        {
            get { return strmAnalogOP_mA_4; }
            set { strmAnalogOP_mA_4 = value; }
        }

        //Property for Analog op mA sensor 1mA
        public string StrmAnalogOP_mA_1
        {
            get { return strmAnalogOP_mA_1; }
            set { strmAnalogOP_mA_1 = value; }
        }

        //Property for Analog ip Volt sensor 10V
        public string StrmAnlogIP_Volt_10
        {
            get { return strmAnlogIP_Volt_10; }
            set { strmAnlogIP_Volt_10 = value; }
        }

        //Property for Analog ip Volt sensor 5V
        public string StrmAnlogIP_Volt_5
        {
            get { return strmAnlogIP_Volt_5; }
            set { strmAnlogIP_Volt_5 = value; }
        }

        //Property for Analog ip Volt sensor 1V
        public string StrmAnlogIP_Volt_1
        {
            get { return strmAnlogIP_Volt_1; }
            set { strmAnlogIP_Volt_1 = value; }
        }

        //Property for Analog ip mA sensor 20 Value
        public string StrmAnlogIP_mA_20
        {
            get { return strmAnlogIP_mA_20; }
            set { strmAnlogIP_mA_20 = value; }
        }

        //Property for Analog ip mA sensor 12 Value
        public string StrmAnlogIP_mA_12
        {
            get { return strmAnlogIP_mA_12; }
            set { strmAnlogIP_mA_12 = value; }
        }

        //Property for Analog ip mA sensor 4 Value
        public string StrmAnlogIP_mA_4
        {
            get { return strmAnlogIP_mA_4; }
            set { strmAnlogIP_mA_4 = value; }
        }

        //Property for R sensor Zero Value
        public string StrmR_ZERO
        {
            get { return strmR_ZERO; }
            set { strmR_ZERO = value; }
        }

        //Property for R sensor 1000 Value
        public string StrmR_1000
        {
            get { return strmR_1000; }
            set { strmR_1000 = value; }
        }

        //Property for R sensor 1750 Value
        public string StrmR_1750
        {
            get { return strmR_1750; }
            set { strmR_1750 = value; }
        }

        //Property for PT100 sensor 700 Value
        public string StrmPT100_700
        {
            get { return strmPT100_700; }
            set { strmPT100_700 = value; }
        }

        //Property for PT100 sensor 400 Value
        public string StrmPT100_400
        {
            get { return strmPT100_400; }
            set { strmPT100_400 = value; }
        }

        //Property for PT100 sensor Zero Value
        public string StrmPT100_ZERO
        {
            get { return strmPT100_ZERO; }
            set { strmPT100_ZERO = value; }
        }
        //-------Changed By Shubham
        //Date:- 24-02-2018
        //Version:- V16
        //Property for Reference voltage read
        public string StrmRef_Vtg
        {
            get { return strmRef_Vtg; }
            set { strmRef_Vtg = value; }
        }

        ///<MemberName>Clear</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>4/01/2018</Date>
        ///<summary>
        ///This function resets the data of the members. sets the default value to NA.
        ///</summary>
        ///<ClassName>clsDataLog</ClassName>
        public void Clear()
        {
            try
            {
                dblgChkSum = 0;
                StrmPT100_ZERO = "0";
                StrmPT100_400 = "0";
                StrmPT100_700 = "0";
                StrmR_1750 = "0";
                StrmR_1000 = "0";
                StrmR_ZERO = "0";
                StrmAnlogIP_mA_4 = "0";
                StrmAnlogIP_mA_12 = "0";
                StrmAnlogIP_mA_20 = "0";
                StrmAnlogIP_Volt_1 = "0";
                StrmAnlogIP_Volt_5 = "0";
                StrmAnlogIP_Volt_10 = "0";
                StrmAnalogOP_mA_4 = "0";
                StrmAnalogOP_mA_1 = "0";
                StrmAnalogOP_mA_20 = "0";
                StrmAnalogOP_mA_12 = "0";
                StrmAnalogOP_Volt_1 = "0";
                StrmAnalogOP_Volt_10 = "0";
                strmAnalogOP_Volt_5 = "0";
                StrmJ_ZERO = "0";
                StrmJ_400 = "0";
                StrmJ_700 = "0";
                //StrmJ_NEG_100 = "0";
                //-------Changed By Shubham
                //Date:- 24-02-2018
                //Version:- V16
                //Statement:- Default value is stored in the variable.
                StrmRef_Vtg = "0";
                //------Changes end.

                //-------Changed By Shubham
                //Date:- 28-04-2018
                //Version:- V17
                //Statement:- Default value is stored in the variable.
                StrmCalibrationTime = "0";
                StrmAccuracyTime = "0";
                //--------Changes end.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);               
            }
        }

        ///<MemberName>WriteLogFile</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>4/01/2018</Date>
        ///<summary>
        ///This function writes the data into the log file(*.csv file).
        ///</summary>
        ///<ClassName>clsDataLog</ClassName>
        public void WriteLogFile(byte btmdata)
        {
            //double dblChecksum = 0;
            FileStream objFilestrm = null;
            StreamWriter objStrmwriter = null;
            StreamReader objStrmRdr = null;
            //string strmLine = "";
            //double dblTempSum = 0;
            List<string> strmfiles = new List<string>();

            try
            {
                //If directory is not present then directory is created.
                if (!Directory.Exists(Application.StartupPath + "\\Data Log"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Data Log"); 
                }
                switch (btmdata)
                {
                    //-------Changed By Shubham
                    //Date:- 28-04-2018
                    //Version:- V17
                    //Statement:- Time required for Calibration and Accuracy is added in the log file. 
                    //This case is for device having analog op sensor but does not contains analog input sensors.
                    case clsGlobalVariables.Case_WithAnalogOP_WithoutAnalogIP:
                       
                            string[] arrstrmHeader = { "TimeStamp", "Device No.", "PT100("+ clsGlobalVariables.shpPT100Zero +")", "PT100("+ clsGlobalVariables.shpPT100FourHundred +")", "PT100("+ clsGlobalVariables.shpPT100SevenHundred +")", "R ("+ clsGlobalVariables.shpR0 +")", "R ("+ clsGlobalVariables.shpR1000 +")", "R ("+ clsGlobalVariables.shpR1750 +")", "Analog_OP ("+ clsGlobalVariables.shp4mA +"mA)", "Analog_OP ("+ clsGlobalVariables.shp20mA +"mA)", "Analog_OP ("+ clsGlobalVariables.shp12mA +"mA)", "Analog_OP ("+ clsGlobalVariables.shp1Volt +"Volt)", "Analog_OP ("+ clsGlobalVariables.shp10Volt + "Volt)", "Analog_OP ("+ clsGlobalVariables.shp5Volt + "Volt)", "Calibration Time", "Accuracy Time" };
                            string[] arrstrmDataTobeupdated = {clsGlobalVariables.objDataLog.StrmPT100_ZERO, clsGlobalVariables.objDataLog.StrmPT100_400,
                                           clsGlobalVariables.objDataLog.StrmPT100_700   , clsGlobalVariables.objDataLog.StrmR_ZERO , clsGlobalVariables.objDataLog.StrmR_1000,
                                           clsGlobalVariables.objDataLog.StrmR_1750  , clsGlobalVariables.objDataLog.StrmAnalogOP_mA_4 ,
                                           clsGlobalVariables.objDataLog.StrmAnalogOP_mA_20  , clsGlobalVariables.objDataLog.StrmAnalogOP_mA_12 ,
                                           clsGlobalVariables.objDataLog.StrmAnalogOP_Volt_1 , clsGlobalVariables.objDataLog.StrmAnalogOP_Volt_10,
                                           clsGlobalVariables.objDataLog.StrmAnalogOP_Volt_5,clsGlobalVariables.objDataLog.StrmCalibrationTime,clsGlobalVariables.objDataLog.StrmAccuracyTime};
                            UpdateLog(arrstrmDataTobeupdated, arrstrmHeader, btmdata);
                        break;
                    //This case is for device without analog op and analog input sensors.
                    case clsGlobalVariables.Case_WithoutAnalogOP_WithoutAnalogIP:
                        string[] arrstrmHeader1 = { "TimeStamp", "Device No.", "PT100("+ clsGlobalVariables.shpPT100Zero +")", "PT100("+ clsGlobalVariables.shpPT100FourHundred +")", "PT100("+ clsGlobalVariables.shpPT100SevenHundred +")", "R ("+ clsGlobalVariables.shpR0 +")", "R ("+ clsGlobalVariables.shpR1000 +")", "R ("+ clsGlobalVariables.shpR1750 +")", "Calibration Time", "Accuracy Time"};
                        string[] arrstrmDataTobeupdated1 = {clsGlobalVariables.objDataLog.StrmPT100_ZERO , clsGlobalVariables.objDataLog.StrmPT100_400 ,
                                           clsGlobalVariables.objDataLog.StrmPT100_700 , clsGlobalVariables.objDataLog.StrmR_ZERO , clsGlobalVariables.objDataLog.StrmR_1000,
                                           clsGlobalVariables.objDataLog.StrmR_1750,clsGlobalVariables.objDataLog.StrmCalibrationTime,clsGlobalVariables.objDataLog.StrmAccuracyTime};
                        UpdateLog(arrstrmDataTobeupdated1, arrstrmHeader1, btmdata);
                        break;
                    //This case is for device having analog op sensor and analog input sensors.
                    case clsGlobalVariables.Case_WithAnalogOP_WithAnalogIP:
                        //-------Changed By Shubham
                        //Date:- 24-02-2018
                        //Version:- V16
                        //Statement:- "VREF" value will get stored in the log file.
                        string[] arrstrmHeader2 = { "TimeStamp", "Device No.", "PT100("+ clsGlobalVariables.shpPT100Zero +")", "PT100("+ clsGlobalVariables.shpPT100FourHundred +")", "PT100("+ clsGlobalVariables.shpPT100SevenHundred +")", "R ("+ clsGlobalVariables.shpR0 +")", " R ("+ clsGlobalVariables.shpR1000 +")", " R ("+ clsGlobalVariables.shpR1750 +")", "Analog_IP ("+ clsGlobalVariables.shp4mA +"mA)", "Analog_IP ("+ clsGlobalVariables.shp12mA +"mA)", "Analog_IP ("+ clsGlobalVariables.shp20mA +"mA)", "Analog_IP ("+ clsGlobalVariables.shp1Volt + "Volt)", "Analog_IP ("+ clsGlobalVariables.shp5Volt + "Volt)", "Analog_IP ("+ clsGlobalVariables.shp10Volt + "Volt)", " Analog_OP (4mA)", "Analog_OP (20mA)", " Analog_OP (12mA)", "Analog_OP (1V)", "Analog_OP (10V)", " Analog_OP (5V)", "VREF Value", "Calibration Time", "Accuracy Time" };
                        string[] arrstrmDataTobeupdated2 = {clsGlobalVariables.objDataLog.StrmPT100_ZERO , clsGlobalVariables.objDataLog.StrmPT100_400 ,
                                                    clsGlobalVariables.objDataLog.StrmPT100_700 , clsGlobalVariables.objDataLog.StrmR_ZERO , clsGlobalVariables.objDataLog.StrmR_1000 ,
                                                    clsGlobalVariables.objDataLog.StrmR_1750 , clsGlobalVariables.objDataLog.StrmAnlogIP_mA_4 , clsGlobalVariables.objDataLog.StrmAnlogIP_mA_12 ,
                                                    clsGlobalVariables.objDataLog.StrmAnlogIP_mA_20 , clsGlobalVariables.objDataLog.StrmAnlogIP_Volt_1 ,
                                                    clsGlobalVariables.objDataLog.StrmAnlogIP_Volt_5 , clsGlobalVariables.objDataLog.StrmAnlogIP_Volt_10 ,
                                                    clsGlobalVariables.objDataLog.StrmAnalogOP_mA_4 ,  clsGlobalVariables.objDataLog.StrmAnalogOP_mA_20 ,
                                                    clsGlobalVariables.objDataLog.StrmAnalogOP_mA_12 , clsGlobalVariables.objDataLog.StrmAnalogOP_Volt_1 ,
                                                    clsGlobalVariables.objDataLog.StrmAnalogOP_Volt_10 , clsGlobalVariables.objDataLog.StrmAnalogOP_Volt_5,
                                                    clsGlobalVariables.objDataLog.StrmRef_Vtg,clsGlobalVariables.objDataLog.StrmCalibrationTime,clsGlobalVariables.objDataLog.StrmAccuracyTime};
                        //----------Changes End.
                        UpdateLog(arrstrmDataTobeupdated2, arrstrmHeader2, btmdata);
                        break;
                    //This case is for device having analog input sensor but does not contains analog output sensors.
                    case clsGlobalVariables.Case_WithoutAnalogOP_WithAnalogIP:
                        //-------Changed By Shubham
                        //Date:- 24-02-2018
                        //Version:- V16
                        //Statement:- "VREF" value will get stored in the log file.
                        string[] arrstrmHeader3 = { "TimeStamp", "Device No.", "PT100("+ clsGlobalVariables.shpPT100Zero +")", "PT100("+ clsGlobalVariables.shpPT100FourHundred +")", "PT100("+ clsGlobalVariables.shpPT100SevenHundred +")", "R ("+ clsGlobalVariables.shpR0 +")", "R ("+ clsGlobalVariables.shpR1000 +")", "R ("+ clsGlobalVariables.shpR1750 +")", "Analog_IP ("+ clsGlobalVariables.shp4mA +"mA)", "Analog_IP ("+ clsGlobalVariables.shp12mA +"mA)", "Analog_IP ("+ clsGlobalVariables.shp20mA +"mA)", "Analog_IP ("+ clsGlobalVariables.shp1Volt + "Volt)", "Analog_IP ("+ clsGlobalVariables.shp5Volt + "Volt)", "Analog_IP ("+ clsGlobalVariables.shp10Volt + "Volt)", "VREF Value", "Calibration Time", "Accuracy Time" };
                        string[] arrstrmDataTobeupdated3 ={clsGlobalVariables.objDataLog.StrmPT100_ZERO , clsGlobalVariables.objDataLog.StrmPT100_400 ,
                                        clsGlobalVariables.objDataLog.StrmPT100_700 , clsGlobalVariables.objDataLog.StrmR_ZERO , clsGlobalVariables.objDataLog.StrmR_1000 ,
                                        clsGlobalVariables.objDataLog.StrmR_1750 , clsGlobalVariables.objDataLog.StrmAnlogIP_mA_4 , clsGlobalVariables.objDataLog.StrmAnlogIP_mA_12 ,
                                        clsGlobalVariables.objDataLog.StrmAnlogIP_mA_20 , clsGlobalVariables.objDataLog.StrmAnlogIP_Volt_1 , clsGlobalVariables.objDataLog.StrmAnlogIP_Volt_5 ,
                                        clsGlobalVariables.objDataLog.StrmAnlogIP_Volt_10, clsGlobalVariables.objDataLog.StrmRef_Vtg,clsGlobalVariables.objDataLog.StrmCalibrationTime,clsGlobalVariables.objDataLog.StrmAccuracyTime};
                        UpdateLog(arrstrmDataTobeupdated3, arrstrmHeader3, btmdata);
                        //----------Changes End.                   
                    //----------Changes End.  
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Resources are released
                if (objStrmRdr != null )
                {
                    objStrmRdr.Close();
                }
                if (objStrmwriter != null)
                {
                    objStrmwriter.Close();    
                }
                if (objFilestrm != null)
                {
                    objFilestrm.Close();
                }
            }
        }
        ///<MemberName>GetDeviceNumber</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>4/01/2018</Date>
        ///<summary>
        ///This function reads the device number field from the string passsed as a parameter to it and then 
        ///returns the device number by incrementing the device number. 
        ///</summary>
        ///<ClassName>clsDataLog</ClassName>
        private int GetDeviceNumber(string strdata)
        {
            try
            {
                //last line is read.
                int imIndex = strdata.Substring(0, strdata.Substring(0, strdata.LastIndexOf("\n")).LastIndexOf("\n")).LastIndexOf("\n");
                string strmlastline = strdata.Substring(imIndex + 1);
                string[] arrmdata = strmlastline.Split(arrmSeparator);
                //Device number is updated
                int imdeviceNo = Convert.ToInt32(arrmdata[1]) + 1;
                imIndex = strdata.Substring(0, strdata.LastIndexOf("\n")).LastIndexOf("\n");
                strmlastline = strdata.Substring(imIndex + 1);
                //Checksum present in the file is save in the global variable.
                dblgChkSum = Convert.ToDouble(strmlastline.Replace("\r\n", ""));
                return imdeviceNo;
            }
            catch (Exception)
            {
                //In case of error or exception this function returns -1.
                return -1;
            }
        }
        ///<MemberName>UpdateLog</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>11/01/2018</Date>
        ///<summary>
        ///This function updates the log file if present. If log file is not present then this function
        ///creats new file of log.
        ///This function has three arguments
        ///1. Array of data.
        ///2. Array of header
        ///3. switch case number(type of device) 
        ///</summary>
        ///<ClassName>clsDataLog</ClassName>
        private void UpdateLog(string[] arrstrmDataTobeupdated, string[] arrstrmHeader, byte btmcasedata)
        {
            FileStream objFilestrm = null;
            StreamWriter objStrmwriter = null;
            StreamReader objStrmRdr = null;
            double dblmChecksum =0;
            string strmData = "";
            int imIndex = 0;

            try
            {
                if (clsGlobalVariables.HeaderNeedToWriteForDataLog && File.Exists(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv"))
                {
                    clsGlobalVariables.HeaderNeedToWriteForDataLog = false;
                    
                    string[] arrfilePaths = Directory.GetFiles(Application.StartupPath + "\\Data Log\\", clsGlobalVariables.Selectedcatid.DeviceName + "*.csv");
                    File.Copy(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv", Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + "_" + (DateTime.Now.ToString().Replace('/', '_').Trim().Replace(':', '_')).Replace(' ', '_') + ".csv");
                    File.Delete(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv");
                    
                }
                //File is present or not is checked here.
                if (!File.Exists(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv"))
                {
                    //File is created here.
                    objFilestrm = new FileStream(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv", FileMode.Create);
                    objStrmwriter = new StreamWriter(objFilestrm);
                    //Header data is added in a sting variable.
                    for (imIndex = 0; imIndex < arrstrmHeader.Length; imIndex++)
                    {
                        if (imIndex == arrstrmHeader.Length - 1)
                        {
                            strmData = strmData + arrstrmHeader[imIndex];
                            break;
                        }
                        strmData = strmData + arrstrmHeader[imIndex] + ",";
                    }
                    //Header data is written in log file.
                    objStrmwriter.WriteLine(strmData);
                    //Actual data to be added in log is added in a sting variable.
                    strmData = DateTime.Now.ToString() + ",1,";
                    for (imIndex = 0; imIndex < arrstrmDataTobeupdated.Length; imIndex++)
                    {
                        if (imIndex == arrstrmDataTobeupdated.Length - 1)
                        {
                            strmData = strmData + arrstrmDataTobeupdated[imIndex];
                            break;
                        }
                        strmData = strmData + arrstrmDataTobeupdated[imIndex] + ",";
                    }
                    //Data is written into the log file.
                    objStrmwriter.WriteLine(strmData);
                    //Since device number is 1 for newly created file. So, 1 is added in the checksum.
                    dblmChecksum = 1;
                    //Check sum is calculated here.
                    for (imIndex = 0; imIndex < arrstrmDataTobeupdated.Length; imIndex++)
                    {
                        //-------Changed By Shubham
                        //Date:- 28-04-2018
                        //Version:- V17
                        //Statement:- This change is done to add the time in the checksum.
                        if (arrstrmDataTobeupdated[imIndex].Contains(":"))
                        {
                            arrstrmDataTobeupdated[imIndex] = arrstrmDataTobeupdated[imIndex].Replace(":", "");
                        }
                        //---------Changes End.
                        dblmChecksum = dblmChecksum + Convert.ToDouble(arrstrmDataTobeupdated[imIndex]);
                    }
                    //Checksum is written in the file.
                    objStrmwriter.WriteLine(dblmChecksum.ToString());
                }
                else
                {
                    string strmLine = "";
                    double dblTempSum = 0;

                    //File is read.
                    objStrmRdr = new StreamReader(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv");
                    //All data is read from the file.
                    strmData = objStrmRdr.ReadToEnd();
                    //Next Device number is returned from this function.
                    //Also this function reads the 
                    int imdeviceNo = GetDeviceNumber(strmData);
                    objStrmRdr.Close();
                    objStrmRdr = null;
                    //if file is present but empty then file is deleted and newly created.
                    if (strmData == "")
                    {
                        File.Delete(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv");
                        //This will create a new file.
                        WriteLogFile(btmcasedata);
                        return;
                    }

                    //If total record present in the file are 5000 then rename the existing file and create a new file with the name of ct id.
                    //Also if file gets corrupted then rename the existing file and create a new file with the name of ct id.
                    //meaning of -1 is file is corrupted.
                    if (imdeviceNo == 10001 || imdeviceNo == (-1))
                    {
                        string[] arrfilePaths = Directory.GetFiles(Application.StartupPath + "\\Data Log\\", clsGlobalVariables.Selectedcatid.DeviceName + "*.csv");
                        File.Copy(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv", Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + "_" + (DateTime.Now.ToString().Replace('/', '_').Trim().Replace(':', '_')).Replace(' ', '_') + ".csv");
                        File.Delete(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv");
                        //This will create a new file with headers. 
                        WriteLogFile(btmcasedata);
                        return;
                    }
                    //File is opened for reading here.
                    objStrmRdr = new StreamReader(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv");
                    //first header line is skipped. 
                    strmLine = objStrmRdr.ReadLine();
                    // This try block is added to check whether data is read from the file is number.
                    try
                    {
                        //Here checksum of all the data is calculated here.
                        while (!objStrmRdr.EndOfStream)
                        {
                            string[] arrmdata = objStrmRdr.ReadLine().Split(arrmSeparator);
                            for (imIndex = 1; imIndex < arrmdata.Length; imIndex++)
                            {
                                //-------Changed By Shubham
                                //Date:- 28-04-2018
                                //Version:- V17
                                //Statement:- This change is done to add the time in the checksum.
                                if (arrmdata[imIndex].Contains(":"))
                                {
                                    arrmdata[imIndex] = arrmdata[imIndex].Replace(":", "");
                                }
                                //--------Changes end.
                                dblTempSum = dblTempSum + Convert.ToDouble(arrmdata[imIndex]);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //Resources are released.
                        if (objStrmRdr != null)
                        {
                            objStrmRdr.Close();
                        }
                        if (objStrmwriter != null)
                        {
                            objStrmwriter.Close();
                        }
                        if (objFilestrm != null)
                        {
                            objFilestrm.Close();
                        }
                        string[] arrfilePaths = Directory.GetFiles(Application.StartupPath + "\\Data Log\\", clsGlobalVariables.Selectedcatid.DeviceName + "*.csv");
                        File.Copy(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv", Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + "_" + (DateTime.Now.ToString().Replace('/', '_').Trim().Replace(':', '_')).Replace(' ', '_') + ".csv");
                        File.Delete(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv");
                        //This will create new file with headers.
                        WriteLogFile(btmcasedata);
                        return;                        
                    }
                    objStrmRdr.Close();
                    //Data is rounded to the 2 digits.
                    dblTempSum = Math.Round(dblTempSum, 2);
                    //Data is rounded to the 2 digits.
                    dblgChkSum = Math.Round(dblgChkSum, 2);
                    //If checksum read from the file is matched with calculated checksum then update the data in the file.
                    if (dblTempSum == dblgChkSum)
                    {
                        string strmTempData = "";
                        System.IO.File.WriteAllText(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv", "");
                        objFilestrm = new FileStream(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv", FileMode.Append);
                        objStrmwriter = new StreamWriter(objFilestrm);
                        strmTempData = DateTime.Now.ToString() + "," + imdeviceNo.ToString() + ",";
                        //Data to be added is merged in the string
                        for (imIndex = 0; imIndex < arrstrmDataTobeupdated.Length; imIndex++)
                        {
                            if (imIndex == arrstrmDataTobeupdated.Length - 1)
                            {
                                strmTempData = strmTempData + arrstrmDataTobeupdated[imIndex];
                                break;
                            }
                            strmTempData = strmTempData + arrstrmDataTobeupdated[imIndex] + ",";
                        }
                        //Checksum of the data to be added is calculated here.
                        dblmChecksum = imdeviceNo;
                        for (imIndex = 0; imIndex < arrstrmDataTobeupdated.Length; imIndex++)
                        {
                            //-------Changed By Shubham
                            //Date:- 28-04-2018
                            //Version:- V17
                            //Statement:- This change is done to add the time in the checksum.
                            if (arrstrmDataTobeupdated[imIndex].Contains(":"))
                            {
                                arrstrmDataTobeupdated[imIndex] = arrstrmDataTobeupdated[imIndex].Replace(":", "");
                            }
                            //---------changes End.
                            dblmChecksum = dblmChecksum + Convert.ToDouble(arrstrmDataTobeupdated[imIndex]);
                        }
                        //Previous Checksum is added in the checksum.
                        dblmChecksum = dblmChecksum + dblgChkSum;

                        //Previous checksum is removed from the string
                        strmData = strmData.Remove(strmData.Substring(0, strmData.LastIndexOf("\n")).LastIndexOf("\n") + 1);
                        strmData = strmData + strmTempData;
                        //Data is updated in the log file 
                        objStrmwriter.WriteLine(strmData);
                        //Newly created checksum is updated in the file.
                        objStrmwriter.WriteLine(dblmChecksum.ToString());
                    }
                    else //If checksum read from the file is not matching with calculated checksum then new file is created.
                    {
                        string[] arrfilePaths = Directory.GetFiles(Application.StartupPath + "\\Data Log\\", clsGlobalVariables.Selectedcatid.DeviceName + "*.csv");
                        File.Copy(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv", Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + "_" + (DateTime.Now.ToString().Replace('/', '_').Trim().Replace(':', '_')).Replace(' ', '_') + ".csv");
                        File.Delete(Application.StartupPath + "\\Data Log\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".csv");
                        //This will create new file with headers.
                        WriteLogFile(btmcasedata);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Resources are released
                if (objStrmRdr != null)
                {
                    objStrmRdr.Close();
                }
                if (objStrmwriter != null)
                {
                    objStrmwriter.Close();
                }
                if (objFilestrm != null)
                {
                    objFilestrm.Close();
                }
            }
            finally
            {
                //Resources are released here
                if (objStrmRdr != null)
                {
                    objStrmRdr.Close();
                }
                if (objStrmwriter != null)
                {
                    objStrmwriter.Close();
                }
                if (objFilestrm != null)
                {
                    objFilestrm.Close();
                }
            }
        }
    }//End of class
}//End of namespace
