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

        private string _Test;

        public string Test
        {
            get { return _Test; }
            set { _Test = value; OnPropertyChanged("Test"); }
        }

        private string _TestResult;

        public string TestResult
        {
            get { return _TestResult; }
            set { _TestResult = value; OnPropertyChanged("TestResult"); }
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
