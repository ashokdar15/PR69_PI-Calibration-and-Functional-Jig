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
    
    public class clsTestJIGFunctions
    {
        ResourceManager objResManager = new ResourceManager("PR69_Function_and_Calibration_JIG.Resource.Res", typeof(clsTestJIGFunctions).Assembly);
              
        public byte TestDUT(string strmTest)
        {

            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                switch (strmTest)
                {
                    case "READ_DEVICE_ID":
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(100);
                        //This check is for device having modbus.   
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.ReadDeviceID(DUT);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.DisplayMessage(DUT, clsMessageIDs.WRONG_DEVICE_SELECTION);
                                    continue; 
                                }
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                            }
                        }
                        else //Device without modbus
                        {
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.ReadDeviceIDSalveToDut((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.DisplayMessage(DUT, clsMessageIDs.WRONG_DEVICE_SELECTION);
                                    continue;
                                }
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                            }
                        }
                        break;

                    case "READ_CALIB_CONST_STATUS":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.                        
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBReadDutCalibConst((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                            }
                            else  //Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.ReadCalibConstSalveToDut((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                            }
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            //This "clsGlobalVariables.BEFORE_SOAKING" tells that device is not calibrated.
                            if (clsModelSettings.btmCalibConst == clsGlobalVariables.BEFORE_SOAKING)
                                clsGlobalVariables.mainWindowVM.DisplayMessage(DUT, clsMessageIDs.UNCALIBRATED_DUT);
                            //This "clsGlobalVariables.AFTER_SOAKING" tells that device is calibrated but accuracy testing is pending.
                            else if (clsModelSettings.btmCalibConst == clsGlobalVariables.AFTER_SOAKING)
                            {
                                clsGlobalVariables.mainWindowVM.DisplayMessage(DUT, clsMessageIDs.CALIBRATED_BUT_ACCURACY_ISNOTDONE);
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Accuracy_Test_Not_Done;
                            }
                            //This "clsGlobalVariables.Done" tells that device is calibrated & accuracy testing is also done.
                            else if (clsModelSettings.btmCalibConst == clsGlobalVariables.CALIB_DONE)
                            {
                                clsGlobalVariables.mainWindowVM.DisplayMessage(DUT, clsMessageIDs.CALIBRATED_DUT);
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;

                    case "SWITCH_SENSOR_RELAY": //In this section calibrator settings are get done for further tests.
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
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {


                            btmRetVal = clsGlobalVariables.objQueriescls.SwitchSensorRly(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {

                                //-------Changed By Shubham
                                //Date:- 28-04-2018
                                //Version:- V17
                                //Statement:- Proper name is stored in the global variable to display on the picture message box. 
                                clsGlobalVariables.strgOngoingTestName = "mV Sensor Calibration";
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_POS, DUT);
                                //-----------Changes end.
                                //for 96x96 Cat Id different connection image is displayed.


                                //Check the source knob is present at mV.
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_TEXT, DUT);
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Calibrator sensor is changed to mV.
                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.mV_SENSOR, DUT);
                                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            //one mv value is set in the Calibrator's source.
                                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.strgONE_MV, DUT);
                                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                            {
                                                btmRetVal = clsGlobalVariables.objGlobalFunction.AdjustModeOfDevice(clsGlobalVariables.START_MODE, DUT);
                                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                                {
                                                    //Only for analog devices calibrator's measure is made on.
                                                    if (clsModelSettings.blnAnalogDUT == true)
                                                    {
                                                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOn(DUT);
                                                    }
                                                    else //Otherwise measure of Calibrator is made OFF.
                                                    {
                                                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOFF(DUT);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;
                    //case "SLAVE1_OP1_OFF":
                    //    //MBWriteHoldingReg(MB_SLAVE1_ID, OP1_ADDRESS, OP_OFF)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "SLAVE1_OP2_OFF":
                    //    //MBWriteHoldingReg(MB_SLAVE1_ID, OP2_ADDRESS, OP_OFF)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "SLAVE1_OP3_OFF":
                    //    //MBWriteHoldingReg(MB_SLAVE1_ID, OP3_ADDRESS, OP_OFF)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "SLAVE1_OP1_ON":
                    //    //MBWriteHoldingReg(MB_SLAVE1_ID, OP1_ADDRESS, OP1_ON)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                    //    break;

                    //case "SLAVE1_OP2_ON":
                    //    //MBWriteHoldingReg(MB_SLAVE1_ID, OP2_ADDRESS, OP2_ON)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                    //    break;

                    //case "SLAVE1_OP3_ON":
                    //    //MBWriteHoldingReg(MB_SLAVE1_ID, OP3_ADDRESS, OP3_ON)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                    //    break;

                    //case "SLAVE1_READ_ADC_CNT_RLY_OFF":
                    //    //Relay test bypass logic is present here.

                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.RLY_OFF);
                    //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //    {
                    //        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                    //        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                    //        //CA55clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                    //    }
                    //    break;

                    //case "SLAVE1_READ_ADC_CNT_RLY_ON":
                    //    //relay bypass logic is handled here. 
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE1_ID, clsGlobalVariables.RLY_ON);
                    //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //    {
                    //        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                    //        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                    //        //CA55//clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                    //    }
                    //    break;

                    //case "SLAVE2_OP1_OFF":
                    //    //MBWriteHoldingReg(MB_SLAVE2_ID, OP1_ADDRESS, OP_OFF)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "SLAVE2_OP2_OFF":
                    //    //MBWriteHoldingReg(MB_SLAVE2_ID, OP2_ADDRESS, OP_OFF)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "SLAVE2_OP3_OFF":
                    //    //MBWriteHoldingReg(MB_SLAVE2_ID, OP3_ADDRESS, OP_OFF)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "SLAVE2_OP1_ON":
                    //    //MBWriteHoldingReg(MB_SLAVE2_ID, OP1_ADDRESS, OP1_ON)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                    //    break;

                    //case "SLAVE2_OP2_ON":
                    //    //MBWriteHoldingReg(MB_SLAVE2_ID, OP2_ADDRESS, OP2_ON)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                    //    break;

                    //case "SLAVE2_OP3_ON":
                    //    //MBWriteHoldingReg(MB_SLAVE2_ID, OP3_ADDRESS, OP3_ON)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                    //    break;

                    //case "SLAVE2_READ_ADC_CNT_RLY_OFF":
                    //    //Relay test bypass logic is present here.

                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.RLY_OFF);
                    //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //    {
                    //        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                    //        //CA55clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                    //    }
                    //    break;

                    //case "SLAVE2_READ_ADC_CNT_RLY_ON":
                    //    //relay bypass logic is handled here. 
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts(clsGlobalVariables.MB_SLAVE2_ID, clsGlobalVariables.RLY_ON);
                    //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //    {
                    //        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                    //        //CA55clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                    //    }
                    //    break;

                    //case "SLAVE3_OP3_OFF":
                    //    //MBWriteHoldingReg(MB_SLAVE3_ID, OP3_ADDRESS, OP_OFF)
                    //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    //        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "SLAVE3_OP1_ON":
                    //    //MBWriteHoldingReg(MB_SLAVE3_ID, OP1_ADDRESS, OP1_ON)
                    //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    //        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                    //    break;

                    //case "SLAVE3_OP1_OFF":
                    //    //MBWriteHoldingReg(MB_SLAVE3_ID, OP1_ADDRESS, OP_OFF)
                    //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    //        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "SLAVE3_OP2_ON":
                    //    //MBWriteHoldingReg(MB_SLAVE3_ID, OP2_ADDRESS, OP2_ON)
                    //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    //        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                    //    break;

                    //case "SLAVE3_OP2_OFF":
                    //    //MBWriteHoldingReg(MB_SLAVE3_ID, OP2_ADDRESS, OP_OFF)
                    //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    //        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "SLAVE3_OP3_ON":
                    //    //MBWriteHoldingReg(MB_SLAVE3_ID, OP3_ADDRESS, OP3_ON)
                    //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    //        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                    //    break;

                    //case "SLAVE3_READ_ADC_CNT_RLY_ON":
                    //    //MBReadAdcCounts(MB_SLAVE3_ID, RLY_ON)
                    //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    //        btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.RLY_ON);
                    //    break;

                    //case "CONVERTOR_OP1_ON":
                    //    //ucmReturnVal = MBWriteHoldingReg(MB_CONVERTOR_ID, OP1_ADDRESS, OP1_ON)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                    //    break;

                    //case "CONVERTOR_OP2_ON":
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                    //    break;

                    //case "CONVERTOR_OP3_ON":
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                    //    break;

                    //case "CONVERTOR_OP1_OFF":
                    //    //MBWriteHoldingReg(MB_CONVERTOR_ID, OP1_ADDRESS, OP_OFF)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "CONVERTOR_OP2_OFF":
                    //    //MBWriteHoldingReg(MB_CONVERTOR_ID, OP2_ADDRESS, OP_OFF)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    //case "CONVERTOR_OP3_OFF":
                    //    //MBWriteHoldingReg(MB_CONVERTOR_ID, OP3_ADDRESS, OP_OFF)
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_CONVERTOR_ID, clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                    //    break;

                    case "DUT_READ_ADC_CNT_RLY_ON":

                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCounts((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.RLY_ON);
                            }
                            else //Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBReadAdcCountSlaveToDut(clsGlobalVariables.RLY_ON, DUT);
                            }
                        }
                        break;

                    case "DUT_OP1_ON":
                        //relay bypass logic is handled here. 
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.blngIsOPOneON = true;
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.INPROGRESS);
                            //This check is for device having modbus.                            
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                            }
                            else //Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP1);
                            }

                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatusOutput( Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                                //CA55clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                            }
                        }
                        break;

                    case "DUT_OP1_OFF":
                        //relay bypass logic is handled here. 
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.                            
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                            }
                            else //Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
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
                                //CA55clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                            }
                        }
                        break;

                    case "DUT_OP2_ON":
                        //Relay test bypass logic is written here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput( Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.INPROGRESS);
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                            }
                            else //Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP2);
                            }

                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.blngIsOPTwoON = true;
                            }
                            else
                            {
                                //CA55 Program.objMainForm.ShowStatusOutput( Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                            }

                            //After completion of this test default delay is applied.   
                            //CA55 if (  Program.objMainForm.chkApplyDelay.Checked == true)
                            //{
                            //CA55clsGlobalVariables.objGlobalFunction.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtDelay.Text));
                            //}
                            // else
                            // {
                            //CA55clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST);
                            // }
                        }
                        break;

                    case "DUT_OP2_OFF":
                        //Relay test bypass logic is added here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
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
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                                clsGlobalVariables.blngIsOPTwoON = false;
                            }

                            //Default delay is applied here.
                            //if (  Program.objMainForm.chkApplyDelay.Checked == true)
                            //{
                            //clsGlobalVariables.objGlobalFunction.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtDelay.Text));
                            //}
                            //else
                            //{
                            //clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST);
                            //}
                        }
                        break;

                    case "DUT_OP3_ON":
                        //Relay test bypass logic is handled here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.INPROGRESS);
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP3);
                            }

                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.blngIsOPThreeON = true;
                            }
                            else
                            {
                                //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                            }
                            //Default delay is applied here.
                            //if (  Program.objMainForm.chkApplyDelay.Checked == true)
                            //{
                            //clsGlobalVariables.objGlobalFunction.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtDelay.Text));
                            //}
                            //else
                            //{
                            //clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST);
                            //}
                        }
                        break;

                    case "DUT_OP3_OFF":
                        //Relay bypass logic is added here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP3);
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
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                                clsGlobalVariables.blngIsOPThreeON = false;
                            }
                            //Default delay is applied here.
                            //if (  Program.objMainForm.chkApplyDelay.Checked == true)
                            //{
                            // clsGlobalVariables.objGlobalFunction.ApplyDelay(Convert.ToInt32(Program.objMainForm.txtDelay.Text));
                            //}
                            //else
                            //{
                            //clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.igFIXED_DELAY_IN_RELAYTEST);
                            //}
                        }
                        break;

                    case "START_REL_TEST":
                        //Relay test bypass logic is present here.

                        //Here in this test device itself checks the relay op1 and op2.
                        //So, both relays shape color are handled here. 
                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.INPROGRESS);
                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.INPROGRESS);

                       // .btmRetVal = clsGlobalVariables.objQueriescls.MBStartRelayTest();

                        //if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        //{
                        //    //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.PASS);
                        //    //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.PASS);
                        //}
                        //else
                        //{
                        //    //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                        //    //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                        //    //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                        //}

                        break;

                    case "START_DISP_TEST":
                        clsGlobalVariables.ig_Query_TimeOut = 16000;
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            DialogResult dlgMsgBxRslt;
                            bool blnmStatus;
                            blnmStatus = false;
                            //Display bypass logic is added here. 

                            clsMessages.DisplayMessage(clsMessageIDs.OBSERVE_DISP_TEST);
                            clsGlobalVariables.mainWindowVM.DisplayKeypadTest(DUT, "Display Test", true);
                            Repeat:
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpDisplay, clsGlobalVariables.enmStatus.INPROGRESS);
                            /*Here two tests gets carried out inside display test.
                             1.Display Test 
                             2.Leaky MOSFET Test:- After completion of display test leaky mosfet test gets conducted.
                             */
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBStartTest(clsGlobalVariables.CHK_DISP, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    btmRetVal = clsGlobalVariables.objQueriescls.MBStartTest(clsGlobalVariables.CHK_LEAKY_MOSFET, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                }
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_DISP);
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_LEAKY_MOSFET);
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
                                dlgMsgBxRslt = MessageBox.Show("Do you want to test display again?", clsGlobalVariables.strg_Application, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                                //dlgMsgBxRslt = MessageBox.Show(objResManager.GetString("REPEAT_DISP_TEST", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                                if (dlgMsgBxRslt == DialogResult.OK)
                                {
                                    goto Repeat;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                            clsGlobalVariables.mainWindowVM.DisplayKeypadTest(DUT, "Display Test", false);
                        }
                        break;


                    case "START_KEYPAD_TEST":
                        //Keypad bypass logic is added here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpKeypad, clsGlobalVariables.enmStatus.INPROGRESS);
                            btmRetVal = clsGlobalVariables.objGlobalFunction.TestKeyPad(DUT);
                            clsGlobalVariables.mainWindowVM.DisplayKeypadTest(DUT, "", false);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                            }
                            else
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            }
                            
                        }
                        break;

                    case "WRITE_CALIB_CONST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                            {
                                //This check is for device having modbus.
                                if (clsModelSettings.blnRS485Flag == true)
                                {
                                    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteCalibConst(false, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), DUT);
                                }
                                else//Device without modbus
                                {
                                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.CALIB_STAGE);
                                }
                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.CALIB_STAGE);
                            }
                        }
                        break;

                    //-------Changed By Shubham
                    //Date:- 24-02-2018
                    //Version:- V16
                    //Statement:- New Test is added in the software.
                    //Use of this test is to support the new Firmware V03.                    
                    case "WRITE_CALIB_CONST_WITH_VREF":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                //Meaning of false is to send the VREF value to the DUT.
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteCalibConst(true, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), DUT);
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.CALIB_STAGE);
                            }
                        }
                        break;
                    //---------Changes End.

                    case "CALIBRATE_CURRENT":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBStartCalibration(clsGlobalVariables.CURRENT, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.CALIBRATE_FUNC_CODE, clsGlobalVariables.CURRENT);
                            }
                        }
                        break;

                    case "CALIBRATE_VOLTAGE":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBStartCalibration(clsGlobalVariables.VOLTAGE, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.CALIBRATE_FUNC_CODE, clsGlobalVariables.VOLTAGE);
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;

                    case "SET_DFALT_4MA_CNT":

                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.INPROGRESS);
                        clsGlobalVariables.strgOngoingTestName = "Analog Output mA Calibration";
                        clsMessages.DisplayMessage(clsMessageIDs.CURRENT_SETTING_MSG_ID);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                break;

                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_CURRENT_DFALT_COUNT, clsGlobalVariables.MA_4, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_4_MA_CNT);
                            }

                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //This statement is added here for manual calibration only.
                                //CA55 Program.objMainForm.lblSetVal.Text = "04.00";
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;
                    case "SET_DFALT_1MA_CNT": //only for PI
                        //analog op test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.INPROGRESS);


                        clsGlobalVariables.strgOngoingTestName = "Analog Output mA Calibration";
                        clsMessages.DisplayMessage(clsMessageIDs.CURRENT_SETTING_MSG_ID);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                break;

                            //This check is for device having modbus.
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_1_MA_CNT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //This statement is added here for manual calibration only.
                                //CA55 Program.objMainForm.lblSetVal.Text = "01.00";
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;

                    case "SET_DFALT_20MA_CNT":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_CURRENT_DFALT_COUNT, clsGlobalVariables.MA_20, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_20_MA_CNT);
                            }

                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //This statement is added here for manual calibration only.
                                //CA55 Program.objMainForm.lblSetVal.Text = "20.00";
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
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
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                break;

                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_VOLTAGE_DFALT_COUNT, clsGlobalVariables.VOLT_1, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_1_VOLT_CNT);
                            }

                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //This statement is added here for manual calibration only.
                                //CA55 Program.objMainForm.lblSetVal.Text = "01.00";
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;

                    case "SET_DFALT_10V_CNT":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_VOLTAGE_DFALT_COUNT, clsGlobalVariables.VOLT_10, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_10_VOLT_CNT);
                            }

                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //This statement is added here for manual calibration only.
                                //CA55 Program.objMainForm.lblSetVal.Text = "10.00";
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;
                    case "SET_OBSRVED_4MA_CNT":


                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.

                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //Here calibrator's measure knob position is checked. 
                            //btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);
                            //if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Value present on the calibrator measure has been read and saved in a global variable.
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Here value read from calibrator measure is converted into integer variable. 
                                    clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                    //This function "ValidateAnalogVal" validates the value given by the calibrator's measure. 
                                    btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.FOUR_mAMP);

                                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        //Value read from calibrator for 4mA Analog OP sensor is saved in log object
                                        clsGlobalVariables.objDataLog[DUT].StrmAnalogOP_mA_4 = clsGlobalVariables.strgAnalogData;
                                        //This check is for device having modbus.
                                        if (clsModelSettings.blnRS485Flag == true)
                                        {
                                            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_CURRENT_OBSERVED_COUNT, clsGlobalVariables.MA_4, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                        }
                                        else//Device without modbus
                                        {
                                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_4_MA, clsModelSettings.imAnalOpVal);
                                        }
                                    }
                                }
                                
                            }


                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                            //Messages related to analog test are displayed here.
                            //if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            //{
                            //    clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "4mA", "Successful.");
                            //}
                            //else
                            //{
                            //    clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_CURRENT, "4mA", "Failed.");
                            //    //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                            //}
                            //clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VALUE, "4mA", clsGlobalVariables.strgAnalogData);
                        }
                        break;
                    case "SET_OBSRVED_1MA_CNT":
                        //analog op test bypass logic is present here.

                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);
                        //Delay of 12 Seconds has been added here.
                        //Here calibrator's measure knob position is checked. 
                        // btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);
                        //if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
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
                                    clsGlobalVariables.objDataLog[DUT].StrmAnalogOP_mA_1 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_CURRENT_OBSERVED_COUNT, clsGlobalVariables.MA_1, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_1_MA, clsModelSettings.imAnalOpVal);
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
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

                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                                                                                                                //btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);

                        //if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.TWENTY_mAMP);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 20mA Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog[DUT].StrmAnalogOP_mA_20 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_CURRENT_OBSERVED_COUNT, clsGlobalVariables.MA_20, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_20_MA, clsModelSettings.imAnalOpVal);
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
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;

                    case "SET_OBSRVED_1V_CNT":
                        //analog op test bypass logic is present here.

                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        //btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.One_Volt);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 1V Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog[DUT].StrmAnalogOP_Volt_1 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_VOLTAGE_OBSERVED_COUNT, clsGlobalVariables.VOLT_1, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_1_VOLT, clsModelSettings.imAnalOpVal);
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }

                        //Messages related to analog test are displayed here.
                        //if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        //{
                        //    clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "1 Volt", "Successful.");
                        //}
                        //else
                        //{
                        //    clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VOLTAGE, "1 Volt", "Failed.");

                        //    //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                        //    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                        //    //else
                        //    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP3, clsGlobalVariables.enmStatus.FAIL);
                        //}
                        //clsMessages.ShowAnalogMessageInProgressWindow(clsMessageIDs.ANALOG_TEST_VALUE, "1 Volt", clsGlobalVariables.strgAnalogData);

                        break;
                    case "SET_OBSRVED_10V_CNT":
                        //analog op test bypass logic is present here.

                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        //btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.TEN_Volt);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 10V Analog OP sensor is saved in log object
                                    clsGlobalVariables.objDataLog[DUT].StrmAnalogOP_Volt_10 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_VOLTAGE_OBSERVED_COUNT, clsGlobalVariables.VOLT_10, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_10_VOLT, clsModelSettings.imAnalOpVal);
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
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
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_CURRENT_ANLOP, clsGlobalVariables.MA_12, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_I_FUNC_CODE, clsGlobalVariables.MA_12);
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
                            

                        }
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {


                            //btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT);

                            //if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Value present on the calibrator measure has been read and saved in a global variable.
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
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
                                        clsGlobalVariables.objDataLog[DUT].StrmAnalogOP_mA_12 = clsGlobalVariables.strgAnalogData;
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
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;

                    case "SET_5V_ANLOP":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_VOLTAGE_ANLOP, clsGlobalVariables.VOLT_5, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_V_FUNC_CODE, clsGlobalVariables.VOLT_5);
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
                           
                        }

                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT);

                            //if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Value present on the calibrator measure has been read and saved in a global variable.
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
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
                                        clsGlobalVariables.objDataLog[DUT].StrmAnalogOP_Volt_5 = clsGlobalVariables.strgAnalogData;
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
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
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
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.INPROGRESS);
                            //For 1mV and 50mV calibration, "J" sensor is selected in the DUT.
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_J_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus( Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT, clsMessageIDs.ONEMV_CALIB_ERR);
                                break;
                            }


                            //Callibrator SSource is made on.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT, clsMessageIDs.ONEMV_CALIB_ERR);
                                break;
                            }
                            //1mV value is adjusted in the Calibrator's source.  
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_MV, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT, clsMessageIDs.ONEMV_CALIB_ERR);
                                break;
                            }
                        }
                        //Here software reads the count and validates that count for double acting. 
                        //For single acting devices software does not validates received counts.
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.MV_1_CNT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEMV_CALIB_ERR);
                            break;
                        }
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.PASS);
                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEMV_CALIB_SUCCESS);
                        break;

                    case "CALIB_50_MV_CNT":
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp50mV, clsGlobalVariables.enmStatus.INPROGRESS);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.FIFTY_MV, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp50mV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.FIFTYMV_CALIB_ERR);
                                break;
                            }
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.MV_50_CNT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp50mV, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.FIFTYMV_CALIB_ERR);
                            break;
                        }

                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp50mV, clsGlobalVariables.enmStatus.PASS);
                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.FIFTYMV_CALIB_SUCCESS);
                        break;

                    case "CALC_SLOPE_OFFSET":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag)
                            {
                                clsGlobalVariables.objGlobalFunction.CalSlopeOffset(DUT);
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                            else//Device without modbus
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);

                        }
                        break;

                    case "CALIB_TC":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.Calc_Cjc_Offset(DUT);
                                clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);
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
                                    btmRetVal = clsGlobalVariables.objGlobalFunction.DeviceWrite((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), DUT);
                                    break;
                                }
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objGlobalFunction.MBSendPvSlaveToDut((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    long lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 1);

                                    if (lmData != 2)
                                    {

                                        btmRetVal =(byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                        break;
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);

                        }
                        break;

                    case "CALIB_PT100":

                        clsGlobalVariables.strgOngoingTestName = "PT100 Sensor Calibration";
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                        {
                            clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_96x96);
                        }
                        else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                        {
                            clsMessages.DisplayMessage(clsMessageIDs.THREEWIRE_MSG_ID);
                        }
                        else
                        {
                            clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_PI);
                        }

                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_RTD_KNOB_POS, clsGlobalVariables.SOURCE_RTD_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            //btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_RTD_KNOB_POS, clsGlobalVariables.SOURCE_RTD_KNOB_TEXT);

                            //if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //{
                            //    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                            //    //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                            //    break;
                            //}

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.THREEFIFTY_OHM, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_PT100_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                                break;
                            }
                        }
                       
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.PT100_CNT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                            break;
                        }

                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.Calc_Current(DUT);
                                clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);
                                //This check is added here because while calibrating the device having Analog IP sensor,
                                //if this query is sent to the device after completion of PT100 sensor calibration,
                                //slopes and offsets for analog IP sensors are get written as Zero.
                                //Due to this on the device display proper values does not get displayed.
                                //On the device display zero value gets displayed.
                                if (clsGlobalVariables.igTYPE_OF_DEVICE != clsGlobalVariables.igDoubleActingWithAnalogIPType)
                                {
                                    btmRetVal = clsGlobalVariables.objGlobalFunction.DeviceWrite((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), DUT);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.FAIL);
                                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.THREEFIFTYOHM_CALIB_ERR);
                                        break;
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);

                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp350Ohm, clsGlobalVariables.enmStatus.PASS);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.THREEFIFTYOHM_CALIB_SUCCESS);
                        }
                        break;

                    case "CALIB_1V_CNT":
                        //chkVtg test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.INPROGRESS);
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Error message of the 1V calibration is displyed here.
                                //Also color of shape of 1V present on the main form is changed to RED.
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEVOLT_CALIB_ERR);
                                break;
                            }

                            //-------Changed By Shubham
                            //Date:- 28-04-2018
                            //Version:- V17
                            //Statement:- Proper name is stored in the global variable to display on the picture message box.
                            clsGlobalVariables.strgOngoingTestName = "Analog Input Volt Calibration";
                            //--------Changes End.

                            if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                                break;
                            clsMessages.DisplayMessage(clsMessageIDs.VOLT_CALIBRATION_MSG_ID);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_VOLT_KNOB_POS, clsGlobalVariables.SOURCE_VOLT_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEVOLT_CALIB_ERR);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEVOLT_CALIB_ERR);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_VOLT_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEVOLT_CALIB_ERR);
                                break;
                            }
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_1V);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEVOLT_CALIB_ERR);
                            break;
                        }

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.PASS);
                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEVOLT_CALIB_SUCCESS);

                        break;

                    case "CALIB_9V_CNT":
                        //chkVtg test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.INPROGRESS);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.NINE_VOLT_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_ERR);
                                break;
                            }
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_9V);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_ERR);
                            break;
                        }
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.CalSlopeOffset110V(DUT);
                            }

                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);
                        }
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.PASS);
                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_SUCCESS);
                        break;

                    case "CALIB_4mA_CNT":
                        //chkCurrent test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.INPROGRESS);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            //-------Changed By Shubham
                            //Date:- 28-04-2018
                            //Version:- V17
                            //Statement:- Proper name is stored in the global variable to display on the picture message box.
                            clsGlobalVariables.strgOngoingTestName = "Analog Input mA Calibration";
                            //---------Changes End.
                            if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                                break;
                            clsMessages.DisplayMessage(clsMessageIDs.MA_CALIBRATION_MSG_ID);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            //MessageBox.Show("clsGlobalVariables.mA_SENSOR+1 PENDIGN");
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.mA_SENSOR + 1, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_TEXT, DUT);

                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.FOURMA_CALIB_ERR);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.FOURMA_CALIB_ERR);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.FOUR_mA_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.FOURMA_CALIB_ERR);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_4_20mA_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.FOURMA_CALIB_ERR);
                                break;
                            }
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_4mA);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.FOURMA_CALIB_ERR);
                            break;
                        }

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.PASS);
                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.FOURMA_CALIB_SUCCESS);

                        break;

                    case "CALIB_20mA_CNT":
                        //chkCurrent test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.INPROGRESS);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.TWENTY_mA_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_ERR);
                                break;
                            }
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_20mA);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_ERR);
                            break;
                        }
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.CalSlopeOffset420mA(DUT);
                            }
                            //-------Changed By Shubham
                            //Date:- 24-02-2018
                            //Version:- V16
                            //Statement:- Here calibration constants for 4-20mA sensor are converted to hexadecimal value. 
                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);
                            //------Changes End.
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.PASS);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_SUCCESS);
                        }
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
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);
                                if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                                {
                                    //Remove device connections message is displayed here.
                                    // clsMessages.DisplayMessage(clsMessageIDs.REMOVE_SOURCE_CONN);
                                    //Here device sensor is changed to 1-10V.
                                    btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE, DUT);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.FAIL);
                                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_CALIB_ERR);
                                        break;
                                    }
                                }
                            }
                            //Counts are read from the device for VREF.
                            btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_VREF);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_CALIB_ERR);
                                break;
                            }
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                                clsGlobalVariables.objGlobalFunction.Calc_VREF(DUT);
                        }  
                        else //For devices without modbus.
                        {
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBReferenceVoltageReadSingleActing(DUT);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.FAIL);
                                    //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_CALIB_ERR);
                                    break;
                                }
                            }
                        }

                        if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                        {
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {
                                switch (DUT)
                                {
                                    case 1:
                                        if (clsGlobalVariables.fltgREF_VtgDUT1 > clsGlobalVariables.fltgREF_Vtg_MAX && clsGlobalVariables.fltgREF_VtgDUT1 < clsGlobalVariables.fltgREF_Vtg_MIN)
                                        {
                                            //Error message is displayed in the progress window.
                                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_TOLERANCE_ERR);
                                            //Invalid value enum is returned here.
                                            btmRetVal= Convert.ToByte(clsGlobalVariables.enmResponseError.Invalid_data);
                                            break;
                                        }
                                        //Here vref value is stored in the datalog object.
                                        clsGlobalVariables.objDataLog[DUT].StrmRef_Vtg = clsGlobalVariables.fltgREF_VtgDUT1.ToString();
                                        //Here vref value is added in the calib const array.
                                        clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.VREF_VALUE] = clsGlobalVariables.objGlobalFunction.Float2Hex(clsGlobalVariables.fltgREF_VtgDUT1);
                                        //Pass status to VREF shape is set.
                                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.PASS);                            
                                        //VREF value is displayed in the text box present in debug form.
                                        //CA55 Program.objMainForm.txtVREF.Text = clsGlobalVariables.objDataLog[DUT].StrmRef_Vtg;
                                        break;
                                    case 2:
                                        if (clsGlobalVariables.fltgREF_VtgDUT2 > clsGlobalVariables.fltgREF_Vtg_MAX && clsGlobalVariables.fltgREF_VtgDUT2 < clsGlobalVariables.fltgREF_Vtg_MIN)
                                        {
                                            //Error message is displayed in the progress window.
                                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_TOLERANCE_ERR);
                                            //Invalid value enum is returned here.
                                            btmRetVal = Convert.ToByte(clsGlobalVariables.enmResponseError.Invalid_data);
                                            break;
                                        }
                                        //Here vref value is stored in the datalog object.
                                        clsGlobalVariables.objDataLog[DUT].StrmRef_Vtg = clsGlobalVariables.fltgREF_VtgDUT2.ToString();
                                        //Here vref value is added in the calib const array.
                                        clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.VREF_VALUE] = clsGlobalVariables.objGlobalFunction.Float2Hex(clsGlobalVariables.fltgREF_VtgDUT2);
                                        //Pass status to VREF shape is set.
                                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.PASS);                            
                                        //VREF value is displayed in the text box present in debug form.
                                        //CA55 Program.objMainForm.txtVREF.Text = clsGlobalVariables.objDataLog[DUT].StrmRef_Vtg;
                                        break;
                                    case 3:
                                        if (clsGlobalVariables.fltgREF_VtgDUT3 > clsGlobalVariables.fltgREF_Vtg_MAX && clsGlobalVariables.fltgREF_VtgDUT3 < clsGlobalVariables.fltgREF_Vtg_MIN)
                                        {
                                            //Error message is displayed in the progress window.
                                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_TOLERANCE_ERR);
                                            //Invalid value enum is returned here.
                                            btmRetVal = Convert.ToByte(clsGlobalVariables.enmResponseError.Invalid_data);
                                            break;
                                        }
                                        //Here vref value is stored in the datalog object.
                                        clsGlobalVariables.objDataLog[DUT].StrmRef_Vtg = clsGlobalVariables.fltgREF_VtgDUT3.ToString();
                                        //Here vref value is added in the calib const array.
                                        clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.VREF_VALUE] = clsGlobalVariables.objGlobalFunction.Float2Hex(clsGlobalVariables.fltgREF_VtgDUT3);
                                        //Pass status to VREF shape is set.
                                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.PASS);                            
                                        //VREF value is displayed in the text box present in debug form.
                                        //CA55 Program.objMainForm.txtVREF.Text = clsGlobalVariables.objDataLog[DUT].StrmRef_Vtg;
                                        break;
                                    case 4:
                                        if (clsGlobalVariables.fltgREF_VtgDUT4 > clsGlobalVariables.fltgREF_Vtg_MAX && clsGlobalVariables.fltgREF_VtgDUT4 < clsGlobalVariables.fltgREF_Vtg_MIN)
                                        {
                                            //Error message is displayed in the progress window.
                                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_TOLERANCE_ERR);
                                            //Invalid value enum is returned here.
                                            btmRetVal = Convert.ToByte(clsGlobalVariables.enmResponseError.Invalid_data);
                                            break;
                                        }
                                        //Here vref value is stored in the datalog object.
                                        clsGlobalVariables.objDataLog[DUT].StrmRef_Vtg = clsGlobalVariables.fltgREF_VtgDUT4.ToString();
                                        //Here vref value is added in the calib const array.
                                        clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.VREF_VALUE] = clsGlobalVariables.objGlobalFunction.Float2Hex(clsGlobalVariables.fltgREF_VtgDUT4);
                                        //Pass status to VREF shape is set.
                                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.PASS);                            
                                        //VREF value is displayed in the text box present in debug form.
                                        //CA55 Program.objMainForm.txtVREF.Text = clsGlobalVariables.objDataLog[DUT].StrmRef_Vtg;
                                        break;
                                }
                                //Validation on VREF value is applied here.
                                
                            }
                        }
                        else
                        {
                            //Fail status to VREF shape is set.
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_CALIB_ERR);
                        }
                        break;
                    //---------Changes END.

                    //This test is not present in the INI file. After successful calibration, Calibrator's source is made OFF.
                    //case "SOURCE_OFF":
                    //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    //        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);

                    //    break;
                    case "START_REL_TEST_OP1_RELAY":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //24,28,32,36
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(5);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(9);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(13);

                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }

                            }
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(5);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(9);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(13);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(24);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(28);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(32);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(36);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        break;
                    case "START_REL_TEST_OP2_RELAY":
                        //24,28,32,36
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                                
                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP2);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                                
                            }
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(3);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(7);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(11);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(15);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                                
                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                                
                            }
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(3);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(7);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(11);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(15);

                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    break;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(clsGlobalVariables.DUT1, clsGlobalVariables.PASS);
                        }

                        break;
                    case "START_REL_TEST_PI":
                    ////Relay test bypass logic is present here.

                    ////Here in this test device itself checks the relay op1 and op2.
                    ////So, both relays shape color are handled here. 
                    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.INPROGRESS);
                    ////CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.INPROGRESS);
                    //btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                    //if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //{
                    //    clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                    //    if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //    {
                    //        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //        // btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevicesPI(clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_RELAY);
                    //        if (MainWindowVM.initilizeCommonObject.objplcSerialComm.OpenCommPort(clsGlobalVariables.strgComPortPLC, false))
                    //        {
                    //            clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //            //05 from PLC
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(5);
                    //            if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //            {
                    //                clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //                //DUT_OP1_ON
                    //                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP1);
                    //                if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //                {
                    //                    clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //                    //06 from PLC
                    //                    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(6);
                    //                    if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //                    {
                    //                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //                        //DUT_OP2_ON
                    //                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP2);
                    //                        if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //                        {
                    //                            clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //                            //0A from PLC
                    //                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(10);
                    //                            if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //                            {
                    //                                clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //                                //DUT_OP1_OFF
                    //                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                    //                                if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //                                {
                    //                                    clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //                                    //09 from PLC
                    //                                    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(9);
                    //                                    if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //                                    {
                    //                                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //                                        //DUT_OP2_OFF
                    //                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                    //                                        if (btmRetVal == Convert.ToByte(clsGlobalVariables.enmResponseError.Success))
                    //                                        {
                    //                                            clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.PLC_ZIG_COMM_DELAY);
                    //                                            //05 from PLC
                    //                                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartRelayTest_PI(5);
                    //                                        }
                    //                                    }
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            btmRetVal = Convert.ToByte(clsGlobalVariables.enmResponseError.Invalid_data);
                    //        }
                    //    }
                    //}
                    //MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();

                    //if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    //{
                    //    //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.PASS);
                    //    //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.PASS);
                    //}
                    //else
                    //{
                    //    //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP1, clsGlobalVariables.enmStatus.FAIL);
                    //    //CA55 Program.objMainForm.ShowStatusOutput(Program.objMainForm.PictOP2, clsGlobalVariables.enmStatus.FAIL);
                    //    //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.RELAY_DEBUG_MSG_ID);
                    //}

                    //break;
                    case "CALIB_1V_CNT_PI":
                        //chkVtg test bypass logic is present here.

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.INPROGRESS);

                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_VOLT_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEVOLT_CALIB_ERR);
                                break;
                            }
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_1V);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEVOLT_CALIB_ERR);
                            break;
                        }

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.PASS);
                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEVOLT_CALIB_SUCCESS);
                        break;
                    case "CALIB_9V_CNT_PI":

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.INPROGRESS);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                                break;
                            clsMessages.DisplayMessage(clsMessageIDs.VOLT_CALIBRATION_MSG_ID);

                            clsGlobalVariables.strgOngoingTestName = "Analog Input Volt Calibration";
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_VOLT_KNOB_POS, clsGlobalVariables.SOURCE_VOLT_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.VTG_SENSOR_30V, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            //btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_VOLT_KNOB_POS, clsGlobalVariables.SOURCE_VOLT_KNOB_TEXT);
                            //if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //{
                            //    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            //    //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_ERR);
                            //    break;
                            //}
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Error message of the 1V calibration is displyed here.
                                //Also color of shape of 1V present on the main form is changed to RED.
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_ERR);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_ERR);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.NINE_VOLT_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_ERR);
                                break;
                            }
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_9V);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_ERR);
                            break;
                        }
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.CalSlopeOffset110V(DUT);
                            }

                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);

                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.PASS);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_SUCCESS);
                        }
                        break;
                    case "CALIB_1mA_CNT_PI":

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.INPROGRESS);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            clsGlobalVariables.strgOngoingTestName = "Analog Input mA Calibration";
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_mA_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEMA_CALIB_ERR);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_20mA_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEMA_CALIB_ERR);
                                break;
                            }
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_4mA);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEMA_CALIB_ERR);
                            break;
                        }

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpFourmA, clsGlobalVariables.enmStatus.PASS);
                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.ONEMA_CALIB_SUCCESS);
                        break;
                    case "CALIB_20mA_CNT_PI":

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.INPROGRESS);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                                break;
                            clsMessages.DisplayMessage(clsMessageIDs.MA_CALIBRATION_MSG_ID);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.mA_SENSOR, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_ERR);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_ERR);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.TWENTY_mA_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_ERR);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_20mA_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_ERR);
                                break;
                            }
                        }
                        btmRetVal = clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_20mA);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                            //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_ERR);
                            break;
                        }
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.CalSlopeOffset420mA(DUT);
                            }
                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);
                        }
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.PASS);
                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_SUCCESS);
                        break;
                    case "START_MODBUS_TEST":

                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpModbusTest, clsGlobalVariables.enmStatus.INPROGRESS);
                        MessageBox.Show("pending");
                        //btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartModBusTest_PI();
                        //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        //    {
                        //        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpModbusTest, clsGlobalVariables.enmStatus.FAIL);
                        //        break;
                        //    }
                       
                        ////CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpModbusTest, clsGlobalVariables.enmStatus.PASS);
                        break;
                    case "24V_OP_TEST":
                        MessageBox.Show("pending");
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp24VoltOPTest, clsGlobalVariables.enmStatus.INPROGRESS);
                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.Test_24Volt_OUTPUT_TEST_MSG);
                        //if (MainWindowVM.initilizeCommonObject.objplcSerialComm.OpenCommPort(clsGlobalVariables.strgComPortPLC, false))
                        //{
                        //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBReadPLC_Output();
                        //}
                        //MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();

                        //if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        //{
                        //    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp24VoltOPTest, clsGlobalVariables.enmStatus.FAIL);                            
                        //    break;
                        //}
                        btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp24VoltOPTest, clsGlobalVariables.enmStatus.PASS);
                        break;
                    case "CJC_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {

                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_J_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.INPROGRESS);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_TC_KNOB_POS, clsGlobalVariables.SOURCE_TC_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.J_SENSOR, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpOneV, clsGlobalVariables.enmStatus.FAIL);

                                break;
                            }

                        }

                            clsGlobalVariables.objGlobalFunction.ApplyDelay(1000);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            btmRetVal = clsGlobalVariables.objQueriescls.ReadPVSingleActingCJC((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.Shp_CJC.TextONShape = clsGlobalVariables.shrtgCJC.ToString();
                               
                                    //btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                    break;
                                
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.PASS);
                            }
                          
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_POS,DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }


                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_60_MV_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.mV_SENSOR, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                                break;
                            }



                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.strgONE_MV, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp_CJC, clsGlobalVariables.enmStatus.FAIL);
                            }
                        }
                        break;
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                //throw ex;
            }
            return btmRetVal;
        }
    }
}

