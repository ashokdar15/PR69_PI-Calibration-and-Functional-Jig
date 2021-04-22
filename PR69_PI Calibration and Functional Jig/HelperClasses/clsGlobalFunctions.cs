using System;
using System.Text;
using System.IO.Ports;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using PR69_PI_Calibration_and_Functional_Jig.ViewModel;
using System.Threading.Tasks;
using PR69_PI_Calibration_and_Functional_Jig.Views;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    /********************************************************************************************
              Class Name        : clsGlobalFunctions Class
              Purpose           : This class contains All global methods such as calculation of slope & offsets,
                                  conversion functions, display related, Auto com port detection, etc.  
              Date              : 1/06/2017
              Written By        : Shubham
              CopyRight         : General Industrial Controls Pvt. Ltd. Pune
              Modified          : Date                
              Released Version  :  V15
              Changed By        :  NA
              Decription Of Change:  NA
    ********************************************************************************************/
    public class clsGlobalFunctions
    {
        private System.Windows.Forms.Timer tmrApplyDelay = new System.Windows.Forms.Timer();
        public clsGlobalFunctions()
        {
            tmrApplyDelay.Tick += new System.EventHandler(this.tmrApplyDelay_Tick);
        }
       
        
        private void tmrApplyDelay_Tick(object sender, EventArgs e)
        {
            clsGlobalVariables.blngApplyDelayOver = true;
            tmrApplyDelay.Stop();
        }


        public void DisplayMessageBox(string Msg, string Msgtype)
        {
            DisplayMessage displayMessage = new DisplayMessage(Msg, Msgtype);
            displayMessage.ShowDialog();
        }

        /// <summary>
        /// <membername>GetAvailablePortName</membername>
        /// <membertype>Method</membertype>
        /// This function populates the combo box of the Available ports. 
        /// <MemberType>Method</MemberType>
        /// </summary>       
        /// 

        public void GetAvailablePortName(string PLCComPort)
        {
            clsGlobalVariables.algAvailableComPorts.Clear();
            ArrayList almListOfComPorts = new ArrayList();   //This List is taken to sort the ComPorts.  
            foreach (string strPort in System.IO.Ports.SerialPort.GetPortNames())
            {
                // Object of SerialPort class is declared to check if port is available to use or not.
                if (strPort != PLCComPort)
                {
                    SerialPort comport = new SerialPort(strPort);
                    try
                    {
                        almListOfComPorts.Add(strPort);
                        comport.Open();
                        comport.Close();
                    }
                    catch (Exception)
                    {
                        almListOfComPorts.Remove(strPort);
                        comport.Close();
                    }
                }
            }
            almListOfComPorts.Sort(); //Com Ports are sorted here
            foreach (var item in almListOfComPorts)
            {
                //This is a global list of com ports.
                clsGlobalVariables.algAvailableComPorts.Add(item); 
            }
        }// End of Function.

        ///<MemberName>CalculateChecksum</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///Here data checksum is calculated by addiotion of data sent in the second parameter to function.
        ///Default 5 is added in the checksum.
        ///</summary>
        ///<param name="btmDataLen">This is the data length upto which checksum is to be calculated.</param>
        ///<param name="btmDataBuffer">This is the list(Array) of data on which actual checksum is calculated.</param>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public byte CalculateChecksum(byte btmDataLen, ref byte[] btmDataBuffer)
        {
            try
            {
                byte btmLoopCntr;
                byte btmCheckSum;

                btmCheckSum = clsGlobalVariables.btgCheckSumDefaultConst; 
                for (btmLoopCntr = 0; btmLoopCntr <= btmDataLen - 1; ++btmLoopCntr)
                {
                    btmCheckSum = (byte)(btmCheckSum + btmDataBuffer[btmLoopCntr]);
                }
                return btmCheckSum;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        ///<MemberName>GetNumber</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function gives a number on which validations are done after the post function call.
        ///</summary>
        ///<param name="arrbtData">This is the data buffer from which a number is genereted by using below calculations.</param>
        ///<param name="btmDataPos">This is the position in the array from which data is taken.</param>
        ///<param name="btmNumOFBytes">This field tells that how many entries have to be checked after data posion variable value.</param>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public long GetNumber(ref byte[] arrbtData, byte btmDataPos, byte btmNumOFBytes)
        {
            long lmRetVal = 0;
            long lmMul = 1;
            byte btmCntr;

            btmCntr = btmNumOFBytes;

            while (btmCntr!=0)
            {
                lmRetVal = lmRetVal + arrbtData[btmDataPos + btmCntr - 1] * lmMul;
                lmMul = lmMul * 256;
                btmCntr = --btmCntr;
            }
            return lmRetVal;
        }

        ///<MemberName>ConvertStringToInt</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function takes string as parameter and converts that string into number variable.
        ///e.g. if string 5.22 is passed to this function then this function returns
        ///5 * 100 +22 = "522" value.
        ///</summary>
        ///<param name="strmData">This is a number present in the string format</param>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public int ConvertStringToInt(string strmData)
        {
            try
            {
                double dblmData;
                string strmTemp1, strmTemp2;
                dblmData = Convert.ToDouble(strmData);
                //Format of the number string is changed.
                strmData = string.Format("{0:0.000}", dblmData);
                int imIndex = strmData.IndexOf('.');
                strmTemp1 = strmData.Substring(0, imIndex);
                strmTemp2 = strmData.Substring(imIndex + 1, strmData.Length - imIndex - 1);

                if (strmTemp1 != "" && strmTemp2 != "")
                {
                    return (Convert.ToInt32(strmTemp1) * 1000) + Convert.ToInt32(strmTemp2);
                }                
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ValidateAnalogVal</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function valiates the analog voltage or current within fixed band. If voltage or current is not within the tolerance
        ///then this function does not returns success. Which value of current/voltage has to be validated is decided by the
        ///parameter passed to this function.
        ///</summary>
        ///<param name="btmData">This byte number is the switch case number sent by caller function.</param>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public byte ValidateAnalogVal(byte btmData)
        {
            byte btmRetVal;
            try
            {
                //Voltage ranges are added for Process indicator with _PI variable name
                //for One_Volt, FIVE_Volt, TEN_Volt, FOUR_mAMP, ONE_mAMP_case, TWELVE_mAMP, TWENTY_mAMP
                switch (btmData)
                {

                    case clsGlobalVariables.One_Volt:
                        if (clsGlobalVariables.selectedDeviceType ==clsGlobalVariables.SelectedDeviceType.PR69_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.One_VOLT_MIN) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.One_VOLT_MAX))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        else
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.One_VOLT_MIN_PI) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.One_VOLT_MAX_PI))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        break;

                    case clsGlobalVariables.FIVE_Volt:
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.FIVE_VOLT_MIN) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.FIVE_VOLT_MAX))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        else
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.FIVE_VOLT_MIN_PI) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.FIVE_VOLT_MAX_PI))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        break;

                    case clsGlobalVariables.TEN_Volt:
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.TEN_VOLT_MIN) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.TEN_VOLT_MAX))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        else
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.TEN_VOLT_MIN_PI) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.TEN_VOLT_MAX_PI))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        break;

                    case clsGlobalVariables.FOUR_mAMP:
                        if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.FOUR_mAMP_MIN) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.FOUR_mAMP_MAX))
                        {
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                        else
                        {
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        break;
                    case clsGlobalVariables.ONE_mAMP_case:
                        if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.ONE_mAMP_MIN_PI) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.ONE_mAMP_MAX_PI))
                        {

                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                        else
                        {
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        break;

                    case clsGlobalVariables.TWELVE_mAMP:
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.TWELVE_mA_MIN) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.TWELVE_mA_MAX))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        else
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.TWELVE_mA_MIN_PI) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.TWELVE_mA_MAX_PI))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        break;

                    case clsGlobalVariables.TWENTY_mAMP:
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.TWENTY_mAMP_MIN) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.TWENTY_mAMP_MAX))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        else
                        {
                            if ((clsModelSettings.imAnalOpVal < clsGlobalVariables.TWENTY_mAMP_MIN_PI) || (clsModelSettings.imAnalOpVal > clsGlobalVariables.TWENTY_mAMP_MAX_PI))
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                        }
                        break;

                    default:
                        btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        break;
                }
                return btmRetVal;
            }
            catch (Exception)
            {
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
        }

        ///<MemberName>AdjustModeOfDevice</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function adjusts mode of DUT according to variable passed to it as parameter(Start mode or Run Mode).
        ///This function sends query to either single acting device or double acting device.
        ///</summary>
        ///<param name="btmMode">This variable contains mode to be set in DUT.</param>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public byte AdjustModeOfDevice(byte btmMode,byte DUT)
        {
            byte btmRetVal;

            try
            {
                //This check is for device having modbus.                
                if (clsModelSettings.blnRS485Flag == true )
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.MBAdjustMode(btmMode, (byte)(DUT + clsGlobalVariables.MB_DUT_ID_WM_BASE));
                }
                else//Device without modbus
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(DUT + clsGlobalVariables.MB_SLAVE_ID_WO_BASE),clsGlobalVariables.ADJUST_MODE, btmMode);
                }
                return btmRetVal;
            }
            catch (Exception)
            {
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;   
            }
 
        }

        ///<MemberName>GetCounts</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function reads different counts from the device. Also this function puts device in both Start mode or Stop mode.
        ///Some fixed value delays are applied before and after Run mode. For double acting devices counts read have been validated.
        ///</summary>
        ///<param name="btmData">This byte number tells that which calibration is going on.</param>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public byte GetCounts(byte btmData)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            long lmData = 0;
            try
            {
                //DUT is put in start mode.
                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {
                    btmRetVal = AdjustModeOfDevice(clsGlobalVariables.START_MODE, DUT);

                }
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    //Delays are applied for signal stabilization.

                    {
                        if (btmData == clsGlobalVariables.MV_1_CNT || btmData == clsGlobalVariables.MV_50_CNT)
                        {
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.ONEmV_DELAY_AFTER_STARTMODE);
                        }

                        if (btmData == clsGlobalVariables.PT100_CNT)
                        {
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PT100_DELAY_AFTER_STARTMODE);
                        }

                        if (btmData == clsGlobalVariables.CALIB_4mA || btmData == clsGlobalVariables.CALIB_20mA)
                        {
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.FOURmA_DELAY_AFTER_STARTMODE);
                        }

                        if (btmData == clsGlobalVariables.CALIB_1V || btmData == clsGlobalVariables.CALIB_9V)
                        {
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.ONEVolt_DELAY_AFTER_STARTMODE);
                        }
                        //-------Changed By Shubham
                        //Date:- 27-02-2018
                        //Version:- V16
                        //Statement:- For VREF Start mode delay present in INI File is applied here.
                        if (btmData == clsGlobalVariables.CALIB_VREF)
                        {
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.VREF_READ_DELAY_STARTMODE);
                        }
                    }
                }
                else
                {
                    return btmRetVal;
                }
                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {
                    //DUT is put in run mode 
                    btmRetVal = AdjustModeOfDevice(clsGlobalVariables.RUN_MODE, DUT);
                }
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    //Delays are applied for signal stabilization.


                    if (btmData == clsGlobalVariables.MV_1_CNT || btmData == clsGlobalVariables.MV_50_CNT)
                    {
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.ONEmV_DELAY_AFTER_RUNMODE);
                    }

                    if (btmData == clsGlobalVariables.PT100_CNT)
                    {
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PT100_DELAY_AFTER_RUNMODE);
                    }

                    if (btmData == clsGlobalVariables.CALIB_4mA || btmData == clsGlobalVariables.CALIB_20mA)
                    {
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.FOURmA_DELAY_AFTER_RUNMODE);
                    }

                    if (btmData == clsGlobalVariables.CALIB_1V || btmData == clsGlobalVariables.CALIB_9V)
                    {
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.ONEVolt_DELAY_AFTER_RUNMODE);
                    }
                    //-------Changed By Shubham
                    //Date:- 27-02-2018
                    //Version:- V16
                    //Statement:- For VREF Run mode delay present in INI File is applied here.
                    if (btmData == clsGlobalVariables.CALIB_VREF)
                    {
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.VREF_READ_DELAY_RUNMODE);
                    }
                    //---------Changes End.

                }
                else
                {
                    return btmRetVal;
                }

                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {
                    //-------Changed By Shubham
                    //Date:- 27-02-2018
                    //Version:- V16
                    //Statement:- For VREF Calibration source should be OFF. So, Source OFF is cheked.
                    if (btmData == clsGlobalVariables.CALIB_VREF)
                    {
                        //Source OFF is checked. 
                        if (clsGlobalVariables.objCalibQueriescls.CheckSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                    else
                    {
                        //Source on is checked. 
                        if (clsGlobalVariables.objCalibQueriescls.CheckSourceON(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                    //--------Changes End.

                    //This check is for device having modbus.                
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBStartCalibration(clsGlobalVariables.GET_COUNTS, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.CALIBRATE_FUNC_CODE, btmData);
                    }

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        //This check is for device having modbus.                    
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            //For double acting device software validates the data received from the device.
                            btmRetVal = ValidateCounts(clsGlobalVariables.btgRxBuffer, btmData, DUT);
                            if (btmRetVal  != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                return btmRetVal;
                            }
                            
                        }
                        else//Device without modbus
                        {
                            //For single acting device does not read data or validates the data.
                            //Device itself does the calculations, software checks only valid response.
                            lmData = GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 1);

                            if (lmData != 2)
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                        }
                    }
                    else
                    {
                        return btmRetVal;
                    }
                }
            }
            catch (Exception)
            {
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
            return btmRetVal;
        }

        ///<MemberName>ValidateCounts</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function gets executed in case of Double acting devices because validation of the counts received from the DUT is
        ///done in the software side.
        ///</summary>
        ///<param name="arrbtmdata">This is the bytes data array received from the DUT.</param>
        ///<param name="btmData">This byte number tells that for which calibration data has to be validated.</param>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public byte ValidateCounts(byte[] arrbtmdata,byte btmdata,byte DUT)
        {
            try
            {
                long[,] lngvCount = new long[4, 8];
                switch (DUT)
                {
                    case 1:
                        lngvCount = clsGlobalVariables.lngvCountDUT1;
                        break;
                    case 2:
                        lngvCount = clsGlobalVariables.lngvCountDUT2;
                        break;
                    case 3:
                        lngvCount = clsGlobalVariables.lngvCountDUT3;
                        break;
                    case 4:
                        lngvCount = clsGlobalVariables.lngvCountDUT4;
                        break;

                }
                if ((btmdata == clsGlobalVariables.CALIB_1V) || (btmdata == clsGlobalVariables.CALIB_9V) || (btmdata == clsGlobalVariables.CALIB_4mA) || (btmdata == clsGlobalVariables.CALIB_20mA) || (btmdata == clsGlobalVariables.CALIB_VREF))
                {
                    //Counts are converted in the long variable.
                    lngvCount[clsGlobalVariables.REF1_CNT, (btmdata - 4)] = ConvertCounts(ref arrbtmdata, (3 + (4 * clsGlobalVariables.REF1_CNT)), 4);
                    lngvCount[clsGlobalVariables.SIGNAL_CNT, (btmdata - 4)] = ConvertCounts(ref arrbtmdata, (3 + (4 * clsGlobalVariables.SIGNAL_CNT)), 4);
                    lngvCount[clsGlobalVariables.LD_CJC_CNT, (btmdata - 4)] = ConvertCounts(ref arrbtmdata, (3 + (4 * clsGlobalVariables.LD_CJC_CNT)), 4);
                    lngvCount[clsGlobalVariables.REF2_CNT, (btmdata - 4)] = ConvertCounts(ref arrbtmdata, (3 + (4 * clsGlobalVariables.REF2_CNT)), 4);
                }
                else
                {
                    //Counts are converted in the long variable.
                    lngvCount[clsGlobalVariables.REF1_CNT, btmdata] = ConvertCounts(ref arrbtmdata, (3 + (4 * clsGlobalVariables.REF1_CNT)), 4);
                    lngvCount[clsGlobalVariables.SIGNAL_CNT, btmdata] = ConvertCounts(ref arrbtmdata, (3 + (4 * clsGlobalVariables.SIGNAL_CNT)), 4);
                    lngvCount[clsGlobalVariables.LD_CJC_CNT, btmdata] = ConvertCounts(ref arrbtmdata, (3 + (4 * clsGlobalVariables.LD_CJC_CNT)), 4);
                    lngvCount[clsGlobalVariables.REF2_CNT, btmdata] = ConvertCounts(ref arrbtmdata, (3 + (4 * clsGlobalVariables.REF2_CNT)), 4);
                }                

                //Validations of counts for 1 mV is done here
                if (btmdata == clsGlobalVariables.MV_1_CNT)
                {
                    if (((lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_1_CNT] < clsGlobalVariables.REF1_CNT_MIN_1mV) || (lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_1_CNT] > clsGlobalVariables.REF1_CNT_MAX_1mV)) ||
                        ((lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.MV_1_CNT] < clsGlobalVariables.REF2_CNT_MIN_1mV) || (lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.MV_1_CNT] > clsGlobalVariables.REF2_CNT_MAX_1mV)))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.REF_CH_ERR);   
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.MV_1_CNT] < clsGlobalVariables.SIGNAL_CNT_MIN_1mV) || (lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.MV_1_CNT] > clsGlobalVariables.SIGNAL_CNT_MAX_1mV))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.SIG_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.LD_CJC_CNT, clsGlobalVariables.MV_1_CNT] < clsGlobalVariables.LD_CJC_CNT_MIN_1mV) || (lngvCount[clsGlobalVariables.LD_CJC_CNT, clsGlobalVariables.MV_1_CNT] > clsGlobalVariables.LD_CJC_CNT_MAX_1mV))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CJC_CNT_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }
                //Validations of counts for 50 mV is done here
                if (btmdata == clsGlobalVariables.MV_50_CNT)
                {
                    if (((lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_50_CNT] < clsGlobalVariables.REF1_CNT_MIN_50mV) || (lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_50_CNT] > clsGlobalVariables.REF1_CNT_MAX_50mV)) ||
                        ((lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.MV_50_CNT] < clsGlobalVariables.REF2_CNT_MIN_50mV) || (lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.MV_50_CNT] > clsGlobalVariables.REF2_CNT_MAX_50mV)))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.REF_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.MV_50_CNT] < clsGlobalVariables.SIGNAL_CNT_MIN_50mV) || (lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.MV_50_CNT] > clsGlobalVariables.SIGNAL_CNT_MAX_50mV))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.SIG_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.LD_CJC_CNT, clsGlobalVariables.MV_50_CNT] < clsGlobalVariables.LD_CJC_CNT_MIN_50mV) || (lngvCount[clsGlobalVariables.LD_CJC_CNT, clsGlobalVariables.MV_50_CNT] > clsGlobalVariables.LD_CJC_CNT_MAX_50mV))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CJC_CNT_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }
                //Validations of counts for PT100(350 Ohm) is done here
                if (btmdata == clsGlobalVariables.PT100_CNT)
                {
                    if (((lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.PT100_CNT] < clsGlobalVariables.REF1_CNT_MIN_350Ohm) || (lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.PT100_CNT] > clsGlobalVariables.REF1_CNT_MAX_350Ohm)) ||
                        ((lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.PT100_CNT] < clsGlobalVariables.REF2_CNT_MIN_350Ohm) || (lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.PT100_CNT] > clsGlobalVariables.REF2_CNT_MAX_350Ohm)))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.REF_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.PT100_CNT] < clsGlobalVariables.SIGNAL_CNT_MIN_350Ohm) || (lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.PT100_CNT] > clsGlobalVariables.SIGNAL_CNT_MAX_350Ohm))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.SIG_CH_ERR);
	                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }                
                    if((lngvCount[clsGlobalVariables.LD_CJC_CNT, clsGlobalVariables.PT100_CNT] < clsGlobalVariables.LD_CJC_CNT_MIN_350Ohm) || (lngvCount[clsGlobalVariables.LD_CJC_CNT, clsGlobalVariables.PT100_CNT] > clsGlobalVariables.LD_CJC_CNT_MAX_350Ohm))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CJC_CNT_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }
                //Validations of counts for 4mA Input calibration is done here
                if (btmdata == clsGlobalVariables.CALIB_4mA)
                {
                    if (((lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.FOURMA_Index] < clsGlobalVariables.REF1_CNT_MIN_4mA) || (lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.FOURMA_Index] > clsGlobalVariables.REF1_CNT_MAX_4mA)) ||
                        ((lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.FOURMA_Index] < clsGlobalVariables.REF2_CNT_MIN_4mA) || (lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.FOURMA_Index] > clsGlobalVariables.REF2_CNT_MAX_4mA)))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.REF_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.FOURMA_Index] < clsGlobalVariables.SIGNAL_CNT_MIN_4mA) || (lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.FOURMA_Index] > clsGlobalVariables.SIGNAL_CNT_MAX_4mA))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.SIG_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }
                //Validations of counts for 20mA Input calibration is done here
                if (btmdata == clsGlobalVariables.CALIB_20mA)
                {
                    if (((lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.TWENTYMA_Index] < clsGlobalVariables.REF1_CNT_MIN_20mA) || (lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.TWENTYMA_Index] > clsGlobalVariables.REF1_CNT_MAX_20mA)) ||
                        ((lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.TWENTYMA_Index] < clsGlobalVariables.REF2_CNT_MIN_20mA) || (lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.TWENTYMA_Index] > clsGlobalVariables.REF2_CNT_MAX_20mA)))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.REF_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.TWENTYMA_Index] < clsGlobalVariables.SIGNAL_CNT_MIN_20mA) || (lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.TWENTYMA_Index] > clsGlobalVariables.SIGNAL_CNT_MAX_20mA))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.SIG_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Success;                    
                }
                //Validations of counts for 1Volt Input calibration is done here
                if (btmdata == clsGlobalVariables.CALIB_1V)
                {
                    if (((lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.ONEVOLT_Index] < clsGlobalVariables.REF1_CNT_MIN_1V) || (lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.ONEVOLT_Index] > clsGlobalVariables.REF1_CNT_MAX_1V)) || 
                        ((lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.ONEVOLT_Index] < clsGlobalVariables.REF2_CNT_MIN_1V) || (lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.ONEVOLT_Index] > clsGlobalVariables.REF2_CNT_MAX_1V)))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.REF_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.ONEVOLT_Index] < clsGlobalVariables.SIGNAL_CNT_MIN_1V) || (lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.ONEVOLT_Index] > clsGlobalVariables.SIGNAL_CNT_MAX_1V))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.SIG_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Success;                    
                }
                //Validations of counts for 9Volt Input calibration is done here
                if (btmdata == clsGlobalVariables.CALIB_9V)
                {
                    if (((lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.NINEVOLT_Index] < clsGlobalVariables.REF1_CNT_MIN_9V) || (lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.NINEVOLT_Index] > clsGlobalVariables.REF1_CNT_MAX_9V)) ||
                        ((lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.NINEVOLT_Index] < clsGlobalVariables.REF2_CNT_MIN_9V) || (lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.NINEVOLT_Index] > clsGlobalVariables.REF2_CNT_MAX_9V)))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.REF_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.NINEVOLT_Index] < clsGlobalVariables.SIGNAL_CNT_MIN_9V) || (lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.NINEVOLT_Index] > clsGlobalVariables.SIGNAL_CNT_MAX_9V))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.SIG_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Success;           
                }
                //Statement:- Validations of counts for VREF is done here.
                if (btmdata == clsGlobalVariables.CALIB_VREF)
                {
                    if (((lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.VREF_Index] < clsGlobalVariables.REF1_CNT_MIN_VREF) || (lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.VREF_Index] > clsGlobalVariables.REF1_CNT_MAX_VREF)) ||
                        ((lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.VREF_Index] < clsGlobalVariables.REF2_CNT_MIN_VREF) || (lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.VREF_Index] > clsGlobalVariables.REF2_CNT_MAX_VREF)))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.REF_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    if ((lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.VREF_Index] < clsGlobalVariables.SIGNAL_CNT_MIN_VREF) || (lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.VREF_Index] > clsGlobalVariables.SIGNAL_CNT_MAX_VREF))
                    {
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.SIG_CH_ERR);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
            catch (Exception)
            {
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
        }

        internal byte AutomaticCOMPortDetections(int numberOfDUTs)
        {
            if (clsGlobalVariables.NUMBER_OF_DUTS != clsGlobalVariables.OLD_NUMBER_OF_DUTS)
            {
                clsGlobalVariables.blngIsComportDetected = false;
                clsGlobalVariables.blngIsComportDetectedForPLC = false;
            }
            if (PLCCOM_Detection())
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (clsGlobalVariables.objGlobalFunction.AutoComPortDetection()!= (byte)clsGlobalVariables.enmResponseError.Success)
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            //Calibrator Port detection
            //Jig
            //PLC port detections
            clsGlobalVariables.OLD_NUMBER_OF_DUTS = clsGlobalVariables.NUMBER_OF_DUTS;
            return (byte)clsGlobalVariables.enmResponseError.Success;
        }
        public bool PLCCOM_Detection()
        {


            if (clsGlobalVariables.blngIsComportDetectedForPLC == false)
            {
                byte btmRetVal;

                btmRetVal = clsGlobalVariables.objGlobalFunction.AutoComPortDetectionPLC();
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return true;
                }
                else
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.PLC_COM_PORT);
                    clsGlobalVariables.blngIsComportDetectedForPLC = true;
                    if (PLC_ON_OFF_QUERY(true))
                        return true;
                }

            }
            else
            {
                clsGlobalVariables.objGlobalFunction.PLC_ON();
            }


            return false;
        }

        public byte AutoComPortDetectionPLC()
        {
            try
            {
                clsGlobalVariables.algAvailableComPorts.Clear();
                GetAvailablePortName("");
                if (clsGlobalVariables.algAvailableComPorts.Count != 0)
                {
                    clsGlobalVariables.strgComPortPLC = "";
                    foreach (var item in clsGlobalVariables.algAvailableComPorts)
                    {
                        if (MainWindowVM.initilizeCommonObject.objplcSerialComm.OpenCommPort(item.ToString(), false))
                        {
                            //This query is sent to the calibrator to check that plc is connected to this port.
                            byte btmRetVal = clsGlobalVariables.objPLCQueriescls.AutoComPortQueryForPLC();
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.strgComPortPLC = item.ToString();

                                MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();
                                return (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                            MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();
                        }
                    }
                }
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
            catch (Exception ex)
            {
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
        }

        public bool PLC_ON()
        {
            return PLC_ON_OFF_QUERY(true);
        }
        public bool PLC_OFF()
        {
            return PLC_ON_OFF_QUERY(false);
        }
        public bool PLC_ON_OFF_QUERY(bool flag)
        {
            if (clsGlobalVariables.strgComPortPLC != "")
            {
                try
                {
                    byte btmRetVal;
                    if (MainWindowVM.initilizeCommonObject.objplcSerialComm.OpenCommPort(clsGlobalVariables.strgComPortPLC, false))
                    {
                        btmRetVal = clsGlobalVariables.objPLCQueriescls.DutONOFFQueryToPLC(flag);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            return true;
                    }
                    else
                        return true;
                    MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();
                    if (flag)
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ON_TIME_DELAY);
                }
                catch (Exception ex)
                {
                    MyLogWriterDLL.LogWriter.WriteLog(ex.Message + ex.StackTrace);

                }
            }
            return false;
        }

        public void ApplyDelay(int imDelay)
        {
            tmrApplyDelay.Stop();
            tmrApplyDelay.Interval = imDelay;
            tmrApplyDelay.Start();

            while (clsGlobalVariables.blngApplyDelayOver == false)
            {
                Application.DoEvents();
            }
            clsGlobalVariables.blngApplyDelayOver = false;
        }


        ///<MemberName>ConvertCounts</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///DUT send counts of "Signal", "Ref1", "Ref2", "LD_Cjc" in group of 4 bytes. This function combines 4 bytes 
        ///and convert it in long variable and returns that variabale.
        ///</summary>
        ///<param name="btmarrData">This is the bytes data array received from the DUT.</param>
        ///<param name="btmDataPos">This byte number tells starting location from which data has to be taken.</param>
        ///<param name="btmNumOfBytes">This byte number tells length of the data is to be converted.</param>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public long ConvertCounts(ref byte[] btmarrData, byte btmDataPos, byte btmNumOfBytes)
        {
            long lmRetVal;
            double dblmMul;
            byte btmCntr;

            try
            {
                dblmMul = 1;
                lmRetVal = 0;

                for ( btmCntr = 0; btmCntr < btmNumOfBytes; ++btmCntr)
                {
                    lmRetVal = Convert.ToInt64(lmRetVal + btmarrData[(btmDataPos + btmCntr)] * dblmMul);
                    dblmMul = dblmMul * 256;
                }
                return lmRetVal;
            }
            catch (Exception)
            {
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data; 
            }
        }

        ///<MemberName>CalSlopeOffset</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function calulates slope and offset and stores that in the global variables.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void CalSlopeOffset(byte DUT)
        {
            float flmInterVal1, flmInterval2;

            try
            {
                long[,] lngvCount = new long[4, 8];
                switch (DUT)
                {
                    case 1:
                        lngvCount = clsGlobalVariables.lngvCountDUT1;
                        break;
                    case 2:
                        lngvCount = clsGlobalVariables.lngvCountDUT2;
                        break;
                    case 3:
                        lngvCount = clsGlobalVariables.lngvCountDUT3;
                        break;
                    case 4:
                        lngvCount = clsGlobalVariables.lngvCountDUT4;
                        break;

                }
                flmInterVal1 = (float)(lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.MV_1_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_1_CNT]) /
                                (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.MV_1_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_1_CNT]);

                flmInterval2 = (float)(lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.MV_50_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_50_CNT]) /
                               (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.MV_50_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_50_CNT]);



                switch (DUT)
                {
                    case 1:
                        clsGlobalVariables.fltgvPrConfigSlopeDUT1 = (float)(clsGlobalVariables.CONST_CALIB_V1 - clsGlobalVariables.CONST_CALIB_V2) / (float)((flmInterVal1 - flmInterval2) * 1000);
                        clsGlobalVariables.fltgvPrConfigOffsetDUT1 = ((float)clsGlobalVariables.CONST_CALIB_V1 / (float)(1000 * clsGlobalVariables.fltgvPrConfigSlopeDUT1)) - flmInterVal1;
                        break;
                    case 2:
                        clsGlobalVariables.fltgvPrConfigSlopeDUT2 = (float)(clsGlobalVariables.CONST_CALIB_V1 - clsGlobalVariables.CONST_CALIB_V2) / (float)((flmInterVal1 - flmInterval2) * 1000);
                        clsGlobalVariables.fltgvPrConfigOffsetDUT2 = ((float)clsGlobalVariables.CONST_CALIB_V1 / (float)(1000 * clsGlobalVariables.fltgvPrConfigSlopeDUT2)) - flmInterVal1;
                        break;
                    case 3:
                        clsGlobalVariables.fltgvPrConfigSlopeDUT3 = (float)(clsGlobalVariables.CONST_CALIB_V1 - clsGlobalVariables.CONST_CALIB_V2) / (float)((flmInterVal1 - flmInterval2) * 1000);
                        clsGlobalVariables.fltgvPrConfigOffsetDUT3 = ((float)clsGlobalVariables.CONST_CALIB_V1 / (float)(1000 * clsGlobalVariables.fltgvPrConfigSlopeDUT3)) - flmInterVal1;
                        break;
                    case 4:
                        clsGlobalVariables.fltgvPrConfigSlopeDUT4 = (float)(clsGlobalVariables.CONST_CALIB_V1 - clsGlobalVariables.CONST_CALIB_V2) / (float)((flmInterVal1 - flmInterval2) * 1000);
                        clsGlobalVariables.fltgvPrConfigOffsetDUT4 = ((float)clsGlobalVariables.CONST_CALIB_V1 / (float)(1000 * clsGlobalVariables.fltgvPrConfigSlopeDUT4)) - flmInterVal1;
                        break;

                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///<MemberName>CalSlopeOffset420mA</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function is written to calculate slope and offsets for 4 & 20mA calibration by using counts received from DUT.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void CalSlopeOffset420mA(byte DUT)
        {
            float flmInterVal1, flmInterval2;

            try
            {
                long[,] lngvCount = new long[4, 8];
                switch (DUT)
                {
                    case 1:
                        lngvCount = clsGlobalVariables.lngvCountDUT1;
                        break;
                    case 2:
                        lngvCount = clsGlobalVariables.lngvCountDUT2;
                        break;
                    case 3:
                        lngvCount = clsGlobalVariables.lngvCountDUT3;
                        break;
                    case 4:
                        lngvCount = clsGlobalVariables.lngvCountDUT4;
                        break;

                }
                flmInterVal1 = (float)(lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.FOURMA_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.FOURMA_Index]) /
                                (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.FOURMA_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.FOURMA_Index]);

                flmInterval2 = (float)(lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.TWENTYMA_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.TWENTYMA_Index]) /
                               (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.TWENTYMA_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.TWENTYMA_Index]);
                switch (DUT)
                {
                    case 1:
                        clsGlobalVariables.fltgv4to20mAConfigSlopeDUT1 = (float)(clsGlobalVariables.CONST_CALIB_4mA - clsGlobalVariables.CONST_CALIB_20mA) / (float)(flmInterVal1 - flmInterval2);
                        clsGlobalVariables.fltgv4to20mAConfigOffsetDUT1 = ((float)clsGlobalVariables.CONST_CALIB_4mA / (float)(clsGlobalVariables.fltgv4to20mAConfigSlopeDUT1)) - flmInterVal1;

                        break;
                    case 2:
                        clsGlobalVariables.fltgv4to20mAConfigSlopeDUT2 = (float)(clsGlobalVariables.CONST_CALIB_4mA - clsGlobalVariables.CONST_CALIB_20mA) / (float)(flmInterVal1 - flmInterval2);
                        clsGlobalVariables.fltgv4to20mAConfigOffsetDUT2 = ((float)clsGlobalVariables.CONST_CALIB_4mA / (float)(clsGlobalVariables.fltgv4to20mAConfigSlopeDUT2)) - flmInterVal1;
                        break;
                    case 3:
                        clsGlobalVariables.fltgv4to20mAConfigSlopeDUT3 = (float)(clsGlobalVariables.CONST_CALIB_4mA - clsGlobalVariables.CONST_CALIB_20mA) / (float)(flmInterVal1 - flmInterval2);
                        clsGlobalVariables.fltgv4to20mAConfigOffsetDUT3 = ((float)clsGlobalVariables.CONST_CALIB_4mA / (float)(clsGlobalVariables.fltgv4to20mAConfigSlopeDUT3)) - flmInterVal1;
                        break;
                    case 4:
                        clsGlobalVariables.fltgv4to20mAConfigSlopeDUT4 = (float)(clsGlobalVariables.CONST_CALIB_4mA - clsGlobalVariables.CONST_CALIB_20mA) / (float)(flmInterVal1 - flmInterval2);
                        clsGlobalVariables.fltgv4to20mAConfigOffsetDUT4 = ((float)clsGlobalVariables.CONST_CALIB_4mA / (float)(clsGlobalVariables.fltgv4to20mAConfigSlopeDUT4)) - flmInterVal1;
                        break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>CalSlopeOffset110V</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function is written to calculate slope and offsets for 1 & 9V calibration by using counts received from DUT.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void CalSlopeOffset110V(byte DUT)
        {
            float flmInterVal1, flmInterval2;

            try
            {
                long[,] lngvCount = new long[4, 8];
                switch (DUT)
                {
                    case 1:
                        lngvCount = clsGlobalVariables.lngvCountDUT1;
                        break;
                    case 2:
                        lngvCount = clsGlobalVariables.lngvCountDUT2;
                        break;
                    case 3:
                        lngvCount = clsGlobalVariables.lngvCountDUT3;
                        break;
                    case 4:
                        lngvCount = clsGlobalVariables.lngvCountDUT4;
                        break;

                }
                flmInterVal1 = (float)(lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.ONEVOLT_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.ONEVOLT_Index]) /
                                (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.ONEVOLT_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.ONEVOLT_Index]);

                flmInterval2 = (float)(lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.NINEVOLT_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.NINEVOLT_Index]) /
                               (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.NINEVOLT_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.NINEVOLT_Index]);
                switch (DUT)
                {
                    case 1:
                        clsGlobalVariables.fltgv1to9VConfigSlopeDUT1 = (float)(clsGlobalVariables.CONST_CALIB_1V - clsGlobalVariables.CONST_CALIB_9V) / (float)((flmInterVal1 - flmInterval2));
                        clsGlobalVariables.fltgv1to9VConfigOffsetDUT1 = ((float)clsGlobalVariables.CONST_CALIB_1V / (float)(clsGlobalVariables.fltgv1to9VConfigSlopeDUT1)) - flmInterVal1;

                        break;
                    case 2:
                        clsGlobalVariables.fltgv1to9VConfigSlopeDUT2 = (float)(clsGlobalVariables.CONST_CALIB_1V - clsGlobalVariables.CONST_CALIB_9V) / (float)((flmInterVal1 - flmInterval2));
                        clsGlobalVariables.fltgv1to9VConfigOffsetDUT2 = ((float)clsGlobalVariables.CONST_CALIB_1V / (float)(clsGlobalVariables.fltgv1to9VConfigSlopeDUT2)) - flmInterVal1;
                        break;
                    case 3:
                        clsGlobalVariables.fltgv1to9VConfigSlopeDUT3 = (float)(clsGlobalVariables.CONST_CALIB_1V - clsGlobalVariables.CONST_CALIB_9V) / (float)((flmInterVal1 - flmInterval2));
                        clsGlobalVariables.fltgv1to9VConfigOffsetDUT3 = ((float)clsGlobalVariables.CONST_CALIB_1V / (float)(clsGlobalVariables.fltgv1to9VConfigSlopeDUT3)) - flmInterVal1;
                        break;
                    case 4:
                        clsGlobalVariables.fltgv1to9VConfigSlopeDUT4 = (float)(clsGlobalVariables.CONST_CALIB_1V - clsGlobalVariables.CONST_CALIB_9V) / (float)((flmInterVal1 - flmInterval2));
                        clsGlobalVariables.fltgv1to9VConfigOffsetDUT4 = ((float)clsGlobalVariables.CONST_CALIB_1V / (float)(clsGlobalVariables.fltgv1to9VConfigSlopeDUT4)) - flmInterVal1;
                        break;

                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>Calc_Cjc_Offset</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function calulates cjc offset value and stores that in the global variables.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void Calc_Cjc_Offset(byte DUT)
        {
            float flmInterVal, flmCJCLdCmpAnlOp;

            try
            {
                long[,] lngvCount = new long[4, 8];
                switch (DUT)
                {
                    case 1:
                        lngvCount = clsGlobalVariables.lngvCountDUT1;
                        break;
                    case 2:
                        lngvCount = clsGlobalVariables.lngvCountDUT2;
                        break;
                    case 3:
                        lngvCount = clsGlobalVariables.lngvCountDUT3;
                        break;
                    case 4:
                        lngvCount = clsGlobalVariables.lngvCountDUT4;
                        break;

                }

                flmInterVal = (float)(lngvCount[clsGlobalVariables.LD_CJC_CNT, clsGlobalVariables.MV_50_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_50_CNT]) /
                               (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.MV_1_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.MV_1_CNT]);

                flmCJCLdCmpAnlOp = clsGlobalVariables.fltgvPrConfigSlopeDUT1 * (flmInterVal + clsGlobalVariables.fltgvPrConfigOffsetDUT1);
                //"3852.71616F" & "49.25279F" -> These values are coming from CJC Calculation document. These 
                //values are fixed value used for Voltage to temperature conversion of CJC IC. 
                flmCJCLdCmpAnlOp = flmCJCLdCmpAnlOp * 3852.71616F - 49.25279F;
                switch (DUT)
                {
                    case 1:
                        clsGlobalVariables.fltgvPrConfigCjcDUT1 = flmCJCLdCmpAnlOp - (clsGlobalVariables.igvProcessValue) + 2;
                        break;
                    case 2:
                        clsGlobalVariables.fltgvPrConfigCjcDUT2 = flmCJCLdCmpAnlOp - (clsGlobalVariables.igvProcessValue) + 2;
                        break;
                    case 3:
                        clsGlobalVariables.fltgvPrConfigCjcDUT3 = flmCJCLdCmpAnlOp - (clsGlobalVariables.igvProcessValue) + 2;
                        break;
                    case 4:
                        clsGlobalVariables.fltgvPrConfigCjcDUT4 = flmCJCLdCmpAnlOp - (clsGlobalVariables.igvProcessValue) + 2;
                        break;

                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>Calc_VREF</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>26/02/2018</Date>
        ///<summary>
        ///This function calulates VREF Value from the counts received from the device.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void Calc_VREF(byte DUT)
        {
            float flmInterVal;

            try
            {
                long[,] lngvCount = new long[4, 8];
                switch (DUT)
                {
                    case 1:
                        lngvCount = clsGlobalVariables.lngvCountDUT1;
                        break;
                    case 2:
                        lngvCount = clsGlobalVariables.lngvCountDUT2;
                        break;
                    case 3:
                        lngvCount = clsGlobalVariables.lngvCountDUT3;
                        break;
                    case 4:
                        lngvCount = clsGlobalVariables.lngvCountDUT4;
                        break;
                }

                //The reference voltage calculation is done with the help of sensing circuit of 0 -10V
                //analog sensor.The input to this section is kept open, so that only Vref=~2.5V will act as
                //source to that sensing circuit and as per the voltage divider rule, we get the output of 
                //that circuit. With the help of revers voltage divider formula, it is possible to get
                //the reference voltage.
                //For eg. consider the ouput of divider section is Vout and R1 = 5.6K, R2 = 330K(Actual used resistors in circuit).
                //Please refer the schematic "1521CD0B-03" for the circuit.
                //If values of resistors in divider section get changed then update the formula with similar way.
                //R = R2/(R1 + R2) = 330 / (330 + 5.6) = 0.983313
                //then equation for Vout is
                //   Vout = Vref * R...........Equation 1.
                //This Vout is further processed and recoverd back with the help of calibration slope and offset.
                //so, by reverse calculation,
                //   Vref = Vout / R............From equation 1.
                //   Vref = Vout / 0.983313
                //   Vref = 1.01697 * Vout.
                //Thus, the reference voltage is calculated as per the above formula.
                flmInterVal = (float)(lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.VREF_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.VREF_Index]) /
                                (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.VREF_Index] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.VREF_Index]);
                switch (DUT)
                {
                    case 1:
                        clsGlobalVariables.fltgREF_VtgDUT1 = clsGlobalVariables.fltgv1to9VConfigSlopeDUT1 * (flmInterVal + clsGlobalVariables.fltgv1to9VConfigOffsetDUT1);
                        //Here "1.01697" is the constant value used to calculate VREF.
                        //This value is taken as it is from device firmware.
                        clsGlobalVariables.fltgREF_VtgDUT1 = (float)1.01697 * (float)(clsGlobalVariables.fltgREF_VtgDUT1 / (float)1000);
                        break;
                    case 2:
                        clsGlobalVariables.fltgREF_VtgDUT2 = clsGlobalVariables.fltgv1to9VConfigSlopeDUT2 * (flmInterVal + clsGlobalVariables.fltgv1to9VConfigOffsetDUT2);
                        //Here "1.01697" is the constant value used to calculate VREF.
                        //This value is taken as it is from device firmware.
                        clsGlobalVariables.fltgREF_VtgDUT2 = (float)1.01697 * (float)(clsGlobalVariables.fltgREF_VtgDUT2 / (float)1000); break;
                    case 3:
                        clsGlobalVariables.fltgREF_VtgDUT3 = clsGlobalVariables.fltgv1to9VConfigSlopeDUT3 * (flmInterVal + clsGlobalVariables.fltgv1to9VConfigOffsetDUT3);
                        //Here "1.01697" is the constant value used to calculate VREF.
                        //This value is taken as it is from device firmware.
                        clsGlobalVariables.fltgREF_VtgDUT3 = (float)1.01697 * (float)(clsGlobalVariables.fltgREF_VtgDUT3 / (float)1000); break;
                    case 4:
                        clsGlobalVariables.fltgREF_VtgDUT4 = clsGlobalVariables.fltgv1to9VConfigSlopeDUT4 * (flmInterVal + clsGlobalVariables.fltgv1to9VConfigOffsetDUT4);
                        //Here "1.01697" is the constant value used to calculate VREF.
                        //This value is taken as it is from device firmware.
                        clsGlobalVariables.fltgREF_VtgDUT4 = (float)1.01697 * (float)(clsGlobalVariables.fltgREF_VtgDUT4 / (float)1000); break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ConvertCalibConst</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function invokes a function which converts the float data in hexadecimal string value.
        ///calulated hex string is then stored in the global array.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void ConvertCalibConst(byte DUT)
        {
            switch (DUT)
            {
                case 1:
                    clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.CJC] = Float2Hex(clsGlobalVariables.fltgvPrConfigCjcDUT1);
                    clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.RTD] = Float2Hex(clsGlobalVariables.fltgvPrConfigRtdCurrentDUT1);
                    clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.SLOPE] = Float2Hex(clsGlobalVariables.fltgvPrConfigSlopeDUT1);
                    clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.OFFSET] = Float2Hex(clsGlobalVariables.fltgvPrConfigOffsetDUT1);
                    clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.SLOPE_420mA] = Float2Hex(clsGlobalVariables.fltgv4to20mAConfigSlopeDUT1);
                    clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.OFFSET_420mA] = Float2Hex(clsGlobalVariables.fltgv4to20mAConfigOffsetDUT1);
                    clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.SLOPE_19V] = Float2Hex(clsGlobalVariables.fltgv1to9VConfigSlopeDUT1);
                    clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.OFFSET_19V] = Float2Hex(clsGlobalVariables.fltgv1to9VConfigOffsetDUT1);
                    break;
                case 2:
                    clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.CJC] = Float2Hex(clsGlobalVariables.fltgvPrConfigCjcDUT2);
                    clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.RTD] = Float2Hex(clsGlobalVariables.fltgvPrConfigRtdCurrentDUT2);
                    clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.SLOPE] = Float2Hex(clsGlobalVariables.fltgvPrConfigSlopeDUT2);
                    clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.OFFSET] = Float2Hex(clsGlobalVariables.fltgvPrConfigOffsetDUT2);
                    clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.SLOPE_420mA] = Float2Hex(clsGlobalVariables.fltgv4to20mAConfigSlopeDUT2);
                    clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.OFFSET_420mA] = Float2Hex(clsGlobalVariables.fltgv4to20mAConfigOffsetDUT2);
                    clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.SLOPE_19V] = Float2Hex(clsGlobalVariables.fltgv1to9VConfigSlopeDUT2);
                    clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.OFFSET_19V] = Float2Hex(clsGlobalVariables.fltgv1to9VConfigOffsetDUT2);
                    break;
                case 3:
                    clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.CJC] = Float2Hex(clsGlobalVariables.fltgvPrConfigCjcDUT3);
                    clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.RTD] = Float2Hex(clsGlobalVariables.fltgvPrConfigRtdCurrentDUT3);
                    clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.SLOPE] = Float2Hex(clsGlobalVariables.fltgvPrConfigSlopeDUT3);
                    clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.OFFSET] = Float2Hex(clsGlobalVariables.fltgvPrConfigOffsetDUT3);
                    clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.SLOPE_420mA] = Float2Hex(clsGlobalVariables.fltgv4to20mAConfigSlopeDUT3);
                    clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.OFFSET_420mA] = Float2Hex(clsGlobalVariables.fltgv4to20mAConfigOffsetDUT3);
                    clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.SLOPE_19V] = Float2Hex(clsGlobalVariables.fltgv1to9VConfigSlopeDUT3);
                    clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.OFFSET_19V] = Float2Hex(clsGlobalVariables.fltgv1to9VConfigOffsetDUT3);
                    break;
                case 4:
                    clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.CJC] = Float2Hex(clsGlobalVariables.fltgvPrConfigCjcDUT4);
                    clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.RTD] = Float2Hex(clsGlobalVariables.fltgvPrConfigRtdCurrentDUT4);
                    clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.SLOPE] = Float2Hex(clsGlobalVariables.fltgvPrConfigSlopeDUT4);
                    clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.OFFSET] = Float2Hex(clsGlobalVariables.fltgvPrConfigOffsetDUT4);
                    clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.SLOPE_420mA] = Float2Hex(clsGlobalVariables.fltgv4to20mAConfigSlopeDUT4);
                    clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.OFFSET_420mA] = Float2Hex(clsGlobalVariables.fltgv4to20mAConfigOffsetDUT4);
                    clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.SLOPE_19V] = Float2Hex(clsGlobalVariables.fltgv1to9VConfigSlopeDUT4);
                    clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.OFFSET_19V] = Float2Hex(clsGlobalVariables.fltgv1to9VConfigOffsetDUT4);
                    break;
            }
                       
        }

        ///<MemberName>ConvertCalibConst</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function converts the float data into hexadecimal string
        ///</summary>
        ///<param name="fltData">This float variable is converted into hex string.</param>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public string Float2Hex(float fltData)
        {
            try
            {
                string strmdata = "";
                //First float value is converted into bytes array
                byte[] bytes = BitConverter.GetBytes(fltData);

                //and then every byte value is converted in the hexadecimal value.
                for (int imLoopCntr = bytes.Length - 1; imLoopCntr >= 0; imLoopCntr--)
                {
                    if (imLoopCntr != 0)
                    {
                        strmdata = strmdata + bytes[imLoopCntr].ToString() + ",";
                    }
                    else
                    {
                        strmdata = strmdata + bytes[imLoopCntr].ToString();
                    }
                }
                return strmdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>DeviceWrite</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function sends Write calib constant query to device.
        ///</summary>        
        ///<ClassName>clsGlobalFunctions</ClassName>
        public byte DeviceWrite(byte slaveID,byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data; 
            try
            {
                //This check is for device having modbus.                
                if (clsModelSettings.blnRS485Flag == true)
                {
                    //-------Changed By Shubham
                    //Date:- 24-02-2018
                    //Version:- V16
                    //Statement:- Parameter is passed to the function. 
                    //Meaning of false is not to send the VREF value to the DUT.
                    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteCalibConst(false, slaveID,DUT); 
                    //-------Changes End.
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBSendPvSlaveToDut</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public byte MBSendPvSlaveToDut(byte slaveID)
        {
            byte btmRetVal;
            int imResultData = 0;

            try
            {
                imResultData = (clsGlobalVariables.igvProcessValue * 0x100) |(clsGlobalVariables.TC_CNT);

                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(slaveID, clsGlobalVariables.CALIBRATE_FUNC_CODE, imResultData);

                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>Calc_Current</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function calculates RTD current here by using some formulaes and saves it in a global string.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void Calc_Current(byte DUT)
        {
            float flmInterVal, flmCJCLdCmpAnlOp, flmLdCmp;

            try
            {
                long[,] lngvCount = new long[4, 8];
                switch (DUT)
                {
                    case 1:
                        lngvCount = clsGlobalVariables.lngvCountDUT1;
                        break;
                    case 2:
                        lngvCount = clsGlobalVariables.lngvCountDUT2;
                        break;
                    case 3:
                        lngvCount = clsGlobalVariables.lngvCountDUT3;
                        break;
                    case 4:
                        lngvCount = clsGlobalVariables.lngvCountDUT4;
                        break;
                }
                flmInterVal = (float)(lngvCount[clsGlobalVariables.SIGNAL_CNT, clsGlobalVariables.PT100_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.PT100_CNT]) /
                               (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.PT100_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.PT100_CNT]);

                

                flmInterVal = (float)(lngvCount[clsGlobalVariables.LD_CJC_CNT, clsGlobalVariables.PT100_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.PT100_CNT]) /
                               (float)(lngvCount[clsGlobalVariables.REF2_CNT, clsGlobalVariables.PT100_CNT] - lngvCount[clsGlobalVariables.REF1_CNT, clsGlobalVariables.PT100_CNT]);


                switch (DUT)
                {
                    case 1:
                        flmCJCLdCmpAnlOp = clsGlobalVariables.fltgvPrConfigSlopeDUT1 * (flmInterVal + clsGlobalVariables.fltgvPrConfigOffsetDUT1);

                        flmLdCmp = clsGlobalVariables.fltgvPrConfigSlopeDUT1 * (flmInterVal + clsGlobalVariables.fltgvPrConfigOffsetDUT1);

                        clsGlobalVariables.fltgvPrConfigRtdCurrentDUT1 = (float)(2 * flmCJCLdCmpAnlOp - flmLdCmp) / (float)350;
                        break;
                    case 2:
                        flmCJCLdCmpAnlOp = clsGlobalVariables.fltgvPrConfigSlopeDUT2 * (flmInterVal + clsGlobalVariables.fltgvPrConfigOffsetDUT2);

                        flmLdCmp = clsGlobalVariables.fltgvPrConfigSlopeDUT2 * (flmInterVal + clsGlobalVariables.fltgvPrConfigOffsetDUT2);

                        clsGlobalVariables.fltgvPrConfigRtdCurrentDUT2 = (float)(2 * flmCJCLdCmpAnlOp - flmLdCmp) / (float)350;
                        break;
                    case 3:
                        flmCJCLdCmpAnlOp = clsGlobalVariables.fltgvPrConfigSlopeDUT3 * (flmInterVal + clsGlobalVariables.fltgvPrConfigOffsetDUT3);

                        flmLdCmp = clsGlobalVariables.fltgvPrConfigSlopeDUT3 * (flmInterVal + clsGlobalVariables.fltgvPrConfigOffsetDUT3);

                        clsGlobalVariables.fltgvPrConfigRtdCurrentDUT3 = (float)(2 * flmCJCLdCmpAnlOp - flmLdCmp) / (float)350;
                        break;
                    case 4:
                        flmCJCLdCmpAnlOp = clsGlobalVariables.fltgvPrConfigSlopeDUT4 * (flmInterVal + clsGlobalVariables.fltgvPrConfigOffsetDUT4);

                        flmLdCmp = clsGlobalVariables.fltgvPrConfigSlopeDUT4 * (flmInterVal + clsGlobalVariables.fltgvPrConfigOffsetDUT4);

                        clsGlobalVariables.fltgvPrConfigRtdCurrentDUT4 = (float)(2 * flmCJCLdCmpAnlOp - flmLdCmp) / (float)350;
                        break;
                }


                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>AutoComPortDetection</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function sends query to Calibrator and JIG to check communication port by using available port.
        ///If response to that query is valid then that port number is saved in the global string for each calibrator as well as JIG.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public byte AutoComPortDetection()
        {
            try
            {
                if (clsGlobalVariables.blngIsComportDetected == false)
                {


                    int imTimeout = 50;
                    clsGlobalVariables.algAvailableComPorts.Clear();
                    //This function gets all the available ports of the system and saves in the array list.
                    GetAvailablePortName(clsGlobalVariables.strgComPortPLC);

                    if (clsGlobalVariables.algAvailableComPorts.Count != 0)
                    {

                        clsGlobalVariables.strgComPortCalibratorDUT1 = "";
                        clsGlobalVariables.strgComPortCalibratorDUT2 = "";
                        clsGlobalVariables.strgComPortCalibratorDUT3 = "";
                        clsGlobalVariables.strgComPortCalibratorDUT4 = "";
                        clsGlobalVariables.strgComPortCalibratorDUT5 = "";
                        clsGlobalVariables.strgComPortCalibratorDUT6 = "";
                        foreach (var item in clsGlobalVariables.algAvailableComPorts)
                        {
                            if (MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(item.ToString(), true))
                            {
                                //This query is sent to the calibrator to check that Calibrator is connected to this port.
                                string CalibratorSerialNumber = "";
                                byte btmRetVal = clsGlobalVariables.objCalibQueriescls.AutoComPortQueryForCalibratorWithSerialNumber(ref CalibratorSerialNumber);
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //clsGlobalVariables.strgComPortCalibratorDUT1 = item.ToString();
                                    SetCalibratorCOMPort(item.ToString(), CalibratorSerialNumber);
                                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.CloseCommPort();

                                   // break;
                                }
                                MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.CloseCommPort();
                            }
                        }

                        //Clibrator remove port.

                        if (!CheckAllCalibratorConnected())
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;


                        RemoveClibratorPort();   
                        
                        //Intercharacter delay is set here. For programming this delay is set to 10msec.
                        MainWindowVM.initilizeCommonObject.objJIGSerialComm.uiDataEndTimeout = imTimeout;
                        clsGlobalVariables.strgComPortJIG = "";
                        foreach (var item in clsGlobalVariables.algAvailableComPorts)
                        {
                            if (MainWindowVM.initilizeCommonObject.objJIGSerialComm.OpenCommPort(item.ToString(), false, true))
                            {
                                //JIG query timeout is reduced because if timeout is kept as original then it will take long time to detect the port itself.
                                clsGlobalVariables.ig_Query_TimeOut = 1000;
                                //This query is sent to the JIG to check that JIG is connected to this port.
                                byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                if (clsModelSettings.blnRS485Flag)
                                {
                                     btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID_WM_BASE + 1, clsGlobalVariables.ALM1_TYPE, clsGlobalVariables.SET_ALM_TYPE_VAL);
                                }
                                else
                                {
                                      btmRetVal = clsGlobalVariables.objQueriescls.ReadDeviceIDSalveToDutPortDetection(clsGlobalVariables.MB_SLAVE_ID_WO_BASE+1);
                                }
                               
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.strgComPortJIG = item.ToString();
                                    for (byte DUT = 1; DUT < clsGlobalVariables.NUMBER_OF_DUTS; DUT++)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.ReadDeviceIDSalveToDutPortDetection(DUT + clsGlobalVariables.MB_SLAVE_ID_WO_BASE);
                                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            MessageBox.Show("com port fail with DUT number :" + (DUT).ToString());
                                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                        }
                                    }
                                    //now check all DUT is connected or Not...
                                    MainWindowVM.initilizeCommonObject.objJIGSerialComm.CloseCommPort();
                                    break;
                                }
                                MainWindowVM.initilizeCommonObject.objJIGSerialComm.CloseCommPort();
                            }
                        }
                    }
                }
                
                if (clsGlobalVariables.strgComPortJIG == "")
                {
                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data; 
                }
                else
                {
                    clsGlobalVariables.blngIsComportDetectedForPLC = true;
                    clsGlobalVariables.blngIsComportDetected = true;

                    return (byte)clsGlobalVariables.enmResponseError.Success; 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RemoveClibratorPort()
        {
            if (clsGlobalVariables.algAvailableComPorts.Contains(clsGlobalVariables.strgComPortCalibratorDUT1))
                clsGlobalVariables.algAvailableComPorts.Remove(clsGlobalVariables.strgComPortCalibratorDUT1);
            if (clsGlobalVariables.algAvailableComPorts.Contains(clsGlobalVariables.strgComPortCalibratorDUT2))
                clsGlobalVariables.algAvailableComPorts.Remove(clsGlobalVariables.strgComPortCalibratorDUT2);
            if (clsGlobalVariables.algAvailableComPorts.Contains(clsGlobalVariables.strgComPortCalibratorDUT3))
                clsGlobalVariables.algAvailableComPorts.Remove(clsGlobalVariables.strgComPortCalibratorDUT3);
            if (clsGlobalVariables.algAvailableComPorts.Contains(clsGlobalVariables.strgComPortCalibratorDUT4))
                clsGlobalVariables.algAvailableComPorts.Remove(clsGlobalVariables.strgComPortCalibratorDUT4);
            if (clsGlobalVariables.algAvailableComPorts.Contains(clsGlobalVariables.strgComPortCalibratorDUT5))
                clsGlobalVariables.algAvailableComPorts.Remove(clsGlobalVariables.strgComPortCalibratorDUT5);
            if (clsGlobalVariables.algAvailableComPorts.Contains(clsGlobalVariables.strgComPortCalibratorDUT6))
                clsGlobalVariables.algAvailableComPorts.Remove(clsGlobalVariables.strgComPortCalibratorDUT6);
        }

        private bool CheckAllCalibratorConnected()
        {
            switch(clsGlobalVariables.NUMBER_OF_DUTS)
            {
                case 6:
                    if (clsGlobalVariables.strgComPortCalibratorDUT1 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT2 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT3 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT4 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT5 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT6 == "")
                        return false;
                    break;
                case 5:
                    if (clsGlobalVariables.strgComPortCalibratorDUT1 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT2 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT3 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT4 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT5 == "" )
                        
                        return false;
                    break;
                case 4:
                    if (clsGlobalVariables.strgComPortCalibratorDUT1 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT2 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT3 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT4 == "" )

                        return false;
                    break;
                case 3:
                    if (clsGlobalVariables.strgComPortCalibratorDUT1 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT2 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT3 == "" )
                        return false;
                    break;
                case 2:
                    if (clsGlobalVariables.strgComPortCalibratorDUT1 == "" ||
                        clsGlobalVariables.strgComPortCalibratorDUT2 == "")
                        return false;
                    break;
                case 1:
                    if (clsGlobalVariables.strgComPortCalibratorDUT1 == "")
                        return false;
                    break;
                
            }
            return true;
        }

        private void SetCalibratorCOMPort(string ComPort, string calibratorSerialNumber)
        {
            if (clsGlobalVariables.CLIBRATOR_SR1== calibratorSerialNumber)
            {
                clsGlobalVariables.strgComPortCalibratorDUT1 = ComPort;
            }
            else if (clsGlobalVariables.CLIBRATOR_SR2 == calibratorSerialNumber)
            {
                clsGlobalVariables.strgComPortCalibratorDUT2 = ComPort;
            }
            else if (clsGlobalVariables.CLIBRATOR_SR3 == calibratorSerialNumber)
            {
                clsGlobalVariables.strgComPortCalibratorDUT3 = ComPort;
            }
            else if (clsGlobalVariables.CLIBRATOR_SR4 == calibratorSerialNumber)
            {
                clsGlobalVariables.strgComPortCalibratorDUT4 = ComPort;
            }
            else if (clsGlobalVariables.CLIBRATOR_SR5 == calibratorSerialNumber)
            {
                clsGlobalVariables.strgComPortCalibratorDUT5 = ComPort;
            }
            else if (clsGlobalVariables.CLIBRATOR_SR6 == calibratorSerialNumber)
            {
                clsGlobalVariables.strgComPortCalibratorDUT6 = ComPort;
            }

        }

        ///<MemberName>TestKeyPad</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function sends the query to DUT for keypad test. Keypad test sequence is "ESC","UP","DOWN", "ENTER".
        ///After completion of all keys test this function returns success.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public byte TestKeyPad(byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            long lmData;
            int imCnt;
            byte btmAttemptCounter = 1;
            byte btmKeyCnt = 0;
            byte btmMaxAttempt = 20;

            try
            {
                //CA55Program.objMainForm.EnableGroupBox(clsGlobalVariables.KEYPAD_CONFIG);
                for (imCnt = 0; imCnt < clsGlobalVariables.NUM_OF_KEYS; imCnt++)
                {
                    //CA55 Program.objMainForm.shpKey.TextONShape = clsGlobalVariables.arrstrgKeysNames[btmKeyCnt];
                    //clsGlobalVariables.arrstrgKeysNames[btmKeyCnt];
                    clsGlobalVariables.objGlobalFunction.ApplyDelay(200);
                    clsGlobalVariables.mainWindowVM.DisplayKeypadTest(DUT, clsGlobalVariables.arrstrgKeysNames[btmKeyCnt], true);
                    //This attempt counter is for each key.
                    while (btmAttemptCounter <= btmMaxAttempt)
                    {
                        //This check is for device having modbus.                        
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBStartTest(clsGlobalVariables.CHK_KEYPAD, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_KEYPAD);
                        }

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //This check is for device having modbus.                            
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 1);
                            }
                            else//Device without modbus
                            {
                                lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);
                            }
                            //Data of the received key is checked here.
                            if (lmData == (long)clsGlobalVariables.arrigKeysValue[btmKeyCnt])
                            {
                                btmAttemptCounter = 1;
                                ++btmKeyCnt;
                                break;
                            }
                            else
                            {
                                ++btmAttemptCounter;
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;  
                            }
                        }
                        else
                        {
                            break;
                        }
                    }                                
                }
                //CA55 Program.objMainForm.EnableGroupBox(clsGlobalVariables.DEVICE_CONFIG);
                //CA55 Program.objMainForm.shpKey.TextONShape = "";
                //CA55 Program.objMainForm.txtProgressInfo.Focus();    
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>LoadKeypadData</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function Loads the global array containing keys names and keys value of DUT.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void LoadKeypadData()
        {
            try
            {
                clsGlobalVariables.arrstrgKeysNames[0] = clsGlobalVariables.strgESC;
                clsGlobalVariables.arrstrgKeysNames[1] = clsGlobalVariables.strgDOWN;
                clsGlobalVariables.arrstrgKeysNames[2] = clsGlobalVariables.strgUP;
                clsGlobalVariables.arrstrgKeysNames[3] = clsGlobalVariables.strgEnter;

                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48  || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                {
                    clsGlobalVariables.arrigKeysValue[0] = clsGlobalVariables.igESCVal;
                    clsGlobalVariables.arrigKeysValue[1] = clsGlobalVariables.igDOWNVal;
                    clsGlobalVariables.arrigKeysValue[2] = clsGlobalVariables.igUPKeyVal;
                    clsGlobalVariables.arrigKeysValue[3] = clsGlobalVariables.igEnterKeyVal;                   
                }
                else
                {
                    clsGlobalVariables.arrigKeysValue[0] = clsGlobalVariables.igESCVal_PI;
                    clsGlobalVariables.arrigKeysValue[1] = clsGlobalVariables.igDOWNVal_PI;
                    clsGlobalVariables.arrigKeysValue[2] = clsGlobalVariables.igUPKeyVal_PI;
                    clsGlobalVariables.arrigKeysValue[3] = clsGlobalVariables.igEnterKeyVal_PI;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>GenerateLog</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>4/01/2018</Date>
        ///<summary>
        ///This function determins which type of device it is.
        ///1) Analog device without analog ip sensors
        ///2) Analog device with analog ip sensors.
        ///3) Device without analog output and input sensors.
        ///4) Device without analog output but with Analog input sensors.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void GenerateLog(byte DUT)
        {  
            //Analog device without analog ip sensors
            if ((clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igSingleActingType
                || clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingType
                || clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWOModbusType)
                && clsModelSettings.blnAnalogDUT == true)
            {
                clsGlobalVariables.objDataLog[DUT].WriteLogFile(clsGlobalVariables.Case_WithAnalogOP_WithoutAnalogIP, DUT);
            }
            //Device without analog output and input sensors.
            else if ((clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igSingleActingType
                || clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingType
                || clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWOModbusType)
                && clsModelSettings.blnAnalogDUT == false)
            {
                clsGlobalVariables.objDataLog[DUT].WriteLogFile(clsGlobalVariables.Case_WithoutAnalogOP_WithoutAnalogIP, DUT);
            }
            //Analog device with analog ip sensors.
            else if ((clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igSingleActingWithAnalogIPType
                || clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWithAnalogIPType
                || clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWithAnalogIPWOModbusType)
                && clsModelSettings.blnAnalogDUT == true)
            {
                clsGlobalVariables.objDataLog[DUT].WriteLogFile(clsGlobalVariables.Case_WithAnalogOP_WithAnalogIP, DUT);
            }
            //Device without analog output but with Analog input sensors.
            else if ((clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igSingleActingWithAnalogIPType
                || clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWithAnalogIPType
                || clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWithAnalogIPWOModbusType)
                && clsModelSettings.blnAnalogDUT == false)
            {
                clsGlobalVariables.objDataLog[DUT].WriteLogFile(clsGlobalVariables.Case_WithoutAnalogOP_WithAnalogIP, DUT);
            }
        }

        ///<MemberName>DeleteDirectory</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>17/01/2018</Date>
        ///<summary>
        ///This function is used to delete the dirctory which is passed to it as a parameter.
        ///</summary>
        ///<ClassName>clsGlobalFunctions</ClassName>
        public void DeleteDirectory(string strmpath, bool blnrecursive)
        {
            try
            {
                if (blnrecursive)
                {
                    var subfolders = Directory.GetDirectories(strmpath);
                    foreach (var s in subfolders)
                    {
                        DeleteDirectory(s, blnrecursive);
                    }
                }
                var files = Directory.GetFiles(strmpath);
                //read only attribute of every file present in the directory is checked.
                foreach (var objfiles in files)
                {
                    //If file which is to be saved is read only then 
                    //Read Only attribute to that file is removed here before saving that file.
                    var attr = File.GetAttributes(objfiles);
                    if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(objfiles, attr ^ FileAttributes.ReadOnly);
                    }
                    File.Delete(objfiles);
                }
                // At this point, all the files and sub-folders have been deleted.
                // So we delete the empty folder using the OOTB Directory.Delete method.
                Directory.Delete(strmpath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DisplayImgMessageBox(string MsgDescription, string DisplayImgPath, string TitleImgMsg)
        {
           
               DisplayMessageWindow displayMessageWindow = new DisplayMessageWindow(TitleImgMsg, clsGlobalVariables.DispImgpath + DisplayImgPath, MsgDescription);
            displayMessageWindow.ShowDialog();
        }


    }//End of class
}//End of namespace
