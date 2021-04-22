using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{
    public class ProgrammingWindowVM : INotifyPropertyChanged
    {
        public ProgrammingWindowVM()
        {
            StartProgramBtnVis = true;
            strtestReport = "Start Program";
            _btnStartProgram = new RelayCommand(btnStartProgramClk);
        }

        private async void btnStartProgramClk(object obj)
        {
            for (int i = 0; i < 10; i++)
            {
                CurrentValue += 10;
                StatusInPercentage = CurrentValue;
                await Task.Delay(1000);
            }
        }

        private RelayCommand _btnStartProgram;

        public RelayCommand btnStartProgram
        {
            get { return _btnStartProgram; }
            set { _btnStartProgram = value; }
        }

        private int _StatusInPercentage;

        public int StatusInPercentage
        {
            get { return _StatusInPercentage; }
            set { _StatusInPercentage = value; OnPropertyChanged("StatusInPercentage"); }
        }
        
        private int _CurrentValue;

        public int CurrentValue
        {
            get { return _CurrentValue; }
            set { _CurrentValue = value; OnPropertyChanged("CurrentValue"); }
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
