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
                                clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                                btmRetVal = clsGlobalVariables.objQueriescls.ReadDeviceID((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                            }
                            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        }
                        else //Device without modbus
                        {
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {
                                clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                                btmRetVal = clsGlobalVariables.objQueriescls.ReadDeviceIDSalveToDut((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                            }
                            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        }
                        break;

                    case "READ_CALIB_STATUS":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Accuracy_Test_Not_Done;
                                continue;
                            }
                            //This "clsGlobalVariables.Done" tells that device is calibrated & accuracy testing is also done.
                            else if (clsModelSettings.btmCalibConst == clsGlobalVariables.CALIB_DONE)
                            {
                                clsGlobalVariables.mainWindowVM.DisplayMessage(DUT, clsMessageIDs.CALIBRATED_DUT);
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }

                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "DISPLAY_TEST":
                        clsGlobalVariables.ig_Query_TimeOut = 16000;
                        clsGlobalVariables.objGlobalFunction.DisplayMessageBox(clsMessages.DisplayMessage(clsMessageIDs.OBSERVE_DISP_TEST), clsGlobalVariables.strNotifyMsg);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            DialogResult dlgMsgBxRslt;
                            bool blnmStatus;
                            blnmStatus = false;
                            //Display bypass logic is added here. 
                            clsGlobalVariables.mainWindowVM.DisplayKeypadTest(DUT, "Display Test", true);
                            Repeat:
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
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "KEYPAD_TEST":
                        clsGlobalVariables.ig_Query_TimeOut = 1200;
                        //Keypad bypass logic is added here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpKeypad, clsGlobalVariables.enmStatus.INPROGRESS);
                            btmRetVal = clsGlobalVariables.objGlobalFunction.TestKeyPad(DUT);
                            clsGlobalVariables.mainWindowVM.DisplayKeypadTest(DUT, "", false);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "SWITCH_SENSOR_RELAY": //In this section calibrator settings are get done for further tests.
                        clsGlobalVariables.strgOngoingTestName = "mV Sensor Calibration";
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                            clsMessages.DisplayMessage(clsMessageIDs.TWO_WIRE_MSG_96x96);
                        else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                            clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID);
                        else
                            clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID_PI);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objQueriescls.SwitchSensorRly(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Statement:- Proper name is stored in the global variable to display on the picture message box. 
                                clsGlobalVariables.strgOngoingTestName = "mV Sensor Calibration";
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_POS, DUT);
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
                                            if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_48x48)
                                                btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.strgEight_MV, DUT);
                                            else
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
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;


                    #region SSR Test Commented
                    //case "SSR_Test":
                    //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    //    {
                    //        if (DUT == 1)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(9);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 2)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(13);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 3)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(17);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 4)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(21);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (clsModelSettings.blnRS485Flag == true)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                    //        }
                    //        else//Device without modbus
                    //        {
                    //            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP3);
                    //        }
                    //        int count = 0;
                    //        if (clsGlobalVariables.objPLCQueriescls.ReadInputRegistersQuery(clsGlobalVariables.ANALOG_INPUT_REG_OFFSET_ADD_STD + (DUT - 1), ref count))
                    //        {
                    //            //check count.
                    //            if (count < 418 || count > 462)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 1)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(9);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 2)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(13);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 3)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(17);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 4)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(21);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 1)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(8);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 2)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(12);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 3)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(16);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 4)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(20);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        bool testPassFlag = false;
                    //        //Display message to to check SSR on dispaly of DUt
                    //        if (DialogResult.Yes != MessageBox.Show("Check SSR on DUT : " + DUT.ToString(), "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    //        {
                    //            //Fails
                    //            testPassFlag = true;
                    //            //clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //            //continue;
                    //        }
                    //        if (clsModelSettings.blnRS485Flag == true)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                    //        }
                    //        else//Device without modbus
                    //        {
                    //            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP3);
                    //        }
                    //        if (DUT == 1)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(8);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 2)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(12);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 3)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(16);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (DUT == 4)
                    //        {
                    //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(20);
                    //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    //            {
                    //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //                continue;
                    //            }
                    //        }
                    //        if (testPassFlag)
                    //        {
                    //            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                    //            continue;
                    //        }
                    //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                    //    }
                    //    clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                    //    break; 
                    #endregion


                    case "OP2_OP3_TEST":
                        clsGlobalVariables.ig_Query_TimeOut = 5000;
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.Relay_SSR_OP2_OP3_TEST[(DUT - 1)]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            #region Comment
                            //if (DUT == 1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(10);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(14);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(18);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(22);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //} 
                            #endregion

                            long lmData;
                            bool testPassFailFlag = false;
                            //This check is for device having modbus.                
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBStartTest(clsGlobalVariables.TEST_REL, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.START_TEST_FUNC_CODE, clsGlobalVariables.CHK_RELAY);
                            }

                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //This check is for device having modbus.                
                                if (clsModelSettings.blnRS485Flag == true)
                                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 1);
                                else//Device without modbus
                                    lmData = clsGlobalVariables.objGlobalFunction.GetNumber(ref clsGlobalVariables.btgRxBuffer, 3, 2);
                                if (lmData != 1)
                                    testPassFailFlag = true;
                            }
                            else
                            {
                                testPassFailFlag = true;
                            }

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.Relay_SSR_OP2_OP3_TEST[(DUT - 1)]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            #region Comment
                            //if (DUT == 1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(10);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(14);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(18);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(22);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //} 
                            #endregion

                            if (testPassFailFlag)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                        for (int DUT = 0; DUT < clsGlobalVariables.Relay_SSR_OP2_OP3_TEST.Length; DUT++)
                        {
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.Relay_SSR_OP2_OP3_TEST[(DUT)]);
                        }

                        //btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(10);
                        //btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(14);
                        //btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(18);
                        //btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(22);
                        break;
                   

                    case "OP1_SSR_TEST":
                        clsGlobalVariables.ig_Query_TimeOut = 5000;
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP_SSR_TEST[(DUT - 1)]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            #region Comment
                            //if (DUT == 1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(9);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(13);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(17);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(21);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //} 
                            #endregion
                            
                            int count = 0;

                            if (clsGlobalVariables.objPLCQueriescls.ReadInputRegistersQuery(clsGlobalVariables.ANALOG_INPUT_REG_OFFSET_ADD_STD + (DUT - 1), ref count))
                            {
                                //check count.
                                if (count < 418 || count > 525)
                                {

                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    if (clsModelSettings.blnRS485Flag)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                            continue; ;
                                        }

                                    }
                                    else
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                            continue; ;
                                        }
                                    }
                                    continue;
                                }
                            }
                            else
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                if (clsModelSettings.blnRS485Flag)
                                {
                                    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                        continue; ;
                                    }

                                }
                                else
                                {
                                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                        continue; ;
                                    }
                                }
                                continue; ;
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                        for (int DUT = 0; DUT < clsGlobalVariables.OP_SSR_TEST.Length; DUT++)
                        {
                            clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP_SSR_TEST[(DUT)]);
                        }

                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(9);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(13);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(17);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(21);

                        break;

                    case "OP2_SSR_TEST":
                        clsGlobalVariables.ig_Query_TimeOut = 5000;
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP2);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP_SSR_TEST[(DUT - 1)]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            #region Comment
                            //if (DUT == 1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(9);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(13);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(17);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //}
                            //if (DUT == 4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(21);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;
                            //    }
                            //} 
                            #endregion
                            
                            int count = 0;

                            if (clsGlobalVariables.objPLCQueriescls.ReadInputRegistersQuery(clsGlobalVariables.ANALOG_INPUT_REG_OFFSET_ADD_STD + (DUT - 1), ref count))
                            {
                                //check count.
                                if (count < 418 || count > 525)
                                {

                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    if (clsModelSettings.blnRS485Flag)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                            continue; ;
                                        }

                                    }
                                    else
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                            continue; ;
                                        }
                                    }
                                    continue;
                                }
                            }
                            else
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                if (clsModelSettings.blnRS485Flag)
                                {
                                    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                        continue; ;
                                    }

                                }
                                else
                                {
                                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                        continue; ;
                                    }
                                }
                                continue; ;
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();


                        for (int DUT = 0; DUT < clsGlobalVariables.OP_SSR_TEST.Length; DUT++)
                        {
                            clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP_SSR_TEST[(DUT)]);
                        }

                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(9);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(13);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(17);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(21);

                        break;

                    case "WRITE_CALIB_CONST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;                                      
                    case "WRITE_CALIB_CONST_WITH_VREF":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                        //12mA
                    case "CALIBRATE_CURRENT":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBStartCalibration(clsGlobalVariables.CURRENT, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.CALIBRATE_FUNC_CODE, clsGlobalVariables.CURRENT);
                            }


                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);

                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                        //5V
                    case "CALIBRATE_VOLTAGE":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBStartCalibration(clsGlobalVariables.VOLTAGE, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.CALIBRATE_FUNC_CODE, clsGlobalVariables.VOLTAGE);
                            }
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;

                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "SET_DFALT_4MA_CNT":
                        clsGlobalVariables.strgOngoingTestName = "Analog Output mA Calibration";
                        clsMessages.DisplayMessage(clsMessageIDs.CURRENT_SETTING_MSG_ID);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_CURRENT_DFALT_COUNT, clsGlobalVariables.MA_4, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_4_MA_CNT);
                            }

                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "SET_DFALT_1MA_CNT": //only for PI
                        clsGlobalVariables.strgOngoingTestName = "Analog Output mA Calibration";
                        clsMessages.DisplayMessage(clsMessageIDs.CURRENT_SETTING_MSG_ID);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_mA_KNOB_POS, clsGlobalVariables.MEASURE_mA_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            //This check is for device having modbus.
                            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_1_MA_CNT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "SET_DFALT_20MA_CNT":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_CURRENT_DFALT_COUNT, clsGlobalVariables.MA_20, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_20_MA_CNT);
                            }

                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "SET_DFALT_1V_CNT":
                        clsGlobalVariables.strgOngoingTestName = "Analog Output Volt Calibration";
                        clsMessages.DisplayMessage(clsMessageIDs.VOLTAGE_SETTING_MSG_ID);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_VOLTAGE_DFALT_COUNT, clsGlobalVariables.VOLT_1, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_1_VOLT_CNT);
                            }
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "SET_DFALT_10V_CNT":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //This checSET_OBSRVED_10V_CNTk is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_VOLTAGE_DFALT_COUNT, clsGlobalVariables.VOLT_10, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, clsGlobalVariables.DEFAULT_10_VOLT_CNT);
                            }

                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                        //4mA
                    case "4mA_ANALOG_OP_TEST":
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable. 
                                clsModelSettings.imAnalOpVal[DUT - 1] = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure. 
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.FOUR_mAMP, DUT);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 4mA Analog OP sensor is saved in log object
                                    // clsGlobalVariables.objDataLog[DUT - 1].StrmAnalogOP_mA_4 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.

                                    //Log objDataLog[DUT - 1]


                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_CURRENT_OBSERVED_COUNT, clsGlobalVariables.MA_4, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_4_MA, clsModelSettings.imAnalOpVal[DUT - 1]);
                                    }
                                    
                                }
                            }
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "1mA_ANALOG_OP_TEST":
                        //analog op test bypass logic is present here.
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable. 
                                clsModelSettings.imAnalOpVal[DUT - 1] = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //ssageBox.Show(clsModelSettings.imAnalOpVal[DUT-1].ToString());
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure. 
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.ONE_mAMP_case, DUT);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 1mA Analog OP sensor is saved in log object
                                   // clsGlobalVariables.objDataLog[DUT - 1].StrmAnalogOP_mA_1 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_CURRENT_OBSERVED_COUNT, clsGlobalVariables.MA_1, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_1_MA, clsModelSettings.imAnalOpVal[DUT - 1]);
                                    }
                                }
                                else
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "20mA_ANALOG_OP_TEST":
                        //analog op test bypass logic is present here.
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);
                        //Delay of 12 Seconds has been added here.                                            
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal[DUT - 1] = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.TWENTY_mAMP, DUT);
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 20mA Analog OP sensor is saved in log object
                                  //  clsGlobalVariables.objDataLog[DUT - 1].StrmAnalogOP_mA_20 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.

                                    //log

                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_CURRENT_OBSERVED_COUNT, clsGlobalVariables.MA_20, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_20_MA, clsModelSettings.imAnalOpVal[DUT - 1]);
                                    }
                                }
                            }
                            //Messages related to analog test are displayed here.
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "1V_ANALOG_OP_TEST":
                        //analog op test bypass logic is present here.
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        //btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal[DUT - 1] = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.One_Volt, DUT);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 1V Analog OP sensor is saved in log object
                                    //clsGlobalVariables.objDataLog[DUT - 1].StrmAnalogOP_Volt_1 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_VOLTAGE_OBSERVED_COUNT, clsGlobalVariables.VOLT_1, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_1_VOLT, clsModelSettings.imAnalOpVal[DUT - 1]);
                                    }
                                }
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;

                                }
                            }
                            else
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "10V_ANALOG_OP_TEST":
                        //analog op test bypass logic is present here.
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        //btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorMeasureOnAndReadKnobPos(clsGlobalVariables.MEASURE_10VOLT_KNOB_POS, clsGlobalVariables.MEASURE_10VOLT_KNOB_TEXT);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal[DUT - 1] = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.TEN_Volt, DUT);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //Value read from calibrator for 10V Analog OP sensor is saved in log object
                                   // clsGlobalVariables.objDataLog[DUT - 1].StrmAnalogOP_Volt_10 = clsGlobalVariables.strgAnalogData;
                                    //This check is for device having modbus.
                                    if (clsModelSettings.blnRS485Flag == true)
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteMeasuredAnlopVal(clsGlobalVariables.MB_SET_VOLTAGE_OBSERVED_COUNT, clsGlobalVariables.VOLT_10, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                                    }
                                    else//Device without modbus
                                    {
                                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_OBSERVED_10_VOLT, clsModelSettings.imAnalOpVal[DUT - 1]);
                                    }
                                }
                            }
                            else
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;

                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;

                    case "SET_12MA_ANLOP":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_CURRENT_ANLOP, clsGlobalVariables.MA_12, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_I_FUNC_CODE, clsGlobalVariables.MA_12);
                            }
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {

                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsModelSettings.btmAnalogsetVal = clsGlobalVariables.TWELVE_mA;
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal[DUT - 1] = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.TWELVE_mA, DUT);
                                //Messages related to analog test are displayed here.
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                //Value read from calibrator for 12mA Analog OP sensor is saved in log object
                               // clsGlobalVariables.objDataLog[DUT - 1].StrmAnalogOP_mA_12 = clsGlobalVariables.strgAnalogData;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "SET_5V_ANLOP":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBSetAnalogOutput(clsGlobalVariables.MB_SET_VOLTAGE_ANLOP, clsGlobalVariables.VOLT_5, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_V_FUNC_CODE, clsGlobalVariables.VOLT_5);
                            }
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsModelSettings.btmAnalogsetVal = clsGlobalVariables.FIVE_Volt;
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(clsGlobalVariables.CALIB_MEASURE_DELAY);//Delay of 12 Seconds has been added here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //Value present on the calibrator measure has been read and saved in a global variable.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ReadCalibratorMeasureValue(DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //Here value read from calibrator measure is converted into integer variable.
                                clsModelSettings.imAnalOpVal[DUT - 1] = clsGlobalVariables.objGlobalFunction.ConvertStringToInt(clsGlobalVariables.strgAnalogData);
                                //This function "ValidateAnalogVal" validates the value given by the calibrator's measure.
                                btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsGlobalVariables.FIVE_Volt, DUT);
                                //Messages related to analog test are displayed here.
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                //clsGlobalVariables.objDataLog[DUT - 1].StrmAnalogOP_Volt_5 = clsGlobalVariables.strgAnalogData;
                            }                           
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;

                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break; 
                    case "12mA_ANALOG_OP_TEST":
                    case "5V_ANALOG_OP_TEST":
                        //analog op test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //In manual calibration mode this test is present. for 12 mA and 5Volts analog output validation.  
                            btmRetVal = clsGlobalVariables.objGlobalFunction.ValidateAnalogVal(clsModelSettings.btmAnalogsetVal, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "1mV_CALIB_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp1mv, clsGlobalVariables.enmStatus.INPROGRESS);
                            //For 1mV and 50mV calibration, "J" sensor is selected in the DUT.
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_J_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            //Callibrator SSource is made on.
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            //1mV value is adjusted in the Calibrator's source.  
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_MV, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.MV_1_CNT);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        break;
                    case "50mV_CALIB_TEST":
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.Shp50mV, clsGlobalVariables.enmStatus.INPROGRESS);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.FIFTY_MV, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.MV_50_CNT);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        break;
                    case "CALC_SLOPE_OFFSET":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "CALIB_PT100_313":
                        clsGlobalVariables.strgOngoingTestName = "PT100 Sensor Calibration";
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.THREEOneThree_OHM, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.PT313_CNT);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                                        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                        continue;
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "CALIB_47_MV_CNT":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.FOURTYSEVEN_MV, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.MV_50_CNT);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        break;
                    case "CALIB_PT100_100":
                        clsGlobalVariables.strgOngoingTestName = "PT100 Sensor Calibration";
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                            clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_96x96);
                        else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                            clsMessages.DisplayMessage(clsMessageIDs.THREEWIRE_MSG_ID);
                        else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_48x48)
                            clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_PI);
                        else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_96x96)
                            clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_PI);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //for 96x96 Cat Id different connection image is displayed.clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_RTD_KNOB_POS, clsGlobalVariables.SOURCE_RTD_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_RTD_KNOB_POS, clsGlobalVariables.SOURCE_RTD_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.OneHund_OHM, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_PT100_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.PT100_100_CNT_PR43);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                                        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                        continue;
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "CALIB_TC":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                                }
                                else
                                {
                                    btmRetVal = clsGlobalVariables.objGlobalFunction.DeviceWrite((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), DUT);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                        continue;
                                    }
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
                                        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                        continue;
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;

                    case "350_OHM_CALIB_TEST":
                        clsGlobalVariables.strgOngoingTestName = "PT100 Sensor Calibration";
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                            clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_96x96);
                        else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                            clsMessages.DisplayMessage(clsMessageIDs.THREEWIRE_MSG_ID);
                        else
                            clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_PI);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_RTD_KNOB_POS, clsGlobalVariables.SOURCE_RTD_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.THREEFIFTY_OHM, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_PT100_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.PT100_CNT);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                                //MessageBox.Show("Pending igTYPE_OF_DEVICE");
                                if (clsGlobalVariables.igTYPE_OF_DEVICE != clsGlobalVariables.igDoubleActingWithAnalogIPType)
                                {
                                    btmRetVal = clsGlobalVariables.objGlobalFunction.DeviceWrite((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), DUT);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                        continue;
                                    }
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "PR69_1V_ANALOG_IP_TEST":
                        clsGlobalVariables.strgOngoingTestName = "Analog Input Volt Calibration";
                        //chkVtg test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_1V);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        break;

                    case "PR69_9V_ANALOG_IP_TEST":
                        //chkVtg test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.NINE_VOLT_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.NINEVOLT_CALIB_ERR);
                                break;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_9V);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.CalSlopeOffset110V(DUT);
                            }

                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "PR69_1mA_ANALOG_IP_TEST":
                        clsGlobalVariables.strgOngoingTestName = "Analog Input mA Calibration";
                        //chkCurrent test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            clsGlobalVariables.strgOngoingTestName = "Analog Input mA Calibration";
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
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_4mA);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        break;

                    case "PR69_20mA_ANALOG_IP_TEST":
                        //chkCurrent test bypass logic is present here.
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.TWENTY_mA_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpTwentymA, clsGlobalVariables.enmStatus.FAIL);
                                //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.TWENTYMA_CALIB_ERR);
                                break;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_20mA);                       
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.CalSlopeOffset420mA(DUT);
                            }
                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;                   
                    case "REF_VOLTAGE_CALC":
                        //Progress status to VREF shape is set.
                        if (clsModelSettings.blnRS485Flag == true)
                        {
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {
                                clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
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
                             clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_VREF);
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                                clsGlobalVariables.objGlobalFunction.Calc_VREF(DUT);
                        }  
                        else //For devices without modbus.
                        {
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {
                                clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                                btmRetVal = clsGlobalVariables.objQueriescls.MBReferenceVoltageReadSingleActing(DUT);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.FAIL);
                                    //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_CALIB_ERR);
                                    break;
                                }
                            }
                        }
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            switch (DUT)
                            {
                                case 1:
                                    if (clsGlobalVariables.fltgREF_VtgDUT1 > clsGlobalVariables.fltgREF_Vtg_MAX && clsGlobalVariables.fltgREF_VtgDUT1 < clsGlobalVariables.fltgREF_Vtg_MIN)
                                    {
                                        //Error message is displayed in the progress window.
                                        //CA55 clsGlobalVariables.mainWindowVM.DisplayMessage(DUT,clsMessageIDs.VREF_TOLERANCE_ERR);
                                        //Invalid value enum is returned here.
                                        btmRetVal = Convert.ToByte(clsGlobalVariables.enmResponseError.Invalid_data);
                                        break;
                                    }
                                    //Here vref value is stored in the datalog object.
                                    //clsGlobalVariables.objDataLog[DUT - 1].StrmRef_Vtg = clsGlobalVariables.fltgREF_VtgDUT1.ToString();
                                    //Here vref value is added in the calib const array.
                                    clsGlobalVariables.strgarrCalibConstDUT1[clsGlobalVariables.VREF_VALUE] = clsGlobalVariables.objGlobalFunction.Float2Hex(clsGlobalVariables.fltgREF_VtgDUT1);
                                    //Pass status to VREF shape is set.
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.PASS);                            
                                    //VREF value is displayed in the text box present in debug form.
                                    //CA55 Program.objMainForm.txtVREF.Text = clsGlobalVariables.objDataLog[DUT-1].StrmRef_Vtg;
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
                                  //  clsGlobalVariables.objDataLog[DUT - 1].StrmRef_Vtg = clsGlobalVariables.fltgREF_VtgDUT2.ToString();
                                    //Here vref value is added in the calib const array.
                                    clsGlobalVariables.strgarrCalibConstDUT2[clsGlobalVariables.VREF_VALUE] = clsGlobalVariables.objGlobalFunction.Float2Hex(clsGlobalVariables.fltgREF_VtgDUT2);
                                    //Pass status to VREF shape is set.
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.PASS);                            
                                    //VREF value is displayed in the text box present in debug form.
                                    //CA55 Program.objMainForm.txtVREF.Text = clsGlobalVariables.objDataLog[DUT-1].StrmRef_Vtg;
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
                                    ///clsGlobalVariables.objDataLog[DUT - 1].StrmRef_Vtg = clsGlobalVariables.fltgREF_VtgDUT3.ToString();
                                    ///Here vref value is added in the calib const array.
                                    clsGlobalVariables.strgarrCalibConstDUT3[clsGlobalVariables.VREF_VALUE] = clsGlobalVariables.objGlobalFunction.Float2Hex(clsGlobalVariables.fltgREF_VtgDUT3);
                                    //Pass status to VREF shape is set.
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.PASS);                            
                                    //VREF value is displayed in the text box present in debug form.
                                    //CA55 Program.objMainForm.txtVREF.Text = clsGlobalVariables.objDataLog[DUT-1].StrmRef_Vtg;
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
                                    //clsGlobalVariables.objDataLog[DUT - 1].StrmRef_Vtg = clsGlobalVariables.fltgREF_VtgDUT4.ToString();
                                    //Here vref value is added in the calib const array.
                                    clsGlobalVariables.strgarrCalibConstDUT4[clsGlobalVariables.VREF_VALUE] = clsGlobalVariables.objGlobalFunction.Float2Hex(clsGlobalVariables.fltgREF_VtgDUT4);
                                    //Pass status to VREF shape is set.
                                    //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpVREF, clsGlobalVariables.enmStatus.PASS);                            
                                    //VREF value is displayed in the text box present in debug form.
                                    //CA55 Program.objMainForm.txtVREF.Text = clsGlobalVariables.objDataLog[DUT-1].StrmRef_Vtg;
                                    break;
                            }
                            //Validation on VREF value is applied here.

                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "OP1_1NO_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            //24,28,32,36

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT1_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            #region Comment
                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;;
                            //    }
                            //} 
                            #endregion

                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(5);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(9);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(13);

                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(5);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(9);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(13);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            #region Comment
                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;;

                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue;;
                            //    }
                            //} 
                            #endregion

                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                        for (int DUT = 0; DUT < clsGlobalVariables.OP1_DUT1_PLC_ON_Number.Length; DUT++)
                        {
                            clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number[DUT]);
                        }

                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);
                        break;

                    case "OP2_1NO_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //26,30,34,38
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP2_DUT1_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP2_DUT1_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP2_DUT2_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP2_DUT3_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP2_DUT4_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP2);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(3);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(7);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(11);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(15);

                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(3);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(7);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(11);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(15);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT1_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT1_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT2_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;

                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT3_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT4_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                        for (int DUT = 0; DUT < clsGlobalVariables.OP2_DUT1_PLC_ON_Number.Length; DUT++)
                        {
                            clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT1_PLC_ON_Number[DUT]);
                        }

                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT1_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT2_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT3_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT4_PLC_ON_Number);
                        break;
                        
                    case "OP3_1NO_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //25,29,33,37
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP3_DUT1_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP3_DUT1_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP3_DUT2_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP3_DUT3_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP3_DUT4_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP3);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(20);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(21);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(22);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(23);

                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP3);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(20);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(21);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(22);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_OFF(23);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT1_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT1_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT2_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;

                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT3_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT4_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                        for (int DUT = 0; DUT < clsGlobalVariables.OP3_DUT1_PLC_ON_Number.Length; DUT++)
                        {
                            clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT1_PLC_ON_Number[DUT]);
                        }

                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT1_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT2_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT3_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT4_PLC_ON_Number);
                        break;

                    case "OP1_1NC_NO_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //24,28,32,36
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT1_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            #region Comment
                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //} 
                            #endregion

                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(5);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(9);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(13);

                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT1_NC_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            #region Comment
                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT1_NC_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT2_NC_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT3_NC_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT4_NC_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //} 
                            #endregion

                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(0);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(4);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(8);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(12);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;;
                                }
                            }

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            #region Comment
                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;

                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //} 

                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_NC_PLC_ON_Number[DUT - 1]);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; ;
                            }

                            //if (DUT == clsGlobalVariables.DUT1)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_NC_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT2)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT2_NC_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT3)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT3_NC_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            //if (DUT == clsGlobalVariables.DUT4)
                            //{
                            //    btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT4_NC_PLC_ON_Number);
                            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            //    {
                            //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                            //        continue; ;
                            //    }
                            //}
                            #endregion
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                        for (int DUT = 0; DUT < clsGlobalVariables.OP1_DUT1_PLC_ON_Number.Length; DUT++)
                        {
                            clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number[DUT]);
                            clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_NC_PLC_ON_Number[DUT]);
                        }

                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);

                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_NC_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT2_NC_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT3_NC_PLC_ON_Number);
                        //clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT4_NC_PLC_ON_Number);

                        break;

                    case "OP2_1NC_NO_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP2);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(3);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(7);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(11);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(15);

                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(2);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(6);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(10);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(14);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                        break;

                    case "OP3_1NC_NO_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(clsGlobalVariables.AllDUTON);
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP3);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(20);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(21);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(22);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(23);

                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (clsModelSettings.blnRS485Flag)
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            else
                            {
                                btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP3);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(500);
                            if (DUT == clsGlobalVariables.DUT1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(16);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(17);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(18);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            if (DUT == clsGlobalVariables.DUT4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(19);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue; ;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                        break;

                    #region Commented
                    /*

                                //case "OP1_1CO_TEST_PR69":
                                //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                                //    {
                                //        //24,28,32,36
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (clsModelSettings.blnRS485Flag)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP1_ON);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }

                                //        }
                                //        else
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP1);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(1);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(5);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(9);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(13);

                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (clsModelSettings.blnRS485Flag)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP1_ADDRESS, clsGlobalVariables.OP_OFF);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }

                                //        }
                                //        else
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP1);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }

                                //        }
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(0);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(4);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(8);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(12);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;

                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                                //    }
                                //    clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT1_PLC_ON_Number);
                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT2_PLC_ON_Number);
                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT3_PLC_ON_Number);
                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP1_DUT4_PLC_ON_Number);

                                //    break;

                                //case "OP2_1CO_TEST_PR69":
                                //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                                //    {
                                //        //26,30,34,38
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP2_DUT1_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP2_DUT2_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP2_DUT3_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP2_DUT4_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (clsModelSettings.blnRS485Flag)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP2_ON);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }

                                //        }
                                //        else
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP2);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(3);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(7);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(11);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(15);

                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (clsModelSettings.blnRS485Flag)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP2_ADDRESS, clsGlobalVariables.OP_OFF);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }

                                //        }
                                //        else
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP2);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }

                                //        }
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(2);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(6);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(10);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(14);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT1_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT2_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;

                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT3_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT4_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                                //    }
                                //    clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT1_PLC_ON_Number);
                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT2_PLC_ON_Number);
                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT3_PLC_ON_Number);
                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP2_DUT4_PLC_ON_Number);

                                //    break;

                                //case "OP3_1CO_TEST_PR69":
                                //    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                                //    {
                                //        //25,29,33,37
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP3_DUT1_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP3_DUT2_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP3_DUT3_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(clsGlobalVariables.OP3_DUT4_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (clsModelSettings.blnRS485Flag)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP3_ON);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }

                                //        }
                                //        else
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_ON_FUNC_CODE, clsGlobalVariables.OP3);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(20);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(21);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(22);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(23);

                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (clsModelSettings.blnRS485Flag)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.OP3_ADDRESS, clsGlobalVariables.OP_OFF);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }

                                //        }
                                //        else
                                //        {
                                //            btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SWITCH_OFF_FUNC_CODE, clsGlobalVariables.OP3);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }

                                //        }
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(16);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(17);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(18);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartReadPLC_Input_ON(19);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT1)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT1_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT2)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT2_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;

                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT3)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT3_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        if (DUT == clsGlobalVariables.DUT4)
                                //        {
                                //            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT4_PLC_ON_Number);
                                //            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                //            {
                                //                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                //                continue; ;
                                //            }
                                //        }
                                //        clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                                //    }
                                //    clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT1_PLC_ON_Number);
                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT2_PLC_ON_Number);
                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT3_PLC_ON_Number);
                                //    clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(clsGlobalVariables.OP3_DUT4_PLC_ON_Number);

                                //    break;
                                */ 
                    #endregion
                    case "PI_1V_ANALOG_IP_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_VOLT_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_1V);
                        break;
                    case "PI_9V_ANALOG_IP_TEST":
                        clsGlobalVariables.strgOngoingTestName = "Analog Input Volt Calibration";
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsMessages.DisplayMessage(clsMessageIDs.VOLT_CALIBRATION_MSG_ID);
                        //CA55 Program.objMainForm.ShowStatus(Program.objMainForm.ShpNineV, clsGlobalVariables.enmStatus.INPROGRESS);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);

                            clsGlobalVariables.strgOngoingTestName = "Analog Input Volt Calibration";
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_VOLT_KNOB_POS, clsGlobalVariables.SOURCE_VOLT_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; 
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.VTG_SENSOR_30V, DUT);

                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue; 
                            }
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE, DUT);

                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.NINE_VOLT_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_9V);                        
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            //This check is for device having modbus.
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.CalSlopeOffset110V(DUT);
                            }
                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "PI_1mA_ANALOG_IP_TEST":
                        clsGlobalVariables.strgOngoingTestName = "Analog Input mA Calibration";
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            clsGlobalVariables.strgOngoingTestName = "Analog Input mA Calibration";
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.ONE_mA_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_20mA_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_4mA);
                        break;
                    case "PI_20mA_ANALOG_IP_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsMessages.DisplayMessage(clsMessageIDs.MA_CALIBRATION_MSG_ID);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.mA_SENSOR, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_TEXT, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.TWENTY_mA_INPUT_CAL, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_20mA_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.GetCounts(clsGlobalVariables.CALIB_20mA);
                       foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                clsGlobalVariables.objGlobalFunction.CalSlopeOffset420mA(DUT);
                            }
                            clsGlobalVariables.objGlobalFunction.ConvertCalibConst(DUT);
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "MODBUS_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(39);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            if (DUT != 1)
                            {
                                clsGlobalVariables.objGlobalFunction.ApplyDelay(250);
                            }
                            //56,60 On
                            if (DUT == 1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(56);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(60);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(57);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(61);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(58);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(62);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(59);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(63);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            //39 On
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(39);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            //delay 500 mSec
                            clsGlobalVariables.objGlobalFunction.ApplyDelay(250);

                            //Read Q19,
                            btmRetVal = clsGlobalVariables.objPLCQueriescls.MBReadPLC_Output(19);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            //56,60 On
                            if (DUT == 1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(56);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(60);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(57);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(61);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(58);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(62);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(59);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(63);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(39);
                        break;
                    case "24V_OP_TEST":                        
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            if (DUT == 1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(9);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(13);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(17);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_ON(21);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            int count = 0;
                            if (clsGlobalVariables.objPLCQueriescls.ReadInputRegistersQuery(clsGlobalVariables.ANALOG_INPUT_REG_OFFSET_ADD_STD + (DUT - 1), ref count))
                            {
                                //check count.
                                if (count < 800 || count > 950)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 1)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(9);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 2)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(13);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 3)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(17);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            if (DUT == 4)
                            {
                                btmRetVal = clsGlobalVariables.objPLCQueriescls.MBStartPLC_OFF(21);
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                    continue;
                                }
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                    case "CJC_TEST":
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_J_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_TC_KNOB_POS, clsGlobalVariables.SOURCE_TC_KNOB_POS, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.J_SENSOR, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }

                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        clsGlobalVariables.objGlobalFunction.ApplyDelay(1000);
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            clsGlobalVariables.mainWindowVM.EnableProcessingWindow(DUT);
                            btmRetVal = clsGlobalVariables.objQueriescls.ReadPVSingleActingCJC((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_POS,DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_60_MV_TYPE, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.mV_SENSOR, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(clsGlobalVariables.strgONE_MV, DUT);
                            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.FAIL);
                                continue;
                            }
                            clsGlobalVariables.mainWindowVM.UpdateTestResult(DUT, clsGlobalVariables.PASS);
                        }
                        clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                        break;
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                return btmRetVal;
            }           
        }
    }
}

