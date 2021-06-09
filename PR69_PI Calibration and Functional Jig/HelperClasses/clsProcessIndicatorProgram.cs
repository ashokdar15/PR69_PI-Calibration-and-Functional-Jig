using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    class clsProcessIndicatorProgram
    {

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



        public delegate void addTextmessageAndProessbar(string message, int percentage);
        public event addTextmessageAndProessbar AddMessageOnUI;
        public delegate void delegateClearLOG();
        public event delegateClearLOG ClearLog;
        public delegate void EnableUI(bool enable);
        public event EnableUI _EnableUI;
        //public delegate void ChnageShapeColor(string color);
        //public event ChnageShapeColor _ChangeShapeColor;


        public clsProcessIndicatorProgram(string filePath)
        {
           // Environment.
            FilePath = filePath;
        }

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
                    AddMessageOnUI( CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
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
                        AddMessageOnUI( CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
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
                            AddMessageOnUI( CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
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
                                AddMessageOnUI( CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
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
                                    AddMessageOnUI( CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
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
                                        AddMessageOnUI( CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
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
                                            AddMessageOnUI( CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
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
                                                AddMessageOnUI( CHIP_NOT_FOUND + Environment.NewLine +COMM_FAIL + Environment.NewLine + Environment.NewLine, 0);
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
