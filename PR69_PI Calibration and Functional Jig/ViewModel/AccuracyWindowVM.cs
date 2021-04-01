using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{
    public class AccuracyWindowVM : INotifyPropertyChanged
    {
        private RelayCommand _StartAccuracyTesting;
        public bool blnmDivideBy100 = false;
        public bool blnTimerElapsed = false;

        private ObservableCollection<clsAccuracyTestDeviceConnected> _AccuracymAmpTestsDetails;

        public ObservableCollection<clsAccuracyTestDeviceConnected> AccuracymAmpTestsDetails
        {
            get { return _AccuracymAmpTestsDetails; }
            set { _AccuracymAmpTestsDetails = value; OnPropertyChanged("AccuracymAmpTestsDetails"); }
        }

        private ObservableCollection<clsAccuracyTestDeviceConnected> _AccuracyVoltTestsDetails;

        public ObservableCollection<clsAccuracyTestDeviceConnected> AccuracyVoltTestsDetails
        {
            get { return _AccuracyVoltTestsDetails; }
            set { _AccuracyVoltTestsDetails = value; OnPropertyChanged("AccuracyVoltTestsDetails"); }
        }

        private ObservableCollection<clsAccuracyTestDeviceConnected> _AccuracyPT100SnsrTestsDetails;

        public ObservableCollection<clsAccuracyTestDeviceConnected> AccuracyPT100SnsrTestsDetails
        {
            get { return _AccuracyPT100SnsrTestsDetails; }
            set { _AccuracyPT100SnsrTestsDetails = value; OnPropertyChanged("AccuracyPT100SnsrTestsDetails"); }
        }

        private ObservableCollection<clsAccuracyTestDeviceConnected> _AccuracyRSensorTestsDetails;

        public ObservableCollection<clsAccuracyTestDeviceConnected> AccuracyRSensorTestsDetails
        {
            get { return _AccuracyRSensorTestsDetails; }
            set { _AccuracyRSensorTestsDetails = value; OnPropertyChanged("AccuracyRSensorTestsDetails"); }
        }

        private ObservableCollection<clsAccuracyTestDeviceConnected> _AccuracyJSensorTestsDetails;

        public ObservableCollection<clsAccuracyTestDeviceConnected> AccuracyJSensorTestsDetails
        {
            get { return _AccuracyJSensorTestsDetails; }
            set { _AccuracyJSensorTestsDetails = value; OnPropertyChanged("AccuracyJSensorTestsDetails"); }
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
        
        public AccuracyWindowVM()
        {
            
            _AccuracymAmpTestsDetails = new ObservableCollection<clsAccuracyTestDeviceConnected>();
            _AccuracyVoltTestsDetails = new ObservableCollection<clsAccuracyTestDeviceConnected>();
            _AccuracyPT100SnsrTestsDetails = new ObservableCollection<clsAccuracyTestDeviceConnected>();
            _AccuracyRSensorTestsDetails = new ObservableCollection<clsAccuracyTestDeviceConnected>();
            _AccuracyJSensorTestsDetails = new ObservableCollection<clsAccuracyTestDeviceConnected>();

            EnableDUT();

            if (clsGlobalVariables.Selectedcatid.IsmAmpTestEnabled)
            {
                IsmAmpVis = true;
                foreach (AccuracyTests objAccuracyTests in clsGlobalVariables.Selectedcatid.mAmpTests)
                {

                    FillTotalNumberOfPointsDetails(objAccuracyTests, clsGlobalVariables.AccuracyParameter.mAmp);
                                        
                }
            }
            else
                IsmAmpVis = false;

            if (clsGlobalVariables.Selectedcatid.IsVoltTestEnabled)
            {
                IsVoltVis = true;
                foreach (AccuracyTests objAccuracyTests in clsGlobalVariables.Selectedcatid.VoltTests)
                {
                    FillTotalNumberOfPointsDetails(objAccuracyTests, clsGlobalVariables.AccuracyParameter.Volt);
                }
            }
            else
                IsVoltVis = false;

            if (clsGlobalVariables.Selectedcatid.IsPT100SensorTestEnabled)
            {
                IsPT100SensorVis = true;
                foreach (AccuracyTests objAccuracyTests in clsGlobalVariables.Selectedcatid.PT100SensorTests)
                {
                    FillTotalNumberOfPointsDetails(objAccuracyTests, clsGlobalVariables.AccuracyParameter.PT100Sensor);
                }
            }
            else
                IsPT100SensorVis = false;

            if (clsGlobalVariables.Selectedcatid.IsRSensorTestEnabled)
            {
                IsRSensorVis = true;
                foreach (AccuracyTests objAccuracyTests in clsGlobalVariables.Selectedcatid.RSensor)
                {
                    FillTotalNumberOfPointsDetails(objAccuracyTests, clsGlobalVariables.AccuracyParameter.RSensor);
                }
            }
            else
                IsRSensorVis = false;

            if (clsGlobalVariables.Selectedcatid.IsJSensorTestEnabled)
            {
                IsJSensorVis = true;
                foreach (AccuracyTests objAccuracyTests in clsGlobalVariables.Selectedcatid.JSensor)
                {
                    FillTotalNumberOfPointsDetails(objAccuracyTests, clsGlobalVariables.AccuracyParameter.JSensor);
                }
            }
            else
                IsJSensorVis = false;

            _StartAccuracyTesting = new RelayCommand(StartAccuracyTestingClk);
            tmrPVTimerTimeout.Tick += TmrPVTimerTimeout_Tick;
        }

        private void FillTotalNumberOfPointsDetails(AccuracyTests objAccuracyTests, clsGlobalVariables.AccuracyParameter accuracyParameter)
        {
            int numberofpoints = Convert.ToInt32(objAccuracyTests.NumberTestPoints);
            switch (accuracyParameter)
            {
                case clsGlobalVariables.AccuracyParameter.mAmp:
                    Dispatcher.CurrentDispatcher.Invoke(delegate
                    {
                        AddAccuracymAmpTest(numberofpoints, objAccuracyTests);
                    });
                    break;
                case clsGlobalVariables.AccuracyParameter.Volt:

                    break;
                case clsGlobalVariables.AccuracyParameter.PT100Sensor:

                    break;
                case clsGlobalVariables.AccuracyParameter.RSensor:

                    break;
                case clsGlobalVariables.AccuracyParameter.JSensor:

                    break;
                default:
                    break;
            }
            
        }

        private void AddAccuracymAmpTest(int numberofpoints, AccuracyTests objAccuracyTests)
        {
            switch (numberofpoints)
            {
                case 1:
                    AccuracymAmpTestsDetails.Add(new clsAccuracyTestDeviceConnected() { AccuracyParameter = "mAmp", TestPoint = objAccuracyTests.P1 });
                    break;

                default:
                    break;
            }

        }

        private void AddAccuracyVoltTest(int numberofpoints, AccuracyTests objAccuracyTests)
        {
            switch (numberofpoints)
            {
                case 1:
                    AccuracyVoltTestsDetails.Add(new clsAccuracyTestDeviceConnected() { AccuracyParameter = "Volt", TestPoint = objAccuracyTests.P1 });
                    break;

                default:
                    break;
            }                      
        }

        private void AddAccuracyPT100SensorTest(int numberofpoints, AccuracyTests objAccuracyTests)
        {
            switch (numberofpoints)
            {
                case 1:
                    AccuracyVoltTestsDetails.Add(new clsAccuracyTestDeviceConnected() { AccuracyParameter = "PT100Sensor", TestPoint = objAccuracyTests.P1 });
                    break;

                default:
                    break;
            }
        }

        private void AddAccuracyRSensorTest(int numberofpoints, AccuracyTests objAccuracyTests)
        {
            switch (numberofpoints)
            {
                case 1:
                    AccuracyVoltTestsDetails.Add(new clsAccuracyTestDeviceConnected() { AccuracyParameter = "RSensor", TestPoint = objAccuracyTests.P1 });
                    break;

                default:
                    break;
            }
        }

        private void AddAccuracyJSensorTest(int numberofpoints, AccuracyTests objAccuracyTests)
        {
            switch (numberofpoints)
            {
                case 1:
                    AccuracyVoltTestsDetails.Add(new clsAccuracyTestDeviceConnected() { AccuracyParameter = "JSensor", TestPoint = objAccuracyTests.P1 });
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
            //Auto com port detection
            if (clsGlobalVariables.objGlobalFunction.AutomaticCOMPortDetections(clsGlobalVariables.NUMBER_OF_DUTS) != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                System.Windows.Forms.MessageBox.Show("fail Auto maticCOMPortDetections");
                return;
            }
            //start accuracy with user define point

            //write constant
            //write data log in sqlite.

        }
        private bool VoltSensorTest(bool firstIteration, string testPoint)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;

            if (firstIteration)
            {
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF() != (byte)clsGlobalVariables.enmResponseError.Success)
                    return false;
                clsMessages.DisplayMessage(clsMessageIDs.VOLT_CALIBRATION_MSG_ID);
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.mA_V_AccuracyDelay;
                btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_10V_TYPE);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
                //This check is for device having modbus.
                if (clsModelSettings.blnRS485Flag == true)
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_0_10V_TYPE_DOUBLE_ACTING);
                }
                else//Device without modbus
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_0_10V_TYPE);
                }

                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    
                    btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_ZERO);
                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        btmRetVal = SetISCH(clsGlobalVariables.TEN_Volt);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            btmRetVal = SetISCL(clsGlobalVariables.ZERO_VOLT);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_TWO);
                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {

                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_VOLT_KNOB_POS, clsGlobalVariables.SOURCE_VOLT_KNOB_TEXT);

                                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                    {
                                        btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
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
            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(testPoint);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                return false;
            blnmDivideBy100 = true;
            btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.R_SENSOR);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                return false;

            return true;
        }
        private bool RSensorText(bool firstIteration, string testPoint)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (firstIteration)
            {
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF() != (byte)clsGlobalVariables.enmResponseError.Success)
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
                btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_TEXT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_R_TYPE);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
                //This check is for device having modbus.                        
                if (clsModelSettings.blnRS485Flag == true)
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_R_TYPE_DOUBLE_ACTING);
                }
                else//Device without modbus
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_R_TYPE);
                }
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
            }
            clsMessages.ShowMessageInProgressWindowForAccuracy(clsMessageIDs.ACCURACY_R, testPoint + "°C.");
            blnmDivideBy100 = false;
            btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.R_SENSOR);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                return false;

            return true;
        }
        private bool JSensorTest(bool firstIteration, string testPoint)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (firstIteration)
            {
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF() != (byte)clsGlobalVariables.enmResponseError.Success)
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
                btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mV_KNOB_POS, clsGlobalVariables.SOURCE_mV_KNOB_TEXT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_J_TYPE);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
                //This check is for device having modbus.                        
                if (clsModelSettings.blnRS485Flag == true)
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_J_TYPE_DOUBLE_ACTING);
                }
                else//Device without modbus
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_J_TYPE);
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
            btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.J_SENSOR);
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                return false;

            }
            return true;
        }
        private bool mAmpSensorTest(bool firstIteration, string testPoint)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (firstIteration)
            {
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF() != (byte)clsGlobalVariables.enmResponseError.Success)
                    return false;
                clsMessages.DisplayMessage(clsMessageIDs.MA_CALIBRATION_MSG_ID);
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.mA_V_AccuracyDelay;
                 btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_0_20mA_TYPE);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
                //This check is for device having modbus.                        
                if (clsModelSettings.blnRS485Flag == true)
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_0_20mA_TYPE_DOUBLE_ACTING);
                }
                else//Device without modbus
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_0_20mA_TYPE);
                }
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
                btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_ZERO);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    btmRetVal = SetISCH(clsGlobalVariables.TWENTY_mAMP);
                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        btmRetVal = SetISCL(clsGlobalVariables.FOUR_mAMP);
                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_TWO);
                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_mA_KNOB_POS, clsGlobalVariables.SOURCE_mA_KNOB_TEXT);

                                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
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
            btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistance(testPoint);
            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
            {
                blnmDivideBy100 = true;
                btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.PT100_SENSOR);
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
        private bool PT100SensorTest(bool firstIteration,string testPoint)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            if (firstIteration)
            {
                clsGlobalVariables.igPV_TIMEOUT_DELAY = clsGlobalVariables.PT100_AccuracyDelay;
                if (clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOFF() != (byte)clsGlobalVariables.enmResponseError.Success)
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
                 btmRetVal = clsGlobalVariables.objCalibQueriescls.CheckSourceKnobPos(clsGlobalVariables.SOURCE_RTD_KNOB_POS, clsGlobalVariables.SOURCE_RTD_KNOB_TEXT);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }

                btmRetVal = clsGlobalVariables.objCalibQueriescls.MakeCalibratorSourceOn();
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
                btmRetVal = clsGlobalVariables.objQueriescls.ChangeSensor(clsGlobalVariables.SENSOR_PT100_TYPE);
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return false;
                }
            }
            
          
           
            //This check is for device having modbus.                        
            if (clsModelSettings.blnRS485Flag == true)
            {
                btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeDoubleActing(clsGlobalVariables.SENSOR_PT100_TYPE_DOUBLE_ACTING);
            }
            else//Device without modbus
            {
                btmRetVal = clsGlobalVariables.objQueriescls.ReadSensorTypeSingleActing(clsGlobalVariables.SENSOR_PT100_TYPE);
            }
            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
            {
                clsMessages.ShowMessageInProgressWindowForAccuracy(clsMessageIDs.ACCURACY_PT100, testPoint + "°C.");
                

                btmRetVal = ChangeDP(clsGlobalVariables.DP_VAL_ZERO);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    blnmDivideBy100 = false;
                    btmRetVal = TestAccuracy(testPoint, clsGlobalVariables.PT100_SENSOR);
                    if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private byte TestAccuracy(string strmValue, byte btmSensor)
        {
            byte btmRetVal;
            try
            {
                btmRetVal = clsGlobalVariables.objCalibQueriescls.ChangeCalibratorSensor(btmSensor);
                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    btmRetVal = clsGlobalVariables.objCalibQueriescls.MBAdjustCalibratorVoltageOrResistanceZeroTemp(strmValue);
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
                            btmData = clsGlobalVariables.objQueriescls.ReadPVDoubleActing();
                        }
                        else//Device without modbus
                        {
                            btmData = clsGlobalVariables.objQueriescls.ReadPVSingleActing();
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
        private byte ChangeDP(byte btmdata)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                //This check is for device having modbus.                
                if (clsModelSettings.blnRS485Flag == true)
                {
                    btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID, clsGlobalVariables.DP_SET, btmdata);
                }
                else//Device without modbus
                {
                    int imResultData;
                    imResultData = ((btmdata * 0x100) | clsGlobalVariables.DP_VAL);
                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_WRITE_FUNC_CODE, imResultData);
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private byte SetISCH(byte btmdata)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                {
                    if (clsGlobalVariables.TWENTY_mAMP == btmdata)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_AIRH, clsGlobalVariables.TWENTY_mAMP_PI);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            return btmRetVal;
                        }
                    }
                    else if (clsGlobalVariables.TEN_Volt == btmdata)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_AIRH, clsGlobalVariables.TEN_Volt_PI);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            return btmRetVal;
                        }
                    }

                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_ISCH, btmdata);
                }
                else
                {
                    //This check is for device having modbus.                
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID, clsGlobalVariables.ISCH_SET, btmdata);
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_ISCH, btmdata);
                    }
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private byte SetISCL(byte btmdata)
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                {
                    if (clsGlobalVariables.ZERO_mAMP == btmdata || clsGlobalVariables.ZERO_VOLT == btmdata)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_AIRL, clsGlobalVariables.ZERO_mAMP);
                        if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            return btmRetVal;
                        }
                    }
                    btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_ISCL, btmdata);
                }
                else
                {
                    //This check is for device having modbus.                
                    if (clsModelSettings.blnRS485Flag == true)
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBWriteHoldingReg(clsGlobalVariables.MB_DUT_ID, clsGlobalVariables.ISCL_SET, btmdata);
                    }
                    else//Device without modbus
                    {
                        btmRetVal = clsGlobalVariables.objQueriescls.MBQueryForWOModbusDevices(clsGlobalVariables.SET_ISCL, btmdata);
                    }
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
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
