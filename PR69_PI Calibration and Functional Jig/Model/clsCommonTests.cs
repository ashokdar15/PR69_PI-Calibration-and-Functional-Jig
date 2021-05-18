using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsCommonTests : INotifyPropertyChanged
    {

        private bool _READ_DEVICE_ID;

        public bool READ_DEVICE_ID
        {
            get { return _READ_DEVICE_ID; }
            set { _READ_DEVICE_ID = value; OnPropertyChanged("READ_DEVICE_ID"); }
        }

        private bool _READ_CALIB_CONST;

        public bool READ_CALIB_CONST
        {
            get { return _READ_CALIB_CONST; }
            set { _READ_CALIB_CONST = value; OnPropertyChanged("READ_CALIB_CONST"); }
        }

        private bool _SWITCH_SENSOR_RELAY;

        public bool SWITCH_SENSOR_RELAY
        {
            get { return _SWITCH_SENSOR_RELAY; }
            set { _SWITCH_SENSOR_RELAY = value; OnPropertyChanged("SWITCH_SENSOR_RELAY"); }
        }

       
        private bool _START_DISP_TEST;

        public bool START_DISP_TEST
        {
            get { return _START_DISP_TEST; }
            set { _START_DISP_TEST = value; OnPropertyChanged("START_DISP_TEST"); }
        }


        private bool _START_KEYPAD_TEST;

        public bool START_KEYPAD_TEST
        {
            get { return _START_KEYPAD_TEST; }
            set { _START_KEYPAD_TEST = value; OnPropertyChanged("START_KEYPAD_TEST"); }
        }

        private bool _Vtg24V_OP_TEST;

        public bool Vtg24V_OP_TEST
        {
            get { return _Vtg24V_OP_TEST; }
            set { _Vtg24V_OP_TEST = value; OnPropertyChanged("Vtg24V_OP_TEST"); }
        }

        private bool _START_MODBUS_TEST;

        public bool START_MODBUS_TEST
        {
            get { return _START_MODBUS_TEST; }
            set { _START_MODBUS_TEST = value; OnPropertyChanged("START_MODBUS_TEST"); }
        }

        private bool _CJC_TEST;

        public bool CJC_TEST
        {
            get { return _CJC_TEST; }
            set { _CJC_TEST = value; OnPropertyChanged("CJC_TEST"); }
        }


        internal void ParseRelayOrSSRDetails(CatIdList catId)
        {
            if (catId.CommonCalibTests != null)
            {
                if (catId.CommonCalibTests.Count != 0)
                {
                    READ_DEVICE_ID = catId.CommonCalibTests[0].READ_DEVICE_ID;
                                    
                    READ_CALIB_CONST = catId.CommonCalibTests[0].READ_CALIB_CONST;
                   
                    START_DISP_TEST = catId.CommonCalibTests[0].START_DISP_TEST;
                    START_KEYPAD_TEST = catId.CommonCalibTests[0].START_KEYPAD_TEST;
                    SWITCH_SENSOR_RELAY = catId.CommonCalibTests[0].SWITCH_SENSOR_RELAY;
                    Vtg24V_OP_TEST = catId.CommonCalibTests[0].Vtg24V_OP_TEST;
                    START_MODBUS_TEST = catId.CommonCalibTests[0].START_MODBUS_TEST;
                    CJC_TEST = catId.CommonCalibTests[0].CJC_TEST;
                }
            }
        }

        internal CommonTests SaveCalibConstantsTests()
        {
            try
            {
                CommonTests CommonTests = new CommonTests()
                {
                    READ_DEVICE_ID = READ_DEVICE_ID,
                  
                    READ_CALIB_CONST = READ_CALIB_CONST,
                   
                    START_DISP_TEST = START_DISP_TEST,
                    START_KEYPAD_TEST = START_KEYPAD_TEST,
                    SWITCH_SENSOR_RELAY = SWITCH_SENSOR_RELAY,
                    Vtg24V_OP_TEST = Vtg24V_OP_TEST,
                    START_MODBUS_TEST = START_MODBUS_TEST,
                    CJC_TEST = CJC_TEST
                };

                return CommonTests;
            }
            catch (Exception)
            {
                return null;
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
