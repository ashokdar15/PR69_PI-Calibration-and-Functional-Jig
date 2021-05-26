
using Newtonsoft.Json;
using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using PR69_PI_Calibration_and_Functional_Jig.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Windows;
using System.Windows.Threading;
using static PR69_PI_Calibration_and_Functional_Jig.HelperClasses.clsGlobalVariables;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{

    public class MainWindowVM : INotifyPropertyChanged
    {
        public static InitilizeCommonObject initilizeCommonObject = new InitilizeCommonObject();
        //Constructor
        private async void btnStartClk(object obj)
        {

            CatIdList catId = clsGlobalVariables.Selectedcatid;
            //Port detection.
            //IsProcessOn = true;
            StartStopWatch(true);
            EnableDisableUI(false);
            objDataLog[0] = new clsDataLog();
            objDataLog[1] = new clsDataLog();
            objDataLog[2] = new clsDataLog();
            objDataLog[3] = new clsDataLog();
            //clsModelSettings.igDutID  need to set deive iD

            //clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID);
            //clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(clsMessages.objResManager.GetString("TWOWIRE_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + clsMessages.objResManager.GetString("TWOWIRE_MSG_ID2", clsGlobalVariables.objCultureinfo);

            if (ListOfTests.Count == 0)
            {
                GetListOfAllEnabledtests(catId);
            }

            CurrenttstgrpDUT1 = catId.ListOfGroupSequence[0];
            CurrentTestStatusDUT1 = ListOfTests[0];

            CurrenttstgrpDUT2 = catId.ListOfGroupSequence[1];
            CurrentTestStatusDUT2 = ListOfTests[0];

            CurrenttstgrpDUT3 = catId.ListOfGroupSequence[2];
            CurrentTestStatusDUT3 = ListOfTests[0];

            CurrenttstgrpDUT4 = catId.ListOfGroupSequence[0];
            CurrentTestStatusDUT4 = ListOfTests[0];

            clsTotalTestsGroups.Clear();
            int count = 0;

            clsGlobalVariables.algTests_Auto.Clear();
            //    //PIB12C

            #region "PI"
            //clsGlobalVariables.algTests_Auto.Add("READ_DEVICE_ID");
            //clsGlobalVariables.algTests_Auto.Add("READ_CALIB_CONST_STATUS");
            //clsGlobalVariables.algTests_Auto.Add("SWITCH_SENSOR_RELAY");
            //clsGlobalVariables.algTests_Auto.Add("START_DISP_TEST");
            //clsGlobalVariables.algTests_Auto.Add("START_KEYPAD_TEST");
            //clsGlobalVariables.algTests_Auto.Add("START_REL_TEST_OP1_RELAY_PR43_PI");
            //clsGlobalVariables.algTests_Auto.Add("START_REL_TEST_OP2_RELAY_PI");
            //clsGlobalVariables.algTests_Auto.Add("24V_OP_TEST");
            //clsGlobalVariables.algTests_Auto.Add("START_MODBUS_TEST");
            //clsGlobalVariables.algTests_Auto.Add("CJC_TEST");
            //clsGlobalVariables.algTests_Auto.Add("SET_DFALT_1MA_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_1MA_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_DFALT_20MA_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_20MA_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALIBRATE_CURRENT");
            //clsGlobalVariables.algTests_Auto.Add("SET_12MA_ANLOP");
            //clsGlobalVariables.algTests_Auto.Add("CHK_ANALOG_OP_VAL");
            //clsGlobalVariables.algTests_Auto.Add("SET_DFALT_1V_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_1V_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_DFALT_10V_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_10V_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALIBRATE_VOLTAGE");
            //clsGlobalVariables.algTests_Auto.Add("SET_5V_ANLOP");
            //clsGlobalVariables.algTests_Auto.Add("CHK_ANALOG_OP_VAL");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_1_MV_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_50_MV_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALC_SLOPE_OFFSET");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_PT100");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_9V_CNT_PI");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_1V_CNT_PI");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_20mA_CNT_PI");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_1mA_CNT_PI");
            //clsGlobalVariables.algTests_Auto.Add("WRITE_CALIB_CONST");

            #endregion
            #region "151B12B"
            clsGlobalVariables.algTests_Auto.Add("READ_DEVICE_ID");
            clsGlobalVariables.algTests_Auto.Add("READ_CALIB_CONST_STATUS");
            clsGlobalVariables.algTests_Auto.Add("SWITCH_SENSOR_RELAY");
            clsGlobalVariables.algTests_Auto.Add("START_DISP_TEST");
            //clsGlobalVariables.algTests_Auto.Add("START_KEYPAD_TEST"); 
            clsGlobalVariables.algTests_Auto.Add("SSR_Test_PR69");
            clsGlobalVariables.algTests_Auto.Add("SET_DFALT_4MA_CNT");
            clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_4MA_CNT");
            clsGlobalVariables.algTests_Auto.Add("SET_DFALT_20MA_CNT");
            clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_20MA_CNT");
            clsGlobalVariables.algTests_Auto.Add("CALIBRATE_CURRENT");
            clsGlobalVariables.algTests_Auto.Add("SET_12MA_ANLOP");
            clsGlobalVariables.algTests_Auto.Add("CHK_ANALOG_OP_VAL");
            clsGlobalVariables.algTests_Auto.Add("SET_DFALT_1V_CNT");
            clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_1V_CNT");
            clsGlobalVariables.algTests_Auto.Add("SET_DFALT_10V_CNT");
            clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_10V_CNT");
            clsGlobalVariables.algTests_Auto.Add("CALIBRATE_VOLTAGE");
            clsGlobalVariables.algTests_Auto.Add("SET_5V_ANLOP");
            clsGlobalVariables.algTests_Auto.Add("CHK_ANALOG_OP_VAL");
            clsGlobalVariables.algTests_Auto.Add("CALIB_1_MV_CNT");
            clsGlobalVariables.algTests_Auto.Add("CALIB_50_MV_CNT");
            clsGlobalVariables.algTests_Auto.Add("CALC_SLOPE_OFFSET");
            clsGlobalVariables.algTests_Auto.Add("CALIB_PT100");
            clsGlobalVariables.algTests_Auto.Add("CALIB_TC");
            clsGlobalVariables.algTests_Auto.Add("WRITE_CALIB_CONST");
            #endregion
            #region "PR43"

            //clsGlobalVariables.algTests_Auto.Add("READ_DEVICE_ID");
            //clsGlobalVariables.algTests_Auto.Add("READ_CALIB_CONST_STATUS");
            //clsGlobalVariables.algTests_Auto.Add("SWITCH_SENSOR_RELAY");
            //clsGlobalVariables.algTests_Auto.Add("START_DISP_TEST");
            //clsGlobalVariables.algTests_Auto.Add("START_KEYPAD_TEST");
            //clsGlobalVariables.algTests_Auto.Add("START_REL_TEST_OP1_RELAY_PR43_PI");
            //clsGlobalVariables.algTests_Auto.Add("SSR_Test_PR43");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_1_MV_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_47_MV_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_PT100_100");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_PT100_313");
            //clsGlobalVariables.algTests_Auto.Add("WRITE_CALIB_CONST");
            #endregion
            //
            //clsGlobalVariables.algTests_Auto.Add("CALIB_1_MV_CNT");
            ////clsGlobalVariables.algTests_Auto.Add("CALIB_50_MV_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALC_SLOPE_OFFSET");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_PT100");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_TC");
            //
            //CALIB_PT100_313
            //clsGlobalVariables.algTests_Auto.Add("SSR_Test2");
            //SSR_Test2
            //clsGlobalVariables.algTests_Auto.Add("START_REL_TEST_OP1_RELAY");
            //clsGlobalVariables.algTests_Auto.Add("START_REL_TEST_OP2_RELAY");

            //Analog 
            //clsGlobalVariables.algTests_Auto.Add("SET_DFALT_4MA_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_4MA_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_DFALT_20MA_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_20MA_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALIBRATE_CURRENT");
            //clsGlobalVariables.algTests_Auto.Add("SET_12MA_ANLOP");
            //clsGlobalVariables.algTests_Auto.Add("CHK_ANALOG_OP_VAL");
            ////clsGlobalVariables.algTests_Auto.Add("SLAVE2_OP1_OFF");
            ////clsGlobalVariables.algTests_Auto.Add("SLAVE2_OP2_ON");
            //clsGlobalVariables.algTests_Auto.Add("SET_DFALT_1V_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_1V_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_DFALT_10V_CNT");
            //clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_10V_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALIBRATE_VOLTAGE");
            //clsGlobalVariables.algTests_Auto.Add("SET_5V_ANLOP");
            //clsGlobalVariables.algTests_Auto.Add("CHK_ANALOG_OP_VAL");

            //clsGlobalVariables.algTests_Auto.Add("CALIB_1_MV_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_50_MV_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALC_SLOPE_OFFSET");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_PT100");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_TC");
            //clsGlobalVariables.algTests_Auto.Add("");

            //Test21 = SET_DFALT_4MA_CNT
            //Test22 = SET_OBSRVED_4MA_CNT
            //Test23 = SET_DFALT_20MA_CNT
            //Test24 = SET_OBSRVED_20MA_CNT
            //Test25 = CALIBRATE_CURRENT
            //Test26 = SET_12MA_ANLOP
            //Test27 = CHK_ANALOG_OP_VAL
            //Test28 = SLAVE2_OP1_OFF
            //Test29 = SLAVE2_OP2_ON
            //Test30 = SET_DFALT_1V_CNT
            //Test31 = SET_OBSRVED_1V_CNT
            //Test32 = SET_DFALT_10V_CNT
            //Test33 = SET_OBSRVED_10V_CNT
            //Test34 = CALIBRATE_VOLTAGE
            //Test35 = SET_5V_ANLOP
            //Test36 = CHK_ANALOG_OP_VAL
            //clsGlobalVariables.algTests_Auto.Add("SLAVE1_OP3_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE2_OP1_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE2_OP2_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE2_OP3_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE3_OP3_OFF");
            //clsGlobalVariables.algTests_Auto.Add("CONVERTOR_OP1_OFF");
            //clsGlobalVariables.algTests_Auto.Add("CONVERTOR_OP2_OFF");
            //clsGlobalVariables.algTests_Auto.Add("DUT_OP1_OFF");
            //clsGlobalVariables.algTests_Auto.Add("DUT_OP2_OFF");
            //clsGlobalVariables.algTests_Auto.Add("DUT_OP3_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE1_OP1_ON");
            //clsGlobalVariables.algTests_Auto.Add("START_REL_TEST");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE1_OP1_OFF");
            //clsGlobalVariables.algTests_Auto.Add("DUT_OP2_OFF");
            //clsGlobalVariables.algTests_Auto.Add("DUT_OP3_OFF");
            //clsGlobalVariables.algTests_Auto.Add("DUT_OP1_ON");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE1_OP2_ON");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE3_OP3_ON");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE2_OP3_ON");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE2_READ_ADC_CNT_RLY_ON");
            //clsGlobalVariables.algTests_Auto.Add("DUT_OP1_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE2_READ_ADC_CNT_RLY_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE2_OP3_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE3_OP3_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE1_OP2_OFF");
            //clsGlobalVariables.algTests_Auto.Add("START_DISP_TEST");
            //clsGlobalVariables.algTests_Auto.Add("START_KEYPAD_TEST");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE3_OP1_OFF");
            //clsGlobalVariables.algTests_Auto.Add("SLAVE3_OP2_OFF");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_1_MV_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_50_MV_CNT");
            //clsGlobalVariables.algTests_Auto.Add("CALC_SLOPE_OFFSET");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_PT100");
            //clsGlobalVariables.algTests_Auto.Add("CALIB_TC");
            // clsGlobalVariables.algTests_Auto.Add("WRITE_CALIB_CONST");
            foreach (string test in clsGlobalVariables.algTests_Auto)
            {
                clsTotalTestsGroups.Add(new clsTotalTestsGroups() { TestNumber = count + 1, Test = test });
                count++;
            }
            TestsDetailsVis = true;
            IsProductSelected = true;
            clsTotalConnectedDevicesList.Clear();
            for (int Testnum = 0; Testnum < clsGlobalVariables.algTests_Auto.Count; Testnum++)
            {
                Dispatcher.CurrentDispatcher.Invoke(delegate {

                    clsTotalConnectedDevicesList.Add(new clsTotalConnectedDevices() { TestNumber = Testnum + 1, TestresultDevice1 = "", TestresultDevice2 = "", TestresultDevice3 = "", TestresultDevice4 = "", TestresultDevice5 = "", TestresultDevice6 = "" });

                });
            }

            //clsGlobalVariables.objGlobalFunction.ApplyDelay(1000);

            //Parameters
            //1. DUT Number
            //2. Test Number
            //3. Test Status
            clsGlobalVariables.NUMBER_OF_DUTS = Convert.ToInt32(NumberOfDUTs);
            //If usr change number of device then need to find com port again.

            if (clsGlobalVariables.NUMBER_OF_DUTS != clsGlobalVariables.OLD_NUMBER_OF_DUTS)
            {
                clsGlobalVariables.blngIsComportDetected = false;
                clsGlobalVariables.blngIsComportDetectedForPLC = false;
            }
            clsModelSettings.igDutID = 10;
            clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PR69_48x48;
            clsGlobalVariables.objGlobalFunction.LoadKeypadData();
            clsModelSettings.blnRS485Flag = false;
            if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                clsGlobalVariables.MB_MASTER_TO_DUT = 200;
            else
                clsGlobalVariables.MB_MASTER_TO_DUT = 100;
            if (clsGlobalVariables.objGlobalFunction.AutomaticCOMPortDetections(Convert.ToInt32(NumberOfDUTs)) != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                //imNumOfTests = clsGlobalVariables.algTests_Auto.Count;
                //almTempTestList = new ArrayList(clsGlobalVariables.algTests_Auto);
                FailurHandel();
                EnableDisableUI(true);
                return;
                //PLC off
            }

            OpenJigCOMPort();

            // clsGlobalVariables.objQueriescls.MBErase((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + 1));
            //clsGlobalVariables.objQueriescls.MBErase((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + 2));
            //clsGlobalVariables.objQueriescls.MBErase((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + 3));
            //clsGlobalVariables.objQueriescls.MBErase((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + 4));
            byte btmRetVal;
            int imLoopCntr;
            int imNumOfTests;
            ArrayList almTempTestList = null;
            clsGlobalVariables.NUMBER_OF_DUTS_List.Clear();
            for (int i = 1; i <= clsGlobalVariables.NUMBER_OF_DUTS; i++)
            {
                clsGlobalVariables.NUMBER_OF_DUTS_List.Add((byte)i);
            }

            imNumOfTests = clsGlobalVariables.algTests_Auto.Count;
            almTempTestList = new ArrayList(clsGlobalVariables.algTests_Auto);


            //Data log object is cleared here. Default data will be written into all the data menbers of this object.
            //clsGlobalVariables.objDataLog[DUT-1].Clear();
            //--------Changed By Shubham
            //Date:- 30-03-2018
            //Version:- V16
            //statement:- VREF text box value is cleared here.
            //txtVREF.Text = "";
            //-------Changes End.

            //prgbar.Maximum = imNumOfTests;
            //prgbar.Value = prgbar.Minimum;
            //This timeout is resseted here to original.           

            for (imLoopCntr = 0; imLoopCntr < imNumOfTests; ++imLoopCntr)
            {
                clsGlobalVariables.CurrentTestNumber = imLoopCntr + 1;
                if (clsGlobalVariables._StopFlag)
                {

                    clsGlobalVariables.objGlobalFunction.PLC_OFF();
                    CloseAllComport();
                    EnableDisableUI(true);
                    return;
                }
                //show current test on each DUT
                CurrentTestStatusDUT1 = almTempTestList[imLoopCntr].ToString();
                CurrentTestStatusDUT2 = almTempTestList[imLoopCntr].ToString();
                CurrentTestStatusDUT3 = almTempTestList[imLoopCntr].ToString();
                CurrentTestStatusDUT4 = almTempTestList[imLoopCntr].ToString();
                CurrentTestStatusDUT5 = almTempTestList[imLoopCntr].ToString();
                CurrentTestStatusDUT6 = almTempTestList[imLoopCntr].ToString();

                SelectedIndexDG1 = imLoopCntr;
                SelectedIndexDG2 = imLoopCntr;


                clsGlobalVariables.ig_Query_TimeOut = 1200;
                btmRetVal = clsGlobalVariables.objTestJIGFunctions.TestDUT(almTempTestList[imLoopCntr].ToString());
                clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PI;

                if (clsGlobalVariables.NUMBER_OF_DUTS_List.Count == 0)
                {
                    btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }

                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Accuracy_Test_Not_Done)
                {
                    //
                    // txtProgressInfo.Text = txtProgressInfo.Text + Environment.NewLine + "Test" + (imLoopCntr + 1) + " Fail." + "(" + almTempTestList[imLoopCntr] + ")";
                    clsMessages.DisplayMessage(clsMessageIDs.CALIBRATED_BUT_ACCURACY_ISNOTDONE);

                    clsGlobalVariables.objGlobalFunction.PLC_OFF();
                    CloseAllComport();
                    //CA 55
                    //if (mnuAutoCalibration.Checked == true)
                    //{
                    //    objfrmAccTest.ShowDialog();
                    //}
                    EnableDisableUI(true);

                    AccuracyWindow accuracyWindow1 = new AccuracyWindow();
                    accuracyWindow1.ShowDialog();

                    return;
                }
                else if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    //In case of error response below changes are done in the software.
                    clsGlobalVariables.objGlobalFunction.PLC_OFF();
                    CloseAllComport();
                    EnableDisableUI(true);
                    //txtProgressInfo.Text = txtProgressInfo.Text + Environment.NewLine + "Test" + (imLoopCntr + 1) + " Fail." + "(" + almTempTestList[imLoopCntr] + ")";
                    clsMessages.DisplayMessage(clsMessageIDs.Main_ERR_MSG);

                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.DUT_CALIB_FAILED);

                    //CA55 ClearSerialComPort();

                    return;
                }
                else
                {
                    clsGlobalVariables.mainWindowVM.DisplayMessage(clsGlobalVariables.DISPLAY_MSG_DUT_NUMBER, "Test" + (imLoopCntr + 1) + " Pass." + "(" + almTempTestList[imLoopCntr] + ")");
                    //CA55  txtProgressInfo.SelectionStart = txtProgressInfo.Text.Length - 1;
                    //Fix delay of 300msec is applied here to maintain query and response timeout compatibility with VB6.0 software.
                    //This delay is added by observing the query and response of the old VB software.
                    //clsGlobalVariables.objGlobalFunction.ApplyDelay(100);
                }
                //CA55 prgbar.Value = imLoopCntr;
            }
            //CA55 
            //This function performs JIG Initialization tests. this is only for PR69 devices and not for PI.
            //if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
            //{
            //    //This function performs JIG Initialization tests.
            //    btmRetVal = JIGInitializationTests();
            //    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
            //    {
            //        clsMessages.DisplayMessage(clsMessageIDs.Main_ERR_MSG);
            //        frmMain.objCalibratorSerialComm.CloseCommPort();
            //        frmMain.objJIGSerialComm.CloseCommPort();
            //        ClearSerialComPort();
            //        clsMessages.ShowMessageInProgressWindow(clsMessageIDs.JIG_INITIALZATION_FAILED);
            //        EnableControls(true);
            //        return;
            //    }
            //}
            //btmRetVal = clsGlobalVariables.objTestJIGFunctions.TestDUT("SOURCE_OFF");
            //shpPassFail.ShapeColor = Color.Green;
            //shpPassFail.TextONShape = "PASS";
            //shpPassFail.FontColor = Color.White;
            foreach (var DUT in clsGlobalVariables.NUMBER_OF_DUTS_List)
                clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT);
            clsMessages.ShowMessageInProgressWindow(clsMessageIDs.DUT_CALIB_COMPLETED);
            CloseAllComport();
            ///IsProcessOn = false;
            clsGlobalVariables.objGlobalFunction.PLC_ON_OFF_QUERY(false);
            //clsGlobalVariables.objGlobalFunction.ApplyDelay(5000);
            //clsGlobalVariables.objGlobalFunction.PLC_ON_OFF_QUERY(true);
            StartStopWatch(false);
            AccuracyWindow accuracyWindow = new AccuracyWindow();
            accuracyWindow.ShowDialog();

        }

        public MainWindowVM()
        {
            
            btnOkCmd = new RelayCommand(BtnOkclk);
            exitCmd = new RelayCommand(BtnExitclk);
            ComSettingCmd = new RelayCommand(BtnChangeComSettingsclk);
            OpenHelpfileCmd = new RelayCommand(BtnHelpfileclk);
            OpenDeviceSelectionWindow = new RelayCommand(BtnOpenDeviceSelWindow);
            OpenProdConfigWindow = new RelayCommand(OpenProdConfigClk);
            OpenAccuracyWindow = new RelayCommand(OpenAccuracyWindowClk);
            OpenProgrammingWindow = new RelayCommand(OpenProgrammingWindowClk);
            OpenAboutWindow = new RelayCommand(OpenAboutWindowClk);

            _btnStart = new RelayCommand(btnStartClk);
            _BtnStopCmd = new RelayCommand(btnStopClk);
            _OkTestCmd = new RelayCommand(OkTestClk);

            _PortList = new ObservableCollection<string>();
            _DeviceList = new ObservableCollection<string>();
            _DeviceNameList = new ObservableCollection<string>();
            _CatId = new ObservableCollection<ConfigurationDataList>();
            _clsTotalTestsGroups = new ObservableCollection<clsTotalTestsGroups>();
            _TotalConnectedDevicesList = new ObservableCollection<clsTotalConnectedDevices>();
            _ListOfTests = new ObservableCollection<string>();

            ShowProductSelectionWindow();

            if (!refresDataOfJsonFile())
            {
                ShowMessageBox("Error while reading data from json, something is wrong in json OR check json file path", false, "", clsGlobalVariables.MsgIcon.Error);
                return;
            }

            IsPR69TypeCatId = true;
            IsProductSelected = true;

            MyLogWriterDLL.LogWriter logWritter = new MyLogWriterDLL.LogWriter();
            logWritter.LogWriterStatus();

            DisableControl();

            //keypadTextDevice1 = "ENTER";
            //keypadTextDevice2 = "  UP";
            //keypadTextDevice3 = "DOWN";
            //keypadTextDevice4 = "ESCAPE";

            clsGlobalVariables.DispImgpath = Directory.GetCurrentDirectory()+ "\\Images\\";
            
        }

        public static List<IList<AccuracyTests>> accuracyTestsName = new List<IList<AccuracyTests>>();

        public static void FillCollectionofAccuracy()
        {
            accuracyTestsName.Add(clsGlobalVariables.Selectedcatid.mAmpTests);
            accuracyTestsName.Add(clsGlobalVariables.Selectedcatid.VoltTests);
            accuracyTestsName.Add(clsGlobalVariables.Selectedcatid.PT100SensorTests);
            accuracyTestsName.Add(clsGlobalVariables.Selectedcatid.RSensor);
            accuracyTestsName.Add(clsGlobalVariables.Selectedcatid.JSensor);
        }

        public void DisableControl()
        {
            DUT1DetailsVis = false;
            DUT2DetailsVis = false;
            DUT3DetailsVis = false;
            DUT4DetailsVis = false;
            DUT5DetailsVis = false;
            DUT6DetailsVis = false;
            TestsDetailsVis = false;

            keypadDevice1Vis = false;
            keypadDevice2Vis = false;
            keypadDevice3Vis = false;
            keypadDevice4Vis = false;
            keypadDevice5Vis = false;
            keypadDevice6Vis = false;
        }

        //DUTnumber
        //Enumkey
        //Enable/Disable        
        public void DisplayKeypadTest(byte enmConnectedDevices , string displaytext, bool Enabletest)
        {
            switch (enmConnectedDevices)
            {
                case 1:
                    keypadDevice1Vis = Enabletest;
                    keypadTextDevice1 = displaytext;
                    break;
                case 2:
                    keypadDevice2Vis = Enabletest;
                    keypadTextDevice2 = displaytext;
                    break;
                case 3:
                    keypadDevice3Vis = Enabletest;
                    keypadTextDevice3 = displaytext;
                    break;
                case 4:
                    keypadDevice4Vis = Enabletest;
                    keypadTextDevice4 = displaytext;
                    break;
                case 5:
                    keypadDevice5Vis = Enabletest;
                    keypadTextDevice5 = displaytext;
                    break;
                case 6:
                    keypadDevice6Vis = Enabletest;
                    keypadTextDevice6 = displaytext;
                    break;
                default:
                    break;
            }
        }

        private void OpenAboutWindowClk(object obj)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void OpenProgrammingWindowClk(object obj)
        {
            ProgrammingWindow programmingWindow = new ProgrammingWindow();
            programmingWindow.ShowDialog();
        }

        private void OpenAccuracyWindowClk(object obj)
        {
            AccuracyWindow accuracyWindow = new AccuracyWindow();
            accuracyWindow.ShowDialog();
        }

        private void OkTestClk(object obj)
        {
            
            CancelTestBtnVis = false;

            switch (obj.ToString())
            {
                case "OkTestDUT1":
                    OkTestBtnVisDUT1 = false;
                    break;

                case "OkTestDUT2":
                    OkTestBtnVisDUT2 = false;
                    break;

                case "OkTestDUT3":
                    OkTestBtnVisDUT3 = false;
                    break;

                case "OkTestDUT4":
                    OkTestBtnVisDUT4 = false;
                    break;

                case "OkTestDUT5":
                    OkTestBtnVisDUT5 = false;
                    break;

                case "OkTestDUT6":
                    OkTestBtnVisDUT6 = false;
                    break;

                default:
                    break;
            }
        }
        private RelayCommand _btnStart;

        public RelayCommand btnStart
        {
            get { return _btnStart; }
            set { _btnStart = value; }
        }

        private RelayCommand _BtnStopCmd;

        public RelayCommand BtnStopCmd
        {
            get { return _BtnStopCmd; }
            set { _BtnStopCmd = value; }
        }

        private RelayCommand _OkTestCmd;

        public RelayCommand OkTestCmd
        {
            get { return _OkTestCmd; }
            set { _OkTestCmd = value; }
        }

        private bool _keypadDevice1Vis;

        public bool keypadDevice1Vis
        {
            get { return _keypadDevice1Vis; }
            set { _keypadDevice1Vis = value; OnPropertyChanged("keypadDevice1Vis"); }
        }

        private bool _keypadDevice2Vis;

        public bool keypadDevice2Vis
        {
            get { return _keypadDevice2Vis; }
            set { _keypadDevice2Vis = value; OnPropertyChanged("keypadDevice2Vis"); }
        }

        private bool _keypadDevice3Vis;

        public bool keypadDevice3Vis
        {
            get { return _keypadDevice3Vis; }
            set { _keypadDevice3Vis = value; OnPropertyChanged("keypadDevice3Vis"); }
        }

        private bool _keypadDevice4Vis;

        public bool keypadDevice4Vis
        {
            get { return _keypadDevice4Vis; }
            set { _keypadDevice4Vis = value; OnPropertyChanged("keypadDevice4Vis"); }
        }

        private bool _keypadDevice5Vis;

        public bool keypadDevice5Vis
        {
            get { return _keypadDevice5Vis; }
            set { _keypadDevice5Vis = value; OnPropertyChanged("keypadDevice5Vis"); }
        }

        private bool _keypadDevice6Vis;

        public bool keypadDevice6Vis
        {
            get { return _keypadDevice6Vis; }
            set { _keypadDevice6Vis = value; OnPropertyChanged("keypadDevice6Vis"); }
        }

        private string _keypadTextDevice1;

        public string keypadTextDevice1
        {
            get { return _keypadTextDevice1; }
            set { _keypadTextDevice1 = value; OnPropertyChanged("keypadTextDevice1"); }
        }

        private string _keypadTextDevice2;

        public string keypadTextDevice2
        {
            get { return _keypadTextDevice2; }
            set { _keypadTextDevice2 = value; OnPropertyChanged("keypadTextDevice2"); }
        }

        private string _keypadTextDevice3;

        public string keypadTextDevice3
        {
            get { return _keypadTextDevice3; }
            set { _keypadTextDevice3 = value; OnPropertyChanged("keypadTextDevice3"); }
        }

        private string _keypadTextDevice4;

        public string keypadTextDevice4
        {
            get { return _keypadTextDevice4; }
            set { _keypadTextDevice4 = value; OnPropertyChanged("keypadTextDevice4"); }
        }

        private string _keypadTextDevice5;

        public string keypadTextDevice5
        {
            get { return _keypadTextDevice5; }
            set { _keypadTextDevice5 = value; OnPropertyChanged("keypadTextDevice5"); }
        }

        private string _keypadTextDevice6;

        public string keypadTextDevice6
        {
            get { return _keypadTextDevice6; }
            set { _keypadTextDevice6 = value; OnPropertyChanged("keypadTextDevice6"); }
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
        
        
        private string _StopwatchTime;

        public string StopwatchTime
        {
            get { return _StopwatchTime; }
            set { _StopwatchTime = value; OnPropertyChanged("StopwatchTime"); }
        }
        private string _CurrentTestStatusDUT1;

        public string CurrentTestStatusDUT1
        {
            get { return _CurrentTestStatusDUT1; }
            set { _CurrentTestStatusDUT1 = value; OnPropertyChanged("CurrentTestStatusDUT1"); }
        }

        private string _CurrentTestStatusDUT2;

        public string CurrentTestStatusDUT2
        {
            get { return _CurrentTestStatusDUT2; }
            set { _CurrentTestStatusDUT2 = value; OnPropertyChanged("CurrentTestStatusDUT2"); }
        }

        private string _CurrentTestStatusDUT3;

        public string CurrentTestStatusDUT3
        {
            get { return _CurrentTestStatusDUT3; }
            set { _CurrentTestStatusDUT3 = value; OnPropertyChanged("CurrentTestStatusDUT3"); }
        }

        private string _CurrentTestStatusDUT4;

        public string CurrentTestStatusDUT4
        {
            get { return _CurrentTestStatusDUT4; }
            set { _CurrentTestStatusDUT4 = value; OnPropertyChanged("CurrentTestStatusDUT4"); }
        }

        private string _CurrentTestStatusDUT5;

        public string CurrentTestStatusDUT5
        {
            get { return _CurrentTestStatusDUT5; }
            set { _CurrentTestStatusDUT5 = value; OnPropertyChanged("CurrentTestStatusDUT5"); }
        }

        private string _CurrentTestStatusDUT6;

        public string CurrentTestStatusDUT6
        {
            get { return _CurrentTestStatusDUT6; }
            set { _CurrentTestStatusDUT6 = value; OnPropertyChanged("CurrentTestStatusDUT6"); }
        }

        private string _CurrenttstgrpDUT1;

        public string CurrenttstgrpDUT1
        {
            get { return _CurrenttstgrpDUT1; }
            set { _CurrenttstgrpDUT1 = value; OnPropertyChanged("CurrenttstgrpDUT1"); }
        }

        private string _CurrenttstgrpDUT2;

        public string CurrenttstgrpDUT2
        {
            get { return _CurrenttstgrpDUT2; }
            set { _CurrenttstgrpDUT2 = value; OnPropertyChanged("CurrenttstgrpDUT2"); }
        }

        private string _CurrenttstgrpDUT3;

        public string CurrenttstgrpDUT3
        {
            get { return _CurrenttstgrpDUT3; }
            set { _CurrenttstgrpDUT3 = value; OnPropertyChanged("CurrenttstgrpDUT3"); }
        }

        private string _CurrenttstgrpDUT4;

        public string CurrenttstgrpDUT4
        {
            get { return _CurrenttstgrpDUT4; }
            set { _CurrenttstgrpDUT4 = value; OnPropertyChanged("CurrenttstgrpDUT4"); }
        }
        private string _CurrenttstgrpDUT5;

        public string CurrenttstgrpDUT5
        {
            get { return _CurrenttstgrpDUT5; }
            set { _CurrenttstgrpDUT5 = value; OnPropertyChanged("CurrenttstgrpDUT5"); }
        }

        private string _CurrenttstgrpDUT6;

        public string CurrenttstgrpDUT6
        {
            get { return _CurrenttstgrpDUT6; }
            set { _CurrenttstgrpDUT6 = value; OnPropertyChanged("CurrenttstgrpDUT6"); }
        }



        private bool _MesssageVis = false;
        
        public bool MesssageVis
        {
            get { return _MesssageVis; }
            set { _MesssageVis = value; OnPropertyChanged("MesssageVis"); }
        }
        
        private bool _IsDialogOpen = false;

        public bool IsDialogOpen
        {
            get { return _IsDialogOpen; }
            set { _IsDialogOpen = value; OnPropertyChanged("IsDialogOpen"); }
        }

        private bool _IsProductSelected = false;

        public bool IsProductSelected
        {
            get { return _IsProductSelected; }
            set { _IsProductSelected = value; OnPropertyChanged("IsProductSelected"); }
        }

        private bool _IsProcessOn = false;

        public bool IsProcessOn
        {
            get { return _IsProcessOn; }
            set { _IsProcessOn = value; OnPropertyChanged("IsProcessOn"); }
        }



        private bool _DUT1DetailsVis;

        public bool DUT1DetailsVis
        {
            get { return _DUT1DetailsVis; }
            set { _DUT1DetailsVis = value; OnPropertyChanged("DUT1DetailsVis"); }
        }

        private bool _DUT2DetailsVis;

        public bool DUT2DetailsVis
        {
            get { return _DUT2DetailsVis; }
            set { _DUT2DetailsVis = value; OnPropertyChanged("DUT2DetailsVis"); }
        }

        private bool _DUT3DetailsVis;

        public bool DUT3DetailsVis
        {
            get { return _DUT3DetailsVis; }
            set { _DUT3DetailsVis = value; OnPropertyChanged("DUT3DetailsVis"); }
        }

        private bool _DUT4DetailsVis;

        public bool DUT4DetailsVis
        {
            get { return _DUT4DetailsVis; }
            set { _DUT4DetailsVis = value; OnPropertyChanged("DUT4DetailsVis"); }
        }

        private bool _DUT5DetailsVis;

        public bool DUT5DetailsVis
        {
            get { return _DUT5DetailsVis; }
            set { _DUT5DetailsVis = value; OnPropertyChanged("DUT5DetailsVis"); }
        }

        private bool _DUT6DetailsVis;

        public bool DUT6DetailsVis
        {
            get { return _DUT6DetailsVis; }
            set { _DUT6DetailsVis = value; OnPropertyChanged("DUT6DetailsVis"); }
        }

        private bool _TestsDetailsVis;

        public bool TestsDetailsVis
        {
            get { return _TestsDetailsVis; }
            set { _TestsDetailsVis = value; OnPropertyChanged("TestsDetailsVis"); }
        }

        private bool _OkTestBtnVisDUT1;

        public bool OkTestBtnVisDUT1
        {
            get { return _OkTestBtnVisDUT1; }
            set { _OkTestBtnVisDUT1 = value; OnPropertyChanged("OkTestBtnVisDUT1"); }
        }

        private bool _OkTestBtnVisDUT2;

        public bool OkTestBtnVisDUT2
        {
            get { return _OkTestBtnVisDUT2; }
            set { _OkTestBtnVisDUT2 = value; OnPropertyChanged("OkTestBtnVisDUT2"); }
        }

        private bool _OkTestBtnVisDUT3;

        public bool OkTestBtnVisDUT3
        {
            get { return _OkTestBtnVisDUT3; }
            set { _OkTestBtnVisDUT3 = value; OnPropertyChanged("OkTestBtnVisDUT3"); }
        }

        private bool _OkTestBtnVisDUT4;

        public bool OkTestBtnVisDUT4
        {
            get { return _OkTestBtnVisDUT4; }
            set { _OkTestBtnVisDUT4 = value; OnPropertyChanged("OkTestBtnVisDUT4"); }
        }

        private bool _OkTestBtnVisDUT5;

        public bool OkTestBtnVisDUT5
        {
            get { return _OkTestBtnVisDUT5; }
            set { _OkTestBtnVisDUT5 = value; OnPropertyChanged("OkTestBtnVisDUT5"); }
        }

        private bool _OkTestBtnVisDUT6;

        public bool OkTestBtnVisDUT6
        {
            get { return _OkTestBtnVisDUT6; }
            set { _OkTestBtnVisDUT6 = value; OnPropertyChanged("OkTestBtnVisDUT6"); }
        }

        private string _strmsgDUT1;

        public string strmsgDUT1
        {
            get { return _strmsgDUT1; }
            set { _strmsgDUT1 = value; OnPropertyChanged("strmsgDUT1"); }
        }

        private string _strmsgDUT2;

        public string strmsgDUT2
        {
            get { return _strmsgDUT2; }
            set { _strmsgDUT2 = value; OnPropertyChanged("strmsgDUT2"); }
        }

        private string _strmsgDUT3;

        public string strmsgDUT3
        {
            get { return _strmsgDUT3; }
            set { _strmsgDUT3 = value; OnPropertyChanged("strmsgDUT3"); }
        }

        private string _strmsgDUT4;

        public string strmsgDUT4
        {
            get { return _strmsgDUT4; }
            set { _strmsgDUT4 = value; OnPropertyChanged("strmsgDUT4"); }
        }

        private string _strmsgDUT5;

        public string strmsgDUT5
        {
            get { return _strmsgDUT5; }
            set { _strmsgDUT5 = value; OnPropertyChanged("strmsgDUT5"); }
        }

        private string _strmsgDUT6;

        public string strmsgDUT6
        {
            get { return _strmsgDUT6; }
            set { _strmsgDUT6 = value; OnPropertyChanged("strmsgDUT6"); }
        }


        private bool _CancelTestBtnVis;

        public bool CancelTestBtnVis
        {
            get { return _CancelTestBtnVis; }
            set { _CancelTestBtnVis = value; OnPropertyChanged("CancelTestBtnVis"); }
        }


        private bool _OkBtnVis = false;

        public bool OkbtnVis
        {
            get { return _OkBtnVis; }
            set { _OkBtnVis = value; OnPropertyChanged("OkbtnVis"); }
        }

        private string _Sender = "";

        public string Sender
        {
            get { return _Sender; }
            set { _Sender = value; OnPropertyChanged("Sender"); }
        }

        private string _Msg = "";

        public string Msg
        {
            get { return _Msg; }
            set { _Msg = value; OnPropertyChanged("Msg"); }
        }

        private bool _YesNoBtnVis = false;

        public bool YesNoBtnVis
        {
            get { return _YesNoBtnVis; }
            set { _YesNoBtnVis = value; OnPropertyChanged("YesNoBtnVis"); }
        }

        private bool _CancelBtnVis ;

        public bool CancelBtnVis
        {
            get { return _CancelBtnVis; }
            set { _CancelBtnVis = value; OnPropertyChanged("CancelBtnVis"); }
        }

        private bool _IconMsgVis = true;

        public bool IconMsgVis
        {
            get { return _IconMsgVis; }
            set { _IconMsgVis = value; OnPropertyChanged("IconMsgVis"); }
        }

        private bool _IconErrorVis = false;

        public bool IconErrorVis
        {
            get { return _IconErrorVis; }
            set { _IconErrorVis = value; OnPropertyChanged("IconErrorVis"); }
        }

        private bool _IconQuestionVis;

        public bool IconQuestionVis
        {
            get { return _IconQuestionVis; }
            set { _IconQuestionVis = value; OnPropertyChanged("IconQuestionVis"); }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }

        private int _SelectedIndexDG1;

        public int SelectedIndexDG1
        {
            get { return _SelectedIndexDG1; }
            set { _SelectedIndexDG1 = value; OnPropertyChanged("SelectedIndexDG1"); }
        }

        private int _SelectedIndexDG2;

        public int SelectedIndexDG2
        {
            get { return _SelectedIndexDG2; }
            set { _SelectedIndexDG2 = value; OnPropertyChanged("SelectedIndexDG2"); }
        }


        private string _SelectedDeviceName;

        public string SelectedDeviceName
        {
            get { return _SelectedDeviceName; }
            set
            {
                _SelectedDeviceName = value;
                if (_SelectedDeviceName != null)
                {                    

                    for (int devtype = 0; devtype < CatId[0].ConfigurationData.Count; devtype++)
                    {
                        for (int catid = 0; catid < CatId[0].ConfigurationData[devtype].CatIdLists.Count; catid++)
                        {
                            if (CatId[0].ConfigurationData[devtype].CatIdLists[catid].DeviceName == _SelectedDeviceName)
                            {
                                IsEnabledOkbtn = true;
                                StartBtnVis = true;
                                AssignConfigurationDetailsToUI();
                                NumberOfDUTs = "4";
                                BatchNumber = "1";
                                ListOfTests.Clear();
                            }
                        }
                    }

                }
                else
                {
                    IsEnabledOkbtn = false;
                    IsProductSelected = false;
                }
                    
                OnPropertyChanged("SelectedDeviceName");
            }
        }

        private bool _ProductSelectionVis = false;

        public bool ProductSelectionVis
        {
            get { return _ProductSelectionVis; }
            set { _ProductSelectionVis = value; OnPropertyChanged("ProductSelectionVis"); }
        }

        private string _SelectedDeviceType;

        public string SelectedDeviceType
        {
            get { return _SelectedDeviceType; }
            set
            {
                _SelectedDeviceType = value;
                DeviceNameList.Clear();
                for (int devtype = 0; devtype < CatId[0].ConfigurationData.Count; devtype++)
                {
                    if (CatId[0].ConfigurationData[devtype].DeviceType == _SelectedDeviceType)
                    {
                        for (int catid = 0; catid < CatId[0].ConfigurationData[devtype].CatIdLists.Count; catid++)
                        {
                            DeviceNameList.Add(CatId[0].ConfigurationData[devtype].CatIdLists[catid].DeviceName);
                        }
                    }
                }

                OnPropertyChanged("SelectedDeviceType");
            }
        }

        private bool _DeviceTypeError;

        public bool DeviceTypeError
        {
            get { return _DeviceTypeError; }
            set { _DeviceTypeError = value; OnPropertyChanged("DeviceTypeError"); }
        }

        private bool _DeviceNameError;

        public bool DeviceNameError
        {
            get { return _DeviceNameError; }
            set { _DeviceNameError = value; OnPropertyChanged("_DeviceNameError"); }
        }

        private bool _IsPR43TypeCatId;

        public bool IsPR43TypeCatId
        {
            get { return _IsPR43TypeCatId; }
            set
            {
                _IsPR43TypeCatId = value;
                
                if (_IsPR43TypeCatId)
                {
                    clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PR69_48x48;
                    DeviceTypeList.Clear();
                    DeviceNameList.Clear();
                    for (int devtype = 0; devtype < CatId[0].ConfigurationData.Count; devtype++)
                    {
                        if (CatId[0].ConfigurationData[devtype].DeviceType.Contains("43"))
                        {
                            DeviceTypeList.Add(CatId[0].ConfigurationData[devtype].DeviceType);
                        }
                    }
                    OnPropertyChanged("IsPR43TypeCatId");
                }
            }
        }

        private bool _IsPR69TypeCatId;

        public bool IsPR69TypeCatId
        {
            get { return _IsPR69TypeCatId; }
            set
            {
                _IsPR69TypeCatId = value;
                if (_IsPR69TypeCatId)
                {                    
                    DeviceTypeList.Clear();
                    DeviceNameList.Clear();
                    clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PR69_96x96;

                    for (int devtype = 0; devtype < CatId[0].ConfigurationData.Count; devtype++)
                    {
                        if (CatId[0].ConfigurationData[devtype].DeviceType.Contains("69"))
                        {
                            DeviceTypeList.Add(CatId[0].ConfigurationData[devtype].DeviceType);
                        }
                    }

                    OnPropertyChanged("IsPR69TypeCatId");
                }
            }
        }
        private bool _IsProcessIndTypeCatId;

        public bool IsProcessIndTypeCatId
        {
            get { return _IsProcessIndTypeCatId; }
            set
            {
                _IsProcessIndTypeCatId = value;

                if (_IsProcessIndTypeCatId)
                {
                    DeviceTypeList.Clear();
                    DeviceNameList.Clear();
                    clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PI;

                    for (int devtype = 0; devtype < CatId[0].ConfigurationData.Count; devtype++)
                    {
                        if (CatId[0].ConfigurationData[devtype].DeviceType.Contains("Pr"))
                        {
                            DeviceTypeList.Add(CatId[0].ConfigurationData[devtype].DeviceType);
                        }
                    }
                }

                OnPropertyChanged("IsProcessIndTypeCatId");
            }
        }

        private string _SelectedComPort;

        public string SelectedComPort
        {
            get { return _SelectedComPort; }
            set
            {
                _SelectedComPort = value;
                
                OnPropertyChanged("SelectedComPort");
            }
        }

        private string _SelectedBaudRate;

        public string SelectedBaudRate
        {
            get { return _SelectedBaudRate; }
            set
            {
                _SelectedBaudRate = value;                
                OnPropertyChanged("SelectedBaudRate");
            }
        }

        private bool _ComSelectionVis;

        public bool ComSelectionVis
        {
            get { return _ComSelectionVis; }
            set { _ComSelectionVis = value; OnPropertyChanged("ComSelectionVis"); }
        }

        private bool _ComPortError;

        public bool ComPortError
        {
            get { return _ComPortError; }
            set { _ComPortError = value; OnPropertyChanged("ComPortError"); }
        }

        private bool _BaudRateError;

        public bool BaudRateError
        {
            get { return _BaudRateError; }
            set { _BaudRateError = value; OnPropertyChanged("BaudRateError"); }
        }

        private ObservableCollection<string> _ListOfTests;

        public ObservableCollection<string> ListOfTests
        {
            get { return _ListOfTests; }
            set { _ListOfTests = value; OnPropertyChanged("ListOfTests"); }
        }


        private ObservableCollection<clsTotalTestsGroups> _clsTotalTestsGroups;

        public ObservableCollection<clsTotalTestsGroups> clsTotalTestsGroups
        {
            get { return _clsTotalTestsGroups; }
            set { _clsTotalTestsGroups = value; OnPropertyChanged("clsTotalTestsGroups"); }
        }
        

        private ObservableCollection<clsTotalConnectedDevices> _TotalConnectedDevicesList;

        public ObservableCollection<clsTotalConnectedDevices> clsTotalConnectedDevicesList
        {
            get { return _TotalConnectedDevicesList; }
            set { _TotalConnectedDevicesList = value; OnPropertyChanged("clsTotalConnectedDevicesList"); }
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

        private int _DeviceInfoColumn;

        public int DeviceInfoColumn
        {
            get { return _DeviceInfoColumn; }
            set { _DeviceInfoColumn = value; OnPropertyChanged("DeviceInfoColumn"); }
        }

        private bool _IsEnabledOkbtn;

        public bool IsEnabledOkbtn
        {
            get { return _IsEnabledOkbtn; }
            set { _IsEnabledOkbtn = value; OnPropertyChanged("IsEnabledOkbtn"); }
        }

        private string _BatchNumber;

        public string BatchNumber
        {
            get { return _BatchNumber; }
            set
            {
                _BatchNumber = value;

                if (_BatchNumber != null && _BatchNumber != "")
                {
                    IsEnabledOkbtn = true;
                }
                else
                {
                    TestsDetailsVis = false;
                    IsEnabledOkbtn = false;
                }

                OnPropertyChanged("BatchNumber");
            }
        }


        private string _NumberOfDUTs;

        public string NumberOfDUTs
        {
            get { return _NumberOfDUTs; }
            set
            {
                _NumberOfDUTs = value;

                switch (_NumberOfDUTs)
                {
                    case "0":
                        DUT1DetailsVis = false;
                        DUT2DetailsVis = false;
                        DUT3DetailsVis = false;
                        DUT4DetailsVis = false;
                        DUT5DetailsVis = false;
                        DUT6DetailsVis = false;

                        DeviceNumber1Vis = false;
                        DeviceNumber2Vis = false;
                        DeviceNumber3Vis = false;
                        DeviceNumber4Vis = false;
                        DeviceNumber5Vis = false;
                        DeviceNumber6Vis = false;

                        IsEnabledOkbtn = false;

                        //MessageBox.Show("Please enter at least 1 device for calibration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        //NumberOfDUTs = 1;

                        break;
                    case "1":
                        DUT1DetailsVis = true;
                        DUT2DetailsVis = false;
                        DUT3DetailsVis = false;
                        DUT4DetailsVis = false;
                        DUT5DetailsVis = false;
                        DUT6DetailsVis = false;

                        DeviceNumber1Vis = true;
                        DeviceNumber2Vis = false;
                        DeviceNumber3Vis = false;
                        DeviceNumber4Vis = false;
                        DeviceNumber5Vis = false;
                        DeviceNumber6Vis = false;

                        break;
                    case "2":
                        DUT1DetailsVis = true;
                        DUT2DetailsVis = true;
                        DUT3DetailsVis = false;
                        DUT4DetailsVis = false;
                        DUT5DetailsVis = false;
                        DUT6DetailsVis = false;

                        DeviceNumber1Vis = true;
                        DeviceNumber2Vis = true;
                        DeviceNumber3Vis = false;
                        DeviceNumber4Vis = false;
                        DeviceNumber5Vis = false;
                        DeviceNumber6Vis = false;

                        break;
                    case "3":
                        DUT1DetailsVis = true;
                        DUT2DetailsVis = true;
                        DUT3DetailsVis = true;
                        DUT4DetailsVis = false;
                        DUT5DetailsVis = false;
                        DUT6DetailsVis = false;

                        DeviceNumber1Vis = true;
                        DeviceNumber2Vis = true;
                        DeviceNumber3Vis = true;
                        DeviceNumber4Vis = false;
                        DeviceNumber5Vis = false;
                        DeviceNumber6Vis = false;

                        break;
                    case "4":
                        DUT1DetailsVis = true;
                        DUT2DetailsVis = true;
                        DUT3DetailsVis = true;
                        DUT4DetailsVis = true;
                        DUT5DetailsVis = false;
                        DUT6DetailsVis = false;

                        DeviceNumber1Vis = true;
                        DeviceNumber2Vis = true;
                        DeviceNumber3Vis = true;
                        DeviceNumber4Vis = true;
                        DeviceNumber5Vis = false;
                        DeviceNumber6Vis = false;

                        break;
                    //case 5:
                    //    DUT1DetailsVis = true;
                    //    DUT2DetailsVis = true;
                    //    DUT3DetailsVis = true;
                    //    DUT4DetailsVis = true;
                    //    DUT5DetailsVis = true;
                    //    DUT6DetailsVis = false;

                    //    DeviceNumber1Vis = true;
                    //    DeviceNumber2Vis = true;
                    //    DeviceNumber3Vis = true;
                    //    DeviceNumber4Vis = true;
                    //    DeviceNumber5Vis = true;
                    //    DeviceNumber6Vis = false;

                    //    break;
                    //case 6:
                    //    DUT1DetailsVis = true;
                    //    DUT2DetailsVis = true;
                    //    DUT3DetailsVis = true;
                    //    DUT4DetailsVis = true;
                    //    DUT5DetailsVis = true;
                    //    DUT6DetailsVis = true;

                    //    DeviceNumber1Vis = true;
                    //    DeviceNumber2Vis = true;
                    //    DeviceNumber3Vis = true;
                    //    DeviceNumber4Vis = true;
                    //    DeviceNumber5Vis = true;
                    //    DeviceNumber6Vis = true;

                    //    break;

                    default:                      
                        //ShowMessageBox("Please enter total number of devices less than or equal to 6.", false, "InvalidDUT_No", clsGlobalVariables.MsgIcon.Error);
                       // MessageBox.Show("Please enter total number of devices less than or equal to 4.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                       // NumberOfDUTs = "4";
                        break;
                }

                if (_NumberOfDUTs != null && _NumberOfDUTs != "")
                {
                    if (Convert.ToInt32(_NumberOfDUTs) > 0 && Convert.ToInt32(_NumberOfDUTs) < 5)
                    {
                        if (Convert.ToInt32(_NumberOfDUTs) > 4)
                            DeviceInfoColumn = 3;
                        else
                            DeviceInfoColumn = 2;

                        IsProductSelected = true;
                        TestsDetailsVis = true;
                        IsEnabledOkbtn = true;
                    }
                    else
                    {
                        TestsDetailsVis = false;
                        IsEnabledOkbtn = false;
                    }
                }
                else
                {
                    TestsDetailsVis = false;
                    IsEnabledOkbtn = false;
                }
                
                OnPropertyChanged("NumberOfDUTs");
            }
        }
        
        private ObservableCollection<string> _DeviceList;

        public ObservableCollection<string> DeviceTypeList
        {
            get { return _DeviceList; }
            set { _DeviceList = value; OnPropertyChanged("DeviceTypeList"); }
        }

        private ObservableCollection<string> _DeviceNameList;

        public ObservableCollection<string> DeviceNameList
        {
            get { return _DeviceNameList; }
            set { _DeviceNameList = value; OnPropertyChanged("DeviceNameList"); }
        }
        private ObservableCollection<string> _PortList;

        public ObservableCollection<string> PortList
        {
            get { return _PortList; }
            set { _PortList = value; OnPropertyChanged("PortList"); }
        }

        private ObservableCollection<ConfigurationDataList> _CatId;

        public ObservableCollection<ConfigurationDataList> CatId
        {
            get { return _CatId; }
            set { _CatId = value; OnPropertyChanged("CatId"); }
        }

        public RelayCommand btnOkCmd { get; set; }
        public RelayCommand exitCmd { get; set; }
        public RelayCommand ComSettingCmd { get; set; }
        public RelayCommand OpenHelpfileCmd { get; set; }
        public RelayCommand OpenDeviceSelectionWindow { get; set; }
        public RelayCommand OpenProdConfigWindow { get; set; }
        public RelayCommand OpenAccuracyWindow { get; set; }
        public RelayCommand OpenProgrammingWindow { get; set; }
        public RelayCommand OpenAboutWindow { get; set; }

        private void btnStopClk(object obj)
        {
            StartBtnVis = true;
            StopBtnVis = false;
            tmrMbTimer.Dispose();
        }
        


        //Parameters
        //1. DUT Number
        //2. Test Number
        //3. Test Status
        public void UpdateTestResult(int DUTNumber, string status)
        {
            if (status==clsGlobalVariables.FAIL)
            {
                clsGlobalVariables.NUMBER_OF_FAIL_DUTS_List.Add((byte)DUTNumber);
            }
            int testnumber = clsGlobalVariables.CurrentTestNumber;
            switch (DUTNumber)
            {
                case 1:
                    foreach (clsTotalConnectedDevices item in clsTotalConnectedDevicesList)
                    {
                        if (item.TestNumber == testnumber)
                        {
                            clsTotalConnectedDevicesList[item.TestNumber - 1] = new clsTotalConnectedDevices {TestNumber = item.TestNumber, TestresultDevice1 = status,TestresultDevice2 = item.TestresultDevice2,TestresultDevice3 = item.TestresultDevice3, TestresultDevice4 = item.TestresultDevice4,TestresultDevice5 = item.TestresultDevice5,TestresultDevice6 = item.TestresultDevice6 };
                            break;
                        }
                    }
                    break;

                case 2:
                    foreach (clsTotalConnectedDevices item in clsTotalConnectedDevicesList)
                    {
                        if (item.TestNumber == testnumber)
                        {
                            clsTotalConnectedDevicesList[item.TestNumber - 1] = new clsTotalConnectedDevices { TestNumber = item.TestNumber, TestresultDevice1 = item.TestresultDevice1, TestresultDevice2 = status, TestresultDevice3 = item.TestresultDevice3, TestresultDevice4 = item.TestresultDevice4, TestresultDevice5 = item.TestresultDevice5, TestresultDevice6 = item.TestresultDevice6 };
                            break;
                        }
                    }
                    break;

                case 3:
                    foreach (clsTotalConnectedDevices item in clsTotalConnectedDevicesList)
                    {
                        if (item.TestNumber == testnumber)
                        {
                            clsTotalConnectedDevicesList[item.TestNumber - 1] = new clsTotalConnectedDevices { TestNumber = item.TestNumber, TestresultDevice1 = item.TestresultDevice1, TestresultDevice2 = item.TestresultDevice2, TestresultDevice3 = status, TestresultDevice4 = item.TestresultDevice4, TestresultDevice5 = item.TestresultDevice5, TestresultDevice6 = item.TestresultDevice6 };
                            break;
                        }
                    }
                    break;

                case 4:
                    foreach (clsTotalConnectedDevices item in clsTotalConnectedDevicesList)
                    {
                        if (item.TestNumber == testnumber)
                        {
                            clsTotalConnectedDevicesList[item.TestNumber - 1] = new clsTotalConnectedDevices { TestNumber = item.TestNumber, TestresultDevice1 = item.TestresultDevice1, TestresultDevice2 = item.TestresultDevice2, TestresultDevice3 = item.TestresultDevice3, TestresultDevice4 = status, TestresultDevice5 = item.TestresultDevice5, TestresultDevice6 = item.TestresultDevice6 };
                            break;
                        }
                    }
                    break;

                case 5:
                    foreach (clsTotalConnectedDevices item in clsTotalConnectedDevicesList)
                    {
                        if (item.TestNumber == testnumber)
                        {
                            clsTotalConnectedDevicesList[item.TestNumber - 1] = new clsTotalConnectedDevices { TestNumber = item.TestNumber, TestresultDevice1 = item.TestresultDevice1, TestresultDevice2 = item.TestresultDevice2, TestresultDevice3 = item.TestresultDevice3, TestresultDevice4 = item.TestresultDevice4, TestresultDevice5 = status, TestresultDevice6 = item.TestresultDevice6 };
                            break;
                        }
                    }
                    break;

                case 6:
                    foreach (clsTotalConnectedDevices item in clsTotalConnectedDevicesList)
                    {
                        if (item.TestNumber == testnumber)
                        {
                            clsTotalConnectedDevicesList[item.TestNumber - 1] = new clsTotalConnectedDevices { TestNumber = item.TestNumber, TestresultDevice1 = item.TestresultDevice1, TestresultDevice2 = item.TestresultDevice2, TestresultDevice3 = item.TestresultDevice3, TestresultDevice4 = item.TestresultDevice4, TestresultDevice5 = item.TestresultDevice5, TestresultDevice6 = status};
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void DisplayMessage(int DeviceNumber, int msgID)
        {
            switch (DeviceNumber)
            {
                case 1:
                    OkTestBtnVisDUT1 = true;
                    strmsgDUT1 = clsMessages.DisplayMessage(msgID);                    
                    break;

                case 2:
                    OkTestBtnVisDUT2 = true;
                    strmsgDUT2 = clsMessages.DisplayMessage(msgID);
                    break;

                case 3:
                    OkTestBtnVisDUT3 = true;
                    strmsgDUT3 = clsMessages.DisplayMessage(msgID);
                    break;

                case 4:
                    OkTestBtnVisDUT4 = true;
                    strmsgDUT4 = clsMessages.DisplayMessage(msgID);
                    break;

                case 5:
                    OkTestBtnVisDUT5 = true;
                    strmsgDUT5 = clsMessages.DisplayMessage(msgID);
                    break;

                case 6:
                    OkTestBtnVisDUT6 = true;
                    strmsgDUT6 = clsMessages.DisplayMessage(msgID);
                    break;

                default:
                    break;
            }
        }
        public void DisplayMessage(int DeviceNumber, string msgID)
        {
            switch (DeviceNumber)
            {
                case 1:
                    OkTestBtnVisDUT1 = true;
                    strmsgDUT1 = msgID;
                    break;

                case 2:
                    OkTestBtnVisDUT2 = true;
                    strmsgDUT2 = msgID;
                    break;

                case 3:
                    OkTestBtnVisDUT3 = true;
                    strmsgDUT3 = msgID;
                    break;

                case 4:
                    OkTestBtnVisDUT4 = true;
                    strmsgDUT4 = msgID;
                    break;

                case 5:
                    OkTestBtnVisDUT5 = true;
                    strmsgDUT5 = msgID;
                    break;

                case 6:
                    OkTestBtnVisDUT6 = true;
                    strmsgDUT6 = msgID;
                    break;

                default:
                    break;
            }
        }
        public void OpenJigCOMPort()
        {
            switch (clsGlobalVariables.NUMBER_OF_DUTS)
            {
                case 1:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    break;
                case 2:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    break;
                case 3:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT3, true);
                    break;
                case 4:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT3, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT4, true);
                    break;
                case 5:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT3, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT4, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT5.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT5, true);
                    break;
                case 6:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT3, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT4, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT5.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT5, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT6.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT6, true);
                    break;
                default:
                    break;
            }
            MainWindowVM.initilizeCommonObject.objJIGSerialComm.OpenCommPort(clsGlobalVariables.strgComPortJIG, false, true);
            MainWindowVM.initilizeCommonObject.objplcSerialComm.OpenCommPort(clsGlobalVariables.strgComPortPLC, false);
            MainWindowVM.initilizeCommonObject.objJIGSerialComm.uiDataEndTimeout = 50;
        }

        private void FailurHandel()
        {
            EnableDisableUI(true);
            CloseAllComport();
            StartStopWatch(false);
        }
        private void EnableDisableUI(bool v)
        {
            //StartBtnVis = v;
            //StopBtnVis = !v;
        }

        public void CloseAllComport()
        {
            MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.CloseCommPort();
            MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT2.CloseCommPort();
            MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT3.CloseCommPort();
            MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT4.CloseCommPort();
            MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT5.CloseCommPort();
            MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT6.CloseCommPort();
            MainWindowVM.initilizeCommonObject.objJIGSerialComm.CloseCommPort();
            MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();            
        }
        private void GetListOfAllEnabledtests(CatIdList catId)
        {
            
            foreach (var Grpname in catId.ListOfGroupSequence)
            {
                switch (Grpname)
                {
                    case "Common Tests":
                        AddCommonTests(catId);
                        break;

                    case "Analog Input Tests":
                        AddAnalogInputTests(catId);
                        break;

                    case "Analog Output Tests":
                        AddAnalogOutputTests(catId);
                        break;

                    case "TC RTD Tests":
                        AddTCRTDTests(catId);
                        break;

                    case "Relay,SSR Tests":
                        AddRelaySSRTests(catId);
                        break;

                    case "Calibration Constant Tests":
                        AddCalibConstantTests(catId);
                        break;

                    default:
                        break;
                }
                
                
            }
        }
		private void AddCommonTests(CatIdList catId)
        {
            if (catId.CommonCalibTests[0].READ_DEVICE_ID)            
                ListOfTests.Add("READ_DEVICE_ID");

            if (catId.CommonCalibTests[0].READ_CALIB_CONST)
                ListOfTests.Add("READ_CALIB_CONST");

            if (catId.CommonCalibTests[0].SWITCH_SENSOR_RELAY)
                ListOfTests.Add("SWITCH_SENSOR_RELAY");
            
            if (catId.CommonCalibTests[0].START_DISP_TEST)
                ListOfTests.Add("START_DISP_TEST");

            if (catId.CommonCalibTests[0].START_KEYPAD_TEST)
                ListOfTests.Add("START_KEYPAD_TEST");

            if (catId.CommonCalibTests[0].Vtg24V_OP_TEST)
                ListOfTests.Add("24V_OP_TEST");

            if (catId.CommonCalibTests[0].START_MODBUS_TEST)
                ListOfTests.Add("START_MODBUS_TEST");

            if (catId.CommonCalibTests[0].CJC_TEST)
                ListOfTests.Add("CJC_TEST");

        }

        private void AddCalibConstantTests(CatIdList catId)
        {
            if (catId.CalibrationConstantsTests[0].WRITE_CALIB_CONST)
                ListOfTests.Add("WRITE_CALIB_CONST");

            if (catId.CalibrationConstantsTests[0].WRITE_CALIB_CONST_WITH_VREF)
                ListOfTests.Add("WRITE_CALIB_CONST_WITH_VREF");
           
        }

        private void AddRelaySSRTests(CatIdList catId)
        {
            //if (catId.RelayOrSSRTests[0].SLAVE1_OP1_ON)
            //    ListOfTests.Add("SLAVE1_OP1_ON");

            //if (catId.RelayOrSSRTests[0].SLAVE1_OP1_OFF)
            //    ListOfTests.Add("SLAVE1_OP1_OFF");

            //if (catId.RelayOrSSRTests[0].START_REL_TEST)
            //    ListOfTests.Add("START_REL_TEST");

            //if (catId.RelayOrSSRTests[0].DUT_OP1_ON)
            //    ListOfTests.Add("DUT_OP1_ON");

            //if (catId.RelayOrSSRTests[0].DUT_OP1_OFF)
            //    ListOfTests.Add("DUT_OP1_OFF");

            //if (catId.RelayOrSSRTests[0].DUT_OP2_ON)
            //    ListOfTests.Add("DUT_OP2_ON");

            //if (catId.RelayOrSSRTests[0].DUT_OP2_OFF)
            //    ListOfTests.Add("DUT_OP2_OFF");

            //if (catId.RelayOrSSRTests[0].DUT_OP3_ON)
            //    ListOfTests.Add("DUT_OP3_ON");

            //if (catId.RelayOrSSRTests[0].DUT_OP3_OFF)
            //    ListOfTests.Add("DUT_OP3_OFF");

            //if (catId.RelayOrSSRTests[0].SLAVE1_OP2_ON)
            //    ListOfTests.Add("SLAVE1_OP2_ON");

            //if (catId.RelayOrSSRTests[0].SLAVE1_OP3_ON)
            //    ListOfTests.Add("SLAVE1_OP3_ON");

            //if (catId.RelayOrSSRTests[0].CONVERTOR_OP1_ON)
            //    ListOfTests.Add("CONVERTOR_OP1_ON");

            //if (catId.RelayOrSSRTests[0].CONVERTOR_OP2_ON)
            //    ListOfTests.Add("CONVERTOR_OP2_ON");

            //if (catId.RelayOrSSRTests[0].SLAVE1_READ_ADC_CNT_RLY_ON)
            //    ListOfTests.Add("SLAVE1_READ_ADC_CNT_RLY_ON");

            //if (catId.RelayOrSSRTests[0].SLAVE1_READ_ADC_CNT_RLY_OFF)
            //    ListOfTests.Add("SLAVE1_READ_ADC_CNT_RLY_OFF");

            //if (catId.RelayOrSSRTests[0].CONVERTOR_OP1_OFF)
            //    ListOfTests.Add("CONVERTOR_OP1_OFF");

            //if (catId.RelayOrSSRTests[0].CONVERTOR_OP2_OFF)
            //    ListOfTests.Add("CONVERTOR_OP2_OFF");

            //if (catId.RelayOrSSRTests[0].SLAVE1_OP3_OFF)
            //    ListOfTests.Add("SLAVE1_OP3_OFF");

            //if (catId.RelayOrSSRTests[0].SLAVE2_OP3_ON)
            //    ListOfTests.Add("SLAVE2_OP3_ON");

            //if (catId.RelayOrSSRTests[0].SLAVE3_OP3_ON)
            //    ListOfTests.Add("SLAVE3_OP3_ON");

            //if (catId.RelayOrSSRTests[0].SLAVE2_READ_ADC_CNT_RLY_OFF)
            //    ListOfTests.Add("SLAVE2_READ_ADC_CNT_RLY_OFF");

            //if (catId.RelayOrSSRTests[0].SLAVE2_READ_ADC_CNT_RLY_ON)
            //    ListOfTests.Add("SLAVE2_READ_ADC_CNT_RLY_ON");

            //if (catId.RelayOrSSRTests[0].START_REL_TEST_PI)
            //    ListOfTests.Add("START_REL_TEST_PI");

        }


        private void AddAnalogOutputTests(CatIdList catId)
        {
            if (catId.AnalogOpTests[0].SET_DFALT_1MA_CNT)
                ListOfTests.Add("SET_DFALT_1MA_CNT");

            if (catId.AnalogOpTests[0].SET_DFALT_4MA_CNT)
                ListOfTests.Add("SET_DFALT_4MA_CNT");

            if (catId.AnalogOpTests[0].SET_OBSRVED_1MA_CNT)
                ListOfTests.Add("SET_OBSRVED_1MA_CNT");

            if (catId.AnalogOpTests[0].SET_OBSRVED_4MA_CNT)
                ListOfTests.Add("SET_OBSRVED_4MA_CNT");

            if (catId.AnalogOpTests[0].SET_DFALT_20MA_CNT)
                ListOfTests.Add("SET_DFALT_20MA_CNT");

            if (catId.AnalogOpTests[0].SET_OBSRVED_20MA_CNT)
                ListOfTests.Add("SET_OBSRVED_20MA_CNT");

            if (catId.AnalogOpTests[0].CALIBRATE_CURRENT)
                ListOfTests.Add("CALIBRATE_CURRENT");

            if (catId.AnalogOpTests[0].SET_12MA_ANLOP)
                ListOfTests.Add("SET_12MA_ANLOP");

            if (catId.AnalogOpTests[0].CHK_ANALOG_OP_VAL)
                ListOfTests.Add("CHK_ANALOG_OP_VAL");

            //if (catId.AnalogOpTests[0].SLAVE2_OP1_ON)
            //    ListOfTests.Add("SLAVE2_OP1_ON");

            //if (catId.AnalogOpTests[0].SLAVE2_OP1_OFF)
            //    ListOfTests.Add("SLAVE2_OP1_OFF");

            //if (catId.AnalogOpTests[0].SLAVE2_OP2_ON)
            //    ListOfTests.Add("SLAVE2_OP2_ON");

            //if (catId.AnalogOpTests[0].SLAVE2_OP2_OFF)
            //    ListOfTests.Add("SLAVE2_OP2_OFF");

            if (catId.AnalogOpTests[0].SET_DFALT_1V_CNT)
                ListOfTests.Add("SET_DFALT_1V_CNT");

            if (catId.AnalogOpTests[0].SET_OBSRVED_1V_CNT)
                ListOfTests.Add("SET_OBSRVED_1V_CNT");

            if (catId.AnalogOpTests[0].SET_DFALT_10V_CNT)
                ListOfTests.Add("SET_DFALT_10V_CNT");

            if (catId.AnalogOpTests[0].SET_OBSRVED_10V_CNT)
                ListOfTests.Add("SET_OBSRVED_10V_CNT");

            if (catId.AnalogOpTests[0].CALIBRATE_VOLTAGE)
                ListOfTests.Add("CALIBRATE_VOLTAGE");

            if (catId.AnalogOpTests[0].SET_5V_ANLOP)
                ListOfTests.Add("SET_5V_ANLOP");
                        
        }

        private void AddAnalogInputTests(CatIdList catId)
        {
            if (catId.AnalogIpTests[0].CALIB_1V_CNT)
                ListOfTests.Add("CALIB_1V_CNT");

            if (catId.AnalogIpTests[0].CALIB_9V_CNT)
                ListOfTests.Add("CALIB_9V_CNT");

            if (catId.AnalogIpTests[0].CALIB_4mA_CNT)
                ListOfTests.Add("CALIB_4mA_CNT");

            if (catId.AnalogIpTests[0].CALIB_20mA_CNT)
                ListOfTests.Add("CALIB_20mA_CNT");

            if (catId.AnalogIpTests[0].CALIB_9V_CNT_PI)
                ListOfTests.Add("CALIB_9V_CNT_PI");

            if (catId.AnalogIpTests[0].CALIB_1V_CNT_PI)
                ListOfTests.Add("CALIB_1V_CNT_PI");

            if (catId.AnalogIpTests[0].CALIB_20mA_CNT_PI)
                ListOfTests.Add("CALIB_20mA_CNT_PI");

            if (catId.AnalogIpTests[0].CALIB_1mA_CNT_PI)
                ListOfTests.Add("CALIB_1mA_CNT_PI");

        }

        private void AddTCRTDTests(CatIdList catId)
        {
            if (catId.TC_RTDTests[0].CALIB_1_MV_CNT)
                ListOfTests.Add("CALIB_1_MV_CNT");

            if (catId.TC_RTDTests[0].CALIB_47_68_MV_CNT)
                ListOfTests.Add("CALIB_47_68_MV_CNT");

            if (catId.TC_RTDTests[0].CALIB_50_MV_CNT)
                ListOfTests.Add("CALIB_50_MV_CNT");

            if (catId.TC_RTDTests[0].CALC_SLOPE_OFFSET)
                ListOfTests.Add("CALC_SLOPE_OFFSET");

            if (catId.TC_RTDTests[0].CALIB_PT100)
                ListOfTests.Add("CALIB_PT100");

            if (catId.TC_RTDTests[0].CALIB_TC)
                ListOfTests.Add("CALIB_TC");

            if (catId.TC_RTDTests[0].CALIB_100_OHM)
                ListOfTests.Add("CALIB_100_OHM");

            if (catId.TC_RTDTests[0].CALIB_313_71_OHM)
                ListOfTests.Add("CALIB_313_71_OHM");
            
        }
        private void OpenProdConfigClk(object obj)
        {
            //PasswordWindow passwordWindow = new PasswordWindow();
            //var pwdRes = passwordWindow.ShowDialog();

            //if (pwdRes == false)
            //{
            //    MessageBox.Show("Incorrect password entered!","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
            //    //ShowMessageBox("Incorrect password entered!", false, "", clsGlobalVariables.MsgIcon.Error);
            //    return;
            //}

            ConfigurationWindow objconfiguration = new ConfigurationWindow();
            objconfiguration.ShowDialog();
        }

        private bool refresDataOfJsonFile()
        {
            try
            {
                string jsonFilePath = Directory.GetCurrentDirectory() + "\\Configuration.json";

                ConfigurationDataList result = new ConfigurationDataList();
                using (StreamReader file = File.OpenText(jsonFilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    result = (ConfigurationDataList)serializer.Deserialize(file, typeof(ConfigurationDataList));
                }
                CatId.Clear();
                CatId.Add(result);

                foreach (ConfigurationData item in result.ConfigurationData)
                {
                    DeviceTypeList.Add(item.DeviceType); 
                }
                return true;

                #region Cmt
                //string json = File.ReadAllText(jsonFilePath);
                //Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                //foreach (var item in json_Dictionary)
                //{
                //    var obj = item.Value;
                //} 
                #endregion

            }
            catch (Exception ex)
            {
                MyLogWriterDLL.LogWriter.WriteLog("In refreshDataOfJsonFile" + ex.Message);
                return false;
            }
        }

        private void AssignConfigurationDetailsToUI()
        {
            int found = 0;
            CatIdList Selectedcatid = new CatIdList();

            for (int catid = 0; catid < CatId.Count; catid++)
            {
                for (int ConfigData = 0; ConfigData < CatId[catid].ConfigurationData.Count; ConfigData++)
                {
                    for (int CatIDList = 0; CatIDList < CatId[catid].ConfigurationData[ConfigData].CatIdLists.Count; CatIDList++)
                    {
                        if (CatId[catid].ConfigurationData[ConfigData].CatIdLists[CatIDList].DeviceName == SelectedDeviceName)
                        {
                            Selectedcatid = CatId[catid].ConfigurationData[ConfigData].CatIdLists[CatIDList];
                            clsGlobalVariables.Selectedcatid = Selectedcatid;
                            accuracyTestsName.Clear();
                            FillCollectionofAccuracy();
                            Description = Selectedcatid.Description;
                            found = 1;
                            break;
                        }
                    }
                    if (found == 1)
                        break;
                }
                if (found == 1)
                    break;
            }
        }

        private void BtnOpenDeviceSelWindow(object obj)
        {
            ShowProductSelectionWindow();
        }

        private void BtnHelpfileclk(object obj)
        {
            
        }

        private void BtnChangeComSettingsclk(object obj)
        {            
            GetPortList();
            //SelectedComPort = clsGlobalVariables.SelectedComPort;
            //SelectedBaudRate = clsGlobalVariables.SelectedBaudRate;
            ShowCommunicationSettingWindow();
        }

        private void ShowCommunicationSettingWindow()
        {
            YesNoBtnVis = false;
            OkbtnVis = true;
            MesssageVis = false;
            CancelBtnVis = true;
            ProductSelectionVis = false;
            ComSelectionVis = true;
            
            Sender = "CommunicationSettingWindow";
            IsDialogOpen = true;
        }

        /// <summary>
        /// GetPortList()
        /// This function gets the list of all available COM ports connected to PC
        /// and assigns it to the COM port combobox List. 
        /// </summary>
        private void GetPortList()
        {
            PortList.Clear();
            string[] GetPorts = SerialPort.GetPortNames();
            foreach (string item in GetPorts)
            {
                PortList.Add(item);
            }
        }
        private void BtnExitclk(object obj)
        {
            System.Windows.Application.Current.Shutdown();
            Environment.Exit(0);
        }
        /// <summary>
        /// Open product selection window
        /// All other parameters hided
        /// </summary>
        public void ShowProductSelectionWindow()
        {
            OkbtnVis = true;
            CancelBtnVis = false;
            MesssageVis = false;
            YesNoBtnVis = false;
            DeviceTypeError = false;
            DeviceNameError = false;
            ComSelectionVis = false;
            IsDialogOpen = true;
            ProductSelectionVis = true;
        }
        /// <summary>
        /// Command parameters are passed as ok, yes, cancel
        /// Here taken action on Ok button click
        /// Here taken action on yes button click
        /// Here taken action on cancel button click
        /// </summary>
        /// <param name="obj"></param>
        private void BtnOkclk(object obj)
        { 
            clsGlobalVariables.NUMBER_OF_DUTS = Convert.ToInt32(NumberOfDUTs);
            switch (obj.ToString())
            {
                case "Ok":
                    clsModelSettings.igDutID = Selectedcatid.DeviceId;
                    clsGlobalVariables.selectedDeviceType = GetDevicetypeFromString(SelectedDeviceType);
                    clsModelSettings.blnRS485Flag = Selectedcatid.ModbusSupport;
                    break;
                case "Yes":

                    break;
                case "Cancel":

                    break;
                default:
                    break;
            }
            clsGlobalVariables.NUMBER_OF_DUTS_List.Clear();
            for (int i = 1; i <= clsGlobalVariables.NUMBER_OF_DUTS; i++)
            {
                clsGlobalVariables.NUMBER_OF_DUTS_List.Add((byte)i);
            }
            hide();
        }

        private SelectedDeviceType GetDevicetypeFromString(string selectedDeviceType)
        {
            if (selectedDeviceType == clsGlobalVariables.PR69_96x96)            
                return clsGlobalVariables.SelectedDeviceType.PR69_96x96;
            else if (selectedDeviceType == clsGlobalVariables.PR69_48x48)
                return clsGlobalVariables.SelectedDeviceType.PR69_48x48;
            else if (selectedDeviceType == clsGlobalVariables.PR43_96x96)
                return clsGlobalVariables.SelectedDeviceType.PR43_96x96;
            else if (selectedDeviceType == clsGlobalVariables.PR43_48x48)
                return clsGlobalVariables.SelectedDeviceType.PR43_48x48;
            else if (selectedDeviceType == clsGlobalVariables.PI)
                return clsGlobalVariables.SelectedDeviceType.PI;
            else
            {
                MessageBox.Show("Not Implemented");
                return 0;
            }

           

        }

        private void hide()
        {
            IsDialogOpen = false;
            MesssageVis = false;
            OkbtnVis = false;
            CancelBtnVis = false;
            YesNoBtnVis = false;
            ProductSelectionVis = false;
        }
                
        private void ShowMessageBox(string msg, bool yesNo, string sender, clsGlobalVariables.MsgIcon icon)
        {
            switch (icon)
            {
                case clsGlobalVariables.MsgIcon.Message:
                    IconMsgVis = true;
                    IconErrorVis = false;
                    IconQuestionVis = false;
                    break;
                case clsGlobalVariables.MsgIcon.Error:
                    IconMsgVis = false;
                    IconErrorVis = true;
                    IconQuestionVis = false;
                    break;
                case clsGlobalVariables.MsgIcon.Question:
                    IconMsgVis = false;
                    IconErrorVis = false;
                    IconQuestionVis = true;
                    break;
                default:
                    IconMsgVis = true;
                    IconErrorVis = false;
                    break;
            }
            YesNoBtnVis = yesNo;
            CancelBtnVis = yesNo;
            OkbtnVis = !yesNo;
            MesssageVis = true;
            Msg = msg;
            Sender = sender;
            IsDialogOpen = true;
        }

        //Stopwatch purpose : Calculate time required for all calibration and programming
        Stopwatch objstpWatch = new Stopwatch(); //Added for Calibration Timer
        public System.Threading.TimerCallback tmrCallback;
        public System.Threading.Timer tmrMbTimer;
        
        /// <summary>
        /// StartStopWatch()
        /// This function is used to start stopwatch to display time on UI.
        /// Stopwatch is initialised with value 00:00:00
        /// </summary>
        private void StartStopWatch(bool state)
        {
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
                MessageBox.Show("In Timer Thread");
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
    }
}
