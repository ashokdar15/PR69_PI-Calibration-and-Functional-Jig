using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsCalibrationDelays : INotifyPropertyChanged
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

        private int _VREFReadDelayStartMode;

        public int VREFReadDelayStartMode
        {
            get { return _VREFReadDelayStartMode; }
            set { _VREFReadDelayStartMode = value; OnPropertyChanged("VREFReadDelayStartMode"); }
        }

        private int _VREFReadDelayRunMode;

        public int VREFReadDelayRunMode
        {
            get { return _VREFReadDelayRunMode; }
            set { _VREFReadDelayRunMode = value; OnPropertyChanged("VREFReadDelayRunMode"); }
        }

        public void ParseCalibrationDelays(ObservableCollection<ConfigurationDataList> ModifiedCatId)
        {
            try
            {
                OnemVOrFiftymVStartModeDelay = ModifiedCatId[0].CalibrationDelays[0].OnemVOrFiftymVStartModeDelay;
                OnemVOrFiftymVRunModeDelay = ModifiedCatId[0].CalibrationDelays[0].OnemVOrFiftymVRunModeDelay;
                ThreeFiftyOhmStartModeDelay = ModifiedCatId[0].CalibrationDelays[0].ThreeFiftyOhmStartModeDelay;
                ThreeFiftyOhmRunModeDelay = ModifiedCatId[0].CalibrationDelays[0].ThreeFiftyOhmRunModeDelay;
                FourmAORTwentymAStartModeDelay = ModifiedCatId[0].CalibrationDelays[0].FourmAORTwentymAStartModeDelay;
                FourmAORTwentymARunModeDelay = ModifiedCatId[0].CalibrationDelays[0].FourmAORTwentymARunModeDelay;
                VREFReadDelayStartMode = ModifiedCatId[0].CalibrationDelays[0].VREFReadDelayStartMode;
                VREFReadDelayRunMode = ModifiedCatId[0].CalibrationDelays[0].VREFReadDelayRunMode;
                OneVoltOrNineVoltStartModeDelay = ModifiedCatId[0].CalibrationDelays[0].OneVoltOrNineVoltStartModeDelay;
                OneVoltOrNineVoltRunModeDelay = ModifiedCatId[0].CalibrationDelays[0].OneVoltOrNineVoltRunModeDelay;
                AnalogOutputObservedValueDelay = ModifiedCatId[0].CalibrationDelays[0].AnalogOutputObservedValueDelay;
            }
            catch (Exception)
            {

            }
        }

        internal CalibrationDelays SaveCalibrationDelays()
        {
            try
            {
                CalibrationDelays CalibConstdelays = new CalibrationDelays()
                {
                    OnemVOrFiftymVStartModeDelay = OnemVOrFiftymVStartModeDelay,
                    OnemVOrFiftymVRunModeDelay = OnemVOrFiftymVRunModeDelay,
                    ThreeFiftyOhmStartModeDelay =ThreeFiftyOhmStartModeDelay,
                    ThreeFiftyOhmRunModeDelay = ThreeFiftyOhmRunModeDelay,
                    FourmAORTwentymAStartModeDelay = FourmAORTwentymAStartModeDelay,
                    FourmAORTwentymARunModeDelay = FourmAORTwentymARunModeDelay,
                    OneVoltOrNineVoltStartModeDelay = OneVoltOrNineVoltStartModeDelay,
                    OneVoltOrNineVoltRunModeDelay = OneVoltOrNineVoltRunModeDelay,
                    AnalogOutputObservedValueDelay = AnalogOutputObservedValueDelay,
                    VREFReadDelayStartMode = VREFReadDelayStartMode,
                    VREFReadDelayRunMode = VREFReadDelayRunMode
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
