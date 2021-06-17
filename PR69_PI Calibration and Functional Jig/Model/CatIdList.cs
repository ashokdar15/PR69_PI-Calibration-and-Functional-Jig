using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class CatIdList
    {
        //Added by Anil 

        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public bool ModbusSupport { get; set; }
        public string Communication_Start_Time { get; set; }
        
        public bool IsAnalogInputTestApplicable { get; set; }
        public IList<AnalogInputTests> AnalogIpTests { get; set; }
        public bool IsAnalogOutputTestApplicable { get; set; }
        public IList<AnalogOutputTests> AnalogOpTests { get; set; }
        public bool IsTC_RTDTestApplicable { get; set; }
        public IList<TC_RTDCalibTests> TC_RTDTests { get; set; }
        public bool IsRelayOrSSRTestsApplicable { get; set; }
        public IList<RelayORSSRTests> RelayOrSSRTests { get; set; }
        public bool IsCalibrationConstApplicable { get; set; }
        public IList<CalibrationConstants> CalibrationConstantsTests { get; set; }
        public bool IsCommonTestsApplicable { get; set; }
        public IList<CommonTests> CommonCalibTests { get; set; }
        public IList<string> ListOfGroupSequence { get; set; }

        public bool IsmAmpTestEnabled { get; set; }        
        public IList<AccuracyTests> mAmpTests { get; set; }

        public bool IsVoltTestEnabled { get; set; }        
        public IList<AccuracyTests> VoltTests { get; set; }

        public bool IsPT100SensorTestEnabled { get; set; }       
        public IList<AccuracyTests> PT100SensorTests { get; set; }

        public bool IsRSensorTestEnabled { get; set; }     
        public IList<AccuracyTests> RSensor { get; set; }

        public bool IsJSensorTestEnabled { get; set; }
        public IList<AccuracyTests> JSensor { get; set; }

        public IList<string> ListOfAccuracyTestsSequence { get; set; }

    }
    public class ConfigurationData
    {
        public string DeviceType { get; set; }
        public IList<CatIdList> CatIdLists { get; set; }
    }

    public class ConfigurationDataList
    {
        public IList<ConfigurationData> ConfigurationData { get; set; }
        public IList<CalibrationDelays> CalibrationDelays { get; set; }
        public IList<CalibrationDelaysPR43> CalibrationDelaysPR43 { get; set; }
        public IList<CalibrationDelaysPI> CalibrationDelaysPI { get; set; }
        public IList<TolerancesOfPI> TolerancesofPI { get; set; }
        public IList<TolerancesOfPR69> TolerancesOfPR69 { get; set; }
        public string motfilepath { get; set; }

    }
        
    public class AnalogInputTests
    {
        public bool CALIB_1V_CNT { get; set; }
        public bool CALIB_9V_CNT { get; set; }
        public bool CALIB_4mA_CNT { get; set; }
        public bool CALIB_20mA_CNT { get; set; }
        public bool CALIB_9V_CNT_PI { get; set; }
        public bool CALIB_1V_CNT_PI { get; set; }
        public bool CALIB_20mA_CNT_PI { get; set; }
        public bool CALIB_1mA_CNT_PI { get; set; }
    }

    public class AnalogOutputTests
    {
        public bool SET_DFALT_1MA_CNT { get; set; }   //PI
        public bool SET_DFALT_4MA_CNT { get; set; }   //PR69        
        public bool SET_OBSRVED_1MA_CNT { get; set; } //PI
        public bool SET_OBSRVED_4MA_CNT { get; set; } //PR69        
        public bool SET_DFALT_20MA_CNT { get; set; }
        public bool SET_OBSRVED_20MA_CNT { get; set; }
        public bool CALIBRATE_CURRENT { get; set; }
        public bool SET_12MA_ANLOP { get; set; }
        public bool CHK_ANALOG_OP_VAL { get; set; }        
        public bool SET_DFALT_1V_CNT { get; set; }
        public bool SET_OBSRVED_1V_CNT { get; set; }
        public bool SET_DFALT_10V_CNT { get; set; }
        public bool SET_OBSRVED_10V_CNT { get; set; }
        public bool CALIBRATE_VOLTAGE { get; set; }
        public bool SET_5V_ANLOP { get; set; }
       
    }

    public class TC_RTDCalibTests
    {
        public bool CALIB_1_MV_CNT { get; set; }
        public bool CALIB_47_68_MV_CNT { get; set; }     //PR43 (48X48 & 96X96)        
        public bool CALIB_50_MV_CNT { get; set; }        //PR69 (48X48 & 96X96) & PI
        public bool CALC_SLOPE_OFFSET { get; set; }      //PR69 (48X48 & 96X96)
        public bool CALIB_PT100 { get; set; }            //PR69 (48X48 & 96X96) & PI
        public bool CALIB_TC { get; set; }               //PR69 (48X48 & 96X96)
        public bool CALIB_100_OHM { get; set; }          //PR43 (48X48 & 96X96)
       // public bool CALIB_350_OHM { get; set; }       //PR43 (48X48 & 96X96)
        public bool CALIB_313_71_OHM { get; set; }       //PR43 (48X48 & 96X96)

    }

    public class RelayORSSRTests
    {
        public bool OP1{ get; set; }
        public string SelectedOP1Type { get; set; }
        public string SelectedOP1RelayType { get; set; }

        public bool OP2{ get; set; }
        public string SelectedOP2Type { get; set; }
        public string SelectedOP2RelayType { get; set; }

        public bool OP3{ get; set; }
        public string SelectedOP3Type { get; set; }
        public string SelectedOP3RelayType { get; set; }
        

    }

    public class CalibrationConstants
    {
        public bool WRITE_CALIB_CONST { get; set; }
        public bool WRITE_CALIB_CONST_WITH_VREF { get; set; }
    }

    public class CommonTests
    {
        public bool READ_DEVICE_ID { get; set; }
        public bool READ_CALIB_CONST { get; set; }
        public bool SWITCH_SENSOR_RELAY { get; set; }       
        public bool START_DISP_TEST { get; set; }
        public bool START_KEYPAD_TEST { get; set; }
        public bool Vtg24V_OP_TEST { get; set; }
        public bool START_MODBUS_TEST { get; set; }
        public bool CJC_TEST { get; set; }
    }

    public class AccuracyTests
    {
        public string NumberTestPoints { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Tolerance { get; set; }
        public string DelayForNexttest { get; set; }
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public string P4 { get; set; }
        public string P5 { get; set; }
        public string P6 { get; set; }
        public string P7 { get; set; }
        public string P8 { get; set; }
        public string P9 { get; set; }
        public string P10 { get; set; }
    }

    public class CalibrationDelays
    {
        public int ONEmV_DELAY_AFTER_STARTMODE { get; set; }
        public int ONEmV_DELAY_AFTER_RUNMODE { get; set; }
        public int PT100_DELAY_AFTER_STARTMODE { get; set; }
        public int PT100_DELAY_AFTER_RUNMODE { get; set; }
        public int FOURmA_DELAY_AFTER_STARTMODE { get; set; }
        public int FOURmA_DELAY_AFTER_RUNMODE { get; set; }
        public int ONEVolt_DELAY_AFTER_STARTMODE { get; set; }
        public int ONEVolt_DELAY_AFTER_RUNMODE { get; set; }
        public int CALIB_MEASURE_DELAY { get; set; }
        public int VREF_READ_DELAY_STARTMODE { get; set; }
        public int VREF_READ_DELAY_RUNMODE { get; set; }

    }

    public class CalibrationDelaysPR43
    {
        public int PT100_PR43_DELAY_AFTER_STARTMODE { get; set; }
        public int PT100_PR43_DELAY_AFTER_RUNMODE { get; set; }
        public int PT313_DELAY_AFTER_STARTMODE { get; set; }
        public int PT313_DELAY_AFTER_RUNMODE { get; set; }
       
    }

    public class CalibrationDelaysPI
    {
        public int ONEmV_DELAY_AFTER_STARTMODE { get; set; }
        public int ONEmV_DELAY_AFTER_RUNMODE { get; set; }
        public int PT100_DELAY_AFTER_STARTMODE { get; set; }
        public int PT100_DELAY_AFTER_RUNMODE { get; set; }
        public int FOURmA_DELAY_AFTER_STARTMODE { get; set; }
        public int FOURmA_DELAY_AFTER_RUNMODE { get; set; }
        public int ONEVolt_DELAY_AFTER_STARTMODE { get; set; }
        public int ONEVolt_DELAY_AFTER_RUNMODE { get; set; }
        public int CALIB_MEASURE_DELAY { get; set; }
       
    }

    public class TolerancesOfPI
    {
        public int FIVE_VOLT_MIN_PI { get; set; }
        public int FIVE_VOLT_MAX_PI { get; set; }
        public int TWELVE_mA_MIN_PI { get; set; }
        public int TWELVE_mA_MAX_PI { get; set; }
        public int One_VOLT_MAX_PI { get; set; }
        public int One_VOLT_MIN_PI { get; set; }
        public int TEN_VOLT_MAX_PI { get; set; }
        public int TEN_VOLT_MIN_PI { get; set; }
        public int ONE_mAMP_MAX { get; set; }
        public int ONE_mAMP_MIN { get; set; }
        public int TWENTY_mAMP_MAX_PI { get; set; }
        public int TWENTY_mAMP_MIN_PI { get; set; }
    }
    public class TolerancesOfPR69
    {
        public int FIVE_VOLT_MIN { get; set; }
        public int FIVE_VOLT_MAX { get; set; }
        public int TWELVE_mA_MIN { get; set; }
        public int TWELVE_mA_MAX { get; set; }
        public int One_VOLT_MAX { get; set; }
        public int One_VOLT_MIN { get; set; }
        public int TEN_VOLT_MAX { get; set; }
        public int TEN_VOLT_MIN { get; set; }
        public int FOUR_mAMP_MAX { get; set; }
        public int FOUR_mAMP_MIN { get; set; }
        public int TWENTY_mAMP_MAX { get; set; }
        public int TWENTY_mAMP_MIN { get; set; }
    }

    
}
