using PR69_Function_and_Calibration_JIG.Classes;
using PR69_PI_Calibration_and_Functional_Jig.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    public class clsCalibratorQueries
    {
        #region "---------------------------Calibrator Queries--------------------------"

        ///<MemberName>ChangeCalibratorSensor</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy></CreatedBy>
        ///<CommentedBy></CommentedBy>
        ///<Date></Date>
        ///<summary>
        ///This function is used to set the sensor in the Calibrator by using Query. Sensor value is passed to it as a parameter.
        ///This is Calibrator Query.
        ///</summary>        
        ///<param name="btmSensorVal">This is sensor value to be set in Calibrator.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte ChangeCalibratorSensor(byte btmSensorVal, byte DUT_ID)
        {
            byte btmRetVal;

            try
            {
                //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator); 
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);
                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'R';
                clsGlobalVariables.btgTxBuffer[2] = btmSensorVal;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;
                //For the above query Calibrator doen not give response, so here send Query function is used.
               btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);

                    clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'R';
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                    
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if (clsGlobalVariables.btgRxBuffer[2] == btmSensorVal)
                            return btmRetVal;
                        else
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;

                    }
                    else
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                else
                {
                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ReadCalibratorMeasureValue</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy></CreatedBy>
        ///<CommentedBy></CommentedBy>
        ///<Date>12/05/</Date>
        ///<summary>
        ///This function send the query to read the value of Calibrator measure.
        ///This is Calibrator Query.
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public byte ReadCalibratorMeasureValue( byte DUT_ID)
        {
            byte btmRetVal;
            int imLoopcounter;
            string strmTempValue = "";

            try
            {
                //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 4);

                clsGlobalVariables.btgTxBuffer[0] = (byte)'O';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'D';
                clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.LF;

                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);

                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    if (clsGlobalVariables.btgRxBuffer[clsGlobalVariables.btgRxBuffer.Length - 1] != clsGlobalVariables.LF)
                    {
                        ReadCalibratorMeasureValue(DUT_ID);
                    }

                    for (imLoopcounter = 0; imLoopcounter < clsGlobalVariables.btgRxBuffer.Length; imLoopcounter++)
                    {
                        if ((char)clsGlobalVariables.btgRxBuffer[imLoopcounter] == 'E')
                            break;
                        else
                        {
                            strmTempValue = strmTempValue + (char)clsGlobalVariables.btgRxBuffer[imLoopcounter];
                        }
                    }
                }
                else
                {
                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }
                clsGlobalVariables.strgAnalogData = strmTempValue;
                return (byte)clsGlobalVariables.enmResponseError.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>CheckSourceKnobPos</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy></CreatedBy>
        ///<CommentedBy></CommentedBy>
        ///<Date>12/05/</Date>
        ///<summary>
        ///This function send the query to read the knob position of source. After reading knob position,
        ///this is compared with first parameter of the function. If it does not match then software displays
        ///error message mentioning second parameter.
        ///This is Calibrator Query.
        ///</summary>
        ///<param name="btmKnobPos">This is position of knob to be compared.</param>
        ///<param name="strmKnob">This is text on the knob.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte CheckSourceKnobPos(byte btmKnobPos, string strmKnob, byte DUT_ID)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            int imRetryCounter = 0;
            try
            {
                //Three retires are provided to keep knob at correct position.
                while (imRetryCounter < 3)
                {
                    //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                    Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                    clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'F';
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                    clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                    switch (DUT_ID)
                    {
                        case 1:
                            _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                            break;
                        case 2:
                            _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                            break;
                        case 3:
                            _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                            break;
                        case 4:
                            _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                            break;
                    }
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) == btmKnobPos)
                        {
                            imRetryCounter = 3;
                            return (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        else
                        {
                            imRetryCounter = imRetryCounter + 1;
                            if (imRetryCounter == 3)
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            MessageBox.Show("Set Source Knob to " + strmKnob + " Position.", clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        imRetryCounter = 3;
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
        public byte CheckSourceSetPosition(byte btmKnobPos, Byte knobData, byte DUT_ID)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            int imRetryCounter = 0;
            try
            {
                //Three retires are provided to keep knob at correct position.
                while (imRetryCounter < 3)
                {
                    //frmMain.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                    Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                    clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'F';
                    clsGlobalVariables.btgTxBuffer[2] = (byte)Convert.ToChar(knobData.ToString());
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;
                    clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                    switch (DUT_ID)
                    {
                        case 1:
                            _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                            break;
                        case 2:
                            _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                            break;
                        case 3:
                            _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                            break;
                        case 4:
                            _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                            break;
                    }
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) == btmKnobPos)
                        {
                            imRetryCounter = 3;
                            return (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        else
                        {
                            imRetryCounter = imRetryCounter + 1;
                            if (imRetryCounter == 3)
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            //MessageBox.Show("Set Source Knob to " + strmKnob + " Position.", clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        imRetryCounter = 3;
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
        ///<MemberName>MakeCalibratorMeasureOnAndReadKnobPos</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy></CreatedBy>
        ///<CommentedBy></CommentedBy>
        ///<Date>12/05/</Date>
        ///<summary>
        ///This function send the query to first to ON measure of calibrator then read the knob position of measure. After reading knob position,
        ///this is compared with first parameter of the function. If it does not match then software displays
        ///error message mentioning second parameter.
        ///This is Calibrator Query.
        ///</summary>
        ///<param name="btmKnobPos">This is position of knob to be compared.</param>
        ///<param name="strmKnob">This is text on the knob.</param>
        ///<ClassName>clsQueries</ClassName>
        public byte MakeCalibratorMeasureOnAndReadKnobPos(byte btmKnobPos, string strmKnob, byte DUT_ID)
        {
            byte btmRetVal;
            int imRetryCounter = 0;

            try
            {
                //frmMain.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);
                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                clsGlobalVariables.btgTxBuffer[0] = (byte)'M';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.ONE_Val;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

               btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);

                    clsGlobalVariables.btgTxBuffer[0] = (byte)'M';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                    
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) != 1)
                        {
                            clsMessages.DisplayMessage(clsMessageIDs.CALIB_MEASURE_NOT_ON);
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                    else
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                    clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);





                    //Three retires are provided to keep knob at correct position.
                    while (imRetryCounter < 3)
                    {

                        Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                        Array.Resize(ref clsGlobalVariables.btgTxBuffer, 7);

                        clsGlobalVariables.btgTxBuffer[0] = (byte)'M';
                        clsGlobalVariables.btgTxBuffer[1] = (byte)'F';
                        clsGlobalVariables.btgTxBuffer[2] = (byte)'0';
                        clsGlobalVariables.btgTxBuffer[3] = (byte)',';

                        if (clsGlobalVariables.MEASURE_mA_KNOB_POS == btmKnobPos)
                        {
                            clsGlobalVariables.btgTxBuffer[4] = (byte)'1';
                        }
                        else
                        {
                            clsGlobalVariables.btgTxBuffer[4] = (byte)'0';
                        }

                        clsGlobalVariables.btgTxBuffer[5] = clsGlobalVariables.CR;
                        clsGlobalVariables.btgTxBuffer[6] = clsGlobalVariables.LF;

                       btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            imRetryCounter = 3;
                            return btmRetVal;
                        }
                        Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                        Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);
                        //frmMain.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                        clsGlobalVariables.btgTxBuffer[0] = (byte)'M';
                        clsGlobalVariables.btgTxBuffer[1] = (byte)'F';
                        clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                        clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                        clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                        
                        
                        btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);


                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) == btmKnobPos)
                            {
                                clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);
                                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 7);

                                clsGlobalVariables.btgTxBuffer[0] = (byte)'M';
                                clsGlobalVariables.btgTxBuffer[1] = (byte)'R';
                                clsGlobalVariables.btgTxBuffer[2] = (byte)'0';
                                clsGlobalVariables.btgTxBuffer[3] = (byte)',';

                                if (clsGlobalVariables.MEASURE_mA_KNOB_POS == btmKnobPos)
                                {
                                    clsGlobalVariables.btgTxBuffer[4] = (byte)'0';
                                }
                                else
                                {
                                    clsGlobalVariables.btgTxBuffer[4] = (byte)'2';
                                }

                                clsGlobalVariables.btgTxBuffer[5] = clsGlobalVariables.CR;
                                clsGlobalVariables.btgTxBuffer[6] = clsGlobalVariables.LF;

                               btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {



                                    imRetryCounter = 3;
                                    return btmRetVal;
                                }
                                else
                                {
                                    imRetryCounter = 3;
                                    return (byte)clsGlobalVariables.enmResponseError.Success;
                                }

                            }
                            else
                            {
                                imRetryCounter = imRetryCounter + 1;
                                if (imRetryCounter == 3)
                                {
                                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                }
                                //MessageBox.Show("Set measure Knob to " + strmKnob + " Position.", clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            imRetryCounter = 3;
                            return btmRetVal;
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
        ///<MemberName>MakeCalibratorSourceOn</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy></CreatedBy>
        ///<CommentedBy></CommentedBy>
        ///<Date>12/05/</Date>
        ///<summary>
        ///This function send the query to ON Source of calibrator.
        ///This is Calibrator Query.
        ///</summary>        
        ///<ClassName>clsQueries</ClassName>
        public byte MakeCalibratorSourceOn(byte DUT_ID)
        {
            byte btmRetVal;

            try
            {
                // MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);
                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.ONE_Val;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

               btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                    
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);


                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) == clsGlobalVariables.SOURCE_ON)
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        else
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                    else
                    {
                        return btmRetVal;
                    }
                }
                else
                {
                    return btmRetVal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MakeCalibratorMeasureOn</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function send the query to ON Measure of calibrator.
        ///This is Calibrator Query.
        ///</summary>        
        ///<ClassName>clsQueries</ClassName>
        public byte MakeCalibratorMeasureOn( byte DUT_ID)
        {
            byte btmRetVal;

            try
            {
                //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);
                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                clsGlobalVariables.btgTxBuffer[0] = (byte)'M';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.ONE_Val;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

               btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsGlobalVariables.btgTxBuffer[0] = (byte)'M';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                    
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);


                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        //Measure ON value is checked here.
                        if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) == clsGlobalVariables.MEASURE_ON)
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        else
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                    else
                    {
                        return btmRetVal;
                    }
                }
                else
                {
                    return btmRetVal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>MakeCalibratorMeasureOFF</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>13/11/2017</Date>
        ///<summary>
        ///This function send the query to OFF Measure of calibrator.
        ///This is Calibrator Query.
        ///</summary>        
        ///<ClassName>clsQueries</ClassName>
        public byte MakeCalibratorMeasureOFF( byte DUT_ID)
        {
            byte btmRetVal;

            try
            {
                //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);
                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                clsGlobalVariables.btgTxBuffer[0] = (byte)'M';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.ZERO_Val;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

               btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsGlobalVariables.btgTxBuffer[0] = (byte)'M';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                    
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        //Measure OFF value is checked here.
                        if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) == clsGlobalVariables.MEASURE_OFF)
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        else
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                    else
                    {
                        return btmRetVal;
                    }
                }
                else
                {
                    return btmRetVal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        ///<MemberName>MakeCalibratorSourceOFF</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function send the query to switch OFF the Source of calibrator.
        ///This is Calibrator Query.
        ///</summary>        
        ///<ClassName>clsQueries</ClassName>
        public byte MakeCalibratorSourceOFF( byte DUT_ID)
        {
            byte btmRetVal;

            try
            {
                //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);
                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.ZERO_Val;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

               btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                    
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);


                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) == clsGlobalVariables.SOURCE_OFF)
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        else
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                    else
                    {
                        return btmRetVal;
                    }
                }
                else
                {
                    return btmRetVal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>AutoComPortQueryForCalibrator</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function is used to send query to Calibrator to check on which com port calibrator is connected.
        ///This is Calibrator Query.
        ///</summary>        
        ///<ClassName>clsQueries</ClassName>
        public byte AutoComPortQueryForCalibrator( byte DUT_ID)
        {
            byte btmRetVal;
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);

                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte AutoComPortQueryForCalibratorWithSerialNumber(ref string SerialNumber)
        {
            byte btmRetVal;
            try
            {
                Byte retryCount = 0;
                while (retryCount < 3)
                {
                    Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 6);

                    clsGlobalVariables.btgTxBuffer[0] = (byte)'B';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'S';
                    clsGlobalVariables.btgTxBuffer[2] = (byte)'N';
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[5] = clsGlobalVariables.LF;

                    btmRetVal = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);
                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if (clsGlobalVariables.btgRxBuffer.Length == 11)
                        {
                            for (int i = 0; i < clsGlobalVariables.btgRxBuffer.Length - 2; i++)
                            {
                                SerialNumber += (Convert.ToChar(clsGlobalVariables.btgRxBuffer[i])).ToString();
                            }
                            return btmRetVal;
                        }
                    }
                    else
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    retryCount++;
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
        }

        ///<MemberName>CheckSourceOFF</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Version>V16</Version>
        ///<Date>27/02/2018</Date>
        ///<summary>
        ///This function is used to send query to Calibrator to check on Source is OFF.
        ///This is Calibrator Query.
        ///</summary>        
        ///<ClassName>clsCalibratorQueries</ClassName>
        public byte CheckSourceOFF(byte DUT_ID)
        {
            byte btmRetVal;

            try
            {
                //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);
                //Success is cheked here.
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    //Source OFF value is checked here with Received value from calibrator.
                    if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) == clsGlobalVariables.SOURCE_OFF)
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Success;
                    }
                    else
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                else
                {
                    return btmRetVal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        ///<MemberName>CheckSourceON</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function is used to send query to Calibrator to check on which com port calibrator is connected.
        ///This is Calibrator Query.
        ///</summary>        
        ///<ClassName>clsQueries</ClassName>
        public byte CheckSourceON(byte DUT_ID)
        {
            byte btmRetVal;

            try
            {
                //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'O';
                clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);
                //
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    if ((clsGlobalVariables.btgRxBuffer[2] - clsGlobalVariables.igCONST_FOR_ASCII) == clsGlobalVariables.SOURCE_ON)
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Success;
                    }
                    else
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                else
                {
                    return btmRetVal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte MBAdjustCalibratorVoltageOrResistance(string strValue, byte DUT_ID)
        {
            byte btmRetVal;
            int imCounter = 1, i;
            byte[] arrbtData = new byte[5];
            string strtemp = "";
            int imArrayLength = 0;

            try
            {
                //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, (4 + strValue.Length));
                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'D';
               // clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.PLUS;

                foreach (char item in strValue)
                {
                    clsGlobalVariables.btgTxBuffer[1 + imCounter] = (byte)item;
                    imCounter++;
                }
                clsGlobalVariables.btgTxBuffer[imCounter + 1] = clsGlobalVariables.CR;
                clsGlobalVariables.btgTxBuffer[imCounter + 2] = clsGlobalVariables.LF;

               btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                    clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'D';
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                    
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        for (i = 0; i < clsGlobalVariables.btgRxBuffer.Length; i++)
                        {
                            if (clsGlobalVariables.btgRxBuffer[i] != clsGlobalVariables.CR)//LF
                            {
                                imArrayLength = imArrayLength + 1;
                            }
                            else
                                break;
                        }

                        Array.Clear(arrbtData, 0, arrbtData.Length);
                        Array.Resize(ref arrbtData, imArrayLength);

                        for (i = 0; i < imArrayLength; i++)
                        {
                            arrbtData[i] = clsGlobalVariables.btgRxBuffer[i];
                        }

                        for (i = 2; i < imArrayLength; i++)
                        {
                            strtemp = strtemp + (char)arrbtData[i];
                        }
                        if (strValue.Contains("."))
                        {
                            if (Convert.ToDouble(strtemp.Replace(" ", string.Empty)) == Convert.ToDouble(strValue))
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                            else
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Convert.ToDouble(strtemp.Replace(" ", string.Empty))) == Convert.ToDouble(strValue))
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                            else
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
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
        public byte MBAdjustCalibratorVoltageOrResistanceZeroTemp(string strValue, byte DUT_ID)
        {
            byte btmRetVal;
            int imCounter = 1, i;
            byte[] arrbtData = new byte[5];
            string strtemp = "";
            int imArrayLength = 0;

            try
            {
                //MainWindowVM.initilizeCommonObject.objCalibratorSerialComm.OpenCommPort(clsGlobalVariables.strgComPortCalibrator);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                clsSerialCommunication _clsSerialComm = new clsSerialCommunication();
                switch (DUT_ID)
                {
                    case 1:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1;
                        break;
                    case 2:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2;
                        break;
                    case 3:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3;
                        break;
                    case 4:
                        _clsSerialComm = MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4;
                        break;
                }
                if (strValue.Contains("-"))
                {
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, (5 + strValue.Length-1));
                }
                else
                {
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, (5 + strValue.Length-1));
                }
                

                clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                clsGlobalVariables.btgTxBuffer[1] = (byte)'D';
                if (strValue.Contains("-"))
                {
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.NEG;
                }
                else
                {
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.PLUS;
                }

                if (strValue.Contains("-"))
                {
                    foreach (char item in strValue)
                    {
                        if ((byte)item != clsGlobalVariables.NEG)
                        {
                            clsGlobalVariables.btgTxBuffer[2 + imCounter] = (byte)item;
                            imCounter++;
                        }

                    }
                    clsGlobalVariables.btgTxBuffer[imCounter + 2] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[imCounter + 3] = clsGlobalVariables.LF;
                }
                else
                {
                    foreach (char item in strValue)
                    {
                        if ((byte)item != clsGlobalVariables.NEG)
                        {
                            clsGlobalVariables.btgTxBuffer[1 + imCounter] = (byte)item;
                            imCounter++;
                        }

                    }
                    clsGlobalVariables.btgTxBuffer[imCounter + 1] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[imCounter + 2] = clsGlobalVariables.LF;

                }
                    

               btmRetVal = _clsSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Calib_Query_TimeOut);

                //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igDelay_In_Two_Calib_Queries);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 5);

                    clsGlobalVariables.btgTxBuffer[0] = (byte)'S';
                    clsGlobalVariables.btgTxBuffer[1] = (byte)'D';
                    clsGlobalVariables.btgTxBuffer[2] = clsGlobalVariables.Question_MARK;
                    clsGlobalVariables.btgTxBuffer[3] = clsGlobalVariables.CR;
                    clsGlobalVariables.btgTxBuffer[4] = clsGlobalVariables.LF;

                    
                    btmRetVal = _clsSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Calib_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        for (i = 0; i < clsGlobalVariables.btgRxBuffer.Length; i++)
                        {
                            if (clsGlobalVariables.btgRxBuffer[i] != clsGlobalVariables.CR)//LF
                            {
                                imArrayLength = imArrayLength + 1;
                            }
                            else
                                break;
                        }

                        Array.Clear(arrbtData, 0, arrbtData.Length);
                        Array.Resize(ref arrbtData, imArrayLength);

                        for (i = 0; i < imArrayLength; i++)
                        {
                            arrbtData[i] = clsGlobalVariables.btgRxBuffer[i];
                        }

                        for (i = 2; i < imArrayLength; i++)
                        {
                            strtemp = strtemp + (char)arrbtData[i];
                        }
                        if ( strValue.Contains("-"))
                        {
                            if (Convert.ToInt32(Convert.ToDouble(strtemp.Replace(" ", string.Empty))) == Convert.ToInt32(strValue))
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                            else
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Convert.ToDouble(strtemp.Replace(" ", string.Empty))) == Convert.ToInt32(" " + strValue))
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                            else
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
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
        #endregion
    }
}
