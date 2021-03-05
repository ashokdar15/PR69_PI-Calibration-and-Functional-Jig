using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Collections;
using PR69_PI_Calibration_and_Functional_Jig.ViewModel;
using PR69_PI_Calibration_and_Functional_Jig.Views;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    /********************************************************************************************
              Class Name        : clsTestJIGFunctions Class
              Purpose           : This class contains all the tests which gets carried out during calibration and functional tests on DUT.
              Date              : 1/06/2017
              Written By        : Shubham
              CopyRight         : General Industrial Controls Pvt. Ltd. Pune
              Modified          : Date                
              Released Version  :  V15
              Changed By        :  NA
              Decription Of Change:  NA
    ********************************************************************************************/
    public class clsTestJIGFunctions
    {
        ResourceManager objResManager = new ResourceManager("PR69_Function_and_Calibration_JIG.Resource.Res", typeof(clsTestJIGFunctions).Assembly);
        ///<MemberName>TestsForJIG</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function contains Swich case. Every case in this function represents Test of JIG. 
        ///All JIG related test cases are present here.
        ///</summary>
        ///<param name="strmJigTest">This is the name of test to be performed on JIG.</param>
        ///<ClassName>clsTestJIGFunctions</ClassName>
        public byte TestsForJIG(string strmJigTest)
        {
            byte btmRetVal;

            try
            {
                switch (strmJigTest)
                {
                    case "SLAVE1_ALM1_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, ALM1_TYPE, SET_ALM_TYPE_VAL);
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.ALM1_TYPE, clsGlobalVariables.SET_ALM_TYPE_VAL);

                        break;

                    case "SLAVE1_ALM1_THRESHLD_SET":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, ALM1_THRESHOLD, SET_ALM_THRESH_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.ALM2_THRESHOLD, clsGlobalVariables.SET_ALM_THRESH_VAL);

                        break;

                    case "SLAVE1_ALM2_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, ALM2_TYPE, SET_ALM_TYPE_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.ALM2_TYPE, clsGlobalVariables.SET_ALM_TYPE_VAL);

                        break;

                    case "SLAVE1_ALM2_THRESHLD_SET":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, ALM2_THRESHOLD, SET_ALM_THRESH_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.ALM2_THRESHOLD, clsGlobalVariables.SET_ALM_THRESH_VAL);

                        break;

                    case "SLAVE1_SP1_VALUE":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, SP1_VALUE, SET_SP_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.SP1_VALUE, clsGlobalVariables.SET_SP_VAL);

                        break;

                    case "SLAVE1_SENS_VALUE":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, SENS_SET, SET_SENS_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.SENS_SET, clsGlobalVariables.SET_SENS_VAL);

                        break;

                    case "SLAVE1_OUTPUT_CONFIG_SET":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, OUTPUT_CONF, OP_CONF_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OUTPUT_CONF, clsGlobalVariables.OP_CONF_VAL);

                        break;

                    case "SLAVE1_CONT_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, CONT_TYPE, SET_CONT_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.CONT_TYPE, clsGlobalVariables.SET_CONT_VAL);

                        break;

                    case "SLAVE1_FUNC_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, FUNC_TYPE, SET_FUNC_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.FUNC_TYPE, clsGlobalVariables.SET_FUNC_VAL);

                        break;

                    case "SLAVE2_ALM1_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, ALM1_TYPE, SET_ALM_TYPE_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.ALM1_TYPE, clsGlobalVariables.SET_ALM_TYPE_VAL);
                        break;

                    case "SLAVE2_ALM1_THRESHLD_SET":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, ALM1_THRESHOLD, SET_ALM_THRESH_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.ALM2_THRESHOLD, clsGlobalVariables.SET_ALM_THRESH_VAL);
                        break;

                    case "SLAVE2_ALM2_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, ALM2_TYPE, SET_ALM_TYPE_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.ALM2_TYPE, clsGlobalVariables.SET_ALM_TYPE_VAL);

                        break;

                    case "SLAVE2_ALM2_THRESHLD_SET":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, ALM2_THRESHOLD, SET_ALM_THRESH_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.ALM2_THRESHOLD, clsGlobalVariables.SET_ALM_THRESH_VAL);

                        break;

                    case "SLAVE2_SP1_VALUE":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, SP1_VALUE, SET_SP_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.SP1_VALUE, clsGlobalVariables.SET_SP_VAL);

                        break;

                    case "SLAVE2_SENS_VALUE":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, SENS_SET, SET_SENS_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.SENS_SET, clsGlobalVariables.SET_SENS_VAL);

                        break;

                    case "SLAVE2_OUTPUT_CONFIG_SET":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, OUTPUT_CONF, OP_CONF_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OUTPUT_CONF, clsGlobalVariables.OP_CONF_VAL);

                        break;

                    case "SLAVE2_CONT_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, CONT_TYPE, SET_CONT_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.CONT_TYPE, clsGlobalVariables.SET_CONT_VAL);

                        break;

                    case "SLAVE2_FUNC_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, FUNC_TYPE, SET_FUNC_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.FUNC_TYPE, clsGlobalVariables.SET_FUNC_VAL);

                        break;


                    case "SLAVE3_ALM1_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, ALM1_TYPE, SET_ALM_TYPE_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.ALM1_TYPE, clsGlobalVariables.SET_ALM_TYPE_VAL);
                        break;

                    case "SLAVE3_ALM1_THRESHLD_SET":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, ALM1_THRESHOLD, SET_ALM_THRESH_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.ALM2_THRESHOLD, clsGlobalVariables.SET_ALM_THRESH_VAL);
                        break;

                    case "SLAVE3_ALM2_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, ALM2_TYPE, SET_ALM_TYPE_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.ALM2_TYPE, clsGlobalVariables.SET_ALM_TYPE_VAL);

                        break;

                    case "SLAVE3_ALM2_THRESHLD_SET":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, ALM2_THRESHOLD, SET_ALM_THRESH_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.ALM2_THRESHOLD, clsGlobalVariables.SET_ALM_THRESH_VAL);
                        break;

                    case "SLAVE3_SP1_VALUE":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, SP1_VALUE, SET_SP_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.SP1_VALUE, clsGlobalVariables.SET_SP_VAL);

                        break;

                    case "SLAVE3_SENS_VALUE":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, SENS_SET, SET_SENS_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.SENS_SET, clsGlobalVariables.SET_SENS_VAL);

                        break;

                    case "SLAVE3_OUTPUT_CONFIG_SET":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, OUTPUT_CONF, OP_CONF_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.OUTPUT_CONF, clsGlobalVariables.OP_CONF_VAL);

                        break;

                    case "SLAVE3_CONT_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, CONT_TYPE, SET_CONT_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.CONT_TYPE, clsGlobalVariables.SET_CONT_VAL);

                        break;

                    case "SLAVE3_FUNC_TYPE_SET":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, FUNC_TYPE, SET_FUNC_VAL)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.FUNC_TYPE, clsGlobalVariables.SET_FUNC_VAL);

                        break;

                    default:
                        btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        break;
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>TestDUT</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This function contains Swich case of tests. Every case in this function represents Test for DUT. 
        ///All DUT related test are present here.
        ///</summary>
        ///<param name="strmTest">This is the name of test to be performed on DUT.</param>
        ///<ClassName>clsTestJIGFunctions</ClassName>
        public byte TestDUT(string strmTest)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                switch (strmTest)
                {
                    case "READ_DEVICE_ID":
                        //This check is for device having modbus.   
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.ReadDeviceID();
                        }
                        else //Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.ReadDeviceIDSalveToDut();
                        }
                        break;

                    case "READ_CALIB_CONST_STATUS":
                        //This check is for device having modbus.                        
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBReadDutCalibConst();
                        }
                        else  //Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.ReadCalibConstSalveToDut();
                        }
                        //This "clsGlobalVariables.BEFORE_SOAKING" tells that device is not calibrated.
                        if (clsModelSettings.btmCalibConst == clsGlobalVariables.BEFORE_SOAKING)
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.UNCALIBRATED_DUT);
                        //This "clsGlobalVariables.AFTER_SOAKING" tells that device is calibrated but accuracy testing is pending.
                        else if (clsModelSettings.btmCalibConst == clsGlobalVariables.AFTER_SOAKING)
                        {
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CALIBRATED_BUT_ACCURACY_ISNOTDONE);
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Accuracy_Test_Not_Done;
                        }
                        //This "clsGlobalVariables.Done" tells that device is calibrated & accuracy testing is also done.
                        else if (clsModelSettings.btmCalibConst == clsGlobalVariables.CALIB_DONE)
                        {
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CALIBRATED_DUT);
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                        break;

                    case "SWITCH_SENSOR_RELAY": //In this section calibrator settings are get done for further tests.

                        btmRetVal = clsGlobalVariables.objQueriescls.SwitchSensorRly();
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {

                            //-------Changed By Shubham
                            //Date:- 28-04-2018
                            //Version:- V17
                            //Statement:- Proper name is stored in the global variable to display on the picture message box. 
                            clsGlobalVariables.strgOngoingTestName = "mV Sensor Calibration";
                            //-----------Changes end.
                            //for 96x96 Cat Id different connection image is displayed.
                            
                            if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                            {
                                clsMessages.DisplayMessage(clsMessageIDs.TWO_WIRE_MSG_96x96);
                            }
                            else
                            {
                                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                                    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID);
                                else
                                    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID_PI);
                            }
                            //Check the source knob is present at mV.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_TEXT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Calibrator sensor is changed to mV.
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.mV_SENSOR);
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        //one mv value is set in the Calibrator's source.
                                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.strgONE_MV);
                                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            btmRetVal = clsGlobalVariables.objGlobalFunction.AdjustModeOfDevice(clsGlobalVariables.START_MODE);
                                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                            {
                                                //Only for analog devices calibrator's measure is made on.
                                                if (clsModelSettings.blnAnalogDUT == true)
                                                {
                                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOn();
                                                }
                                                else //Otherwise measure of Calibrator is made OFF.
                                                {
                                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOFF();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "SLAVE1_OP1_OFF":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, OP1_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "SLAVE1_OP2_OFF":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, OP2_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "SLAVE1_OP3_OFF":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, OP3_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "SLAVE1_OP1_ON":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, OP1_ADDRESS, OP1_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                        break;

                    case "SLAVE1_OP2_ON":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, OP2_ADDRESS, OP2_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                        break;

                    case "SLAVE1_OP3_ON":
                        //MBWriteHoldingReg(MB_SLAVE1_ID, OP3_ADDRESS, OP3_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                        break;

                    case "SLAVE1_READ_ADC_CNT_RLY_OFF":
                        //Relay test bypass logic is present here.

                        btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.RLY_OFF);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }
                        break;

                    case "SLAVE1_READ_ADC_CNT_RLY_ON":
                        //relay bypass logic is handled here. 
                        btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.RLY_ON);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }
                        break;

                    case "SLAVE2_OP1_OFF":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, OP1_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "SLAVE2_OP2_OFF":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, OP2_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "SLAVE2_OP3_OFF":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, OP3_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "SLAVE2_OP1_ON":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, OP1_ADDRESS, OP1_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                        break;

                    case "SLAVE2_OP2_ON":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, OP2_ADDRESS, OP2_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                        break;

                    case "SLAVE2_OP3_ON":
                        //MBWriteHoldingReg(MB_SLAVE2_ID, OP3_ADDRESS, OP3_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                        break;

                    case "SLAVE2_READ_ADC_CNT_RLY_OFF":
                        //Relay test bypass logic is present here.

                        btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.RLY_OFF);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }
                        break;

                    case "SLAVE2_READ_ADC_CNT_RLY_ON":
                        //relay bypass logic is handled here. 
                        btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.RLY_ON);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }
                        break;

                    case "SLAVE3_OP3_OFF":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, OP3_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "SLAVE3_OP1_ON":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, OP1_ADDRESS, OP1_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                        break;

                    case "SLAVE3_OP1_OFF":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, OP1_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "SLAVE3_OP2_ON":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, OP2_ADDRESS, OP2_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                        break;

                    case "SLAVE3_OP2_OFF":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, OP2_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "SLAVE3_OP3_ON":
                        //MBWriteHoldingReg(MB_SLAVE3_ID, OP3_ADDRESS, OP3_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                        break;

                    case "SLAVE3_READ_ADC_CNT_RLY_ON":
                        //MBReadAdcCounts(MB_SLAVE3_ID, RLY_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.RLY_ON);
                        break;

                    case "CONVERTOR_OP1_ON":
                        //ucmReturnVal = MBWriteHoldingReg(MB_CONVERTOR_ID, OP1_ADDRESS, OP1_ON)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                        break;

                    case "CONVERTOR_OP2_ON":
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                        break;

                    case "CONVERTOR_OP3_ON":
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                        break;

                    case "CONVERTOR_OP1_OFF":
                        //MBWriteHoldingReg(MB_CONVERTOR_ID, OP1_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "CONVERTOR_OP2_OFF":
                        //MBWriteHoldingReg(MB_CONVERTOR_ID, OP2_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "CONVERTOR_OP3_OFF":
                        //MBWriteHoldingReg(MB_CONVERTOR_ID, OP3_ADDRESS, OP_OFF)
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                        break;

                    case "DUT_READ_ADC_CNT_RLY_ON":
                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE3_ID, clsGlobalVariables.RLY_ON);
                        }
                        else //Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCountSlaveToDut(clsGlobalVariables.RLY_ON);
                        }
                        break;

                    case "DUT_OP1_ON":
                        //relay bypass logic is handled here. 

                        clsGlobalVariables.blngIsOPOneON = true;
                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.INPROGRESS);
                        //This check is for device having modbus.                            
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                        }
                        else //Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP1);
                        }

                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput( Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }
                        break;

                    case "DUT_OP1_OFF":
                        //relay bypass logic is handled here. 

                        //This check is for device having modbus.                            
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                        }
                        else //Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                        }
                        //This condition tells that OP1 test is completed. If OP1 is previously in ON state and response
                        //of query is also success then mark this test as completed(PASS).
                        if ((btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success) && clsGlobalVariables.blngIsOPOneON == true)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.PASS);
                            clsGlobalVariables.blngIsOPOneON = false;
                        }
                        else if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }

                        break;

                    case "DUT_OP2_ON":
                        //Relay test bypass logic is written here.

                        //CA55 Program.objMainForm.ShowStatusOutput( Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.INPROGRESS);
                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                        }
                        else //Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP2);
                        }

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            clsGlobalVariables.blngIsOPTwoON = true;
                        }
                        else
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput( Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }

                        //After completion of this test default delay is applied.   
                        //CA55 if (  Program.objMainForm.chkApplyDelay.Checked == true)
                        //{
                            //CA55 //CA55  Program.objMainForm.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtDelay.Text));
                        //}
                           // else
                           // {
                            //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST);
                           // }
                       
                        break;

                    case "DUT_OP2_OFF":
                        //Relay test bypass logic is added here.

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                        }
                        //This condition tells that OP2 test is completed. If OP2 is previously in ON state and response
                        //of query is also success then mark this test as completed(PASS).
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success && clsGlobalVariables.blngIsOPTwoON == true)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput( Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.PASS);
                            clsGlobalVariables.blngIsOPTwoON = false;
                        }
                        else if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput( Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                            clsGlobalVariables.blngIsOPTwoON = false;
                        }

                        //Default delay is applied here.
                        //if (  Program.objMainForm.chkApplyDelay.Checked == true)
                        //{
                        ////CA55 //CA55  Program.objMainForm.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtDelay.Text));
                        //}
                        //else
                        //{
                        ////CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST);
                        //}

                        break;

                    case "DUT_OP3_ON":
                        //Relay test bypass logic is handled here.

                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.INPROGRESS);
                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP3);
                        }

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            clsGlobalVariables.blngIsOPThreeON = true;
                        }
                        else
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }
                        //Default delay is applied here.
                        //if (  Program.objMainForm.chkApplyDelay.Checked == true)
                        //{
                        ////CA55 //CA55  Program.objMainForm.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtDelay.Text));
                        //}
                        //else
                        //{
                        ////CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST);
                        //}
                        break;

                    case "DUT_OP3_OFF":
                        //Relay bypass logic is added here.

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP3);
                        }
                        //This condition tells that OP3 test is completed. If OP3 is previously in ON state and response
                        //of query is also success then mark this test as completed(PASS).
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success && clsGlobalVariables.blngIsOPThreeON == true)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.PASS);
                            clsGlobalVariables.blngIsOPThreeON = false;
                        }
                        else if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                            clsGlobalVariables.blngIsOPThreeON = false;
                        }
                        //Default delay is applied here.
                        //if (  Program.objMainForm.chkApplyDelay.Checked == true)
                        //{
                        ////CA55 //CA55  Program.objMainForm.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtDelay.Text));
                        //}
                        //else
                        //{
                        ////CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST);
                        //}

                        break;

                    case "START_REL_TEST":
                        //Relay test bypass logic is present here.

                        //Here in this test device itself checks the relay op1 and op2.
                        //So, both relays shape color are handled here. 
                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.INPROGRESS);
                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.INPROGRESS);

                        btmRetVal = clsGlobalVariables.objQueriescls.MBStartRelayTest();

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.PASS);
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.PASS);
                        }
                        else
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }

                        break;

                    case "START_DISP_TEST":
                        DialogResult dlgMsgBxRslt;
                        bool blnmStatus;
                        blnmStatus = false;
                        //Display bypass logic is added here. 

                        clsMessages.DisplayMessage(clsMessageIDs.OBSERVE_DISP_TEST);

                        Repeat:
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpDisplay, clsGlobalVariables.enmStatus.INPROGRESS);
                        /*Here two tests gets carried out inside display test.
                         1.Display Test 
                         2.Leaky MOSFET Test:- After completion of display test leaky mosfet test gets conducted.
                         */
                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBStartTest(clsGlobalVariables.CHK_DISP);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBStartTest(clsGlobalVariables.CHK_LEAKY_MOSFET);
                            }
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_DISP);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_LEAKY_MOSFET);
                            }
                        }

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpDisplay, clsGlobalVariables.enmStatus.PASS);
                        }
                        else
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpDisplay, clsGlobalVariables.enmStatus.FAIL);
                        }

                        if (blnmStatus == false)
                        {
                            //Requirement in the display test was to ask user to perform disply test once again.
                            //Also software must ask this only once.
                            blnmStatus = true;
                            //dlgMsgBxRslt = MessageBox.Show("Do you want to test display again?", clsGlobalVariables.strg_Application, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);  
                            dlgMsgBxRslt = MessageBox.Show(objResManager.GetString("REPEAT_DISP_TEST", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                            if (dlgMsgBxRslt == DialogResult.OK)
                            {
                                goto Repeat;
                            }
                        }

                        break;


                    case "START_KEYPAD_TEST":
                        //Keypad bypass logic is added here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpKeypad, clsGlobalVariables.enmStatus.INPROGRESS);
                        btmRetVal = clsGlobalVariables.objGlobalFunction.TestKeyPad();

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpKeypad, clsGlobalVariables.enmStatus.PASS);
                        }
                        else
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpKeypad, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 Program.objMainForm.EnableGroupBox(clsGlobalVariables.DEVICE_CONFIG);
                        }

                        break;

                    case "WRITE_CALIB_CONST":

                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                //-------Changed By Shubham
                                //Date:- 24-02-2018
                                //Version:- V16
                                //Statement:- Parameter is passed to the function. 
                                //Meaning of false is not to send the VREF value to the DUT.
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteCalibConst(false);
                                //-------Changes End.
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.CALIB_STAGE);
                            }
                        }
                        else
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.CALIB_STAGE);
                        }
                        break;

                    //-------Changed By Shubham
                    //Date:- 24-02-2018
                    //Version:- V16
                    //Statement:- New Test is added in the software.
                    //Use of this test is to support the new Firmware V03.                    
                    case "WRITE_CALIB_CONST_WITH_VREF":
                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            //Meaning of false is to send the VREF value to the DUT.
                            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteCalibConst(true);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.CALIB_STAGE);
                        }
                        break;
                    //---------Changes End.

                    case "CALIBRATE_CURRENT":
                        //analog op test bypass logic is present here.

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBStartCalibration(clsGlobalVariables.CURRENT);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.CALIBRATE_FUNC_CODE, clsGlobalVariables.CURRENT);
                        }
                        break;

                    case "CALIBRATE_VOLTAGE":
                        //analog op test bypass logic is present here.

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBStartCalibration(clsGlobalVariables.VOLTAGE);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.CALIBRATE_FUNC_CODE, clsGlobalVariables.VOLTAGE);
                        }
                        break;

                    case "SET_DFALT_4MA_CNT":

                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.INPROGRESS);


                        //-------Changed By Shubham
                        //Date:- 28-04-2018
                        //Version:- V17
                        //Statement:- Proper name is stored in the global variable to display on the picture message box.
                        clsGlobalVariables.strgOngoingTestName = "Analog Output mA Calibration";
                        //-----------Changes end.
                        clsMessages.DisplayMessage(clsMessageIDs.CURRENT_SETTING_MSG_ID);
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            break;

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_CURRENT_DFALT_COUNT, clsGlobalVariables.MA_4);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_4_MA_CNT);
                        }

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //This statement is added here for manual calibration only.
                            //CA55 Program.objMainForm.lblSetVal.Text = "04.00";
                        }

                        break;
                    case "SET_DFALT_1MA_CNT":
                        //analog op test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.INPROGRESS);


                        clsGlobalVariables.strgOngoingTestName = "Analog Output mA Calibration";
                        clsMessages.DisplayMessage(clsMessageIDs.CURRENT_SETTING_MSG_ID);
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            break;

                        //This check is for device having modbus.
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_1_MA_CNT);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //This statement is added here for manual calibration only.
                            //CA55 Program.objMainForm.lblSetVal.Text = "01.00";
                        }
                        break;

                    case "SET_DFALT_20MA_CNT":
                        //analog op test bypass logic is present here.

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_CURRENT_DFALT_COUNT, clsGlobalVariables.MA_20);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_20_MA_CNT);
                        }

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //This statement is added here for manual calibration only.
                            //CA55 Program.objMainForm.lblSetVal.Text = "20.00";
                        }
                        break;

                    case "SET_DFALT_1V_CNT":
                        //analog op test bypass logic is present here.

                        //-------Changed By Shubham
                        //Date:- 28-04-2018
                        //Version:- V17
                        //Statement:- Proper name is stored in the global variable to display on the picture message box.
                        clsGlobalVariables.strgOngoingTestName = "Analog Output Volt Calibration";
                        //-----------Changes End.
                        clsMessages.DisplayMessage(clsMessageIDs.VOLTAGE_SETTING_MSG_ID);
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            break;

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_VOLTAGE_DFALT_COUNT, clsGlobalVariables.VOLT_1);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_1_VOLT_CNT);
                        }

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //This statement is added here for manual calibration only.
                            //CA55 Program.objMainForm.lblSetVal.Text = "01.00";
                        }

                        break;

                    case "SET_DFALT_10V_CNT":
                        //analog op test bypass logic is present here.

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_VOLTAGE_DFALT_COUNT, clsGlobalVariables.VOLT_10);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_10_VOLT_CNT);
                        }

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //This statement is added here for manual calibration only.
                            //CA55 Program.objMainForm.lblSetVal.Text = "10.00";
                        }

                        break;
                    case "SET_OBSRVED_4MA_CNT":


                        //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        //Here calibrator's measure knob position is checked. 
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue();
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable. 
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure. 
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.FOUR_mAMP);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 4mA Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog.StrmAnalogOP_mA_4 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_CURRENT_OBSERVED_COUNT, clsGlobalVariables.MA_4);
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_OBSERVED_4_MA, clsModelSettings.imAnalOpVal);
                                    }
                                }
                            }
                        }



                        //Messages related to analog test are displayed here.
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "4mA", "Successful.");
                        }
                        else
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "4mA", "Failed.");
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                        }
                        clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VALUE, "4mA", clsGlobalVariables.strgAnalogData);

                        break;
                    case "SET_OBSRVED_1MA_CNT":
                        //analog op test bypass logic is present here.

                        //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        //Here calibrator's measure knob position is checked. 
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue();
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable. 
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //ssageBox.Show(clsModelSettings.imAnalOpVal.ToString());
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure. 
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.ONE_mAMP_case);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 1mA Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog.StrmAnalogOP_mA_1 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_CURRENT_OBSERVED_COUNT, clsGlobalVariables.MA_1);
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_OBSERVED_1_MA, clsModelSettings.imAnalOpVal);
                                    }
                                }
                            }
                        }


                        //Messages related to analog test are displayed here.
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "1mA", "Successful.");
                        }
                        else
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "1mA", "Failed.");
                            //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            //else
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                        }
                        clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VALUE, "1mA", clsGlobalVariables.strgAnalogData);

                        break;
                    case "SET_OBSRVED_20MA_CNT":
                        //analog op test bypass logic is present here.

                        //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue();
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.TWENTY_mAMP);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 20mA Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog.StrmAnalogOP_mA_20 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_CURRENT_OBSERVED_COUNT, clsGlobalVariables.MA_20);
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_OBSERVED_20_MA, clsModelSettings.imAnalOpVal);
                                    }
                                }
                            }
                        }

                        //Messages related to analog test are displayed here.
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "20mA", "Successful.");
                        }
                        else
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "20mA", "Failed.");
                            //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            //else
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                        }
                        clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VALUE, "20mA", clsGlobalVariables.strgAnalogData);

                        break;

                    case "SET_OBSRVED_1V_CNT":
                        //analog op test bypass logic is present here.

                        //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue();
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.One_Volt);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 1V Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog.StrmAnalogOP_Volt_1 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_VOLTAGE_OBSERVED_COUNT, clsGlobalVariables.VOLT_1);
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_OBSERVED_1_VOLT, clsModelSettings.imAnalOpVal);
                                    }
                                }
                            }
                        }

                        //Messages related to analog test are displayed here.
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "1 Volt", "Successful.");
                        }
                        else
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "1 Volt", "Failed.");

                            //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            //else
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                        }
                        clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VALUE, "1 Volt", clsGlobalVariables.strgAnalogData);

                        break;
                    case "SET_OBSRVED_10V_CNT":
                        //analog op test bypass logic is present here.

                        //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue();
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.TEN_Volt);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 10V Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog.StrmAnalogOP_Volt_10 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_VOLTAGE_OBSERVED_COUNT, clsGlobalVariables.VOLT_10);
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_OBSERVED_10_VOLT, clsModelSettings.imAnalOpVal);
                                    }
                                }
                            }
                        }

                        //Messages related to analog test are displayed here.
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "10Volts", "Successful.");
                        }
                        else
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "10Volts", "Failed.");
                            //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            //else
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                        }
                        clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VALUE, "10Volts", clsGlobalVariables.strgAnalogData);


                        break;

                    case "SET_12MA_ANLOP":
                        //analog op test bypass logic is present here.

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_CURRENT_ANLOP, clsGlobalVariables.MA_12);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_I_FUNC_CODE, clsGlobalVariables.MA_12);
                        }
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //This statement is added here for manual calibration only.
                            //CA55 Program.objMainForm.lblSetVal.Text = "12.00";
                            clsModelSettings.btmAnalogsetVal = clsGlobalVariables.TWELVE_mA;
                        }
                        else
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "12mA", "Failed.");
                            //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            //else
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                            //break;
                        }


                        //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue();
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.TWELVE_mA);
                                //Messages related to analog test are displayed here.
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 12mA Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog.StrmAnalogOP_mA_12 = clsGlobalVariables.strgAnalogData;
                                    clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "12mA", "Successful.");
                                }
                                else
                                {
                                    clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "12mA", "Failed.");
                                    //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                                    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                                    //else
                                    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                                }
                                clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VALUE, "12mA", clsGlobalVariables.strgAnalogData);
                            }
                        }

                        break;

                    case "SET_5V_ANLOP":
                        //analog op test bypass logic is present here.

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_VOLTAGE_ANLOP, clsGlobalVariables.VOLT_5);
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_V_FUNC_CODE, clsGlobalVariables.VOLT_5);
                        }
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //This statement is added here for manual calibration only.
                            //CA55 Program.objMainForm.lblSetVal.Text = "05.00";
                            clsModelSettings.btmAnalogsetVal = clsGlobalVariables.FIVE_Volt;
                        }
                        else
                        {
                            clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "5Volts", "Failed.");
                            //if (Program.objMainForm.rad48by48DUT.Checked|| Program.objMainForm.rad96by96DUT.Checked)
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            //else
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);

                            break;
                        }


                        //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT);

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue();
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.FIVE_Volt);
                                //Messages related to analog test are displayed here.
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 5V Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog.StrmAnalogOP_Volt_5 = clsGlobalVariables.strgAnalogData;
                                    clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "5Volts", "Successful.");
                                    //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                                    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.PASS);
                                    //else
                                    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.PASS);

                                }
                                else
                                {
                                    clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "5Volts", "Failed.");
                                    //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                                    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                                    //else
                                    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                                }
                                clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VALUE, "5Volts", clsGlobalVariables.strgAnalogData);
                            }
                        }

                        break;

                    //case "SHOW_ENTR_CURR_SCREEN":
                    ////CA55 Program.objMainForm.grpEnterAnalogData.Visible = true;
                    ////CA55 Program.objMainForm.grpEnterAnalogData.Location = new Point(Program.objMainForm.grpDeviceConfig.Location.X, Program.objMainForm.grpDeviceConfig.Location.Y);
                    ////CA55 Program.objMainForm.grpEnterAnalogData.Size = Program.objMainForm.grpDeviceConfig.Size;
                    ////CA55 Program.objMainForm.txtObservedValue.Text = "";
                    ////CA55 Program.objMainForm.txtObservedValue.Focus();
                    ////CA55 Program.objMainForm.lblObsmAOrVolt.Text = "mA";
                    ////CA55 Program.objMainForm.lblSetmAOrVolt.Text = "mA";
                    //    //After entering the value in the text box given for analog tests are the below flags sets to true.
                    //    //And after that software will come out of this while loop.
                    //    while (clsGlobalVariables.blngUserEnteredValue == false)
                    //    {
                    //        Application.DoEvents();
                    //    }
                    //    clsGlobalVariables.blngUserEnteredValue = false;
                    //    btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                    //    break;

                    //case "SHOW_ENTR_VTG_SCREEN":
                    ////CA55 Program.objMainForm.grpEnterAnalogData.Visible = true;
                    ////CA55 Program.objMainForm.grpEnterAnalogData.Location = new Point(Program.objMainForm.grpDeviceConfig.Location.X, Program.objMainForm.grpDeviceConfig.Location.Y);
                    ////CA55 Program.objMainForm.grpEnterAnalogData.Size = Program.objMainForm.grpDeviceConfig.Size;
                    ////CA55 Program.objMainForm.txtObservedValue.Text = "";
                    ////CA55 Program.objMainForm.txtObservedValue.Focus();
                    ////CA55 Program.objMainForm.lblObsmAOrVolt.Text = "Volts";
                    ////CA55 Program.objMainForm.lblSetmAOrVolt.Text = "Volts";
                    //    //After entering the value in the text box given for analog tests are the below flags sets to true.
                    //    //And after that software will come out of this while loop.
                    //    while (clsGlobalVariables.blngUserEnteredValue == false)
                    //    {
                    //        Application.DoEvents();
                    //    }
                    //    clsGlobalVariables.blngUserEnteredValue = false;
                    //    btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                    //    break;

                    case "CHK_ANALOG_OP_VAL":
                        //analog op test bypass logic is present here.

                        //In manual calibration mode this test is present. for 12 mA and 5Volts analog output validation.  
                        btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsModelSettings.btmAnalogsetVal);

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            if (clsModelSettings.btmAnalogsetVal == clsGlobalVariables.TWELVE_mA)
                            {
                                clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "12mA", "Successful.");
                            }
                            else if (clsModelSettings.btmAnalogsetVal == clsGlobalVariables.FIVE_Volt)
                            {
                                clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "5Volts", "Successful.");
                                //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                                ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.PASS);
                                //else
                                ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.PASS);
                            }
                        }
                        else
                        {
                            if (clsModelSettings.btmAnalogsetVal == clsGlobalVariables.TWELVE_mA)
                            {
                                clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "12mA", "Failed.");
                            }
                            else if (clsModelSettings.btmAnalogsetVal == clsGlobalVariables.FIVE_Volt)
                            {
                                clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "5Volts", "Failed.");
                            }
                            //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            //else
                            ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);

                        }
                        break;

                    case "CALIB_1_MV_CNT":
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.INPROGRESS);
                        //For 1mV and 50mV calibration, "J" sensor is selected in the DUT.
                        btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_J_TYPE);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus( Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEMV_CALIB_ERR);
                            break;
                        }


                        //Callibrator SSource is made on.
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEMV_CALIB_ERR);
                            break;
                        }
                        //1mV value is adjusted in the Calibrator's source.  
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_MV);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEMV_CALIB_ERR);
                            break;
                        }
                        //Here software reads the count and validates that count for double acting. 
                        //For single acting devices software does not validates received counts.
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.MV_1_CNT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEMV_CALIB_ERR);
                            break;
                        }

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEMV_CALIB_SUCCESS);
                        break;

                    case "CALIB_50_MV_CNT":
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp50mV, clsGlobalVariables.enmStatus.INPROGRESS);

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.FIFTY_MV);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp50mV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FIFTYMV_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.MV_50_CNT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp50mV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FIFTYMV_CALIB_ERR);
                            break;
                        }


                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp50mV, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FIFTYMV_CALIB_SUCCESS);
                        break;

                    case "CALC_SLOPE_OFFSET":
                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag)
                        {
                            clsGlobalVariables.objGlobalFunction.CalSlopeOffset();
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        else//Device without modbus
                        {
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        break;

                    case "CALIB_TC":
                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            clsGlobalVariables.objGlobalFunction.Calc_Cjc_Offset();
                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst();
                            //This check is added here because while calibrating the device having Analog IP sensor,
                            //if this query is sent to the device after completion of PT100 sensor calibration,
                            //slopes and offsets for analog IP sensors are get written as Zero.
                            //Due to this on the device display proper values does not get displayed.
                            //On the device display zero value gets displayed.
                            if (clsGlobalVariables.igTYPE_OF_DEVICE == clsGlobalVariables.igDoubleActingWithAnalogIPType)
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                                break;
                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objGlobalFunction.DeviceWrite();
                                break;
                            }
                        }
                        else//Device without modbus
                        {
                            btmRetVal = clsGlobalVariables.objGlobalFunction.MBSendPvSlaveToDut();

                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                long lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 1);

                                if (lmData == 2)
                                {
                                    return (byte)clsGlobalVariables.enmResponseError.Success;
                                }
                                else
                                {
                                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                }
                            }
                        }
                        break;

                    case "CALIB_PT100":


                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.INPROGRESS);

                        //-------Changed By Shubham
                        //Date:- 28-04-2018
                        //Version:- V17
                        //Statement:- Proper name is stored in the global variable to display on the picture message box.
                        clsGlobalVariables.strgOngoingTestName = "PT100 Sensor Calibration";
                        //------Changes End.
                        //for 96x96 Cat Id different connection image is displayed.
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                        {
                            clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_96x96);
                        }
                        else
                        {
                            if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                            {
                                clsMessages.DisplayMessage(clsMessageIDs.THREEWIRE_MSG_ID);
                            } else
                            {
                                clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_PI);
                            }

                        }

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_RTD_KNOB_POS, clsGlobalVariables.SOURCE_RTD_KNOB_TEXT);

                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.THREEFIFTY_OHM);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_PT100_TYPE);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.PT100_CNT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                            break;
                        }



                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            clsGlobalVariables.objGlobalFunction.Calc_Current();
                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst();
                            //This check is added here because while calibrating the device having Analog IP sensor,
                            //if this query is sent to the device after completion of PT100 sensor calibration,
                            //slopes and offsets for analog IP sensors are get written as Zero.
                            //Due to this on the device display proper values does not get displayed.
                            //On the device display zero value gets displayed.
                            if (clsGlobalVariables.igTYPE_OF_DEVICE != clsGlobalVariables.igDoubleActingWithAnalogIPType)
                            {
                                btmRetVal = clsGlobalVariables.objGlobalFunction.DeviceWrite();
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                                    break;
                                }
                            }
                        }

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.THREEFIFTYOHM_CALIB_SUCCESS);
                        break;

                    case "CALIB_1V_CNT":
                        //chkVtg test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.INPROGRESS);
                        btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //Error message of the 1V calibration is displyed here.
                            //Also color of shape of 1V present on the main form is changed to RED.
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEVOLT_CALIB_ERR);
                            break;
                        }

                        //-------Changed By Shubham
                        //Date:- 28-04-2018
                        //Version:- V17
                        //Statement:- Proper name is stored in the global variable to display on the picture message box.
                        clsGlobalVariables.strgOngoingTestName = "Analog Input Volt Calibration";
                        //--------Changes End.

                        if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF() != (byte)clsGlobalVariables.enmResponseError.Success)
                            break;
                        clsMessages.DisplayMessage(clsMessageIDs.VOLT_CALIBRATION_MSG_ID);
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_VOLT_KNOB_POS, clsGlobalVariables.SOURCE_VOLT_KNOB_TEXT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEVOLT_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEVOLT_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_VOLT_INPUT_CAL);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEVOLT_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_1V);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEVOLT_CALIB_ERR);
                            break;
                        }

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEVOLT_CALIB_SUCCESS);
                        break;

                    case "CALIB_9V_CNT":
                        //chkVtg test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.INPROGRESS);


                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.NINE_VOLT_INPUT_CAL);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.NINEVOLT_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_9V);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.NINEVOLT_CALIB_ERR);
                            break;
                        }

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            clsGlobalVariables.objGlobalFunction.CalSlopeOffset110V();
                        }

                        clsGlobalVariables.objGlobalFunction.ConvertCalibConst();

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.NINEVOLT_CALIB_SUCCESS);
                        break;

                    case "CALIB_4mA_CNT":
                        //chkCurrent test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.INPROGRESS);


                        //-------Changed By Shubham
                        //Date:- 28-04-2018
                        //Version:- V17
                        //Statement:- Proper name is stored in the global variable to display on the picture message box.
                        clsGlobalVariables.strgOngoingTestName = "Analog Input mA Calibration";
                        //---------Changes End.
                        if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF() != (byte)clsGlobalVariables.enmResponseError.Success)
                            break;
                        clsMessages.DisplayMessage(clsMessageIDs.MA_CALIBRATION_MSG_ID);
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_TEXT);

                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FOURMA_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FOURMA_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.FOUR_mA_INPUT_CAL);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FOURMA_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_4_20mA_TYPE);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FOURMA_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_4mA);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FOURMA_CALIB_ERR);
                            break;
                        }

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FOURMA_CALIB_SUCCESS);
                        break;

                    case "CALIB_20mA_CNT":
                        //chkCurrent test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.INPROGRESS);


                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.TWENTY_mA_INPUT_CAL);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.TWENTYMA_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_20mA);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.TWENTYMA_CALIB_ERR);
                            break;
                        }

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            clsGlobalVariables.objGlobalFunction.CalSlopeOffset420mA();
                        }
                        //-------Changed By Shubham
                        //Date:- 24-02-2018
                        //Version:- V16
                        //Statement:- Here calibration constants for 4-20mA sensor are converted to hexadecimal value. 
                        clsGlobalVariables.objGlobalFunction.ConvertCalibConst();
                        //------Changes End.
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.TWENTYMA_CALIB_SUCCESS);
                        break;
                    //-------Changed By Shubham
                    //Date:- 24-02-2018
                    //Version:- V16
                    //Statement:- New Test is added in the software. This test will be used for devices
                    //having analog input sensors. Use of this test is to read the VREF value from the DUT and
                    //save that value in log file.
                    case "REF_VOLTAGE_CALC":
                        //Progress status to VREF shape is set.
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.INPROGRESS);
                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            btmRetVal = clsGlobalVariables.objTestJIGFunctions.TestDUT("SOURCE_OFF");
                            if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                            {
                                //Remove device connections message is displayed here.
                                // clsMessages.DisplayMessage(clsMessageIDs.REMOVE_SOURCE_CONN);
                                //Here device sensor is changed to 1-10V.
                                btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.FAIL);
                                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.VREF_CALIB_ERR);
                                    break;
                                }
                                //Counts are read from the device for VREF.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_VREF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.FAIL);
                                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.VREF_CALIB_ERR);
                                    break;
                                }
                                clsGlobalVariables.objGlobalFunction.Calc_VREF();
                            }
                        }
                        else //For devices without modbus.
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.MBReferenceVoltageReadSingleActing();
                        }

                        if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                        {
                            //Validation on VREF value is applied here.
                            if (clsGlobalVariables.fltgREF_Vtg > clsGlobalVariables.fltgREF_Vtg_MAX && clsGlobalVariables.fltgREF_Vtg < clsGlobalVariables.fltgREF_Vtg_MIN)
                            {
                                //Error message is displayed in the progress window.
                                clsMessages.ShowMessageInProgressWindow(clsMessageIDs.VREF_TOLERANCE_ERR);
                                //Invalid value enum is returned here.
                                return Convert.ToByte(clsGlobalVariables.enmResponseError.Invalid_data);
                            }
                            //Here vref value is stored in the datalog object.
                            clsGlobalVariables.objDataLog.StrmRef_Vtg = clsGlobalVariables.fltgREF_Vtg.ToString();
                            //Here vref value is added in the calib const array.
                            clsGlobalVariables.strgarrCalibConst[clsGlobalVariables.VREF_VALUE] = clsGlobalVariables.objGlobalFunction.Float2Hex(clsGlobalVariables.fltgREF_Vtg);
                            //Pass status to VREF shape is set.
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.PASS);                            
                            //VREF value is displayed in the text box present in debug form.
                            //CA55 Program.objMainForm.txtVREF.Text = clsGlobalVariables.objDataLog.StrmRef_Vtg;
                        }
                        else
                        {
                            //Fail status to VREF shape is set.
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.VREF_CALIB_ERR);
                        }
                        break;
                    //---------Changes END.

                    //This test is not present in the INI file. After successful calibration, Calibrator's source is made OFF.
                    case "SOURCE_OFF":

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF();

                        break;
                    case "START_REL_TEST_PI":
                        //Relay test bypass logic is present here.

                        //Here in this test device itself checks the relay op1 and op2.
                        //So, both relays shape color are handled here. 
                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.INPROGRESS);
                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.INPROGRESS);
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                        if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                        {
                            //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                            if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                            {
                                //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                // btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevicesPI(clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_RELAY);
                                if (MainWindowVM.initilizeCommonObject.objplcSerialComm.OpenCommPort(clsGlobalVariables.strgComPortPLC, false))
                                {
                                    //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                    //05 from PLC
                                    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(5);
                                    if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                                    {
                                        //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                        //DUT_OP1_ON
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP1);
                                        if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                                        {
                                            //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                            //06 from PLC
                                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(6);
                                            if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                                            {
                                                //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                                //DUT_OP2_ON
                                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP2);
                                                if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                                                {
                                                    //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                                    //0A from PLC
                                                    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(10);
                                                    if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                                                    {
                                                        //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                                        //DUT_OP1_OFF
                                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                                                        if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                                                        {
                                                            //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                                            //09 from PLC
                                                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(9);
                                                            if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                                                            {
                                                                //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                                                //DUT_OP2_OFF
                                                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                                                                if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                                                                {
                                                                    //CA55 //CA55  Program.objMainForm.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                                                                    //05 from PLC
                                                                    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(5);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    btmRetVal = Convert.ToByte(clsGlobalVariables.enmResponseError.Invalid_data);
                                }
                            }
                        }
                        MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.PASS);
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.PASS);
                        }
                        else
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        }

                        break;
                    case "CALIB_1V_CNT_PI":
                        //chkVtg test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.INPROGRESS);

                        

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_VOLT_INPUT_CAL);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                                clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEVOLT_CALIB_ERR);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_1V);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                                clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEVOLT_CALIB_ERR);
                                break;
                            }
                        
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEVOLT_CALIB_SUCCESS);
                        break;
                    case "CALIB_9V_CNT_PI":

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.INPROGRESS);


                        if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF() != (byte)clsGlobalVariables.enmResponseError.Success)
                            break;
                        clsMessages.DisplayMessage(clsMessageIDs.VOLT_CALIBRATION_MSG_ID);

                        clsGlobalVariables.strgOngoingTestName = "Analog Input Volt Calibration";
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_VOLT_KNOB_POS, clsGlobalVariables.SOURCE_VOLT_KNOB_TEXT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.NINEVOLT_CALIB_ERR);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //Error message of the 1V calibration is displyed here.
                            //Also color of shape of 1V present on the main form is changed to RED.
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.NINEVOLT_CALIB_ERR);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.NINEVOLT_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.NINE_VOLT_INPUT_CAL);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.NINEVOLT_CALIB_ERR);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_9V);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.NINEVOLT_CALIB_ERR);
                            break;
                        }

                        //This check is for device having modbus.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            clsGlobalVariables.objGlobalFunction.CalSlopeOffset110V();
                        }

                        clsGlobalVariables.objGlobalFunction.ConvertCalibConst();

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.NINEVOLT_CALIB_SUCCESS);
                        break;
                    case "CALIB_1mA_CNT_PI":

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.INPROGRESS);


                        clsGlobalVariables.strgOngoingTestName = "Analog Input mA Calibration";
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_mA_INPUT_CAL);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEMA_CALIB_ERR);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_20mA_TYPE);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEMA_CALIB_ERR);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_4mA);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEMA_CALIB_ERR);
                            break;
                        }

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ONEMA_CALIB_SUCCESS);
                        break;
                    case "CALIB_20mA_CNT_PI":

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.INPROGRESS);


                        if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF() != (byte)clsGlobalVariables.enmResponseError.Success)
                            break;
                        clsMessages.DisplayMessage(clsMessageIDs.MA_CALIBRATION_MSG_ID);
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_TEXT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.TWENTYMA_CALIB_ERR);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.TWENTYMA_CALIB_ERR);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.TWENTY_mA_INPUT_CAL);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.TWENTYMA_CALIB_ERR);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_20mA_TYPE);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.TWENTYMA_CALIB_ERR);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_20mA);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.TWENTYMA_CALIB_ERR);
                            break;
                        }

                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            clsGlobalVariables.objGlobalFunction.CalSlopeOffset420mA();
                        }
                        clsGlobalVariables.objGlobalFunction.ConvertCalibConst();
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.PASS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.TWENTYMA_CALIB_SUCCESS);
                        break;
                    case "START_MODBUS_TEST":

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpModbusTest, clsGlobalVariables.enmStatus.INPROGRESS);
                        if (MainWindowVM.initilizeCommonObject.objplcSerialComm.OpenCommPort(clsGlobalVariables.strgComPortPLC, false))
                        {
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartModBusTest_PI();
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpModbusTest, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                        }
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpModbusTest, clsGlobalVariables.enmStatus.PASS);
                        MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();
                        break;
                    case "24V_OP_TEST":
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp24VoltOPTest, clsGlobalVariables.enmStatus.INPROGRESS);
                        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.Test_24Volt_OUTPUT_TEST_MSG);
                        if (MainWindowVM.initilizeCommonObject.objplcSerialComm.OpenCommPort(clsGlobalVariables.strgComPortPLC, false))
                        {
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBReadPLC_Output();
                        }
                        MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();

                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp24VoltOPTest, clsGlobalVariables.enmStatus.FAIL);                            
                            break;
                        }
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp24VoltOPTest, clsGlobalVariables.enmStatus.PASS);
                        break;
                    case "CJC_TEST":

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.INPROGRESS);
                        btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_J_TYPE);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                            break;
                        }

                        btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.J_SENSOR);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadPVSingleActingCJC();
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.Shp_CJC.TextONShape = clsGlobalVariables.shrtgCJC.ToString();
                            if (clsGlobalVariables.shrtgCJC < clsGlobalVariables.CJC_min_Value || clsGlobalVariables.shrtgCJC > clsGlobalVariables.CJC_max_Value)
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.PASS);
                        }
                        else
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.mV_SENSOR);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                            break;
                        }
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.strgONE_MV);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                        }
                        btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_60_MV_TYPE);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                        }
                        break;
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

