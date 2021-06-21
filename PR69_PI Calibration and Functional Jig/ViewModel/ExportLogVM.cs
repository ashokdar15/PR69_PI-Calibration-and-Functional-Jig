using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{
    public class ExportLogVM : INotifyPropertyChanged
    {
        public ExportLogVM()
        {
            btnExport = new RelayCommand(btnExportClk);
            FromSelecteddate = DateTime.Now;
            ToSelecteddate = DateTime.Now;
        }

        private DateTime _FromSelecteddate;

        public DateTime FromSelecteddate
        {
            get { return _FromSelecteddate; }
            set
            {
                _FromSelecteddate = value;
                if (_ToSelecteddate != null && _FromSelecteddate != null)
                {
                    btnExportEnable = true;
                }
                else
                    btnExportEnable = false;
                OnPropertyChanged("FromSelectedDate");
            }
        }

        private DateTime _ToSelecteddate;

        public DateTime ToSelecteddate
        {
            get
            {
                return _ToSelecteddate;
            }
            set
            {
                _ToSelecteddate = value;
                if (_ToSelecteddate != null && _FromSelecteddate != null)
                {
                    btnExportEnable = true;
                }
                else
                    btnExportEnable = false;
                OnPropertyChanged("ToSelectedDate");
            }
        }

        private bool _btnExportEnable;

        public bool btnExportEnable
        {
            get { return _btnExportEnable; }
            set { _btnExportEnable = value; OnPropertyChanged("btnExportEnable"); }
        }



        private void btnExportClk(object obj)
        {
            //List<clsLoggingData> getWholeData = clsLoggingData.getDataLog(FromSelecteddate,ToSelecteddate);


        }

        public RelayCommand btnExport { get; set; }

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
