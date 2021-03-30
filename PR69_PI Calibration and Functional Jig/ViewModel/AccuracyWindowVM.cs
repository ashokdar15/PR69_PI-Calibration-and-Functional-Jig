using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using System;
using System.Collections.Generic;
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
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        public RelayCommand StartAccuracyTesting
        {
            get { return _StartAccuracyTesting; }
            set { _StartAccuracyTesting = value; }
        }
        
        public AccuracyWindowVM()
        {
           
            _StartAccuracyTesting = new RelayCommand(StartAccuracyTestingClk);
            tmrPVTimerTimeout.Tick += TmrPVTimerTimeout_Tick;
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

            // write constant
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
        

    }
}
