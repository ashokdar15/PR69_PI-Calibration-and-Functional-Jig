using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsTotalTestsGroups : INotifyPropertyChanged
    {

        private int _TestNumber;

        public int TestNumber
        {
            get { return _TestNumber; }
            set { _TestNumber = value; OnPropertyChanged("TestNumber"); }
        }

        private string _TestGroup;

        public string TestGroup
        {
            get { return _TestGroup; }
            set { _TestGroup = value; OnPropertyChanged("TestGroup"); }
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
