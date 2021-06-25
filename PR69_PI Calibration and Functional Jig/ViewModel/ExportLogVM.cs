using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            clsGlobalVariables.DataLogStatus status = clsLoggingData.getDataLog(FromSelecteddate,ToSelecteddate);
            if (status == clsGlobalVariables.DataLogStatus.Valid)
            {

            }

        }

        public void CreateLogRangePotCalibration(CatIdList catID)
        {
            try
            {
                string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");  //
                string CurrentTime = DateTime.Now.ToString("h:mm:ss tt");  //
                sbpotData.Clear();
                sbpotData.AppendLine();

                sbpotData.AppendLine("Product Name : " + clsGlobalVariables.Selectedcatid.DeviceName);
                sbpotData.AppendLine("Creation Date : " + CurrentDate);
                sbpotData.AppendLine("Creation Time : " + CurrentTime);
                sbpotData.AppendLine();
               

                sbpotData.AppendFormat("CurrentDateTime");

                //if (catID.ControllerType == "G10")
                //{
                //    switch (potcount)
                //    {
                //        case (int)EnmNumberofpots.One:
                //            sbpotData.AppendFormat(", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10");
                //            break;
                //        case (int)EnmNumberofpots.Two:
                //            sbpotData.AppendFormat(", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10" + ", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10");
                //            break;
                //        case (int)EnmNumberofpots.Three:
                //            sbpotData.AppendFormat(", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10" + ", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10" + ", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10");
                //            break;
                //        case (int)EnmNumberofpots.Four:
                //            sbpotData.AppendFormat(", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10" + ", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10" + ", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10" + ", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10");
                //            break;
                //        default:
                //            break;
                //    }
                //}
                //else if (catID.ControllerType == "G13" || catID.ControllerType == "G12")
                //{
                //    for (int cnt = 0; cnt < potcount; cnt++)
                //    {
                //        if (catID.Micon175_PotDetail[cnt].PotName == "Mode" || catID.Micon175_PotDetail[cnt].PotName == "Timing2")
                //        {
                //            sbpotData.AppendFormat(", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10" + "," + "Pos11" + "," + "Pos12" + "," + "Pos13" + "," + "Pos14" + "," + "Pos15");
                //        }
                //        else
                //        {
                //            sbpotData.AppendFormat(", ," + "Pot Type" + "," + "Pos1" + "," + "Pos2" + "," + "Pos3" + "," + "Pos4" + "," + "Pos5" + "," + "Pos6" + "," + "Pos7" + "," + "Pos8" + "," + "Pos9" + "," + "Pos10");
                //        }
                //    }
                //}

                sbpotData.AppendLine();

                System.IO.File.AppendAllText(FileNameRangeCalData, sbpotData.ToString());
                sbpotData.Clear();
            }
            catch (IOException ex)
            {
                MessageBox.Show("File path not found, please check given path" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while saving file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        StringBuilder sbpotData;
        public string FileNameRangeCalData = "";
        public long FileSize = 10485760; // 10 MB
        /// <summary>
        /// UpdateLogRangePotCalibration()
        /// This function is used to append calibration data of all available pots of selected cat id to the file.
        /// First it will check size of existing file, if size of file is larger than 10MB, it will call another 
        /// function "CreateLogRangePotCalibration()" to create new file.
        /// Second it will append available calibration counts of each pot available in selected cat id to the file in proper format.
        /// If any error occures, respective error message will be displayed on UI.
        /// </summary>
        public void UpdateLogRangePotCalibration(CatIdList catID)
        {
            try
            {
                string CurrentDateTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");  //Current Date Time
                long length = new System.IO.FileInfo(FileNameRangeCalData).Length;

                int posncount = 10;
                if (length >= FileSize)
                {
                    String parentDir = Directory.GetParent(FileNameRangeCalData).ToString();
                    File.Copy(FileNameRangeCalData, parentDir + "\\" + Path.GetFileNameWithoutExtension(FileNameRangeCalData) + "_" + CurrentDateTime + ".csv");
                    File.Delete(FileNameRangeCalData);
                    CreateLogRangePotCalibration(catID);
                }
                //Variable name changed
                var strCalibratededData = "";

                sbpotData.AppendFormat(CurrentDateTime);

                ////Timing1 data
                //if (Timing1ReadDatabytes.Count > 0)
                //{
                //    posncount = 10;
                //    if (Timing1ReadDatabytes.Count != posncount)
                //    {
                //        for (int cnt = Timing1ReadDatabytes.Count; cnt < posncount; cnt++)
                //        {
                //            Timing1ReadDatabytes.Add(0);
                //        }
                //    }
                //    strCalibratededData = String.Join(",", Timing1ReadDatabytes);
                //    sbpotData.AppendFormat(", ," + "Timing1" + "," + strCalibratededData);
                //}
                ////Timing2 data
                //if (Timing2ReadDatabytes.Count > 0)
                //{
                //    posncount = 15;
                //    if (Timing2ReadDatabytes.Count != posncount)
                //    {
                //        for (int cnt = Timing2ReadDatabytes.Count; cnt < posncount; cnt++)
                //        {
                //            Timing2ReadDatabytes.Add(0);
                //        }
                //    }
                //    strCalibratededData = String.Join(",", Timing2ReadDatabytes);
                //    sbpotData.AppendFormat(", ," + "Timing2" + "," + strCalibratededData);

                //}

                ////Range1 data
                //if (Range1ReadDatabytes.Count > 0)
                //{
                //    posncount = 10;
                //    if (Range1ReadDatabytes.Count != posncount)
                //    {
                //        for (int cnt = Range1ReadDatabytes.Count; cnt < posncount; cnt++)
                //        {
                //            Range1ReadDatabytes.Add(0);
                //        }
                //    }
                //    strCalibratededData = String.Join(",", Range1ReadDatabytes);
                //    sbpotData.AppendFormat(", ," + "Range1" + "," + strCalibratededData);

                //}

                ////Range2 data
                //if (Range2ReadDatabytes.Count > 0)
                //{
                //    posncount = 10;
                //    if (Range2ReadDatabytes.Count != posncount)
                //    {
                //        for (int cnt = Range2ReadDatabytes.Count; cnt < posncount; cnt++)
                //        {
                //            Range2ReadDatabytes.Add(0);
                //        }
                //    }
                //    strCalibratededData = String.Join(",", Range2ReadDatabytes);
                //    sbpotData.AppendFormat(", ," + "Range2" + "," + strCalibratededData);

                //}
                //// Mode data
                //if (ModeReadDatabytes.Count > 0)
                //{
                //    if (catID.ControllerType == "G10")
                //        posncount = 10;
                //    else if (catID.ControllerType == "G12" || catID.ControllerType == "G13")
                //        posncount = 15;

                //    if (ModeReadDatabytes.Count != posncount)
                //    {
                //        for (int cnt = ModeReadDatabytes.Count; cnt < posncount; cnt++)
                //        {
                //            ModeReadDatabytes.Add(0);
                //        }
                //    }
                //    strCalibratededData = String.Join(",", ModeReadDatabytes);
                //    sbpotData.AppendFormat(", ," + "Mode" + "," + strCalibratededData);

                //}
                //// Extra pot data
                //if (Pot4ReadDatabytes.Count > 0)
                //{
                //    posncount = 10;
                //    if (Pot4ReadDatabytes.Count != posncount)
                //    {
                //        for (int cnt = Pot4ReadDatabytes.Count; cnt < posncount; cnt++)
                //        {
                //            Pot4ReadDatabytes.Add(0);
                //        }
                //    }
                //    strCalibratededData = String.Join(",", Pot4ReadDatabytes);
                //    sbpotData.AppendFormat(", ," + "Extra" + "," + strCalibratededData);
                //}
                sbpotData.AppendLine();
                if (sbpotData.Length != 0)
                    System.IO.File.AppendAllText(FileNameRangeCalData, sbpotData.ToString());
                sbpotData.Clear();
            }
            catch (IOException ex)
            {
                MessageBox.Show("File not found or if file is open, please close the file /" + ex.Message, "Error");
                sbpotData.Clear();
            }
            catch (Exception)
            {
                sbpotData.Clear();
                MessageBox.Show("Error occured while saving file.", "Error");
            }
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
