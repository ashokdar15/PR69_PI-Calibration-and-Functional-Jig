using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{
    public class AccuracyWindowVM : INotifyPropertyChanged
    {
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
            _StartAccuracyTesting = new RelayCommand(StartAccuracyTestingClk);
            tmrPVTimerTimeout.Tick += TmrPVTimerTimeout_Tick;

            _StopAccuracyTesting = new RelayCommand(Stoptesting);
            _NextAccuracyTesting = new RelayCommand(NextAccuracyTestClk);

            _AccuracymAmpTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();
            _AccuracyVoltTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();
            _AccuracyPT100SnsrTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();
            _AccuracyRSensorTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();
            _AccuracyJSensorTestsDetails = new ObservableCollection<clsAccuracyTestsDevices>();

            EnableDUT();

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

            
        }

        private void NextAccuracyTestClk(object obj)
        {
            
        }

        private void Stoptesting(object obj)
        {
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
                            AccuracymAmpTestsDetails.Add(new clsAccuracyTestsDevices() { Testnumber = 1, AccuracyParameter = "mAmp", TestPoint = objAccuracyTests.P1,TestresultDevice1 ="10.10" });
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

        private void StartAccuracyTestingClk(object obj)
        {
           
            StartStopWatch(true);
            
            clsGlobalVariables.strAccuracyParameter = clsGlobalVariables.AccuracyParameter.RSensor;

            //UpdateTestResult(2, 2, "10.12", clsGlobalVariables.AccuracyParameter.RSensor);
            //UpdateTestResult(1,2,"15.12", clsGlobalVariables.AccuracyParameter.RSensor);

            //Auto com port detection
            if (clsGlobalVariables.objGlobalFunction.AutomaticCOMPortDetections(clsGlobalVariables.NUMBER_OF_DUTS) != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                System.Windows.Forms.MessageBox.Show("fail Auto maticCOMPortDetections");
                StartStopWatch(false);
                return;
            }
            //        public static string mAmpAccuracyTest = "mAmpAccTests";
            //public static string voltAccuracyTest = "voltAccTests";
            //public static string pt100sensorAccuracyTest = "pt100sensorAccTests";
            //public static string RsensorAccuracyTest = "RsensorAccTests";
            //public static string JsensorAccuracyTest = "JsensorAccTests";
            Dictionary<string, List<string>> AccuracyList = new Dictionary<string, List<string>>();
            GetAccuracyDataFromJSON(AccuracyList);

            //start accuracy with user define point
            //write constant
            //write data log in sqlite.

            StartStopWatch(false);
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

        public void UpdateTestResult(int DUTNumber, int testnumber, string result,clsGlobalVariables.AccuracyParameter accuracyParameter)
        {          
           
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
                                    AccuracymAmpTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1= obj.TestresultDevice1 ,TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
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
                                    AccuracyVoltTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1= obj.TestresultDevice1 ,TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
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
                                    AccuracyPT100SnsrTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1= obj.TestresultDevice1 ,TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
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
                                    AccuracyRSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1= obj.TestresultDevice1 ,TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
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
                                    AccuracyJSensorTestsDetails[item.Testnumber - 1] = new clsAccuracyTestsDevices { Testnumber = obj.Testnumber, TestPoint = TestPoint, TestresultDevice1= obj.TestresultDevice1 ,TestresultDevice2 = result, TestresultDevice3 = obj.TestresultDevice3, TestresultDevice4 = obj.TestresultDevice4, TestresultDevice5 = obj.TestresultDevice5, TestresultDevice6 = obj.TestresultDevice6 };
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
            
            
        }

        private bool VoltSensorTest(bool firstIteration, string testPoint,byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;

            if (firstIteration)
            {
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                    return false;
                clsMessages.DisplayMessage(clsMessageIDs.VOLT_CALIBRATION_MSG_ID);
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.mA_V_AccuracyDelay;
                btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE,1);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
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

                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_VOLT_KNOB_POS, clsGlobalVariables.SOURCE_VOLT_KNOB_TEXT, DUT);

                                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(testPoint, DUT);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                return false;
            blnmDivideBy100 = true;
            btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.R_SENSOR,DUT);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                return false;

            return true;
        }
        private bool RSensorText(bool firstIteration, string testPoint,byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (firstIteration)
            {
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                    return false;
                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                    clsMessages.DisplayMessage(clsMessageIDs.TWO_WIRE_MSG_96x96);
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID);
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID_PI);
                else
                {
                    System.Windows.Forms.MessageBox.Show("Not Implemented");
                    return false;
                }
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.ThermoCouple_AccuracyDelay;
                btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_TEXT, DUT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_R_TYPE, DUT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
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
                    return false;
                }
            }
            clsMessages.ShowMessageInProgressWindowForAccuracy(clsMessageIDs.ACCURACY_R, testPoint + "°C.");
            blnmDivideBy100 = false;
            btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.R_SENSOR,DUT);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                return false;

            return true;
        }
        private bool JSensorTest(bool firstIteration, string testPoint,byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (firstIteration)
            {
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                    return false; 

                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                    clsMessages.DisplayMessage(clsMessageIDs.TWO_WIRE_MSG_96x96);
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID);
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                    clsMessages.DisplayMessage(clsMessageIDs.TWOWIRE_MSG_ID_PI);
                else
                { 
                    System.Windows.Forms.MessageBox.Show("Not Implemented");
                    return false;
                }
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.ThermoCouple_AccuracyDelay;
                btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_TEXT, DUT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_J_TYPE,1);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
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

                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        return false;
                    }
                }
            }
            clsMessages.ShowMessageInProgressWindowForAccuracy(clsMessageIDs.ACCURACY_J, testPoint + "°C.");
            blnmDivideBy100 = false;
            btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.J_SENSOR,DUT);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                return false;
            }
            return true;
        }
        private bool mAmpSensorTest(bool firstIteration, string testPoint,byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (firstIteration)
            {
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                    return false;
                clsMessages.DisplayMessage(clsMessageIDs.MA_CALIBRATION_MSG_ID);
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.mA_V_AccuracyDelay;
                 btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_20mA_TYPE,1);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
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
                    return false;
                }
                btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_ZERO, DUT);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    btmRetVal = SetISCH(clsGlobalVariables.TWENTY_mAMP, DUT);
                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        btmRetVal = SetISCL(clsGlobalVariables.FOUR_mAMP, DUT);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_TWO, DUT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_TEXT, DUT);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                return false;
            }
            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(testPoint, DUT);
            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
            {
                blnmDivideBy100 = true;
                btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.PT100_SENSOR,DUT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        private bool PT100SensorTest(bool firstIteration,string testPoint,byte DUT)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (firstIteration)
            {
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.PT100_AccuracyDelay;
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF(DUT) != (byte)clsGlobalVariables.enmResponseError.Success)
                    return false;
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
                else
                {
                    System.Windows.Forms.MessageBox.Show("Not Implemented");
                    return false;
                }
                 btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_RTD_KNOB_POS, clsGlobalVariables.SOURCE_RTD_KNOB_TEXT, DUT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn(DUT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
                btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_PT100_TYPE,1);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
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
            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
            {
                clsMessages.ShowMessageInProgressWindowForAccuracy(clsMessageIDs.ACCURACY_PT100, testPoint + "°C.");
                

                btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_ZERO, DUT);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    blnmDivideBy100 = false;
                    btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.PT100_SENSOR,DUT);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private byte TestAccuracy(string strmValue, byte btmSensor,byte DUT)
        {
            byte btmRetVal;
            try
            {
                btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(btmSensor, DUT);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistanceZeroTemp(strmValue, DUT);
                }                
                EnablePVTimeoutTimer();
                blnTimerElapsed = true;
                while (blnTimerElapsed && btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
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
                                // txtPVValue.Text = flmData.ToString();
                                System.Windows.Forms.MessageBox.Show(flmData.ToString());
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show(clsGlobalVariables.shrtgPV.ToString());
                                //txtPVValue.Text = clsGlobalVariables.shrtgPV.ToString();
                            }
                            //this.btnNext.Enabled = true;
                            //this.btnNext.Focus();
                            
                        }
                    }
                    catch (Exception)
                    {
                        return  (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
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
                    int imResultData;
                    imResultData = ((btmdata * 0x100) | clsGlobalVariables.DP_VAL);
                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices((byte)(clsGlobalVariables.MB_SLAVE_ID_WO_BASE + DUT),clsGlobalVariables.SET_WRITE_FUNC_CODE, imResultData);
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
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
