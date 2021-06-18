using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
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
        }

        private void btnExportClk(object obj)
        {
            
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
