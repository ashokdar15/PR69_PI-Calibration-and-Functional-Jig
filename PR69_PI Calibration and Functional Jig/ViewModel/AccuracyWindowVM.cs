using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{
    public class AccuracyWindowVM : INotifyPropertyChanged
    {

        private bool stopBtnPress = false;

        private string DUT1Result = "";
        private string DUT2Result = "";
        private string DUT3Result = "";
        private string DUT4Result = "";

        private void btnStartClk(object obj)
        {
            bool DisplayMsg = true;
            stopBtnPress = false;
            StartStopWatch(true);

            ClearData();

            clsGlobalVariables.NUMBER_OF_DUTS_List.Clear();
            for (int i = 1; i <= clsGlobalVariables.NUMBER_OF_DUTS; i++)
            {
                clsGlobalVariables.NUMBER_OF_DUTS_List.Add((byte)i);
            }

            clsGlobalVariables.strAccuracyParameter = clsGlobalVariables.AccuracyParameter.RSensor;
            clsGlobalVariables.NUMBER_OF_FAIL_DUTS_List.Clear();
            //UpdateTestResult(2, 2, "10.12", clsGlobalVariables.AccuracyParameter.RSensor);
            //UpdateTestResult(1,2,"15.12", clsGlobalVariables.AccuracyParameter.RSensor);
            //clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PI;
            //clsModelSettings.blnRS485Flag = false;

            if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                clsGlobalVariables.MB_MASTER_TO_DUT = 200;
            else
                clsGlobalVariables.MB_MASTER_TO_DUT = 100;

            //Auto com port detection
            if (clsGlobalVariables.objGlobalFunction.AutomaticCOMPortDetections(clsGlobalVariables.NUMBER_OF_DUTS) != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                System.Windows.Forms.MessageBox.Show("fail Auto maticCOMPortDetections");
                StartStopWatch(false);
                return;
            }
           // clsModelSettings.blnRS485Flag = false;
            
            clsGlobalVariables.mainWindowVM.OpenJigCOMPort();
           // clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PI;           
            Dictionary<string, List<string>> AccuracyList = new Dictionary<string, List<string>>();
            GetAccuracyDataFromJSON(AccuracyList);
            int currentTestNumber = 1;

            bool TestOkFlag = false;
            foreach (var item in AccuracyList)
            {
                if (stopBtnPress)
                {
                    break;
                }
                currentTestNumber = 1;
                switch (item.Key)
                {
                    case clsGlobalVariables.mAmpAccuracyTest:
                        clsGlobalVariables.strgOngoingTestName = "Analog Input mA Accuracy";
                        foreach (var actualTest in item.Value)
                        {
                            if (stopBtnPress)                            
                                break;
                            
                            mAmpSensorTest(actualTest, currentTestNumber, clsGlobalVariables.AccuracyParameter.mAmp);
                            
                            clsGlobalVariables.accuracyTests = clsGlobalVariables.Selectedcatid.mAmpTests;
                            clsGlobalVariables.listAccTest.Clear();
                            clsGlobalVariables.enmpointcalibration = (clsGlobalVariables.Enmpointcalibration)currentTestNumber;
                            TestOkFlag = false;
                            clsGlobalVariables.Validateaccuracytestbackcolor = true;
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {
                                
                                if (DUT == 1)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT1Result, clsGlobalVariables.AccuracyParameter.mAmp);
                                    if (!TestOkFlag)
                                            break;
                                }
                                else if (DUT == 2)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT2Result, clsGlobalVariables.AccuracyParameter.mAmp);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 3)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT3Result, clsGlobalVariables.AccuracyParameter.mAmp);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 4)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT4Result, clsGlobalVariables.AccuracyParameter.mAmp);
                                    if (!TestOkFlag)
                                        break;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                            clsGlobalVariables.Validateaccuracytestbackcolor = false;
                            if (!TestOkFlag && DisplayMsg)
                            {
                                DisplayMsg = false;
                                DialogResult dlgMsgBxRslt = System.Windows.Forms.MessageBox.Show("Do you want to abort the test?", clsGlobalVariables.strg_Application, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                                if (dlgMsgBxRslt==DialogResult.Yes)
                                    break;
                            }
                            
                            currentTestNumber++;

                        }
                        if (!TestOkFlag)                        
                            break;
                        
                        break;
                    case clsGlobalVariables.voltAccuracyTest:
                        clsGlobalVariables.strgOngoingTestName = "Analog Input Volt Accuracy";
                        foreach (var actualTest in item.Value)
                        {
                            if (stopBtnPress)
                                break;

                            VoltSensorTest(actualTest, currentTestNumber, clsGlobalVariables.AccuracyParameter.Volt);

                            clsGlobalVariables.accuracyTests = clsGlobalVariables.Selectedcatid.VoltTests;
                            clsGlobalVariables.listAccTest.Clear();
                            clsGlobalVariables.enmpointcalibration = (clsGlobalVariables.Enmpointcalibration)currentTestNumber;
                            TestOkFlag = false;
                            clsGlobalVariables.Validateaccuracytestbackcolor = true;
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {

                                if (DUT == 1)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT1Result, clsGlobalVariables.AccuracyParameter.Volt);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 2)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT2Result, clsGlobalVariables.AccuracyParameter.Volt);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 3)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT3Result, clsGlobalVariables.AccuracyParameter.Volt);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 4)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT4Result, clsGlobalVariables.AccuracyParameter.Volt);
                                    if (!TestOkFlag)
                                        break;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                            clsGlobalVariables.Validateaccuracytestbackcolor = false;
                            if (!TestOkFlag && DisplayMsg)
                            {
                                DisplayMsg = false;
                                DialogResult dlgMsgBxRslt = System.Windows.Forms.MessageBox.Show("Do you want to abort the test?", clsGlobalVariables.strg_Application, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                                if (dlgMsgBxRslt == DialogResult.Yes)
                                    break;
                            }

                            //validate
                            currentTestNumber++;
                        }
                        if (!TestOkFlag)
                            break;
                        break;
                    case clsGlobalVariables.pt100sensorAccuracyTest:
                        clsGlobalVariables.strgOngoingTestName = "PT100 Sensor Accuracy";
                        foreach (var actualTest in item.Value)
                        {
                            if (stopBtnPress)
                                break;

                            PT100SensorTest(actualTest, currentTestNumber, clsGlobalVariables.AccuracyParameter.PT100Sensor);

                            clsGlobalVariables.accuracyTests = clsGlobalVariables.Selectedcatid.PT100SensorTests;
                            clsGlobalVariables.listAccTest.Clear();
                            clsGlobalVariables.enmpointcalibration = (clsGlobalVariables.Enmpointcalibration)currentTestNumber;
                            TestOkFlag = false;
                            clsGlobalVariables.Validateaccuracytestbackcolor = true;
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {

                                if (DUT == 1)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT1Result, clsGlobalVariables.AccuracyParameter.PT100Sensor);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 2)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT2Result, clsGlobalVariables.AccuracyParameter.PT100Sensor);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 3)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT3Result, clsGlobalVariables.AccuracyParameter.PT100Sensor);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 4)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT4Result, clsGlobalVariables.AccuracyParameter.PT100Sensor);
                                    if (!TestOkFlag)
                                        break;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                            clsGlobalVariables.Validateaccuracytestbackcolor = false;
                            if (!TestOkFlag && DisplayMsg)
                            {
                                DisplayMsg = false;
                                DialogResult dlgMsgBxRslt = System.Windows.Forms.MessageBox.Show("Do you want to abort the test?", clsGlobalVariables.strg_Application, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                                if (dlgMsgBxRslt == DialogResult.Yes)
                                    break;
                            }

                            currentTestNumber++;
                        }
                        if (!TestOkFlag)
                            break;
                        break;
                    case clsGlobalVariables.RsensorAccuracyTest:
                        clsGlobalVariables.strgOngoingTestName = "R Sensor Accuracy";
                        foreach (var actualTest in item.Value)
                        {
                            if (stopBtnPress)
                                break;

                            RSensorText(actualTest, currentTestNumber, clsGlobalVariables.AccuracyParameter.RSensor);

                            clsGlobalVariables.accuracyTests = clsGlobalVariables.Selectedcatid.RSensor;
                            clsGlobalVariables.listAccTest.Clear();
                            clsGlobalVariables.enmpointcalibration = (clsGlobalVariables.Enmpointcalibration)currentTestNumber;
                            TestOkFlag = false;
                            clsGlobalVariables.Validateaccuracytestbackcolor = true;
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {

                                if (DUT == 1)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT1Result, clsGlobalVariables.AccuracyParameter.RSensor);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 2)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT2Result, clsGlobalVariables.AccuracyParameter.RSensor);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 3)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT3Result, clsGlobalVariables.AccuracyParameter.RSensor);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 4)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT4Result, clsGlobalVariables.AccuracyParameter.RSensor);
                                    if (!TestOkFlag)
                                        break;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                            clsGlobalVariables.Validateaccuracytestbackcolor = false;
                            if (!TestOkFlag && DisplayMsg)
                            {
                                DisplayMsg = false;
                                DialogResult dlgMsgBxRslt = System.Windows.Forms.MessageBox.Show("Do you want to abort the test?", clsGlobalVariables.strg_Application, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                                if (dlgMsgBxRslt == DialogResult.Yes)
                                    break;
                            }

                            currentTestNumber++;
                        }
                        if (!TestOkFlag)
                            break;
                        break;

                    case clsGlobalVariables.JsensorAccuracyTest:
                        clsGlobalVariables.strgOngoingTestName = "J Sensor Accuracy";
                        foreach (var actualTest in item.Value)
                        {
                            if (stopBtnPress)
                                break;

                            JSensorTest(actualTest, currentTestNumber, clsGlobalVariables.AccuracyParameter.JSensor);

                            clsGlobalVariables.accuracyTests = clsGlobalVariables.Selectedcatid.JSensor;
                            clsGlobalVariables.listAccTest.Clear();
                            clsGlobalVariables.enmpointcalibration = (clsGlobalVariables.Enmpointcalibration)currentTestNumber;
                            TestOkFlag = false;
                            clsGlobalVariables.Validateaccuracytestbackcolor = true;
                            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                            {

                                if (DUT == 1)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT1Result, clsGlobalVariables.AccuracyParameter.JSensor);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 2)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT2Result, clsGlobalVariables.AccuracyParameter.JSensor);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 3)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT3Result, clsGlobalVariables.AccuracyParameter.JSensor);
                                    if (!TestOkFlag)
                                        break;
                                }
                                else if (DUT == 4)
                                {
                                    TestOkFlag = UpdateTestResult(DUT, currentTestNumber, DUT4Result, clsGlobalVariables.AccuracyParameter.JSensor);
                                    if (!TestOkFlag)
                                        break;
                                }

                            }
                            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                            clsGlobalVariables.Validateaccuracytestbackcolor = false;
                            if (!TestOkFlag && DisplayMsg)
                            {
                                DisplayMsg = false;
                                DialogResult dlgMsgBxRslt = System.Windows.Forms.MessageBox.Show("Do you want to abort the test?", clsGlobalVariables.strg_Application, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                                if (dlgMsgBxRslt == DialogResult.Yes)
                                    break;
                            }

                            currentTestNumber++;
                        }
                        if (!TestOkFlag)
                            break;
                        break;
                    default:
                        break;
                }
                //if (!TestOkFlag)
                //{
                //    break;

                //}
            }
            if (TestOkFlag && !stopBtnPress)
            {
                
                //write calibration constant to all DUT
                byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                if (clsGlobalVariables.algTests_Auto.Contains("WRITE_CALIB_CONST_WITH_VREF"))
                {
                    btmRetVal = clsGlobalVariables.objTestJIGFunctions.TestDUT("WRITE_CALIB_CONST_WITH_VREF");
                }
                else
                {
                    btmRetVal = clsGlobalVariables.objTestJIGFunctions.TestDUT("WRITE_CALIB_CONST");
                }

                //write data of accuracy in sqlite

                AddAccuracyDataInDatabase(AccuracyList);

                clsLoggingData.addDataLog(clsGlobalVariables.objDataLog[0]);
                clsLoggingData.addDataLog(clsGlobalVariables.objDataLog[1]);
                clsLoggingData.addDataLog(clsGlobalVariables.objDataLog[2]);
                clsLoggingData.addDataLog(clsGlobalVariables.objDataLog[3]);
            }

            //start accuracy with user define point
            //write constant
            //write data log in sqlite.
            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);
            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.DUT_CALIB_COMPLETED);
            clsGlobalVariables.mainWindowVM.CloseAllComport();
            
            clsGlobalVariables.objGlobalFunction.PLC_ON_OFF_QUERY(false);
            StartStopWatch(false);

            clsGlobalVariables.AccuracyStopwatchTime = StopwatchTime;
            if (clsGlobalVariables.accuracyWindow != null)
            {
                clsGlobalVariables.accuracyWindow.Close();
            }            
                        
           // MainWindowVM.initilizeCommonObject.clsLoggingDatas[0] = clsLoggingData;
        }

        private void ClearData()
        {
            string bkColorwhite = "#fafafa";
            foreach (var item in AccuracymAmpTestsDetails)
            {
                item.TestresultDevice1 = "";
                item.TestresultDevice2 = "";
                item.TestresultDevice3 = "";
                item.TestresultDevice4 = "";
                item.TestresultDevice5 = "";
                item.TestresultDevice6 = "";

                item.BackcolorDevice1 = bkColorwhite;
                item.BackcolorDevice2 = bkColorwhite;
                item.BackcolorDevice3 = bkColorwhite;
                item.BackcolorDevice4 = bkColorwhite;
                item.BackcolorDevice5 = bkColorwhite;
                item.BackcolorDevice6 = bkColorwhite;

            }

            foreach (var item in AccuracyVoltTestsDetails)
            {
                item.TestresultDevice1 = "";
                item.TestresultDevice2 = "";
                item.TestresultDevice3 = "";
                item.TestresultDevice4 = "";
                item.TestresultDevice5 = "";
                item.TestresultDevice6 = "";

                item.BackcolorDevice1 = bkColorwhite;
                item.BackcolorDevice2 = bkColorwhite;
                item.BackcolorDevice3 = bkColorwhite;
                item.BackcolorDevice4 = bkColorwhite;
                item.BackcolorDevice5 = bkColorwhite;
                item.BackcolorDevice6 = bkColorwhite;
            }

            foreach (var item in AccuracyPT100SnsrTestsDetails)
            {
                item.TestresultDevice1 = "";
                item.TestresultDevice2 = "";
                item.TestresultDevice3 = "";
                item.TestresultDevice4 = "";
                item.TestresultDevice5 = "";
                item.TestresultDevice6 = "";

                item.BackcolorDevice1 = bkColorwhite;
                item.BackcolorDevice2 = bkColorwhite;
                item.BackcolorDevice3 = bkColorwhite;
                item.BackcolorDevice4 = bkColorwhite;
                item.BackcolorDevice5 = bkColorwhite;
                item.BackcolorDevice6 = bkColorwhite;
            }

            foreach (var item in AccuracyRSensorTestsDetails)
            {
                item.TestresultDevice1 = "";
                item.TestresultDevice2 = "";
                item.TestresultDevice3 = "";
                item.TestresultDevice4 = "";
                item.TestresultDevice5 = "";
                item.TestresultDevice6 = "";

                item.BackcolorDevice1 = bkColorwhite;
                item.BackcolorDevice2 = bkColorwhite;
                item.BackcolorDevice3 = bkColorwhite;
                item.BackcolorDevice4 = bkColorwhite;
                item.BackcolorDevice5 = bkColorwhite;
                item.BackcolorDevice6 = bkColorwhite;
            }

            foreach (var item in AccuracyJSensorTestsDetails)
            {
                item.TestresultDevice1 = "";
                item.TestresultDevice2 = "";
                item.TestresultDevice3 = "";
                item.TestresultDevice4 = "";
                item.TestresultDevice5 = "";
                item.TestresultDevice6 = "";

                item.BackcolorDevice1 = bkColorwhite;
                item.BackcolorDevice2 = bkColorwhite;
                item.BackcolorDevice3 = bkColorwhite;
                item.BackcolorDevice4 = bkColorwhite;
                item.BackcolorDevice5 = bkColorwhite;
                item.BackcolorDevice6 = bkColorwhite;
            }
        }

        private void AddAccuracyDataInDatabase(Dictionary<string, List<string>> AccuracyList)
        {
            clsGlobalVariables.objDataLog[0] = new clsLoggingData();
            clsGlobalVariables.objDataLog[1] = new clsLoggingData();
            clsGlobalVariables.objDataLog[2] = new clsLoggingData();
            clsGlobalVariables.objDataLog[3] = new clsLoggingData();
            foreach (var item in AccuracyList)
            {
                int currentTestNumber = 0;
                switch (item.Key)
                {
                    case clsGlobalVariables.mAmpAccuracyTest:
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            switch (DUT)
                            {
                                case 1:
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent1 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent2 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber + 1].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent3 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber + 2].TestresultDevice1);
                                    break;
                                case 2:
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent1 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent2 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber + 1].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent3 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber + 2].TestresultDevice2);
                                    break;
                                case 3:
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent1 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent2 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber + 1].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent3 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber + 2].TestresultDevice3);
                                    break;
                                case 4:
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent1 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent2 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber + 1].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].AnalogInputCurrent3 = Convert.ToDouble(AccuracymAmpTestsDetails[currentTestNumber + 2].TestresultDevice4);
                                    break;


                                default:
                                    break;
                            }

                        }
                        break;
                    case clsGlobalVariables.voltAccuracyTest:
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            switch (DUT)
                            {
                                case 1:
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage1 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage2 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber + 1].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage3 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber + 2].TestresultDevice1);
                                    break;                                 
                                case 2:                                    
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage1 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage2 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber + 1].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage3 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber + 2].TestresultDevice2);
                                    break;                                 
                                case 3:                                    
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage1 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage2 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber + 1].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage3 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber + 2].TestresultDevice3);
                                    break;                                 
                                case 4:                                    
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage1 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage2 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber + 1].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].OutputVoltage3 = Convert.ToDouble(AccuracyVoltTestsDetails[currentTestNumber + 2].TestresultDevice4);
                                    break;


                                default:
                                    break;
                            }

                        }
                        break;
                    case clsGlobalVariables.pt100sensorAccuracyTest:
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            switch (DUT)
                            {
                                case 1:
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp1 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp2 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber + 1].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp3 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber + 2].TestresultDevice1);
                                    break;                                 
                                case 2:                                    
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp1 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp2 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber + 1].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp3 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber + 2].TestresultDevice2);
                                    break;                                 
                                case 3:                                    
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp1 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp2 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber + 1].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp3 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber + 2].TestresultDevice3);
                                    break;                                 
                                case 4:                                    
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp1 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp2 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber + 1].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].PT100SensTemp3 = Convert.ToDouble(AccuracyPT100SnsrTestsDetails[currentTestNumber + 2].TestresultDevice4);
                                    break;


                                default:
                                    break;
                            }

                        }
                        break;
                    case clsGlobalVariables.RsensorAccuracyTest:
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            switch (DUT)
                            {
                                case 1:
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp1 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp2 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber + 1].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp3 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber + 2].TestresultDevice1);
                                    break;
                                case 2:                                    
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp1 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp2 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber + 1].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp3= Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber + 2].TestresultDevice2);
                                    break;                                 
                                case 3:                                    
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp1 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp2 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber + 1].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp3 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber + 2].TestresultDevice3);
                                    break;                                 
                                case 4:                                    
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp1 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp2 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber + 1].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].RSensTemp3 = Convert.ToDouble(AccuracyRSensorTestsDetails[currentTestNumber + 2].TestresultDevice4);
                                    break;


                                default:
                                    break;
                            }

                        }
                        break;
                    case clsGlobalVariables.JsensorAccuracyTest:
                        foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                        {
                            switch (DUT)
                            {
                                case 1:
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp1 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp2 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber + 1].TestresultDevice1);
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp3 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber + 2].TestresultDevice1);
                                    break;                                                    
                                case 2:                                                       
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp1 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp2 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber + 1].TestresultDevice2);
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp3 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber + 2].TestresultDevice2);
                                    break;                                                  
                                case 3:                                                     
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp1 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp2 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber + 1].TestresultDevice3);
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp3 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber + 2].TestresultDevice3);
                                    break;                                                    
                                case 4:                                                       
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp1 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp2 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber + 1].TestresultDevice4);
                                    clsGlobalVariables.objDataLog[DUT - 1].JSensTemp3 = Convert.ToDouble(AccuracyJSensorTestsDetails[currentTestNumber + 2].TestresultDevice4);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetAccuracyDataFromJSON(Dictionary<string, List<string>> AccuracyList)
        {
            foreach (var item in clsGlobalVariables.Selectedcatid.ListOfAccuracyTestsSequence)
            {
                switch (item)
                {
                    case clsGlobalVariables.mAmpAccuracyTest:
                        if (clsGlobalVariables.Selectedcatid.IsmAmpTestEnabled)
                        {
                            List<string> tempList = new List<string>();
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P1);
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P2);
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P3);
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P4);
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P5);
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P6);
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P7);
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P8);
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P9);
                            tempList.Add(clsGlobalVariables.Selectedcatid.mAmpTests[0].P10);

                            tempList.RemoveRange(Convert.ToInt32(clsGlobalVariables.Selectedcatid.mAmpTests[0].NumberTestPoints), (10 - Convert.ToInt32(clsGlobalVariables.Selectedcatid.mAmpTests[0].NumberTestPoints)));
                            AccuracyList.Add(item, tempList);
                        }
                        break;
                    case clsGlobalVariables.voltAccuracyTest:
                        if (clsGlobalVariables.Selectedcatid.IsVoltTestEnabled)
                        {
                            List<string> tempList = new List<string>();
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P1);
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P2);
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P3);
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P4);
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P5);
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P6);
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P7);
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P8);
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P9);
                            tempList.Add(clsGlobalVariables.Selectedcatid.VoltTests[0].P10);
                            tempList.RemoveRange(Convert.ToInt32(clsGlobalVariables.Selectedcatid.VoltTests[0].NumberTestPoints), (10 - Convert.ToInt32(clsGlobalVariables.Selectedcatid.VoltTests[0].NumberTestPoints)));
                            AccuracyList.Add(item, tempList);

                        }
                        break;
                    case clsGlobalVariables.pt100sensorAccuracyTest:
                        if (clsGlobalVariables.Selectedcatid.IsPT100SensorTestEnabled)
                        {
                            List<string> tempList = new List<string>();
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P1);
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P2);
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P3);
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P4);
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P5);
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P6);
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P7);
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P8);
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P9);
                            tempList.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].P10);
                            tempList.RemoveRange(Convert.ToInt32(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].NumberTestPoints), (10 - Convert.ToInt32(clsGlobalVariables.Selectedcatid.PT100SensorTests[0].NumberTestPoints)));
                            AccuracyList.Add(item, tempList);
                        }
                        break;
                    case clsGlobalVariables.RsensorAccuracyTest:
                        if (clsGlobalVariables.Selectedcatid.IsRSensorTestEnabled)
                        {
                            List<string> tempList = new List<string>();
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P1);
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P2);
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P3);
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P4);
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P5);
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P6);
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P7);
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P8);
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P9);
                            tempList.Add(clsGlobalVariables.Selectedcatid.RSensor[0].P10);
                            tempList.RemoveRange(Convert.ToInt32(clsGlobalVariables.Selectedcatid.RSensor[0].NumberTestPoints), (10 - Convert.ToInt32(clsGlobalVariables.Selectedcatid.RSensor[0].NumberTestPoints)));
                            AccuracyList.Add(item, tempList);
                        }
                        break;

                    case clsGlobalVariables.JsensorAccuracyTest:
                        if (clsGlobalVariables.Selectedcatid.IsJSensorTestEnabled)
                        {
                            List<string> tempList = new List<string>();
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P1);
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P2);
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P3);
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P4);
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P5);
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P6);
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P7);
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P8);
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P9);
                            tempList.Add(clsGlobalVariables.Selectedcatid.JSensor[0].P10);
                            tempList.RemoveRange(Convert.ToInt32(clsGlobalVariables.Selectedcatid.JSensor[0].NumberTestPoints), (10 - Convert.ToInt32(clsGlobalVariables.Selectedcatid.JSensor[0].NumberTestPoints)));
                            AccuracyList.Add(item, tempList);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private bool VoltSensorTest(string testPoint, int currentTestNumber, clsGlobalVariables.AccuracyParameter sensorType)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (currentTestNumber == 1)
            {
                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {
                    btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    
                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                            continue;
                        }

                }
                clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                clsMessages.DisplayMessage(clsMessageIDs.VOLT_CALIBRATION_MSG_ID);
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.mA_V_AccuracyDelay;
            }
                        
            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
            {
                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                if (currentTestNumber == 1)
                {
                   
                    btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    //This check is for device having modbus.
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_0_10V_TYPE_DOUBLE_ACTING, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_0_10V_TYPE, (byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                    }
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {

                        btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_ZERO, DUT);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            btmRetVal = SetISCH(clsGlobalVariables.TEN_Volt, DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                btmRetVal = SetISCL(clsGlobalVariables.ZERO_VOLT, DUT);
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_TWO, DUT);
                                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                    {

                                        btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_VOLT_KNOB_POS, clsGlobalVariables.SOURCE_VOLT_KNOB_POS, DUT);
                                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                                            continue;
                                        }
                                        btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.VTG_SENSOR_30V, DUT);
                                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                                            continue;
                                        }

                                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                                            continue;
                                        }

                                    }
                                }
                            }
                        }
                    }
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                }
                btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(testPoint, DUT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                    continue;
                }
            }
            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

            blnmDivideBy100 = true;
            btmRetVal = TestAccuracy(testPoint, currentTestNumber, clsGlobalVariables.mV_SENSOR, sensorType);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                return false;

            return true;
        }
        private bool RSensorText(string testPoint, int currentTestNumber, clsGlobalVariables.AccuracyParameter sensorType)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (currentTestNumber==1)
            {
                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    
                    clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.ThermoCouple_AccuracyDelay;
                }
                clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                    clsMessages.DisplayMessage(clsMessageIDs.TWO_WIRE_MSG_96x96);
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID);
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID_PI);
                else
                    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID_PI);
            }

            // btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.CJC_ON_OFF, clsGlobalVariables.CJC_ON);



            
            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
            {
                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                if (currentTestNumber == 1)
                {
                    
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_TC_KNOB_POS, clsGlobalVariables.SOURCE_TC_KNOB_POS, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.R_SENSOR, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                    {
                        System.Windows.Forms.MessageBox.Show("Please turn on the cjc.....of calibrator"+ DUT.ToString() +".");
                    }
                    else
                        System.Windows.Forms.MessageBox.Show("Please turn off the cjc.....of all calibrator" + DUT.ToString() + ".");

                    //
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_R_TYPE, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    //btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.CJC_ON_OFF, clsGlobalVariables.CJC_ON,DUT);
                    if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI )
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.CJC_ON_OFF, clsGlobalVariables.CJC_OFF);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                            continue;
                        }
                    }
                    
                    //This check is for device having modbus.                        
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_R_TYPE_DOUBLE_ACTING, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_R_TYPE, (byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                    }
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    
                }
            }
            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
            //clsMessages.ShowMessageInProgressWindowForAccuracy(clsMessageIDs.ACCURACY_R, testPoint + "°C.");
            blnmDivideBy100 = false;
            btmRetVal = TestAccuracy(testPoint, currentTestNumber, clsGlobalVariables.R_SENSOR, sensorType);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                return false;

            return true;
        }
        private bool JSensorTest(string testPoint, int currentTestNumber, clsGlobalVariables.AccuracyParameter sensorType)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            
            if (currentTestNumber == 1)
            {
                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                }
                clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                //if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                //    clsMessages.DisplayMessage(clsMessageIDs.TWO_WIRE_MSG_96x96);
                //else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                //    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID);
                //else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                //    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID_PI);
                //else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_48x48)
                //    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID_PI);
                //else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_96x96)
                //    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID_PI);

                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.ThermoCouple_AccuracyDelay;
            }
            
            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
            {
                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                if (currentTestNumber == 1)
                {
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_TC_KNOB_POS, clsGlobalVariables.SOURCE_TC_KNOB_POS, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.J_SENSOR, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }

                    btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_J_TYPE, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    //This check is for device having modbus.                        
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_J_TYPE_DOUBLE_ACTING, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_J_TYPE, (byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                    }

                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                }
            }
            clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
            clsMessages.ShowMessageInProgressWindowForAccuracy(clsMessageIDs.ACCURACY_J, testPoint + "°C.");
            blnmDivideBy100 = false;
            btmRetVal = TestAccuracy(testPoint, currentTestNumber, clsGlobalVariables.J_SENSOR, sensorType);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                return false;
            }
            return true;
        }
        private bool mAmpSensorTest(string testPoint, int currentTestNumber, clsGlobalVariables.AccuracyParameter sensorType)
        {

            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (currentTestNumber==1)
            {
                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                }
                clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                clsMessages.DisplayMessage(clsMessageIDs.MA_CALIBRATION_MSG_ID);
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.mA_V_AccuracyDelay;
            }
            // If test start and currentTestNumber is 1, then displaying messages, 
            // change settings of calibrator and DUT. This is one time, at starting.
            if (currentTestNumber == 1)
            {
                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {

                    btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_20mA_TYPE, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    //This check is for device having modbus.                        
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_0_20mA_TYPE_DOUBLE_ACTING, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_0_20mA_TYPE, (byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                    }
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_ZERO, DUT);
                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        btmRetVal = SetISCH(clsGlobalVariables.TWENTY_mAMP, DUT);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            if (clsGlobalVariables.selectedDeviceType==clsGlobalVariables.SelectedDeviceType.PI)
                                btmRetVal = SetISCL(clsGlobalVariables.ZERO_mAMP, DUT);
                            else
                                btmRetVal = SetISCL(clsGlobalVariables.FOUR_mAMP, DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_TWO, DUT);
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_POS, DUT);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                                        continue;
                                    }

                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(clsGlobalVariables.mA_SENSOR, DUT);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                                        continue;
                                    }
                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                                }
                                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                                    continue;
                                }

                            }

                        }
                    }
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }

                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(testPoint, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                }
                clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
            }
            blnmDivideBy100 = true;
            btmRetVal = TestAccuracy(testPoint, currentTestNumber, clsGlobalVariables.mV_SENSOR, sensorType);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                return false;
            }
            return true;
        }
        private bool PT100SensorTest(string testPoint, int currentTestNumber, clsGlobalVariables.AccuracyParameter sensorType)
        {
            
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            
            if (currentTestNumber == 1)
            {
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.PT100_AccuracyDelay;
                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {

                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                }
                clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                {
                    clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_96x96);
                }
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                {
                    clsMessages.DisplayMessage(clsMessageIDs.THREEWIRE_MSG_ID);
                }
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                {
                    clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_PI);
                }
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_48x48)
                {
                    clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_PI);
                }
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_96x96)
                {
                    clsMessages.DisplayMessage(clsMessageIDs.ALL_WIRE_MSG_PI);
                }

                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {
                    btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    //Make zero

                    btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceSetPosition(clsGlobalVariables.SOURCE_RTD_KNOB_POS_ACC, clsGlobalVariables.SOURCE_RTD_KNOB_POS_ACC, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }

                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_PT100_TYPE, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    //This check is for device having modbus.                        
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_PT100_TYPE_DOUBLE_ACTING, (byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_PT100_TYPE, (byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                    }
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                    btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_ZERO, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                }
                clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();

            }

           

           
            clsMessages.ShowMessageInProgressWindowForAccuracy(clsMessageIDs.ACCURACY_PT100, testPoint + "°C.");
            blnmDivideBy100 = false;
            btmRetVal = TestAccuracy(testPoint, currentTestNumber, clsGlobalVariables.PT100_SENSOR, sensorType);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                return false;
            }
            return true;
        }
        private byte TestAccuracy(string strmValue, int currentTestNumber, byte btmSensor, clsGlobalVariables.AccuracyParameter sensorType)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                //Send accuracy pt. to calibrator
                foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                {
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistanceZeroTemp(strmValue, DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.FAIL, sensorType);
                        continue;
                    }
                }
                clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                //Start timer here, time is given in database
                EnablePVTimeoutTimer();
                blnTimerElapsed = true;

                //Get actual data from DUT's(read data) until press next button.  
                while (blnTimerElapsed && btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                    {
                        byte btmData;
                        float flmData;
                        try
                        {
                            //This check is for device having modbus.                
                            if (clsModelSettings.blnRS485Flag == true)
                            {
                                btmData = clsGlobalVariables.objQueriescls.ReadPVDoubleActing((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT));
                            }
                            else//Device without modbus
                            {
                                btmData = clsGlobalVariables.objQueriescls.ReadPVSingleActing((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT));
                            }

                            if (btmData == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                if (blnmDivideBy100 == true)
                                {
                                    flmData = ((float)clsGlobalVariables.shrtgPV / 100);
                                    UpdateTestResult(DUT, currentTestNumber, flmData.ToString(), sensorType);
                                    if (DUT == 1)
                                        DUT1Result = flmData.ToString();
                                    else if (DUT == 2)
                                        DUT2Result = flmData.ToString();
                                    else if (DUT == 3)
                                        DUT3Result = flmData.ToString();
                                    else if (DUT == 4)
                                        DUT4Result = flmData.ToString();
                                   
                                }
                                else
                                {
                                    UpdateTestResult(DUT, currentTestNumber, clsGlobalVariables.shrtgPV.ToString(), sensorType);
                                    if (DUT == 1)
                                    {
                                        DUT1Result = clsGlobalVariables.shrtgPV.ToString();
                                        
                                    }

                                    else if (DUT == 2)
                                        DUT2Result = clsGlobalVariables.shrtgPV.ToString();
                                    else if (DUT == 3)
                                        DUT3Result = clsGlobalVariables.shrtgPV.ToString();
                                    else if (DUT == 4)
                                        DUT4Result = clsGlobalVariables.shrtgPV.ToString();
                                    
                                }
                            }
                        }
                        catch (Exception)
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                    clsGlobalVariables.objGlobalFunction.RemoveFailedDUT();
                }

                 return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        private byte ChangeDP(byte btmdata,byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                //This check is for device having modbus.                
                if (clsModelSettings.blnRS485Flag == true)
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.DP_SET, btmdata);
                }
                else//Device without modbus
                {
                    if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_48x48 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_96x96)
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Success;
                    }
                    else
                    {
                        int imResultData;
                        imResultData = ((btmdata * 0x100) | clsGlobalVariables.DP_VAL);
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT), clsGlobalVariables.SET_WRITE_FUNC_CODE, imResultData);
                       
                    }
                    
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                return btmRetVal;
            }
        }
        private byte SetISCH(byte btmdata,byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                {
                    if (clsGlobalVariables.TWENTY_mAMP == btmdata)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SET_AIRH, clsGlobalVariables.TWENTY_mAMP_PI);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            return btmRetVal;
                        }
                    }
                    else if (clsGlobalVariables.TEN_Volt == btmdata)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SET_AIRH, clsGlobalVariables.TEN_Volt_PI);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            return btmRetVal;
                        }
                    }

                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SET_ISCH, btmdata);
                }
                else
                {
                    //This check is for device having modbus.                
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.ISCH_SET, btmdata);
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SET_ISCH, btmdata);
                    }
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private byte SetISCL(byte btmdata,byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                {
                    if (clsGlobalVariables.ZERO_mAMP == btmdata || clsGlobalVariables.ZERO_VOLT == btmdata)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SET_AIRL, clsGlobalVariables.ZERO_mAMP);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            return btmRetVal;
                        }
                    }
                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SET_ISCL, btmdata);
                }
                else
                {
                    //This check is for device having modbus.                
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg((byte)(clsGlobalVariables.MB_DUT_ID_WM_BASE + DUT), clsGlobalVariables.ISCL_SET, btmdata);
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SET_ISCL, btmdata);
                    }
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void NextAccuracyTestClk(object obj)
        {
            blnTimerElapsed = false;
        }
        private void Stoptesting(object obj)
        {
            stopBtnPress = true;
            StartBtnVis = true;
            StopBtnVis = false;
            StartStopWatch(false);
        }

        private void FillTotalNumberOfPointsDetails(AccuracyTests objAccuracyTests, clsGlobalVariables.AccuracyParameter accuracyParameter)
        {
            int numberofpoints = Convert.ToInt32(objAccuracyTests.NumberTestPoints);

            Dispatcher.CurrentDispatcher.Invoke(delegate
            {
                AddAccuracyTests(numberofpoints, objAccuracyTests, accuracyParameter);
            });

        }

        private void AddAccuracyTests(int numberofpoints, AccuracyTests objAccuracyTests, clsGlobalVariables.AccuracyParameter accuracyParameter)
        {
            switch (numberofpoints)
            {
                case 1:
                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, AccuracyParameter = "mAmp", TestPoint = objAccuracyTests.P1, TestresultDevice1 = "10.10" });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, AccuracyParameter = "Volt", TestPoint = objAccuracyTests.P1 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, AccuracyParameter = "PT100", TestPoint = objAccuracyTests.P1 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, AccuracyParameter = "RSensor", TestPoint = objAccuracyTests.P1 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, AccuracyParameter = "JSensor", TestPoint = objAccuracyTests.P1 });
                            break;
                        default:
                            break;
                    }

                    break;
                case 2:
                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            break;
                        default:
                            break;
                    }

                    break;

                case 3:
                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            break;
                        default:
                            break;
                    }

                    break;

                case 4:
                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            break;
                        default:
                            break;
                    }

                    break;

                case 5:

                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            break;
                        default:
                            break;
                    }

                    break;

                case 6:

                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            break;
                        default:
                            break;
                    }

                    break;

                case 7:

                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            break;
                        default:
                            break;
                    }

                    break;

                case 8:
                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            break;
                        default:
                            break;
                    }

                    break;

                case 9:

                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            break;

                        default:
                            break;
                    }

                    break;

                case 10:

                    switch (accuracyParameter)
                    {
                        case clsGlobalVariables.AccuracyParameter.mAmp:
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 10, TestPoint = objAccuracyTests.P10 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.Volt:
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            AccuracyVoltTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 10, TestPoint = objAccuracyTests.P10 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            AccuracyPT100SnsrTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 10, TestPoint = objAccuracyTests.P10 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.RSensor:
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            AccuracyRSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 10, TestPoint = objAccuracyTests.P10 });
                            break;
                        case clsGlobalVariables.AccuracyParameter.JSensor:
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, TestPoint = objAccuracyTests.P1 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 2, TestPoint = objAccuracyTests.P2 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 3, TestPoint = objAccuracyTests.P3 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 4, TestPoint = objAccuracyTests.P4 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 5, TestPoint = objAccuracyTests.P5 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 6, TestPoint = objAccuracyTests.P6 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 7, TestPoint = objAccuracyTests.P7 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 8, TestPoint = objAccuracyTests.P8 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 9, TestPoint = objAccuracyTests.P9 });
                            AccuracyJSensorTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 10, TestPoint = objAccuracyTests.P10 });
                            break;

                        default:
                            break;
                    }

                    break;

                default:
                    break;
            }

        }

        private void EnableDUT()
        {
            switch (clsGlobalVariables.NUMBER_OF_DUTS)
            {

                case 0:
                    DeviceNumber1Vis = false;
                    DeviceNumber2Vis = false;
                    DeviceNumber3Vis = false;
                    DeviceNumber4Vis = false;
                    DeviceNumber5Vis = false;
                    DeviceNumber6Vis = false;
                    break;
                case 1:
                    DeviceNumber1Vis = true;
                    DeviceNumber2Vis = false;
                    DeviceNumber3Vis = false;
                    DeviceNumber4Vis = false;
                    DeviceNumber5Vis = false;
                    DeviceNumber6Vis = false;
                    break;
                case 2:
                    DeviceNumber1Vis = true;
                    DeviceNumber2Vis = true;
                    DeviceNumber3Vis = false;
                    DeviceNumber4Vis = false;
                    DeviceNumber5Vis = false;
                    DeviceNumber6Vis = false;
                    break;
                case 3:
                    DeviceNumber1Vis = true;
                    DeviceNumber2Vis = true;
                    DeviceNumber3Vis = true;
                    DeviceNumber4Vis = false;
                    DeviceNumber5Vis = false;
                    DeviceNumber6Vis = false;
                    break;
                case 4:
                    DeviceNumber1Vis = true;
                    DeviceNumber2Vis = true;
                    DeviceNumber3Vis = true;
                    DeviceNumber4Vis = true;
                    DeviceNumber5Vis = false;
                    DeviceNumber6Vis = false;
                    break;
                case 5:
                    DeviceNumber1Vis = true;
                    DeviceNumber2Vis = true;
                    DeviceNumber3Vis = true;
                    DeviceNumber4Vis = true;
                    DeviceNumber5Vis = true;
                    DeviceNumber6Vis = false;
                    break;
                case 6:
                    DeviceNumber1Vis = true;
                    DeviceNumber2Vis = true;
                    DeviceNumber3Vis = true;
                    DeviceNumber4Vis = true;
                    DeviceNumber5Vis = true;
                    DeviceNumber6Vis = true;
                    break;

                default:
                    DeviceNumber1Vis = false;
                    DeviceNumber2Vis = false;
                    DeviceNumber3Vis = false;
                    DeviceNumber4Vis = false;
                    DeviceNumber5Vis = false;
                    DeviceNumber6Vis = false;
                    break;
            }
        }
        #region TimerRegion
        DispatcherTimer tmrPVTimerTimeout = new DispatcherTimer();
        private void EnablePVTimeoutTimer()
        {
            // MyLogWriterDLL.LogWriter.WriteLog("EnablePVTimeoutTimer : " + clsGlobalVariables.igPV_TIMEOUT_DELAY.ToString());
            tmrPVTimerTimeout.Stop();
            tmrPVTimerTimeout.Interval = new TimeSpan(0, 0, 0, 0, clsGlobalVariables.igPV_TIMEOUT_DELAY);
            tmrPVTimerTimeout.Start();
        }

        private void TmrPVTimerTimeout_Tick(object sender, EventArgs e)
        {
            blnTimerElapsed = false;
        }

        #endregion
        #region"UI Related code"

        //Stopwatch purpose : Calculate time required for all calibration and programming
        Stopwatch objstpWatch = new Stopwatch(); //Added for Calibration Timer
        public System.Threading.TimerCallback tmrCallback;
        public System.Threading.Timer tmrMbTimer;
        private RelayCommand _StartAccuracyTesting;
        private RelayCommand _StopAccuracyTesting;
        private RelayCommand _NextAccuracyTesting;

        public bool blnmDivideBy100 = false;
        public bool blnTimerElapsed = false;

        private ObservableCollection<clsAccuracyTestsDevices> _AccuracymAmpTestsDetails;

        public ObservableCollection<clsAccuracyTestsDevices> AccuracymAmpTestsDetails
        {
            get { return _AccuracymAmpTestsDetails; }
            set { _AccuracymAmpTestsDetails = value; OnPropertyChanged("AccuracymAmpTestsDetails"); }
        }

        private ObservableCollection<clsAccuracyTestsDevices> _AccuracyVoltTestsDetails;

        public ObservableCollection<clsAccuracyTestsDevices> AccuracyVoltTestsDetails
        {
            get { return _AccuracyVoltTestsDetails; }
            set { _AccuracyVoltTestsDetails = value; OnPropertyChanged("AccuracyVoltTestsDetails"); }
        }

        private ObservableCollection<clsAccuracyTestsDevices> _AccuracyPT100SnsrTestsDetails;

        public ObservableCollection<clsAccuracyTestsDevices> AccuracyPT100SnsrTestsDetails
        {
            get { return _AccuracyPT100SnsrTestsDetails; }
            set { _AccuracyPT100SnsrTestsDetails = value; OnPropertyChanged("AccuracyPT100SnsrTestsDetails"); }
        }

        private ObservableCollection<clsAccuracyTestsDevices> _AccuracyRSensorTestsDetails;

        public ObservableCollection<clsAccuracyTestsDevices> AccuracyRSensorTestsDetails
        {
            get { return _AccuracyRSensorTestsDetails; }
            set { _AccuracyRSensorTestsDetails = value; OnPropertyChanged("AccuracyRSensorTestsDetails"); }
        }

        private ObservableCollection<clsAccuracyTestsDevices> _AccuracyJSensorTestsDetails;

        public ObservableCollection<clsAccuracyTestsDevices> AccuracyJSensorTestsDetails
        {
            get { return _AccuracyJSensorTestsDetails; }
            set { _AccuracyJSensorTestsDetails = value; OnPropertyChanged("AccuracyJSensorTestsDetails"); }
        }

        private bool _StartBtnVis;

        public bool StartBtnVis
        {
            get { return _StartBtnVis; }
            set { _StartBtnVis = value; OnPropertyChanged("StartBtnVis"); }
        }

        private bool _StopBtnVis;

        public bool StopBtnVis
        {
            get { return _StopBtnVis; }
            set { _StopBtnVis = value; OnPropertyChanged("StopBtnVis"); }
        }

        private bool _IsStartEnable;

        public bool IsStartEnable
        {
            get { return _IsStartEnable; }
            set { _IsStartEnable = value; OnPropertyChanged("IsStartEnable"); }
        }

        private bool _IsStopEnable;

        public bool IsStopEnable
        {
            get { return _IsStopEnable; }
            set { _IsStopEnable = value; OnPropertyChanged("IsStopEnable"); }
        }


        private string _StopwatchTime;

        public string StopwatchTime
        {
            get { return _StopwatchTime; }
            set { _StopwatchTime = value; OnPropertyChanged("StopwatchTime"); }
        }


        private bool _IsmAmpVis;

        public bool IsmAmpVis
        {
            get { return _IsmAmpVis; }
            set { _IsmAmpVis = value; OnPropertyChanged("IsmAmpVis"); }
        }

        private bool _IsVoltVis;

        public bool IsVoltVis
        {
            get { return _IsVoltVis; }
            set { _IsVoltVis = value; OnPropertyChanged("IsVoltVis"); }
        }

        private bool _IsPT100SensorVis;

        public bool IsPT100SensorVis
        {
            get { return _IsPT100SensorVis; }
            set { _IsPT100SensorVis = value; OnPropertyChanged("IsPT100SensorVis"); }
        }

        private bool _IsRSensorVis;

        public bool IsRSensorVis
        {
            get { return _IsRSensorVis; }
            set { _IsRSensorVis = value; OnPropertyChanged("IsRSensorVis"); }
        }

        private bool _IsJSensorVis;

        public bool IsJSensorVis
        {
            get { return _IsJSensorVis; }
            set { _IsJSensorVis = value; OnPropertyChanged("IsJSensorVis"); }
        }

        private bool _DeviceNumber1Vis;

        public bool DeviceNumber1Vis
        {
            get { return _DeviceNumber1Vis; }
            set { _DeviceNumber1Vis = value; OnPropertyChanged("DeviceNumber1Vis"); }
        }

        private bool _DeviceNumber2Vis;

        public bool DeviceNumber2Vis
        {
            get { return _DeviceNumber2Vis; }
            set { _DeviceNumber2Vis = value; OnPropertyChanged("DeviceNumber2Vis"); }
        }
        private bool _DeviceNumber3Vis;

        public bool DeviceNumber3Vis
        {
            get { return _DeviceNumber3Vis; }
            set { _DeviceNumber3Vis = value; OnPropertyChanged("DeviceNumber3Vis"); }
        }
        private bool _DeviceNumber4Vis;

        public bool DeviceNumber4Vis
        {
            get { return _DeviceNumber4Vis; }
            set { _DeviceNumber4Vis = value; OnPropertyChanged("DeviceNumber4Vis"); }
        }
        private bool _DeviceNumber5Vis;

        public bool DeviceNumber5Vis
        {
            get { return _DeviceNumber5Vis; }
            set { _DeviceNumber5Vis = value; OnPropertyChanged("DeviceNumber5Vis"); }
        }
        private bool _DeviceNumber6Vis;

        public bool DeviceNumber6Vis
        {
            get { return _DeviceNumber6Vis; }
            set { _DeviceNumber6Vis = value; OnPropertyChanged("DeviceNumber6Vis"); }
        }

        public RelayCommand StartAccuracyTesting
        {
            get { return _StartAccuracyTesting; }
            set { _StartAccuracyTesting = value; }
        }


        public RelayCommand StopAccuracyTesting
        {
            get { return _StopAccuracyTesting; }
            set { _StopAccuracyTesting = value; }
        }

        public RelayCommand NextAccuracyTesting
        {
            get { return _NextAccuracyTesting; }
            set { _NextAccuracyTesting = value; }
        }
        public AccuracyWindowVM()
        {
            StartBtnVis = true;
            StopBtnVis = false;
            _StartAccuracyTesting = new RelayCommand(btnStartClk);
            tmrPVTimerTimeout.Tick += TmrPVTimerTimeout_Tick;

            _StopAccuracyTesting = new RelayCommand(Stoptesting);
            _NextAccuracyTesting = new RelayCommand(NextAccuracyTestClk);

            _AccuracymAmpTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();
            _AccuracyVoltTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();
            _AccuracyPT100SnsrTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();
            _AccuracyRSensorTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();
            _AccuracyJSensorTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();

            EnableDUT();

            if (clsGlobalVariables.Selectedcatid != null)
            {
                if (clsGlobalVariables.Selectedcatid.IsmAmpTestEnabled)
                {
                    IsmAmpVis = true;

                    FillTotalNumberOfPointsDetails(clsGlobalVariables.Selectedcatid.mAmpTests[0], clsGlobalVariables.AccuracyParameter.mAmp);

                }
                else
                    IsmAmpVis = false;


                if (clsGlobalVariables.Selectedcatid.IsVoltTestEnabled)
                {
                    IsVoltVis = true;

                    FillTotalNumberOfPointsDetails(clsGlobalVariables.Selectedcatid.VoltTests[0], clsGlobalVariables.AccuracyParameter.Volt);

                }
                else
                    IsVoltVis = false;

                if (clsGlobalVariables.Selectedcatid.IsPT100SensorTestEnabled)
                {
                    IsPT100SensorVis = true;

                    FillTotalNumberOfPointsDetails(clsGlobalVariables.Selectedcatid.PT100SensorTests[0], clsGlobalVariables.AccuracyParameter.PT100Sensor);

                }
                else
                    IsPT100SensorVis = false;

                if (clsGlobalVariables.Selectedcatid.IsRSensorTestEnabled)
                {
                    IsRSensorVis = true;

                    FillTotalNumberOfPointsDetails(clsGlobalVariables.Selectedcatid.RSensor[0], clsGlobalVariables.AccuracyParameter.RSensor);

                }
                else
                    IsRSensorVis = false;

                if (clsGlobalVariables.Selectedcatid.IsJSensorTestEnabled)
                {
                    IsJSensorVis = true;

                    FillTotalNumberOfPointsDetails(clsGlobalVariables.Selectedcatid.JSensor[0], clsGlobalVariables.AccuracyParameter.JSensor);

                }
                else
                    IsJSensorVis = false;


                if (IsmAmpVis == false && IsVoltVis == false & IsPT100SensorVis == false && IsRSensorVis == false && IsJSensorVis == false)
                {
                    IsStartEnable = false;
                    IsStopEnable = false;
                }
                else
                {
                    IsStartEnable = true;
                    IsStopEnable = true;
                }
            }
        }

        /// <summary>
        /// StartStopWatch()
        /// This function is used to start stopwatch to display time on UI.
        /// Stopwatch is initialised with value 00:00:00
        /// </summary>
        private void StartStopWatch(bool state)
        {
            StartBtnVis = !state;
            StopBtnVis = state;

            if (state == true)
            {
                objstpWatch.Reset();
                objstpWatch.Start();
                tmrCallback = new System.Threading.TimerCallback(MbTimerIntr);
                tmrMbTimer = new System.Threading.Timer(tmrCallback, null, 200, 200);
                tmrMbTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);// Stop timer 
                tmrMbTimer.Change(1000, System.Threading.Timeout.Infinite); // 1sec timer is started here.
                StopwatchTime = "00:00:00";
            }
            else
                tmrMbTimer.Dispose();

        }


        /// <summary>
        /// MbTimerIntr()
        /// This function is used to start timer thread 
        /// TimeSpan represent time interval in seconds
        /// </summary>
        /// <param name="obj"></param>
        private void MbTimerIntr(object obj)
        {
            try
            {
                //CheckForIllegalCrossThreadCalls = false;
                tmrMbTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);// Stop inter packet timer 
                TimeSpan objtmSpn = objstpWatch.Elapsed;
                // Format and display the TimeSpan value.
                StopwatchTime = String.Format("{0:00}:{1:00}:{2:00}", objtmSpn.Hours, objtmSpn.Minutes, objtmSpn.Seconds);

                tmrMbTimer.Change(1000, System.Threading.Timeout.Infinite); // Start inter packet timer 
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("In Timer Thread");
                MyLogWriterDLL.LogWriter.WriteLog("ex.Message , ex.StackTrace" + ex.Message + "," + ex.StackTrace);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        public bool UpdateTestResult(int DUTNumber, int testnumber, string result, clsGlobalVariables.AccuracyParameter accuracyParameter)
        {
            if (result ==clsGlobalVariables.FAIL)                          
                clsGlobalVariables.NUMBER_OF_FAIL_DUTS_List.Add((byte)DUTNumber);
                            
            switch (accuracyParameter)
            {
                case clsGlobalVariables.AccuracyParameter.mAmp:
                    foreach (clsAccuracyTestsDevices item in AccuracymAmpTestsDetails)
                    {
                        if (item.Testnumber == testnumber)
                        {
                            clsAccuracyTestsDevices obj = AccuracymAmpTestsDetails[item.Testnumber - 1];
                            string TestPoint = obj.TestPoint;

                            switch (DUTNumber)
                            {
                                case 1:
                                    AccuracymAmpTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = result, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 2:
                                    AccuracymAmpTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 3:
                                    AccuracymAmpTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = result, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 4:
                                    AccuracymAmpTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = result, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 5:
                                    AccuracymAmpTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = result, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 6:
                                    AccuracymAmpTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = result };
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    }
                    break;
                case clsGlobalVariables.AccuracyParameter.Volt:
                    foreach (clsAccuracyTestsDevices item in AccuracyVoltTestsDetails)
                    {
                        if (item.Testnumber == testnumber)
                        {
                            clsAccuracyTestsDevices obj = AccuracyVoltTestsDetails[item.Testnumber - 1];
                            string TestPoint = obj.TestPoint;

                            switch (DUTNumber)
                            {
                                case 1:
                                    AccuracyVoltTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = result, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 2:
                                    AccuracyVoltTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 3:
                                    AccuracyVoltTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = result, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 4:
                                    AccuracyVoltTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = result, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 5:
                                    AccuracyVoltTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = result, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 6:
                                    AccuracyVoltTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = result };
                                    break;
                                default:
                                    break;
                            }

                            break;
                        }
                    }
                    break;
                case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                    foreach (clsAccuracyTestsDevices item in AccuracyPT100SnsrTestsDetails)
                    {
                        if (item.Testnumber == testnumber)
                        {
                            clsAccuracyTestsDevices obj = AccuracyPT100SnsrTestsDetails[item.Testnumber - 1];
                            string TestPoint = obj.TestPoint;

                            switch (DUTNumber)
                            {
                                case 1:
                                    AccuracyPT100SnsrTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = result, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 2:
                                    AccuracyPT100SnsrTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 3:
                                    AccuracyPT100SnsrTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = result, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 4:
                                    AccuracyPT100SnsrTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = result, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 5:
                                    AccuracyPT100SnsrTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = result, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 6:
                                    AccuracyPT100SnsrTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = result };
                                    break;
                                default:
                                    break;
                            }

                            break;
                        }
                    }
                    break;
                case clsGlobalVariables.AccuracyParameter.RSensor:
                    foreach (clsAccuracyTestsDevices item in AccuracyRSensorTestsDetails)
                    {
                        if (item.Testnumber == testnumber)
                        {
                            clsAccuracyTestsDevices obj = AccuracyRSensorTestsDetails[item.Testnumber - 1];
                            string TestPoint = obj.TestPoint;


                            switch (DUTNumber)
                            {
                                case 1:
                                    AccuracyRSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = result, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 2:
                                    AccuracyRSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 3:
                                    AccuracyRSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = result, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 4:
                                    AccuracyRSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = result, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 5:
                                    AccuracyRSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = result, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 6:
                                    AccuracyRSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = result };
                                    break;
                                default:
                                    break;
                            }

                            break;
                        }
                    }
                    break;
                case clsGlobalVariables.AccuracyParameter.JSensor:
                    foreach (clsAccuracyTestsDevices item in AccuracyJSensorTestsDetails)
                    {
                        if (item.Testnumber == testnumber)
                        {
                            clsAccuracyTestsDevices obj = AccuracyJSensorTestsDetails[item.Testnumber - 1];
                            string TestPoint = obj.TestPoint;

                            switch (DUTNumber)
                            {
                                case 1:
                                    AccuracyJSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = result, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 2:
                                    AccuracyJSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 3:
                                    AccuracyJSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = result, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 4:
                                    AccuracyJSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = result, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 5:
                                    AccuracyJSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = result, TestresultDevice6 = obj.TestresultDevice6 };
                                    break;
                                case 6:
                                    AccuracyJSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1 = obj.TestresultDevice1, TestresultDevice2 = obj.TestresultDevice2, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = result };
                                    break;
                                default:
                                    break;
                            }

                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            if (clsGlobalVariables.listAccTest.Contains(false) == true)
            {
                clsGlobalVariables.listAccTest.Clear();
                return false;
            }
               
            return true;
        }
        #endregion
    }
}
