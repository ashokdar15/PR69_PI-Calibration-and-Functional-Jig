using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsAnalogInputTests : INotifyPropertyChanged
    {

        private bool _CALIB_1V_CNT;

        public bool CALIB_1V_CNT
        {
            get { return _CALIB_1V_CNT; }
            set { _CALIB_1V_CNT = value; OnPropertyChanged("CALIB_1V_CNT"); }
        }

        private bool _CALIB_9V_CNT;

        public bool CALIB_9V_CNT
        {
            get { return _CALIB_9V_CNT; }
            set { _CALIB_9V_CNT = value; OnPropertyChanged("CALIB_9V_CNT"); }
        }

        private bool _CALIB_4mA_CNT;

        public bool CALIB_4mA_CNT
        {
            get { return _CALIB_4mA_CNT; }
            set { _CALIB_4mA_CNT = value; OnPropertyChanged("CALIB_4mA_CNT"); }
        }

        private bool _CALIB_20mA_CNT;

        public bool CALIB_20mA_CNT
        {
            get { return _CALIB_20mA_CNT; }
            set { _CALIB_20mA_CNT = value; OnPropertyChanged("CALIB_20mA_CNT"); }
        }

        private bool _CALIB_9V_CNT_PI;

        public bool CALIB_9V_CNT_PI
        {
            get { return _CALIB_9V_CNT_PI; }
            set { _CALIB_9V_CNT_PI = value; OnPropertyChanged("CALIB_9V_CNT_PI"); }
        }

        private bool _CALIB_1V_CNT_PI;

        public bool CALIB_1V_CNT_PI
        {
            get { return _CALIB_1V_CNT_PI; }
            set { _CALIB_1V_CNT_PI = value; OnPropertyChanged("CALIB_1V_CNT_PI"); }
        }

        private bool _CALIB_20mA_CNT_PI;

        public bool CALIB_20mA_CNT_PI
        {
            get { return _CALIB_20mA_CNT_PI; }
            set { _CALIB_20mA_CNT_PI = value; OnPropertyChanged("CALIB_20mA_CNT_PI"); }
        }

        private bool _CALIB_1mA_CNT_PI;

        public bool CALIB_1mA_CNT_PI
        {
            get { return _CALIB_1mA_CNT_PI; }
            set { _CALIB_1mA_CNT_PI = value; OnPropertyChanged("CALIB_1mA_CNT_PI"); }
        }

        public void ParseAnalogIPDetails(CatIdList catId)
        {
            if (catId.AnalogIpTests != null)
            {
                if (catId.AnalogIpTests.Count != 0)
                {
                    CALIB_1mA_CNT_PI = catId.AnalogIpTests[0].CALIB_1mA_CNT_PI;
                    CALIB_1V_CNT = catId.AnalogIpTests[0].CALIB_1V_CNT;
                    CALIB_1V_CNT_PI = catId.AnalogIpTests[0].CALIB_1V_CNT_PI;
                    CALIB_20mA_CNT = catId.AnalogIpTests[0].CALIB_20mA_CNT;
                    CALIB_20mA_CNT_PI = catId.AnalogIpTests[0].CALIB_20mA_CNT_PI;
                    CALIB_4mA_CNT = catId.AnalogIpTests[0].CALIB_4mA_CNT;
                    CALIB_9V_CNT = catId.AnalogIpTests[0].CALIB_9V_CNT;
                    CALIB_9V_CNT_PI = catId.AnalogIpTests[0].CALIB_9V_CNT_PI;
                }
            }
        }

        public AnalogInputTests SaveAnalogIPTests()
        {
            try
            {
                AnalogInputTests analogInputTests = new AnalogInputTests()
                {
                    CALIB_1mA_CNT_PI = CALIB_1mA_CNT_PI,
                    CALIB_1V_CNT= CALIB_1V_CNT,
                    CALIB_1V_CNT_PI=CALIB_1V_CNT_PI,
                    CALIB_20mA_CNT=CALIB_20mA_CNT,
                    CALIB_20mA_CNT_PI=CALIB_20mA_CNT_PI,
                    CALIB_4mA_CNT=CALIB_4mA_CNT,
                    CALIB_9V_CNT=CALIB_9V_CNT,
                    CALIB_9V_CNT_PI=CALIB_9V_CNT_PI
                };

                return analogInputTests;
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
