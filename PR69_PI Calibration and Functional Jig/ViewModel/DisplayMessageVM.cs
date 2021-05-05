using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{
    public class DisplayMessageVM : INotifyPropertyChanged
    {

        private string _DisplayImgPath;

        public string DisplayImgPath
        {
            get { return _DisplayImgPath; }
            set { _DisplayImgPath = value; OnPropertyChanged("DisplayImgPath"); }
        }

        private string _MsgDescription;

        public string MsgDescription
        {
            get { return _MsgDescription; }
            set { _MsgDescription = value; OnPropertyChanged("MsgDescription"); }
        }

        private string _TitleImgMsg;

        public string TitleImgMsg
        {
            get { return _TitleImgMsg; }
            set { _TitleImgMsg = value; OnPropertyChanged("TitleImgMsg"); }
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
