
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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel 
{

    public class MainWindowVM : INotifyPropertyChanged
    {
        public static InitilizeCommonObject initilizeCommonObject = new InitilizeCommonObject();
        //Constructor
        public MainWindowVM()
        {
            
            btnOkCmd = new RelayCommand(BtnOkclk);
            exitCmd = new RelayCommand(BtnExitclk);
            ComSettingCmd = new RelayCommand(BtnChangeComSettingsclk);
            OpenHelpfileCmd = new RelayCommand(BtnHelpfileclk);
            OpenDeviceSelectionWindow = new RelayCommand(BtnOpenDeviceSelWindow);
            OpenProdConfigWindow = new RelayCommand(OpenProdConfigClk);

            _btnStart = new RelayCommand(btnStartClk);

            _PortList = new ObservableCollection<string>();
            _DeviceList = new ObservableCollection<string>();
            _DeviceNameList = new ObservableCollection<string>();
            _CatId = new ObservableCollection<ConfigurationDataList>();
            _clsTotalTestsGroups = new ObservableCollection<clsTotalTestsGroups>();
            _TotalConnectedDevicesList = new ObservableCollection<TotalConnectedDevices>();

            ShowProductSelectionWindow();

            if (!refresDataOfJsonFile())
            {
                ShowMessageBox("Error while reading data from json, something is wrong in json OR check json file path", false, "", clsGlobalVariables.MsgIcon.Error);
                return;
            }

            Is48TypeCatId = true;
            IsProductSelected = true;

            MyLogWriterDLL.LogWriter logWritter = new MyLogWriterDLL.LogWriter();
            logWritter.LogWriterStatus();

            DUT1DetailsVis = false;
            DUT2DetailsVis = false;
            DUT3DetailsVis = false;
            DUT4DetailsVis = false;
            DUT5DetailsVis = false;
            DUT6DetailsVis = false;
            TestsDetailsVis = false;
        }
        private RelayCommand _btnStart;

        public RelayCommand btnStart
        {
            get { return _btnStart; }
            set { _btnStart = value; }
        }

        private string _StopwatchTime;

        public string StopwatchTime
        {
            get { return _StopwatchTime; }
            set { _StopwatchTime = value; OnPropertyChanged("StopwatchTime"); }
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
                                clsTotalTestsGroups.Clear();                                
                                int count = 0;
                                foreach (string testgrp in CatId[0].ConfigurationData[devtype].CatIdLists[catid].ListOfGroupSequence)
                                {
                                    clsTotalTestsGroups.Add(new clsTotalTestsGroups() { TestNumber = count + 1, TestGroup = testgrp });
                                    count++;
                                }
                                TestsDetailsVis = true;
                                IsProductSelected = true;
                            }
                        }
                    }

                }
                else
                    IsProductSelected = false;
                AssignConfigurationDetailsToUI();

                NumberOfDUTs = 0;

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

        private bool _Is48TypeCatId;

        public bool Is48TypeCatId
        {
            get { return _Is48TypeCatId; }
            set
            {
                _Is48TypeCatId = value;
                if (_Is48TypeCatId)
                {                   
                    DeviceTypeList.Clear();
                    DeviceNameList.Clear();

                    clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PR69_48x48;

                    for (int devtype = 0; devtype < CatId[0].ConfigurationData.Count; devtype++)
                    {
                        if (CatId[0].ConfigurationData[devtype].DeviceType.Contains("48"))
                        {
                            DeviceTypeList.Add(CatId[0].ConfigurationData[devtype].DeviceType);
                        }
                    }
                    OnPropertyChanged("Is48TypeCatId");
                }
            }
        }

        private bool _Is96TypeCatId;

        public bool Is96TypeCatId
        {
            get { return _Is96TypeCatId; }
            set
            {
                _Is96TypeCatId = value;
                if (_Is96TypeCatId)
                {                    
                    DeviceTypeList.Clear();
                    DeviceNameList.Clear();
                    clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PR69_96x96;

                    for (int devtype = 0; devtype < CatId[0].ConfigurationData.Count; devtype++)
                    {
                        if (CatId[0].ConfigurationData[devtype].DeviceType.Contains("96"))
                        {
                            DeviceTypeList.Add(CatId[0].ConfigurationData[devtype].DeviceType);
                        }
                    }

                    OnPropertyChanged("Is96TypeCatId");
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
        
        private ObservableCollection<clsTotalTestsGroups> _clsTotalTestsGroups;

        public ObservableCollection<clsTotalTestsGroups> clsTotalTestsGroups
        {
            get { return _clsTotalTestsGroups; }
            set { _clsTotalTestsGroups = value; OnPropertyChanged("clsTotalTestsGroups"); }
        }
        

        private ObservableCollection<TotalConnectedDevices> _TotalConnectedDevicesList;

        public ObservableCollection<TotalConnectedDevices> TotalConnectedDevicesList
        {
            get { return _TotalConnectedDevicesList; }
            set { _TotalConnectedDevicesList = value; OnPropertyChanged("TotalConnectedDevicesList"); }
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

        private int _NumberOfDUTs;

        public int NumberOfDUTs
        {
            get { return _NumberOfDUTs; }
            set
            {
                _NumberOfDUTs = value;

                switch (_NumberOfDUTs)
                {
                    case 0:
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
                        
                        //MessageBox.Show("Please enter at least 1 device for calibration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        //NumberOfDUTs = 1;
                      
                        break;
                    case 1:
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
                    case 2:
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
                    case 3:
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
                    case 4:
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
                    case 5:
                        DUT1DetailsVis = true;
                        DUT2DetailsVis = true;
                        DUT3DetailsVis = true;
                        DUT4DetailsVis = true;
                        DUT5DetailsVis = true;
                        DUT6DetailsVis = false;

                        DeviceNumber1Vis = true;
                        DeviceNumber2Vis = true;
                        DeviceNumber3Vis = true;
                        DeviceNumber4Vis = true;
                        DeviceNumber5Vis = true;
                        DeviceNumber6Vis = false;

                        break;
                    case 6:
                        DUT1DetailsVis = true;
                        DUT2DetailsVis = true;
                        DUT3DetailsVis = true;
                        DUT4DetailsVis = true;
                        DUT5DetailsVis = true;
                        DUT6DetailsVis = true;

                        DeviceNumber1Vis = true;
                        DeviceNumber2Vis = true;
                        DeviceNumber3Vis = true;
                        DeviceNumber4Vis = true;
                        DeviceNumber5Vis = true;
                        DeviceNumber6Vis = true;

                        break;


                    default:                      
                        //ShowMessageBox("Please enter total number of devices less than or equal to 6.", false, "InvalidDUT_No", clsGlobalVariables.MsgIcon.Error);
                        MessageBox.Show("Please enter total number of devices less than or equal to 6.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                        NumberOfDUTs = 1;
                        break;
                }
                                
                if (_NumberOfDUTs > 0 && _NumberOfDUTs < 7)
                {                  
                    IsProductSelected = true;
                    TestsDetailsVis = true;
                }
                else
                {
                    TestsDetailsVis = false;
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
                

        private void btnStartClk(object obj)
        {
            CatIdList catId = clsGlobalVariables.Selectedcatid;
            //Port detection.
            
            //clsModelSettings.igDutID  need to set deive iD


            //Type type = clsGlobalVariables.Selectedcatid.AnalogIpTests[0].GetType();
            //PropertyInfo[] props = type.GetProperties();
            StartStopWatch(true);

            clsGlobalVariables.objGlobalFunction.ApplyDelay(5000);
            EnableDisableUI(false);
            clsGlobalVariables.NUMBER_OF_DUTS = NumberOfDUTs;
            //If usr change number of device then need to find com port again.
            if (clsGlobalVariables.NUMBER_OF_DUTS != clsGlobalVariables.OLD_NUMBER_OF_DUTS)
            {
                clsGlobalVariables.blngIsComportDetected = false;
                clsGlobalVariables.blngIsComportDetectedForPLC = false;
            }
            if (clsGlobalVariables.objGlobalFunction.AutomaticCOMPortDetections(NumberOfDUTs) != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                //imNumOfTests = clsGlobalVariables.algTests_Auto.Count;
                //almTempTestList = new ArrayList(clsGlobalVariables.algTests_Auto);
                FailurHandel();
                //PLC off
            }
            else
            {
                
            }
            MainWindowVM.initilizeCommonObject.objJIGSerialComm.OpenCommPort(clsGlobalVariables.strgComPortJIG, false);
            MainWindowVM.initilizeCommonObject.objJIGSerialComm.uiDataEndTimeout = 50;
            OpenJigCOMPort();
            byte btmRetVal;
            int imLoopCntr;
            int imNumOfTests;
            ArrayList almTempTestList = null;
            //    btmRetVal = JIGInitializationTests();.
            
            
            
            //PIB12C
            clsGlobalVariables.algTests_Auto.Add("READ_DEVICE_ID");
            clsGlobalVariables.algTests_Auto.Add("READ_CALIB_CONST_STATUS");
            clsGlobalVariables.algTests_Auto.Add("SWITCH_SENSOR_RELAY");
            clsGlobalVariables.algTests_Auto.Add("START_DISP_TEST");
            clsGlobalVariables.algTests_Auto.Add("START_KEYPAD_TEST");
            clsGlobalVariables.algTests_Auto.Add("START_REL_TEST_PI");
            clsGlobalVariables.algTests_Auto.Add("24V_OP_TEST");
            clsGlobalVariables.algTests_Auto.Add("START_MODBUS_TEST");
            clsGlobalVariables.algTests_Auto.Add("CJC_TEST");
            clsGlobalVariables.algTests_Auto.Add("SET_DFALT_1MA_CNT");
            clsGlobalVariables.algTests_Auto.Add("SET_OBSRVED_1MA_CNT");
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
            clsGlobalVariables.algTests_Auto.Add("CALIB_9V_CNT_PI");
            clsGlobalVariables.algTests_Auto.Add("CALIB_1V_CNT_PI");
            clsGlobalVariables.algTests_Auto.Add("CALIB_20mA_CNT_PI");
            clsGlobalVariables.algTests_Auto.Add("CALIB_1mA_CNT_PI");
            clsGlobalVariables.algTests_Auto.Add("WRITE_CALIB_CONST");


           imNumOfTests = clsGlobalVariables.algTests_Auto.Count;
           almTempTestList = new ArrayList(clsGlobalVariables.algTests_Auto);
            
           
            //Data log object is cleared here. Default data will be written into all the data menbers of this object.
            //clsGlobalVariables.objDataLog.Clear();
            //--------Changed By Shubham
            //Date:- 30-03-2018
            //Version:- V16
            //statement:- VREF text box value is cleared here.
            //txtVREF.Text = "";
            //-------Changes End.

            //prgbar.Maximum = imNumOfTests;
            //prgbar.Value = prgbar.Minimum;
            //This timeout is resseted here to original.
            clsGlobalVariables.ig_Query_TimeOut = 16000;
            for (imLoopCntr = 0; imLoopCntr < imNumOfTests; ++imLoopCntr)
            {
                if (clsGlobalVariables._StopFlag)
                {
                    
                    clsGlobalVariables.objGlobalFunction.PLC_OFF();
                    CloseAllComport();
                    EnableDisableUI(true);
                    return;
                }
                btmRetVal = clsGlobalVariables.objTestJIGFunctions.TestDUT(almTempTestList[imLoopCntr].ToString());

                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Accuracy_Test_Not_Done)
                {
                   
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
                    //CA55  txtProgressInfo.Text = txtProgressInfo.Text + Environment.NewLine + "Test" + (imLoopCntr + 1) + " Pass." + "(" + almTempTestList[imLoopCntr] + ")";
                    //CA55  txtProgressInfo.SelectionStart = txtProgressInfo.Text.Length - 1;
                    //Fix delay of 300msec is applied here to maintain query and response timeout compatibility with VB6.0 software.
                    //This delay is added by observing the query and response of the old VB software.
                    clsGlobalVariables.objGlobalFunction.ApplyDelay(300);
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

        }

        private void OpenJigCOMPort()
        {
            switch (clsGlobalVariables.NUMBER_OF_DUTS)
            {
                case 1:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    break;
                case 2:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    break;
                case 3:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT3, true);
                    break;
                case 4:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT3, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT4, true);
                    break;
                case 5:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT3, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT4, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT5, true);
                    break;
                case 6:
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT1, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT2, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT3, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT4, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT5, true);
                    MainWindowVM.initilizeCommonObject.objCalibratorSerialDUT1.OpenCommPort(clsGlobalVariables.strgComPortCalibratorDUT6, true);
                    break;
                default:
                    break;
            }
        }

        private void FailurHandel()
        {
            EnableDisableUI(true);
            CloseAllComport();
            StartStopWatch(false);
        }

        private void EnableDisableUI(bool v)
        {
            
        }

        private void CloseAllComport()
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

        private void OpenProdConfigClk(object obj)
        {
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
            CancelBtnVis = true;
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
            switch (obj.ToString())
            {
                case "Ok":

                    break;
                case "Yes":

                    break;
                case "Cancel":

                    break;
                default:
                    break;
            }
            hide();
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
