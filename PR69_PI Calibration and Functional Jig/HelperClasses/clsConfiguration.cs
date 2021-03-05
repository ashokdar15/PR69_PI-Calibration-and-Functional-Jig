using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
/********************************************************************************************
          Class Name          : clsConfiguration Class
          Purpose             : This class contains methods to write and read data from INI file.
          Date                : 1/06/2017
          Written By          : Shubham
          CopyRight           : General Industrial Controls Pvt. Ltd. Pune
          Modified            : NA
          Released Version    :  V15
          Changed By          :  NA
          Decription Of Change:  NA
********************************************************************************************/
    public class clsConfiguration
    {       
        //These functions are present in the kernel32.dll file which is present on System32 path.
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,string val,string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key,string def, StringBuilder retVal,int size,string filePath);
      
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section">Section name</PARAM>
        /// <PARAM name="Key">Key Name</PARAM>
        /// <PARAM name="Value">Value Name</PARAM>
        public void IniWriteValue(string Section,string Key,string Value)
        {
            WritePrivateProfileString(Section, Key, Value, clsGlobalVariables.strgConfigFilePath);
        }
        
        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section,string Key,string Default)
        {
            StringBuilder temp = new StringBuilder(255);
            int imData = GetPrivateProfileString(Section, Key, Default, temp,
                                            255, clsGlobalVariables.strgConfigFilePath);
            return temp.ToString();
        }
    }
}
