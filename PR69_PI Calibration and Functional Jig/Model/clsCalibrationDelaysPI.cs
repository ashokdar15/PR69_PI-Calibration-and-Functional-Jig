using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsCalibrationDelaysPI : INotifyPropertyChanged
    {
        private int _OnemVOrFiftymVStartModeDelay;

        public int OnemVOrFiftymVStartModeDelay
        {
            get { return _OnemVOrFiftymVStartModeDelay; }
            set { _OnemVOrFiftymVStartModeDelay = value; OnPropertyChanged("OnemVOrFiftymVStartModeDelay"); }
        }

        private int _OnemVOrFiftymVRunModeDelay;

        public int OnemVOrFiftymVRunModeDelay
        {
            get { return _OnemVOrFiftymVRunModeDelay; }
            set { _OnemVOrFiftymVRunModeDelay = value; OnPropertyChanged("OnemVOrFiftymVRunModeDelay"); }
        }

        private int _ThreeFiftyOhmStartModeDelay;

        public int ThreeFiftyOhmStartModeDelay
        {
            get { return _ThreeFiftyOhmStartModeDelay; }
            set { _ThreeFiftyOhmStartModeDelay = value; OnPropertyChanged("ThreeFiftyOhmStartModeDelay"); }
        }

        private int _ThreeFiftyOhmRunModeDelay;

        public int ThreeFiftyOhmRunModeDelay
        {
            get { return _ThreeFiftyOhmRunModeDelay; }
            set { _ThreeFiftyOhmRunModeDelay = value; OnPropertyChanged("ThreeFiftyOhmRunModeDelay"); }
        }

        private int _FourmAORTwentymAStartModeDelay;

        public int FourmAORTwentymAStartModeDelay
        {
            get { return _FourmAORTwentymAStartModeDelay; }
            set { _FourmAORTwentymAStartModeDelay = value; OnPropertyChanged("FourmAORTwentymAStartModeDelay"); }
        }

        private int _FourmAORTwentymARunModeDelay;

        public int FourmAORTwentymARunModeDelay
        {
            get { return _FourmAORTwentymARunModeDelay; }
            set { _FourmAORTwentymARunModeDelay = value; OnPropertyChanged("FourmAORTwentymARunModeDelay"); }
        }

        private int _OneVoltOrNineVoltStartModeDelay;

        public int OneVoltOrNineVoltStartModeDelay
        {
            get { return _OneVoltOrNineVoltStartModeDelay; }
            set { _OneVoltOrNineVoltStartModeDelay = value; OnPropertyChanged("OneVoltOrNineVoltStartModeDelay"); }
        }

        private int _OneVoltOrNineVoltRunModeDelay;

        public int OneVoltOrNineVoltRunModeDelay
        {
            get { return _OneVoltOrNineVoltRunModeDelay; }
            set { _OneVoltOrNineVoltRunModeDelay = value; OnPropertyChanged("OneVoltOrNineVoltRunModeDelay"); }
        }

        private int _AnalogOutputObservedValueDelay;

        public int AnalogOutputObservedValueDelay
        {
            get { return _AnalogOutputObservedValueDelay; }
            set { _AnalogOutputObservedValueDelay = value; OnPropertyChanged("AnalogOutputObservedValueDelay"); }
        }

        
        public void ParseCalibrationDelays(ObservableCollection<ConfigurationDataList> ModifiedCatId)
        {
            try
            {
                OnemVOrFiftymVStartModeDelay = ModifiedCatId[0].CalibrationDelaysPI[0].ONEmV_DELAY_AFTER_STARTMODE;
                OnemVOrFiftymVRunModeDelay = ModifiedCatId[0].CalibrationDelaysPI[0].ONEmV_DELAY_AFTER_RUNMODE;
                ThreeFiftyOhmStartModeDelay = ModifiedCatId[0].CalibrationDelaysPI[0].ONEVolt_DELAY_AFTER_STARTMODE;
                ThreeFiftyOhmRunModeDelay = ModifiedCatId[0].CalibrationDelaysPI[0].ONEVolt_DELAY_AFTER_RUNMODE;
                FourmAORTwentymAStartModeDelay = ModifiedCatId[0].CalibrationDelaysPI[0].FOURmA_DELAY_AFTER_STARTMODE;
                FourmAORTwentymARunModeDelay = ModifiedCatId[0].CalibrationDelaysPI[0].FOURmA_DELAY_AFTER_RUNMODE;
               
                OneVoltOrNineVoltStartModeDelay = ModifiedCatId[0].CalibrationDelaysPI[0].ONEVolt_DELAY_AFTER_STARTMODE;
                OneVoltOrNineVoltRunModeDelay = ModifiedCatId[0].CalibrationDelaysPI[0].ONEVolt_DELAY_AFTER_RUNMODE;
                AnalogOutputObservedValueDelay = ModifiedCatId[0].CalibrationDelaysPI[0].CALIB_MEASURE_DELAY;
            }
            catch (Exception)
            {

            }
        }

        internal CalibrationDelaysPI SaveCalibrationDelays()
        {
            try
            {
                CalibrationDelaysPI CalibConstdelays = new CalibrationDelaysPI()
                {
                    ONEmV_DELAY_AFTER_STARTMODE = OnemVOrFiftymVStartModeDelay,
                    ONEmV_DELAY_AFTER_RUNMODE = OnemVOrFiftymVRunModeDelay,
                    ONEVolt_DELAY_AFTER_STARTMODE = ThreeFiftyOhmStartModeDelay,
                    ONEVolt_DELAY_AFTER_RUNMODE = ThreeFiftyOhmRunModeDelay,
                    FOURmA_DELAY_AFTER_STARTMODE = FourmAORTwentymAStartModeDelay,
                    FOURmA_DELAY_AFTER_RUNMODE = FourmAORTwentymARunModeDelay,
                    PT100_DELAY_AFTER_RUNMODE = OneVoltOrNineVoltStartModeDelay,
                    PT100_DELAY_AFTER_STARTMODE = OneVoltOrNineVoltRunModeDelay,
                    CALIB_MEASURE_DELAY = AnalogOutputObservedValueDelay
                    
                };

                return CalibConstdelays;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
