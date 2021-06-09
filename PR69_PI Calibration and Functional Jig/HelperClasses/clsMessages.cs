using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Resources;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    /********************************************************************************************
              Class Name        : clsMessages Class
              Purpose           : This class contains Messages. On the basis of message ID messages are displayed. 
              Date              : 1/06/2017
              Written By        : Shubham
              CopyRight         : General Industrial Controls Pvt. Ltd. Pune
              Modified          : Date                
              Released Version  :  NA
              Changed By        :  NA
              Decription Of Change:  NA
    ********************************************************************************************/
    public static class clsMessages
    {
        //This is object of the Resource manager class to read the strings from the resource file.
        //public static ResourceManager objResManager = new ResourceManager("PR69_Function_and_Calibration_JIG.Resource.Res", typeof(clsMessages).Assembly);
        public static ResourceManager objResManager = Properties.Resources.ResourceManager;

        ///<MemberName>ShowMessageInProgressWindow</MemberName>
        ///<MemberType>function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        /// This function displays message in the progress window of frmmain, accuracy test, jig test. 
        ///</summary>
        ///<param name="imMsgID">This is the ID of message which is to be displayed</param>
        ///<ClassName>clsMessages</ClassName>
        public static void ShowMessageInProgressWindow(int imMsgID)
        {
            try
            {
                switch (imMsgID)
                {
                    
                    case clsMessageIDs.FAIL_TO_CONNECT:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("FAIL_TO_CONNECT", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.DEVICE_CONNECTED:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("DEVICE_CONNECTED", clsGlobalVariables.objCultureinfo) + "\n";
                        break;

                    case clsMessageIDs.FAIL_TO_SET_BAUDRATE:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("FAIL_TO_SET_BAUDRATE", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.BAUDRATE_SET_SUCCESSFUL:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("BAUDRATE_SET_SUCCESSFUL", clsGlobalVariables.objCultureinfo) + "\n";
                        break;

                    case clsMessageIDs.CODE_LOCK_NOT_MATCH:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("CODE_LOCK_NOT_MATCH", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.CODE_LOCK_MATCHED:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("CODE_LOCK_MATCHED", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.FAILED_TO_ERASE:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("FAILED_TO_ERASE", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.ERASED_SUCCESSFULLY:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("ERASED_SUCCESSFULLY", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.FAIL_TO_PROGRAM:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("FAIL_TO_PROGRAM", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.PROGRAM_SUCCESSFUL:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("PROGRAM_SUCCESSFUL", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.FAIL_ENDPROGRAM:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("FAIL_ENDPROGRAM", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.ENDPROGRAMMING_SUCCESSFUL:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("ENDPROGRAMMING_SUCCESSFUL", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.PROGRAMMING:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("PROGRAMMING", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.CHECK_CONNECTIONS:
                          clsGlobalVariables.ObjprogVM.strtestReport = clsGlobalVariables.ObjprogVM.strtestReport + objResManager.GetString("CHECK_CONNECTIONS1", clsGlobalVariables.objCultureinfo) + Environment.NewLine + objResManager.GetString("CHECK_CONNECTIONS2", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                   // case clsMessageIDs.PORT_NOT_PRESENT:
                   //       clsGlobalVariables.objTestSetupfrm.txtDescription.Text = objResManager.GetString("PORT_NOT_PRESENT", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.SLAVE1_OK:
                   //       clsGlobalVariables.objTestSetupfrm.txtDescription.Text = objResManager.GetString("SLAVE1_OK", clsGlobalVariables.objCultureinfo) ;
                   //     break;

                   // case clsMessageIDs.SLAVE2_OK:
                   //       clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE2_OK", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.SLAVE3_OK:
                   //       clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE3_OK", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.SLAVE4_OK:
                   //       clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE4_OK", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.SLAVE1_NOT_OK:
                   //       clsGlobalVariables.objTestSetupfrm.txtDescription.Text = objResManager.GetString("SLAVE1_NOT_OK", clsGlobalVariables.objCultureinfo) ;
                   //     break;

                   // case clsMessageIDs.SLAVE2_NOT_OK:
                   //       clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE2_NOT_OK", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.SLAVE3_NOT_OK:
                   //       clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE3_NOT_OK", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.SLAVE4_NOT_OK:
                   //       clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE4_NOT_OK", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.CALIB_CONST_WRITE_SUCCESSFUL:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("CALIB_CONST_WRITE_SUCCESSFUL", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.CALIB_CONST_WRITE_UNSUCCESSFUL:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("CALIB_CONST_WRITE_UNSUCCESSFUL", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ERR_IN_ACCURACY:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ERR_IN_ACCURACY", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_PT100_0:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_0", clsGlobalVariables.objCultureinfo);
                   //     break;
                   // case clsMessageIDs.ACCURACY_PT100_NEG_100:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_NEG_100", clsGlobalVariables.objCultureinfo);
                   //     break;
                   // case clsMessageIDs.ACCURACY_PT100_400:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_400", clsGlobalVariables.objCultureinfo);
                   //     break;
                   // case clsMessageIDs.ACCURACY_PT100_300:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_300", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_PT100_700:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_700", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_R_1750:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_R_1750", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_R_1000:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_R_1000", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_R_0:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_R_0", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_J_0:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_0", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_J_400:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_400", clsGlobalVariables.objCultureinfo);
                   //     break;
                   // case clsMessageIDs.ACCURACY_J_300:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_300", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_J_700:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_700", clsGlobalVariables.objCultureinfo);
                   //     break;
                   // case clsMessageIDs.ACCURACY_J_NEG_100:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_NEG_100", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_mA_4:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_4", clsGlobalVariables.objCultureinfo);
                   //     break;
                   // case clsMessageIDs.ACCURACY_mA_1:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_1", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_mA_12:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_12", clsGlobalVariables.objCultureinfo);
                   //     break;
                   // case clsMessageIDs.ACCURACY_mA_12_PI:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_12_PI", clsGlobalVariables.objCultureinfo);
                   //     break;


                   // case clsMessageIDs.ACCURACY_mA_20:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_20", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_mA_20_PI:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_20_PI", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_VOLT_1:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_VOLT_1", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_VOLT_5:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_VOLT_5", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.ACCURACY_VOLT_10:
                   //       Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_VOLT_10", clsGlobalVariables.objCultureinfo);
                   //     break;

                   // case clsMessageIDs.VREF_TOLERANCE_ERR:
                   //       Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("VREF_TOLERANCE_ERR", clsGlobalVariables.objCultureinfo);
                   //     break;
                   //case clsMessageIDs.CJC_Test:
                   //       Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("CJC_TEST", clsGlobalVariables.objCultureinfo);
                   //     break;
                   // case clsMessageIDs.VREF_CALIB_ERR:
                   //       Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("VREF_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                   //     break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void ShowMessageInProgressWindowForAccuracy(int imMsgID,string message)
        {
            try
            {
                switch (imMsgID)
                {
                    case clsMessageIDs.ACCURACY_PT100:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100", clsGlobalVariables.objCultureinfo) + message;
                        break;
                    case clsMessageIDs.ACCURACY_R:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_R", clsGlobalVariables.objCultureinfo) + message;
                        break;
                    case clsMessageIDs.ACCURACY_J:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J", clsGlobalVariables.objCultureinfo) + message;
                        break;
                    case clsMessageIDs.ACCURACY_mA:
                        //CA55if (Program.objMainForm.radioButton_PI.Checked)
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_PI", clsGlobalVariables.objCultureinfo) + message;
                        //CA55else
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA", clsGlobalVariables.objCultureinfo) + message;
                        break;
                    case clsMessageIDs.ACCURACY_VOLT:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_VOLT", clsGlobalVariables.objCultureinfo) + message;
                        break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///<MemberName>ShowAnalogMessageInProgressWindow</MemberName>
        ///<MemberType>function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        /// This function displays Analog message in the progress window of frmmain.
        ///</summary>
        ///<param name="imMsgID">This is the ID of message which is to be displayed</param>
        ///<param name="strmAnalogTest">This parameter tells that whether it is current or voltage test.</param>
        ///<param name="strmValue">This parameter tells that which value is read for the current test.</param>
        ///<ClassName>clsMessages</ClassName>
        public static void ShowAnalogMessageInProgressWindow(int imMsgID,string strmAnalogTest, string strmValue)
        {
            try
            {
                switch (imMsgID)
                {
                    case clsMessageIDs.ANALOG_TEST_CURRENT:
                        //CA55 Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ANALOG_TEST_CURRENT", clsGlobalVariables.objCultureinfo) + strmAnalogTest + objResManager.GetString("is", clsGlobalVariables.objCultureinfo) + strmValue;
                        break;

                    case clsMessageIDs.ANALOG_TEST_VOLTAGE:
                        //CA55 Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ANALOG_TEST_VOLTAGE", clsGlobalVariables.objCultureinfo) + strmAnalogTest + objResManager.GetString("is", clsGlobalVariables.objCultureinfo) + strmValue;
                        break;

                    case clsMessageIDs.ANALOG_TEST_VALUE:
                        //CA55 Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("for", clsGlobalVariables.objCultureinfo) + strmAnalogTest + objResManager.GetString("ANALOG_TEST_VALUE", clsGlobalVariables.objCultureinfo) + strmValue;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>DisplayMessage</MemberName>
        ///<MemberType>function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
        ///<summary>
        /// This function displays message box with messge having message Id passed to it as a parameter.
        ///</summary>
        ///<param name="imMsgID">This is the ID of message which is to be displayed</param>
        ///<ClassName>clsMessages</ClassName>
        public static string DisplayMessage(int imMsgID)
        {
            try
            {
                switch (imMsgID)
                {
                    case clsMessageIDs.Main_ERR_MSG:
                        //CA55 clsGlobalVariables.objGlobalFunction.PLC_OFF();
                        string strmessage = objResManager.GetString("Main_ERR_MSG1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                                  objResManager.GetString("Main_ERR_MSG2", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                                  objResManager.GetString("Main_ERR_MSG3", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                                  objResManager.GetString("Main_ERR_MSG4", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                                  objResManager.GetString("Main_ERR_MSG5", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                                  objResManager.GetString("Main_ERR_MSG6", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                                  objResManager.GetString("Main_ERR_MSG7", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                                  objResManager.GetString("Main_ERR_MSG8", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                                  objResManager.GetString("Main_ERR_MSG9", clsGlobalVariables.objCultureinfo);
                        MessageBox.Show(strmessage, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    //-------Changed By Shubham
                    //Date:- 28-04-2018
                    //Version:- V17
                    //Statement:- global variable which stores the ongoing test name is passed as a parameter to the display message function.
                    case clsMessageIDs.TWOWIRE_MSG_ID:
                        clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("TWOWIRE_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("TWOWIRE_MSG_ID2", clsGlobalVariables.objCultureinfo)
                         + System.Environment.NewLine + objResManager.GetString("TWOWIRE_MSG_ID3", clsGlobalVariables.objCultureinfo), "Two_Wires_Connected.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    case clsMessageIDs.TWOWIRE_MSG_ID_PI:
                        clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("TWOWIRE_MSG_ID1_PI", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("TWOWIRE_MSG_ID2_PI", clsGlobalVariables.objCultureinfo)
                         + System.Environment.NewLine + objResManager.GetString("TWOWIRE_MSG_ID3_PI", clsGlobalVariables.objCultureinfo), "Two_Wires_Connected_PI.jpg", clsGlobalVariables.strgOngoingTestName);                       
                        break;
                    case clsMessageIDs.TWO_WIRE_MSG_96x96:
                        clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("TWO_WIRE_MSG_96x961", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("TWO_WIRE_MSG_96x962", clsGlobalVariables.objCultureinfo)
                         + System.Environment.NewLine + objResManager.GetString("TWO_WIRE_MSG_96x963", clsGlobalVariables.objCultureinfo), "Two_Wires_Connected_96x96.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    case clsMessageIDs.THREEWIRE_MSG_ID:
                        clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("THREEWIRE_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("THREEWIRE_MSG_ID2", clsGlobalVariables.objCultureinfo),
                         "All_Wires_Connected.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    case clsMessageIDs.ALL_WIRE_MSG_96x96:
                        clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("ALL_WIRE_MSG_96x961", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("ALL_WIRE_MSG_96x962", clsGlobalVariables.objCultureinfo),
                         "All_Wires_Connected_96x96.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    case clsMessageIDs.ALL_WIRE_MSG_PI:
                        clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("ALL_WIRE_MSG_PI1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("ALL_WIRE_MSG_PI2", clsGlobalVariables.objCultureinfo),
                                                 "All_Wires_Connected_PI.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;

                    case clsMessageIDs.CURRENT_SETTING_MSG_ID:
                        clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("CURRENT_SETTING_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("CURRENT_SETTING_MSG_ID2", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                         objResManager.GetString("CURRENT_SETTING_MSG_ID3", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("CURRENT_SETTING_MSG_ID4", clsGlobalVariables.objCultureinfo), "Current_Connection.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    case clsMessageIDs.VOLTAGE_SETTING_MSG_ID:
                        clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("VOLTAGE_SETTING_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("VOLTAGE_SETTING_MSG_ID2", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                         objResManager.GetString("VOLTAGE_SETTING_MSG_ID3", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("VOLTAGE_SETTING_MSG_ID4", clsGlobalVariables.objCultureinfo), "Voltage_Connection.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;

                    case clsMessageIDs.MA_CALIBRATION_MSG_ID:
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                            clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("MA_CALIBRATION_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("MA_CALIBRATION_MSG_ID2", clsGlobalVariables.objCultureinfo), "mA_Calibration_Wires_Connected.jpg", clsGlobalVariables.strgOngoingTestName);
                        else
                            clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("MA_CALIBRATION_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("MA_CALIBRATION_MSG_ID2_PI", clsGlobalVariables.objCultureinfo), "mA_Calibration_Wires_Connected_PI.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;

                    case clsMessageIDs.VOLT_CALIBRATION_MSG_ID:
                        if (clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_96x96 || clsGlobalVariables.selectedDeviceType == clsGlobalVariables.SelectedDeviceType.PR69_48x48)
                            clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("VOLT_CALIBRATION_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("VOLT_CALIBRATION_MSG_ID2", clsGlobalVariables.objCultureinfo), "Volt_Calibration_Wires_Connected.jpg", clsGlobalVariables.strgOngoingTestName);
                        else
                            clsGlobalVariables.objGlobalFunction.DisplayImgMessageBox(objResManager.GetString("VOLT_CALIBRATION_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("VOLT_CALIBRATION_MSG_ID2_PI", clsGlobalVariables.objCultureinfo), "Volt_Calibration_Wires_Connected_PI.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    //---------------Changes End.

                    case clsMessageIDs.JIG_AND_CALIB_PORT_SAME:
                        return objResManager.GetString("JIG_AND_CALIB_PORT_SAME", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.ACCURACY_COMPLETE:
                        return objResManager.GetString("ACCURACY_COMPLETE", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.ACCURACY_FAILED:
                        return objResManager.GetString("ACCURACY_FAILED", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.ERROR_INI_TOTAL_NO_OF_CATID:
                        return objResManager.GetString("ERROR_INI_TOTAL_NO_OF_CATID", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.ERROR_INI_TOTAL_NO_OF_CATID_POSITIVE:
                        return objResManager.GetString("ERROR_INI_TOTAL_NO_OF_CATID_POSITIVE", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.OBSERVE_DISP_TEST:
                        return objResManager.GetString("OBSERVE_DISP_TEST", clsGlobalVariables.objCultureinfo);
                    //MessageBox.Show("Dispaly Test", clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    case clsMessageIDs.SET_1mV_IN_CALIB:
                        return objResManager.GetString("SET_1mV_IN_CALIB", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.ENSURE_1mV_IN_CALIB_PI:
                        return objResManager.GetString("ENSURE_1mV_IN_CALIB_PI", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.SET_50mV_IN_CALIB:
                        return objResManager.GetString("SET_50mV_IN_CALIB", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.ACCURACY_POINT_NOT_IN_RANGE:
                        return objResManager.GetString("ACCURACY_POINT_NOT_IN_RANGE", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.SET_350Ohm_IN_CALIB:
                        return objResManager.GetString("SET_350Ohm_IN_CALIB", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.SET_1V_IN_CALIB:
                        return objResManager.GetString("SET_1V_IN_CALIB", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.SET_9V_IN_CALIB:
                        return objResManager.GetString("SET_9V_IN_CALIB", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.WRONG_DEVICE_SELECTION:
                        return objResManager.GetString("WRONG_DEVICE_SELECTION", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.SET_4mA_IN_CALIB:
                        return objResManager.GetString("SET_4mA_IN_CALIB", clsGlobalVariables.objCultureinfo);
                    case clsMessageIDs.SET_1mA_IN_CALIB:
                        return objResManager.GetString("SET_1mA_IN_CALIB", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.SET_20mA_IN_CALIB:
                        return objResManager.GetString("SET_20mA_IN_CALIB", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.CALIB_MEASURE_NOT_ON:
                        return objResManager.GetString("CALIB_MEASURE_NOT_ON", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.DUT_ALREADY_CALIBRATED:
                        return objResManager.GetString("DUT_ALREADY_CALIBRATED", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.MAX_DELAY_ERR:
                        return objResManager.GetString("MAX_DELAY_ERR", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.POWER_CYCLE_MSG_ID:
                        return objResManager.GetString("POWER_CYCLE_MSG_ID", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.UNABLE_TO_CONNECT:
                        return objResManager.GetString("UNABLE_TO_CONNECT", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.PROGRAMMING_COMPLETE:
                        return objResManager.GetString("PROGRAMMING_COMPLETE", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.ENTER_VALID_PASSWORD:
                        return objResManager.GetString("ENTER_VALID_PASSWORD", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.NOT_VALID_PASSWORD:
                        return objResManager.GetString("NOT_VALID_PASSWORD", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.ANALOG_NOT_FOUND_INI_ERR:
                        return objResManager.GetString("ANALOG_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.DUTID_NOT_FOUND_INI_ERR:
                        return objResManager.GetString("DUTID_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.TYPE_NOT_FOUND_INI_ERR:
                        return objResManager.GetString("TYPE_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.DESCRIPTION_NOT_FOUND_INI_ERR:
                        return objResManager.GetString("DESCRIPTION_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.CATID_NOT_FOUND_INI_ERR:
                        return objResManager.GetString("CATID_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.TYPEORRELAY_NOT_FOUND_INI_ERR:
                        return objResManager.GetString("TYPEORRELAY_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.TEST_NOT_FOUND_INI_ERR:
                        return objResManager.GetString("TEST_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.MAKE_SOURCE_OFF:
                        return objResManager.GetString("MAKE_SOURCE_OFF", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.INI_FILE_NOT_PRESENT:
                        return objResManager.GetString("INI_FILE_NOT_PRESENT", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.CALIBRATED_BUT_ACCURACY_ISNOTDONE:
                        return objResManager.GetString("CALIBRATED_BUT_ACCURACY_ISNOTDONE", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.MOT_FILE_PATH_NOT_PRESENT:
                        return objResManager.GetString("MOT_FILE_PATH_NOT_PRESENT", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.FAIL_TO_PROGRAM:
                        return objResManager.GetString("FAIL_TO_PROGRAM", clsGlobalVariables.objCultureinfo);

                    //-------Changed By 
                    //Date:- 24-02-
                    //Version:- V16
                    //Statement:- New Message added.    
                    case clsMessageIDs.REMOVE_SOURCE_CONN:
                        return objResManager.GetString("REMOVE_SOURCE_CONN", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.ABORT_BUTTON_CLICK:
                        return objResManager.GetString("ABORT_BUTTON_CLICK", clsGlobalVariables.objCultureinfo);
                  
                    case clsMessageIDs.Test_24Volt_OUTPUT_TEST_MSG:
                        return objResManager.GetString("Test_24_OP_MESSAGE", clsGlobalVariables.objCultureinfo) ;
                        
                   
                        

                    case clsMessageIDs.DUT_CALIB_FAILED:
                        return objResManager.GetString("DUT_CALIB_FAILED", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.DUT_CALIB_COMPLETED:
                        return objResManager.GetString("DUT_CALIB_COMPLETED", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.ERASING_DUT:
                        return objResManager.GetString("ERASING_DUT", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.DUT_ERASE_FAILED:
                        return objResManager.GetString("DUT_ERASE_FAILED", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.DUT_ERASE_COMPLETED:
                        return objResManager.GetString("DUT_ERASE_COMPLETED", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.ONEMV_DEBUG_MSG_ID:
                        return objResManager.GetString("ONEMV_DEBUG_MSG_ID1", clsGlobalVariables.objCultureinfo) +Environment.NewLine+
                         objResManager.GetString("ONEMV_DEBUG_MSG_ID2", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("ONEMV_DEBUG_MSG_ID3", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("ONEMV_DEBUG_MSG_ID4", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("ONEMV_DEBUG_MSG_ID5", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("ONEMV_DEBUG_MSG_ID6", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("ONEMV_DEBUG_MSG_ID7", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.FIFTYMV_DEBUG_MSG_ID:
                        return objResManager.GetString("FIFTYMV_DEBUG_MSG_ID1", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("FIFTYMV_DEBUG_MSG_ID2", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("FIFTYMV_DEBUG_MSG_ID3", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("FIFTYMV_DEBUG_MSG_ID4", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("FIFTYMV_DEBUG_MSG_ID5", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("FIFTYMV_DEBUG_MSG_ID6", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("FIFTYMV_DEBUG_MSG_ID7", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.THREEFIFTYOHM_DEBUG_MSG_ID:
                        return objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID1", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID2", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID3", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID4", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID5", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID6", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID7", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                         objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID8", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.RELAY_DEBUG_MSG_ID:
                        return objResManager.GetString("RELAY_DEBUG_MSG_ID1", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                        objResManager.GetString("RELAY_DEBUG_MSG_ID2", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                        objResManager.GetString("RELAY_DEBUG_MSG_ID3", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                        objResManager.GetString("RELAY_DEBUG_MSG_ID4", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                        objResManager.GetString("RELAY_DEBUG_MSG_ID5", clsGlobalVariables.objCultureinfo) + Environment.NewLine +
                        objResManager.GetString("RELAY_DEBUG_MSG_ID6", clsGlobalVariables.objCultureinfo);

                    case clsMessageIDs.THREEFIFTYOHM_CALIB_ERR:
                        return objResManager.GetString("THREEFIFTYOHM_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.ONEMV_CALIB_ERR:
                        return objResManager.GetString("ONEMV_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.FIFTYMV_CALIB_ERR:
                        return objResManager.GetString("FIFTYMV_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.THREEFIFTYOHM_CALIB_SUCCESS:
                        return objResManager.GetString("THREEFIFTYOHM_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.ONEMV_CALIB_SUCCESS:
                        return objResManager.GetString("ONEMV_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.FIFTYMV_CALIB_SUCCESS:
                        return objResManager.GetString("FIFTYMV_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.CALIBRATED_DUT:
                        return objResManager.GetString("CALIBRATED_DUT", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.UNCALIBRATED_DUT:
                        return objResManager.GetString("UNCALIBRATED_DUT", clsGlobalVariables.objCultureinfo);
                        

                   // case clsMessageIDs.CALIBRATED_BUT_ACCURACY_ISNOTDONE:
                       // return objResManager.GetString("CALIBRATED_BUT_ACCURACY_ISNOTDONE", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.ONEVOLT_CALIB_ERR:
                        return objResManager.GetString("ONEVOLT_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.ONEVOLT_CALIB_SUCCESS:
                        return objResManager.GetString("ONEVOLT_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.NINEVOLT_CALIB_ERR:
                        return objResManager.GetString("NINEVOLT_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.NINEVOLT_CALIB_SUCCESS:
                        return objResManager.GetString("NINEVOLT_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        
                    case clsMessageIDs.ONEMA_CALIB_ERR:
                        return objResManager.GetString("ONEMA_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.ONEMA_CALIB_SUCCESS:
                        return objResManager.GetString("ONEMA_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.FOURMA_CALIB_ERR:
                        return objResManager.GetString("FOURMA_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.FOURMA_CALIB_SUCCESS:
                        return objResManager.GetString("FOURMA_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.TWENTYMA_CALIB_ERR:
                        return objResManager.GetString("TWENTYMA_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.TWENTYMA_CALIB_SUCCESS:
                        return objResManager.GetString("TWENTYMA_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.JIG_INITIALZATION_BYPASSED:
                        return objResManager.GetString("JIG_INITIALZATION_BYPASSED", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.JIG_INITIALZATION_STARTED:
                        return objResManager.GetString("JIG_INITIALZATION_STARTED", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.JIG_INITIALZATION_COMPLETED:
                        return objResManager.GetString("JIG_INITIALZATION_COMPLETED", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.JIG_INITIALZATION_FAILED:
                        return objResManager.GetString("JIG_INITIALZATION_FAILED", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.REF_CH_ERR:
                        return objResManager.GetString("REF_CH_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.SIG_CH_ERR:
                        return objResManager.GetString("SIG_CH_ERR", clsGlobalVariables.objCultureinfo);
                        

                    case clsMessageIDs.CJC_CNT_ERR:
                        return objResManager.GetString("CJC_CNT_ERR", clsGlobalVariables.objCultureinfo);
                        
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        ///<MemberName>DisplayININOTAvailableMessage</MemberName>
        ///<MemberType>function</MemberType>
        ///<CreatedBy></CreatedBy>
        ///<CommentedBy></CommentedBy>
        ///<Date>12/05/</Date>
        ///<summary>
        /// This function displays message box with messge having message Id passed to it as a parameter.
        ///</summary>
        ///<param name="imMsgID">This is the ID of message which is to be displayed</param>
        ///<ClassName>clsMessages</ClassName>
        public static void DisplayININOTAvailableMessage(int imMsgID, string strmPath)
        {
            try
            {
                switch (imMsgID)
                {
                    case clsMessageIDs.MOT_FILE_PATH_NOT_AVAILABLE:
                    case  clsMessageIDs.HEX_FILE_PATH_NOT_AVAILABLE:                        
                        MessageBox.Show(objResManager.GetString("MOT_FILE_PATH_NOT_AVAILABLE", clsGlobalVariables.objCultureinfo) + strmPath, clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
