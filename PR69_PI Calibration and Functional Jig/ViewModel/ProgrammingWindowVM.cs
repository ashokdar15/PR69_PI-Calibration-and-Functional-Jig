using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PR69_PI_Calibration_and_Functional_Jig.ViewModel
{
    public class ProgrammingWindowVM : INotifyPropertyChanged
    {

        public static bool BatchProgFlag = false;
        public ProgrammingWindowVM()
        {
            StartProgramBtnVis = true;
            strtestReport = "Start Program";
            _btnStartProgram = new RelayCommand(btnStartProgramClk);
            _StopProgramBtn = new RelayCommand(btnStopProgramClk);
            IsStartBtnEnable = true;
            IsStopBtnEnable = false;

            if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
            { 
                //Batch checkbox
                //Stop btn
                //Setting
                IsBatchProgEnable = true;
            }
            else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 ||
                clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
            {
                IsBatchProgEnable = false;
            }
            else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_96x96 ||
               clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR43_48x48)
            {
                IsBatchProgEnable = false;
            }

        }

        private void btnStopProgramClk(object obj)
        {
            
        }

        private async void btnStartProgramClk(object obj)
        {
           
            try
            {
                CurrentValue = 0;// clsGlobalVariables.objfrmProgramming.prgBar.Minimum;                 
                StatusInPercentage = 0;
                strtestReport = "";
                IsStartBtnEnable = false;
                clsGlobalVariables.objGlobalFunction.GetAvailablePortName("");

                if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 ||
                clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
                {
                    ExecuteProgramPR69();
                }
                else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PI)
                {
                    ExecuteProgramPI();
                }
                
                IsStartBtnEnable = true;
            }
            catch (Exception ex)
            {
                clsGlobalVariables.mainWindowVM.CloseAllComport();
                // MainWindowVM.initilizeCommonObject.objJIGSerialComm.CloseCommPort();
                //MessageBox.Show(ex.Message, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExecuteProgramPR69()
        {
            byte btmRetVal;
            string strmPath = "";

            btmRetVal = CheckCOMPORT();
            if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
            {
                clsMessages.DisplayMessage(clsMessageIDs.UNABLE_TO_CONNECT);
                clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CHECK_CONNECTIONS);
                MainWindowVM.initilizeCommonObject.objJIGSerialComm.CloseCommPort();
                IsStartBtnEnable = true;
                return;
            }
            //strgMotFileFolderPath_PR69_48x48
            //strgMotFileFolderPath_PR69_96x96
            //strgMotFileFolderPath_PR43_96x96
            //strgMotFileFolderPath_PR43_48x48
            //strgHexFileFolderPath_PI = "";
            if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
            {
                strmPath = clsGlobalVariables.strgMotFileFolderPath_PR69_48x48 + "\\" + clsGlobalVariables.mainWindowVM.SelectedDeviceName + "\\" + clsGlobalVariables.mainWindowVM.SelectedDeviceName + ".mot";
            }
            else if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96)
            {
                strmPath = clsGlobalVariables.strgMotFileFolderPath_PR69_96x96 + "\\" + clsGlobalVariables.mainWindowVM.SelectedDeviceName + "\\" + clsGlobalVariables.mainWindowVM.SelectedDeviceName + ".mot";
            }

            //strmPath = clsGlobalVariables.mainWindowVM.SelectedDeviceName + ".mot";

            // MainWindowVM.initilizeCommonObject.objJIGSerialComm.OpenCommPort(clsGlobalVariables.strgComPortJIG, false, true);
            //This ExtractDataFromMotFile() creates the binary file from the Mot file.
            //Here software reads mot main folder path then searches the mot file by the name of selected Cat ID. 
            if (ExtractDataFromMotFile(strmPath, Directory.GetCurrentDirectory() + "/temp.bin") == 1)
            {
                //This function is used to write binary into device.
                Programming();
            }
            clsGlobalVariables.mainWindowVM.CloseAllComport();
            //MainWindowVM.initilizeCommonObject.objJIGSerialComm.CloseCommPort();
            //CurrentValue = 100;
            //StatusInPercentage = 100;
        }

        public void ExecuteProgramPI()
        {
            string FilePath = clsGlobalVariables.strgHexFileFolderPath_PI;
            if (chkbatchProgramming)
                BatchProgFlag = true;
            else
                BatchProgFlag = false;
            //string temp = clsGlobalVariables.WorkingDirectory;
            //if (FilePath == "NotFound")
            //{
            //    MessageBox.Show("An error occurred when reading the Config file,Please Check path of Config file.","Process Indicator",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //    return;
            //}
            clsProcessIndicatorProgram.STOP_PROGRAMMING = "Stop programming...";// clsMessages.objResManager.IniReadValue("MESSAGE_PI", "STOP_PROGRAMMING", "NotFound");
            clsProcessIndicatorProgram.DeviceIsProgramming = "Device is programming...";// clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "DeviceIsProgramming", "NotFound");
            clsProcessIndicatorProgram.PART_NUMBER = "1. Read part No. : M0516LDE"; //clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "PART_NUMBER", "NotFound");
            clsProcessIndicatorProgram.ChipResetFinish = "2. Chip Reset finish."; //clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "ChipResetFinish", "NotFound");
            if (chkSkipCalibration)
                clsProcessIndicatorProgram.EraseChipAllFlash = "3. Erase chip all flash finish."; //clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "EraseChipAPROMFlash", "NotFound");
            else
                clsProcessIndicatorProgram.EraseChipAllFlash = "3. Erase chip APROM flash finish."; // clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "EraseChipAllFlash", "NotFound");

            clsProcessIndicatorProgram.FinishWriteCFG0 = "4. Finish write to CFG0."; // clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "FinishWriteCFG0", "NotFound");
            clsProcessIndicatorProgram.VerifyREG_CFG0_0xFF5FFBFF_success = "5. Verify REG_CFG0 0xFF5FFBFF success.";//clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "VerifyREG_CFG0_0xFF5FFBFF_success", "NotFound");
            clsProcessIndicatorProgram.WriteAPROMFinish = "6. Write APROM finish."; //clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "WriteAPROMFinish", "NotFound");
            clsProcessIndicatorProgram.VerifyAPROMDataSuccess = "7. Verify APROM data success."; // clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "VerifyAPROMDataSuccess", "NotFound");
            clsProcessIndicatorProgram.ChipResetFinish8 = "8. Chip Reset finish."; // clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "ChipResetFinish8", "NotFound");
            clsProcessIndicatorProgram.PROGRAM_COMPLETE = "*********** Programming completed ***********"; // clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "PROGRAM_COMPLETE", "NotFound");
            clsProcessIndicatorProgram.CHIP_NOT_FOUND = ">>> Cannot find target IC chip connect with NuLink!!! <<< "; // clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "CHIP_NOT_FOUND", "NotFound");
            clsProcessIndicatorProgram.COMM_FAIL = "Communication failed."; // clsGlobalVariables.objconfig.IniReadValue("MESSAGE_PI", "COMM_FAIL ", "NotFound");
            FilePath += "\\" + clsGlobalVariables.Selectedcatid.DeviceName + "\\" + clsGlobalVariables.Selectedcatid.DeviceName + ".hex";
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("An error occurred when reading the Config file,Please Check path of Config file.", "Process Indicator", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           // EnableDiableUI(false);
            strtestReport = "";
            IsStopBtnEnable = false;
          //  clsProcessIndicatorProgram obj = new clsProcessIndicatorProgram(FilePath);
            //obj._ChangeShapeColor += Obj__ChangeShapeColor;
            //obj.AddMessageOnUI += Obj_AddMessageOnUI;
            //obj.ClearLog += Obj_ClearLog;
            //obj._EnableUI += Obj__EnableUI;
          //  obj.SkipCalibrationConst = chkSkipCalibration;
            //Thread thread1 = new Thread(obj.StartProgram);
            //thread1.Priority = ThreadPriority.Highest;
            //thread1.Start();

            StartProgram();
        }

        private void ClearLog()
        {
            strtestReport = "";
        }

        private void _EnableUI(bool state)
        {
           IsStartBtnEnable = state;
           IsStopBtnEnable = !state;
           chkbatchProgramming = state;
           chkSkipCalibration = state;
        }

        private void AddMessageOnUI(string sTOP_PROGRAMMING, int value)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate {
                // update UI
                clsGlobalVariables.objGlobalFunction.ApplyDelay(100);
               strtestReport += sTOP_PROGRAMMING;
               CurrentValue = value;
               StatusInPercentage = value;
            });

        }

        //private void Obj__ChangeShapeColor(string color)
        //{
        //    if (shpIndication.InvokeRequired)
        //        shpIndication.Invoke(new Action(() => ChangeShapeColor(color)));
        //    else
        //        ChangeShapeColor(color);
        //}

        private void Programming()
        {
            byte btmretVal;
            try
            {
                btmretVal = clsGlobalVariables.objProgramingQrycls.ForceToProgram();

                if (btmretVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FAIL_TO_CONNECT);
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CHECK_CONNECTIONS);
                    clsMessages.DisplayMessage(clsMessageIDs.FAIL_TO_PROGRAM);
                    IsStartBtnEnable = true;
                    return;
                }
                else
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.DEVICE_CONNECTED);
                }

                btmretVal = clsGlobalVariables.objProgramingQrycls.SetBaudRate();

                if (btmretVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FAIL_TO_SET_BAUDRATE);
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CHECK_CONNECTIONS);
                    clsMessages.DisplayMessage(clsMessageIDs.FAIL_TO_PROGRAM);
                    IsStartBtnEnable = true;
                    return;
                }
                else
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.BAUDRATE_SET_SUCCESSFUL);
                }

                MainWindowVM.initilizeCommonObject.objJIGSerialComm.SetBaudRate(38400);

                btmretVal = clsGlobalVariables.objProgramingQrycls.CodeLockCheck();

                if (btmretVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CODE_LOCK_NOT_MATCH);
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CHECK_CONNECTIONS);
                    clsMessages.DisplayMessage(clsMessageIDs.FAIL_TO_PROGRAM);
                    IsStartBtnEnable = true;
                    return;
                }
                else
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CODE_LOCK_MATCHED);
                }

                btmretVal = clsGlobalVariables.objProgramingQrycls.EraseAllUnlockedBlock();

                if (btmretVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FAILED_TO_ERASE);
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CHECK_CONNECTIONS);
                    clsMessages.DisplayMessage(clsMessageIDs.FAIL_TO_PROGRAM);
                    IsStartBtnEnable = true;
                    return;
                }
                else
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ERASED_SUCCESSFULLY);
                }

                clsMessages.ShowMessageInProgressWindow(clsMessageIDs.PROGRAMMING);
                btmretVal = clsGlobalVariables.objProgramingQrycls.ProgramDevice();
                if (btmretVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FAIL_TO_PROGRAM);
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CHECK_CONNECTIONS);
                    clsMessages.DisplayMessage(clsMessageIDs.FAIL_TO_PROGRAM);
                    IsStartBtnEnable = true;
                    return;
                }
                else
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.PROGRAM_SUCCESSFUL);
                }

                btmretVal = clsGlobalVariables.objProgramingQrycls.EndProgramming();

                if (btmretVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.FAIL_ENDPROGRAM);
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CHECK_CONNECTIONS);
                    clsMessages.DisplayMessage(clsMessageIDs.FAIL_TO_PROGRAM);
                    IsStartBtnEnable = true;
                    return;
                }
                else
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ENDPROGRAMMING_SUCCESSFUL);
                    clsMessages.DisplayMessage(clsMessageIDs.PROGRAMMING_COMPLETE);
                }
                CurrentValue = 100;// clsGlobalVariables.objfrmProgramming.prgBar.Maximum;
            }
            catch (Exception ex)
            {
                
                clsMessages.DisplayMessage(clsMessageIDs.FAIL_TO_PROGRAM);
                // throw ex;
                IsStartBtnEnable = true;
            }
        }

        private byte CheckCOMPORT()
        {
            byte btmRetVal;
            try
            {
                if (clsGlobalVariables.blngIsComportDetected == false)
                {
                    byte[] btmarrAsciiBytes = Encoding.ASCII.GetBytes("PR69");

                    Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, btmarrAsciiBytes.Length);

                    btmarrAsciiBytes.CopyTo(clsGlobalVariables.btgTxBuffer, 0);

                    for (int imCounter = 0; imCounter < clsGlobalVariables.algAvailableComPorts.Count; imCounter++)
                    {
                        MainWindowVM.initilizeCommonObject.objJIGSerialComm.OpenCommPort(clsGlobalVariables.algAvailableComPorts[imCounter].ToString(), false);
                        clsGlobalVariables.ig_Query_TimeOut = 1800;

                        btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(1000, false);

                        if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            if (clsGlobalVariables.btgRxBuffer.Length == 4)
                            {
                                Array.Reverse(clsGlobalVariables.btgRxBuffer);
                                for (int imElementIndex = 0; imElementIndex < clsGlobalVariables.btgRxBuffer.Length; imElementIndex++)
                                {
                                    if (clsGlobalVariables.btgRxBuffer[imElementIndex] != clsGlobalVariables.btgTxBuffer[imElementIndex])
                                    {
                                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                    }
                                }
                                clsGlobalVariables.strgComPortJIG = clsGlobalVariables.algAvailableComPorts[imCounter].ToString();
                                MainWindowVM.initilizeCommonObject.objJIGSerialComm.uiDataEndTimeout = 10;
                                return btmRetVal;
                            }
                            return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }
                else
                {
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte ExtractDataFromMotFile(string strmMotFile, string strmBinFilePath)
        {
            //Opening Files...
            FileStream omMotFileStream = new FileStream(strmMotFile, FileMode.Open, FileAccess.Read, FileShare.None);
            FileStream omBinStream = new FileStream(strmBinFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamReader omFileStreamReader = new StreamReader(omMotFileStream);
            String strmReadLineTemp = "";
            UInt32 uimStartAddress = 0;
            Byte btmNumberOfDataBytes = 0;
            //imBinFileSize is added as maximum file size for DUT mot is 128K
            int imBinFileSize = 131072;
            string strmData;
            try
            {
                while (omBinStream.Position != imBinFileSize)
                {
                    omBinStream.WriteByte(0xFF);
                }
                omBinStream.Position = 0;

                while (!omFileStreamReader.EndOfStream)
                {
                    strmReadLineTemp = omFileStreamReader.ReadLine();
                    //Read each line of MOT file.
                    //Checking for S2 Series...
                    if (strmReadLineTemp.Contains("S2"))
                    {
                        btmNumberOfDataBytes = Convert.ToByte(strmReadLineTemp.Substring(2, 2), 16);
                        uimStartAddress = Convert.ToUInt32(strmReadLineTemp.Substring(4, 6), 16);
                        uimStartAddress = uimStartAddress - clsGlobalVariables.CODE_START_ADDR;

                        strmData = strmReadLineTemp.Substring(10, ((btmNumberOfDataBytes * 2) - 8));
                        omBinStream.Position = uimStartAddress;
                        for (int imCounter = 0; imCounter < strmData.Length; ++imCounter)
                        {
                            string strTemp = strmData.Substring((imCounter), 2);
                            omBinStream.WriteByte(Convert.ToByte(strTemp, 16));
                            omBinStream.Position = uimStartAddress + 1;
                            uimStartAddress = uimStartAddress + 1;
                            ++imCounter;
                        }
                    }
                }
                //Closing Files...
                omBinStream.Close();
                omFileStreamReader.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Closing Files...
                if (omBinStream != null)
                {
                    omBinStream.Close();
                }
                if (omFileStreamReader != null)
                {
                    omFileStreamReader.Close();
                }
            }
        }

        private RelayCommand _btnStartProgram;

        public RelayCommand btnStartProgram
        {
            get { return _btnStartProgram; }
            set { _btnStartProgram = value; }
        }

        private RelayCommand _StopProgramBtn;

        public RelayCommand StopProgramBtn
        {
            get { return _StopProgramBtn; }
            set { _StopProgramBtn = value; }
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
            set
            {
                _strtestReport = value + Environment.NewLine;

                OnPropertyChanged("strtestReport");
            }
        }

        private bool _IsStartBtnEnable;

        public bool IsStartBtnEnable
        {
            get { return _IsStartBtnEnable; }
            set { _IsStartBtnEnable = value; OnPropertyChanged("IsStartBtnEnable"); }
        }

        private bool _chkbatchProgramming;

        public bool chkbatchProgramming
        {
            get { return _chkbatchProgramming; }
            set { _chkbatchProgramming = value; OnPropertyChanged("chkbatchProgramming"); }
        }


        private bool _StartBtnVis;

        public bool StartProgramBtnVis
        {
            get { return _StartBtnVis; }
            set { _StartBtnVis = value; OnPropertyChanged("StartProgramBtnVis"); }
        }

        private bool _IsBatchProgEnable;

        public bool IsBatchProgEnable
        {
            get { return _IsBatchProgEnable; }
            set
            {
                _IsBatchProgEnable = value;

                OnPropertyChanged("IsBatchProgEnable");
            }
        }

        private bool _IsStopBtnEnable;

        public bool IsStopBtnEnable
        {
            get { return _IsStopBtnEnable; }
            set { _IsStopBtnEnable = value; OnPropertyChanged("IsStopBtnEnable"); }
        }


        private bool _chkSkipCalibration;

        public bool chkSkipCalibration
        {
            get { return _chkSkipCalibration; }
            set { _chkSkipCalibration = value; OnPropertyChanged("chkSkipCalibration"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }



        private string NuLinkEXE = "NuLink.exe";
        private string PartNumber = " M0516LDE";
        private string ReadPartNo = "NuLink -p";
        private string ResetDevice = "NuLink -reset";
        private string EraseAllData = "NuLink -e ALL";
        private string WriteConfigData = "NuLink -w CFG0 0xFF5FFBFF";
        private string VerifyConfigData = "NuLink -v CFG0 0xFF5FFBFF";
        private string WriteFileCmd = @"NuLink -w APROM ";
        private string VerifyFileCmd = @"NuLink -v APROM ";
        private string FilePath = @"C:\Users\ashok.darade\Desktop\V7_UT\PIB120\PIB120.hex";
        string None = "None";
        string GREEN = "green";
        string RED = "red";
        string YELLOW = "yellow";
        string WHITE = "white";
        string ERROR = "Error";




        // private string  CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine = CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine;
        private string ErrorMessage = " >>> Cannot find target IC chip connect with NuLink.!!! <<<";



        public static string STOP_PROGRAMMING = "Stop programming...";
        public static string DeviceIsProgramming = "Device is programming...";
        public static string PART_NUMBER = "1. Read part No. : M0516LDE";
        public static string ChipResetFinish = "2. Chip Reset finish.";
        public static string EraseChipAllFlash = "3. Erase chip all flash finish.";
        public static string FinishWriteCFG0 = "4. Finish write to CFG0.";
        public static string VerifyREG_CFG0_0xFF5FFBFF_success = "5. Verify REG_CFG0 0xFF5FFBFF success.";
        public static string WriteAPROMFinish = "6. Write APROM finish.";
        public static string VerifyAPROMDataSuccess = "7. Verify APROM data success.";
        public static string ChipResetFinish8 = "8. Chip Reset finish.";
        public static string PROGRAM_COMPLETE = "*********** Programming completed ***********";
        public static string CHIP_NOT_FOUND = ">>> Cannot find target IC chip connect with NuLink!!! <<< ";
        public static string COMM_FAIL = "Communication failed.";



        //public delegate void addTextmessageAndProessbar(string message, int percentage);
        //public event addTextmessageAndProessbar AddMessageOnUI;
        //public delegate void delegateClearLOG();
        //public event delegateClearLOG ClearLog;
        //public delegate void EnableUI(bool enable);
        //public event EnableUI _EnableUI;
        //public delegate void ChnageShapeColor(string color);
        //public event ChnageShapeColor _ChangeShapeColor;


        //public clsProcessIndicatorProgram(string filePath)
        //{
        //    // Environment.
        //    FilePath = filePath;
        //}

        private bool _skipCalibrationConst;
        public bool SkipCalibrationConst
        {
            get { return _skipCalibrationConst; }
            set { _skipCalibrationConst = value; }
        }

        private string ExecuteCMD(string cmdStr)
        {
            string retString = "";
            try
            {
                string readCMDLine = "";
                using (Process cmd = new Process())
                {
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\Nuvoton Tools\\NuLink Command Tool";
                    //cmd.StartInfo.WorkingDirectory = clsGlobalVariables.WorkingDirectory;
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.StartInfo.CreateNoWindow = true;

                    cmd.Start();
                    using (StreamReader sw1 = cmd.StandardOutput)
                    {
                        StreamWriter sw = cmd.StandardInput;
                        {
                            if (sw.BaseStream.CanWrite)
                            {
                                try
                                {
                                    sw.WriteLine(cmdStr);
                                    if (sw.BaseStream != null)
                                        sw.Flush();
                                    if (sw.BaseStream != null)
                                        sw.Close();
                                    readCMDLine = sw1.ReadToEnd();
                                }
                                catch (Exception)
                                {
                                    return retString = "Error";
                                }
                            }
                        }
                    }
                }
                //MyLogWriterDLL.LogWriter temp = new MyLogWriterDLL.LogWriter();
                //    temp.LogWriterStatus();
                //MyLogWriterDLL.LogWriter.WriteLog(cmdStr+" : "+readCMDLine);
                string tempstring = readCMDLine.Trim('\r');
                string[] mystring = tempstring.Split('\n');
                foreach (var item in mystring)
                {
                    if (item.Contains(">>>"))
                    {
                        retString += " " + item + " \n";
                    }
                }
            }
            catch (Exception ex)
            {
                return retString = "Error";
            }
            return retString;
        }



        public void StartProgram()
        {
            if (SkipCalibrationConst)
                EraseAllData = "NuLink -e APROM";
            else
                EraseAllData = "NuLink -e ALL";

            //_ChangeShapeColor(None);
            //int retryCount = 0;
            bool FirstIteration = true;
            do
            {
                string result = "";
                if (clsGlobalVariables.StopButtonFlagPIProgramming)
                {
                    AddMessageOnUI(STOP_PROGRAMMING, 0);
                    _EnableUI(true);
                    break;
                }
                if (FirstIteration)
                {
                    result = ExecuteCMD(ReadPartNo);
                    FirstIteration = false;
                }
                else
                {
                    bool firstFound = false;
                    bool SecondFound = false;
                    bool evenOddIteration = true;
                    do
                    {
                        if (evenOddIteration)
                        {
                            //_ChangeShapeColor(RED);
                            evenOddIteration = false;
                        }
                        else
                        {
                            //_ChangeShapeColor(YELLOW);
                            evenOddIteration = true;
                        }
                        result = ExecuteCMD(ReadPartNo);
                        if (!result.Contains(PartNumber))
                            firstFound = true;
                        if (firstFound)
                        {
                            if (result.Contains(PartNumber) && firstFound)
                                SecondFound = true;
                        }
                        if (SecondFound && firstFound)
                            break;
                        if (clsGlobalVariables.StopButtonFlagPIProgramming)
                        {
                            AddMessageOnUI(STOP_PROGRAMMING, 0);
                            _EnableUI(true);
                            // _ChangeShapeColor(None);
                            return;
                        }
                    } while (true);

                }
                ClearLog();
                //_ChangeShapeColor(None);
                if (result.Contains(ERROR) || result.Contains(ErrorMessage) || result == "")
                {
                    AddMessageOnUI(CHIP_NOT_FOUND + Environment.NewLine + COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
                    //_ChangeShapeColor(RED);

                }
                else
                {
                    AddMessageOnUI(DeviceIsProgramming, 0);
                    AddMessageOnUI(PART_NUMBER, 12);
                    if (clsGlobalVariables.StopButtonFlagPIProgramming)
                    {
                        AddMessageOnUI(STOP_PROGRAMMING, 0);
                        _EnableUI(true);
                        return;
                    }
                    result = ExecuteCMD(ResetDevice);
                    if (result.Contains(ERROR) || result.Contains(ErrorMessage) || result == "")
                    {
                        AddMessageOnUI(CHIP_NOT_FOUND + Environment.NewLine + COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
                        //_ChangeShapeColor(RED);
                    }
                    else
                    {
                        AddMessageOnUI(ChipResetFinish, 24);
                        if (clsGlobalVariables.StopButtonFlagPIProgramming)
                        {
                            AddMessageOnUI(STOP_PROGRAMMING, 0);
                            _EnableUI(true);
                            return;
                        }


                        result = ExecuteCMD(EraseAllData);
                        if (result.Contains(ERROR) || result.Contains(ErrorMessage) || result == "")
                        {
                            AddMessageOnUI(CHIP_NOT_FOUND + Environment.NewLine + COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
                            //_ChangeShapeColor("red");
                        }
                        else
                        {
                            AddMessageOnUI(EraseChipAllFlash, 36);
                            if (clsGlobalVariables.StopButtonFlagPIProgramming)
                            {
                                AddMessageOnUI(STOP_PROGRAMMING, 0);
                                _EnableUI(true);
                                return;
                            }

                            result = ExecuteCMD(WriteConfigData);
                            if (result.Contains(ERROR) || result.Contains(ErrorMessage) || result == "")
                            {
                                AddMessageOnUI(CHIP_NOT_FOUND + Environment.NewLine + COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
                                // _ChangeShapeColor("red");
                            }
                            else
                            {
                                AddMessageOnUI(FinishWriteCFG0, 48);
                                if (clsGlobalVariables.StopButtonFlagPIProgramming)
                                {
                                    AddMessageOnUI(STOP_PROGRAMMING, 0);
                                    _EnableUI(true);
                                    return;
                                }

                                result = ExecuteCMD(VerifyConfigData);
                                if (result.Contains(ERROR) || result.Contains(ErrorMessage) || result == "")
                                {
                                    AddMessageOnUI(CHIP_NOT_FOUND + Environment.NewLine + COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
                                    //_ChangeShapeColor("red");
                                }
                                else
                                {
                                    AddMessageOnUI(VerifyREG_CFG0_0xFF5FFBFF_success, 60);
                                    if (clsGlobalVariables.StopButtonFlagPIProgramming)
                                    {
                                        AddMessageOnUI(STOP_PROGRAMMING, 0);
                                        _EnableUI(true);
                                        return;
                                    }
                                    //AddMessageOnUI("Start write APROM.", 72);
                                    result = ExecuteCMD(WriteFileCmd + FilePath);
                                    if (result.Contains(ERROR) || result.Contains(ErrorMessage) || result == "" || result == " \f>>> !!!    Please check command again. !!! <<<\r \n")
                                    {
                                        AddMessageOnUI(CHIP_NOT_FOUND + Environment.NewLine + COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
                                        //_ChangeShapeColor(RED);
                                    }
                                    else
                                    {
                                        AddMessageOnUI(WriteAPROMFinish, 72);
                                        if (clsGlobalVariables.StopButtonFlagPIProgramming)
                                        {
                                            AddMessageOnUI(STOP_PROGRAMMING, 0);
                                            _EnableUI(true);
                                            return;
                                        }
                                        // AddMessageOnUI("Start read APROM.", 84);
                                        result = ExecuteCMD(VerifyFileCmd + FilePath);
                                        if (result.Contains(ERROR) || result.Contains(ErrorMessage) || result == "" || result == " \f>>> !!!    Please check command again. !!! <<<\r \n")
                                        {
                                            AddMessageOnUI(CHIP_NOT_FOUND + Environment.NewLine + COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
                                            //_ChangeShapeColor("red");
                                        }
                                        else
                                        {
                                            AddMessageOnUI(VerifyAPROMDataSuccess, 84);
                                            if (clsGlobalVariables.StopButtonFlagPIProgramming)
                                            {
                                                AddMessageOnUI(STOP_PROGRAMMING, 0);
                                                _EnableUI(true);
                                                return;
                                            }
                                            result = ExecuteCMD(ResetDevice);
                                            if (result.Contains(ERROR) || result.Contains(ErrorMessage) || result == "")
                                            {
                                                AddMessageOnUI(CHIP_NOT_FOUND + Environment.NewLine + COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
                                                //_ChangeShapeColor("red");
                                            }
                                            else
                                            {
                                                //retryCount = 0;
                                                AddMessageOnUI(ChipResetFinish8, 100);
                                                AddMessageOnUI(Environment.NewLine + PROGRAM_COMPLETE + Environment.NewLine, 100);
                                                // _ChangeShapeColor(WHITE);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            } while (false);
            //CA55 true above condition
            _EnableUI(true);
        


    }


}
}
