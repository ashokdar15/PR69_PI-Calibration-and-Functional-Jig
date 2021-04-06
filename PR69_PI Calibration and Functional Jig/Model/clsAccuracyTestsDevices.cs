using System;
using System.Collections.Generic;
using System.ComponentModel;
using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsAccuracyTestsDevices : INotifyPropertyChanged
    {

        private string _backcolorDevice1;

        public string BackcolorDevice1
        {
            get { return _backcolorDevice1; }
            set { _backcolorDevice1 = value; OnPropertyChanged("BackcolorDevice1"); }
        }

        private string _backcolorDevice2;

        public string BackcolorDevice2
        {
            get { return _backcolorDevice2; }
            set { _backcolorDevice2 = value; OnPropertyChanged("BackcolorDevice2"); }
        }

        private string _BackcolorDevice3;

        public string BackcolorDevice3
        {
            get { return _BackcolorDevice3; }
            set { _BackcolorDevice3 = value; OnPropertyChanged("BackcolorDevice3"); }
        }

        private string _backcolorDevice4;

        public string BackcolorDevice4
        {
            get { return _backcolorDevice4; }
            set { _backcolorDevice4 = value; OnPropertyChanged("BackcolorDevice4"); }
        }

        private string _backcolorDevice5;

        public string BackcolorDevice5
        {
            get { return _backcolorDevice5; }
            set { _backcolorDevice5 = value; OnPropertyChanged("BackcolorDevice5"); }
        }

        private string _backcolorDevice6;

        public string BackcolorDevice6
        {
            get { return _backcolorDevice6; }
            set { _backcolorDevice6 = value; OnPropertyChanged("BackcolorDevice6"); }
        }

      
        private int _testnumber;

        public int Testnumber
        {
            get { return _testnumber; }
            set { _testnumber = value; }
        }

        private string _AccuracyParameter;

        public string AccuracyParameter
        {
            get { return _AccuracyParameter; }
            set { _AccuracyParameter = value; OnPropertyChanged("AccuracyParameter"); }
        }


        private string _TestPoint;

        public string TestPoint
        {
            get { return _TestPoint; }
            set
            {
                _TestPoint = value;
                OnPropertyChanged("TestPoint");
            }
        }
        
        private string _TestresultDevice1;

        public string TestresultDevice1
        {
            get { return _TestresultDevice1; }
            set
            {
                _TestresultDevice1 = value;

                switch (clsGlobalVariables.strAccuracyParameter)
                {
                    case clsGlobalVariables.AccuracyParameter.mAmp:
                        
                        break;
                    case clsGlobalVariables.AccuracyParameter.Volt:

                        break;
                    case clsGlobalVariables.AccuracyParameter.PT100Sensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.RSensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.JSensor:

                        break;
                    default:
                        break;
                }

                OnPropertyChanged("TestresultDevice1");
            }
        }

        private string _TestresultDevice2;

        public string TestresultDevice2
        {
            get { return _TestresultDevice2; }
            set
            {
                _TestresultDevice2 = value;

                switch (clsGlobalVariables.strAccuracyParameter)
                {
                    case clsGlobalVariables.AccuracyParameter.mAmp:

                        break;
                    case clsGlobalVariables.AccuracyParameter.Volt:

                        break;
                    case clsGlobalVariables.AccuracyParameter.PT100Sensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.RSensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.JSensor:

                        break;
                    default:
                        break;
                }

                OnPropertyChanged("TestresultDevice2");
            }
        }

        private string _TestresultDevice3;

        public string TestresultDevice3
        {
            get { return _TestresultDevice3; }
            set
            {
                _TestresultDevice3 = value;

                switch (clsGlobalVariables.strAccuracyParameter)
                {
                    case clsGlobalVariables.AccuracyParameter.mAmp:

                        break;
                    case clsGlobalVariables.AccuracyParameter.Volt:

                        break;
                    case clsGlobalVariables.AccuracyParameter.PT100Sensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.RSensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.JSensor:

                        break;
                    default:
                        break;
                }

                OnPropertyChanged("TestresultDevice3");
            }
        }

        private string _TestresultDevice4;

        public string TestresultDevice4
        {
            get { return _TestresultDevice4; }
            set
            {
                _TestresultDevice4 = value;

                switch (clsGlobalVariables.strAccuracyParameter)
                {
                    case clsGlobalVariables.AccuracyParameter.mAmp:

                        break;
                    case clsGlobalVariables.AccuracyParameter.Volt:

                        break;
                    case clsGlobalVariables.AccuracyParameter.PT100Sensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.RSensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.JSensor:

                        break;
                    default:
                        break;
                }

                OnPropertyChanged("TestresultDevice4");
            }
        }

        private string _TestresultDevice5;

        public string TestresultDevice5
        {
            get { return _TestresultDevice5; }
            set
            {
                _TestresultDevice5 = value;

                switch (clsGlobalVariables.strAccuracyParameter)
                {
                    case clsGlobalVariables.AccuracyParameter.mAmp:

                        break;
                    case clsGlobalVariables.AccuracyParameter.Volt:

                        break;
                    case clsGlobalVariables.AccuracyParameter.PT100Sensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.RSensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.JSensor:

                        break;
                    default:
                        break;
                }

                OnPropertyChanged("TestresultDevice5");
            }
        }

        private string _TestresultDevice6;

        public string TestresultDevice6
        {
            get { return _TestresultDevice6; }
            set
            {
                _TestresultDevice6 = value;

                switch (clsGlobalVariables.strAccuracyParameter)
                {
                    case clsGlobalVariables.AccuracyParameter.mAmp:

                        break;
                    case clsGlobalVariables.AccuracyParameter.Volt:

                        break;
                    case clsGlobalVariables.AccuracyParameter.PT100Sensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.RSensor:

                        break;
                    case clsGlobalVariables.AccuracyParameter.JSensor:

                        break;
                    default:
                        break;
                }

                OnPropertyChanged("TestresultDevice6");
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
