using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{
    public class ProgrammingWindowVM : INotifyPropertyChanged
    {
        public ProgrammingWindowVM()
        {
            StartProgramBtnVis = true;

            _btnStartProgram = new RelayCommand(btnStartProgramClk);
        }

        private void btnStartProgramClk(object obj)
        {
            
        }

        private RelayCommand _btnStartProgram;

        public RelayCommand btnStartProgram
        {
            get { return _btnStartProgram; }
            set { _btnStartProgram = value; }
        }


        private string _strtestReport;

        public string strtestReport
        {
            get { return _strtestReport; }
            set { _strtestReport = value; OnPropertyChanged("strtestReport"); }
        }

        private bool _StartBtnVis;

        public bool StartProgramBtnVis
        {
            get { return _StartBtnVis; }
            set { _StartBtnVis = value; OnPropertyChanged("StartProgramBtnVis"); }
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
