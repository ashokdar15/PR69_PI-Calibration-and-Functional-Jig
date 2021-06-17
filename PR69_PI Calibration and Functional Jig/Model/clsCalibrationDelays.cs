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
                OnemVOrFiftymVStartModeDelay = ModifiedCatId[0].CalibrationDelays[0].ONEmV_DELAY_AFTER_STARTMODE;
                OnemVOrFiftymVRunModeDelay = ModifiedCatId[0].CalibrationDelays[0].ONEmV_DELAY_AFTER_RUNMODE;
                ThreeFiftyOhmStartModeDelay = ModifiedCatId[0].CalibrationDelays[0].PT100_DELAY_AFTER_STARTMODE;
                ThreeFiftyOhmRunModeDelay = ModifiedCatId[0].CalibrationDelays[0].PT100_DELAY_AFTER_RUNMODE;
                FourmAORTwentymAStartModeDelay = ModifiedCatId[0].CalibrationDelays[0].FOURmA_DELAY_AFTER_STARTMODE;
                FourmAORTwentymARunModeDelay = ModifiedCatId[0].CalibrationDelays[0].FOURmA_DELAY_AFTER_RUNMODE;
                VREFReadDelayStartMode = ModifiedCatId[0].CalibrationDelays[0].VREF_READ_DELAY_STARTMODE;
                VREFReadDelayRunMode = ModifiedCatId[0].CalibrationDelays[0].VREF_READ_DELAY_RUNMODE;
                OneVoltOrNineVoltStartModeDelay = ModifiedCatId[0].CalibrationDelays[0].ONEVolt_DELAY_AFTER_STARTMODE;
                OneVoltOrNineVoltRunModeDelay = ModifiedCatId[0].CalibrationDelays[0].ONEVolt_DELAY_AFTER_RUNMODE;
                AnalogOutputObservedValueDelay = ModifiedCatId[0].CalibrationDelays[0].CALIB_MEASURE_DELAY;
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
                    ONEmV_DELAY_AFTER_STARTMODE = OnemVOrFiftymVStartModeDelay,
                    ONEmV_DELAY_AFTER_RUNMODE = OnemVOrFiftymVRunModeDelay,
                    PT100_DELAY_AFTER_STARTMODE = ThreeFiftyOhmStartModeDelay,
                    PT100_DELAY_AFTER_RUNMODE = ThreeFiftyOhmRunModeDelay,
                    FOURmA_DELAY_AFTER_STARTMODE = FourmAORTwentymAStartModeDelay,
                    FOURmA_DELAY_AFTER_RUNMODE = FourmAORTwentymARunModeDelay,
                    ONEVolt_DELAY_AFTER_STARTMODE = OneVoltOrNineVoltStartModeDelay,
                    ONEVolt_DELAY_AFTER_RUNMODE = OneVoltOrNineVoltRunModeDelay,
                    CALIB_MEASURE_DELAY = AnalogOutputObservedValueDelay,
                    VREF_READ_DELAY_STARTMODE = VREFReadDelayStartMode,
                    VREF_READ_DELAY_RUNMODE = VREFReadDelayRunMode
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

    public class clsCalibrationDelaysPR43 : INotifyPropertyChanged
    {
        private int _OnemVOrFiftymVStartModeDelay;

        public int PT100_Start_Delay
        {
            get { return _OnemVOrFiftymVStartModeDelay; }
            set { _OnemVOrFiftymVStartModeDelay = value; OnPropertyChanged("PT100_Start_Delay"); }
        }

        private int _OnemVOrFiftymVRunModeDelay;

        public int PT100_Run_Delay
        {
            get { return _OnemVOrFiftymVRunModeDelay; }
            set { _OnemVOrFiftymVRunModeDelay = value; OnPropertyChanged("PT100_Run_Delay"); }
        }

        private int _ThreeFiftyOhmStartModeDelay;

        public int PT313_Start_Delay
        {
            get { return _ThreeFiftyOhmStartModeDelay; }
            set { _ThreeFiftyOhmStartModeDelay = value; OnPropertyChanged("PT313_Start_Delay"); }
        }

        private int _ThreeFiftyOhmRunModeDelay;

        public int PT313_Run_Delay
        {
            get { return _ThreeFiftyOhmRunModeDelay; }
            set { _ThreeFiftyOhmRunModeDelay = value; OnPropertyChanged("PT313_Run_Delay"); }
        }

        
        public void ParseCalibrationDelays(ObservableCollection<ConfigurationDataList> ModifiedCatId)
        {
            try
            {
                PT100_Start_Delay = ModifiedCatId[0].CalibrationDelaysPR43[0].PT100_PR43_DELAY_AFTER_RUNMODE;
                PT100_Run_Delay = ModifiedCatId[0].CalibrationDelaysPR43[0].PT100_PR43_DELAY_AFTER_STARTMODE;
                PT313_Start_Delay = ModifiedCatId[0].CalibrationDelaysPR43[0].PT313_DELAY_AFTER_RUNMODE;
                PT313_Run_Delay = ModifiedCatId[0].CalibrationDelaysPR43[0].PT313_DELAY_AFTER_STARTMODE;
                
            }
            catch (Exception)
            {

            }
        }

        internal CalibrationDelaysPR43 SaveCalibrationDelays()
        {
            try
            {
                CalibrationDelaysPR43 CalibConstdelays = new CalibrationDelaysPR43()
                {
                    PT100_PR43_DELAY_AFTER_RUNMODE = PT100_Run_Delay,
                    PT100_PR43_DELAY_AFTER_STARTMODE = PT100_Start_Delay,
                    PT313_DELAY_AFTER_RUNMODE = PT313_Run_Delay,
                    PT313_DELAY_AFTER_STARTMODE = PT313_Start_Delay,
                   
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
