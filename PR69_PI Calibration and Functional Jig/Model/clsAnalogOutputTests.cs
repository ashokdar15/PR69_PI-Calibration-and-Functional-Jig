using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
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
            set
            {
                _SET_DFALT_1MA_CNT = value;

                if (_SET_DFALT_1MA_CNT)
                {
                    SET_OBSRVED_1MA_CNT = true;
                    SET_DFALT_20MA_CNT = true;
                    SET_OBSRVED_20MA_CNT = true;
                    CALIBRATE_CURRENT = true;
                    SET_12MA_ANLOP = true;
                    CHK_ANALOG_OP_VAL = true;
                }
                else
                {
                    SET_OBSRVED_1MA_CNT = false;
                    SET_DFALT_20MA_CNT = false;
                    SET_OBSRVED_20MA_CNT = false;
                    CALIBRATE_CURRENT = false;
                    SET_12MA_ANLOP = false;
                    CHK_ANALOG_OP_VAL = false;
                }

                OnPropertyChanged("SET_DFALT_1MA_CNT");
            }
        }


        private bool _SET_DFALT_4MA_CNT;

        public bool SET_DFALT_4MA_CNT
        {
            get { return _SET_DFALT_4MA_CNT; }
            set
            {
                _SET_DFALT_4MA_CNT = value;

                if (_SET_DFALT_4MA_CNT)
                {
                    SET_OBSRVED_4MA_CNT = true;
                    SET_DFALT_20MA_CNT = true;
                    SET_OBSRVED_20MA_CNT = true;
                    CALIBRATE_CURRENT = true;
                    SET_12MA_ANLOP = true;
                    CHK_ANALOG_OP_VAL = true;
                }
                else
                {
                    SET_OBSRVED_4MA_CNT = false;
                    SET_DFALT_20MA_CNT = false;
                    SET_OBSRVED_20MA_CNT = false;
                    CALIBRATE_CURRENT = false;
                    SET_12MA_ANLOP = false;
                    CHK_ANALOG_OP_VAL = false;
                }

                OnPropertyChanged("SET_DFALT_4MA_CNT");
            }
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

        private bool _IsPR69Product;

        public bool IsPR69Product
        {
            get { return _IsPR69Product; }
            set { _IsPR69Product = value; OnPropertyChanged("IsPR69Product"); }
        }

        private bool _IsPIProduct;

        public bool IsPIProduct
        {
            get { return _IsPIProduct; }
            set { _IsPIProduct = value; OnPropertyChanged("IsPIProduct"); }
        }



        private bool _SET_DFALT_1V_CNT;

        public bool SET_DFALT_1V_CNT
        {
            get { return _SET_DFALT_1V_CNT; }
            set
            {
                _SET_DFALT_1V_CNT = value;

                if (_SET_DFALT_1V_CNT)
                {
                    SET_OBSRVED_1V_CNT = true;
                    SET_DFALT_10V_CNT = true;
                    SET_OBSRVED_10V_CNT = true;
                    CALIBRATE_VOLTAGE = true;
                    SET_5V_ANLOP = true;
                    CHK_ANALOG_OP_VAL = true;
                }
                else
                {
                    SET_OBSRVED_1V_CNT = false;
                    SET_DFALT_10V_CNT = false;
                    SET_OBSRVED_10V_CNT = false;
                    CALIBRATE_VOLTAGE = false;
                    SET_5V_ANLOP = false;
                    CHK_ANALOG_OP_VAL = false;
                }

                OnPropertyChanged("SET_DFALT_1V_CNT");
            }
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
                }
                IsPR69Product = true;
                IsPIProduct = true;
                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 ||
                    clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48 ||
                    clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_48x48)
                {
                    //IsPR69Product = true;
                    //IsPIProduct = false;
                }
                else
                {
                    //IsPR69Product = false;
                    //IsPIProduct = true;
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
                     SET_OBSRVED_4MA_CNT = SET_OBSRVED_4MA_CNT

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
