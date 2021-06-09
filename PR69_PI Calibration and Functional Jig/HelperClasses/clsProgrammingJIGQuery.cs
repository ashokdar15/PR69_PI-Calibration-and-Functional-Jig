using PR69_PI_Calibration_and_Functional_Jig.ViewModel;
using System;
using System.IO;
using System.Windows.Forms;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses

{
    public class clsProgrammingJIGQuery
    {

        #region "------------------Programming JIG Queries---------------------"
        ///<MemberName>ForceToProgram</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>05/06/2017</Date>
        ///<summary>
        ///This function is used to connect DUT.
        ///This is Programming Query for Device.
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public byte ForceToProgram()
        {
            byte btmRetVal;
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 1);

                clsGlobalVariables.btgTxBuffer[0] = clsGlobalVariables.FORCE_TO_PROGRAM;

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, false);

                if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    if (clsGlobalVariables.btgRxBuffer[0] == clsGlobalVariables.FORCE_TO_PROGRAM)
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Success;
                    }
                }
                else
                {
                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>SetBaudRate</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>05/06/2017</Date>
        ///<summary>
        ///This function is used to Set 38400 baud rate to Programmer.
        ///This is Programming Query for Device.
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public byte SetBaudRate()
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            int imCounter;
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 17);

                for (imCounter = 0; imCounter <= 15; ++imCounter)
                {
                    clsGlobalVariables.btgTxBuffer[imCounter] = 0;
                }

                clsGlobalVariables.btgTxBuffer[imCounter] = clsGlobalVariables.BAUD_RATE_38400;

                for (int imloopCntr = 0; imloopCntr < 2; imloopCntr++)
                {
                    btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if (clsGlobalVariables.btgRxBuffer[0] == clsGlobalVariables.BAUD_RATE_38400)
                        {
                            return (byte)clsGlobalVariables.enmResponseError.Success;
                        }
                        else
                        {
                            btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                        }
                    }
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ReadStatus</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>05/06/2017</Date>
        ///<summary>
        ///This function is reads the status of the programmer JIG.
        ///This is Programming Query for Device.
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public byte ReadStatus()
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;

            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 1);
                clsGlobalVariables.btgTxBuffer[0] = clsGlobalVariables.READ_STATUS;

                for (int imloopCntr = 0; imloopCntr <= 2; imloopCntr++)
                {
                    btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if (clsGlobalVariables.btgRxBuffer.Length >= 2)
                        {
                            if (clsGlobalVariables.btgRxBuffer[0] == 0x80 && clsGlobalVariables.btgRxBuffer[1] == 0x0C)
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                        }
                        btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                return btmRetVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ClearStatus</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>05/06/2017</Date>
        ///<summary>
        ///This function clears the status of the programmer JIG.
        ///This is Programming Query for Device.
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public void ClearStatus()
        {
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 1);
                clsGlobalVariables.btgTxBuffer[0] = clsGlobalVariables.CLEAR_STATUS;

                byte btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Query_TimeOut);

                //CA55  Program.objMainForm.ApplyDelay(20);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>EraseAllUnlockedBlock</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>05/06/2017</Date>
        ///<summary>
        ///This function Erases bolcks of the DUT.
        ///This is Programming Query for Device.
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public byte EraseAllUnlockedBlock()
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                ClearStatus();
                //CA55  Program.objMainForm.ApplyDelay(200);
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 2);
                clsGlobalVariables.btgTxBuffer[0] = clsGlobalVariables.ERASE_UNLOCKED_BLOCKS;
                clsGlobalVariables.btgTxBuffer[1] = clsGlobalVariables.ERASE_CONFIRM_CMD;

                btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Query_TimeOut);

                //CA55  Program.objMainForm.ApplyDelay(200);

                btmRetVal = ReadStatus();
                if (btmRetVal != (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    return btmRetVal;
                }
                return btmRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>EndProgramming</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>05/06/2017</Date>
        ///<summary>
        ///This Query tells the end of programming.
        ///This is Programming Query for Device.
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public byte EndProgramming()
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                Array.Resize(ref clsGlobalVariables.btgTxBuffer, 1);
                clsGlobalVariables.btgTxBuffer[0] = clsGlobalVariables.END_CMD;


                for (int imloopcntr = 0; imloopcntr < 2; imloopcntr++)
                {
                    btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, false);

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        if (clsGlobalVariables.btgRxBuffer.Length >= 1)
                        {
                            if (clsGlobalVariables.btgRxBuffer[0] == clsGlobalVariables.END_CMD)
                            {
                                return (byte)clsGlobalVariables.enmResponseError.Success;
                            }
                            else
                            {
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                        }
                        btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                    }
                }
                return btmRetVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>CodeLockCheck</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>05/06/2017</Date>
        ///<summary>
        ///This Query is send to check/match codelock.
        ///This is Programming Query for Device.
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public byte CodeLockCheck()
        {
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            try
            {
                byte[,] btmarrCodeLockData = new byte[3, 11];

                btmarrCodeLockData[0, 0] = 0xDF;
                btmarrCodeLockData[0, 1] = 0xFF;
                btmarrCodeLockData[0, 2] = 0x0;
                btmarrCodeLockData[0, 3] = 0x7;
                btmarrCodeLockData[0, 4] = 0x0;
                btmarrCodeLockData[0, 5] = 0x0;
                btmarrCodeLockData[0, 6] = 0x0;
                btmarrCodeLockData[0, 7] = 0x0;
                btmarrCodeLockData[0, 8] = 0x0;
                btmarrCodeLockData[0, 9] = 0x0;
                btmarrCodeLockData[0, 10] = 0x0;

                btmarrCodeLockData[1, 0] = 0xDF;
                btmarrCodeLockData[1, 1] = 0xFF;
                btmarrCodeLockData[1, 2] = 0x0;
                btmarrCodeLockData[1, 3] = 0x7;
                btmarrCodeLockData[1, 4] = 0xFF;
                btmarrCodeLockData[1, 5] = 0xFF;
                btmarrCodeLockData[1, 6] = 0xFF;
                btmarrCodeLockData[1, 7] = 0xFF;
                btmarrCodeLockData[1, 8] = 0xFF;
                btmarrCodeLockData[1, 9] = 0xFF;
                btmarrCodeLockData[1, 10] = 0xFF;

                btmarrCodeLockData[2, 0] = 0xDF;
                btmarrCodeLockData[2, 1] = 0xFF;
                btmarrCodeLockData[2, 2] = 0x0;
                btmarrCodeLockData[2, 3] = 0x7;
                btmarrCodeLockData[2, 4] = Convert.ToByte((int)('G'));
                btmarrCodeLockData[2, 5] = Convert.ToByte((int)('I'));
                btmarrCodeLockData[2, 6] = Convert.ToByte((int)('C'));
                btmarrCodeLockData[2, 7] = Convert.ToByte((int)('P'));
                btmarrCodeLockData[2, 8] = Convert.ToByte((int)('V'));
                btmarrCodeLockData[2, 9] = Convert.ToByte((int)('T'));
                btmarrCodeLockData[2, 10] = Convert.ToByte((int)(' '));

                ClearStatus();

                btmRetVal = ReadStatus();


                for (int imLoopcntr = 0; imLoopcntr <= 2; imLoopcntr++)
                {
                    Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 12);

                    clsGlobalVariables.btgTxBuffer[0] = clsGlobalVariables.ID_CHECK;

                    for (int imCounter = 0; imCounter <= 10; imCounter++)
                    {
                        clsGlobalVariables.btgTxBuffer[imCounter + 1] = btmarrCodeLockData[imLoopcntr, imCounter];
                    }

                    MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQuery(clsGlobalVariables.btgTxBuffer, clsGlobalVariables.ig_Query_TimeOut);

                    //CA55  Program.objMainForm.ApplyDelay(20);

                    btmRetVal = ReadStatus();

                    if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        return btmRetVal;
                    }

                }
                return btmRetVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>ProgramDevice</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>05/06/2017</Date>
        ///<summary>
        ///This is the main programming function in the software. 
        ///This is Programming Query for Device.
        ///</summary>                
        ///<ClassName>clsQueries</ClassName>
        public byte ProgramDevice()
        {
            long lmPageStartAddress;
            string strmHexVal;
            byte btmBlankBlock = 1;
            long lmCheckSum = 0;
            long lmTempCheckSum;
            byte[] btmarrChecksum = new byte[2];
            byte btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
            FileStream omBinStream = null;
            try
            {
                //Reads the "temp.ini" file from Application path.
                omBinStream = new FileStream(Application.StartupPath + "/temp.bin", FileMode.Open, FileAccess.Read, FileShare.None);

                //Fix number of pages are 284.
                for (int imPageCounter = 0; imPageCounter < 284; imPageCounter++)
                {

                    clsGlobalVariables.ObjprogVM.CurrentValue = (int)((imPageCounter * 100) / 284) + 1;
                    clsGlobalVariables.ObjprogVM.StatusInPercentage = (int)((imPageCounter * 100) / 284) + 1;
                    Array.Clear(clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length);
                    Array.Resize(ref clsGlobalVariables.btgTxBuffer, 262);

                    clsGlobalVariables.btgTxBuffer[0] = clsGlobalVariables.PAGE_PROGRAM;
                    lmPageStartAddress = (long)(clsGlobalVariables.CODE_START_ADDR + (long)(imPageCounter * clsGlobalVariables.PAGE_SIZE));
                    strmHexVal = lmPageStartAddress.ToString("X"); //Conversion to hexadecimal value. 
                    if (strmHexVal.Length == 4)
                    {
                        strmHexVal = "00" + strmHexVal;
                    }
                    if (strmHexVal.Length == 5)
                    {
                        strmHexVal = "0" + strmHexVal;
                    }

                    clsGlobalVariables.btgTxBuffer[1] = Convert.ToByte(strmHexVal.Substring(2, 2), 16);
                    clsGlobalVariables.btgTxBuffer[2] = Convert.ToByte(strmHexVal.Substring(0, 2), 16);

                    lmCheckSum = 0;
                    btmBlankBlock = 1;

                    for (int imDataCntr = 1; imDataCntr < 257; imDataCntr++)
                    {
                        omBinStream.Position = (lmPageStartAddress - clsGlobalVariables.CODE_START_ADDR) + imDataCntr - 1;
                        //data is read from the binary file.
                        clsGlobalVariables.btgTxBuffer[imDataCntr + 2] = Convert.ToByte(omBinStream.ReadByte());
                        lmCheckSum = lmCheckSum + clsGlobalVariables.btgTxBuffer[imDataCntr + 2];

                        if ((btmBlankBlock == 1) && (clsGlobalVariables.btgTxBuffer[imDataCntr + 2] != 0xFF))
                        {
                            btmBlankBlock = 0;
                        }
                    }
                    lmCheckSum = lmCheckSum + 5;

                    //check sum is calculated here.
                    lmTempCheckSum = lmCheckSum & 0xFF00;
                    lmTempCheckSum = lmTempCheckSum / 256;

                    clsGlobalVariables.btgTxBuffer[259] = clsGlobalVariables.READ_PAGE;
                    clsGlobalVariables.btgTxBuffer[260] = clsGlobalVariables.btgTxBuffer[1];
                    clsGlobalVariables.btgTxBuffer[261] = clsGlobalVariables.btgTxBuffer[2];
                    //Final calculated Checksum is stored ina variable.
                    btmarrChecksum[0] = Convert.ToByte(lmCheckSum & 0xFF);
                    btmarrChecksum[1] = Convert.ToByte(lmTempCheckSum & 0xFF);


                    if (btmBlankBlock == 0)
                    {
                        for (int imloopcntr = 0; imloopcntr <= 2; imloopcntr++)
                        {
                            //Here packet will get sent to the Programming Jig.
                            btmRetVal = MainWindowVM.initilizeCommonObject.objJIGSerialComm.SendQueryGetResponse(clsGlobalVariables.ig_Query_TimeOut, false);

                            if (btmRetVal == (byte)clsGlobalVariables.enmResponseError.Success)
                            {
                                if (clsGlobalVariables.btgRxBuffer.Length >= 2)
                                {
                                    //Here Jig returns checksum of packet sent to it.
                                    //So calculated checksum is compared with received checksum. 
                                    if (clsGlobalVariables.btgRxBuffer[0] != btmarrChecksum[0] ||
                                        clsGlobalVariables.btgRxBuffer[1] != btmarrChecksum[1])
                                    {
                                        return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                btmRetVal = (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                            }
                            else
                            {
                                if (imloopcntr == 2)
                                {
                                    return (byte)clsGlobalVariables.enmResponseError.Invalid_data;
                                }
                            }
                        }
                    }
                }

                omBinStream.Close();
                return btmRetVal;
            }
            catch (Exception ex)
            {
                omBinStream.Close();
                throw ex;
            }
            finally
            {
                omBinStream.Close();
            }
        }
        #endregion
    
    }
}
