using PR69_Function_and_Calibration_JIG.Classes;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR69_PI_Calibration_and_Functional_Jig.HelperClasses
{
    public class InitilizeCommonObject
    {
        public  clsSerialCommunication objJIGSerialComm = new clsSerialCommunication();
        public  clsSerialCommunication objCalibratorSerialDUT1 = new clsSerialCommunication();
        public  clsSerialCommunication objCalibratorSerialDUT2 = new clsSerialCommunication();
        public  clsSerialCommunication objCalibratorSerialDUT3 = new clsSerialCommunication();
        public  clsSerialCommunication objCalibratorSerialDUT4 = new clsSerialCommunication();
        public  clsSerialCommunication objCalibratorSerialDUT5 = new clsSerialCommunication();
        public  clsSerialCommunication objCalibratorSerialDUT6 = new clsSerialCommunication();

        public  clsSerialCommunication objplcSerialComm = new clsSerialCommunication();

        public clsLoggingData[] clsLoggingDatas = new clsLoggingData[4];

        

    }
}
