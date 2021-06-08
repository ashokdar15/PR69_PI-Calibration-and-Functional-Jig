using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using PR69_PI_Calibration_and_Functional_Jig.ViewModel;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    /********************************************************************************************
              Class Name        : clsQueries Class
              Purpose           : This class contains All queries used in the software(Jig Queries, Calibrator Queries and Programming Jig queries).
              Date              : 1/06/2017
              Written By        : Shubham
              CopyRight         : General Industrial Controls Pvt. Ltd. Pune
              Modified          : Date                
              Released Version  :  NA
              Changed By        :  NA
              Decription Of Change:  NA
    ********************************************************************************************/
    public class clsQueries
    {

        #region "-----------------------------Functional JIG Queries ------------------------"

        ///<MemberName>MBSwitchSensor</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function forms query to change the sensor of the DUT. 
        ///This is for Double Acting device.
        ///</summary>
        ///<param name="btmData">This is type of sensor which is going to set in the DUT.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBSwitchSensor(byte btmData,byte slaveID)
        {
            byte btmRetVal;
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = slaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_SWITCH_SENSOR;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = btmData;
                
                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);                
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBSwitchSensorSlaveToDut</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function forms query to change the sensor of the DUT. 
        ///This is for Single Acting device.
        ///</summary>
        ///<param name="btmData">This is type of sensor which is going to set in the DUT.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBSwitchSensorSlaveToDut(int btmData,byte slaveID)
        {
            byte btmReturnVal;
            int imResultData;

            try
            {
                imResultData = ((btmData * 0x100) | clsGlobalVariables.SWITCH_SENSOR);
                btmReturnVal = MBQueryForWOModbusDevices(slaveID, clsGlobalVariables.SET_WRITE_FUNC_CODE, imResultData);

                return btmReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        ///<MemberName>MBAdjustMode</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function forms query to set the mode in the device (Start mode/ Run mode)
        ///This is for Double Acting device.
        ///</summary>
        ///<param name="btmData">This is mode which is going to set in the DUT.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBAdjustMode(byte btmModeData, byte slaveID)
        {
            byte btmRetVal;

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = slaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_ADJUST_MODE;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = btmModeData;

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);

                return btmRetVal; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBErase</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function forms query to erase the data from the DUT.
        ///This is only applicable for Double Acting device. For single acting devices Erase query is not present.
        ///</summary>        
        ///<ClassName>clsQueries</ClassName>
        public byte MBErase(byte salveID)
        {
            byte btmRetVal;
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 4);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = salveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_ERASE;

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);

                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBReadAdcCountSlaveToDut</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function reads the ADC count from the device and validates that counts accordingly.
        ///This is for Single Acting device. 
        ///</summary>        
        ///<param name="btmData">This parameter conatains state/event of the relay whether it is ON or OFF.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBReadAdcCountSlaveToDut(byte btmEventtype,byte slaveID)
        {
            byte btmReturnVal;
            long lmData;
            int imHiLmt;
            int imLowLmt;

            try
            {
                //On-Off related counts are saved in local variables.
                if (btmEventtype == clsGlobalVariables.RLY_ON)
                {
                    imHiLmt = clsGlobalVariables.RLY_ON_ADC_HIGH_LMT_COUNT;
                    imLowLmt = clsGlobalVariables.RLY_ON_ADC_LOW_LMT_COUNT;
                }
                else
                {
                    imHiLmt = clsGlobalVariables.RLY_OFF_ADC_HIGH_LMT_COUNT;
                    imLowLmt = clsGlobalVariables.RLY_OFF_ADC_LOW_LMT_COUNT;
                }

                btmReturnVal = MBQueryForWOModbusDevices(slaveID, clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_ADC_CNT);

                if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);

                    if ((lmData < (long)imLowLmt) || (lmData > (long)imHiLmt))
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                return btmReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        ///<MemberName>MBReadDutCalibConst</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function reads the Calib constant from the device. USe of this is to tell whether device is calibrated or not.
        ///This is for Double Acting device. 
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public byte MBReadDutCalibConst(byte btmSlaveID)
        {
            byte btmRetVal;
            long lmData;

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 4);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] =btmSlaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_READ_CALIB_CONST_STATUS;

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);

                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 2, 1);
                    clsModelSettings.btmCalibConst = (byte)lmData;

                    if (lmData == clsGlobalVariables.CALIB_DONE)
                    {
                        clsMessages.DisplayMessage(clsMessageIDs.DUT_ALREADY_CALIBRATED);   
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;   
                    }  
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBStartTest</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function is common for multiple tests such as Display, Relay, Keypad, Leaky MOSFET. 
        ///This is for Double Acting device. 
        ///</summary>                
        ///<param name="btmData">This parameter conatains which test should be started. Display, Relay, Leaky MOSFET, etc.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBStartTest(byte btmData,byte slaveID)
        {
            byte btmRetVal;

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = slaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_START_TEST;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = btmData; 

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        ///<MemberName>MBStartCalibration</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function is common for Current, voltage and Get counts tests. 
        ///This is for Double Acting device. 
        ///</summary>                
        ///<param name="btmData">This parameter conatains which test should be started. Current, voltage or Get counts tests. </param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBStartCalibration(byte btmData,byte slaveID)
        {
            byte btmRetVal;

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = slaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_CALIBRATE;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = btmData;

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBSetAnalogOutput</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function sets value of current or voltage in the DUT.
        ///This is for Double Acting device. 
        ///</summary>                
        ///<param name="btmFunCode">This parameter contains Current or voltage value.</param>
        ///<param name="btmData">This parameter conatains actual current or voltage which is to be set.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBSetAnalogOutput(byte btmFunCode, byte btmData,byte SlaveID)
        {
            byte btmRetVal;
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = SlaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = btmFunCode;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = btmData;

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                return btmRetVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBWriteMeasuredAnlopVal</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function sets the observed value of current or voltage in the DUT.
        ///This is for Double Acting device. 
        ///</summary>                
        ///<param name="btmFunCode">This parameter contains Current or voltage value.</param>
        ///<param name="btmData">This parameter conatains actual current or voltage for which observed value is to be set.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBWriteMeasuredAnlopVal(byte btmFunCode, byte btmData,byte slaveID)
        {
            byte btmRetVal;
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 7);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = slaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = btmFunCode;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = btmData;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 1] = (byte)((int)(clsModelSettings.imAnalOpVal[slaveID-1]) /256);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 2] = (byte)(clsModelSettings.imAnalOpVal[slaveID-1] & 0xFF);

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                return btmRetVal;
            }
            catch (Exception ex) 
            {
                throw ex;
            }

        }

        ///<MemberName>ReadPVSingleActing</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function reads PV value from the DUT.
        ///This is for Single Acting device. 
        ///</summary>                        
        ///<ClassName>clsQueries</ClassName>
        public byte ReadPVSingleActing(byte slaveID)
        {
            byte btmReturnVal;
            long lmData;
            byte[] arrbtmDataBuff =new byte [3];

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 8);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = slaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 1] = clsGlobalVariables.MB_MASTER_TO_DUT;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 2] = clsGlobalVariables.READ_PV_Value_Func_CODE;
                //CAT_NO value will be neglected at Device side since it is read query.
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 3] = clsGlobalVariables.CAT_NO / 256;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 4] = clsGlobalVariables.CAT_NO & 0xFF;
                
                arrbtmDataBuff[0] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 2];
                arrbtmDataBuff[1] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 3];
                arrbtmDataBuff[2] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 4];

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 5]= clsGlobalVariables.objGlobalFunction.CalculateChecksum((byte)arrbtmDataBuff.Length ,ref arrbtmDataBuff );

                btmReturnVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);
                    //Value read form the device is set into the global variable.
                    clsGlobalVariables.shrtgPV = (short)lmData; 
                }
                return btmReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte ReadPVSingleActingCJC(byte slaveID, byte DUT)
        {
            byte btmReturnVal;
            long lmData;
            byte[] arrbtmDataBuff = new byte[3];

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 8);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = slaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 1] = clsGlobalVariables.MB_MASTER_TO_DUT;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 2] = clsGlobalVariables.READ_PV_Value_Func_CODE;
                //CAT_NO value will be neglected at Device side since it is read query.
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 3] = clsGlobalVariables.CJC_NO / 256;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 4] = clsGlobalVariables.CJC_NO & 0xFF;

                arrbtmDataBuff[0] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 2];
                arrbtmDataBuff[1] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 3];
                arrbtmDataBuff[2] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 4];

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 5] = clsGlobalVariables.objGlobalFunction.CalculateChecksum((byte)arrbtmDataBuff.Length, ref arrbtmDataBuff);

                btmReturnVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);
                    //Value read form the device is set into the global variable.
                    switch (DUT)
                    {
                     case 1:
                            clsGlobalVariables.shrtgCJCDUT1 = (short)lmData;
                            if (clsGlobalVariables.shrtgCJCDUT1 < clsGlobalVariables.CJC_min_Value || clsGlobalVariables.shrtgCJCDUT1 > clsGlobalVariables.CJC_max_Value)
                            {
                                //MessageBox.Show("CJC faild value : " + clsGlobalVariables.shrtgCJCDUT1.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                btmReturnVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                                break;
                        case 2:
                            clsGlobalVariables.shrtgCJCDUT2 = (short)lmData;
                            if (clsGlobalVariables.shrtgCJCDUT2 < clsGlobalVariables.CJC_min_Value || clsGlobalVariables.shrtgCJCDUT2 > clsGlobalVariables.CJC_max_Value)
                            {
                                //MessageBox.Show("CJC faild value : " + clsGlobalVariables.shrtgCJCDUT2.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                btmReturnVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            break;
                        case 3:
                            clsGlobalVariables.shrtgCJCDUT3 = (short)lmData;
                            if (clsGlobalVariables.shrtgCJCDUT3 < clsGlobalVariables.CJC_min_Value || clsGlobalVariables.shrtgCJCDUT3 > clsGlobalVariables.CJC_max_Value)
                            {
                                //MessageBox.Show("CJC faild value : " + clsGlobalVariables.shrtgCJCDUT3.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                btmReturnVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            break;
                        case 4:
                            clsGlobalVariables.shrtgCJCDUT4 = (short)lmData;
                            if (clsGlobalVariables.shrtgCJCDUT4 < clsGlobalVariables.CJC_min_Value || clsGlobalVariables.shrtgCJCDUT4 > clsGlobalVariables.CJC_max_Value)
                            {
                                //MessageBox.Show("CJC faild value : " + clsGlobalVariables.shrtgCJCDUT4.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                btmReturnVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            break;
                    }
                }
                return btmReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///<MemberName>ReadPVDoubleActing</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function reads PV value from the DUT.
        ///This is for Double Acting device. 
        ///</summary>                        
        ///<ClassName>clsQueries</ClassName>
        public byte ReadPVDoubleActing(byte SlaveID)
        {
            byte btmReturnVal;
            long lmData;

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 8);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = SlaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_READ_HOLDIND_REG;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = clsGlobalVariables.READ_PV / 256;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 1] = clsGlobalVariables.READ_PV & 0xFF;
                //CAT_NO value will be neglected at Device side since it is read query.
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 2] = 1 / 256;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 3] = 1 & 0xFF;

                btmReturnVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);
                    //Value read form the device is set into the global variable.
                    clsGlobalVariables.shrtgPV = (short)lmData;                    
                }
                return btmReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ReadSensorTypeSingleActing</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function Reads the selected sensor from the DUT and compares it with sensor passed as a parameter.
        ///This is for Single Acting device. 
        ///</summary>                    
        ///<param name="lmSensor">This parameter is used to compare read sensor fom the device.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte ReadSensorTypeSingleActing(long lmSensor,byte slaveID)
        {
            byte btmReturnVal;
            long lmData;
            try
            {
                btmReturnVal = MBQueryForWOModbusDevices(slaveID, clsGlobalVariables.READ_Sensor_Func_CODE, clsGlobalVariables.CAT_NO);
                if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);

                    if (lmData != lmSensor)
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                return btmReturnVal; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBReadHoldingReg</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function Reads the data from the device with help for adderess passed as a parameter to it.
        ///This is for Double Acting device. 
        ///</summary>                    
        ///<param name="btmSlaveID">Slave ID.</param>
        ///<param name="imHldngRegAddr">Query address</param>
        ///<param name="imNumOfBytes">Number of bytes to be read from the device.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBReadHoldingReg(byte btmSlaveID, int imHldngRegAddr, int imNumOfBytes)
        {
            byte btmRetVal;

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 8);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = btmSlaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_READ_HOLDIND_REG;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = (byte)(imHldngRegAddr/ 256);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 1] = (byte)(imHldngRegAddr & 0xFF);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 2] = (byte)(imNumOfBytes / 256);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 3] = (byte)(imNumOfBytes & 0xFF);

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
              
                return btmRetVal; 

            }
            catch (Exception ex)
            {
                throw ex;
            }
    
        }

        ///<MemberName>MBReadHoldingReg</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function writes the data in the device with help for adderess passed as a parameter to it.
        ///This is for Double Acting device. 
        ///</summary>                    
        ///<param name="btmSlaveID">Slave ID.</param>
        ///<param name="imHldngRegAddr">Write Query address</param>
        ///<param name="imDataVal">Data to be written in the device.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBWriteHoldingReg(byte btmSlaveID, int imHldngRegAddr, int imDataVal)
        {
            byte btmRetVal;

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 8);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = btmSlaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_WRITE_HOLDIND_REG;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = (byte)(imHldngRegAddr / 256);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 1] = (byte)(imHldngRegAddr & 0xFF);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 2] = (byte)(imDataVal / 256);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 3] = (byte)(imDataVal & 0xFF);

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;          
            }
        }


        public byte MBWriteChangeCommMode(byte btmSlaveID, int imTTLModeOrOnewire)
        {
            byte btmRetVal;

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 6);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = btmSlaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_WRITE_CHANGE_COMM_MODE;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS] = (byte)(imTTLModeOrOnewire / 256);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_DATA_POS + 1] = (byte)(imTTLModeOrOnewire & 0xFF);
              
                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ReadSensorTypeDoubleActing</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function Reads the selected sensor from the DUT and compares it with sensor passed as a parameter.
        ///This is for Double Acting device. 
        ///</summary>                    
        ///<param name="lmSensor">This parameter is used to compare read sensor fom the device.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte ReadSensorTypeDoubleActing(long lmSensor,byte slaveID)
        {
            byte btmReturnVal;
            long lmData;

            try
            {
                btmReturnVal = MBReadHoldingReg(slaveID, clsGlobalVariables.READ_Sensor, 1);

                if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);

                    if (lmData != lmSensor)
                    {                        
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                return btmReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ReadDeviceID</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function Reads the Device ID from the DUT.
        ///This is for Double Acting device. 
        ///</summary>                            
        ///<ClassName>clsQueries</ClassName>
        public byte ReadDeviceID(byte SlaveID)
        {
            byte btmReturnVal;
            long lmData;

            try
            {
                btmReturnVal = MBReadHoldingReg(SlaveID, clsGlobalVariables.MVER_ADDRESS, 1);

                if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);

                    if (lmData != clsModelSettings.igDutID)
                    {
                        //clsGlobalVariables.mainWindowVM.DisplayMessage(1, clsMessageIDs.WRONG_DEVICE_SELECTION);

                        //clsMessages.DisplayMessage(clsMessageIDs.WRONG_DEVICE_SELECTION); 
                        //MessageBox.Show("Wrong Device selection.", clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                return btmReturnVal; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ReadDeviceIDSalveToDut</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function Reads the Device ID from the DUT.
        ///This is for Double Acting device. 
        ///</summary>                            
        ///<ClassName>clsQueries</ClassName>
        public byte ReadDeviceIDSalveToDut(byte slaveID)
        {
            byte btmReturnVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            long lmData;

            try
            {
                btmReturnVal = MBQueryForWOModbusDevices(slaveID, clsGlobalVariables.READ_FUNC_CODE, clsGlobalVariables.CAT_NO);
                if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);

                    if (lmData != clsModelSettings.igDutID)
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                return btmReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public byte ReadDeviceIDSalveToDutPortDetection(int slaveID)
        {

            try
            {
                return MBQueryForWOModbusDevices(slaveID, clsGlobalVariables.READ_FUNC_CODE, clsGlobalVariables.CAT_NO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public byte[] ReadCalibrationConstToDut()
        //{
        //    byte btmReturnVal;

        //    try
        //    {
        //        btmReturnVal = MBQueryForWOModbusDevices(clsGlobalVariables.MB_SLAVE3_ID,clsGlobalVariables.READ_FUNC_CODE, clsGlobalVariables.CALIB_CONST);

        //        if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
        //        {
        //             return clsGlobalVariables.btgRxBuffer;
        //        }
        //        return new byte[1];
        //    }
        //    catch (Exception ex)
        //    {
        //        return new byte[1];
        //    }
        //}
        ///<MemberName>ReadDeviceIDSalveToDut</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function Reads Calibration constant from the DUT. This query is used to determine that whether device
        ///is calibrated or uncalibrated.
        ///This is for Single Acting device. 
        ///</summary>                            
        ///<ClassName>clsQueries</ClassName>
        public byte ReadCalibConstSalveToDut(byte slaveID)
        {
            byte btmReturnVal;
            long lmData;

            try
            {
                btmReturnVal = MBQueryForWOModbusDevices(slaveID, clsGlobalVariables.READ_FUNC_CODE, clsGlobalVariables.CALIB_STATUS);

                if (btmReturnVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);
                    clsModelSettings.btmCalibConst = (byte)lmData;                    
                }
                return btmReturnVal;  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ChangeSensor</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function Reads and compares sensor either for double acting device or single acting device.
        ///</summary>    
        ///<param name="btmData">This parameter is used to compare read sensor fom the device.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte ChangeSensor(byte btmData,byte slaveID)
        {
            byte btmReturnVal;

            try
            {
                //This check is for device having modbus.
                if (clsModelSettings.blnRS485Flag == true)
                {
                    btmReturnVal = MBSwitchSensor(btmData, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE+ slaveID));
                }
                else//Device without modbus
                {
                    btmReturnVal = MBSwitchSensorSlaveToDut(btmData, (byte)(slaveID +clsGlobalVariables.MB_SLAVE_ID_WO_BASE));
                }                
                return btmReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBStartRelayTest</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function is used to start relay test conducted by either double acting device or single acting device.
        ///This test is done for OP2 and OP3 output(Relays).
        ///</summary>
        ///<ClassName>clsQueries</ClassName>
        //public byte MBStartRelayTest()
        //{
        //    byte btmRetVal;
        //    long lmData;

        //    try
        //    {
        //        //This check is for device having modbus.                
        //        if (clsModelSettings.blnRS485Flag == true)
        //        {
        //            btmRetVal = MBStartTest(clsGlobalVariables.TEST_REL); 
        //        }
        //        else//Device without modbus
        //        {
        //            btmRetVal = MBQueryForWOModbusDevices(clsGlobalVariables.MB_SLAVE3_ID,clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_RELAY);   
        //        }

        //        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
        //        {
        //            //This check is for device having modbus.                
        //            if (clsModelSettings.blnRS485Flag == true)
        //            {
        //                lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 1);  
        //            }
        //            else//Device without modbus
        //            {
        //                lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);  
        //            }

        //            if (lmData == 1)
        //            {
        //                return (byte)clsGlobalVariables.enmResponseError.Success;
        //            }
        //            else
        //            {
        //                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
        //            }
        //        }   
        //        return btmRetVal;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///<MemberName>SwitchSensorRly</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function sets the sensor to 60 mv.
        ///</summary>
        ///<ClassName>clsQueries</ClassName>
        public byte SwitchSensorRly(byte slaveID)
        {
            byte btmRetVal;
            try
            {
                btmRetVal = ChangeSensor(clsGlobalVariables.SENSOR_60_MV_TYPE, slaveID);

                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBReadAdcCounts</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function reads counts from the device and validates that counts.
        ///</summary>
        ///<param name="btmData">This parameter is Slave id of the device.</param>
        ///<param name="btmEventype">This parameter determines that whether Realy state is on or off.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBReadAdcCounts(byte btmSlaveID,byte btmEventype)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            long lmData;
            int imHiLmt,imLowLmt,imRetryCountPass,imRetryCountFail,imRetryCountPassFail =0 ;
            string strmNewline = System.Environment.NewLine;

            try 
	        {
                //if (clsGlobalVariables.blngIsDebugPresent == true)
                //{
                //    imRetryCountFail = Convert.ToInt32(Program.objMainForm.txtRetriesFail.Text);
                //    imRetryCountPass = Convert.ToInt32(Program.objMainForm.txtRetriesPass.Text);
                //}
                //else
                //{
                    imRetryCountFail = clsGlobalVariables.igRetries_Fail_RelayTest;
                    imRetryCountPass = clsGlobalVariables.igRetries_Pass_RelayTest;
                //}
                //Retry counter is checked.
                while (imRetryCountPassFail < clsGlobalVariables.igMax_Retries_PassFail_RelayTest)
                {
                   
                        if (imRetryCountPass > (clsGlobalVariables.igRetries_Pass_RelayTest * 2))
                            break;
                        else if (imRetryCountFail > (clsGlobalVariables.igRetries_Fail_RelayTest * 2))
                            break;
                    

                    Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 4);

                    clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = btmSlaveID;
                    clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_READ_ADC_COUNT;

                    if (btmEventype == clsGlobalVariables.RLY_ON)
                    {
                        imHiLmt = clsGlobalVariables.RLY_ON_ADC_HIGH_LMT_COUNT;
                        imLowLmt = clsGlobalVariables.RLY_ON_ADC_LOW_LMT_COUNT;
                    }
                    else
                    {
                        imHiLmt = clsGlobalVariables.RLY_OFF_ADC_HIGH_LMT_COUNT;
                        imLowLmt = clsGlobalVariables.RLY_OFF_ADC_LOW_LMT_COUNT;
                    }
                    //Actual query is sent here
                    btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                    imRetryCountPassFail++;

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                       

                        lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 2, 2);
                        //This is validation of counts
                        if ((lmData < imLowLmt) || (lmData > imHiLmt))
                        {
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            imRetryCountFail++;
                            imRetryCountPass = 0;
                        }
                        else
                        {
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            imRetryCountPass++;
                            imRetryCountFail = 0;
                        }
                    }
                }
                return btmRetVal;
	        }
	        catch (Exception ex)
	        {
		        throw ex;
	        }
        }

        ///<MemberName>MBWriteCalibConst</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<ModifiedBy> Shubham</ModifiedBy>
        ///<Modified Date>24-02-2018</Modified Date>
        ///<Version>V16</Version>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function writes Calibration counts in the device.
        ///</summary>
        ///<param name="blnIsVREFPresent">
        ///Value of this parameter can be either true or false.
        ///If this is true then software will send Vref value to DUT.
        ///If this is false then software will not send Vref value to DUT.
        ///</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBWriteCalibConst(bool blnIsVREFPresent,byte slaveID,byte DUT)
        {
            byte btmRetVal;
            int imCounter = 0;
            int imLoopMax = 0;
            try
            {
                string[] strgarrCalibConst = new string[9]; 
                switch (DUT)
                {
                    case 1:
                        strgarrCalibConst = clsGlobalVariables.strgarrCalibConstDUT1;
                        break;
                    case 2:
                        strgarrCalibConst = clsGlobalVariables.strgarrCalibConstDUT2;
                        break;
                    case 3:
                        strgarrCalibConst = clsGlobalVariables.strgarrCalibConstDUT3;
                        break;
                    case 4:
                        strgarrCalibConst = clsGlobalVariables.strgarrCalibConstDUT4;
                        break;
                }
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);

                //If "blnIsVREFPresent" is false then query without VREF data will be send to DUT.
                if (((clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igSingleActingWithAnalogIPType) ||
                    (clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWithAnalogIPType) ||
                    (clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWithAnalogIPWOModbusType)) && blnIsVREFPresent == false)
                {//For Cat Id having analog input this query length is increased.
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 36);
                    //This is done because VREF value will not be sent to the device here.
                    //In the below array at last index VERF value is present.                    
                    imLoopMax = strgarrCalibConst.Length - 1;
                }
                //If "blnIsVREFPresent" is true then query with VREF data will be send to DUT.
                else if (((clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igSingleActingWithAnalogIPType) ||
                (clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWithAnalogIPType) ||
                (clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWithAnalogIPWOModbusType)) && blnIsVREFPresent == true)
                {//For Cat Id having analog input this query length is increased.
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 40);
                    imLoopMax = strgarrCalibConst.Length;
                }
                else
                {//this query length is for Cat Id without analog ip.
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 20);
                    //This is modified because "strgarrCalibConst" length is increased by one to store the value of VREF. 
                    imLoopMax = strgarrCalibConst.Length - 5;
                }
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] =slaveID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] = clsGlobalVariables.MB_WRITE_CALIB_CONST;

                for (int imArrayIndex = 0; imArrayIndex < imLoopMax; imArrayIndex++)
                {

                    if (strgarrCalibConst[imArrayIndex] == null)
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Success; 
                    }
                    //This "strgarrCalibConst" array contains data ijn hex strings seperated by ",".
                    String[] substrings = strgarrCalibConst[imArrayIndex].Split(',');
                    imCounter = 0;
                    for (int imParametersIndex = substrings.Length - 1; imParametersIndex >= 0; imParametersIndex--)
                    {
                        //Data is stored in the array in reverse direction.
                        //e.g. "12,FA,58,46" is the string so the data will get written into the DUT is "46,58,FA,12".
                        clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS + ((imArrayIndex * 4) + imCounter) + 1] = Convert.ToByte(substrings[imParametersIndex]);
                        imCounter = imCounter + 1;
                    }
                    Array.Clear(substrings, 0, substrings.Length);   
                }
                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MBQueryForSingleActingDevices</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function is used to send query to Single Acting device.
        ///</summary>
        ///<param name="btmFuncCode">This is function code of Query which is to be sent to device.</param>
        ///<param name="imData">This data is sent to DUT.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MBQueryForWOModbusDevices(int btmSlaveCode, byte btmFuncCode, int imData)
        {        
            byte btmRetVal;
            byte[] btmDataBuff = new byte[3];

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 8);

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] =(byte) btmSlaveCode;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 1] = clsGlobalVariables.MB_MASTER_TO_DUT;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 2] = btmFuncCode;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 3] = (byte)(imData / 256);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 4] = (byte)(imData & 0xFF);

                btmDataBuff[0] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 2];
                btmDataBuff[1] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 3];
                btmDataBuff[2] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 4];

                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 5] = clsGlobalVariables.objGlobalFunction.CalculateChecksum((byte)btmDataBuff.Length, ref btmDataBuff);
                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, true);

                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }
        //public byte MBQueryForWOModbusDevicesPI(byte btmFuncCode, int imData)
        //{
        //    byte btmRetVal;
        //    byte[] btmDataBuff = new byte[3];
        //    try
        //    {
        //        Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
        //        Array.Resize(ref clsGlobalVariables.btgTxBuffer, 8);
        //        clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] = clsGlobalVariables.MB_SLAVE3_ID;
        //        clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 1] = clsGlobalVariables.MB_MASTER_TO_DUT;
        //        clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 2] = btmFuncCode;
        //        clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 3] = (byte)(imData / 256);
        //        clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 4] = (byte)(imData & 0xFF);
        //        btmDataBuff[0] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 2];
        //        btmDataBuff[1] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 3];
        //        btmDataBuff[2] = clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 4];
        //        clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS + 5] = clsGlobalVariables.objGlobalFunction.CalculateChecksum((byte)btmDataBuff.Length, ref btmDataBuff);
        //        btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut_PI, true);
        //        return btmRetVal;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        ///<MemberName>MBReferenceVoltageReadSingleActing</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Version>V16</Version>
        ///<Date>24/02/2018</Date>
        ///<summary>
        ///New Query is added in the software for devices without modbus. This query reads 
        ///the VREF value from the DUT and saves it into global variable.
        ///</summary>        
        ///<ClassName>clsQueries</ClassName>
        public byte MBReferenceVoltageReadSingleActing(byte DUT)
        {
            try
            {
                byte btmRetVal;
                //This array will hold VREF value.
                byte[] btgRefVtgData = new byte[4];
                //Source is made OFF here.
                btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);
                if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                {
                   // clsMessages.DisplayMessage(clsMessageIDs.REMOVE_SOURCE_CONN);
                    btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {                        
                        return btmRetVal;
                    }

                    btmRetVal = clsGlobalVariables.objGlobalFunction.AdjustModeOfDevice(clsGlobalVariables.START_MODE, DUT);
                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        //Delays are added here.
                       // if (clsGlobalVariables.blngIsDebugPresent == true)
                        //{
                            //CA55  Program.objMainForm.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtVREFDelayStartMode.Text));
                       // }
                        //else
                       // {
                            //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.VREF_READ_DELAY_STARTMODE);
                       // }
                    }
                    else
                    {
                        return btmRetVal;
                    }

                    btmRetVal = clsGlobalVariables.objGlobalFunction.AdjustModeOfDevice(clsGlobalVariables.RUN_MODE,DUT);
                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        //Delays are added here.
                        //if (clsGlobalVariables.blngIsDebugPresent == true)
                        //{
                            //CA55  Program.objMainForm.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtVREFDelayRunMode.Text));
                        //}
                        //else
                        //{
                            //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.VREF_READ_DELAY_RUNMODE);
                        //}
                    }
                    else
                    {
                        return btmRetVal;
                    }
                    //Only in case of Auto calibration query to calibrator can be sent.
                    
                        //Source OFF is checked. 
                        if (clsGlobalVariables.objCalibQueriescls.CheckSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                   

                    //Calculate VREF query is send to device.
                    btmRetVal = MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.CALIBRATE_FUNC_CODE, clsGlobalVariables.CALC_VREF);
                    if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    {
                        //LSB data of the VREF is read from the Device.
                        btmRetVal = MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.READ_FUNC_CODE, clsGlobalVariables.READ_VREF_LSB);
                        if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                        {
                            //Data is saved in the array.
                            btgRefVtgData[0] = clsGlobalVariables.btgRxBuffer[3];
                            btgRefVtgData[1] = clsGlobalVariables.btgRxBuffer[4];
                            //MSB data of the VREF is read from the Device.
                            btmRetVal = MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.READ_FUNC_CODE, clsGlobalVariables.READ_VREF_MSB);
                            if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                            {
                                //Data is saved in the array.
                                btgRefVtgData[2] = clsGlobalVariables.btgRxBuffer[3];
                                btgRefVtgData[3] = clsGlobalVariables.btgRxBuffer[4];
                                //Data is saved in the global variable.
                                switch (DUT)
                                {
                                    case 1:
                                        clsGlobalVariables.fltgREF_VtgDUT1 = System.BitConverter.ToSingle(btgRefVtgData, 0);
                                        break;
                                    case 2:
                                        clsGlobalVariables.fltgREF_VtgDUT2 = System.BitConverter.ToSingle(btgRefVtgData, 0);
                                        break;
                                    case 3:
                                        clsGlobalVariables.fltgREF_VtgDUT3 = System.BitConverter.ToSingle(btgRefVtgData, 0);
                                        break;
                                    case 4:
                                        clsGlobalVariables.fltgREF_VtgDUT4 = System.BitConverter.ToSingle(btgRefVtgData, 0);
                                        break;
                                }
                                
                            }
                        }
                    }
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------Changes End.
        #endregion
    }
}
