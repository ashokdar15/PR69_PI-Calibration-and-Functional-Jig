using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PR69_PI_Calibration_and_Functional_Jig.HelperClasses.clsGlobalVariables;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsAccuracyTests : INotifyPropertyChanged
    {

        private string _NumberTestPoints;

        public string NumberTestPoints
        {
            get { return _NumberTestPoints; }
            set
            {
                int numberofTestpts = 0;
                _NumberTestPoints = value;

                P1 = "";
                P2 = "";
                P3 = "";
                P4 = "";
                P5 = "";
                P6 = "";
                P7 = "";
                P8 = "";
                P9 = "";
                P10 = "";

                try
                {
                    numberofTestpts = Convert.ToInt32(_NumberTestPoints);
                }
                catch (Exception)
                {

                    numberofTestpts = 0;
                }               
                switch (numberofTestpts)
                {
                    case 10:
                        VisP1 = true;
                        VisP2 = true;
                        VisP3 = true;
                        VisP4 = true;
                        VisP5 = true;
                        VisP6 = true;
                        VisP7 = true;
                        VisP8 = true;
                        VisP9 = true;
                        VisP10 = true;
                        break;
                    case 9:
                        VisP1 = true;
                        VisP2 = true;
                        VisP3 = true;
                        VisP4 = true;
                        VisP5 = true;
                        VisP6 = true;
                        VisP7 = true;
                        VisP8 = true;
                        VisP9 = true;
                        VisP10 = false;
                        break;

                    case 8:
                        VisP1 = true;
                        VisP2 = true;
                        VisP3 = true;
                        VisP4 = true;
                        VisP5 = true;
                        VisP6 = true;
                        VisP7 = true;
                        VisP8 = true;
                        VisP9 = false;
                        VisP10 = false;
                        break;

                    case 7:
                        VisP1 = true;
                        VisP2 = true;
                        VisP3 = true;
                        VisP4 = true;
                        VisP5 = true;
                        VisP6 = true;
                        VisP7 = true;
                        VisP8 = false;
                        VisP9 = false;
                        VisP10 = false;
                        break;

                    case 6:
                        VisP1 = true;
                        VisP2 = true;
                        VisP3 = true;
                        VisP4 = true;
                        VisP5 = true;
                        VisP6 = true;
                        VisP7 = false;
                        VisP8 = false;
                        VisP9 = false;
                        VisP10 = false;
                        break;

                    case 5:
                        VisP1 = true;
                        VisP2 = true;
                        VisP3 = true;
                        VisP4 = true;
                        VisP5 = true;
                        VisP6 = false;
                        VisP7 = false;
                        VisP8 = false;
                        VisP9 = false;
                        VisP10 = false;
                        break;

                    case 4:
                        VisP1 = true;
                        VisP2 = true;
                        VisP3 = true;
                        VisP4 = true;
                        VisP5 = false;
                        VisP6 = false;
                        VisP7 = false;
                        VisP8 = false;
                        VisP9 = false;
                        VisP10 = false;
                        break;

                    case 3:
                        VisP1 = true;
                        VisP2 = true;
                        VisP3 = true;
                        VisP4 = false;
                        VisP5 = false;
                        VisP6 = false;
                        VisP7 = false;
                        VisP8 = false;
                        VisP9 = false;
                        VisP10 = false;
                        break;

                    case 2:
                        VisP1 = true;
                        VisP2 = true;
                        VisP3 = false;
                        VisP4 = false;
                        VisP5 = false;
                        VisP6 = false;
                        VisP7 = false;
                        VisP8 = false;
                        VisP9 = false;
                        VisP10 = false;
                        break;

                    case 1:
                        VisP1 = true;
                        VisP2 = false;
                        VisP3 = false;
                        VisP4 = false;
                        VisP5 = false;
                        VisP6 = false;
                        VisP7 = false;
                        VisP8 = false;
                        VisP9 = false;
                        VisP10 = false;
                        break;


                    default:
                        VisP1 = false;
                        VisP2 = false;
                        VisP3 = false;
                        VisP4 = false;
                        VisP5 = false;
                        VisP6 = false;
                        VisP7 = false;
                        VisP8 = false;
                        VisP9 = false;
                        VisP10 = false;
                        break;
                }
                

                OnPropertyChanged("NumberTestPoints");
            }
        }

        private string _Min;

        public string Min
        {
            get { return _Min; }
            set { _Min = value; OnPropertyChanged("Min"); }
        }

        private string _Max;

        public string Max
        {
            get { return _Max; }
            set { _Max = value; OnPropertyChanged("Max"); }
        }

        private string _Tolerance;

        public string Tolerance
        {
            get { return _Tolerance; }
            set { _Tolerance = value; OnPropertyChanged("Tolerance"); }
        }

        private string _P1;

        public string P1
        {
            get { return _P1; }
            set { _P1 = value; OnPropertyChanged("P1"); }
        }

        private string _P2;

        public string P2
        {
            get { return _P2; }
            set { _P2 = value; OnPropertyChanged("P2"); }
        }

        private string _P3;

        public string P3
        {
            get { return _P3; }
            set { _P3 = value; OnPropertyChanged("P3"); }
        }

        private string _P4;

        public string P4
        {
            get { return _P4; }
            set { _P4 = value; OnPropertyChanged("P4"); }
        }

        private string _P5;

        public string P5
        {
            get { return _P5; }
            set { _P5 = value; OnPropertyChanged("P5"); }
        }

        private string _P6;

        public string P6
        {
            get { return _P6; }
            set { _P6 = value; OnPropertyChanged("P6"); }
        }

        private string _P7;

        public string P7
        {
            get { return _P7; }
            set { _P7 = value; OnPropertyChanged("P7"); }
        }

        private string _P8;

        public string P8
        {
            get { return _P8; }
            set { _P8 = value; OnPropertyChanged("P8"); }
        }

        private string _P9;

        public string P9
        {
            get { return _P9; }
            set { _P9 = value; OnPropertyChanged("P9"); }
        }
        private string _P10;

        public string P10
        {
            get { return _P10; }
            set { _P10 = value; OnPropertyChanged("P10"); }
        }

        private bool _VisP1;

        public bool VisP1
        {
            get { return _VisP1; }
            set { _VisP1 = value; OnPropertyChanged("VisP1"); }
        }

        private bool _VisP2;

        public bool VisP2
        {
            get { return _VisP2; }
            set { _VisP2 = value; OnPropertyChanged("VisP2"); }
        }

        private bool _VisP3;

        public bool VisP3
        {
            get { return _VisP3; }
            set { _VisP3 = value; OnPropertyChanged("VisP3"); }
        }

        private bool _VisP4;

        public bool VisP4
        {
            get { return _VisP4; }
            set { _VisP4 = value; OnPropertyChanged("VisP4"); }
        }

        private bool _VisP5;

        public bool VisP5
        {
            get { return _VisP5; }
            set { _VisP5 = value; OnPropertyChanged("VisP5"); }
        }

        private bool _VisP6;

        public bool VisP6
        {
            get { return _VisP6; }
            set { _VisP6 = value; OnPropertyChanged("VisP6"); }
        }

        private bool _VisP7;

        public bool VisP7
        {
            get { return _VisP7; }
            set { _VisP7 = value; OnPropertyChanged("VisP7"); }
        }

        private bool _VisP8;

        public bool VisP8
        {
            get { return _VisP8; }
            set { _VisP8 = value; OnPropertyChanged("VisP8"); }
        }



        private bool _VisP9;

        public bool VisP9
        {
            get { return _VisP9; }
            set { _VisP9 = value; OnPropertyChanged("VisP9"); }
        }


        private bool _VisP10;

        public bool VisP10
        {
            get { return _VisP10; }
            set { _VisP10 = value; OnPropertyChanged("VisP10"); }
        }


        internal void ParseAccuracyDetails(CatIdList catId, AccuracyParameter Input)
        {
            try
            {
                switch (Input)
                {
                    case AccuracyParameter.mAmp:
                        if (catId.mAmpTests != null)
                        {
                            NumberTestPoints = catId.mAmpTests[0].NumberTestPoints;
                            Min = catId.mAmpTests[0].Min;
                            Max = catId.mAmpTests[0].Max;
                            Tolerance = catId.mAmpTests[0].Tolerance;
                            P1 = catId.mAmpTests[0].P1;
                            P2 = catId.mAmpTests[0].P2;
                            P3 = catId.mAmpTests[0].P3;
                            P4 = catId.mAmpTests[0].P4;
                            P5 = catId.mAmpTests[0].P5;
                            P6 = catId.mAmpTests[0].P6;
                            P7 = catId.mAmpTests[0].P7;
                            P8 = catId.mAmpTests[0].P8;
                            P9 = catId.mAmpTests[0].P9;
                            P10 = catId.mAmpTests[0].P10;
                        }
                        break;

                    case AccuracyParameter.Volt:
                        if (catId.VoltTests != null)
                        {
                            NumberTestPoints = catId.VoltTests[0].NumberTestPoints;
                            Min = catId.VoltTests[0].Min;
                            Max = catId.VoltTests[0].Max;
                            Tolerance = catId.VoltTests[0].Tolerance;
                            P1 = catId.VoltTests[0].P1;
                            P2 = catId.VoltTests[0].P2;
                            P3 = catId.VoltTests[0].P3;
                            P4 = catId.VoltTests[0].P4;
                            P5 = catId.VoltTests[0].P5;
                            P6 = catId.VoltTests[0].P6;
                            P7 = catId.VoltTests[0].P7;
                            P8 = catId.VoltTests[0].P8;
                            P9 = catId.VoltTests[0].P9;
                            P10 = catId.VoltTests[0].P10;
                        }
                        break;

                    case AccuracyParameter.PT100Sensor:
                        if (catId.PT100SensorTests != null)
                        {
                            NumberTestPoints = catId.PT100SensorTests[0].NumberTestPoints;
                            Min = catId.PT100SensorTests[0].Min;
                            Max = catId.PT100SensorTests[0].Max;
                            Tolerance = catId.PT100SensorTests[0].Tolerance;
                            P1 = catId.PT100SensorTests[0].P1;
                            P2 = catId.PT100SensorTests[0].P2;
                            P3 = catId.PT100SensorTests[0].P3;
                            P4 = catId.PT100SensorTests[0].P4;
                            P5 = catId.PT100SensorTests[0].P5;
                            P6 = catId.PT100SensorTests[0].P6;
                            P7 = catId.PT100SensorTests[0].P7;
                            P8 = catId.PT100SensorTests[0].P8;
                            P9 = catId.PT100SensorTests[0].P9;
                            P10 = catId.PT100SensorTests[0].P10;
                        }
                        break;

                    case AccuracyParameter.RSensor:
                        if (catId.RSensor != null)
                        {
                            NumberTestPoints = catId.RSensor[0].NumberTestPoints;
                            Min = catId.RSensor[0].Min;
                            Max = catId.RSensor[0].Max;
                            Tolerance = catId.RSensor[0].Tolerance;
                            P1 = catId.RSensor[0].P1;
                            P2 = catId.RSensor[0].P2;
                            P3 = catId.RSensor[0].P3;
                            P4 = catId.RSensor[0].P4;
                            P5 = catId.RSensor[0].P5;
                            P6 = catId.RSensor[0].P6;
                            P7 = catId.RSensor[0].P7;
                            P8 = catId.RSensor[0].P8;
                            P9 = catId.RSensor[0].P9;
                            P10 = catId.RSensor[0].P10;
                        }
                        break;

                    case AccuracyParameter.JSensor:
                        if (catId.JSensor != null)
                        {
                            NumberTestPoints = catId.JSensor[0].NumberTestPoints;
                            Min = catId.JSensor[0].Min;
                            Max = catId.JSensor[0].Max;
                            Tolerance = catId.JSensor[0].Tolerance;
                            P1 = catId.JSensor[0].P1;
                            P2 = catId.JSensor[0].P2;
                            P3 = catId.JSensor[0].P3;
                            P4 = catId.JSensor[0].P4;
                            P5 = catId.JSensor[0].P5;
                            P6 = catId.JSensor[0].P6;
                            P7 = catId.JSensor[0].P7;
                            P8 = catId.JSensor[0].P8;
                            P9 = catId.JSensor[0].P9;
                            P10 = catId.JSensor[0].P10;
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception )
            {
                
            }
        }

        public AccuracyTests SaveAccuracyTestsTests()
        {
            try
            {
                AccuracyTests accuracyTests = new AccuracyTests
                {
                    NumberTestPoints = NumberTestPoints,
                    Max = Max,
                    Min = Min,
                    Tolerance = Tolerance,
                    P1 = P1,
                    P2 = P2,
                    P3 = P3,
                    P4 = P4,
                    P5 = P5,
                    P6 = P6,
                    P7 = P7,
                    P8 = P8,
                    P9 = P9,
                    P10 = P10
                };

                return accuracyTests;
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
