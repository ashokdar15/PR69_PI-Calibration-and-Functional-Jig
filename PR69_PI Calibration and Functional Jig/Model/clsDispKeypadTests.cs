using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsDispKeypadTests : INotifyPropertyChanged
    {
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

        internal void ParseRelayOrSSRDetails(CatIdList catId)
        {
            if (catId.DispKeypadTests != null)
            {
                if (catId.DispKeypadTests.Count != 0)
                {
                    START_DISP_TEST = catId.DispKeypadTests[0].START_DISP_TEST;
                    START_KEYPAD_TEST = catId.DispKeypadTests[0].START_KEYPAD_TEST;
                }
            }
        }

        internal DispKeypadTests SaveCalibConstantsTests()
        {
            try
            {
                DispKeypadTests DispkeypadTests = new DispKeypadTests()
                {
                    START_DISP_TEST = START_DISP_TEST,
                    START_KEYPAD_TEST =START_KEYPAD_TEST
                };

                return DispkeypadTests;
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
