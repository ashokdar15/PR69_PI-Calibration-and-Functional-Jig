using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsAnalogOutputTests : INotifyPropertyChanged
    {

        private bool _SET_DFALT_1MA_CNT;

        public bool SET_DFALT_1MA_CNT
        {
            get { return _SET_DFALT_1MA_CNT; }
            set { _SET_DFALT_1MA_CNT = value; OnPropertyChanged("SET_DFALT_1MA_CNT"); }
        }

        private bool _SET_DFALT_4MA_CNT;

        public bool SET_DFALT_4MA_CNT
        {
            get { return _SET_DFALT_4MA_CNT; }
            set { _SET_DFALT_4MA_CNT = value; OnPropertyChanged("SET_DFALT_4MA_CNT"); }
        }

        private bool _SET_OBSRVED_1MA_CNT;

        public bool SET_OBSRVED_1MA_CNT
        {
            get { return _SET_OBSRVED_1MA_CNT; }
            set { _SET_OBSRVED_1MA_CNT = value; OnPropertyChanged("SET_OBSRVED_1MA_CNT"); }
        }

        private bool _SET_OBSRVED_4MA_CNT;

        public bool SET_OBSRVED_4MA_CNT
        {
            get { return _SET_OBSRVED_4MA_CNT; }
            set { _SET_OBSRVED_4MA_CNT = value; OnPropertyChanged("SET_OBSRVED_4MA_CNT"); }
        }
        private bool _SET_DFALT_20MA_CNT;

        public bool SET_DFALT_20MA_CNT
        {
            get { return _SET_DFALT_20MA_CNT; }
            set { _SET_DFALT_20MA_CNT = value; OnPropertyChanged("SET_DFALT_20MA_CNT"); }
        }

        private bool _SET_OBSRVED_20MA_CNT;

        public bool SET_OBSRVED_20MA_CNT
        {
            get { return _SET_OBSRVED_20MA_CNT; }
            set { _SET_OBSRVED_20MA_CNT = value; OnPropertyChanged("SET_OBSRVED_20MA_CNT"); }
        }

        private bool _CALIBRATE_CURRENT;

        public bool CALIBRATE_CURRENT
        {
            get { return _CALIBRATE_CURRENT; }
            set { _CALIBRATE_CURRENT = value; OnPropertyChanged("CALIBRATE_CURRENT"); }
        }

        private bool _SET_12MA_ANLOP;

        public bool SET_12MA_ANLOP
        {
            get { return _SET_12MA_ANLOP; }
            set { _SET_12MA_ANLOP = value; OnPropertyChanged("SET_12MA_ANLOP"); }
        }

        private bool _CHK_ANALOG_OP_VAL;

        public bool CHK_ANALOG_OP_VAL
        {
            get { return _CHK_ANALOG_OP_VAL; }
            set { _CHK_ANALOG_OP_VAL = value; OnPropertyChanged("CHK_ANALOG_OP_VAL"); }
        }

        private bool _SLAVE2_OP1_ON;

        public bool SLAVE2_OP1_ON
        {
            get { return _SLAVE2_OP1_ON; }
            set { _SLAVE2_OP1_ON = value; OnPropertyChanged("SLAVE2_OP1_ON"); }
        }


        private bool _SLAVE2_OP1_OFF;

        public bool SLAVE2_OP1_OFF
        {
            get { return _SLAVE2_OP1_OFF; }
            set { _SLAVE2_OP1_OFF = value; OnPropertyChanged("SLAVE2_OP1_OFF"); }
        }

        private bool _SLAVE2_OP2_ON;

        public bool SLAVE2_OP2_ON
        {
            get { return _SLAVE2_OP2_ON; }
            set { _SLAVE2_OP2_ON = value; OnPropertyChanged("SLAVE2_OP2_ON"); }
        }

        private bool _SLAVE2_OP2_OFF;

        public bool SLAVE2_OP2_OFF
        {
            get { return _SLAVE2_OP2_OFF; }
            set { _SLAVE2_OP2_OFF = value; OnPropertyChanged("SLAVE2_OP2_OFF"); }
        }


        private bool _SET_DFALT_1V_CNT;

        public bool SET_DFALT_1V_CNT
        {
            get { return _SET_DFALT_1V_CNT; }
            set { _SET_DFALT_1V_CNT = value; OnPropertyChanged("SET_DFALT_1V_CNT"); }
        }

        private bool _SET_OBSRVED_1V_CNT;

        public bool SET_OBSRVED_1V_CNT
        {
            get { return _SET_OBSRVED_1V_CNT; }
            set { _SET_OBSRVED_1V_CNT = value; OnPropertyChanged("SET_OBSRVED_1V_CNT"); }
        }

        private bool _SET_DFALT_10V_CNT;

        public bool SET_DFALT_10V_CNT
        {
            get { return _SET_DFALT_10V_CNT; }
            set { _SET_DFALT_10V_CNT = value; OnPropertyChanged("SET_DFALT_10V_CNT"); }
        }

        private bool _SET_OBSRVED_10V_CNT;

        public bool SET_OBSRVED_10V_CNT
        {
            get { return _SET_OBSRVED_10V_CNT; }
            set { _SET_OBSRVED_10V_CNT = value; OnPropertyChanged("SET_OBSRVED_10V_CNT"); }
        }

        private bool _CALIBRATE_VOLTAGE;

        public bool CALIBRATE_VOLTAGE
        {
            get { return _CALIBRATE_VOLTAGE; }
            set { _CALIBRATE_VOLTAGE = value; OnPropertyChanged("CALIBRATE_VOLTAGE"); }
        }

        private bool _SET_5V_ANLOP;

        public bool SET_5V_ANLOP
        {
            get { return _SET_5V_ANLOP; }
            set { _SET_5V_ANLOP = value; OnPropertyChanged("SET_5V_ANLOP"); }
        }

        public void ParseAnalogOPDetails(CatIdList catId)
        {
            if (catId.AnalogOpTests != null)
            {
                if (catId.AnalogOpTests.Count != 0)
                {
                    SET_DFALT_1MA_CNT = catId.AnalogOpTests[0].SET_DFALT_1MA_CNT;
                    CALIBRATE_CURRENT = catId.AnalogOpTests[0].CALIBRATE_CURRENT;
                    CALIBRATE_VOLTAGE = catId.AnalogOpTests[0].CALIBRATE_VOLTAGE;
                    CHK_ANALOG_OP_VAL = catId.AnalogOpTests[0].CHK_ANALOG_OP_VAL;
                    SET_12MA_ANLOP = catId.AnalogOpTests[0].SET_12MA_ANLOP;
                    SET_5V_ANLOP = catId.AnalogOpTests[0].SET_5V_ANLOP;
                    SET_DFALT_10V_CNT = catId.AnalogOpTests[0].SET_DFALT_10V_CNT;
                    SET_DFALT_1V_CNT = catId.AnalogOpTests[0].SET_DFALT_1V_CNT;
                    SET_DFALT_20MA_CNT = catId.AnalogOpTests[0].SET_DFALT_20MA_CNT;
                    SET_DFALT_4MA_CNT = catId.AnalogOpTests[0].SET_DFALT_4MA_CNT;
                    SET_OBSRVED_10V_CNT = catId.AnalogOpTests[0].SET_OBSRVED_10V_CNT;
                    SET_OBSRVED_1MA_CNT = catId.AnalogOpTests[0].SET_OBSRVED_1MA_CNT;
                    SET_OBSRVED_1V_CNT = catId.AnalogOpTests[0].SET_OBSRVED_1V_CNT;
                    SET_OBSRVED_20MA_CNT = catId.AnalogOpTests[0].SET_OBSRVED_20MA_CNT;
                    SET_OBSRVED_4MA_CNT = catId.AnalogOpTests[0].SET_OBSRVED_4MA_CNT;
                    SLAVE2_OP1_OFF = catId.AnalogOpTests[0].SLAVE2_OP1_OFF;
                    SLAVE2_OP1_ON = catId.AnalogOpTests[0].SLAVE2_OP1_ON;
                    SLAVE2_OP2_OFF = catId.AnalogOpTests[0].SLAVE2_OP2_OFF;
                    SLAVE2_OP2_ON = catId.AnalogOpTests[0].SLAVE2_OP2_ON;
                }
            }
        }

        public AnalogOutputTests SaveAnalogOutputTests()
        {
            try
            {
                AnalogOutputTests analogOPTests = new AnalogOutputTests()
                {
                     SET_DFALT_1MA_CNT = SET_DFALT_1MA_CNT,
                     CALIBRATE_CURRENT = CALIBRATE_CURRENT,
                     CALIBRATE_VOLTAGE = CALIBRATE_VOLTAGE,
                     CHK_ANALOG_OP_VAL = CHK_ANALOG_OP_VAL,
                     SET_12MA_ANLOP = SET_12MA_ANLOP,
                     SET_5V_ANLOP= SET_5V_ANLOP,
                     SET_DFALT_10V_CNT = SET_DFALT_10V_CNT,
                     SET_DFALT_1V_CNT= SET_DFALT_1V_CNT,
                     SET_DFALT_20MA_CNT = SET_DFALT_20MA_CNT,
                     SET_DFALT_4MA_CNT = SET_DFALT_4MA_CNT,
                     SET_OBSRVED_10V_CNT = SET_OBSRVED_10V_CNT,
                     SET_OBSRVED_1MA_CNT = SET_OBSRVED_1MA_CNT,
                     SET_OBSRVED_1V_CNT = SET_OBSRVED_1V_CNT,
                     SET_OBSRVED_20MA_CNT= SET_OBSRVED_20MA_CNT,
                     SET_OBSRVED_4MA_CNT = SET_OBSRVED_4MA_CNT,
                     SLAVE2_OP1_OFF = SLAVE2_OP1_OFF,
                     SLAVE2_OP1_ON = SLAVE2_OP1_ON,
                     SLAVE2_OP2_OFF = SLAVE2_OP2_OFF,
                     SLAVE2_OP2_ON = SLAVE2_OP2_ON

                };

                return analogOPTests;
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
