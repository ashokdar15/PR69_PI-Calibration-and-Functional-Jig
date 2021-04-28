using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ProgrammingWindowVM()
        {
            StartProgramBtnVis = true;
            strtestReport = "Start Program";
            _btnStartProgram = new RelayCommand(btnStartProgramClk);
        }

        private async void btnStartProgramClk(object obj)
        {
            byte btmRetVal;
            string strmPath = "";
            try
            {
                CurrentValue = 0;// clsGlobalVariables.objfrmProgramming.prgBar.Minimum;                             
                
                btmRetVal = CheckCOMPORT();
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsMessages.DisplayMessage(clsMessageIDs.UNABLE_TO_CONNECT);
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CHECK_CONNECTIONS);
                    MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();
                        
                    return;
                }
               
                strmPath = clsGlobalVariables.mainWindowVM.SelectedDeviceName + ".mot";
                
                //This ExtractDataFromMotFile() creates the binary file from the Mot file.
                //Here software reads mot main folder path then searches the mot file by the name of selected Cat ID. 
                if (ExtractDataFromMotFile(strmPath, Directory.GetCurrentDirectory() + "/temp.bin") == 1)
                {
                    //This function is used to write binary into device.
                    Programming();
                }
                    
                MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();
                
            }
            catch (Exception ex)
            {                
                MainWindowVM.initilizeCommonObject.objplcSerialComm.CloseCommPort();
                //MessageBox.Show(ex.Message, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                    return;
                }
                else
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.BAUDRATE_SET_SUCCESSFUL);
                }

                MainWindowVM.initilizeCommonObject.objplcSerialComm.comPort.BaudRate = 38400;

                btmretVal = clsGlobalVariables.objProgramingQrycls.CodeLockCheck();

                if (btmretVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CODE_LOCK_NOT_MATCH);
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.CHECK_CONNECTIONS);
                    clsMessages.DisplayMessage(clsMessageIDs.FAIL_TO_PROGRAM);
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
                    return;
                }
                else
                {
                    clsMessages.ShowMessageInProgressWindow(clsMessageIDs.ENDPROGRAMMING_SUCCESSFUL);
                    clsMessages.DisplayMessage(clsMessageIDs.PROGRAMMING_COMPLETE);
                }
                CurrentValue = 0;// clsGlobalVariables.objfrmProgramming.prgBar.Maximum;
            }
            catch (Exception ex)
            {
                throw ex;
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
                        MainWindowVM.initilizeCommonObject.objplcSerialComm.OpenCommPort(clsGlobalVariables.algAvailableComPorts[imCounter].ToString(), false);
                        clsGlobalVariables.ig_Query_TimeOut = 1800;

                        btmRetVal = MainWindowVM.initilizeCommonObject.objplcSerialComm.SendQueryGetResponse(1000, false);

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
                                MainWindowVM.initilizeCommonObject.objplcSerialComm.uiDataEndTimeout = 10;
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
