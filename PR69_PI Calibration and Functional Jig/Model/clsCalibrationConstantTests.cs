using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsCalibrationConstantTests : INotifyPropertyChanged
    {
        private bool _WRITE_CALIB_CONST;

        public bool WRITE_CALIB_CONST
        {
            get { return _WRITE_CALIB_CONST; }
            set { _WRITE_CALIB_CONST = value; OnPropertyChanged("WRITE_CALIB_CONST"); }
        }

        private bool _WRITE_CALIB_CONST_WITH_VREF;

        public bool WRITE_CALIB_CONST_WITH_VREF
        {
            get { return _WRITE_CALIB_CONST_WITH_VREF; }
            set { _WRITE_CALIB_CONST_WITH_VREF = value; OnPropertyChanged("WRITE_CALIB_CONST_WITH_VREF"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        internal void ParseCalibConstantDetails(CatIdList catId)
        {
            if (catId.CalibrationConstantsTests != null)
            {
                if (catId.CalibrationConstantsTests.Count != 0)
                {
                    WRITE_CALIB_CONST = catId.CalibrationConstantsTests[0].WRITE_CALIB_CONST;
                    WRITE_CALIB_CONST_WITH_VREF = catId.CalibrationConstantsTests[0].WRITE_CALIB_CONST_WITH_VREF;
                    
                }
            }
        }

        internal CalibrationConstants SaveCalibConstantsTests()
        {
            try
            {
                CalibrationConstants CalibConstTests = new CalibrationConstants()
                {
                    WRITE_CALIB_CONST = WRITE_CALIB_CONST,
                    WRITE_CALIB_CONST_WITH_VREF = WRITE_CALIB_CONST_WITH_VREF
                };

                return CalibConstTests;
            }
            catch (Exception)
            {
                return null;
            }
           
        }
    }
}
