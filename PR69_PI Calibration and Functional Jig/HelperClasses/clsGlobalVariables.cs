using System;
using System.Text;
using System.Collections;
using System.Globalization;
using PR69_PI_Calibration_and_Functional_Jig.Model;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    public class clsGlobalVariables
    {
        public static SelectedDeviceType selectedDeviceType;
              
        public enum SelectedDeviceType
        {
            PR69_48x48 = 1,
            PR69_96x96 = 2,
            PI = 3,
            PR69_43x43 = 4
        }
        #region"-----Enum------"
        //This enum is used for response error handling
        public enum enmResponseError
        {
            Success = 0,
            Function_Code_Mismatch_err,
            Address_Mismatch_err,
            Data_Limit_Exceed_err,
            Device_ID_Mismatch_err,
            PageNo_err,
            InvalidNoOfBytes_err,
            Invalid_Password_err,
            Incorrect_CRC_Err,
            Invalid_ModbusID_err,
            QueryLength_Exceed_Err,
            Invalid_SubFunctionCode_Err,
            Failed,
            Data_Receive_Failed,
            ERR_While_Sending_Data,
            Timeout_Err,
            Max_Retries_Reached_Err,
            Accuracy_Test_Not_Done,
            Invalid_data,
            ACCURACY_FAIL
        }
       public  enum enmMbQryPos
        {
            MB_ID_POS,
            MB_FUNCTION_POS,
            MB_START_ADD_HIGH_POS,
            MB_START_ADD_LOW_POS,
            MB_NO_OF_POINTS_HIGH_POS,
            MB_NO_OF_POINTS_LOW_POS,
            MB_NO_OF_DATA_BYTES_POS,
            MB_DATA_BYTES_POS
        }
      
        //Read Input
        public enum enmMbRespPos
        {
            MB_ID_POS,
            MB_FUNCTION_POS,
            MB_BYTE_COUNT,
            MB_RESP_DATA_POS
        }
        public enum enmMbUpFrmRespPos
        {
            MB_ID_POS,
            MB_FUNCTION_POS,
            MB_DEVICE_ID,
            MB_SUB_FUNC_POS,
            MB_NO_OF_BYTES_POS,
            MB_ERROR_POS,
            MB_RESP_DATA_POS
        }
        public enum enmMbUpFrmQryPos
        {
            MB_ID_POS,
            MB_FUNCTION_POS,
            MB_DEVICE_ID,
            MB_SUB_FUNC_POS,
            MB_NO_OF_BYTES_POS,
            MB_PAGE_NO,
            MB_OFFSET_ADDR_HIGH_POS,
            MB_OFFSET_ADDR_LOW_POS,
            MB_DATA_BYTES_POS,
        }
        public enum enmMbQueryPos
        {
            MB_ID_POS,
            MB_FUNCTION_POS,
            MB_START_ADD_HIGH_POS,
            MB_START_ADD_LOW_POS,
            MB_NO_OF_POINTS_HIGH_POS,
            MB_NO_OF_POINTS_LOW_POS,
            MB_NO_OF_DATA_BYTES_POS,
            MB_DATA_BYTES_POS
        }

        //This enum is used to add data in the Tx buffer. 
        public enum enmQueryPosition
        {
            MB_ID_POS = 0, MB_FUNCTION_POS, MB_DATA_POS
        }
        //This enum is used for status verification.
        public enum enmStatus
        {
            PASS = 1, FAIL = 2, INPROGRESS = 3,DEFAULT =4
        }
        #endregion

        public enum MsgIcon
        {
            Message = 1,
            Error = 2,
            Question = 3
        }

        public static int FAILURE = -1;
        public static int SUCCESS = 0;

        public static CatIdList Selectedcatid = new CatIdList();
        public static string SelectedDeviceNameOfTreeView = "";
        public static string configJsonfilepath = "";

        #region"-----Variables and constants------"
        public static string WorkingDirectory = @"C:\Program Files (x86)\Nuvoton Tools\NuLink Command Tool";
        public static bool StopButtonFlag = false;
        public static string strg_Application = "PR69 JIG Software";
        public static byte[] btgTxBuffer = new byte[10];                             //This array is used to send the data.
        public static byte[] btgRxBuffer = new byte[10];                             //This array is used to Receive the data.
        public const byte MAX_NO_RETRIES = 1;                  //Value of maximum retries if device does not sent valid response.
        //btgPassData = 1 means passowrd has to be entered for adding INI file.
        //btgPassData = 1 means passowrd has to be entered for debug.
        //btgPassData = 1 means passowrd has to be entered for Data Logging.
        public static byte btgPassData = 1;
        //public static string strgConfigFilePath = @"\\nas\Release to Production MOT'S HEX & Software\Digicon Release Production\Temperature Controller\PR-69\Software\Config.ini";
        public static string strgConfigFilePath = @"E:\Config.ini";
        //public static string strgMotFileFolderPath_48x48 = Application.StartupPath;
        //public static string strgMotFileFolderPath_96x96 = Application.StartupPath;
       // public static clsConfigurations objconfig = new clsConfigurations();
        public static ArrayList algTests_Auto = new ArrayList();
        public static ArrayList algTests_Manual = new ArrayList();
        public static ArrayList algTests_JIGs = new ArrayList();       
        public static clsGlobalFunctions objGlobalFunction = new clsGlobalFunctions();
        //CA55 public static frmTestJigSetup objTestSetupfrm = new frmTestJigSetup();
        public static clsQueries objQueriescls = new clsQueries();
        public static clsCalibratorQueries objCalibQueriescls = new clsCalibratorQueries();
        public static clsPLCQueries objPLCQueriescls = new clsPLCQueries();
        public static clsProgrammingJIGQuery objProgramingQrycls = new clsProgrammingJIGQuery();  
        public static CultureInfo objCultureinfo = CultureInfo.CreateSpecificCulture("en-US");   
        public static clsTestJIGFunctions objTestJIGFunctions = new clsTestJIGFunctions();
        public static clsDataLog objDataLog = new clsDataLog();
        //CA55public static Forms.frmProgramming objfrmProgramming = new PR69_Function_and_Calibration_JIG.Forms.frmProgramming();
        //CA55public static PR69_Function_and_Calibration_JIG.Forms.frmPictureMessage objPictMsg = new PR69_Function_and_Calibration_JIG.Forms.frmPictureMessage();
        //-------Changed By Shubham
        //Date:- 28-04-2018
        //Version:- V17
        //Statement:- This variable will store the name of ongoing test.
        public static string strgOngoingTestName = "";
        //--------Changes End.
        public static int igTotalNoOfTests_Auto = 0;
        public static int igTotalNoOfTests_Manual = 0; 
        public static string strgComPortJIG = "";
        public static string strgComPortCalibratorDUT1 = "";
        public static string strgComPortCalibratorDUT2 = "";
        public static string strgComPortCalibratorDUT3 = "";
        public static string strgComPortCalibratorDUT4 = "";
        public static string strgComPortCalibratorDUT5 = "";
        public static string strgComPortCalibratorDUT6 = "";
        public static string strgComPortPLC = "";
        public static string strgCJCsupport = "";
        public static string CLIBRATOR_SR1 = "HKX1SA006";
        public static string CLIBRATOR_SR2 = "HKX1SA004";
        public static string CLIBRATOR_SR3 = "HKX1SA005";
        public static string CLIBRATOR_SR4 = "HKX1SA003";
        public static string CLIBRATOR_SR5 = "HKX1SA007";
        public static string CLIBRATOR_SR6 = "HKX1SA008";
        public static int NUMBER_OF_DUTS = 0;
        public static int OLD_NUMBER_OF_DUTS = 0;
        public static int igTYPE_OF_DEVICE = 1;
        public static int ig_Query_TimeOut = 16000;
        public static int ig_Query_TimeOut_PI = 100;
        public static int ig_Query_PLC_TimeOut_PI = 100;
        public const int BASE_ADDR = 0x0;
        public const int COIL_ON = 0xFF00;
        public const int COIL_OFF = 0x00;
        public const int MB_FUNC_FORCE_SINGLECOIL = 0x5;
        //Modbus Query
        public const Byte MB_FUNC_READ_COILSTATUS = 0x1;
        //Read Input
        public const Byte MB_FUNC_READ_INPUTSTATUS = 0x2;
        public static int ig_Calib_Query_TimeOut = 5000;
        public static int CALIB_MEASURE_DELAY = 12000;
        public static int igDelay_In_Two_Calib_Queries = 1000;
        public static int igTooltip_Time_Duration = 2000;
        public static int igPV_TIMEOUT_DELAY = 120000;
        public static int igPV_DISPLAY_DELAY = 1200;
        public static int igPV_PLC_DELAY = 1000;
        public static ArrayList algAvailableComPorts = new ArrayList();
        public static short shrtgPV;
        public static short shrtgCJC;
        public static int CJC_min_Value = 15;
        public static int CJC_max_Value = 45;
        public static int PLC_ON_TIME_DELAY = 8000;
        public static bool PR_69_3_DECIMAL_POINT = false;


        public static int MIN_mA_RANGE = 4;
        public static int MAX_mA_RANGE = 20;
        public static int MIN_J_RANGE = 0;
        public static int MAX_J_RANGE = 700;
        public static int MIN_PT100_RANGE = -200;
        public static int MAX_PT100_RANGE = 700;
        public static int MIN_R_RANGE = 0;
        public static int MAX_R_RANGE = 1750;
        public static int MIN_Volt_RANGE = 0;
        public static int MAX_Volt_RANGE = 10;

        public static int MIN_mA_RANGE_PI = 0;
        public static int MAX_mA_RANGE_PI = 20;
        public static int MIN_J_RANGE_PI = -200;
        public static int MAX_J_RANGE_PI = 750;
        public static int MIN_PT100_RANGE_PI = -200;
        public static int MAX_PT100_RANGE_PI = 850;
        public static int MIN_R_RANGE_PI = 0;
        public static int MAX_R_RANGE_PI = 1750;
        public static int MIN_Volt_RANGE_PI = 0;
        public static int MAX_Volt_RANGE_PI = 10;


        public static string shp4mA = "";
        public static string shp12mA = "";
        public static string shp20mA = "";
        public static string shp1Volt= "";
        public static string shp5Volt= "";
        public static string shp10Volt= "";
        public static string shpPT100Zero= "";
        public static string shpPT100FourHundred = "";
        public static string shpPT100SevenHundred = "";
        public static string shpR1750= "";
        public static string shpR1000= "";
        public static string shpR0= "";
        public static string shpJ0= "";
        public static string shpJ400= "";
        public static string shpJ700= "";


        public static string shp4mAPI = "";
        public static string shp12mAPI = "";
        public static string shp20mAPI = "";
        public static string shp1VoltPI = "";
        public static string shp5VoltPI = "";
        public static string shp10VoltPI = "";
        public static string shpPT100ZeroPI = "";
        public static string shpPT100FourHundredPI = "";
        public static string shpPT100SevenHundredPI = "";
        public static string shpR1750PI = "";
        public static string shpR1000PI = "";
        public static string shpR0PI = "";
        public static string shpJ0PI = "";
        public static string shpJ400PI = "";
        public static string shpJ700PI = "";


        public static Single mA = 0.5F;
        public static Single Volt = 0.5F;
        public static Single PT100Zero = 20;
        public static Single R1750 = 10;
        public static Single J0 = 10;
        public static bool IsAccuracyFailed = false;
        public static bool MessageShown = false;
        public static Boolean blngUserEnteredValue = false;
        public static Boolean blngApplyDelayOver = false;
       // public static Boolean blngIsDebugPresent = false;
        public static Boolean blngIsJIGInitializationComplete = false;
        public static Boolean blngIsOPOneON = false;
        public static Boolean blngIsOPTwoON = false;
        public static Boolean blngIsOPThreeON = false;
        public static Boolean _StopFlag = false;
        public const int igFIXED_DELAY_IN_RELAYTEST = 1000;
        public const int igFIXED_DELAY_IN_RELAYTEST_PLC = 2000;
        public const int igSingleActingType = 1;
        public const int igDoubleActingType = 2;
        public const int igDoubleActingWOModbusType = 3;
        public const int igSingleActingWithAnalogIPType = 4;
        public const int igDoubleActingWithAnalogIPType = 5;
        public const int igDoubleActingWithAnalogIPWOModbusType = 6;
        public const int igRetries_Pass_RelayTest = 3;
        public const int igRetries_Fail_RelayTest = 5;
        public const int igMax_Retries_PassFail_RelayTest = 10;
        public const int DEVICE_CONFIG = 1;
        public const int ANALOG_CONFIG = 2;        
        public const int KEYPAD_CONFIG = 3;
        public static bool blngIsComportDetected = false;
        public static bool blngIsComportDetectedForPLC = false;
        public const int PC_MODBUS_ID = 0xFA;
        public const int MB_FUNC_UPDATEFIRMWARE = 0x64;
        public const int MB_SUBFUNC_GET_MODE_STATUS = 0xE;
        public static Byte DEVICE_MODE_STATUS;
        public const short DEVICE_RUN_MODE = 4;
        public const int igTimeOutMedium = 1000;
        public const int igCONST_FOR_ASCII = 0x30;
        public const byte btgCheckSumDefaultConst = 5;
        public const int CODE_START_ADDR = 0x2400;

        public static int[] arrigKeysValue = new int[4];
        public static string[] arrstrgKeysNames = new string[4];
        public const byte NUM_OF_KEYS = 4;
        public const string strgEnter = "ENTER";
        public const string strgUP = "UP";
        public const string strgDOWN = "DOWN";
        public const string strgESC = "ESC";

        public const int igESCVal = 0x10;        
        public const int igUPKeyVal = 0x40;
        public const int igDOWNVal = 0x20;
        public const int igEnterKeyVal = 0x80;

        public const int igESCVal_PI = 0x80;
        public const int igUPKeyVal_PI = 0x20;
        public const int igDOWNVal_PI = 0x40;
        public const int igEnterKeyVal_PI = 0x10;

        public const byte BEFORE_SOAKING = 0;
        public const byte AFTER_SOAKING = 1;
        public const byte CALIB_DONE = 2;

        public const byte MB_DUT_ID = 0x1;        
        public const byte MB_SLAVE1_ID = 0xA;
        public const byte MB_SLAVE2_ID = 0xB;
        public const byte MB_SLAVE3_ID = 0xC;
        public const byte MB_CONVERTOR_ID = 0xD;

        public const byte CALIB_STAGE = 0x1;
        public const byte SWITCH_SENSOR = 0x2;
        public const byte DEFAULT_4_MA_CNT = 0x3;
        public const byte DEFAULT_1_MA_CNT = 0x3;
        public const byte DEFAULT_12_MA_CNT = 0x4;
        public const byte DEFAULT_20_MA_CNT = 0x5;
        public const byte DP_VAL = 0x09;
        public const byte ISCL = 0x0A; 
        public const byte ISCH = 0x0B;        

        public const byte START_TEST_FUNC_CODE = 0x0;
        public const byte SWITCH_ON_FUNC_CODE = 0x1;
        public const byte SWITCH_OFF_FUNC_CODE = 0x2;
        public const byte READ_FUNC_CODE = 0x3;
        public const byte CALIBRATE_FUNC_CODE = 0x4;
        public const byte SET_I_FUNC_CODE = 0x5;
        public const byte SET_WRITE_FUNC_CODE = 0x6;
        public const byte SET_V_FUNC_CODE = 0x7;
        public const byte SET_OBSERVED_4_MA = 0x8;
        public const byte SET_OBSERVED_1_MA = 0x8;
        public const byte SET_OBSERVED_20_MA = 0x9;
        public const byte SET_ISCL = 17;
        public const byte SET_ISCH = 18;
        public const byte SET_AIRL = 19;
        public const byte SET_AIRH = 20;
        public const byte CJC_ON_OFF = 21;
        public const byte CJC_OFF = 1;
        public const byte CJC_ON = 0;
        public static bool StopButtonFlagPIProgramming = false;
        public static bool HeaderNeedToWriteForDataLog = false;
        //-------Changed By Shubham
        //Date:- 24-02-2018
        //Version:- V16
        //These below are the function codes used to read VREF.  
        public const byte CALC_VREF = 0x0B;
        public const byte READ_VREF_LSB = 0x03;
        public const byte READ_VREF_MSB = 0x04;
        //--------Changes End.        

        public const byte MB_READ_HOLDIND_REG = 3;
        public const byte MB_WRITE_HOLDIND_REG = 6;

        public const byte MB_MASTER_TO_DUT = 100;
        public const byte MB_READ_CALIB_CONST_STATUS = 101;
        public const byte MB_READ_ADC_COUNT = 102;
        public const byte MB_START_TEST = 103;
        public const byte MB_CALIBRATE = 104;
        public const byte MB_WRITE_CALIB_CONST = 105;
        public const byte MB_SET_CURRENT_DFALT_COUNT = 106;
        public const byte MB_SET_VOLTAGE_DFALT_COUNT = 107;
        public const byte MB_SET_CURRENT_OBSERVED_COUNT = 108;
        public const byte MB_SET_VOLTAGE_OBSERVED_COUNT = 109;
        public const byte MB_SET_CURRENT_ANLOP = 110;
        public const byte MB_SET_VOLTAGE_ANLOP = 111;
        public const byte MB_SWITCH_SENSOR = 112;
        public const byte MB_ERASE = 114;
        public const byte MB_ADJUST_MODE = 115;
        //-------Changed By Shubham
        //Date:- 24-02-2018
        //Version:- V16
        //These below are the function codes used to read VREF for devices having modbus.
        public const byte MB_SEND_1TO10V_SLOPE_OFSET = 116;
        public const byte MB_READ_REF_VTG = 117;
        //--------Changes end.

        public const int RLY_ON_ADC_LOW_LMT_COUNT = 150;
        public const int RLY_ON_ADC_HIGH_LMT_COUNT = 300;        
        public const int RLY_OFF_ADC_LOW_LMT_COUNT = 0;
        public const int RLY_OFF_ADC_HIGH_LMT_COUNT = 50;

        public const byte RLY_ON = 1;
        public const byte RLY_OFF = 0;

        public const byte CHK_RELAY = 0x1;
        public const byte CHK_DISP = 0x2;
        public const byte CHK_KEYPAD = 0x3;
        public const byte CHK_ADC_CNT = 0x4;
        public const byte CHK_LEAKY_MOSFET = 0x5;

        public const byte READ_Sensor_Func_CODE = 0x10;
        public const byte READ_PV_Value_Func_CODE = 0xF;

        public const byte CAT_NO = 0x1;
        public const byte CJC_NO = 0x2;
        public const byte CALIB_STATUS = 0x2;
        public const byte CALIB_CONST = 0x3;

        public const int READ_Sensor = 0x2008;
        public const int READ_PV = 0x1000;

        public const byte OP_OFF = 0x7;
        public const byte OP1_ON = 0x0;
        public const byte OP2_ON = 0x1;
        public const byte OP3_ON = 0x3;
        public const byte OP1 = 0x1;
        public const byte OP2 = 0x2;
        public const byte OP3 = 0x3;

        public const int OP1_ADDRESS = 0x2017;
        public const int OP2_ADDRESS = 0x2018;
        public const int OP3_ADDRESS = 0x2019;
        public const int MVER_ADDRESS = 0x1005;
        public const int ANLOP_ADDRESS = 0x1007;

        public const int SP1_VALUE = 0x2004;
        public const int SENS_SET = 0x2008;
        public const int OUTPUT_CONF = 0x2015;
        public const int ALM1_TYPE = 0x201C;
        public const int ALM1_THRESHOLD = 0x201F;
        public const int ALM2_TYPE = 0x2026;
        public const int ALM2_THRESHOLD = 0x2029;
        public const int CONT_TYPE = 0x2030;
        public const int FUNC_TYPE = 0x2031;
        public const int ISCL_SET = 0x2009;
        public const int ISCH_SET = 0x200A;        

        public const int PV_VALUE = 0x1000;
        public const int DP_SET = 0x200E;

        public const byte DP_VAL_TWO = 2;
        public const byte DP_VAL_ZERO = 0;
        public const int SET_SP_VAL = 50;
        public const int SET_SENS_VAL = 0;
        public const int OP_CONF_VAL = 0;
        public const int SET_ALM_TYPE_VAL = 0;
        public const int SET_ALM_THRESH_VAL = 1000;
        public const int SET_CONT_VAL = 1;
        public const int SET_FUNC_VAL = 0;

        public const byte TEST_REL = 0x1;

        public const byte SENSOR_J_TYPE = 0x1;
        public const byte SENSOR_60_MV_TYPE = 0x2;
        public const byte SENSOR_PT100_TYPE = 0x3;
        public const byte SENSOR_R_TYPE = 0x4;
        public const byte SENSOR_0_10V_TYPE =0xC;	
	    public const byte SENSOR_4_20mA_TYPE=0xB;		
	    public const byte SENSOR_0_20mA_TYPE=0xB;		

        public const byte SENSOR_PT100_TYPE_DOUBLE_ACTING = 0x5;
        public const byte SENSOR_R_TYPE_DOUBLE_ACTING = 0x9;
        public const byte SENSOR_J_TYPE_DOUBLE_ACTING = 0x0;
        public const byte SENSOR_4_20mA_TYPE_DOUBLE_ACTING = 0x0A;
        public const byte SENSOR_0_20mA_TYPE_DOUBLE_ACTING = 0x0A;
        public const byte SENSOR_0_10V_TYPE_DOUBLE_ACTING = 0x0B;
        //The below variables are used for calibrator related queries and messages.
        public const byte CR = 0x0D;
        public const byte LF = 0x0A;
        public const byte Question_MARK = 0x3F;
        public const byte ONE_Val = 0x31;
        public const byte ZERO_Val = 0x30;
        public const byte PLUS = 0x2B;
        public const byte NEG = 0x2D;
        //public const byte MEASURE_mA_KNOB_POS = 7;
        public const byte MEASURE_mA_KNOB_POS = 1; //CA550
        public const string MEASURE_mA_KNOB_TEXT = "mA";
        public const byte MEASURE_10VOLT_KNOB_POS = 0;//CA550
        // public const byte MEASURE_10VOLT_KNOB_POS = 2;
        public const string MEASURE_10VOLT_KNOB_TEXT = "10Volt";
        public const byte SOURCE_ON = 1;
        public const byte SOURCE_OFF = 0;
        public const byte MEASURE_ON = 1;
        public const byte MEASURE_OFF = 0;
        //public const byte SOURCE_mV_KNOB_POS = 3;  
        public const byte SOURCE_mV_KNOB_POS = 0; //CA550
        public const string SOURCE_mV_KNOB_TEXT = "100mV";
        public const byte SOURCE_RTD_KNOB_POS = 2; //CA550
        //public const byte SOURCE_RTD_KNOB_POS = 4; 
        public const string SOURCE_RTD_KNOB_TEXT = "RTD";
        public const byte SOURCE_VOLT_KNOB_POS = 0; //CA550
        //public const byte SOURCE_VOLT_KNOB_POS = 1;
        public const string SOURCE_VOLT_KNOB_TEXT = "10 VOLT";
        //public const byte SOURCE_mA_KNOB_POS = 6;
        public const byte SOURCE_mA_KNOB_POS = 1;//CA550
        public const string SOURCE_mA_KNOB_TEXT = "20mA";
        //These valuse will be set into calibrator.
        public const string ZERO = "0";
        public const string FOUR_HUNDRED = "400";
        public const string THREE_HUNDRED = "300";
        public const string SEVEN_HUNDRED = "700";
        public const string THOUSAND = "1000";
        public const string THOUSAND_SEVEN_FIFTY = "1750";
        public const string NEG_ONE_HUNDRED = "100";

        //public const byte PT100_SENSOR = 0x31;
        //public const byte R_SENSOR = 0x35;
        //public const byte J_SENSOR = 0x33;
        //public const byte mV_SENSOR = 0x30;
        //CA550
        public const byte PT100_SENSOR = 0x30;
        public const byte R_SENSOR = 0x34;
        public const byte J_SENSOR = 0x32;
        public const byte mV_SENSOR = 0x30;
        public const byte RTD_SENSOR = 0x30;
        public const byte VTG_SENSOR = 0x32;
        public const byte mA_SENSOR = 0x30;
        //These valuse will be set into calibrator.
        public const string strgONE_MV = "1";
        public const string strgFIFTY_MV = "50";
        public const string strgTHREEFIFTY_OHM = "350";

        public const byte CURRENT = 0x5;
        public const byte VOLTAGE = 0x6;

        public const byte MA_4 = 0x1;
        public const byte MA_1 = 0x1;
        public const byte MA_12 = 0x2;
        public const byte MA_20 = 0x3;

        public const byte VOLT_1 = 0x1;
        public const byte VOLT_5 = 0x2;
        public const byte VOLT_10 = 0x3;

        public const byte DEFAULT_1_VOLT_CNT = 0x6;
        public const byte DEFAULT_5_VOLT_CNT = 0x7;
        public const byte DEFAULT_10_VOLT_CNT = 0x8;

        public static string strgAnalogData;

        public static int FIVE_VOLT_MIN = 492;
        public static int FIVE_VOLT_MAX = 507;
        public static int FIVE_VOLT_MIN_PI = 4920;
        public static int FIVE_VOLT_MAX_PI = 5080;
        public static int TWELVE_mA_MIN = 11920;
        public static int TWELVE_mA_MAX = 12080;
        public static int TWELVE_mA_MIN_PI = 1195;
        public static int TWELVE_mA_MAX_PI = 1205;
        public static int One_VOLT_MAX = 1090;
        public static int One_VOLT_MIN = 910;
        public static int One_VOLT_MAX_PI = 103;
        public static int One_VOLT_MIN_PI = 96;
        public static int TEN_VOLT_MAX = 10080;
        public static int TEN_VOLT_MIN = 9920;
        public static int TEN_VOLT_MAX_PI = 1047;
        public static int TEN_VOLT_MIN_PI = 975;
        public static int FOUR_mAMP_MAX = 4100;
        public static int FOUR_mAMP_MIN = 3900;
        public static int ONE_mAMP_MAX_PI = 1010;//0.9538               1.0129
        public static int ONE_mAMP_MIN_PI = 950;
        public static int TWENTY_mAMP_MAX = 20080;
        public static int TWENTY_mAMP_MIN = 19920;
        public static int TWENTY_mAMP_MAX_PI = 2074;
        public static int TWENTY_mAMP_MIN_PI = 1952;

        public const string ONE_VOLT_INPUT_CAL = "1";
        public const string NINE_VOLT_INPUT_CAL = "9";
        public const string FOUR_mA_INPUT_CAL = "4";
        public const string ONE_mA_INPUT_CAL = "1";
        public const string TWENTY_mA_INPUT_CAL = "20";

        public const byte One_Volt = 1;
        public const byte FIVE_Volt = 5;
        public const byte TEN_Volt = 10;
        public const int TEN_Volt_PI = 1000;
        public const byte TWELVE_mA = 12;
        public const byte FOUR_mAMP = 4;
        
        public const byte ZERO_mAMP = 0;
        public const byte ONE_mAMP_case = 21;
        public const byte TWELVE_mAMP = 12;
        public const byte TWENTY_mAMP = 20;
        public const int TWENTY_mAMP_PI = 2000;
        public const byte ZERO_VOLT = 0;

        public const byte SET_OBSERVED_1_VOLT = 0xA;
        public const byte SET_OBSERVED_10_VOLT = 0xB;
      
        public static long[,] lngvCount = new long[4,8];
    
        public static string[] strgarrCalibConst = new string [9]; 
        //
        //Below variables are used to save slopes and offsets for double acting devices.
        public static float fltgvPrConfigSlope;
        public static float fltgvPrConfigOffset;
        public static float fltgvPrConfigRtdCurrent;
        public static float fltgvPrConfigCjc;
        public static float fltgv4to20mAConfigSlope;
        public static float fltgv4to20mAConfigOffset;
        public static float fltgv1to9VConfigSlope;
        public static float fltgv1to9VConfigOffset;
        
        public static float fltgREF_Vtg = 0;
        //tolerance of the VREF is saved in below variables.
        public static float fltgREF_Vtg_MIN = 2.44f;
        public static float fltgREF_Vtg_MAX = 2.54f;
        //---------Changes End.

        public const byte REF1_CNT = 0;
        public const byte SIGNAL_CNT = 1;
        public const byte LD_CJC_CNT = 2;
        public const byte REF2_CNT = 3;

        public const byte ADJUST_MODE = 0xE;
        public const byte START_MODE = 0x1;
        public const byte RUN_MODE = 0x2;
        // These are delays used during device calibration.
        public static int PT100_DELAY_AFTER_STARTMODE = 30000;
        public static int PT100_DELAY_AFTER_RUNMODE = 60000;
        public static int ONEmV_DELAY_AFTER_STARTMODE = 30000;
        public static int ONEmV_DELAY_AFTER_RUNMODE = 30000;
        public static int FOURmA_DELAY_AFTER_STARTMODE = 30000;
        public static int FOURmA_DELAY_AFTER_RUNMODE = 30000;
        public static int ONEVolt_DELAY_AFTER_STARTMODE = 30000;
        public static int ONEVolt_DELAY_AFTER_RUNMODE = 30000;
        //-------Changed By Shubham

        public static int mA_V_AccuracyDelay = 10000;
        public static int PT100_AccuracyDelay = 30000;
        public static int ThermoCouple_AccuracyDelay = 120000;


        //Date:- 24-02-2018
        //Version:- V16
        //Delays used while reading the VREF values are saved in these variables.
        public static int VREF_READ_DELAY_STARTMODE = 30000;
        public static int VREF_READ_DELAY_RUNMODE = 30000;
        //--------Changes End.

        //For Calibration the below constants are used.
        public const byte MV_1_CNT = 0x0;
        public const byte MV_50_CNT = 0x1;
        public const byte PT100_CNT = 0x2;
        public const byte TC_CNT = 0x3;
        public const byte CALIB_1V = 0x7;	
        public const byte CALIB_9V = 0x8;	
        public const byte CALIB_4mA = 0x9;	
        public const byte CALIB_20mA = 0xA;
        
        public const byte CALIB_VREF = 0xB;
        //-------Changes End.

        public const byte FOURMA_Index =0x05;
        public const byte TWENTYMA_Index = 0x06;
        public const byte ONEVOLT_Index = 0x03;
        public const byte NINEVOLT_Index = 0x04;
       
        public const byte VREF_Index = 0x07;
        //--------Changes End.

        public const byte CJC = 0x0;
        public const byte RTD = 0x1;
        public const byte SLOPE = 0x2;
        public const byte OFFSET = 0x3;
        public const byte SLOPE_420mA = 0x4;
        public const byte OFFSET_420mA = 0x5;
        public const byte SLOPE_19V = 0x6;
        public const byte OFFSET_19V = 0x7;
       
        public const byte VREF_VALUE = 0x8;
        //------Changes End.

        public const long CONST_CALIB_4mA = 4000;
        public const long CONST_CALIB_20mA = 20000;
        public const long CONST_CALIB_1V = 1000;
        public const long CONST_CALIB_9V = 9000;

        //These below tolerances will be used to validate counts received for 1mV for double acting devices.
        //These tolerances are taken from Firmware
        public const long REF1_CNT_MIN_1mV = 27000;
        public const long REF1_CNT_MAX_1mV = 57000;
        public const long SIGNAL_CNT_MIN_1mV = 28000;
        public const long SIGNAL_CNT_MAX_1mV = 58000;
        public const long REF2_CNT_MIN_1mV = 126000;
        public const long REF2_CNT_MAX_1mV = 174000;
        public const long LD_CJC_CNT_MIN_1mV = 65000;
        public const long LD_CJC_CNT_MAX_1mV = 135000;
        //These below tolerances will be used to validate counts received for 50mV for double acting devices.
        //These tolerances are taken from Firmware
        public const long REF1_CNT_MIN_50mV = 27000;
        public const long REF1_CNT_MAX_50mV = 57000;
        public const long SIGNAL_CNT_MIN_50mV = 180000;
        public const long SIGNAL_CNT_MAX_50mV = 210000;
        public const long REF2_CNT_MIN_50mV = 126000;
        public const long REF2_CNT_MAX_50mV = 174000;
        public const long LD_CJC_CNT_MIN_50mV = 65000;
        public const long LD_CJC_CNT_MAX_50mV = 135000;
        //These below tolerances will be used to validate counts received for 350Ohm for double acting devices.
        //These tolerances are taken from Firmware
        public const long REF1_CNT_MIN_350Ohm = 27000;
        public const long REF1_CNT_MAX_350Ohm = 57000;
        public const long SIGNAL_CNT_MIN_350Ohm = 165000;
        public const long SIGNAL_CNT_MAX_350Ohm = 215000;
        public const long REF2_CNT_MIN_350Ohm = 126000;
        public const long REF2_CNT_MAX_350Ohm = 174000;
        public const long LD_CJC_CNT_MIN_350Ohm = 165000;
        public const long LD_CJC_CNT_MAX_350Ohm = 215000;
        //These below tolerances will be used to validate counts received for 4mA for double acting devices.
        //These tolerances are taken from Firmware
        public const long REF1_CNT_MIN_4mA = 27000;
        public const long REF1_CNT_MAX_4mA = 57000;
        public const long SIGNAL_CNT_MIN_4mA = 166000;	 
        public const long SIGNAL_CNT_MAX_4mA = 205000;   
        public const long REF2_CNT_MIN_4mA = 126000;
        public const long REF2_CNT_MAX_4mA = 174000;
        //These below tolerances will be used to validate counts received for 20mA for double acting devices.
        //These tolerances are taken from Firmware
        public const long REF1_CNT_MIN_20mA = 27000;
        public const long REF1_CNT_MAX_20mA = 57000;
        public const long SIGNAL_CNT_MIN_20mA = 36000;	 
        public const long SIGNAL_CNT_MAX_20mA = 68000;   
        public const long REF2_CNT_MIN_20mA = 126000;
        public const long REF2_CNT_MAX_20mA = 174000;
        //These below tolerances will be used to validate counts received for 1V for double acting devices.
        public const long REF1_CNT_MIN_1V = 27000;
        public const long REF1_CNT_MAX_1V = 57000;
        public const long SIGNAL_CNT_MIN_1V = 170000;	 
        public const long SIGNAL_CNT_MAX_1V = 210000;   
        public const long REF2_CNT_MIN_1V = 126000;
        public const long REF2_CNT_MAX_1V = 174000;
        //These below tolerances will be used to validate counts received for 9V for double acting devices.
        public const long REF1_CNT_MIN_9V = 27000;
        public const long REF1_CNT_MAX_9V = 57000;
        public const long SIGNAL_CNT_MIN_9V = 47000;	
        public const long SIGNAL_CNT_MAX_9V = 78000;
        public const long REF2_CNT_MIN_9V = 126000;
        public const long REF2_CNT_MAX_9V = 174000;
        
        public const long REF1_CNT_MIN_VREF = 27000;
        public const long REF1_CNT_MAX_VREF = 57000;
        public const long SIGNAL_CNT_MIN_VREF = 150000;
        public const long SIGNAL_CNT_MAX_VREF = 184000;
        public const long REF2_CNT_MIN_VREF = 126000;
        public const long REF2_CNT_MAX_VREF = 174000;
        //-------Changes End.
        
        public const byte GET_COUNTS = 0x1;
        public const byte WRITE_CONST = 0x2;

        public const string ONE_MV = "1";
        public const string FIFTY_MV = "50";
        public const string THREEFIFTY_OHM = "350";

        public const byte CONST_CALIB_V1 = 1;
        public const byte CONST_CALIB_V2 = 50;
        public const int igvProcessValue = 0; 

         //These below constants are test sequence used during accuracy.
         public const int PT100_ZERO_DEGREE = 1;
         public const int PT100_400_DEGREE = 2;
         public const int PT100_700_DEGREE = 3;
         public const int R_ZERO_DEGREE = 6;
         public const int R_1000_DEGREE = 5;
         public const int R_1750_DEGREE = 4;
         public const int J_ZERO_DEGREE = 7;
         public const int J_400_DEGREE = 8;
         public const int J_700_DEGREE = 9;            
         public const int mA_4_Test = 10;
         public const int mA_12_Test = 11;
         public const int mA_20_Test = 12;
         public const int Volt_1_Test = 13;
         public const int Volt_5_Test = 14;
         public const int Volt_10_Test = 15;
         public const int mA_1_Test = 16;
         //The below value is received in wndProc method when cable is connected or disconnected. 
         public const int WM_DEVICE_CHANGE = 0x219;
         public const int MAX_DELAY_VAL = 300000;
         //These are used in the programming.
         public const int PAGE_SIZE = 256;
         public const byte READ_PAGE = 0xFF;
         //These below are the function codes used for programming.
         public const byte FORCE_TO_PROGRAM = 0xB0;
         public const byte BAUD_RATE_38400 = 0xB2;
         public const byte PAGE_PROGRAM = 0x41;
         public const byte BLOCK_ERASE = 0x20;
         public const byte ERASE_UNLOCKED_BLOCKS = 0xA7;
         public const byte ERASE_CONFIRM_CMD = 0xD0;
         public const byte END_CMD = 0xA0;
         public const byte READ_STATUS = 0x70;
         public const byte CLEAR_STATUS = 0x50;
         public const byte BLANK_CHECK = 0xF7;
         public const byte VERIFY_CHECK = 0xF7;
         public const byte ID_CHECK = 0xF5;

         //These below variables are used for data logging in WriteLogFile().
         public const byte Case_WithAnalogOP_WithoutAnalogIP = 0;
         public const byte Case_WithoutAnalogOP_WithoutAnalogIP = 1;
         public const byte Case_WithAnalogOP_WithAnalogIP = 2;
         public const byte Case_WithoutAnalogOP_WithAnalogIP = 3;
        #endregion

        public static Int32 PLC_ZIG_COMM_DELAY = 100;
        public static Int32 PLC_ZIG_MODBUS_DELAY = 100;
    }
    public static class clsModelSettings
    {
        public static int igDutID;
        public static Boolean blnRS485Flag; //This tells that selected cat id contains modbus or not.
        public static int imAnalOpVal;
        public static byte btmAnalogsetVal;
        public static byte btmCalibConst;
        public static Boolean blnAnalogDUT;
    }//End of clsModelSettings class.
}//End of namespace.
