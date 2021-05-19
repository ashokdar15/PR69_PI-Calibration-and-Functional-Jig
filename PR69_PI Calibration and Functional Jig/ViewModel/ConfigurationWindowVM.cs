using Newtonsoft.Json;
using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{
    public class ConfigurationWindowVM : INotifyPropertyChanged
    {

        #region Constructor
        public ConfigurationWindowVM()
        {
            _ModifiedCatId = new ObservableCollection<ConfigurationDataList>();
            _CatId = new ObservableCollection<ConfigurationDataList>();

            _AnalogInputTestsDetails = new ObservableCollection<AnalogInputTests>();
            _analogOutputTestsDetails = new ObservableCollection<AnalogOutputTests>();
            _tcRTDTestsDetails = new ObservableCollection<TC_RTDCalibTests>();
            _relayOrSSRTestsDetails = new ObservableCollection<RelayORSSRTests>();
            _calibrationconstDetails = new ObservableCollection<CalibrationConstants>();
            _commonTestsDetails = new ObservableCollection<CommonTests>();

            _OP1subtestList = new ObservableCollection<string>();
            _OP2subtestList = new ObservableCollection<string>();
            _OP3subtestList = new ObservableCollection<string>();

            _ListOfGroupSequence = new ObservableCollection<string>();
            _EditUptestgrpCmd = new RelayCommand(EditUptestgrpClk);
            _EditDowntestgrpCmd = new RelayCommand(EditDowntestgrpClk);

            _EditUpAccuracytestCmd = new RelayCommand(EditUpAccuracytestClk);
            _EditDownAccuracytestgrpCmd = new RelayCommand(EditDownAccuracytestClk);

            _SaveCmd = new RelayCommand(SaveBtnClk);
            _AddSeriesCmd = new RelayCommand(AddSeriesClk);
            _AddCatIdCmd = new RelayCommand(AddCatIdClk);
            _DeleteSeriesCmd = new RelayCommand(DeleteSeriesClk);
            _EditCatIdCmd = new RelayCommand(EditCatIdClk);
            _DeleteCatIdCmd = new RelayCommand(DeleteCatIdClk);
            _BtnYesCmd = new RelayCommand(BtnYesClk);
            _BtnNoCmd = new RelayCommand(BtnNoClk);

            _EditAnalogInputTestsCmd = new RelayCommand(EditAnalogInputTestsClk);
            _EditAnalogOutputTestsCmd = new RelayCommand(EditAnalogOutputTestsClk);
            _EditTC_RTDTestsCmd = new RelayCommand(EditTC_RTDTestsClk);
            _EditRelayTestsCmd = new RelayCommand(EditRelayTestsClk);
            _OutputTypeList = new ObservableCollection<string>();
            _RelayTypeList = new ObservableCollection<string>();
            _EditCalibConstantTestsCmd = new RelayCommand(EditCalibConstantTestsClk);
            _EditCommonTestsCmd = new RelayCommand(EditCommonTestsClk);

            _EditmAmpInputTestsCmd = new RelayCommand(EditmAmpInputTestsClk);
            _EditVoltInputTestsCmd = new RelayCommand(EditVoltInputTestsClk);
            _EditPT100sensorInputTestsCmd = new RelayCommand(EditPT100sensorInputTestsClk);
            _EditRsensorInputTestsCmd = new RelayCommand(EditRsensorInputTestsClk);
            _EditJsensorInputTestsCmd = new RelayCommand(EditJsensorInputTestsClk);

            _AccuracyTestSequenceList = new ObservableCollection<string>();

            _AccuracymAmpTestsDetails = new ObservableCollection<AccuracyTests>();
            _AccuracyVoltTestsDetails = new ObservableCollection<AccuracyTests>();
            _AccuracyPT100SnsrTestsDetails = new ObservableCollection<AccuracyTests>();
            _AccuracyRSensorTestsDetails = new ObservableCollection<AccuracyTests>();
            _AccuracyJSensorTestsDetails = new ObservableCollection<AccuracyTests>();

            _CalibrationDelaysDetails = new ObservableCollection<CalibrationDelays>();
            _CalibrationDelaysDetailsPR43 = new ObservableCollection<CalibrationDelaysPR43>();
            _CalibrationDelaysPIDetails = new ObservableCollection<CalibrationDelaysPI>();
            _TolerancesofPIDetails = new ObservableCollection<TolerancesOfPI>();
            _TolerancesofPR69Details = new ObservableCollection<TolerancesOfPR69>();
            _EditTolerancePR69Cmd = new RelayCommand(EditTolerancePR69Clk);
            _EditCalibrationDelaysCmd = new RelayCommand(EditCalibrationDelaysClk);
            _EditCalibrationDelaysPR43Cmd = new RelayCommand(EditCalibrationDelaysPR43Clk);
            _EditCalibrationDelaysPICmd = new RelayCommand(EditCalibrationDelaysPIClk);
            _EditToleranceCmd = new RelayCommand(EditToleranceClk);
            _SaveDelayToleranceCmd = new RelayCommand(SaveDelayToleranceClk);
            _BrowsemotfileCmd = new RelayCommand(BrowsemotfileClk);

            string jsonFilePath = Directory.GetCurrentDirectory() + "\\Configuration.json";
            clsGlobalVariables.configJsonfilepath = jsonFilePath;
            ConfigurationDataList result = new ConfigurationDataList();
            using (StreamReader file = File.OpenText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (ConfigurationDataList)serializer.Deserialize(file, typeof(ConfigurationDataList));
            }

            OriginalCatId.Add(result);
            ModifiedCatId.Add(result);

            IsSaveBtnVis = false;
            
        }

        private void EditCalibrationDelaysPR43Clk(object obj)
        {
            clsCalibrationDelaysPR43 = new clsCalibrationDelaysPR43();
            clsCalibrationDelaysPR43.ParseCalibrationDelays(ModifiedCatId);

            EventSender = "SaveCalibDelaysPR43";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            TC_RTDTestsVis = false;
            RelayOrSSRTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            AccuracyDetailsEditVis = false;

            CalibDelaysPR43EditVis = true;
            CalibDelaysEditVis = false; 
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;

            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }

        private void EditDownAccuracytestClk(object obj)
        {
            try
            {
                ObservableCollection<string> ChangedListOfAccSequence = new ObservableCollection<string>();

                ChangedListOfAccSequence = AccuracyTestSequenceList;
                //ListOfGroupSequence.Clear();

                var selectedIndex = this.AccuracylistviewIndex;

                if (selectedIndex + 1 < ChangedListOfAccSequence.Count)
                {
                    var itemToMoveDown = ChangedListOfAccSequence[selectedIndex];
                    ChangedListOfAccSequence.RemoveAt(selectedIndex);
                    ChangedListOfAccSequence.Insert(selectedIndex + 1, itemToMoveDown);
                    AccuracylistviewIndex = selectedIndex + 1;
                }

                AccuracyTestSequenceList = ChangedListOfAccSequence;
            }
            catch (Exception)
            {

            }
        }

        private void EditUpAccuracytestClk(object obj)
        {
            try
            {
                ObservableCollection<string> ChangedListOfAccSequence = new ObservableCollection<string>();
                ChangedListOfAccSequence = AccuracyTestSequenceList;

                var selectedIndex = this.AccuracylistviewIndex;
                if (selectedIndex > 0)
                {
                    var itemToMoveUp = ChangedListOfAccSequence[selectedIndex];
                    ChangedListOfAccSequence.RemoveAt(selectedIndex);
                    ChangedListOfAccSequence.Insert(selectedIndex - 1, itemToMoveUp);
                    AccuracylistviewIndex = selectedIndex - 1;
                }

                AccuracyTestSequenceList = ChangedListOfAccSequence;
            }
            catch (Exception)
            {

            }
        }

        private void EditJsensorInputTestsClk(object obj)
        {            
            clsGlobalVariables.strAccuracyParameter = clsGlobalVariables.AccuracyParameter.JSensor;
            AccParameter = "J Sensor";
            EditAccuracyTestsDetails(obj);
        }

        private void EditRsensorInputTestsClk(object obj)
        {           
            clsGlobalVariables.strAccuracyParameter = clsGlobalVariables.AccuracyParameter.RSensor;
            AccParameter = "R Sensor";
            EditAccuracyTestsDetails(obj);
        }

        private void EditPT100sensorInputTestsClk(object obj)
        {                  
            clsGlobalVariables.strAccuracyParameter = clsGlobalVariables.AccuracyParameter.PT100Sensor;
            AccParameter = "PT100 Sensor";
            EditAccuracyTestsDetails(obj);
        }

        private void EditVoltInputTestsClk(object obj)
        {              
            clsGlobalVariables.strAccuracyParameter = clsGlobalVariables.AccuracyParameter.Volt;
            AccParameter = "Volt";
            EditAccuracyTestsDetails(obj);
        }
                
        private void EditmAmpInputTestsClk(object obj)
        {            


            clsGlobalVariables.strAccuracyParameter = clsGlobalVariables.AccuracyParameter.mAmp;
            AccParameter = "mAmp";
            EditAccuracyTestsDetails(obj);
        }
        public void EditAccuracyTestsDetails(object obj)
        {
            clsAccuracyTests = new clsAccuracyTests();
            int found = 0;

            for (int CounterCatId = 0; CounterCatId < ModifiedCatId.Count; CounterCatId++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterCatId].ConfigurationData.Count; CounterConfigData++)
                {
                    for (int CounterCatIdList = 0; CounterCatIdList < ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCatIdList++)
                    {
                        if (ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList].DeviceName == obj.ToString())
                        {
                            clsAccuracyTests.ParseAccuracyDetails(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList], clsGlobalVariables.strAccuracyParameter);
                            found = 1;
                        }
                    }
                    if (found == 1) { break; }
                }
                if (found == 1) { break; }
            }

            EventSender = "SaveAccuracyTests";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            RelayOrSSRTestsVis = false;
            TC_RTDTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;
            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            AccuracyDetailsEditVis = true;
            PopupBtnVisibility(true);
        }

        private void EditDowntestgrpClk(object obj)
        {
            try
            {
                ObservableCollection<string> ChangedListOfGrpSequence = new ObservableCollection<string>();

                ChangedListOfGrpSequence = ListOfGroupSequence;
                //ListOfGroupSequence.Clear();

                var selectedIndex = this.listviewIndex;

                if (selectedIndex + 1 < ChangedListOfGrpSequence.Count)
                {
                    var itemToMoveDown = ChangedListOfGrpSequence[selectedIndex];
                    ChangedListOfGrpSequence.RemoveAt(selectedIndex);
                    ChangedListOfGrpSequence.Insert(selectedIndex + 1, itemToMoveDown);
                    listviewIndex = selectedIndex + 1;
                }

                ListOfGroupSequence = ChangedListOfGrpSequence;
            }
            catch (Exception)
            {
                
            }
        }

        private void EditUptestgrpClk(object obj)
        {
            try
            {
                ObservableCollection<string> ChangedListOfGrpSequence = new ObservableCollection<string>();
                ChangedListOfGrpSequence = ListOfGroupSequence;

                var selectedIndex = this.listviewIndex;
                if (selectedIndex > 0)
                {
                    var itemToMoveUp = ChangedListOfGrpSequence[selectedIndex];
                    ChangedListOfGrpSequence.RemoveAt(selectedIndex);
                    ChangedListOfGrpSequence.Insert(selectedIndex - 1, itemToMoveUp);
                    listviewIndex = selectedIndex - 1;
                }

                ListOfGroupSequence = ChangedListOfGrpSequence;
            }
            catch (Exception)
            {
                
            }
        }

        #endregion

        private void PopupBtnVisibility(bool state)
        {           
            YesNoBtnVis = state;
            CancelBtnVis = !state;
            ErrorMsgVis = !state; //Ok btn
        }

        private RelayCommand _BtnYesCmd;

        public RelayCommand BtnYesCmd
        {
            get { return _BtnYesCmd; }
            set { _BtnYesCmd = value; }
        }

        private RelayCommand _BtnNoCmd;

        public RelayCommand BtnNoCmd
        {
            get { return _BtnNoCmd; }
            set { _BtnNoCmd = value; }
        }

        private ObservableCollection<ConfigurationDataList> _CatId;

        public ObservableCollection<ConfigurationDataList> OriginalCatId
        {
            get { return _CatId; }
            set { _CatId = value; OnPropertyChanged("OriginalCatId"); }
        }

        private ObservableCollection<ConfigurationDataList> _ModifiedCatId;

        public ObservableCollection<ConfigurationDataList> ModifiedCatId
        {
            get { return _ModifiedCatId; }
            set { _ModifiedCatId = value; OnPropertyChanged("ModifiedCatId"); }
        }

        private ObservableCollection<CalibrationDelays> _CalibrationDelaysDetails;

        public ObservableCollection<CalibrationDelays> CalibrationDelaysDetails
        {
            get { return _CalibrationDelaysDetails; }
            set { _CalibrationDelaysDetails = value; OnPropertyChanged("CalibrationDelaysDetails"); }
        }

        private ObservableCollection<CalibrationDelaysPR43> _CalibrationDelaysDetailsPR43;

        public ObservableCollection<CalibrationDelaysPR43> CalibrationDelaysDetailsPR43
        {
            get { return _CalibrationDelaysDetailsPR43; }
            set { _CalibrationDelaysDetailsPR43 = value; OnPropertyChanged("CalibrationDelaysDetailsPR43"); }
        }

        private ObservableCollection<CalibrationDelaysPI> _CalibrationDelaysPIDetails;

        public ObservableCollection<CalibrationDelaysPI> CalibrationDelaysPIDetails
        {
            get { return _CalibrationDelaysPIDetails; }
            set { _CalibrationDelaysPIDetails = value; OnPropertyChanged("CalibrationDelaysPIDetails"); }
        }

        private ObservableCollection<TolerancesOfPI> _TolerancesofPIDetails;

        public ObservableCollection<TolerancesOfPI> TolerancesofPIDetails
        {
            get { return _TolerancesofPIDetails; }
            set { _TolerancesofPIDetails = value; OnPropertyChanged("TolerancesofPIDetails"); }
        }

        private ObservableCollection<TolerancesOfPR69> _TolerancesofPR69Details;

        public ObservableCollection<TolerancesOfPR69> TolerancesofPR69Details
        {
            get { return _TolerancesofPR69Details; }
            set { _TolerancesofPR69Details = value; OnPropertyChanged("TolerancesofPR69Details"); }
        }

        private ObservableCollection<string> _RelayTypeList;

        public ObservableCollection<string> RelayTypeList
        {
            get { return _RelayTypeList; }
            set { _RelayTypeList = value; OnPropertyChanged("RelayTypeList"); }
        }

        private ObservableCollection<string> _OutputTypeList;

        public ObservableCollection<string> OutputTypeList
        {
            get { return _OutputTypeList; }
            set { _OutputTypeList = value; OnPropertyChanged("OutputTypeList"); }
        }

        private ObservableCollection<string> _OP1subtestList;

        public ObservableCollection<string> OP1subtestList
        {
            get { return _OP1subtestList; }
            set { _OP1subtestList = value; OnPropertyChanged("OP1subtestList"); }
        }

        private ObservableCollection<string> _OP2subtestList;

        public ObservableCollection<string> OP2subtestList
        {
            get { return _OP2subtestList; }
            set { _OP2subtestList = value; OnPropertyChanged("OP2subtestList"); }
        }

        private ObservableCollection<string> _OP3subtestList;

        public ObservableCollection<string> OP3subtestList
        {
            get { return _OP3subtestList; }
            set { _OP3subtestList = value; OnPropertyChanged("OP3subtestList"); }
        }


        #region Accuracy RelayCommands

        private RelayCommand _EditmAmpInputTestsCmd;

        public RelayCommand EditmAmpInputTestsCmd
        {
            get { return _EditmAmpInputTestsCmd; }
            set { _EditmAmpInputTestsCmd = value; }
        }

        private RelayCommand _EditVoltInputTestsCmd;

        public RelayCommand EditVoltInputTestsCmd
        {
            get { return _EditVoltInputTestsCmd; }
            set { _EditVoltInputTestsCmd = value; }
        }

        private RelayCommand _EditPT100sensorInputTestsCmd;

        public RelayCommand EditPT100sensorInputTestsCmd
        {
            get { return _EditPT100sensorInputTestsCmd; }
            set { _EditPT100sensorInputTestsCmd = value; }
        }

        private RelayCommand _EditRsensorInputTestsCmd;

        public RelayCommand EditRsensorInputTestsCmd
        {
            get { return _EditRsensorInputTestsCmd; }
            set { _EditRsensorInputTestsCmd = value; }
        }

        private RelayCommand _EditJsensorInputTestsCmd;

        public RelayCommand EditJsensorInputTestsCmd
        {
            get { return _EditJsensorInputTestsCmd; }
            set { _EditJsensorInputTestsCmd = value; }
        }

        #endregion

        #region Accuracy Input Properties

        private string _AccParameter;

        public string AccParameter
        {
            get { return _AccParameter; }
            set { _AccParameter = value; OnPropertyChanged("AccParameter"); }
        }


        private bool _AccuracyDetailsEditVis;

        public bool AccuracyDetailsEditVis
        {
            get { return _AccuracyDetailsEditVis; }
            set { _AccuracyDetailsEditVis = value; OnPropertyChanged("AccuracyDetailsEditVis"); }
        }


        private bool _IsmAmpInputTest;

        public bool IsmAmpInputTest
        {
            get { return _IsmAmpInputTest; }
            set
            {
                _IsmAmpInputTest = value;

                if (_IsmAmpInputTest)
                {
                    if (!AccuracyTestSequenceList.Contains(clsGlobalVariables.mAmpAccuracyTest))
                    {
                        AccuracyTestSequenceList.Add(clsGlobalVariables.mAmpAccuracyTest);
                    }
                }
                else
                {
                    if (AccuracyTestSequenceList.Contains(clsGlobalVariables.mAmpAccuracyTest))
                    {
                        int index = 0;
                        foreach (string testgrp in AccuracyTestSequenceList)
                        {
                            if (testgrp == clsGlobalVariables.mAmpAccuracyTest)
                            {
                                AccuracyTestSequenceList.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }

                OnPropertyChanged("IsmAmpInputTest");
            }
        }

        private bool _IsVoltInputTest;

        public bool IsVoltInputTest
        {
            get { return _IsVoltInputTest; }
            set {
                _IsVoltInputTest = value;

                if (_IsVoltInputTest)
                {
                    if (!AccuracyTestSequenceList.Contains(clsGlobalVariables.voltAccuracyTest))
                    {
                        AccuracyTestSequenceList.Add(clsGlobalVariables.voltAccuracyTest);
                    }
                }
                else
                {
                    if (AccuracyTestSequenceList.Contains(clsGlobalVariables.voltAccuracyTest))
                    {
                        int index = 0;
                        foreach (string testgrp in AccuracyTestSequenceList)
                        {
                            if (testgrp == clsGlobalVariables.voltAccuracyTest)
                            {
                                AccuracyTestSequenceList.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }

                OnPropertyChanged("IsVoltInputTest");
            }
        }

        private bool _IsPT100sensorInputTest;

        public bool IsPT100sensorInputTest
        {
            get { return _IsPT100sensorInputTest; }
            set {
                _IsPT100sensorInputTest = value;

                if (_IsPT100sensorInputTest)
                {
                    if (!AccuracyTestSequenceList.Contains(clsGlobalVariables.pt100sensorAccuracyTest))
                    {
                        AccuracyTestSequenceList.Add(clsGlobalVariables.pt100sensorAccuracyTest);
                    }
                }
                else
                {
                    if (AccuracyTestSequenceList.Contains(clsGlobalVariables.pt100sensorAccuracyTest))
                    {
                        int index = 0;
                        foreach (string testgrp in AccuracyTestSequenceList)
                        {
                            if (testgrp == clsGlobalVariables.pt100sensorAccuracyTest)
                            {
                                AccuracyTestSequenceList.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }

                OnPropertyChanged("IsPT100sensorInputTest");
            }
        }

        private bool _IsRsensorInputTest;

        public bool IsRsensorInputTest
        {
            get { return _IsRsensorInputTest; }
            set
            {
                _IsRsensorInputTest = value;

                if (_IsRsensorInputTest)
                {
                    if (!AccuracyTestSequenceList.Contains(clsGlobalVariables.RsensorAccuracyTest))
                    {
                        AccuracyTestSequenceList.Add(clsGlobalVariables.RsensorAccuracyTest);
                    }
                }
                else
                {
                    if (AccuracyTestSequenceList.Contains(clsGlobalVariables.RsensorAccuracyTest))
                    {
                        int index = 0;
                        foreach (string testgrp in AccuracyTestSequenceList)
                        {
                            if (testgrp == clsGlobalVariables.RsensorAccuracyTest)
                            {
                                AccuracyTestSequenceList.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }

                OnPropertyChanged("IsRsensorInputTest");
            }
        }

        private bool _IsJsensorInputTest;

        public bool IsJsensorInputTest
        {
            get { return _IsJsensorInputTest; }
            set { _IsJsensorInputTest = value;

                if (_IsJsensorInputTest)
                {
                    if (!AccuracyTestSequenceList.Contains(clsGlobalVariables.JsensorAccuracyTest))
                    {
                        AccuracyTestSequenceList.Add(clsGlobalVariables.JsensorAccuracyTest);
                    }
                }
                else
                {
                    if (AccuracyTestSequenceList.Contains(clsGlobalVariables.JsensorAccuracyTest))
                    {
                        int index = 0;
                        foreach (string testgrp in AccuracyTestSequenceList)
                        {
                            if (testgrp == clsGlobalVariables.JsensorAccuracyTest)
                            {
                                AccuracyTestSequenceList.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }

                OnPropertyChanged("IsJsensorInputTest");

            }
        }

        #endregion
        
        #region EnableTests
        private bool _AnalogIPTestsEditVis;

        public bool AnalogIPTestsEditVis
        {
            get { return _AnalogIPTestsEditVis; }
            set { _AnalogIPTestsEditVis = value; OnPropertyChanged("AnalogIPTestsEditVis"); }
        }

        private bool _AnalogOPTestsEditVis;

        public bool AnalogOPTestsEditVis
        {
            get { return _AnalogOPTestsEditVis; }
            set { _AnalogOPTestsEditVis = value; OnPropertyChanged("AnalogOPTestsEditVis"); }
        }

        private bool _TC_RTDTestsVis;

        public bool TC_RTDTestsVis
        {
            get { return _TC_RTDTestsVis; }
            set { _TC_RTDTestsVis = value; OnPropertyChanged("TC_RTDTestsVis"); }
        }

        private bool _RelayOrSSRTestsVis;

        public bool RelayOrSSRTestsVis
        {
            get { return _RelayOrSSRTestsVis; }
            set { _RelayOrSSRTestsVis = value; OnPropertyChanged("RelayOrSSRTestsVis"); }
        }

        private bool _CalibConstTestsVis;

        public bool CalibConstTestsVis
        {
            get { return _CalibConstTestsVis; }
            set { _CalibConstTestsVis = value; OnPropertyChanged("CalibConstTestsVis"); }
        }

        private bool _CommonTestsVis;

        public bool CommonTestsVis
        {
            get { return _CommonTestsVis; }
            set { _CommonTestsVis = value; OnPropertyChanged("CommonTestsVis"); }
        }

        private bool _CalibDelaysEditVis;

        public bool CalibDelaysEditVis
        {
            get { return _CalibDelaysEditVis; }
            set { _CalibDelaysEditVis = value; OnPropertyChanged("CalibDelaysEditVis"); }
        }
        private bool _CalibDelaysPR43EditVis;

        public bool CalibDelaysPR43EditVis
        {
            get { return _CalibDelaysPR43EditVis; }
            set { _CalibDelaysPR43EditVis = value; OnPropertyChanged("CalibDelaysPR43EditVis"); }
        }


        private bool _CalibDelaysPIEditVis;

        public bool CalibDelaysPIEditVis
        {
            get { return _CalibDelaysPIEditVis; }
            set { _CalibDelaysPIEditVis = value; OnPropertyChanged("CalibDelaysPIEditVis"); }
        }


        private bool _ToleranceEditVis;

        public bool ToleranceEditVis
        {
            get { return _ToleranceEditVis; }
            set { _ToleranceEditVis = value; OnPropertyChanged("ToleranceEditVis"); }
        }

        private bool _ToleranceofPR69EditVis;

        public bool ToleranceofPR69EditVis
        {
            get { return _ToleranceofPR69EditVis; }
            set { _ToleranceofPR69EditVis = value; OnPropertyChanged("ToleranceofPR69EditVis"); }
        }


        #endregion

        #region CatId Common Property

        private bool _IsSequencebtnEnabled;

        public bool IsSequencebtnEnabled
        {
            get { return _IsSequencebtnEnabled; }
            set
            {
                _IsSequencebtnEnabled = value;
                OnPropertyChanged("IsSequencebtnEnabled");
            }
        }

        private bool _IsDefaultSequence;

        public bool IsDefaultSequence
        {
            get { return _IsDefaultSequence; }
            set
            {
                _IsDefaultSequence = value;

                if (_IsDefaultSequence)
                {
                    IsSequencebtnEnabled = false;
                    ListOfGroupSequence.Clear();
                    //ListOfGroupSequence.Add(clsGlobalVariables.strAnalogInputTests);
                    //ListOfGroupSequence.Add(clsGlobalVariables.strAnalogOutputTests);
                    //ListOfGroupSequence.Add(clsGlobalVariables.strTCRTDTests);
                    //ListOfGroupSequence.Add(clsGlobalVariables.strRelaysSSRTests);
                    //ListOfGroupSequence.Add(clsGlobalVariables.strCalibConstTests);
                    //ListOfGroupSequence.Add(clsGlobalVariables.strCommonTests);


                    if (IsCommonTests)
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strCommonTests);
                    }
                    if (IsAnalogOutputTest)
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strAnalogOutputTests);
                    }
                    if (IsRelayTest)
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strRelaysSSRTests);
                    }
                    if (IsTC_RTDTest)
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strTCRTDTests);
                    }
                    if (IsAnalogInputTest)
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strAnalogInputTests);
                    }
                    if (IsCalibConstantTest)
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strCalibConstTests);
                    }                                    
                    
                }
                else
                    IsSequencebtnEnabled = true;

                OnPropertyChanged("IsDefaultSequence");
            }
        }



        private bool _IsDeviceTypeUneditable;

        public bool IsDeviceTypeUneditable
        {
            get { return _IsDeviceTypeUneditable; }
            set { _IsDeviceTypeUneditable = value; OnPropertyChanged("IsDeviceTypeUneditable"); }
        }

        private bool _IsDeviceNameUneditable;

        public bool IsDeviceNameUneditable
        {
            get { return _IsDeviceNameUneditable; }
            set { _IsDeviceNameUneditable = value; OnPropertyChanged("IsDeviceNameUneditable"); }
        }

        private bool _IsSaveBtnVis;

        public bool IsSaveBtnVis
        {
            get { return _IsSaveBtnVis; }
            set { _IsSaveBtnVis = value; OnPropertyChanged("IsSaveBtnVis"); }
        }

        private string _Savebtntext;

        public string Savebtntext
        {
            get { return _Savebtntext; }
            set { _Savebtntext = value; OnPropertyChanged("Savebtntext"); }
        }

        private bool _IsNotEditable;

        public bool IsNotEditable
        {
            get { return _IsNotEditable; }
            set { _IsNotEditable = value; OnPropertyChanged("IsNotEditable"); }
        }

        private bool _IsDialogOpen;

        public bool IsDialogOpen
        {
            get { return _IsDialogOpen; }
            set { _IsDialogOpen = value; OnPropertyChanged("IsDialogOpen"); }
        }

        private bool _MsgVis;

        public bool MsgVis
        {
            get { return _MsgVis; }
            set { _MsgVis = value; OnPropertyChanged("MsgVis"); }
        }


        private string _DeviceType;

        public string DeviceType
        {
            get { return _DeviceType; }
            set { _DeviceType = value; OnPropertyChanged("DeviceType"); }
        }

        private string _DeviceName;

        public string DeviceName
        {
            get { return _DeviceName; }
            set
            {
                _DeviceName = value;
                OnPropertyChanged("DeviceName");
            }
        }

        private int _DeviceId;

        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; OnPropertyChanged("DeviceId"); }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }

        private string _Communication_Start_Time;

        public string Communication_Start_Time
        {
            get { return _Communication_Start_Time; }
            set { _Communication_Start_Time = value; OnPropertyChanged("Communication_Start_Time"); }
        }


        private bool _ModbusSupport;

        public bool ModbusSupport
        {
            get { return _ModbusSupport; }
            set { _ModbusSupport = value; OnPropertyChanged("ModbusSupport"); }
        }


        private string _SaveParameter;

        public string SaveParameter
        {
            get { return _SaveParameter; }
            set { _SaveParameter = value; OnPropertyChanged("SaveParameter"); }
        } 
        #endregion

        #region RelayCommands Add, update , delete, save

        private RelayCommand _SaveCmd;

        public RelayCommand SaveCmd
        {
            get { return _SaveCmd; }
            set { _SaveCmd = value; }
        }

        private RelayCommand _AddSeriesCmd;

        public RelayCommand AddSeriesCmd
        {
            get { return _AddSeriesCmd; }
            set { _AddSeriesCmd = value; }
        }

        private RelayCommand _AddCatIdCmd;

        public RelayCommand AddCatIdCmd
        {
            get { return _AddCatIdCmd; }
            set { _AddCatIdCmd = value; }
        }

        private RelayCommand _DeleteSeriesCmd;

        public RelayCommand DeleteSeriesCmd
        {
            get { return _DeleteSeriesCmd; }
            set { _DeleteSeriesCmd = value; }
        }

        private RelayCommand _EditCatIdCmd;

        public RelayCommand EditCatIdCmd
        {
            get { return _EditCatIdCmd; }
            set { _EditCatIdCmd = value; }
        }

        private RelayCommand _DeleteCatIdCmd;

        public RelayCommand DeleteCatIdCmd
        {
            get { return _DeleteCatIdCmd; }
            set { _DeleteCatIdCmd = value; }
        } 
        #endregion

        #region RelayCommands Open Test Window
        private RelayCommand _EditAnalogInputTestsCmd;

        public RelayCommand EditAnalogInputTestsCmd
        {
            get { return _EditAnalogInputTestsCmd; }
            set { _EditAnalogInputTestsCmd = value; }
        }

        private RelayCommand _EditAnalogOutputTestsCmd;

        public RelayCommand EditAnalogOutputTestsCmd
        {
            get { return _EditAnalogOutputTestsCmd; }
            set { _EditAnalogOutputTestsCmd = value; }
        }

        private RelayCommand _EditRelayTestsCmd;

        public RelayCommand EditRelayTestsCmd
        {
            get { return _EditRelayTestsCmd; }
            set { _EditRelayTestsCmd = value; }
        }

        private RelayCommand _EditTC_RTDTestsCmd;

        public RelayCommand EditTC_RTDTestsCmd
        {
            get { return _EditTC_RTDTestsCmd; }
            set { _EditTC_RTDTestsCmd = value; }
        }

        private RelayCommand _EditCalibConstantTestsCmd;

        public RelayCommand EditCalibConstantTestsCmd
        {
            get { return _EditCalibConstantTestsCmd; }
            set { _EditCalibConstantTestsCmd = value; OnPropertyChanged("EditCalibConstantTestsCmd"); }
        }

        private RelayCommand _EditCommonTestsCmd;

        public RelayCommand EditCommonTestsCmd
        {
            get { return _EditCommonTestsCmd; }
            set { _EditCommonTestsCmd = value; OnPropertyChanged("EditCommonTestsCmd"); }
        }

        private RelayCommand _EditUptestgrpCmd;

        public RelayCommand EditUptestgrpCmd
        {
            get { return _EditUptestgrpCmd; }
            set { _EditUptestgrpCmd = value; OnPropertyChanged("EditUptestgrpCmd"); }
        }

        private RelayCommand _EditDowntestgrpCmd;

        public RelayCommand EditDowntestgrpCmd
        {
            get { return _EditDowntestgrpCmd; }
            set { _EditDowntestgrpCmd = value; OnPropertyChanged("EditDowntestgrpCmd"); }
        }

        private RelayCommand _EditUpAccuracytestCmd;

        public RelayCommand EditUpAccuracytestCmd
        {
            get { return _EditUpAccuracytestCmd; }
            set { _EditUpAccuracytestCmd = value; }
        }

        private RelayCommand _EditDownAccuracytestgrpCmd;

        public RelayCommand EditDownAccuracytestgrpCmd
        {
            get { return _EditDownAccuracytestgrpCmd; }
            set { _EditDownAccuracytestgrpCmd = value; }
        }


        #endregion

        #region Delay, Tolerance save Relay commands
        private RelayCommand _EditCalibrationDelaysCmd;

        public RelayCommand EditCalibrationDelaysCmd
        {
            get { return _EditCalibrationDelaysCmd; }
            set { _EditCalibrationDelaysCmd = value; OnPropertyChanged("EditCalibrationDelaysCmd"); }
        }

        private RelayCommand _EditCalibrationDelaysPR43Cmd;

        public RelayCommand EditCalibrationDelaysPR43Cmd
        {
            get { return _EditCalibrationDelaysPR43Cmd; }
            set { _EditCalibrationDelaysPR43Cmd = value; OnPropertyChanged("EditCalibrationDelaysPR43Cmd"); }
        }


        private RelayCommand _EditCalibrationDelaysPICmd;

        public RelayCommand EditCalibrationDelaysPICmd
        {
            get { return _EditCalibrationDelaysPICmd; }
            set { _EditCalibrationDelaysPICmd = value; OnPropertyChanged("EditCalibrationDelaysPICmd"); }
        }


        private RelayCommand _EditToleranceCmd;

        public RelayCommand EditToleranceCmd
        {
            get { return _EditToleranceCmd; }
            set { _EditToleranceCmd = value; OnPropertyChanged("EditToleranceCmd"); }
        }

        private RelayCommand _EditTolerancePR69Cmd;

        public RelayCommand EditTolerancePR69Cmd
        {
            get { return _EditTolerancePR69Cmd; }
            set { _EditTolerancePR69Cmd = value; OnPropertyChanged("EditTolerancePR69Cmd"); }
        }
        
        private RelayCommand _SaveDelayToleranceCmd;

        public RelayCommand SaveDelayToleranceCmd
        {
            get { return _SaveDelayToleranceCmd; }
            set { _SaveDelayToleranceCmd = value; OnPropertyChanged("SaveDelayToleranceCmd"); }
        }

        private RelayCommand _BrowsemotfileCmd;

        public RelayCommand BrowsemotfileCmd
        {
            get { return _BrowsemotfileCmd; }
            set { _BrowsemotfileCmd = value; OnPropertyChanged("BrowsemotfileCmd"); }
        }

        #endregion

        #region Visibility Delay, tolerance
        private bool _IsCalibDelayTestsbtnVis;

        public bool IsCalibDelayTestsbtnVis
        {
            get { return _IsCalibDelayTestsbtnVis; }
            set { _IsCalibDelayTestsbtnVis = value; OnPropertyChanged("IsCalibDelayTestsbtnVis"); }
        }

        private bool _IstolerancebtnVis;

        public bool IstolerancebtnVis
        {
            get { return _IstolerancebtnVis; }
            set { _IstolerancebtnVis = value; OnPropertyChanged("IstolerancebtnVis"); }
        }

        private bool _IsSaveDelayToleranceBtnVis;

        public bool IsSaveDelayToleranceBtnVis
        {
            get { return _IsSaveDelayToleranceBtnVis; }
            set { _IsSaveDelayToleranceBtnVis = value; OnPropertyChanged("IsSaveDelayToleranceBtnVis"); }
        }

        private bool _BrowsemotfileBtnVis;

        public bool BrowsemotfileBtnVis
        {
            get { return _BrowsemotfileBtnVis; }
            set { _BrowsemotfileBtnVis = value; OnPropertyChanged("BrowsemotfileBtnVis"); }
        } 
        #endregion
        
        #region Collections of All Tests
        private ObservableCollection<AnalogInputTests> _AnalogInputTestsDetails;

        public ObservableCollection<AnalogInputTests> analogInputTestsDetails
        {
            get { return _AnalogInputTestsDetails; }
            set { _AnalogInputTestsDetails = value; OnPropertyChanged("analogInputTestsDetails"); }
        }

        private ObservableCollection<AnalogOutputTests> _analogOutputTestsDetails;

        public ObservableCollection<AnalogOutputTests> analogOutputTestsDetails
        {
            get { return _analogOutputTestsDetails; }
            set { _analogOutputTestsDetails = value; OnPropertyChanged("analogOutputTestsDetails"); }
        }

        private ObservableCollection<TC_RTDCalibTests> _tcRTDTestsDetails;

        public ObservableCollection<TC_RTDCalibTests> tcRTDTestsDetails
        {
            get { return _tcRTDTestsDetails; }
            set { _tcRTDTestsDetails = value; OnPropertyChanged("tcRTDTestsDetails"); }
        }

        private ObservableCollection<RelayORSSRTests> _relayOrSSRTestsDetails;

        public ObservableCollection<RelayORSSRTests> relayOrSSRTestsDetails
        {
            get { return _relayOrSSRTestsDetails; }
            set { _relayOrSSRTestsDetails = value; OnPropertyChanged("relayOrSSRTestsDetails"); }
        }

        private ObservableCollection<CalibrationConstants> _calibrationconstDetails;

        public ObservableCollection<CalibrationConstants> calibrationconstDetails
        {
            get { return _calibrationconstDetails; }
            set { _calibrationconstDetails = value; OnPropertyChanged("calibrationconstDetails"); }
        }

        private ObservableCollection<CommonTests> _commonTestsDetails;

        public ObservableCollection<CommonTests> commonTestsDetails
        {
            get { return _commonTestsDetails; }
            set { _commonTestsDetails = value; OnPropertyChanged("commonTestsDetails"); }
        }

        private ObservableCollection<AccuracyTests> _AccuracymAmpTestsDetails;

        public ObservableCollection<AccuracyTests> AccuracymAmpTestsDetails
        {
            get { return _AccuracymAmpTestsDetails; }
            set { _AccuracymAmpTestsDetails = value; OnPropertyChanged("AccuracymAmpTestsDetails"); }
        }

        private ObservableCollection<AccuracyTests> _AccuracyVoltTestsDetails;

        public ObservableCollection<AccuracyTests> AccuracyVoltTestsDetails
        {
            get { return _AccuracyVoltTestsDetails; }
            set { _AccuracyVoltTestsDetails = value; OnPropertyChanged("AccuracyVoltTestsDetails"); }
        }

        private ObservableCollection<AccuracyTests> _AccuracyPT100SnsrTestsDetails;

        public ObservableCollection<AccuracyTests> AccuracyPT100SnsrTestsDetails
        {
            get { return _AccuracyPT100SnsrTestsDetails; }
            set { _AccuracyPT100SnsrTestsDetails = value; OnPropertyChanged("AccuracyPT100SnsrTestsDetails"); }
        }

        private ObservableCollection<AccuracyTests> _AccuracyRSensorTestsDetails;

        public ObservableCollection<AccuracyTests> AccuracyRSensorTestsDetails
        {
            get { return _AccuracyRSensorTestsDetails; }
            set { _AccuracyRSensorTestsDetails = value; OnPropertyChanged("AccuracyRSensorTestsDetails"); }
        }

        private ObservableCollection<AccuracyTests> _AccuracyJSensorTestsDetails;

        public ObservableCollection<AccuracyTests> AccuracyJSensorTestsDetails
        {
            get { return _AccuracyJSensorTestsDetails; }
            set { _AccuracyJSensorTestsDetails = value; OnPropertyChanged("AccuracyJSensorTestsDetails"); }
        }


        #endregion

        #region Collection of group sequence

        private ObservableCollection<string> _ListOfGroupSequence;

        public ObservableCollection<string> ListOfGroupSequence
        {
            get { return _ListOfGroupSequence; }
            set { _ListOfGroupSequence = value; OnPropertyChanged("ListOfGroupSequence"); }
        }

        private ObservableCollection<string> _AccuracyTestSequenceList;

        public ObservableCollection<string> AccuracyTestSequenceList
        {
            get { return _AccuracyTestSequenceList; }
            set { _AccuracyTestSequenceList = value; OnPropertyChanged("AccuracyTestSequenceList"); }
        }


        private int _listviewIndex;

        public int listviewIndex
        {
            get { return _listviewIndex; }
            set { _listviewIndex = value; OnPropertyChanged("listviewIndex"); }
        }

        private int _AccuracylistviewIndex;

        public int AccuracylistviewIndex
        {
            get { return _AccuracylistviewIndex; }
            set { _AccuracylistviewIndex = value; OnPropertyChanged("AccuracylistviewIndex"); }
        }


        #endregion

        #region Main UI Tests Visibility

        //ListOfGroupSequence.Add(clsGlobalVariables.strAnalogInputTests);
        //ListOfGroupSequence.Add(clsGlobalVariables.strAnalogOutputTests);
        //ListOfGroupSequence.Add(clsGlobalVariables.strTCRTDTests);
        //ListOfGroupSequence.Add(clsGlobalVariables.strRelaysSSRTests);
        //ListOfGroupSequence.Add(clsGlobalVariables.strCalibConstTests);
        //ListOfGroupSequence.Add(clsGlobalVariables.strCommonTests);

        private bool _IsAnalogInputTest;

        public bool IsAnalogInputTest
        {
            get { return _IsAnalogInputTest; }
            set
            {
                _IsAnalogInputTest = value;

                if (_IsAnalogInputTest)
                {
                    IsAnalogInputTestsVis = true;

                    if (!ListOfGroupSequence.Contains(clsGlobalVariables.strAnalogInputTests))
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strAnalogInputTests);
                    }
                }                    
                else
                {
                    IsAnalogInputTestsVis = false;

                    if (ListOfGroupSequence.Contains(clsGlobalVariables.strAnalogInputTests))
                    {
                        int index = 0;
                        foreach (string testgrp in ListOfGroupSequence)
                        {
                            if (testgrp == clsGlobalVariables.strAnalogInputTests)
                            {
                                ListOfGroupSequence.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }

                OnPropertyChanged("IsAnalogInputTest");
            }
        }

        private bool _IsAnalogInputTestsVis;

        public bool IsAnalogInputTestsVis
        {
            get { return _IsAnalogInputTestsVis; }
            set { _IsAnalogInputTestsVis = value; OnPropertyChanged("IsAnalogInputTestsVis"); }
        }

        private bool _IsAnalogOutputTest;

        public bool IsAnalogOutputTest
        {
            get { return _IsAnalogOutputTest; }
            set
            {
                _IsAnalogOutputTest = value;

                if (_IsAnalogOutputTest)
                {
                    IsAnalogOutputTestsVis = true;
                    if (!ListOfGroupSequence.Contains(clsGlobalVariables.strAnalogOutputTests))
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strAnalogOutputTests);
                    }
                }
                else
                {
                    IsAnalogOutputTestsVis = false;

                    if (ListOfGroupSequence.Contains(clsGlobalVariables.strAnalogOutputTests))
                    {
                        int index = 0;
                        foreach (string testgrp in ListOfGroupSequence)
                        {
                            if (testgrp == clsGlobalVariables.strAnalogOutputTests)
                            {
                                ListOfGroupSequence.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }

                OnPropertyChanged("IsAnalogOutputTest");
            }
        }

        private bool _IsAnalogOutputTestsVis;

        public bool IsAnalogOutputTestsVis
        {
            get { return _IsAnalogOutputTestsVis; }
            set { _IsAnalogOutputTestsVis = value; OnPropertyChanged("IsAnalogOutputTestsVis"); }
        }


        private bool _IsTC_RTDTest;

        public bool IsTC_RTDTest
        {
            get { return _IsTC_RTDTest; }
            set
            {
                _IsTC_RTDTest = value;

                if (_IsTC_RTDTest)
                {
                    IsTC_RTDTestsVis = true;

                    if (!ListOfGroupSequence.Contains(clsGlobalVariables.strTCRTDTests))
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strTCRTDTests);
                    }
                }
                else
                {
                    IsTC_RTDTestsVis = false;

                    if (ListOfGroupSequence.Contains(clsGlobalVariables.strTCRTDTests))
                    {
                        int index = 0;
                        foreach (string testgrp in ListOfGroupSequence)
                        {
                            if (testgrp == clsGlobalVariables.strTCRTDTests)
                            {
                                ListOfGroupSequence.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }

                OnPropertyChanged("IsTC_RTDTest");
            }
        }

        private bool _IsTC_RTDTestsVis;

        public bool IsTC_RTDTestsVis
        {
            get { return _IsTC_RTDTestsVis; }
            set { _IsTC_RTDTestsVis = value; OnPropertyChanged("IsTC_RTDTestsVis"); }
        }
        
         
        private bool _IsRelayTest;

        public bool IsRelayTest
        {
            get { return _IsRelayTest; }
            set
            {
                _IsRelayTest = value;

                if (_IsRelayTest)
                {
                    IsRelayTestsVis = true;

                    if (!ListOfGroupSequence.Contains(clsGlobalVariables.strRelaysSSRTests))
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strRelaysSSRTests);
                    }
                }
                else
                {
                    IsRelayTestsVis = false;

                    if (ListOfGroupSequence.Contains(clsGlobalVariables.strRelaysSSRTests))
                    {
                        int index = 0;
                        foreach (string testgrp in ListOfGroupSequence)
                        {
                            if (testgrp == clsGlobalVariables.strRelaysSSRTests)
                            {
                                ListOfGroupSequence.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }

                OnPropertyChanged("IsRelayTest");
            }
        }

        private bool _IsRelayTestsVis;

        public bool IsRelayTestsVis
        {
            get { return _IsRelayTestsVis; }
            set
            {
                _IsRelayTestsVis = value;
                OnPropertyChanged("IsRelayTestsVis");
            }
        }

        private bool _IsCalibConstantTest;

        public bool IsCalibConstantTest
        {
            get { return _IsCalibConstantTest; }
            set
            {
                _IsCalibConstantTest = value;
                if (_IsCalibConstantTest)
                {
                    IsCalibConstantTestsVis = true;

                    if (!ListOfGroupSequence.Contains(clsGlobalVariables.strCalibConstTests))
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strCalibConstTests);
                    }
                }
                else
                {
                    IsCalibConstantTestsVis = false;

                    if (ListOfGroupSequence.Contains(clsGlobalVariables.strCalibConstTests))
                    {
                        int index = 0;
                        foreach (string testgrp in ListOfGroupSequence)
                        {
                            if (testgrp == clsGlobalVariables.strCalibConstTests)
                            {
                                ListOfGroupSequence.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }
                OnPropertyChanged("IsCalibConstantTest");
            }
        }

        private bool _IsCalibConstantTestsVis;

        public bool IsCalibConstantTestsVis
        {
            get { return _IsCalibConstantTestsVis; }
            set
            {
                _IsCalibConstantTestsVis = value;

                OnPropertyChanged("IsCalibConstantTestsVis");
            }
        }

        private bool _IsCommonTests;

        public bool IsCommonTests
        {
            get { return _IsCommonTests; }
            set {
                _IsCommonTests = value;
                if (_IsCommonTests)
                {
                    IsCommonTestsVis = true;

                    if (!ListOfGroupSequence.Contains(clsGlobalVariables.strCommonTests))
                    {
                        ListOfGroupSequence.Add(clsGlobalVariables.strCommonTests);
                    }
                }
                else
                {
                    IsCommonTestsVis = false; AccuracyDetailsEditVis =false;

                    if (ListOfGroupSequence.Contains(clsGlobalVariables.strCommonTests))
                    {
                        int index = 0;
                        foreach (string testgrp in ListOfGroupSequence)
                        {
                            if (testgrp == clsGlobalVariables.strCommonTests)
                            {
                                ListOfGroupSequence.RemoveAt(index);
                                break;
                            }
                            index++;
                        }
                    }
                }
                OnPropertyChanged("IsCommonTests");
            }
        }

        private bool _IsCommonTestsVis;

        public bool IsCommonTestsVis
        {
            get { return _IsCommonTestsVis; }
            set { _IsCommonTestsVis = value; OnPropertyChanged("IsCommonTestsVis"); }
        }


        #endregion

        #region Common Properties

        private string _Sender = "";

        public string Sender
        {
            get { return _Sender; }
            set { _Sender = value; OnPropertyChanged("Sender"); }
        }

        private string _Motfilepath;

        public string Motfilepath
        {
            get { return _Motfilepath; }
            set { _Motfilepath = value; OnPropertyChanged("Motfilepath"); }
        }


        private string _Msg = "";

        public string Msg
        {
            get { return _Msg; }
            set { _Msg = value; OnPropertyChanged("Msg"); }
        }

        private bool _MesssageVis = false;

        public bool MesssageVis
        {
            get { return _MesssageVis; }
            set { _MesssageVis = value; OnPropertyChanged("MesssageVis"); }
        }

        private bool _YesNoBtnVis = false;

        public bool YesNoBtnVis
        {
            get { return _YesNoBtnVis; }
            set { _YesNoBtnVis = value; OnPropertyChanged("YesNoBtnVis"); }
        }

        private bool _CancelBtnVis = true;

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

        private string _EventSender = "";

        public string EventSender
        {
            get { return _EventSender; }
            set { _EventSender = value; OnPropertyChanged("EventSender"); }
        }

        private bool _IsCheckBoxEnabled;

        public bool IsCheckBoxEnabled
        {
            get { return _IsCheckBoxEnabled; }
            set { _IsCheckBoxEnabled = value; OnPropertyChanged("IsCheckBoxEnabled"); }
        }

        private bool _ErrorMsgVis;

        public bool ErrorMsgVis
        {
            get { return _ErrorMsgVis; }
            set { _ErrorMsgVis = value; OnPropertyChanged("ErrorMsgVis"); }
        }


        private string _EventParam;

        public string EventParam
        {
            get { return _EventParam; }
            set { _EventParam = value; OnPropertyChanged("EventParam"); }
        }

        #endregion

        #region Objects Bindings

        private clsAccuracyTests _clsAccuracyTests;

        public clsAccuracyTests clsAccuracyTests
        {
            get { return _clsAccuracyTests; }
            set { _clsAccuracyTests = value; OnPropertyChanged("clsAccuracyTests"); }
        }
        
        private clsAnalogInputTests _clsAnalogInputTests;

        public clsAnalogInputTests clsAnalogInputTests
        {
            get { return _clsAnalogInputTests; }
            set { _clsAnalogInputTests = value; OnPropertyChanged("clsAnalogInputTests"); }
        }

        private clsAnalogOutputTests _clsAnalogOutputTests;

        public clsAnalogOutputTests clsAnalogOutputTests
        {
            get { return _clsAnalogOutputTests; }
            set { _clsAnalogOutputTests = value; OnPropertyChanged("clsAnalogOutputTests"); }
        }

        private clsTC_RTDTests _clsTC_RTDTests;

        public clsTC_RTDTests clsTC_RTDTests
        {
            get { return _clsTC_RTDTests; }
            set { _clsTC_RTDTests = value; OnPropertyChanged("clsTC_RTDTests"); }
        }

        private clsRelayORSSRTests _clsRelayORSSRTests;

        public clsRelayORSSRTests clsRelayORSSRTests
        {
            get { return _clsRelayORSSRTests; }
            set { _clsRelayORSSRTests = value; OnPropertyChanged("clsRelayORSSRTests"); }
        }

        private clsCalibrationConstantTests _clsCalibrationConstantTests;

        public clsCalibrationConstantTests clsCalibrationConstantTests
        {
            get { return _clsCalibrationConstantTests; }
            set { _clsCalibrationConstantTests = value; OnPropertyChanged("clsCalibrationConstantTests"); }
        }

        private clsCommonTests _clsCommonTests;

        public clsCommonTests clsCommonTests
        {
            get { return _clsCommonTests; }
            set { _clsCommonTests = value; OnPropertyChanged("clsCommonTests"); }
        }


        private clsCalibrationDelays _clsCalibrationDelays;

        public clsCalibrationDelays clsCalibrationDelays
        {
            get { return _clsCalibrationDelays; }
            set { _clsCalibrationDelays = value; OnPropertyChanged("clsCalibrationDelays"); }
        }

        private clsCalibrationDelaysPR43 _clsCalibrationDelaysPR43;

        public clsCalibrationDelaysPR43 clsCalibrationDelaysPR43
        {
            get { return _clsCalibrationDelaysPR43; }
            set { _clsCalibrationDelaysPR43 = value; OnPropertyChanged("clsCalibrationDelaysPR43"); }
        }


        private clsCalibrationDelaysPI _clsCalibrationDelaysPI;

        public clsCalibrationDelaysPI clsCalibrationDelaysPI
        {
            get { return _clsCalibrationDelaysPI; }
            set { _clsCalibrationDelaysPI = value; OnPropertyChanged("clsCalibrationDelaysPI"); }
        }


        private clsTolerancesOfPI _clsTolerancesOfPI;

        public clsTolerancesOfPI clsTolerancesOfPI
        {
            get { return _clsTolerancesOfPI; }
            set { _clsTolerancesOfPI = value; OnPropertyChanged("clsTolerancesOfPI"); }
        }

        private clsTolerancesOfPR69 _clsTolerancesOfPR69;

        public clsTolerancesOfPR69 clsTolerancesOfPR69
        {
            get { return _clsTolerancesOfPR69; }
            set { _clsTolerancesOfPR69 = value; OnPropertyChanged("clsTolerancesOfPR69"); }
        }


        #endregion

        #region Edit Click Methods
        private void EditAnalogInputTestsClk(object obj)
        {
            clsAnalogInputTests = new clsAnalogInputTests();
            int found = 0;

            for (int CounterCatId = 0; CounterCatId < ModifiedCatId.Count; CounterCatId++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterCatId].ConfigurationData.Count; CounterConfigData++)
                {
                    for (int CounterCatIdList = 0; CounterCatIdList < ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCatIdList++)
                    {
                        if (ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList].DeviceName == obj.ToString())
                        {
                            SelDeviceType(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].DeviceType);
                            clsAnalogInputTests.ParseAnalogIPDetails(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList]);
                            found = 1;
                        }
                    }
                    if (found == 1) { break; }
                }
                if (found == 1) { break; }
            }
            
            EventSender = "SaveAnalogIPTests";
            AnalogIPTestsEditVis = true;
            AnalogOPTestsEditVis = false;
            RelayOrSSRTestsVis = false;
            TC_RTDTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;
            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;
            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }

        private void EditAnalogOutputTestsClk(object obj)
        {
            clsAnalogOutputTests = new clsAnalogOutputTests();

            int found = 0;

            for (int CounterCatId = 0; CounterCatId < ModifiedCatId.Count; CounterCatId++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterCatId].ConfigurationData.Count; CounterConfigData++)
                {
                    for (int CounterCatIdList = 0; CounterCatIdList < ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCatIdList++)
                    {
                        if (ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList].DeviceName == obj.ToString())
                        {
                            SelDeviceType(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].DeviceType);
                            clsAnalogOutputTests.ParseAnalogOPDetails(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList]);
                            found = 1;
                        }
                    }
                    if (found == 1) { break; }
                }
                if (found == 1) { break; }
            }

            EventSender = "SaveAnalogOPTests";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = true;
            RelayOrSSRTestsVis = false;
            TC_RTDTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;
            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;
            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }

        private void EditTC_RTDTestsClk(object obj)
        {
            clsTC_RTDTests = new clsTC_RTDTests();

            int found = 0;

            for (int CounterCatId = 0; CounterCatId < ModifiedCatId.Count; CounterCatId++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterCatId].ConfigurationData.Count; CounterConfigData++)
                {
                    for (int CounterCatIdList = 0; CounterCatIdList < ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCatIdList++)
                    {
                        if (ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList].DeviceName == obj.ToString())
                        {
                            SelDeviceType(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].DeviceType);
                            clsTC_RTDTests.ParseTC_RTDDetails(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList]);
                            found = 1;
                        }
                    }
                    if (found == 1) { break; }
                }
                if (found == 1) { break; }
            }

            EventSender = "SaveTC_RTDTests";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            RelayOrSSRTestsVis = false;
            CalibConstTestsVis = false;
            TC_RTDTestsVis = true;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;
            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;
            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }

        private void EditRelayTestsClk(object obj)
        {
            clsRelayORSSRTests = new clsRelayORSSRTests();

            int found = 0;

            for (int CounterCatId = 0; CounterCatId < ModifiedCatId.Count; CounterCatId++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterCatId].ConfigurationData.Count; CounterConfigData++)
                {
                    for (int CounterCatIdList = 0; CounterCatIdList < ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCatIdList++)
                    {
                        if (ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList].DeviceName == obj.ToString())
                        {
                            SelDeviceType(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].DeviceType);
                            clsRelayORSSRTests.ParseRelayOrSSRDetails(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList]);
                            found = 1;
                        }
                    }
                    if (found == 1) { break; }
                }
                if (found == 1) { break; }
            }

            OP1subtestList.Add("START_REL_TEST_OP1_RELAY_PR43_PI");
            OP1subtestList.Add("START_REL_TEST_OP1_RELAY");

            OP2subtestList.Add("START_REL_TEST_OP2_RELAY_PI");
            OP2subtestList.Add("START_REL_TEST_OP2_RELAY");

            OutputTypeList.Add("SSR");
            OutputTypeList.Add("Relay");
            OutputTypeList.Add("A/O");

            RelayTypeList.Add("1C/O");
            RelayTypeList.Add("2C/O");

            EventSender = "SaveRelayOrSSRTests";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            TC_RTDTestsVis = false;
            RelayOrSSRTestsVis = true;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;
            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;
            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }

        private void EditCalibConstantTestsClk(object obj)
        {
            clsCalibrationConstantTests = new clsCalibrationConstantTests();

            int found = 0;

            for (int CounterCatId = 0; CounterCatId < ModifiedCatId.Count; CounterCatId++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterCatId].ConfigurationData.Count; CounterConfigData++)
                {
                    for (int CounterCatIdList = 0; CounterCatIdList < ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCatIdList++)
                    {
                        if (ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList].DeviceName == obj.ToString())
                        {
                            SelDeviceType(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].DeviceType);
                            clsCalibrationConstantTests.ParseRelayOrSSRDetails(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList]);
                            found = 1;
                        }
                    }
                    if (found == 1) { break; }
                }
                if (found == 1) { break; }
            }

            EventSender = "SaveCalibConstTests";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            TC_RTDTestsVis = false;
            RelayOrSSRTestsVis = false;
            CalibConstTestsVis = true;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;
            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;
            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }

       

        private void EditCommonTestsClk(object obj)
        {
            clsCommonTests = new clsCommonTests();

            int found = 0;

            for (int CounterCatId = 0; CounterCatId < ModifiedCatId.Count; CounterCatId++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterCatId].ConfigurationData.Count; CounterConfigData++)
                {
                    for (int CounterCatIdList = 0; CounterCatIdList < ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCatIdList++)
                    {
                        if (ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList].DeviceName == obj.ToString())
                        {
                            SelDeviceType(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].DeviceType);
                            clsCommonTests.ParseRelayOrSSRDetails(ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList]);
                            found = 1;
                        }
                    }
                    if (found == 1) { break; }
                }
                if (found == 1) { break; }
            }

            EventSender = "SaveCommonTests";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            TC_RTDTestsVis = false;
            RelayOrSSRTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = true;
            AccuracyDetailsEditVis = false;
            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;
            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }
        
        public void SelDeviceType(string DeviceType)
        {
            if (DeviceType.Contains("48"))
                clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PR69_48x48;
            else if (DeviceType.Contains("96"))
                clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PR69_96x96;
            else if (DeviceType.Contains("43"))
                clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PR43_48x48;
            else
                clsGlobalVariables.selectedDeviceType = clsGlobalVariables.SelectedDeviceType.PI;
        }
        #endregion

        #region Save

        private void SaveBtnClk(object obj)
        {
            InVisibleAllGroups();
            PopupBtnVisibility(true);
            switch (obj.ToString())
            {
                case "SaveNewSeries":
                    foreach (ConfigurationData item in ModifiedCatId[0].ConfigurationData)
                    {
                        if (item.DeviceType == DeviceType)
                        {
                            ShowMessageBox("Series Type Already exist!\nPlease use another device type name.", false, "", "Error", clsGlobalVariables.MsgIcon.Error);
                            return;
                        }
                    }
                    if (DeviceType == null || DeviceType == "")
                    {
                        ShowMessageBox("Series Type can't be empty !", false, "Error", "", clsGlobalVariables.MsgIcon.Error);
                    }
                    else
                    {
                        ShowMessageBox("Do you want to save the changes?", true, "SaveNewSeriesType", "", clsGlobalVariables.MsgIcon.Question);
                    }
                    break;

                case "SaveNewCatId":
                    if (!ValidationOnSave())
                    {
                        return;
                    }
                    foreach (ConfigurationData item in ModifiedCatId[0].ConfigurationData)
                    {
                        foreach (CatIdList cat in item.CatIdLists)
                        {
                            if (cat.DeviceName == DeviceName && IsDeviceNameUneditable == false)
                            {
                                ShowMessageBox("Device Name Already exist!\nPlease use another device name.", false, "Error", "", clsGlobalVariables.MsgIcon.Error);
                                return;
                            }
                        }
                    }

                    if (DeviceName == null || DeviceName == "")
                    {
                        ShowMessageBox("Device Name can't be empty !", false, "Error", "", clsGlobalVariables.MsgIcon.Error);
                    }
                    else
                    {
                        ShowMessageBox("Do you want to save the changes?", true, "SaveNewCatId", "", clsGlobalVariables.MsgIcon.Question);
                    }

                    break;

                case "SaveEditedCatId":
                    //Validation added 
                    if (!ValidationOnSave())
                    {
                        return;
                    }
                    foreach (ConfigurationData item in ModifiedCatId[0].ConfigurationData)
                    {
                        foreach (CatIdList cat in item.CatIdLists)
                        {
                            if (cat.DeviceName == DeviceName && IsDeviceNameUneditable == false)
                            {
                                ShowMessageBox("Device Name Already exist!\nPlease use another device name.", false, "Error", "", clsGlobalVariables.MsgIcon.Error);
                                return;
                            }
                        }
                    }

                    ShowMessageBox("Do you want to save the changes?", true, "SaveEditedJson", "", clsGlobalVariables.MsgIcon.Question);

                    break;

                default:
                    break;
            }

        }

        private bool ValidationOnSave()
        {
            return true;
        }
               
        #endregion

        #region Assign Fields to UI
        public void AssignDataToFields(object selectedItem)
        {
            try
            {
                if (selectedItem.GetType() == typeof(ConfigurationDataList))
                {
                    DeviceName = "";

                    ConfigurationDataList _configurationDataList = new ConfigurationDataList();

                    _configurationDataList = (ConfigurationDataList)selectedItem;

                    CalibrationDelaysDetails.Clear();
                    if (_configurationDataList.CalibrationDelays != null)
                    {
                        foreach (CalibrationDelays calibdelayobj in _configurationDataList.CalibrationDelays)
                        {
                            CalibrationDelaysDetails.Add(calibdelayobj);
                        }
                    }

                    CalibrationDelaysDetailsPR43.Clear();
                    if (_configurationDataList.CalibrationDelaysPR43 != null)
                    {
                        foreach (CalibrationDelaysPR43 calibdelayobj in _configurationDataList.CalibrationDelaysPR43)
                        {
                            CalibrationDelaysDetailsPR43.Add(calibdelayobj);
                        }
                    }

                    CalibrationDelaysPIDetails.Clear();
                    if (_configurationDataList.CalibrationDelaysPI != null)
                    {
                        foreach (CalibrationDelaysPI calibdelayobj in _configurationDataList.CalibrationDelaysPI)
                        {
                            CalibrationDelaysPIDetails.Add(calibdelayobj);
                        }
                    }

                    TolerancesofPIDetails.Clear();
                    if (_configurationDataList.TolerancesofPI != null)
                    {
                        foreach (TolerancesOfPI tolerancesobj in _configurationDataList.TolerancesofPI)
                        {
                            TolerancesofPIDetails.Add(tolerancesobj);
                        }
                    }

                    TolerancesofPR69Details.Clear();
                    if (_configurationDataList.TolerancesOfPR69 != null)
                    {
                        foreach (TolerancesOfPR69 tolerancesobj in _configurationDataList.TolerancesOfPR69)
                        {
                            TolerancesofPR69Details.Add(tolerancesobj);
                        }
                    }

                    Motfilepath = _configurationDataList.motfilepath;

                    DelaytoleranceVisibility(true);
                    IsCheckBoxEnabled = false;

                }
                else if (selectedItem.GetType() == typeof(CatIdList))
                {
                    DelaytoleranceVisibility(false);

                    CatIdList _catList = new CatIdList();
                    _catList = (CatIdList)selectedItem;
                    DeviceType = null;

                    int CounterConfigData = 0;
                    while (CounterConfigData != -1)
                    {
                        int CounterDeviceType = 0;
                        while (CounterDeviceType != -1)
                        {
                            if (ModifiedCatId[0].ConfigurationData[CounterConfigData].CatIdLists[CounterDeviceType].DeviceName != _catList.DeviceName)
                            {
                                CounterDeviceType++;
                            }
                            else
                            {
                                DeviceType = ModifiedCatId[0].ConfigurationData[CounterConfigData].DeviceType;
                                CounterDeviceType = -1;
                            }

                            if (CounterDeviceType == ModifiedCatId[0].ConfigurationData[CounterConfigData].CatIdLists.Count)
                            {
                                CounterDeviceType = -1;
                            }
                        }
                        if (CounterConfigData == ModifiedCatId[0].ConfigurationData.Count)
                        {
                            CounterConfigData = -1;
                        }
                        else
                        {
                            if (DeviceType != null)
                            {
                                CounterConfigData = -1;
                            }
                            else
                            {
                                CounterConfigData++;
                            }
                        }
                    }

                    DeviceName = _catList.DeviceName;
                    DeviceId = _catList.DeviceId;
                    Description = _catList.Description;
                    ModbusSupport = _catList.ModbusSupport;
                    Communication_Start_Time = _catList.Communication_Start_Time;

                    IsAnalogInputTest = _catList.IsAnalogInputTestApplicable;
                    IsAnalogOutputTest = _catList.IsAnalogOutputTestApplicable;
                    IsTC_RTDTest = _catList.IsTC_RTDTestApplicable;
                    IsRelayTest = _catList.IsRelayOrSSRTestsApplicable;
                    IsCalibConstantTest = _catList.IsCalibrationConstApplicable;
                    IsCommonTests = _catList.IsCommonTestsApplicable;

                    analogInputTestsDetails.Clear();
                    if (_catList.AnalogIpTests != null)
                    {
                        foreach (AnalogInputTests test in _catList.AnalogIpTests)
                        {
                            analogInputTestsDetails.Add(test);
                        }
                    }

                    analogOutputTestsDetails.Clear();
                    if (_catList.AnalogOpTests != null)
                    {
                        foreach (AnalogOutputTests test in _catList.AnalogOpTests)
                        {
                            analogOutputTestsDetails.Add(test);
                        }
                    }

                    tcRTDTestsDetails.Clear();
                    if (_catList.TC_RTDTests != null)
                    {
                        foreach (TC_RTDCalibTests test in _catList.TC_RTDTests)
                        {
                            tcRTDTestsDetails.Add(test);
                        }
                    }

                    relayOrSSRTestsDetails.Clear();
                    if (_catList.RelayOrSSRTests != null)
                    {
                        foreach (RelayORSSRTests test in _catList.RelayOrSSRTests)
                        {
                            relayOrSSRTestsDetails.Add(test);
                        }
                    }

                    calibrationconstDetails.Clear();
                    if (_catList.CalibrationConstantsTests != null)
                    {
                        foreach (CalibrationConstants test in _catList.CalibrationConstantsTests)
                        {
                            calibrationconstDetails.Add(test);
                        }
                    }

                    commonTestsDetails.Clear();
                    if (_catList.CommonCalibTests != null)
                    {
                        foreach (CommonTests test in _catList.CommonCalibTests)
                        {
                            commonTestsDetails.Add(test);
                        }
                    }
                    
                    //List of group sequence
                    ListOfGroupSequence.Clear();
                    if (_catList.ListOfGroupSequence != null)
                    {
                        foreach (string grpname in _catList.ListOfGroupSequence)
                        {
                            ListOfGroupSequence.Add(grpname);
                        }
                    }


                    IsmAmpInputTest = _catList.IsmAmpTestEnabled;
                    IsVoltInputTest = _catList.IsVoltTestEnabled;
                    IsPT100sensorInputTest = _catList.IsPT100SensorTestEnabled;
                    IsRsensorInputTest = _catList.IsRSensorTestEnabled;
                    IsJsensorInputTest = _catList.IsJSensorTestEnabled;

                    AccuracymAmpTestsDetails.Clear();
                    if (_catList.mAmpTests != null)
                    {
                        foreach (AccuracyTests test in _catList.mAmpTests)
                        {
                            AccuracymAmpTestsDetails.Add(test);
                        }
                    }

                    AccuracyVoltTestsDetails.Clear();
                    if (_catList.VoltTests != null)
                    {
                        foreach (AccuracyTests test in _catList.VoltTests)
                        {
                            AccuracyVoltTestsDetails.Add(test);
                        }
                    }

                    AccuracyPT100SnsrTestsDetails.Clear();
                    if (_catList.PT100SensorTests != null)
                    {
                        foreach (AccuracyTests test in _catList.PT100SensorTests)
                        {
                            AccuracyPT100SnsrTestsDetails.Add(test);
                        }
                    }

                    AccuracyRSensorTestsDetails.Clear();
                    if (_catList.RSensor != null)
                    {
                        foreach (AccuracyTests test in _catList.RSensor)
                        {
                            AccuracyRSensorTestsDetails.Add(test);
                        }
                    }

                    AccuracyJSensorTestsDetails.Clear();
                    if (_catList.JSensor != null)
                    {
                        foreach (AccuracyTests test in _catList.JSensor)
                        {
                            AccuracyJSensorTestsDetails.Add(test);
                        }
                    }

                    AccuracyTestSequenceList.Clear();
                    if (_catList.ListOfAccuracyTestsSequence != null)
                    {
                        foreach (string testname in _catList.ListOfAccuracyTestsSequence)
                        {
                            AccuracyTestSequenceList.Add(testname);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Yes, No Buttons

        private void BtnNoClk(object obj)
        {
            IsDialogOpen = false;
            MsgVis = false;
        }

        private void BtnYesClk(object obj)
        {
            try
            {
                switch (EventSender)
                {
                    case "SaveNewCatId":
                        SaveNewcatId();
                        IsDialogOpen = false;
                        IsCheckBoxEnabled = false;
                        break;

                    case "SaveNewSeriesType":
                        SaveNewSeries();
                        IsDialogOpen = false;
                        break;

                    case "SaveEditedJson":
                        SaveEditedcatId();
                        IsDialogOpen = false;
                        IsCheckBoxEnabled = false;
                        break;

                    case "DeleteCatId":
                        DeleteCatID(EventParam);
                        EventParam = "";
                        IsDialogOpen = false;
                        break;

                    case "DeleteDeviceType":
                        DeleteSeriesType(EventParam);
                        EventParam = "";

                        break;

                    case "Error":

                        EventParam = "";
                        break;

                    case "SaveAnalogIPTests":
                        AnalogInputTests analogInputTests = clsAnalogInputTests.SaveAnalogIPTests();
                        analogInputTestsDetails.Clear();
                        analogInputTestsDetails.Add(analogInputTests);
                        EventParam = "";
                        break;

                    case "SaveAnalogOPTests":
                        AnalogOutputTests analogoutputTests = clsAnalogOutputTests.SaveAnalogOutputTests();
                        analogOutputTestsDetails.Clear();
                        analogOutputTestsDetails.Add(analogoutputTests);
                        EventParam = "";
                        break;

                    case "SaveTC_RTDTests":
                        TC_RTDCalibTests tC_RTDCalibTests = clsTC_RTDTests.SaveTC_RTDTests();
                        tcRTDTestsDetails.Clear();
                        tcRTDTestsDetails.Add(tC_RTDCalibTests);
                        EventParam = "";
                        break;

                    case "SaveRelayOrSSRTests":
                        RelayORSSRTests relayOrSSRTests = clsRelayORSSRTests.SaveRelayOrSSRTests();
                        relayOrSSRTestsDetails.Clear();
                        relayOrSSRTestsDetails.Add(relayOrSSRTests);
                        EventParam = "";
                        break;

                    case "SaveCalibConstTests":
                        CalibrationConstants calibrationConstantstests = clsCalibrationConstantTests.SaveCalibConstantsTests();
                        calibrationconstDetails.Clear();
                        calibrationconstDetails.Add(calibrationConstantstests);
                        break;

                    case "SaveCommonTests":
                        CommonTests commonTests = clsCommonTests.SaveCalibConstantsTests();
                        commonTestsDetails.Clear();
                        commonTestsDetails.Add(commonTests);
                        break;

                    case "SaveCalibDelays":
                        CalibrationDelays calibrationDelays = clsCalibrationDelays.SaveCalibrationDelays();
                        CalibrationDelaysDetails.Clear();
                        CalibrationDelaysDetails.Add(calibrationDelays);
                        break;

                    case "SaveCalibDelaysPR43":
                        CalibrationDelaysPR43 calibrationDelaysPr43 = clsCalibrationDelaysPR43.SaveCalibrationDelays();
                        CalibrationDelaysDetailsPR43.Clear();
                        CalibrationDelaysDetailsPR43.Add(calibrationDelaysPr43);
                        break;

                    case "SaveCalibDelaysPI":
                        CalibrationDelaysPI calibrationDelaysPI = clsCalibrationDelaysPI.SaveCalibrationDelays();
                        CalibrationDelaysPIDetails.Clear();
                        CalibrationDelaysPIDetails.Add(calibrationDelaysPI);
                        break;

                    case "SaveTolerancesOfPI":
                        TolerancesOfPI tolerances = clsTolerancesOfPI.SaveTolerancedetails();
                        TolerancesofPIDetails.Clear();
                        TolerancesofPIDetails.Add(tolerances);
                        break;

                    case "SaveTolerancesOfPR69":
                        TolerancesOfPR69 tolerancespr69 = clsTolerancesOfPR69.SaveTolerancedetails();
                        TolerancesofPR69Details.Clear();
                        TolerancesofPR69Details.Add(tolerancespr69);
                        break;

                    case "SavewholeCalibData":
                        DelaytoleranceVisibility(false);
                        SavewholecalibDataDetails();
                        break;
                        
                    case "SaveAccuracyTests":
                        AccuracyTests accuracyTests = clsAccuracyTests.SaveAccuracyTestsTests();

                        switch (clsGlobalVariables.strAccuracyParameter)
                        {
                            case clsGlobalVariables.AccuracyParameter.mAmp:
                                AccuracymAmpTestsDetails.Clear();
                                AccuracymAmpTestsDetails.Add(accuracyTests);
                                break;
                            case clsGlobalVariables.AccuracyParameter.Volt:
                                AccuracyVoltTestsDetails.Clear();
                                AccuracyVoltTestsDetails.Add(accuracyTests);
                                break;
                            case clsGlobalVariables.AccuracyParameter.PT100Sensor:
                                AccuracyPT100SnsrTestsDetails.Clear();
                                AccuracyPT100SnsrTestsDetails.Add(accuracyTests);
                                break;
                            case clsGlobalVariables.AccuracyParameter.RSensor:
                                AccuracyRSensorTestsDetails.Clear();
                                AccuracyRSensorTestsDetails.Add(accuracyTests);
                                break;
                            case clsGlobalVariables.AccuracyParameter.JSensor:
                                AccuracyJSensorTestsDetails.Clear();
                                AccuracyJSensorTestsDetails.Add(accuracyTests);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }

            IsDialogOpen = false;
            MsgVis = false;
        }

        private void DelaytoleranceVisibility(bool state)
        {
            IsCalibDelayTestsbtnVis = state;
            IstolerancebtnVis = state;
            IsSaveDelayToleranceBtnVis = state;
            BrowsemotfileBtnVis = state;            
        }
        #endregion

        #region Edit, Delete Cat id

        private void DeleteSeriesType(string DeviceType)
        {
            try
            {
                int found = 0;
                for (int CounterDltCatId = 0; CounterDltCatId < ModifiedCatId.Count; CounterDltCatId++)
                {
                    for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterDltCatId].ConfigurationData.Count; CounterConfigData++)
                    {
                        if (ModifiedCatId[CounterDltCatId].ConfigurationData[CounterConfigData].DeviceType == DeviceType)
                        {
                            ModifiedCatId[CounterDltCatId].ConfigurationData.RemoveAt(CounterConfigData);

                            found = 1;
                            // serialize JSON directly to a file
                            string res = JsonConvert.SerializeObject(ModifiedCatId[0], Formatting.Indented);
                            System.IO.File.WriteAllText(clsGlobalVariables.configJsonfilepath, res);
                            ModifiedCatId.Clear();
                            ConfigurationDataList result = new ConfigurationDataList();
                            using (StreamReader file = File.OpenText(clsGlobalVariables.configJsonfilepath))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                result = (ConfigurationDataList)serializer.Deserialize(file, typeof(ConfigurationDataList));
                            }
                            ModifiedCatId.Add(result);
                            break;
                        }
                    }
                    if (found == 1) { break; }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void DeleteCatID(string DeviceName)
        {
            int found = 0;
            for (int Countercatid = 0; Countercatid < ModifiedCatId.Count; Countercatid++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[Countercatid].ConfigurationData.Count; CounterConfigData++)
                {
                    for (int CounterCatIdList = 0; CounterCatIdList < ModifiedCatId[Countercatid].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCatIdList++)
                    {
                        if (ModifiedCatId[Countercatid].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList].DeviceName == DeviceName)
                        {
                            ModifiedCatId[Countercatid].ConfigurationData[CounterConfigData].CatIdLists.RemoveAt(CounterCatIdList);

                            found = 1;
                            // serialize JSON directly to a file
                            string res = JsonConvert.SerializeObject(ModifiedCatId[0], Formatting.Indented);
                            System.IO.File.WriteAllText(clsGlobalVariables.configJsonfilepath, res);
                            ModifiedCatId.Clear();
                            ConfigurationDataList result = new ConfigurationDataList();
                            using (StreamReader file = File.OpenText(clsGlobalVariables.configJsonfilepath))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                result = (ConfigurationDataList)serializer.Deserialize(file, typeof(ConfigurationDataList));
                            }
                            ModifiedCatId.Add(result);
                            break;
                        }

                    }
                    if (found == 1) { break; }
                }
                if (found == 1) { break; }
            }
        }

        private void DeleteCatIdClk(object obj)
        {
            PopupBtnVisibility(true);

            ShowMessageBox("Do you want to delete Catalogue Id: " + obj.ToString() + " ?", true, "DeleteCatId", obj.ToString(), clsGlobalVariables.MsgIcon.Question);
        }

        private void EditCatIdClk(object obj)
        {

            IsDeviceNameUneditable = true;
            IsCheckBoxEnabled = true;
            IsSequencebtnEnabled = true;
            PopupBtnVisibility(false);
            object objEdit = new CatIdList();

            for (int CounterEditCatId = 0; CounterEditCatId < ModifiedCatId.Count; CounterEditCatId++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterEditCatId].ConfigurationData.Count; CounterConfigData++)
                {
                    for (int CounterCaTIdList = 0; CounterCaTIdList < ModifiedCatId[CounterEditCatId].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCaTIdList++)
                    {
                        if (ModifiedCatId[CounterEditCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCaTIdList].DeviceName == obj.ToString())
                        {
                            objEdit = ModifiedCatId[CounterEditCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCaTIdList];
                            AssignDataToFields(objEdit);
                            break;
                        }
                        else
                        {
                            objEdit = null;
                        }

                    }
                    if (objEdit != null) { break; }
                }
                if (objEdit != null) { break; }
            }

            Savebtntext = "Save Cat Id";
            IsSaveBtnVis = true;
            SaveParameter = "SaveEditedCatId";
        }

        private void DeleteSeriesClk(object obj)
        {
            ShowMessageBox("Do you want to delete Devicetype: " + obj.ToString() + " ? ", true, "DeleteDeviceType", obj.ToString(), clsGlobalVariables.MsgIcon.Question);

        }

        private void AddCatIdClk(object obj)
        {
            IsDeviceNameUneditable = false;
            IsCheckBoxEnabled = true;
            DeviceName = "";
            DeviceType = obj.ToString();
            Savebtntext = "Save Cat Id";
            IsSaveBtnVis = true;
            SaveParameter = "SaveNewCatId";

            ListOfGroupSequence.Clear();

            ListOfGroupSequence.Add(clsGlobalVariables.strAnalogInputTests);
            ListOfGroupSequence.Add(clsGlobalVariables.strAnalogOutputTests);
            ListOfGroupSequence.Add(clsGlobalVariables.strTCRTDTests);
            ListOfGroupSequence.Add(clsGlobalVariables.strRelaysSSRTests);
            ListOfGroupSequence.Add(clsGlobalVariables.strCalibConstTests);
            ListOfGroupSequence.Add(clsGlobalVariables.strCommonTests);
        }

        private void AddSeriesClk(object obj)
        {
            DeviceType = "";
            IsDeviceTypeUneditable = false;
            Savebtntext = "Save Series";
            IsSaveBtnVis = true;
            SaveParameter = "SaveNewSeries";
        }

        #endregion

        #region Save New, Edited
        private void SaveEditedcatId()
        {
            try
            {
                if (!IsAnalogInputTest)
                    analogInputTestsDetails = null;
                if (!IsAnalogOutputTest)
                    analogOutputTestsDetails = null;
                if (!IsTC_RTDTest)
                    tcRTDTestsDetails = null;
                if (!IsRelayTest)
                    relayOrSSRTestsDetails = null;
                if (!IsCalibConstantTest)
                    calibrationconstDetails = null;
                if (!IsCommonTests)
                    commonTestsDetails = null;
                

                CatIdList EditedcatidObj = new CatIdList()
                {
                    DeviceId = DeviceId,
                    DeviceName = DeviceName,
                    Description = Description,
                    ModbusSupport = ModbusSupport,
                    Communication_Start_Time = Communication_Start_Time,
                    IsAnalogInputTestApplicable = IsAnalogInputTest,
                    AnalogIpTests = analogInputTestsDetails,
                    IsAnalogOutputTestApplicable = IsAnalogOutputTest,
                    AnalogOpTests = analogOutputTestsDetails,
                    IsTC_RTDTestApplicable = IsTC_RTDTest,
                    TC_RTDTests = tcRTDTestsDetails,
                    IsRelayOrSSRTestsApplicable = IsRelayTest,
                    RelayOrSSRTests = relayOrSSRTestsDetails,
                    IsCalibrationConstApplicable = IsCalibConstantTest,
                    CalibrationConstantsTests = calibrationconstDetails,
                    IsCommonTestsApplicable = IsCommonTests,
                    CommonCalibTests = commonTestsDetails,
                    ListOfGroupSequence = ListOfGroupSequence,

                    IsmAmpTestEnabled = IsmAmpInputTest,
                    IsVoltTestEnabled = IsVoltInputTest,
                    IsPT100SensorTestEnabled = IsPT100sensorInputTest,
                    IsRSensorTestEnabled = IsRsensorInputTest,
                    IsJSensorTestEnabled = IsJsensorInputTest,
                    mAmpTests = AccuracymAmpTestsDetails,
                    VoltTests = AccuracyVoltTestsDetails,
                    PT100SensorTests = AccuracyPT100SnsrTestsDetails,
                    RSensor = AccuracyRSensorTestsDetails,
                    JSensor = AccuracyJSensorTestsDetails,
                    ListOfAccuracyTestsSequence = AccuracyTestSequenceList

                };

                int found = 0;
                for (int CounterCatId = 0; CounterCatId < ModifiedCatId.Count; CounterCatId++)
                {
                    for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[CounterCatId].ConfigurationData.Count; CounterConfigData++)
                    {
                        for (int CounterCatIdList = 0; CounterCatIdList < ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists.Count; CounterCatIdList++)
                        {
                            if (ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList].DeviceName == EditedcatidObj.DeviceName)
                            {
                                ModifiedCatId[CounterCatId].ConfigurationData[CounterConfigData].CatIdLists[CounterCatIdList] = EditedcatidObj;
                                IsNotEditable = true;
                                IsCheckBoxEnabled = false;
                                IsSaveBtnVis = false;

                                found = 1;
                                string res = JsonConvert.SerializeObject(ModifiedCatId[0], Formatting.Indented);
                                System.IO.File.WriteAllText(clsGlobalVariables.configJsonfilepath, res);
                                ModifiedCatId.Clear();
                                ConfigurationDataList result = new ConfigurationDataList();
                                using (StreamReader file = File.OpenText(clsGlobalVariables.configJsonfilepath))
                                {
                                    JsonSerializer serializer = new JsonSerializer();
                                    result = (ConfigurationDataList)serializer.Deserialize(file, typeof(ConfigurationDataList));
                                }
                                ModifiedCatId.Add(result);
                                break;
                            }
                        }
                        if (found == 1) { break; }
                    }
                    if (found == 1) { break; }
                }
            }
            catch (Exception)
            {

            }
        }
        private void SaveNewSeries()
        {
            try
            {
                ConfigurationData NewSeriesType = new ConfigurationData()
                {
                    DeviceType = DeviceType,
                    CatIdLists = new ObservableCollection<CatIdList>()
                };

                ModifiedCatId[0].ConfigurationData.Add(NewSeriesType);
                IsSaveBtnVis = false;

                string res = JsonConvert.SerializeObject(ModifiedCatId[0], Formatting.Indented);
                System.IO.File.WriteAllText(clsGlobalVariables.configJsonfilepath, res);
                ModifiedCatId.Clear();
                ConfigurationDataList result = new ConfigurationDataList();
                using (StreamReader file = File.OpenText(clsGlobalVariables.configJsonfilepath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    result = (ConfigurationDataList)serializer.Deserialize(file, typeof(ConfigurationDataList));
                }
                ModifiedCatId.Add(result);
            }
            catch (Exception ex)
            {
                //ShowMessageBox.Show("In SaveNewDeviceType : " + ex.Message);
            }
        }

        private void SaveNewcatId()
        {

            if (!IsAnalogInputTest)
                analogInputTestsDetails = null;
            if (!IsAnalogOutputTest)
                analogOutputTestsDetails = null;
            if (!IsTC_RTDTest)
                tcRTDTestsDetails = null;
            if (!IsRelayTest)
                relayOrSSRTestsDetails = null;
            if (!IsCalibConstantTest)
                calibrationconstDetails = null;
            if (!IsCommonTests)
                commonTestsDetails = null;

            CatIdList NewcatidObj = new CatIdList()
            {
                DeviceId = DeviceId,
                DeviceName = DeviceName,
                Description = Description,
                ModbusSupport = ModbusSupport,
                Communication_Start_Time = Communication_Start_Time,
                IsAnalogInputTestApplicable = IsAnalogInputTest,
                AnalogIpTests = analogInputTestsDetails,
                IsAnalogOutputTestApplicable = IsAnalogOutputTest,
                AnalogOpTests = analogOutputTestsDetails,
                IsTC_RTDTestApplicable = IsTC_RTDTest,
                TC_RTDTests = tcRTDTestsDetails,
                IsRelayOrSSRTestsApplicable = IsRelayTest,
                RelayOrSSRTests = relayOrSSRTestsDetails,
                IsCalibrationConstApplicable = IsCalibConstantTest,
                CalibrationConstantsTests = calibrationconstDetails,
                IsCommonTestsApplicable = IsCommonTests,
                CommonCalibTests = commonTestsDetails,
                ListOfGroupSequence = ListOfGroupSequence,

                IsmAmpTestEnabled = IsmAmpInputTest,
                IsVoltTestEnabled = IsVoltInputTest,
                IsPT100SensorTestEnabled = IsPT100sensorInputTest,
                IsRSensorTestEnabled = IsRsensorInputTest,
                IsJSensorTestEnabled = IsJsensorInputTest,
                mAmpTests = AccuracymAmpTestsDetails,
                VoltTests = AccuracyVoltTestsDetails,
                PT100SensorTests = AccuracyPT100SnsrTestsDetails,
                RSensor = AccuracyRSensorTestsDetails,
                JSensor = AccuracyJSensorTestsDetails,
                ListOfAccuracyTestsSequence = AccuracyTestSequenceList

            };

            int found = 0;
            for (int Countercatid = 0; Countercatid < ModifiedCatId.Count; Countercatid++)
            {
                for (int CounterConfigData = 0; CounterConfigData < ModifiedCatId[Countercatid].ConfigurationData.Count; CounterConfigData++)
                {
                    if (ModifiedCatId[Countercatid].ConfigurationData[CounterConfigData].DeviceType == DeviceType)
                    {
                        //ModifiedCatId[i].ConfigData[j].CatList.RemoveAt(z);
                        ModifiedCatId[Countercatid].ConfigurationData[CounterConfigData].CatIdLists.Add(NewcatidObj);
                        found = 1;
                        IsSaveBtnVis = false;
                        // serialize JSON directly to a file
                        string res = JsonConvert.SerializeObject(ModifiedCatId[0], Formatting.Indented);
                        System.IO.File.WriteAllText(clsGlobalVariables.configJsonfilepath, res);
                        ModifiedCatId.Clear();
                        ConfigurationDataList result = new ConfigurationDataList();
                        using (StreamReader file = File.OpenText(clsGlobalVariables.configJsonfilepath))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            result = (ConfigurationDataList)serializer.Deserialize(file, typeof(ConfigurationDataList));
                        }
                        ModifiedCatId.Add(result);

                        break;
                    }
                }
                if (found == 1) { break; }
            }
        } 
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        public void InVisibleAllGroups()
        {
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            TC_RTDTestsVis = false;
            RelayOrSSRTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;

            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;
        }
        
        private void ShowMessageBox(string msg, bool yesNo, string sender, string eventParam, clsGlobalVariables.MsgIcon icon)
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

            Msg = msg;
            EventSender = sender;
            EventParam = eventParam;
            MesssageVis = true;
            MsgVis = yesNo;            
            IsDialogOpen = true;
        }

        private void BrowsemotfileClk(object obj)
        {
            var dialog = new FolderBrowserDialog();

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Motfilepath = dialog.SelectedPath;
                Environment.SpecialFolder root = dialog.RootFolder;
            }
        }

        private void SaveDelayToleranceClk(object obj)
        {
            InVisibleAllGroups();
            PopupBtnVisibility(true);
            ShowMessageBox("Do you want to save the changes?", true, "SavewholeCalibData", "", clsGlobalVariables.MsgIcon.Question);
        }

        private void SavewholecalibDataDetails()
        {
            ModifiedCatId[0].CalibrationDelays[0] = CalibrationDelaysDetails[0];
            ModifiedCatId[0].CalibrationDelaysPR43[0] = CalibrationDelaysDetailsPR43[0];
            ModifiedCatId[0].CalibrationDelaysPI[0] = CalibrationDelaysPIDetails[0];
            ModifiedCatId[0].TolerancesofPI[0] = TolerancesofPIDetails[0];
            ModifiedCatId[0].TolerancesOfPR69[0] = TolerancesofPR69Details[0];
            ModifiedCatId[0].motfilepath = Motfilepath;

            string res = JsonConvert.SerializeObject(ModifiedCatId[0], Formatting.Indented);
            System.IO.File.WriteAllText(clsGlobalVariables.configJsonfilepath, res);
            ModifiedCatId.Clear();
            ConfigurationDataList result = new ConfigurationDataList();
            using (StreamReader file = File.OpenText(clsGlobalVariables.configJsonfilepath))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (ConfigurationDataList)serializer.Deserialize(file, typeof(ConfigurationDataList));
            }
            ModifiedCatId.Add(result);
        }

        private void EditToleranceClk(object obj)
        {
            //ToleranceEditVis
            clsTolerancesOfPI = new clsTolerancesOfPI();

            clsTolerancesOfPI.ParseTolerancedetails(ModifiedCatId);

            EventSender = "SaveTolerancesOfPI";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            TC_RTDTestsVis = false;
            RelayOrSSRTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;

            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = true;
            ToleranceofPR69EditVis = false;

            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }

        private void EditTolerancePR69Clk(object obj)
        {
            //ToleranceEditVis
            clsTolerancesOfPR69 = new clsTolerancesOfPR69();

            clsTolerancesOfPR69.ParseTolerancedetails(ModifiedCatId);

            EventSender = "SaveTolerancesOfPR69";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            TC_RTDTestsVis = false;
            RelayOrSSRTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;

            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = true;

            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }

        private void EditCalibrationDelaysClk(object obj)
        {
            clsCalibrationDelays = new clsCalibrationDelays();
            clsCalibrationDelays.ParseCalibrationDelays(ModifiedCatId);

            EventSender = "SaveCalibDelays";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            TC_RTDTestsVis = false;
            RelayOrSSRTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;

            CalibDelaysEditVis = true;
            CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = false;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;

            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);
        }


        private void EditCalibrationDelaysPIClk(object obj)
        {
            clsCalibrationDelaysPI = new clsCalibrationDelaysPI();
            clsCalibrationDelaysPI.ParseCalibrationDelays(ModifiedCatId);
            
            EventSender = "SaveCalibDelaysPI";
            AnalogIPTestsEditVis = false;
            AnalogOPTestsEditVis = false;
            TC_RTDTestsVis = false;
            RelayOrSSRTestsVis = false;
            CalibConstTestsVis = false;
            CommonTestsVis = false;
            AccuracyDetailsEditVis =false;

            CalibDelaysEditVis = false; CalibDelaysPR43EditVis = false;
            CalibDelaysPIEditVis = true;
            ToleranceEditVis = false;
            ToleranceofPR69EditVis = false;

            IsDialogOpen = true;
            MsgVis = true;
            MesssageVis = false;
            PopupBtnVisibility(true);

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
