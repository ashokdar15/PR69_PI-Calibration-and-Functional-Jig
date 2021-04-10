using PR69_PI_Calibration_and_Functional_Jig.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    public class clsPLCQueries
    {
        public Boolean GetModeStatus(Byte[] btmmStatus)
        {
            try
            {
                const int BC_DEVICE_MODE_STATUS_QUERY_LEN = 10;                
                clsGlobalVariables.btgTxBuffer = new Byte[BC_DEVICE_MODE_STATUS_QUERY_LEN];

                clsGlobalVariables.btgTxBuffer[Convert.ToByte(clsGlobalVariables.enmMbQueryPos.MB_ID_POS)] = 0xFA;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbUpFrmQryPos.MB_ID_POS] = clsGlobalVariables.PC_MODBUS_ID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbUpFrmQryPos.MB_FUNCTION_POS] = clsGlobalVariables.MB_FUNC_UPDATEFIRMWARE;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbUpFrmQryPos.MB_DEVICE_ID] = 0;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbUpFrmQryPos.MB_SUB_FUNC_POS] = clsGlobalVariables.MB_SUBFUNC_GET_MODE_STATUS;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbUpFrmQryPos.MB_NO_OF_BYTES_POS] = 0;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbUpFrmQryPos.MB_PAGE_NO] = 0;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbUpFrmQryPos.MB_OFFSET_ADDR_HIGH_POS] = 0;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbUpFrmQryPos.MB_OFFSET_ADDR_LOW_POS] = 0;
                clsGlobalVariables.DEVICE_MODE_STATUS = 0;

                Byte btmStatus = MainWindowVM.initilizeCommonObject.objplcSerialComm.SendQueryGetResponse(clsGlobalVariables.igTimeOutMedium, true);
                if (btmStatus == 0)
                {
                    btmmStatus[0] = clsGlobalVariables.btgRxBuffer[(int)clsGlobalVariables.enmMbUpFrmRespPos.MB_RESP_DATA_POS];
                    clsGlobalVariables.DEVICE_MODE_STATUS = btmmStatus[0];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }
        ///<MemberName>AutoComPortQueryForPLC</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Ashok</CreatedBy>
        ///<CommentedBy>Ashok</CommentedBy>
        ///<Date>11/11/2020</Date>
        ///<summary>
        ///This function is used to send query to PLC to check on which com port PLC is connected.
        ///</summary>        
        ///<ClassName>clsQueries</ClassName>
        public byte AutoComPortQueryForPLC()
        {
            byte btmRetVal= (byte)clsGlobalVariables.enmResponseError.Failed;
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 8);
                int lgmStartAdd = 0;
                UInt16 LADDER_RUN_MODE_CODE = 0xFF00;
                clsGlobalVariables.btgTxBuffer[Convert.ToByte(clsGlobalVariables.enmMbQueryPos.MB_ID_POS)] = 0xFA;
                clsGlobalVariables.btgTxBuffer[Convert.ToByte(clsGlobalVariables.enmMbQueryPos.MB_FUNCTION_POS)] = 0x68;
                clsGlobalVariables.btgTxBuffer[Convert.ToByte(clsGlobalVariables.enmMbQueryPos.MB_START_ADD_HIGH_POS)] = ExtractByteFromInteger((lgmStartAdd), false);
                clsGlobalVariables.btgTxBuffer[Convert.ToByte(clsGlobalVariables.enmMbQueryPos.MB_START_ADD_LOW_POS)] = ExtractByteFromInteger((lgmStartAdd), true);
                clsGlobalVariables.btgTxBuffer[Convert.ToByte(clsGlobalVariables.enmMbQueryPos.MB_NO_OF_POINTS_HIGH_POS)] = ExtractByteFromInteger((LADDER_RUN_MODE_CODE), false);
                clsGlobalVariables.btgTxBuffer[Convert.ToByte(clsGlobalVariables.enmMbQueryPos.MB_NO_OF_POINTS_LOW_POS)] = ExtractByteFromInteger((LADDER_RUN_MODE_CODE), true);

                btmRetVal = MainWindowVM.initilizeCommonObject.objplcSerialComm.SendQueryGetResponse(clsGlobalVariables.igPV_PLC_DELAY, true);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    Byte[] ucmInRspArr = { 0 };
                    if (GetModeStatus(ucmInRspArr) == true)
                    {
                        //if Run mode query is send then Device should be in run mode.
                        if (clsGlobalVariables.DEVICE_MODE_STATUS == clsGlobalVariables.DEVICE_RUN_MODE)
                            return 0;
                    }
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte ExtractByteFromInteger(long lgmInData, Boolean blnmHigh)
        {
            String strmHexVal = "";
            int imVal;
            int imlength;
            try
            {
                //e.g ssvInData=2013 and after converting it into Hex we will get 7DD.
                strmHexVal = lgmInData.ToString("X");
                //    smHexVal = Convert.ToString(ssvInData);
                imlength = (strmHexVal.Length - 1);
                //Now loop will append that (4 - length of smHexVal) 0s after loop smHexVal will contain 07DD.
                for (imVal = 0; imVal < (4 - (imlength + 1)); imVal++)
                {
                    strmHexVal = "0" + strmHexVal;
                }
                if (blnmHigh)
                {
                    return (Convert.ToByte(strmHexVal.Substring(2, 2), 16));
                }
                else
                {
                    return (Convert.ToByte(strmHexVal.Substring(0, 2), 16));
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return 0;
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
        public byte MBStartRelayTest_PI(byte btmData)
        {
            Byte[] btmRspArr = { 0 };
            try
            {
                if (ReadInputStatusQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR, 0x8, ref btmRspArr))
                {
                    if (btmRspArr[0] != btmData)
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }
                else
                {
                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }
                ////2sec
                ////CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST_PLC);
                ////06
                //if (ReadInputStatusQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR, 0x8, ref btmRspArr))
                //{
                //    if (btmRspArr[0] != 6)
                //        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                //}
                //else
                //    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                ////2sec
                ////CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST_PLC);
                ////0A
                //if (ReadInputStatusQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR, 0x8, ref btmRspArr))
                //{
                //    if (btmRspArr[0] != 0x0A)
                //        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                //}
                //else
                //    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                ////2sec
                ////CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST_PLC);
                ////09
                //if (ReadInputStatusQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR, 0x8, ref btmRspArr))
                //{
                //    if (btmRspArr[0] != 9)
                //        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                //}
                //else
                //    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                ////05
                //if (ReadInputStatusQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR, 0x8, ref btmRspArr))
                //{
                //    if (btmRspArr[0] != 5)
                //        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                //}
                //else
                //    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;

                return (byte)clsGlobalVariables.enmResponseError.Success;
            }
            catch (Exception ex)
            {
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
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
        public byte MBStartModBusTest_PI()
        {
            Byte[] btmRspArr = { 0 };
            try
            {
                //Off Q0
                if (!ForceSingleCoilQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR + 0x0, clsGlobalVariables.COIL_OFF))
                {
                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }
                //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                //On Q0
                if (!ForceSingleCoilQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR + 0x0, clsGlobalVariables.COIL_ON))
                {
                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }
                //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_MODBUS_DELAY);
                //Read Q1
                Byte[] btmInputRspArr = { 0 };
                if (ReadCoilStatusQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR+1 , 1, ref btmInputRspArr) == true)
                {
                    if (btmInputRspArr[0] == 1)
                    {
                        //Off Q0
                        if (!ForceSingleCoilQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR + 0x0, clsGlobalVariables.COIL_OFF))
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                        return (byte)clsGlobalVariables.enmResponseError.Success;
                    }
                }
                //Off Q0
                if (!ForceSingleCoilQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR + 0x0, clsGlobalVariables.COIL_OFF))
                {
                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }

                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte MBStartReadPLC_Input_ON(byte InputNumber)
        {
            Byte[] btmRspArr = { 0 };
            try
            {
                //Read Q1
                Byte[] btmInputRspArr = { 0 };
                byte actualAddress = (byte)(InputNumber / 8);
                byte inputNUmber = (byte)(InputNumber % 8);
                if (ReadInputStatusQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR , 24, ref btmInputRspArr) == true)
                {
                    if ((btmInputRspArr[actualAddress] &  (1<< inputNUmber)) == ( 1<< inputNUmber))
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Success;
                    }
                }
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte MBStartReadPLC_Input_OFF(byte InputNumber)
        {
            Byte[] btmRspArr = { 0 };
            try
            {
                //Read Q1
                Byte[] btmInputRspArr = { 0 };
                byte actualAddress = (byte)(InputNumber / 8);
                byte inputNUmber = (byte)(InputNumber % 8);
                if (ReadInputStatusQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR, 24, ref btmInputRspArr) == true)
                {
                    if ((btmInputRspArr[actualAddress] & (1 << inputNUmber)) != (1 << inputNUmber))
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Success;
                    }
                }
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte MBStartPLC_ON(byte outputNumber)
        {
            Byte[] btmRspArr = { 0 };
            try
            {
                
                //On Q0
                if (ForceSingleCoilQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR + outputNumber, clsGlobalVariables.COIL_ON))
                {
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }

               
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte MBStartPLC_OFF(byte outputNumber)
        {
            Byte[] btmRspArr = { 0 };
            try
            {

                //On Q0
                if (ForceSingleCoilQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR + outputNumber, clsGlobalVariables.COIL_OFF))
                {
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }


                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte DutONOFFQueryToPLC(bool DutONOFF)
        {
            try
            {
                if (DutONOFF)
                {
                    byte DUT_ON_Value = 0;
                    switch ( clsGlobalVariables.NUMBER_OF_DUTS)
                    {
                        case 4:
                            DUT_ON_Value = 0xff;
                            break;
                        case 3:
                            DUT_ON_Value = 0x77;
                            break;
                        case 2:
                            DUT_ON_Value = 0x33;
                            break;
                        case 1:
                            DUT_ON_Value = 0x11;
                            break;
                        
                    }
                    if (!ForceMultipleCoilsQuery( clsGlobalVariables.BASE_ADDR ,8,1, DUT_ON_Value))
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    
                }
                else
                {
                    byte DUT_ON_Value = 0;
                    if (!ForceMultipleCoilsQuery(clsGlobalVariables.BASE_ADDR, 8, 1, DUT_ON_Value))
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }
                return (byte)clsGlobalVariables.enmResponseError.Success;
            }
            catch (Exception ex)
            {
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
        }
        public byte MBReadPLC_Output()
        {
            Byte[] btmRspArr = { 0 };
            try
            {
                //Read Q2
                Byte[] btmInputRspArr = { 0 };
                if (ReadCoilStatusQuery(clsGlobalVariables.PC_MODBUS_ID, clsGlobalVariables.BASE_ADDR + 3, 1, ref btmInputRspArr) == true)
                    if (btmInputRspArr[0] == 1)
                        return (byte)clsGlobalVariables.enmResponseError.Success;
                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            
        //<MemberName>ReadCoilStatusQuery</MemberName>
        // <MemberType>Method</MemberType>
        // <CreatedBy>Ravina Bothara</CreatedBy>
        // <CommentedBy>Ravina Bothara</CommentedBy>
        // <ModifiedBy></ModifiedBy>
        // <Date>26/4/2017</Date>
        // <ModifiedDate></ModifiedDate>
        // <Version>V10.000</Version>
        // <ModifiedVersion></ModifiedVersion>
        // <summary> Reads the ON/OFF status of discrete outputs (0X references, coils) in the slave</summary>
        // <ModifiedSummary>  
        // </ModifiedSummary>
        // <remarks></remarks>
        // <ClassName>clsGeneralQuery</ClassName>
        public Boolean ReadCoilStatusQuery(Byte btmMbDeviceID, long lgStartCoilAddr, int imNumOfCoils, ref Byte[] btmCoilData)
        {
            try
            {
                const int QUERY_HEADER = 6;
                const int QUERY_FOOTER = 2;
                const int READ_COILSTATUS_QUERY_LEN = (QUERY_HEADER + QUERY_FOOTER);
                Byte[] btmInputRspArr = { 0,0 };

                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, READ_COILSTATUS_QUERY_LEN);



                int imByteCnt;
                Byte btmResponse;
                // Prepare query
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_ID_POS] = btmMbDeviceID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_FUNCTION_POS] = clsGlobalVariables.MB_FUNC_READ_COILSTATUS;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_START_ADD_HIGH_POS] = ExtractByteFromInteger(lgStartCoilAddr, false);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_START_ADD_LOW_POS] = ExtractByteFromInteger(lgStartCoilAddr, true);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_NO_OF_POINTS_HIGH_POS] = ExtractByteFromInteger(imNumOfCoils, false);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_NO_OF_POINTS_LOW_POS] = ExtractByteFromInteger(imNumOfCoils, true);

               

                btmResponse = MainWindowVM.initilizeCommonObject.objplcSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_PLC_TimeOut_PI, true);

                if (btmResponse == 0)
                {
                    for (imByteCnt = 0; imByteCnt < clsGlobalVariables.btgRxBuffer[(int)clsGlobalVariables.enmMbRespPos.MB_BYTE_COUNT]; imByteCnt++)
                    {
                        btmCoilData[imByteCnt] = clsGlobalVariables.btgRxBuffer[(int)clsGlobalVariables.enmMbRespPos.MB_RESP_DATA_POS + imByteCnt];
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        //<MemberName>ForceSingleCoilQuery</MemberName>
        // <MemberType>Method</MemberType>
        // <CreatedBy>Ravina Bothara</CreatedBy>
        // <CommentedBy>Ravina Bothara</CommentedBy>
        // <ModifiedBy></ModifiedBy>
        // <Date>26/4/2017</Date>
        // <ModifiedDate></ModifiedDate>
        // <Version>V10.000</Version>
        // <ModifiedVersion></ModifiedVersion>
        // <summary>Forces a single coil (0X reference) to either ON or OFF.</summary>
        // <ModifiedSummary>  
        // </ModifiedSummary>
        // <remarks></remarks>
        // <ClassName>clsGeneralQuery</ClassName>
        public Boolean ForceSingleCoilQuery(Byte btmMbDeviceID, long lgmStartCoilAdd, int imForceData)
        {

            try
            {
                const int QUERY_HEADER = 6;
                const int QUERY_FOOTER = 2;
                const int FORCE_SINGLE_COIL_QUERY_LEN = (QUERY_HEADER + QUERY_FOOTER);
                Byte btmResponse;
                Byte[] btmTxBuff = new byte[FORCE_SINGLE_COIL_QUERY_LEN];


                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, FORCE_SINGLE_COIL_QUERY_LEN);



                //Prepare query
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_ID_POS] = btmMbDeviceID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_FUNCTION_POS] = clsGlobalVariables.MB_FUNC_FORCE_SINGLECOIL;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_START_ADD_HIGH_POS] = ExtractByteFromInteger(lgmStartCoilAdd, false);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_START_ADD_LOW_POS] = ExtractByteFromInteger(lgmStartCoilAdd, true);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_NO_OF_POINTS_HIGH_POS] = ExtractByteFromInteger(imForceData, false);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_NO_OF_POINTS_LOW_POS] = ExtractByteFromInteger(imForceData, true);


                btmResponse = MainWindowVM.initilizeCommonObject.objplcSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_PLC_TimeOut_PI, true);

                if (btmResponse == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        //<MemberName>ReadInputStatusQuery</MemberName>
        // <MemberType>Method</MemberType>
        // <CreatedBy>Ravina Bothara</CreatedBy>
        // <CommentedBy>Ravina Bothara</CommentedBy>
        // <ModifiedBy></ModifiedBy>
        // <Date>26/4/2017</Date>
        // <ModifiedDate></ModifiedDate>
        // <Version>V10.000</Version>
        // <ModifiedVersion></ModifiedVersion>
        // <summary> Reads the ON/OFF status of discrete inputs (1X references) in the slave</summary>
        // <ModifiedSummary>  
        // </ModifiedSummary>
        // <remarks></remarks>
        // <ClassName>clsGeneralQuery</ClassName>
        public Boolean ReadInputStatusQuery(Byte btmMbDeviceID, long lmStartRegAddr, int imNumOfInputs, ref Byte[] rbtInputData)
        {
            const int QUERY_HEADER = 6;
            const int QUERY_FOOTER = 2;
            try
            {
                const int READ_INPUTSTATUS_QUERY_LEN = (QUERY_HEADER + QUERY_FOOTER);

                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, READ_INPUTSTATUS_QUERY_LEN);

                //Byte[] btmTxBuff = new byte[READ_INPUTSTATUS_QUERY_LEN];
                Byte btmResponse;
                int imByteCnt;
                //Prepare query.
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_ID_POS] = btmMbDeviceID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_FUNCTION_POS] = clsGlobalVariables.MB_FUNC_READ_INPUTSTATUS;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_START_ADD_HIGH_POS] = ExtractByteFromInteger(lmStartRegAddr, false);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_START_ADD_LOW_POS] = ExtractByteFromInteger(lmStartRegAddr, true);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_NO_OF_POINTS_HIGH_POS] = ExtractByteFromInteger(imNumOfInputs, false);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_NO_OF_POINTS_LOW_POS] = ExtractByteFromInteger(imNumOfInputs, true);

                // Send query and get response. btgRxBuffer
                btmResponse = MainWindowVM.initilizeCommonObject.objplcSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_PLC_TimeOut_PI, true);
                // btmResponse = ogMain.omSerial.SendQueryGetResponse(clsGlobalVariables.igTimeOutLow, btmTxBuff, strmCom);
                if (btmResponse == 0)
                {
                     rbtInputData = new byte[10];
                    for (imByteCnt = 0; imByteCnt < clsGlobalVariables.btgRxBuffer[(int)clsGlobalVariables.enmMbRespPos.MB_BYTE_COUNT]; imByteCnt++)
                    {
                        rbtInputData[imByteCnt] = clsGlobalVariables.btgRxBuffer[(int)clsGlobalVariables.enmMbRespPos.MB_RESP_DATA_POS + imByteCnt];
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public Boolean ForceMultipleCoilsQuery( long lgStartCoilAdd, int imNumCoils, int imNumCoilsDataBytes,byte rbtmCoilData)
        {
            try
            {
                const int QUERY_HEADER = 7;
                const int QUERY_FOOTER = 2;
                Byte btmResponse;
                const int FORCE_MULTIPLE_COILS_QUERY_LEN = (QUERY_HEADER + QUERY_FOOTER);

                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, FORCE_MULTIPLE_COILS_QUERY_LEN + imNumCoilsDataBytes);
                // Prepare query
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_ID_POS] = clsGlobalVariables.PC_MODBUS_ID;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_FUNCTION_POS] = clsGlobalVariables.MB_FUNC_FORCE_MULTICOILS;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_START_ADD_HIGH_POS] = ExtractByteFromInteger(lgStartCoilAdd, false);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_START_ADD_LOW_POS] = ExtractByteFromInteger(lgStartCoilAdd, true);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_NO_OF_POINTS_HIGH_POS] = ExtractByteFromInteger(imNumCoils, false);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_NO_OF_POINTS_LOW_POS] = ExtractByteFromInteger(imNumCoils, true);
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_NO_OF_DATA_BYTES_POS] = (Byte)imNumCoilsDataBytes;
                clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmMbQryPos.MB_DATA_BYTES_POS ] = rbtmCoilData;
                
                btmResponse = MainWindowVM.initilizeCommonObject.objplcSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_PLC_TimeOut_PI, true);
                if (btmResponse == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
    }
}
