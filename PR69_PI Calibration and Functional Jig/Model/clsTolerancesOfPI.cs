using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsTolerancesOfPI : INotifyPropertyChanged
    {

        private int _FIVE_VOLT_MIN_PI;

        public int FIVE_VOLT_MIN_PI
        {
            get { return _FIVE_VOLT_MIN_PI; }
            set { _FIVE_VOLT_MIN_PI = value; OnPropertyChanged("FIVE_VOLT_MIN_PI"); }
        }

        private int _FIVE_VOLT_MAX_PI;

        public int FIVE_VOLT_MAX_PI
        {
            get { return _FIVE_VOLT_MAX_PI; }
            set { _FIVE_VOLT_MAX_PI = value; OnPropertyChanged("FIVE_VOLT_MAX_PI"); }
        }

        private int _TWELVE_mA_MIN_PI;

        public int TWELVE_mA_MIN_PI
        {
            get { return _TWELVE_mA_MIN_PI; }
            set { _TWELVE_mA_MIN_PI = value; OnPropertyChanged("TWELVE_mA_MIN_PI"); }
        }

        private int _TWELVE_mA_MAX_PI;

        public int TWELVE_mA_MAX_PI
        {
            get { return _TWELVE_mA_MAX_PI; }
            set { _TWELVE_mA_MAX_PI = value; OnPropertyChanged("TWELVE_mA_MAX_PI"); }
        }

        private int _One_VOLT_MAX_PI;

        public int One_VOLT_MAX_PI
        {
            get { return _One_VOLT_MAX_PI; }
            set { _One_VOLT_MAX_PI = value; OnPropertyChanged("One_VOLT_MAX_PI"); }
        }

        private int _One_VOLT_MIN_PI;

        public int One_VOLT_MIN_PI
        {
            get { return _One_VOLT_MIN_PI; }
            set { _One_VOLT_MIN_PI = value; OnPropertyChanged("One_VOLT_MIN_PI"); }
        }

        private int _TEN_VOLT_MAX_PI;

        public int TEN_VOLT_MAX_PI
        {
            get { return _TEN_VOLT_MAX_PI; }
            set { _TEN_VOLT_MAX_PI = value; OnPropertyChanged("TEN_VOLT_MAX_PI"); }
        }

        private int _TEN_VOLT_MIN_PI;

        public int TEN_VOLT_MIN_PI
        {
            get { return _TEN_VOLT_MIN_PI; }
            set { _TEN_VOLT_MIN_PI = value; OnPropertyChanged("TEN_VOLT_MIN_PI"); }
        }

        private int _ONE_mAMP_MAX;

        public int ONE_mAMP_MAX
        {
            get { return _ONE_mAMP_MAX; }
            set { _ONE_mAMP_MAX = value; OnPropertyChanged("ONE_mAMP_MAX"); }
        }

        private int _ONE_mAMP_MIN;

        public int ONE_mAMP_MIN
        {
            get { return _ONE_mAMP_MIN; }
            set { _ONE_mAMP_MIN = value; OnPropertyChanged("ONE_mAMP_MIN"); }
        }

        private int _TWENTY_mAMP_MAX_PI;

        public int TWENTY_mAMP_MAX_PI
        {
            get { return _TWENTY_mAMP_MAX_PI; }
            set { _TWENTY_mAMP_MAX_PI = value; OnPropertyChanged("TWENTY_mAMP_MAX_PI"); }
        }

        private int _TWENTY_mAMP_MIN_PI;

        public int TWENTY_mAMP_MIN_PI
        {
            get { return _TWENTY_mAMP_MIN_PI; }
            set { _TWENTY_mAMP_MIN_PI = value; OnPropertyChanged("TWENTY_mAMP_MIN_PI"); }
        }

        public void ParseTolerancedetails(ObservableCollection<ConfigurationDataList> ModifiedCatId)
        {
            try
            {
                FIVE_VOLT_MAX_PI = ModifiedCatId[0].TolerancesofPI[0].FIVE_VOLT_MAX_PI;
                FIVE_VOLT_MIN_PI = ModifiedCatId[0].TolerancesofPI[0].FIVE_VOLT_MIN_PI;
                ONE_mAMP_MAX = ModifiedCatId[0].TolerancesofPI[0].ONE_mAMP_MAX;
                ONE_mAMP_MIN = ModifiedCatId[0].TolerancesofPI[0].ONE_mAMP_MIN;
                TEN_VOLT_MAX_PI = ModifiedCatId[0].TolerancesofPI[0].TEN_VOLT_MAX_PI;
                TEN_VOLT_MIN_PI = ModifiedCatId[0].TolerancesofPI[0].TEN_VOLT_MIN_PI;
                One_VOLT_MAX_PI = ModifiedCatId[0].TolerancesofPI[0].One_VOLT_MAX_PI;
                One_VOLT_MIN_PI = ModifiedCatId[0].TolerancesofPI[0].One_VOLT_MIN_PI;
                TWELVE_mA_MAX_PI = ModifiedCatId[0].TolerancesofPI[0].TWELVE_mA_MAX_PI;
                TWELVE_mA_MIN_PI = ModifiedCatId[0].TolerancesofPI[0].TWELVE_mA_MIN_PI;
                TWENTY_mAMP_MAX_PI = ModifiedCatId[0].TolerancesofPI[0].TWENTY_mAMP_MAX_PI;
                TWENTY_mAMP_MIN_PI = ModifiedCatId[0].TolerancesofPI[0].TWENTY_mAMP_MIN_PI;
            }
            catch (Exception)
            {

            }
        }

        public TolerancesOfPI SaveTolerancedetails()
        {
            try
            {
                TolerancesOfPI tolerances = new TolerancesOfPI()
                {
                    FIVE_VOLT_MIN_PI = FIVE_VOLT_MIN_PI,
                    FIVE_VOLT_MAX_PI = FIVE_VOLT_MAX_PI,
                    TWELVE_mA_MIN_PI = TWELVE_mA_MIN_PI,
                    TWELVE_mA_MAX_PI = TWELVE_mA_MAX_PI,
                    One_VOLT_MAX_PI = One_VOLT_MAX_PI,
                    One_VOLT_MIN_PI = One_VOLT_MIN_PI,
                    TEN_VOLT_MAX_PI = TEN_VOLT_MAX_PI,
                    TEN_VOLT_MIN_PI = TEN_VOLT_MIN_PI,
                    ONE_mAMP_MAX = ONE_mAMP_MAX,
                    ONE_mAMP_MIN = ONE_mAMP_MIN,
                    TWENTY_mAMP_MAX_PI = TWENTY_mAMP_MAX_PI,
                    TWENTY_mAMP_MIN_PI = TWENTY_mAMP_MIN_PI
                };

                return tolerances;
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
