using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsRelayORSSRTests : INotifyPropertyChanged
    {
        private bool _SLAVE1_OP1_ON;

        public bool SLAVE1_OP1_ON
        {
            get { return _SLAVE1_OP1_ON; }
            set { _SLAVE1_OP1_ON = value; OnPropertyChanged("SLAVE1_OP1_ON"); }
        }

        private bool _SLAVE1_OP1_OFF;

        public bool SLAVE1_OP1_OFF
        {
            get { return _SLAVE1_OP1_OFF; }
            set { _SLAVE1_OP1_OFF = value; OnPropertyChanged("SLAVE1_OP1_OFF"); }
        }

        private bool _START_REL_TEST;

        public bool START_REL_TEST
        {
            get { return _START_REL_TEST; }
            set { _START_REL_TEST = value; OnPropertyChanged("START_REL_TEST"); }
        }

        private bool _DUT_OP1_ON;

        public bool DUT_OP1_ON
        {
            get { return _DUT_OP1_ON; }
            set { _DUT_OP1_ON = value; OnPropertyChanged("DUT_OP1_ON"); }
        }

        private bool _DUT_OP1_OFF;

        public bool DUT_OP1_OFF
        {
            get { return _DUT_OP1_OFF; }
            set { _DUT_OP1_OFF = value; OnPropertyChanged("DUT_OP1_OFF"); }
        }
        private bool _DUT_OP2_ON;

        public bool DUT_OP2_ON
        {
            get { return _DUT_OP2_ON; }
            set { _DUT_OP2_ON = value; OnPropertyChanged("DUT_OP2_ON"); }
        }
        private bool _DUT_OP2_OFF;

        public bool DUT_OP2_OFF
        {
            get { return _DUT_OP2_OFF; }
            set { _DUT_OP2_OFF = value; OnPropertyChanged("DUT_OP2_OFF"); }
        }

        private bool _DUT_OP3_ON;

        public bool DUT_OP3_ON
        {
            get { return _DUT_OP3_ON; }
            set { _DUT_OP3_ON = value; OnPropertyChanged("DUT_OP3_ON"); }
        }
        private bool _DUT_OP3_OFF;

        public bool DUT_OP3_OFF
        {
            get { return _DUT_OP3_OFF; }
            set { _DUT_OP3_OFF = value; OnPropertyChanged("DUT_OP3_OFF"); }
        }

        private bool _SLAVE1_OP2_ON;

        public bool SLAVE1_OP2_ON
        {
            get { return _SLAVE1_OP2_ON; }
            set { _SLAVE1_OP2_ON = value; OnPropertyChanged("SLAVE1_OP2_ON"); }
        }

        private bool _SLAVE1_OP3_ON;

        public bool SLAVE1_OP3_ON
        {
            get { return _SLAVE1_OP3_ON; }
            set { _SLAVE1_OP3_ON = value; OnPropertyChanged("SLAVE1_OP3_ON"); }
        }
        private bool _CONVERTOR_OP1_ON;

        public bool CONVERTOR_OP1_ON
        {
            get { return _CONVERTOR_OP1_ON; }
            set { _CONVERTOR_OP1_ON = value; OnPropertyChanged("CONVERTOR_OP1_ON"); }
        }
        private bool _CONVERTOR_OP2_ON;

        public bool CONVERTOR_OP2_ON
        {
            get { return _CONVERTOR_OP2_ON; }
            set { _CONVERTOR_OP2_ON = value; OnPropertyChanged("CONVERTOR_OP2_ON"); }
        }

        private bool _SLAVE1_READ_ADC_CNT_RLY_ON;

        public bool SLAVE1_READ_ADC_CNT_RLY_ON
        {
            get { return _SLAVE1_READ_ADC_CNT_RLY_ON; }
            set { _SLAVE1_READ_ADC_CNT_RLY_ON = value; OnPropertyChanged("SLAVE1_READ_ADC_CNT_RLY_ON"); }
        }

        private bool _SLAVE1_READ_ADC_CNT_RLY_OFF;

        public bool SLAVE1_READ_ADC_CNT_RLY_OFF
        {
            get { return _SLAVE1_READ_ADC_CNT_RLY_OFF; }
            set { _SLAVE1_READ_ADC_CNT_RLY_OFF = value; OnPropertyChanged("SLAVE1_READ_ADC_CNT_RLY_OFF"); }
        }
        private bool _CONVERTOR_OP1_OFF;

        public bool CONVERTOR_OP1_OFF
        {
            get { return _CONVERTOR_OP1_OFF; }
            set { _CONVERTOR_OP1_OFF = value; OnPropertyChanged("CONVERTOR_OP1_OFF"); }
        }
        private bool _CONVERTOR_OP2_OFF;

        public bool CONVERTOR_OP2_OFF
        {
            get { return _CONVERTOR_OP2_OFF; }
            set { _CONVERTOR_OP2_OFF = value; OnPropertyChanged("CONVERTOR_OP2_OFF"); }
        }
        private bool _SLAVE1_OP3_OFF;

        public bool SLAVE1_OP3_OFF
        {
            get { return _SLAVE1_OP3_OFF; }
            set { _SLAVE1_OP3_OFF = value; OnPropertyChanged("SLAVE1_OP3_OFF"); }
        }

        private bool _SLAVE2_OP3_ON;

        public bool SLAVE2_OP3_ON
        {
            get { return _SLAVE2_OP3_ON; }
            set { _SLAVE2_OP3_ON = value; OnPropertyChanged("SLAVE2_OP3_ON"); }
        }

        private bool _SLAVE3_OP3_ON;

        public bool SLAVE3_OP3_ON
        {
            get { return _SLAVE3_OP3_ON; }
            set { _SLAVE3_OP3_ON = value; OnPropertyChanged("SLAVE3_OP3_ON"); }
        }
        private bool _SLAVE2_READ_ADC_CNT_RLY_OFF;

        public bool SLAVE2_READ_ADC_CNT_RLY_OFF
        {
            get { return _SLAVE2_READ_ADC_CNT_RLY_OFF; }
            set { _SLAVE2_READ_ADC_CNT_RLY_OFF = value; OnPropertyChanged("SLAVE2_READ_ADC_CNT_RLY_OFF"); }
        }

        private bool _SLAVE2_READ_ADC_CNT_RLY_ON;

        public bool SLAVE2_READ_ADC_CNT_RLY_ON
        {
            get { return _SLAVE2_READ_ADC_CNT_RLY_ON; }
            set { _SLAVE2_READ_ADC_CNT_RLY_ON = value; OnPropertyChanged("SLAVE2_READ_ADC_CNT_RLY_ON"); }
        }

        public void ParseRelayOrSSRDetails(CatIdList catId)
        {
            if (catId.RelayOrSSRTests != null)
            {
                if (catId.RelayOrSSRTests.Count != 0)
                {
                    SLAVE1_OP1_ON = catId.RelayOrSSRTests[0].SLAVE1_OP1_ON;
                    SLAVE1_OP1_OFF = catId.RelayOrSSRTests[0].SLAVE1_OP1_OFF;
                    START_REL_TEST = catId.RelayOrSSRTests[0].START_REL_TEST;
                    DUT_OP1_ON = catId.RelayOrSSRTests[0].DUT_OP1_ON;
                    DUT_OP1_OFF = catId.RelayOrSSRTests[0].DUT_OP1_OFF;
                    DUT_OP2_ON = catId.RelayOrSSRTests[0].DUT_OP2_ON;
                    DUT_OP2_OFF = catId.RelayOrSSRTests[0].DUT_OP2_OFF;
                    DUT_OP3_ON = catId.RelayOrSSRTests[0].DUT_OP3_ON;
                    DUT_OP3_OFF = catId.RelayOrSSRTests[0].DUT_OP3_OFF;
                    SLAVE1_OP2_ON = catId.RelayOrSSRTests[0].SLAVE1_OP2_ON;
                    SLAVE1_OP3_ON = catId.RelayOrSSRTests[0].SLAVE1_OP3_ON;
                    CONVERTOR_OP1_ON = catId.RelayOrSSRTests[0].CONVERTOR_OP1_ON;
                    CONVERTOR_OP2_ON = catId.RelayOrSSRTests[0].CONVERTOR_OP2_ON;
                    SLAVE1_READ_ADC_CNT_RLY_ON = catId.RelayOrSSRTests[0].SLAVE1_READ_ADC_CNT_RLY_ON;
                    SLAVE1_READ_ADC_CNT_RLY_OFF = catId.RelayOrSSRTests[0].SLAVE1_READ_ADC_CNT_RLY_OFF;
                    CONVERTOR_OP1_OFF = catId.RelayOrSSRTests[0].CONVERTOR_OP1_OFF;
                    CONVERTOR_OP2_OFF = catId.RelayOrSSRTests[0].CONVERTOR_OP2_OFF;
                    SLAVE1_OP3_OFF = catId.RelayOrSSRTests[0].SLAVE1_OP3_OFF;
                    SLAVE2_OP3_ON = catId.RelayOrSSRTests[0].SLAVE2_OP3_ON;
                    SLAVE3_OP3_ON = catId.RelayOrSSRTests[0].SLAVE3_OP3_ON;
                    SLAVE2_READ_ADC_CNT_RLY_ON = catId.RelayOrSSRTests[0].SLAVE2_READ_ADC_CNT_RLY_ON;
                    SLAVE2_READ_ADC_CNT_RLY_OFF = catId.RelayOrSSRTests[0].SLAVE2_READ_ADC_CNT_RLY_OFF;
                }
            }
        }

        public RelayORSSRTests SaveRelayOrSSRTests()
        {
            try
            {
                RelayORSSRTests RelayOrSSRTests = new RelayORSSRTests()
                {
                    SLAVE1_OP1_ON = SLAVE1_OP1_ON,
                    SLAVE1_OP1_OFF = SLAVE1_OP1_OFF,
                    START_REL_TEST = START_REL_TEST,
                    DUT_OP1_ON = DUT_OP1_ON,
                    DUT_OP1_OFF = DUT_OP1_OFF,
                    DUT_OP2_ON = DUT_OP2_ON,
                    DUT_OP2_OFF = DUT_OP2_OFF,
                    DUT_OP3_ON = DUT_OP3_ON,
                    DUT_OP3_OFF = DUT_OP3_OFF,
                    SLAVE1_OP2_ON = SLAVE1_OP2_ON,
                    SLAVE1_OP3_ON = SLAVE1_OP3_ON,
                    CONVERTOR_OP1_ON = CONVERTOR_OP1_ON,
                    CONVERTOR_OP2_ON = CONVERTOR_OP2_ON,
                    SLAVE1_READ_ADC_CNT_RLY_ON = SLAVE1_READ_ADC_CNT_RLY_ON,
                    SLAVE1_READ_ADC_CNT_RLY_OFF = SLAVE1_READ_ADC_CNT_RLY_OFF,
                    CONVERTOR_OP1_OFF = CONVERTOR_OP1_OFF,
                    CONVERTOR_OP2_OFF = CONVERTOR_OP2_OFF,
                    SLAVE1_OP3_OFF = SLAVE1_OP3_OFF,
                    SLAVE2_OP3_ON = SLAVE2_OP3_ON,
                    SLAVE3_OP3_ON = SLAVE3_OP3_ON,
                    SLAVE2_READ_ADC_CNT_RLY_ON = SLAVE2_READ_ADC_CNT_RLY_ON,
                    SLAVE2_READ_ADC_CNT_RLY_OFF = SLAVE2_READ_ADC_CNT_RLY_OFF
                };

                return RelayOrSSRTests;
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
