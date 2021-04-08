using System;
using System.Collections;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Resources;
using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;

namespace PR69_Function_and_Calibration_JIG.Classes
{    /********************************************************************************************
          C Name            : SerialCommunication Class
          Purpose           : This class contains all methods which are responsible for serial communication.
          Date              : 1/06/2017
          Written By        : Shubham
          CopyRight         : General Industrial Controls Pvt. Ltd. Pune
          Modified          : Date                :  NA
          Released Version    :  NA
          Changed By          :  NA
          Decription Of Change:  NA
********************************************************************************************/
    public class clsSerialCommunication
    {
        #region "--Variable--"        
        public SerialPort comPort;          
        //public int imBaudRate;
        public System.Threading.TimerCallback tmrCallback;
        public System.Threading.Timer tmrMbTimer;

        public System.Threading.TimerCallback tmrTimeoutCallback;
        public System.Threading.Timer tmrTimeOut;

        public Int64 ulmTmrIntrCounter = 0;
        public int btmNoOfBytesReceived = 0;
        public int uiDataEndTimeout = 50; 
        public Boolean blnmDataRevStart;
        public Boolean blnmDataRecFlg;
        public Boolean blnmTimeOutFlg;

        //Table of CRC values for high–order byte
        byte[] auchCRCHi = new byte[]{0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81,
                                      0x40, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0,
                                      0x80, 0x41, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x0, 0xC1, 0x81, 0x40, 0x1,
                                      0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x1, 0xC0, 0x80, 0x41,
                                      0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x0, 0xC1, 0x81, 
                                      0x40, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x1, 0xC0, 
                                      0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x1, 
                                      0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40,
                                      0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 
                                      0x40, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 
                                      0x80, 0x41, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x0, 0xC1, 0x81, 0x40, 0x1, 
                                      0xC0, 0x80, 0x41, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41,
                                      0x0, 0xC1, 0x81, 0x40, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 
                                      0x40, 0x1, 0xC0, 0x80, 0x41, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 
                                      0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x1, 
                                      0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40, 0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41,
                                      0x0, 0xC1, 0x81, 0x40, 0x1, 0xC0, 0x80, 0x41, 0x1, 0xC0, 0x80, 0x41, 0x0, 0xC1, 0x81, 0x40};

        // Table of CRC values for low–order byte
        byte[] auchCRCLo = new byte[] {0x0, 0xC0, 0xC1, 0x1, 0xC3, 0x3, 0x2, 0xC2, 0xC6, 0x6, 0x7, 0xC7, 0x5, 0xC5, 0xC4, 
                                       0x4, 0xCC, 0xC, 0xD, 0xCD, 0xF, 0xCF, 0xCE, 0xE, 0xA, 0xCA, 0xCB, 0xB, 0xC9, 0x9, 
                                       0x8, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 
                                       0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
                                       0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,
                                       0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
                                       0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,
                                       0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
                                       0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,
                                       0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
                                       0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,
                                       0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5, 
                                       0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91, 
                                       0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 
                                       0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 
                                       0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C, 
                                       0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80, 0x40};
        #endregion              

        #region "--Constructor--"
        ///<MemberName>clsSerialCommunication</MemberName>
        ///<MemberType>Constructor</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        /// This is constructor of the class.
        ///</summary>
        ///<ClassName>clsSerialCommunication</ClassName>
        public clsSerialCommunication()
        {
            //imBaudRate = 57600;
            comPort = new SerialPort();
            comPort.DataBits = 8;
            comPort.Parity = Parity.None;            
            comPort.DataReceived -= new SerialDataReceivedEventHandler(comPort_DataReceived);
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);      
     
            tmrCallback = new System.Threading.TimerCallback(MbTimerIntr);
            tmrMbTimer = new System.Threading.Timer(tmrCallback, null, 200, 200);

            tmrTimeoutCallback = new System.Threading.TimerCallback(TimeOutIntr);
            tmrTimeOut = new System.Threading.Timer(tmrTimeoutCallback, null, 10000, 10000);
        }// End of Constructor.
        #endregion

        #region "----Comport Related Methods-----"
        ///<MemberName>MbTimerIntr</MemberName>
        ///<MemberType>Event</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        /// This is interrupt event of the timer.
        ///</summary>
        ///<ClassName>clsSerialCommunication</ClassName>
        public void MbTimerIntr(object obj)
        {
            if (comPort.IsOpen ==true )
            {
                ulmTmrIntrCounter += 1;
                if (btmNoOfBytesReceived < comPort.BytesToRead)//If data received then
                {
                    ulmTmrIntrCounter = 0; //Reset retry counter
                    btmNoOfBytesReceived = comPort.BytesToRead; //Update number of bytes to read.
                    tmrMbTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);// Stop inter packet timer 
                    tmrMbTimer.Change(uiDataEndTimeout, System.Threading.Timeout.Infinite); // Start inter packet timer 
                    return;
                }
            }
            else if (ulmTmrIntrCounter < 2) //'Check total number of inter packet timer intrupts without receiving any extra bytes. 
            {
                tmrMbTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                tmrMbTimer.Change(uiDataEndTimeout, System.Threading.Timeout.Infinite);
                return;
            }

            tmrMbTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite); // 'Stop inter packet timer 

            if (comPort.BytesToRead > 0)  //If bytes to read greater than 0.
            {
                btmNoOfBytesReceived = 0;
                Array.Resize(ref clsGlobalVariables.btgRxBuffer, comPort.BytesToRead);
                comPort.Read(clsGlobalVariables.btgRxBuffer, 0, comPort.BytesToRead);
                DisableTimeOut();
                blnmDataRecFlg = true;
                blnmDataRevStart = false;
            }        
        }

        ///<MemberName>DisableTimeOut</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///</summary>
        ///<ClassName>clsSerialCommunication</ClassName>
        public void DisableTimeOut()
        {
            tmrTimeOut.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        ///<MemberName>EnableTimeOut</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///Enables timer.
        ///</summary>
        ///<param name="Interval">Assigns value to timer interval.</param>
        ///<ClassName>clsSerialCommunication</ClassName>
        public void EnableTimeOut(Int64 Interval)
        {
            tmrTimeOut.Change(Interval, System.Threading.Timeout.Infinite);
            blnmTimeOutFlg = false;
        }

        ///<MemberName>TimeOutIntr</MemberName>
        ///<MemberType>Event</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///Timeout timer tick.
        ///</summary>        
        ///<ClassName>clsSerialCommunication</ClassName>
        public void TimeOutIntr(object obj)
        {
            blnmTimeOutFlg = true;
            blnmDataRevStart = false;    
        }

        ///<MemberName>OpenCommPort</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///Opens the communication port passed to it.
        ///</summary>        
        ///<param name="strmComPort">Name of port to be opened.</param>
        ///<param name="blnmIsCalibPort">This variable tells that port is opening for Calibrator or JIG</param>
        ///<ClassName>clsSerialCommunication</ClassName>
        public Boolean OpenCommPort(string strmComPort, bool blnmIsCalibPort,bool JigComPort=false)
        {
            try
            {                
                if (comPort.IsOpen == true)
                {
                    comPort.Close();
                }
                //Parameters for the Serial Port has been set.
                comPort.PortName = strmComPort;
                comPort.BaudRate = 9600;
                comPort.DataBits = 8;
                comPort.Parity = Parity.None;
                if (JigComPort)
                    comPort.Parity = Parity.Even;
                comPort.StopBits = StopBits.One;
                comPort.ReceivedBytesThreshold = 1;
                if (blnmIsCalibPort == true)
                {
                    //For Calibrator this setting must be RTS.
                    comPort.Handshake = Handshake.RequestToSend;   
                }
                else
                {
                    //For JIG this setting must be None.
                    comPort.Handshake = Handshake.None;   
                }
                //This is write data timeout set to the comport.
                comPort.WriteTimeout = 2000; 
                // Serial Port is opened.
                comPort.Open();
                //MyLogWriterDLL.LogWriter.WriteLog("port Open: " + comPort.PortName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                comPort.Close();
                return false;
            }
            return true;
        }// End of OpenCommPort().

        ///<MemberName>CloseCommPort</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///Closes the communication port passed to it.
        ///</summary>        
        ///<ClassName>clsSerialCommunication</ClassName>
        public void CloseCommPort()
        {
            try
            {
               if (comPort.IsOpen == true)
                {
                    comPort.DiscardOutBuffer();
                    comPort.DiscardInBuffer(); 
                    //Serial Port is closed.
                    comPort.Close();
                    //MyLogWriterDLL.LogWriter.WriteLog("port close: " + comPort.PortName);
                }
            }
            catch (Exception ex)
            {
                MyLogWriterDLL.LogWriter.WriteLog(ex.Message + ex.StackTrace);//, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }// End of CloseCommPort().

        ///<MemberName>comPort_DataReceived</MemberName>
        ///<MemberType>event</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This event gets fired when a data is received on the com port.
        ///</summary>        
        ///<ClassName>clsSerialCommunication</ClassName>
        void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {       
                 if (blnmDataRevStart == false)
                 {
                    blnmDataRevStart = true;
                    ulmTmrIntrCounter = 0;
                    tmrMbTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite); //Stop the Inter packet Timer (tmrMbTimer)
                    tmrMbTimer.Change(uiDataEndTimeout, System.Threading.Timeout.Infinite); //Start the Inter packet Timer (tmrMbTimer)
                 }
                DisableTimeOut();
                EnableTimeOut(5000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }// End of Data Received Event.       

        ///<MemberName>CalculateCRC</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This Function calculates CRC
        ///</summary>        
        ///<ClassName>clsSerialCommunication</ClassName>
        public void CalculateCRC(ref byte[] DataBuffer,int imstartIndex, int imStopIndex, int imCRCWritePosition)
        {
            byte btmCRCHi;    //higher byte of CRC initialized.
            byte btmCRCLo;    //Lower byte of CRC initialized.
            int imIndex;
            int imCounter;
            byte btmByte;
            
            try
            {
                btmCRCHi = 0xFF;
                btmCRCLo = 0xFF;
                imCounter = imstartIndex;

                while (true)
                {
                    btmByte = DataBuffer[imCounter];
                    imIndex = btmCRCLo ^ btmByte;
                    btmCRCLo = (byte)((btmCRCHi) ^ (auchCRCHi[imIndex]));
                    btmCRCHi = auchCRCLo[imIndex];
                    imCounter++; 
                    if (imCounter > imStopIndex)
                    {
                        DataBuffer[imCRCWritePosition] = btmCRCHi;   // Higher byte is stored in the buffer
                        DataBuffer[imCRCWritePosition - 1] = btmCRCLo;   // Lower byte is stored in the buffer
                        break;
                    }                             
                }                    
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }//End of CalculateCRC().

        ///<MemberName>ValidateCRC</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This Function calculates CRC and then verifies that with the received CRC.
        ///</summary>        
        ///<ClassName>clsSerialCommunication</ClassName>
        private byte ValidateCRC(ref byte[] RxDataBuffer)
        {
            //This array stores the last 2 bytes of CRC.
            byte[] btmoldCRC = new byte[2];
            if (RxDataBuffer.Length > 3)
            {
                //CRC from received buffer is saved in the temporary array. 
                btmoldCRC[0] = RxDataBuffer[RxDataBuffer.Length - 1];
                btmoldCRC[1] = RxDataBuffer[RxDataBuffer.Length - 2];
                //CRC of data received is calculated.
                CalculateCRC(ref RxDataBuffer, 0, RxDataBuffer.Length - 3, RxDataBuffer.Length - 1);            
                
                //Calculated CRC & CRC received from device is validated.  
                if (btmoldCRC[0] == RxDataBuffer[RxDataBuffer.Length-1] && btmoldCRC[1] == RxDataBuffer[RxDataBuffer.Length-2] )
                {
                    return (byte)clsGlobalVariables.enmResponseError.Success; 
                }
                else
                {
                    return (byte)clsGlobalVariables.enmResponseError.Incorrect_CRC_Err;  
                }
            }
            else
            {
                return (byte)clsGlobalVariables.enmResponseError.Incorrect_CRC_Err;    
            }
        }//End of ValidateCRC().

        ///<MemberName>SendQuery</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This Function Sends the Tx buffer which is passed as a parameter to it on the com port.
        ///</summary>        
        ///<param name="imTimeout">This is the timeout of the Respose.</param>
        ///<param name="TxDataBuffer">Tx buffer to be sent on Com port</param>
        ///<ClassName>clsSerialCommunication</ClassName>
        public byte SendQuery(byte[] TxDataBuffer, int imTimeout)
        {
            try
            {                            
                //Condition is used to check is Com port is open or not.
                if (comPort.IsOpen == true)
                {
                    comPort.DiscardInBuffer();
                    comPort.DiscardOutBuffer();
                    //Transmit buffer is send to the device.
                    comPort.Write(TxDataBuffer, 0, TxDataBuffer.Length);
                    DisableTimeOut();
                    EnableTimeOut(imTimeout);
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }
                else
                {
                    return (byte)clsGlobalVariables.enmResponseError.ERR_While_Sending_Data;
                }        
            }
            catch (Exception)
            {
                return (byte)clsGlobalVariables.enmResponseError.ERR_While_Sending_Data;
            }
        }//End of Send Query.

        ///<MemberName>GetResponse</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This Function waits till the response gets received from the device or timeout happens.
        ///</summary>
        ///<ClassName>clsSerialCommunication</ClassName>
        private byte GetResponse()
        {
            try
            {   
                //blnmTimeoutFlg = false;
                blnmDataRecFlg = false;
                while (blnmDataRecFlg == false)
                {
                    Application.DoEvents();
                    // If timeout occurs then failed err is sent. 
                    if (blnmTimeOutFlg == true)
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Timeout_Err;
                    }
                }
                blnmDataRecFlg = false;

                if (clsGlobalVariables.btgRxBuffer.Length < 1)
                {
                    //If timeout has been occured & Data is not received from the device then error is set. 
                    return (byte)clsGlobalVariables.enmResponseError.Data_Receive_Failed;
                }
                else
                {
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }
            }//end of Try
            catch (Exception)
            {
                return (byte)clsGlobalVariables.enmResponseError.Data_Receive_Failed;
            }
        }//end of GetResponse().

        ///<MemberName>IsResponseValid</MemberName>
        ///<MemberType>Function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        ///This Function validates the CRC of the received data from the response also validates modbus id and function code.
        ///</summary>
        ///<ClassName>clsSerialCommunication</ClassName>
        private byte IsResponseValid()
        {
            int btmFuncResp;
            try
            {
                //CRC Validation is done in this function.
                btmFuncResp = ValidateCRC(ref clsGlobalVariables.btgRxBuffer);

                if (btmFuncResp == (byte)clsGlobalVariables.enmResponseError.Success)
                {
                    // Modbus ID of Query sent & Responce of that query is validated.
                    if (clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS] != clsGlobalVariables.btgRxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_ID_POS])
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Invalid_ModbusID_err;
                    }
                    // Function ID of Query sent & Responce of that query is validated.
                    if (clsGlobalVariables.btgTxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS] != clsGlobalVariables.btgRxBuffer[(int)clsGlobalVariables.enmQueryPosition.MB_FUNCTION_POS])
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Function_Code_Mismatch_err;
                    }
                    return (byte)clsGlobalVariables.enmResponseError.Success;
                }
                else
                {
                    return (byte)clsGlobalVariables.enmResponseError.Incorrect_CRC_Err;
                }
            }// end of try.
            catch (Exception)
            {
                return (byte)clsGlobalVariables.enmResponseError.Incorrect_CRC_Err;
            }
        }// End of IsResponseValid().

        /// <summary>
        /// <membername>SendQueryGetResponse</membername>
        /// <membertype>Method</membertype>
        /// This function is used to send the Query to the serial port & get the response of that Query.
        /// If response is correct then return byte is set to Success.
        /// <MemberType>Method</MemberType>
        /// </summary>        
        public byte SendQueryGetResponse(int imTimeout, Boolean blnmNonCalibQuery)
        {
            byte btmAttempts = 1;
            byte btmFuncResp;
            try
            {
                
                while (true)
                {                    
                    Array.Clear(clsGlobalVariables.btgRxBuffer, 0, clsGlobalVariables.btgRxBuffer.Length);   
                    if (blnmNonCalibQuery == true) //for calibrator query there is no need to calculate CRC.
                    {
                        //This function calculates the CRC and adds the Higher & Lower byte of CRC in the Transmitt Buffer.
                        CalculateCRC(ref clsGlobalVariables.btgTxBuffer, 0, clsGlobalVariables.btgTxBuffer.Length - 3, clsGlobalVariables.btgTxBuffer.Length - 1);    
                    }                        
                    // This function returns success if data is transmitted successfully.
                    btmFuncResp = SendQuery(clsGlobalVariables.btgTxBuffer, imTimeout);
                    if (btmFuncResp == (byte)clsGlobalVariables.enmResponseError.Success)
                    {
                        // This function returns success if data is recieved from device.
                        btmFuncResp = GetResponse();

                        DisableTimeOut();                        
                        if (btmFuncResp == (byte)clsGlobalVariables.enmResponseError.Success)
                        {
                            //for calibrator query there is no need to validate the response.
                            if (blnmNonCalibQuery == true)
                            {
                                //Response received is validated in this function. 
                                btmFuncResp = IsResponseValid();

                                if (btmFuncResp == (byte)clsGlobalVariables.enmResponseError.Success)
                                {
                                    return (byte)clsGlobalVariables.enmResponseError.Success;
                                }
                                else
                                {
                                    return btmFuncResp;
                                }
                            }
                            else
                            {
                                return btmFuncResp;
                            }
                        }
                       
                    }//end of inner if.                    
                    btmAttempts++;
                    //Number of attempts has been checked.
                    if (btmAttempts > clsGlobalVariables.MAX_NO_RETRIES)
                    {
                        return (byte)clsGlobalVariables.enmResponseError.Max_Retries_Reached_Err;
                    }
                }// End of While. 
                
            }//End of Try.
            catch (Exception)
            {
                return (byte)clsGlobalVariables.enmResponseError.Max_Retries_Reached_Err;
            }//End of Catch
        }       
        #endregion
    }
}
