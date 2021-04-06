using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsRelayORSSRTests : INotifyPropertyChanged
    {
        private bool _OP1;

        public bool OP1
        {
            get { return _OP1; }
            set { _OP1 = value; OnPropertyChanged("OP1"); }
        }

        private bool _OP2;

        public bool OP2
        {
            get { return _OP2; }
            set { _OP2 = value; OnPropertyChanged("OP2"); }
        }

        private bool _OP3;

        public bool OP3
        {
            get { return _OP3; }
            set { _OP3 = value; OnPropertyChanged("OP3"); }
        }

        private string _SelectedOP1Type;

        public string SelectedOP1Type
        {
            get { return _SelectedOP1Type; }
            set
            {
                _SelectedOP1Type = value;

                if (_SelectedOP1Type == "Relay")                
                    IsOP1RelaySelected = true;                
                else
                {
                    IsOP1RelaySelected = false;
                    SelectedIndexOP1Relay = -1;
                }                   

                OnPropertyChanged("SelectedOP1Type");
            }
        }

        private string _SelectedOP2Type;

        public string SelectedOP2Type
        {
            get { return _SelectedOP2Type; }
            set { _SelectedOP2Type = value;

                if (_SelectedOP2Type == "Relay")
                    IsOP2RelaySelected = true;
                else
                {
                    SelectedIndexOP2Relay = -1;
                    IsOP2RelaySelected = false;
                }
                    
                OnPropertyChanged("SelectedOP2Type"); }
        }

        private string _SelectedOP3Type;

        public string SelectedOP3Type
        {
            get { return _SelectedOP3Type; }
            set { _SelectedOP3Type = value;

                if (_SelectedOP3Type == "Relay")
                    IsOP3RelaySelected = true;
                else
                {
                    SelectedIndexOP3Relay = -1;
                    IsOP3RelaySelected = false;
                }
                    

                OnPropertyChanged("SelectedOP3Type"); }
        }

        private string _SelectedOP1RelayType;

        public string SelectedOP1RelayType
        {
            get { return _SelectedOP1RelayType; }
            set { _SelectedOP1RelayType = value; OnPropertyChanged("SelectedOP1RelayType"); }
        }
        private string _SelectedOP2RelayType;

        public string SelectedOP2RelayType
        {
            get { return _SelectedOP2RelayType; }
            set { _SelectedOP2RelayType = value; OnPropertyChanged("SelectedOP2RelayType"); }
        }

        private string _SelectedOP3RelayType;

        public string SelectedOP3RelayType
        {
            get { return _SelectedOP3RelayType; }
            set { _SelectedOP3RelayType = value; OnPropertyChanged("SelectedOP3RelayType"); }
        }

        private bool _IsOP1RelaySelected;

        public bool IsOP1RelaySelected
        {
            get { return _IsOP1RelaySelected; }
            set { _IsOP1RelaySelected = value; OnPropertyChanged("IsOP1RelaySelected"); }
        }

        private bool _IsOP2RelaySelected;

        public bool IsOP2RelaySelected
        {
            get { return _IsOP2RelaySelected; }
            set { _IsOP2RelaySelected = value; OnPropertyChanged("IsOP2RelaySelected"); }
        }

        private bool _IsOP3RelaySelected;

        public bool IsOP3RelaySelected
        {
            get { return _IsOP3RelaySelected; }
            set { _IsOP3RelaySelected = value; OnPropertyChanged("IsOP3RelaySelected"); }
        }

        private int _SelectedIndexOP1Relay;

        public int SelectedIndexOP1Relay
        {
            get { return _SelectedIndexOP1Relay; }
            set { _SelectedIndexOP1Relay = value; OnPropertyChanged("SelectedIndexOP1Relay"); }
        }

        private int _SelectedIndexOP2Relay;

        public int SelectedIndexOP2Relay
        {
            get { return _SelectedIndexOP2Relay; }
            set { _SelectedIndexOP2Relay = value; OnPropertyChanged("SelectedIndexOP2Relay"); }
        }

        private int _SelectedIndexOP3Relay;

        public int SelectedIndexOP3Relay
        {
            get { return _SelectedIndexOP3Relay; }
            set { _SelectedIndexOP3Relay = value; OnPropertyChanged("SelectedIndexOP3Relay"); }
        }

        public void ParseRelayOrSSRDetails(CatIdList catId)
        {
            if (catId.RelayOrSSRTests != null)
            {
                if (catId.RelayOrSSRTests.Count != 0)
                {
                    OP1 = catId.RelayOrSSRTests[0].OP1;
                    OP2 = catId.RelayOrSSRTests[0].OP2;
                    OP3 = catId.RelayOrSSRTests[0].OP3;

                    SelectedOP1Type = catId.RelayOrSSRTests[0].SelectedOP1Type;
                    SelectedOP2Type = catId.RelayOrSSRTests[0].SelectedOP2Type;
                    SelectedOP3Type = catId.RelayOrSSRTests[0].SelectedOP3Type;

                    SelectedOP1RelayType = catId.RelayOrSSRTests[0].SelectedOP1RelayType;
                    SelectedOP2RelayType = catId.RelayOrSSRTests[0].SelectedOP2RelayType;
                    SelectedOP3RelayType = catId.RelayOrSSRTests[0].SelectedOP3RelayType;

                }
            }
        }

        public RelayORSSRTests SaveRelayOrSSRTests()
        {
            try
            {
                RelayORSSRTests RelayOrSSRTests = new RelayORSSRTests()
                {
                    OP1=OP1,
                    OP2=OP2,
                    OP3=OP3,
                    SelectedOP1Type=SelectedOP1Type,
                    SelectedOP2Type=SelectedOP2Type,
                    SelectedOP3Type=SelectedOP3Type,
                    SelectedOP1RelayType=SelectedOP1RelayType,
                    SelectedOP2RelayType=SelectedOP2RelayType,
                    SelectedOP3RelayType=SelectedOP3RelayType
                };

                return RelayOrSSRTests;
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
