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
        
        private int _TestNumber;

        public int TestNumber
        {
            get { return _TestNumber; }
            set { _TestNumber = value; OnPropertyChanged("TestNumber"); }
        }

        private int _DeviceNumber;

        public int DeviceNumber
        {
            get { return _DeviceNumber; }
            set { _DeviceNumber = value; OnPropertyChanged("DeviceNumber"); }
        }

        private string _TestresultDevice1;

        public string TestresultDevice1
        {
            get { return _TestresultDevice1; }
            set { _TestresultDevice1 = value; OnPropertyChanged("TestresultDevice1"); }
        }

        private string _TestresultDevice2;

        public string TestresultDevice2
        {
            get { return _TestresultDevice2; }
            set { _TestresultDevice2 = value; OnPropertyChanged("TestresultDevice2"); }
        }

        private string _TestresultDevice3;

        public string TestresultDevice3
        {
            get { return _TestresultDevice3; }
            set { _TestresultDevice3 = value; OnPropertyChanged("TestresultDevice3"); }
        }

        private string _TestresultDevice4;

        public string TestresultDevice4
        {
            get { return _TestresultDevice4; }
            set { _TestresultDevice4 = value; OnPropertyChanged("TestresultDevice4"); }
        }

        private string _TestresultDevice5;

        public string TestresultDevice5
        {
            get { return _TestresultDevice5; }
            set { _TestresultDevice5 = value; OnPropertyChanged("TestresultDevice5"); }
        }

        private string _TestresultDevice6;

        public string TestresultDevice6
        {
            get { return _TestresultDevice6; }
            set { _TestresultDevice6 = value; OnPropertyChanged("TestresultDevice6"); }
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
