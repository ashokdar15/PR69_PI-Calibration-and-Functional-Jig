using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsTotalConnectedDevices : INotifyPropertyChanged
    {

        private string BgColorgreen = "#43a047";
        private string BgColorred = "#e53935";

        private string Testrespass = "PASS";
        private string Testresfail = "FAIL";



        private int _TestNumber;

        public int TestNumber
        {
            get { return _TestNumber; }
            set { _TestNumber = value; OnPropertyChanged("TestNumber"); }
        }

        private string _TestresultDevice1BackColor;

        public string TestresultDevice1BackColor
        {
            get { return _TestresultDevice1BackColor; }
            set
            {
                _TestresultDevice1BackColor = value;       
                OnPropertyChanged("TestresultDevice1BackColor");
            }
        }

        private string _TestresultDevice2BackColor;

        public string TestresultDevice2BackColor
        {
            get { return _TestresultDevice2BackColor; }
            set
            {
                _TestresultDevice2BackColor = value;
                OnPropertyChanged("TestresultDevice2BackColor");
            }
        }

        private string _TestresultDevice3BackColor;

        public string TestresultDevice3BackColor
        {
            get { return _TestresultDevice3BackColor; }
            set
            {
                _TestresultDevice3BackColor = value;
                OnPropertyChanged("TestresultDevice3BackColor");
            }
        }

        private string _TestresultDevice4BackColor;

        public string TestresultDevice4BackColor
        {
            get { return _TestresultDevice4BackColor; }
            set
            {
                _TestresultDevice4BackColor = value;
                OnPropertyChanged("TestresultDevice4BackColor");
            }
        }

        private string _TestresultDevice5BackColor;

        public string TestresultDevice5BackColor
        {
            get { return _TestresultDevice5BackColor; }
            set
            {
                _TestresultDevice5BackColor = value;
                OnPropertyChanged("TestresultDevice5BackColor");
            }
        }

        private string _TestresultDevice6BackColor;

        public string TestresultDevice6BackColor
        {
            get { return _TestresultDevice6BackColor; }
            set
            {
                _TestresultDevice6BackColor = value;       
                OnPropertyChanged("TestresultDevice6BackColor");
            }
        }


        private string _TestresultDevice1;

        public string TestresultDevice1
        {
            get { return _TestresultDevice1; }
            set
            {
                _TestresultDevice1 = value;

                if (_TestresultDevice1 == Testrespass)
                {
                    TestresultDevice1BackColor = BgColorgreen;
                }
                else if (_TestresultDevice1 == Testresfail)
                {
                    TestresultDevice1BackColor = BgColorred;
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

                if (_TestresultDevice2 == Testrespass)
                {
                    TestresultDevice2BackColor = BgColorgreen;
                }
                else if (_TestresultDevice2 == Testresfail)
                {
                    TestresultDevice2BackColor = BgColorred;
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

                if (_TestresultDevice3 == Testrespass)
                {
                    TestresultDevice3BackColor = BgColorgreen;
                }
                else if (_TestresultDevice3 == Testresfail)
                {
                    TestresultDevice3BackColor = BgColorred;
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

                if (_TestresultDevice4 == Testrespass)
                {
                    TestresultDevice4BackColor = BgColorgreen;
                }
                else if (_TestresultDevice4 == Testresfail)
                {
                    TestresultDevice4BackColor = BgColorred;
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

                if (_TestresultDevice5 == Testrespass)
                {
                    TestresultDevice5BackColor = BgColorgreen;
                }
                else if (_TestresultDevice5 == Testresfail)
                {
                    TestresultDevice5BackColor = BgColorred;
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

                if (_TestresultDevice6 == Testrespass)
                {
                    TestresultDevice6BackColor = BgColorgreen;
                }
                else if (_TestresultDevice6 == Testresfail)
                {
                    TestresultDevice6BackColor = BgColorred;
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
