using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    /********************************************************************************************
              Class Name        : clsMessageIDs Class
              Purpose           : This class contains Messages Ids. These message Ids are used to show message.
              Date              : 1/06/2017
              Written By        : Shubham
              CopyRight         : General Industrial Controls Pvt. Ltd. Pune
              Modified          : Date                
              Released Version  :  NA
              Changed By        :  NA
              Decription Of Change:  NA
    ********************************************************************************************/
    public static class clsMessageIDs
    {
        public const int DEVICE_ID = 1;
        public const int Main_ERR_MSG = 0;
        public const int JIG_COM_PORT = 2;
        public const int Calibrator_COM_PORT = 3;
        public const int DUT_CALIB_FAILED = 4;
        public const int DUT_CALIB_COMPLETED = 5;
        public const int ERASING_DUT = 6;
        public const int DUT_ERASE_COMPLETED = 7;
        public const int DUT_ERASE_FAILED = 8;
        public const int ONEMV_DEBUG_MSG_ID = 9;
        public const int FIFTYMV_DEBUG_MSG_ID = 10;
        public const int THREEFIFTYOHM_DEBUG_MSG_ID = 11;
        public const int RELAY_DEBUG_MSG_ID = 12;
        public const int TWOWIRE_MSG_ID = 13;
        public const int THREEWIRE_MSG_ID = 14;
        public const int CURRENT_SETTING_MSG_ID = 15;
        public const int VOLTAGE_SETTING_MSG_ID = 16;
        public const int JIG_AND_CALIB_PORT_SAME = 17;
        public const int ANALOG_TEST_VALUE = 18;
        public const int ANALOG_TEST_CURRENT = 19;
        public const int ANALOG_TEST_VOLTAGE = 20;
        public const int THREEFIFTYOHM_CALIB_ERR = 21;
        public const int THREEFIFTYOHM_CALIB_SUCCESS = 22;
        public const int FIFTYMV_CALIB_ERR = 23;
        public const int FIFTYMV_CALIB_SUCCESS = 24;
        public const int ONEMV_CALIB_ERR = 25;
        public const int ONEMV_CALIB_SUCCESS = 26;
        public const int CALIBRATED_DUT = 27;
        public const int UNCALIBRATED_DUT = 28;
        public const int CALIBRATED_BUT_ACCURACY_ISNOTDONE = 29;
        public const int ACCURACY_COMPLETE = 30;
        public const int ACCURACY_FAILED = 31;
        public const int ONEVOLT_CALIB_ERR = 32;
        public const int ONEVOLT_CALIB_SUCCESS = 33;
        public const int NINEVOLT_CALIB_ERR = 34;
        public const int NINEVOLT_CALIB_SUCCESS = 35;
        public const int FOURMA_CALIB_ERR = 36;
        public const int FOURMA_CALIB_SUCCESS = 37;
        public const int TWENTYMA_CALIB_ERR = 38;
        public const int TWENTYMA_CALIB_SUCCESS = 39;
        public const int ERROR_INI_TOTAL_NO_OF_CATID = 40;
        public const int ERROR_INI_TOTAL_NO_OF_CATID_POSITIVE = 41;
        public const int OBSERVE_DISP_TEST = 42;
        public const int SET_1mV_IN_CALIB = 43;
        public const int SET_50mV_IN_CALIB = 44;
        public const int SET_350Ohm_IN_CALIB = 45;
        public const int SET_1V_IN_CALIB = 46;
        public const int SET_9V_IN_CALIB = 47;
        public const int SET_4mA_IN_CALIB = 48;
        public const int SET_20mA_IN_CALIB = 49;
        public const int CALIB_MEASURE_NOT_ON = 50;
        public const int DUT_ALREADY_CALIBRATED = 51;
        public const int MAX_DELAY_ERR = 52;
        public const int JIG_INITIALZATION_STARTED = 53;
        public const int JIG_INITIALZATION_BYPASSED = 54;
        public const int JIG_INITIALZATION_COMPLETED = 55;
        public const int JIG_INITIALZATION_FAILED = 56;
        public const int MA_CALIBRATION_MSG_ID = 57;
        public const int VOLT_CALIBRATION_MSG_ID = 58;
        public const int POWER_CYCLE_MSG_ID = 59;
        public const int FAIL_TO_CONNECT = 60;
        public const int DEVICE_CONNECTED = 61;
        public const int FAIL_TO_SET_BAUDRATE = 62;
        public const int BAUDRATE_SET_SUCCESSFUL = 63;
        public const int CODE_LOCK_NOT_MATCH = 64;
        public const int CODE_LOCK_MATCHED = 65;
        public const int FAILED_TO_ERASE = 66;
        public const int ERASED_SUCCESSFULLY = 67;
        public const int FAIL_TO_PROGRAM = 68;
        public const int PROGRAM_SUCCESSFUL = 69;
        public const int FAIL_ENDPROGRAM = 70;
        public const int ENDPROGRAMMING_SUCCESSFUL = 71;
        public const int PROGRAMMING = 72;
        public const int UNABLE_TO_CONNECT = 73;
        public const int CHECK_CONNECTIONS = 74;
        public const int PROGRAMMING_COMPLETE = 75;
        public const int ENTER_VALID_PASSWORD = 76;
        public const int NOT_VALID_PASSWORD = 77;
        public const int WRONG_DEVICE_SELECTION = 78;
        public const int PORT_NOT_PRESENT = 79;
        public const int SLAVE1_OK = 80;
        public const int SLAVE1_NOT_OK = 81;
        public const int SLAVE2_OK = 82;
        public const int SLAVE2_NOT_OK = 83;
        public const int SLAVE3_OK = 84;
        public const int SLAVE3_NOT_OK = 85;
        public const int SLAVE4_OK = 86;
        public const int SLAVE4_NOT_OK = 87;
        public const int CALIB_CONST_WRITE_SUCCESSFUL = 88;
        public const int CALIB_CONST_WRITE_UNSUCCESSFUL = 89;
        public const int ERR_IN_ACCURACY = 90;
        public const int ACCURACY_PT100_0 = 91;
        public const int ACCURACY_PT100_400 = 92;
        public const int ACCURACY_PT100_700 = 93;
        public const int ACCURACY_R_1750 = 94;
        public const int ACCURACY_R_1000 = 95;
        public const int ACCURACY_R_0 = 96;
        public const int ACCURACY_J_0 = 97;
        public const int ACCURACY_J_400 = 98;
        public const int ACCURACY_J_700 = 99;
        public const int ACCURACY_mA_4 = 100;
        public const int ACCURACY_mA_12 = 101;
        public const int ACCURACY_mA_20 = 102;
        public const int ACCURACY_VOLT_1 = 103;
        public const int ACCURACY_VOLT_5 = 104;
        public const int ACCURACY_VOLT_10 = 105;
        public const int TWO_WIRE_MSG_96x96 = 106;
        public const int ALL_WIRE_MSG_96x96 = 107;
        public const int ANALOG_NOT_FOUND_INI_ERR = 108;
        public const int DUTID_NOT_FOUND_INI_ERR = 109;
        public const int TYPE_NOT_FOUND_INI_ERR = 110;
        public const int DESCRIPTION_NOT_FOUND_INI_ERR = 111;
        public const int CATID_NOT_FOUND_INI_ERR = 112;
        public const int TYPEORRELAY_NOT_FOUND_INI_ERR = 113;
        public const int TEST_NOT_FOUND_INI_ERR = 114;
        public const int MAKE_SOURCE_OFF = 115;
        public const int INI_FILE_NOT_PRESENT = 116;
        public const int MOT_FILE_PATH_NOT_PRESENT = 117;
        public const int REF_CH_ERR = 118;
        public const int SIG_CH_ERR = 119;
        public const int CJC_CNT_ERR = 120;
        public const int MOT_FILE_PATH_NOT_AVAILABLE = 121;
        //-------Changed By Shubham
        //Date:- 24-02-2018
        //Version:- V16
        //Statement:- New Message Id added.
        public const int REMOVE_SOURCE_CONN = 122;
        public const int VREF_TOLERANCE_ERR = 123;
        public const int VREF_CALIB_ERR = 124;
        public const int PLC_COM_PORT = 125;
        public const int ONEMA_CALIB_ERR = 126;
        public const int ONEMA_CALIB_SUCCESS = 127;
        public const int SET_1mA_IN_CALIB = 129;
        public const int ACCURACY_mA_1 = 130;
        public const int ACCURACY_mA_12_PI = 131;
        public const int ACCURACY_mA_20_PI = 132;
        public const int ACCURACY_J_NEG_100 = 133;
        public const int ACCURACY_J_300 = 134;
        public const int ACCURACY_PT100_300 = 135;
        public const int TWOWIRE_MSG_ID_PI = 136;
        public const int ALL_WIRE_MSG_PI = 137;
        public const int ACCURACY_PT100_NEG_100 = 138;
        public const int CJC_Test = 139;
        public const int Test_24Volt_OUTPUT_TEST_ERROR = 140;
        public const int Test_24Volt_OUTPUT_TEST_MSG = 141;
        public const int ENSURE_1mV_IN_CALIB_PI = 142;
        public const int HEX_FILE_PATH_NOT_PRESENT = 143;
        public const int HEX_FILE_PATH_NOT_AVAILABLE = 144;
        public const int ABORT_BUTTON_CLICK = 145;
        public const int ACCURACY_PT100 = 146;
        public const int ACCURACY_R = 147;
        public const int ACCURACY_J = 148;
        public const int ACCURACY_mA = 149;
        public const int ACCURACY_VOLT = 150;
        public const int ACCURACY_POINT_NOT_IN_RANGE = 151;
    }
}
