using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsTolerancesOfPR69 : INotifyPropertyChanged
    {
        private int _FIVE_VOLT_MIN;

        public int FIVE_VOLT_MIN
        {
            get { return _FIVE_VOLT_MIN; }
            set { _FIVE_VOLT_MIN = value; OnPropertyChanged("FIVE_VOLT_MIN"); }
        }

        private int _FIVE_VOLT_MAX;

        public int FIVE_VOLT_MAX
        {
            get { return _FIVE_VOLT_MAX; }
            set { _FIVE_VOLT_MAX = value; OnPropertyChanged("FIVE_VOLT_MAX"); }
        }

        private int _TWELVE_mA_MIN;

        public int TWELVE_mA_MIN
        {
            get { return _TWELVE_mA_MIN; }
            set { _TWELVE_mA_MIN = value; OnPropertyChanged("TWELVE_mA_MIN"); }
        }

        private int _TWELVE_mA_MAX;

        public int TWELVE_mA_MAX
        {
            get { return _TWELVE_mA_MAX; }
            set { _TWELVE_mA_MAX = value; OnPropertyChanged("TWELVE_mA_MAX"); }
        }

        private int _One_VOLT_MAX;

        public int One_VOLT_MAX
        {
            get { return _One_VOLT_MAX; }
            set { _One_VOLT_MAX = value; OnPropertyChanged("One_VOLT_MAX"); }
        }

        private int _One_VOLT_MIN;

        public int One_VOLT_MIN
        {
            get { return _One_VOLT_MIN; }
            set { _One_VOLT_MIN = value; OnPropertyChanged("One_VOLT_MIN"); }
        }

        private int _TEN_VOLT_MAX;

        public int TEN_VOLT_MAX
        {
            get { return _TEN_VOLT_MAX; }
            set { _TEN_VOLT_MAX = value; OnPropertyChanged("TEN_VOLT_MAX"); }
        }

        private int _TEN_VOLT_MIN;

        public int TEN_VOLT_MIN
        {
            get { return _TEN_VOLT_MIN; }
            set { _TEN_VOLT_MIN = value; OnPropertyChanged("TEN_VOLT_MIN"); }
        }

        private int _FOUR_mAMP_MAX;

        public int FOUR_mAMP_MAX
        {
            get { return _FOUR_mAMP_MAX; }
            set { _FOUR_mAMP_MAX = value; OnPropertyChanged("FOUR_mAMP_MAX"); }
        }

        private int _FOUR_mAMP_MIN;

        public int FOUR_mAMP_MIN
        {
            get { return _FOUR_mAMP_MIN; }
            set { _FOUR_mAMP_MIN = value; OnPropertyChanged("FOUR_mAMP_MIN"); }
        }

        private int _TWENTY_mAMP_MAX;

        public int TWENTY_mAMP_MAX
        {
            get { return _TWENTY_mAMP_MAX; }
            set { _TWENTY_mAMP_MAX = value; OnPropertyChanged("TWENTY_mAMP_MAX"); }
        }

        private int _TWENTY_mAMP_MIN;

        public int TWENTY_mAMP_MIN
        {
            get { return _TWENTY_mAMP_MIN; }
            set { _TWENTY_mAMP_MIN = value; OnPropertyChanged("TWENTY_mAMP_MIN"); }
        }

        public void ParseTolerancedetails(ObservableCollection<ConfigurationDataList> ModifiedCatId)
        {
            try
            {
                FIVE_VOLT_MAX = ModifiedCatId[0].TolerancesOfPR69[0].FIVE_VOLT_MAX;
                FIVE_VOLT_MIN = ModifiedCatId[0].TolerancesOfPR69[0].FIVE_VOLT_MIN;
                FOUR_mAMP_MAX = ModifiedCatId[0].TolerancesOfPR69[0].FOUR_mAMP_MAX;
                FOUR_mAMP_MIN = ModifiedCatId[0].TolerancesOfPR69[0].FOUR_mAMP_MIN;
                TEN_VOLT_MAX = ModifiedCatId[0].TolerancesOfPR69[0].TEN_VOLT_MAX;
                TEN_VOLT_MIN = ModifiedCatId[0].TolerancesOfPR69[0].TEN_VOLT_MIN;
                One_VOLT_MAX = ModifiedCatId[0].TolerancesOfPR69[0].One_VOLT_MAX;
                One_VOLT_MIN = ModifiedCatId[0].TolerancesOfPR69[0].One_VOLT_MIN;
                TWELVE_mA_MAX = ModifiedCatId[0].TolerancesOfPR69[0].TWELVE_mA_MAX;
                TWELVE_mA_MIN = ModifiedCatId[0].TolerancesOfPR69[0].TWELVE_mA_MIN;
                TWENTY_mAMP_MAX = ModifiedCatId[0].TolerancesOfPR69[0].TWENTY_mAMP_MAX;
                TWENTY_mAMP_MIN = ModifiedCatId[0].TolerancesOfPR69[0].TWENTY_mAMP_MIN;
            }
            catch (Exception)
            {

            }
        }

        public TolerancesOfPR69 SaveTolerancedetails()
        {
            try
            {
                TolerancesOfPR69 tolerances = new TolerancesOfPR69()
                {
                    FIVE_VOLT_MIN = FIVE_VOLT_MIN,
                    FIVE_VOLT_MAX = FIVE_VOLT_MAX,
                    TWELVE_mA_MIN = TWELVE_mA_MIN,
                    TWELVE_mA_MAX = TWELVE_mA_MAX,
                    One_VOLT_MAX = One_VOLT_MAX,
                    One_VOLT_MIN = One_VOLT_MIN,
                    TEN_VOLT_MAX = TEN_VOLT_MAX,
                    TEN_VOLT_MIN = TEN_VOLT_MIN,
                    FOUR_mAMP_MAX = FOUR_mAMP_MAX,
                    FOUR_mAMP_MIN = FOUR_mAMP_MIN,
                    TWENTY_mAMP_MAX = TWENTY_mAMP_MAX,
                    TWENTY_mAMP_MIN = TWENTY_mAMP_MIN
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
