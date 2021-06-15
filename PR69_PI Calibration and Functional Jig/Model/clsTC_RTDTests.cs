using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsTC_RTDTests : INotifyPropertyChanged
    {

        public clsTC_RTDTests()
        {
            if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_48x48)
            {
                IsPR43Product = true;
                IsPR69Product = false;
            }
            else
            {
                IsPR43Product = false;
                IsPR69Product = true;
            }
        }

        private bool _IsPR43Product;

        public bool IsPR43Product
        {
            get { return _IsPR43Product; }
            set { _IsPR43Product = value; OnPropertyChanged("IsPR43Product"); }
        }

        private bool _IsPR69Product;

        public bool IsPR69Product
        {
            get { return _IsPR69Product; }
            set { _IsPR69Product = value; OnPropertyChanged("IsPR69Product"); }
        }


        private bool _CALIB_MV_CNT;

        public bool CALIB_MV_CNT_PR69_PI
        {
            get { return _CALIB_MV_CNT; }
            set
            {
                _CALIB_MV_CNT = value;
                if (_CALIB_MV_CNT)
                {
                    CALIB_1_MV_CNT = true;
                    CALIB_50_MV_CNT = true;
                    CALC_SLOPE_OFFSET = true;
                    CALIB_47_68_MV_CNT = false;
                }
                else
                {
                    CALIB_1_MV_CNT = false;
                    CALIB_50_MV_CNT = false;
                    CALC_SLOPE_OFFSET = false;
                }                
                OnPropertyChanged("CALIB_MV_CNT_PR69_PI");
            }
        }

        private bool _CALIB_PT100_PR69_PI;

        public bool CALIB_PT100_PR69_PI
        {
            get { return _CALIB_PT100_PR69_PI; }
            set
            {
                _CALIB_PT100_PR69_PI = value;
                if (_CALIB_PT100_PR69_PI)
                {
                    CALIB_PT100 = true;
                    CALIB_TC = true;
                    CALIB_100_OHM = false;
                    CALIB_313_71_OHM = false;
                }
                else
                {
                    CALIB_PT100 = false;
                    CALIB_TC = false;
                }

                OnPropertyChanged("CALIB_PT100_PR69_PI");
            }
        }

        private bool _CALIB_MV_CNT_PR43;

        public bool CALIB_MV_CNT_PR43
        {
            get { return _CALIB_MV_CNT_PR43; }
            set
            {
                _CALIB_MV_CNT_PR43 = value;
                if (_CALIB_MV_CNT_PR43)
                {
                    CALIB_1_MV_CNT = true;                    
                    CALC_SLOPE_OFFSET = true;
                    CALIB_47_68_MV_CNT = true;
                    CALIB_50_MV_CNT = false;
                }
                else
                {
                    //CALIB_1_MV_CNT = false;
                    //CALC_SLOPE_OFFSET = false;
                    //CALIB_47_68_MV_CNT = false;
                }

                OnPropertyChanged("CALIB_MV_CNT_PR43");
            }
        }


        private bool _CALIB_PT100_PR43;

        public bool CALIB_PT100_PR43
        {
            get { return _CALIB_PT100_PR43; }
            set
            {
                _CALIB_PT100_PR43 = value;
                if (_CALIB_PT100_PR43)
                {
                    CALIB_100_OHM = true;
                    CALIB_313_71_OHM = true;
                }
                else
                {
                    CALIB_100_OHM = false;
                    CALIB_313_71_OHM = false;
                }

                OnPropertyChanged("CALIB_PT100_PR43");
            }
        }
        
        private bool _CALIB_1_MV_CNT;

        public bool CALIB_1_MV_CNT
        {
            get { return _CALIB_1_MV_CNT; }
            set { _CALIB_1_MV_CNT = value; OnPropertyChanged("CALIB_1_MV_CNT"); }
        }

        private bool _CALIB_47_68_MV_CNT;

        public bool CALIB_47_68_MV_CNT
        {
            get { return _CALIB_47_68_MV_CNT; }
            set { _CALIB_47_68_MV_CNT = value; OnPropertyChanged("CALIB_47_68_MV_CNT"); }
        }

        private bool _CALIB_50_MV_CNT;

        public bool CALIB_50_MV_CNT
        {
            get { return _CALIB_50_MV_CNT; }
            set { _CALIB_50_MV_CNT = value; OnPropertyChanged("CALIB_50_MV_CNT"); }
        }

        private bool _CALC_SLOPE_OFFSET;

        public bool CALC_SLOPE_OFFSET
        {
            get { return _CALC_SLOPE_OFFSET; }
            set { _CALC_SLOPE_OFFSET = value; OnPropertyChanged("CALC_SLOPE_OFFSET"); }
        }

        private bool _CALIB_PT100;

        public bool CALIB_PT100
        {
            get { return _CALIB_PT100; }
            set
            {
                _CALIB_PT100 = value;
                OnPropertyChanged("CALIB_PT100");
            }
        }

        private bool _CALIB_TC;

        public bool CALIB_TC
        {
            get { return _CALIB_TC; }
            set { _CALIB_TC = value; OnPropertyChanged("CALIB_TC"); }
        }

        private bool _CALIB_100_OHM;

        public bool CALIB_100_OHM
        {
            get { return _CALIB_100_OHM; }
            set { _CALIB_100_OHM = value; OnPropertyChanged("CALIB_100_OHM"); }
        }

        private bool _CALIB_313_71_OHM;

        public bool CALIB_313_71_OHM
        {
            get { return _CALIB_313_71_OHM; }
            set { _CALIB_313_71_OHM = value; OnPropertyChanged("CALIB_313_71_OHM"); }
        }
        
        public void ParseTC_RTDDetails(CatIdList catId)
        {
            if (catId.TC_RTDTests != null)
            {
                if (catId.TC_RTDTests.Count != 0)
                {
                    CALC_SLOPE_OFFSET = catId.TC_RTDTests[0].CALC_SLOPE_OFFSET;
                    CALIB_100_OHM = catId.TC_RTDTests[0].CALIB_100_OHM;
                    CALIB_1_MV_CNT = catId.TC_RTDTests[0].CALIB_1_MV_CNT;
                    CALIB_313_71_OHM = catId.TC_RTDTests[0].CALIB_313_71_OHM;
                    CALIB_47_68_MV_CNT = catId.TC_RTDTests[0].CALIB_47_68_MV_CNT;
                    CALIB_50_MV_CNT = catId.TC_RTDTests[0].CALIB_50_MV_CNT;
                    CALIB_PT100 = catId.TC_RTDTests[0].CALIB_PT100;
                    CALIB_TC = catId.TC_RTDTests[0].CALIB_TC;
                }

                if (CALIB_50_MV_CNT)
                    CALIB_MV_CNT_PR69_PI = true;
                else
                    CALIB_MV_CNT_PR69_PI = false;

                if (CALIB_PT100)
                    CALIB_PT100_PR69_PI = true;
                else
                    CALIB_PT100_PR69_PI = false;

                if (CALIB_47_68_MV_CNT)
                    CALIB_MV_CNT_PR43 = true;              
                else
                    CALIB_MV_CNT_PR43 = false;

                if (CALIB_100_OHM)
                    CALIB_PT100_PR43 = true;
                else
                    CALIB_PT100_PR43 = false;

            }
        }

        public TC_RTDCalibTests SaveTC_RTDTests()
        {
            try
            {
                TC_RTDCalibTests TC_RTDTests = new TC_RTDCalibTests()
                {
                    CALC_SLOPE_OFFSET = CALC_SLOPE_OFFSET,
                    CALIB_100_OHM = CALIB_100_OHM,
                    CALIB_1_MV_CNT = CALIB_1_MV_CNT,
                    CALIB_313_71_OHM = CALIB_313_71_OHM,
                    CALIB_47_68_MV_CNT = CALIB_47_68_MV_CNT,
                    CALIB_50_MV_CNT= CALIB_50_MV_CNT,
                    CALIB_PT100 = CALIB_PT100,
                    CALIB_TC = CALIB_TC
                };

                return TC_RTDTests;
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
