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
        public static ResourceManager objResManager = new ResourceManager("PR69_Function_and_Calibration_JIG.Resource.Res", typeof(clsMessages).Assembly);

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
                    case clsMessageIDs.ABORT_BUTTON_CLICK:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ABORT_BUTTON_CLICK", clsGlobalVariables.objCultureinfo);
                        break;
                    case clsMessageIDs.Test_24Volt_OUTPUT_TEST_MSG:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("Test_24_OP_MESSAGE", clsGlobalVariables.objCultureinfo) ;
                        break;                   
                    case clsMessageIDs.JIG_COM_PORT:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("JIG_COM_PORT", clsGlobalVariables.objCultureinfo) + clsGlobalVariables.strgComPortJIG; 
                        break;

                    case clsMessageIDs.Calibrator_COM_PORT:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("Calibrator_COM_PORT", clsGlobalVariables.objCultureinfo) +clsGlobalVariables.strgComPortCalibrator; 
                        
                        break;
                    case clsMessageIDs.PLC_COM_PORT:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("PLC_COM_PORT", clsGlobalVariables.objCultureinfo) +clsGlobalVariables.strgComPortPLC; 
                        break;
                        
                    case clsMessageIDs.DUT_CALIB_FAILED:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("DUT_CALIB_FAILED", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.DUT_CALIB_COMPLETED:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("DUT_CALIB_COMPLETED", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ERASING_DUT:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ERASING_DUT", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.DUT_ERASE_FAILED:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("DUT_ERASE_FAILED", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.DUT_ERASE_COMPLETED:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("DUT_ERASE_COMPLETED", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ONEMV_DEBUG_MSG_ID:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMV_DEBUG_MSG_ID1", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMV_DEBUG_MSG_ID2", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMV_DEBUG_MSG_ID3", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMV_DEBUG_MSG_ID4", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMV_DEBUG_MSG_ID5", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMV_DEBUG_MSG_ID6", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMV_DEBUG_MSG_ID7", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.FIFTYMV_DEBUG_MSG_ID:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FIFTYMV_DEBUG_MSG_ID1", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FIFTYMV_DEBUG_MSG_ID2", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FIFTYMV_DEBUG_MSG_ID3", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FIFTYMV_DEBUG_MSG_ID4", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FIFTYMV_DEBUG_MSG_ID5", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FIFTYMV_DEBUG_MSG_ID6", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FIFTYMV_DEBUG_MSG_ID7", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.THREEFIFTYOHM_DEBUG_MSG_ID:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID1", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID2", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID3", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID4", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID5", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID6", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID7", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_DEBUG_MSG_ID8", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.RELAY_DEBUG_MSG_ID:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("RELAY_DEBUG_MSG_ID1", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("RELAY_DEBUG_MSG_ID2", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("RELAY_DEBUG_MSG_ID3", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("RELAY_DEBUG_MSG_ID4", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("RELAY_DEBUG_MSG_ID5", clsGlobalVariables.objCultureinfo);
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("RELAY_DEBUG_MSG_ID6", clsGlobalVariables.objCultureinfo);
                        break;
                    
                    case clsMessageIDs.THREEFIFTYOHM_CALIB_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ONEMV_CALIB_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMV_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.FIFTYMV_CALIB_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FIFTYMV_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.THREEFIFTYOHM_CALIB_SUCCESS:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("THREEFIFTYOHM_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ONEMV_CALIB_SUCCESS:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMV_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        break;
 
                    case clsMessageIDs.FIFTYMV_CALIB_SUCCESS:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FIFTYMV_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        break; 

                    case clsMessageIDs.CALIBRATED_DUT:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("CALIBRATED_DUT", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.UNCALIBRATED_DUT:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("UNCALIBRATED_DUT", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.CALIBRATED_BUT_ACCURACY_ISNOTDONE:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("CALIBRATED_BUT_ACCURACY_ISNOTDONE", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ONEVOLT_CALIB_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEVOLT_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ONEVOLT_CALIB_SUCCESS:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEVOLT_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.NINEVOLT_CALIB_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("NINEVOLT_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.NINEVOLT_CALIB_SUCCESS:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("NINEVOLT_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        break;
                     case clsMessageIDs.ONEMA_CALIB_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMA_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ONEMA_CALIB_SUCCESS:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("ONEMA_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.FOURMA_CALIB_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FOURMA_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.FOURMA_CALIB_SUCCESS:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("FOURMA_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.TWENTYMA_CALIB_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("TWENTYMA_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.TWENTYMA_CALIB_SUCCESS:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("TWENTYMA_CALIB_SUCCESS", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.JIG_INITIALZATION_BYPASSED:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("JIG_INITIALZATION_BYPASSED", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.JIG_INITIALZATION_STARTED:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("JIG_INITIALZATION_STARTED", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.JIG_INITIALZATION_COMPLETED:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("JIG_INITIALZATION_COMPLETED", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.JIG_INITIALZATION_FAILED:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("JIG_INITIALZATION_FAILED", clsGlobalVariables.objCultureinfo);
                        break;
                    
                    case clsMessageIDs.REF_CH_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("REF_CH_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.SIG_CH_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("SIG_CH_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.CJC_CNT_ERR:
                        //CA55  Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("CJC_CNT_ERR", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.FAIL_TO_CONNECT:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("FAIL_TO_CONNECT", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.DEVICE_CONNECTED:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("DEVICE_CONNECTED", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.FAIL_TO_SET_BAUDRATE:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("FAIL_TO_SET_BAUDRATE", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.BAUDRATE_SET_SUCCESSFUL:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("BAUDRATE_SET_SUCCESSFUL", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.CODE_LOCK_NOT_MATCH:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("CODE_LOCK_NOT_MATCH", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.CODE_LOCK_MATCHED:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("CODE_LOCK_MATCHED", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.FAILED_TO_ERASE:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("FAILED_TO_ERASE", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.ERASED_SUCCESSFULLY:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("ERASED_SUCCESSFULLY", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.FAIL_TO_PROGRAM:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("FAIL_TO_PROGRAM", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.PROGRAM_SUCCESSFUL:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("PROGRAM_SUCCESSFUL", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.FAIL_ENDPROGRAM:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("FAIL_ENDPROGRAM", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.ENDPROGRAMMING_SUCCESSFUL:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("ENDPROGRAMMING_SUCCESSFUL", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.PROGRAMMING:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("PROGRAMMING", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.CHECK_CONNECTIONS:
                        //CA55 clsGlobalVariables.objfrmProgramming.txtReport.Text = clsGlobalVariables.objfrmProgramming.txtReport.Text + objResManager.GetString("CHECK_CONNECTIONS1", clsGlobalVariables.objCultureinfo) + Environment.NewLine + objResManager.GetString("CHECK_CONNECTIONS2", clsGlobalVariables.objCultureinfo) + Environment.NewLine;
                        break;

                    case clsMessageIDs.PORT_NOT_PRESENT:
                        //CA55 clsGlobalVariables.objTestSetupfrm.txtDescription.Text = objResManager.GetString("PORT_NOT_PRESENT", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.SLAVE1_OK:
                        //CA55 clsGlobalVariables.objTestSetupfrm.txtDescription.Text = objResManager.GetString("SLAVE1_OK", clsGlobalVariables.objCultureinfo) ;
                        break;

                    case clsMessageIDs.SLAVE2_OK:
                        //CA55 clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE2_OK", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.SLAVE3_OK:
                        //CA55 clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE3_OK", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.SLAVE4_OK:
                        //CA55 clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE4_OK", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.SLAVE1_NOT_OK:
                        //CA55 clsGlobalVariables.objTestSetupfrm.txtDescription.Text = objResManager.GetString("SLAVE1_NOT_OK", clsGlobalVariables.objCultureinfo) ;
                        break;

                    case clsMessageIDs.SLAVE2_NOT_OK:
                        //CA55 clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE2_NOT_OK", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.SLAVE3_NOT_OK:
                        //CA55 clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE3_NOT_OK", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.SLAVE4_NOT_OK:
                        //CA55 clsGlobalVariables.objTestSetupfrm.txtDescription.Text = clsGlobalVariables.objTestSetupfrm.txtDescription.Text + Environment.NewLine + objResManager.GetString("SLAVE4_NOT_OK", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.CALIB_CONST_WRITE_SUCCESSFUL:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("CALIB_CONST_WRITE_SUCCESSFUL", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.CALIB_CONST_WRITE_UNSUCCESSFUL:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("CALIB_CONST_WRITE_UNSUCCESSFUL", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ERR_IN_ACCURACY:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ERR_IN_ACCURACY", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_PT100_0:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_0", clsGlobalVariables.objCultureinfo);
                        break;
                    case clsMessageIDs.ACCURACY_PT100_NEG_100:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_NEG_100", clsGlobalVariables.objCultureinfo);
                        break;
                    case clsMessageIDs.ACCURACY_PT100_400:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_400", clsGlobalVariables.objCultureinfo);
                        break;
                    case clsMessageIDs.ACCURACY_PT100_300:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_300", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_PT100_700:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_PT100_700", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_R_1750:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_R_1750", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_R_1000:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_R_1000", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_R_0:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_R_0", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_J_0:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_0", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_J_400:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_400", clsGlobalVariables.objCultureinfo);
                        break;
                    case clsMessageIDs.ACCURACY_J_300:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_300", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_J_700:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_700", clsGlobalVariables.objCultureinfo);
                        break;
                    case clsMessageIDs.ACCURACY_J_NEG_100:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_J_NEG_100", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_mA_4:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_4", clsGlobalVariables.objCultureinfo);
                        break;
                    case clsMessageIDs.ACCURACY_mA_1:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_1", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_mA_12:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_12", clsGlobalVariables.objCultureinfo);
                        break;
                    case clsMessageIDs.ACCURACY_mA_12_PI:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_12_PI", clsGlobalVariables.objCultureinfo);
                        break;


                    case clsMessageIDs.ACCURACY_mA_20:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_20", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_mA_20_PI:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_mA_20_PI", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_VOLT_1:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_VOLT_1", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_VOLT_5:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_VOLT_5", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.ACCURACY_VOLT_10:
                        //CA55 Program.objMainForm.objfrmAccTest.txtDescription.Text = objResManager.GetString("ACCURACY_VOLT_10", clsGlobalVariables.objCultureinfo);
                        break;

                    case clsMessageIDs.VREF_TOLERANCE_ERR:
                        //CA55 Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("VREF_TOLERANCE_ERR", clsGlobalVariables.objCultureinfo);
                        break;
                   case clsMessageIDs.CJC_Test:
                        //CA55 Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("CJC_TEST", clsGlobalVariables.objCultureinfo);
                        break;
                    case clsMessageIDs.VREF_CALIB_ERR:
                        //CA55 Program.objMainForm.txtProgressInfo.Text = Program.objMainForm.txtProgressInfo.Text + Environment.NewLine + objResManager.GetString("VREF_CALIB_ERR", clsGlobalVariables.objCultureinfo);
                        break;
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
        public static void DisplayMessage(int imMsgID)
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
                        MessageBox.Show(strmessage,clsGlobalVariables.strg_Application,MessageBoxButtons.OK ,MessageBoxIcon.Error); 
                        break;
                    //-------Changed By Shubham
                    //Date:- 28-04-2018
                    //Version:- V17
                    //Statement:- global variable which stores the ongoing test name is passed as a parameter to the display message function.
                    case clsMessageIDs.TWOWIRE_MSG_ID:
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("TWOWIRE_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("TWOWIRE_MSG_ID2", clsGlobalVariables.objCultureinfo)
                        //CA55 +System.Environment.NewLine + objResManager.GetString("TWOWIRE_MSG_ID3", clsGlobalVariables.objCultureinfo), "Source_mV.jpg", "Two_Wires_Connected.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    case clsMessageIDs.TWOWIRE_MSG_ID_PI:
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("TWOWIRE_MSG_ID1_PI", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("TWOWIRE_MSG_ID2_PI", clsGlobalVariables.objCultureinfo)
                        //CA55 +System.Environment.NewLine + objResManager.GetString("TWOWIRE_MSG_ID3_PI", clsGlobalVariables.objCultureinfo), "Source_mV.jpg", "Two_Wires_Connected_PI.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;

                    case clsMessageIDs.TWO_WIRE_MSG_96x96:
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("TWO_WIRE_MSG_96x961", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("TWO_WIRE_MSG_96x962", clsGlobalVariables.objCultureinfo)
                        //CA55 +System.Environment.NewLine + objResManager.GetString("TWO_WIRE_MSG_96x963", clsGlobalVariables.objCultureinfo), "Source_mV.jpg", "Two_Wires_Connected_96x96.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;

                    case clsMessageIDs.THREEWIRE_MSG_ID:
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("THREEWIRE_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("THREEWIRE_MSG_ID2", clsGlobalVariables.objCultureinfo),
                        //CA55 "Source_RTD.jpg", "All_Wires_Connected.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;

                    case clsMessageIDs.ALL_WIRE_MSG_96x96:
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("ALL_WIRE_MSG_96x961", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("ALL_WIRE_MSG_96x962", clsGlobalVariables.objCultureinfo),
                        //CA55 "Source_RTD.jpg", "All_Wires_Connected_96x96.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    case clsMessageIDs.ALL_WIRE_MSG_PI:
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("ALL_WIRE_MSG_PI1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("ALL_WIRE_MSG_PI2", clsGlobalVariables.objCultureinfo),
                        //CA55                         "Source_RTD.jpg", "All_Wires_Connected_PI.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;

                    case clsMessageIDs.CURRENT_SETTING_MSG_ID:
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("CURRENT_SETTING_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("CURRENT_SETTING_MSG_ID2", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                        //CA55 objResManager.GetString("CURRENT_SETTING_MSG_ID3", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("CURRENT_SETTING_MSG_ID4", clsGlobalVariables.objCultureinfo), "Measure_mA.jpg", "Current_Connection.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    case clsMessageIDs.VOLTAGE_SETTING_MSG_ID:
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("VOLTAGE_SETTING_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("VOLTAGE_SETTING_MSG_ID2", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine +
                        //CA55 objResManager.GetString("VOLTAGE_SETTING_MSG_ID3", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("VOLTAGE_SETTING_MSG_ID4", clsGlobalVariables.objCultureinfo), "Measure_10V.jpg", "Voltage_Connection.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;

                    case clsMessageIDs.MA_CALIBRATION_MSG_ID:
                        //CA55 if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("MA_CALIBRATION_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("MA_CALIBRATION_MSG_ID2", clsGlobalVariables.objCultureinfo), "Source_20mA.JPG", "mA_Calibration_Wires_Connected.jpg", clsGlobalVariables.strgOngoingTestName);
                        //CA55 else
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("MA_CALIBRATION_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("MA_CALIBRATION_MSG_ID2_PI", clsGlobalVariables.objCultureinfo), "Source_20mA.JPG", "mA_Calibration_Wires_Connected_PI.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;

                    case clsMessageIDs.VOLT_CALIBRATION_MSG_ID:
                        //CA55 if (Program.objMainForm.rad48by48DUT.Checked || Program.objMainForm.rad96by96DUT.Checked)
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("VOLT_CALIBRATION_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("VOLT_CALIBRATION_MSG_ID2", clsGlobalVariables.objCultureinfo), "Source_10V.JPG", "Volt_Calibration_Wires_Connected.jpg", clsGlobalVariables.strgOngoingTestName);
                        //CA55 else
                        //CA55 clsGlobalVariables.objPictMsg.DisplayMessage(objResManager.GetString("VOLT_CALIBRATION_MSG_ID1", clsGlobalVariables.objCultureinfo) + System.Environment.NewLine + objResManager.GetString("VOLT_CALIBRATION_MSG_ID2_PI", clsGlobalVariables.objCultureinfo), "Source_10V.JPG", "Volt_Calibration_Wires_Connected_PI.jpg", clsGlobalVariables.strgOngoingTestName);
                        break;
                    //---------------Changes End.

                    case clsMessageIDs.JIG_AND_CALIB_PORT_SAME:
                        MessageBox.Show(objResManager.GetString("JIG_AND_CALIB_PORT_SAME", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break; 

                    case clsMessageIDs.ACCURACY_COMPLETE:
                        MessageBox.Show(objResManager.GetString("ACCURACY_COMPLETE", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.ACCURACY_FAILED:
                        MessageBox.Show(objResManager.GetString("ACCURACY_FAILED", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.ERROR_INI_TOTAL_NO_OF_CATID:
                        MessageBox.Show(objResManager.GetString("ERROR_INI_TOTAL_NO_OF_CATID", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;

                    case clsMessageIDs.ERROR_INI_TOTAL_NO_OF_CATID_POSITIVE:
                        MessageBox.Show(objResManager.GetString("ERROR_INI_TOTAL_NO_OF_CATID_POSITIVE", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;

                    case clsMessageIDs.OBSERVE_DISP_TEST:
                        MessageBox.Show(objResManager.GetString("OBSERVE_DISP_TEST", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.SET_1mV_IN_CALIB:
                        MessageBox.Show(objResManager.GetString("SET_1mV_IN_CALIB", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case clsMessageIDs.ENSURE_1mV_IN_CALIB_PI:
                        MessageBox.Show(objResManager.GetString("ENSURE_1mV_IN_CALIB_PI", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.SET_50mV_IN_CALIB:
                        MessageBox.Show(objResManager.GetString("SET_50mV_IN_CALIB", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case clsMessageIDs.ACCURACY_POINT_NOT_IN_RANGE:
                        MessageBox.Show(objResManager.GetString("ACCURACY_POINT_NOT_IN_RANGE", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.SET_350Ohm_IN_CALIB:
                        MessageBox.Show(objResManager.GetString("SET_350Ohm_IN_CALIB", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.SET_1V_IN_CALIB:
                        MessageBox.Show(objResManager.GetString("SET_1V_IN_CALIB", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.SET_9V_IN_CALIB:
                        MessageBox.Show(objResManager.GetString("SET_9V_IN_CALIB", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.SET_4mA_IN_CALIB:
                        MessageBox.Show(objResManager.GetString("SET_4mA_IN_CALIB", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case clsMessageIDs.SET_1mA_IN_CALIB:
                        MessageBox.Show(objResManager.GetString("SET_1mA_IN_CALIB", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.SET_20mA_IN_CALIB:
                        MessageBox.Show(objResManager.GetString("SET_20mA_IN_CALIB", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.CALIB_MEASURE_NOT_ON:
                        MessageBox.Show(objResManager.GetString("CALIB_MEASURE_NOT_ON", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.DUT_ALREADY_CALIBRATED:
                        MessageBox.Show(objResManager.GetString("DUT_ALREADY_CALIBRATED", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.MAX_DELAY_ERR:
                        MessageBox.Show(objResManager.GetString("MAX_DELAY_ERR", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.POWER_CYCLE_MSG_ID :
                        MessageBox.Show(objResManager.GetString("POWER_CYCLE_MSG_ID", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.UNABLE_TO_CONNECT:
                        MessageBox.Show(objResManager.GetString("UNABLE_TO_CONNECT", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.PROGRAMMING_COMPLETE:
                        MessageBox.Show(objResManager.GetString("PROGRAMMING_COMPLETE", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.ENTER_VALID_PASSWORD:
                        MessageBox.Show(objResManager.GetString("ENTER_VALID_PASSWORD", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.NOT_VALID_PASSWORD:
                        MessageBox.Show(objResManager.GetString("NOT_VALID_PASSWORD", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.ANALOG_NOT_FOUND_INI_ERR:
                        MessageBox.Show(objResManager.GetString("ANALOG_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.DUTID_NOT_FOUND_INI_ERR:
                        MessageBox.Show(objResManager.GetString("DUTID_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.TYPE_NOT_FOUND_INI_ERR:
                        MessageBox.Show(objResManager.GetString("TYPE_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.DESCRIPTION_NOT_FOUND_INI_ERR:
                        MessageBox.Show(objResManager.GetString("DESCRIPTION_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.CATID_NOT_FOUND_INI_ERR:
                        MessageBox.Show(objResManager.GetString("CATID_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.TYPEORRELAY_NOT_FOUND_INI_ERR:
                        MessageBox.Show(objResManager.GetString("TYPEORRELAY_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.TEST_NOT_FOUND_INI_ERR:
                        MessageBox.Show(objResManager.GetString("TEST_NOT_FOUND_INI_ERR", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.MAKE_SOURCE_OFF:
                        MessageBox.Show(objResManager.GetString("MAKE_SOURCE_OFF", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case clsMessageIDs.INI_FILE_NOT_PRESENT:
                        MessageBox.Show(objResManager.GetString("INI_FILE_NOT_PRESENT", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.CALIBRATED_BUT_ACCURACY_ISNOTDONE:
                        MessageBox.Show(objResManager.GetString("CALIBRATED_BUT_ACCURACY_ISNOTDONE", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                        break;

                    case clsMessageIDs.MOT_FILE_PATH_NOT_PRESENT:
                        MessageBox.Show(objResManager.GetString("MOT_FILE_PATH_NOT_PRESENT", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsMessageIDs.FAIL_TO_PROGRAM:
                        MessageBox.Show(objResManager.GetString("FAIL_TO_PROGRAM", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    //-------Changed By Shubham
                    //Date:- 24-02-2018
                    //Version:- V16
                    //Statement:- New Message added.    
                    case clsMessageIDs.REMOVE_SOURCE_CONN:
                        MessageBox.Show(objResManager.GetString("REMOVE_SOURCE_CONN", clsGlobalVariables.objCultureinfo), clsGlobalVariables.strg_Application, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<MemberName>DisplayININOTAvailableMessage</MemberName>
        ///<MemberType>function</MemberType>
        ///<CreatedBy>Shubham</CreatedBy>
        ///<CommentedBy>Shubham</CommentedBy>
        ///<Date>12/05/2017</Date>
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
