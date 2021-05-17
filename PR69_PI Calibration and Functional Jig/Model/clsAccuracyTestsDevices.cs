using System;
using System.Collections.Generic;
using System.ComponentModel;
using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PR69_PI_Calibration_and_Functional_Jig.ViewModel;

namespace PR69_PI_Calibration_and_Functional_Jig.Model
{
    public class clsAccuracyTestsDevices : INotifyPropertyChanged
    {
        private string BgColorgreen = "#80e27e";
        private string BgColorred = "#f44336";
        private string BgColororange = "#f4511e"; // "#FFA500";

        private string Testrespass = "PASS";
        private string Testresfail = "FAIL";

        private string _backcolorDevice1;

        public string BackcolorDevice1
        {
            get { return _backcolorDevice1; }
            set { _backcolorDevice1 = value; OnPropertyChanged("BackcolorDevice1"); }
        }

        private string _backcolorDevice2;

        public string BackcolorDevice2
        {
            get { return _backcolorDevice2; }
            set { _backcolorDevice2 = value; OnPropertyChanged("BackcolorDevice2"); }
        }

        private string _BackcolorDevice3;

        public string BackcolorDevice3
        {
            get { return _BackcolorDevice3; }
            set { _BackcolorDevice3 = value; OnPropertyChanged("BackcolorDevice3"); }
        }

        private string _backcolorDevice4;

        public string BackcolorDevice4
        {
            get { return _backcolorDevice4; }
            set { _backcolorDevice4 = value; OnPropertyChanged("BackcolorDevice4"); }
        }

        private string _backcolorDevice5;

        public string BackcolorDevice5
        {
            get { return _backcolorDevice5; }
            set { _backcolorDevice5 = value; OnPropertyChanged("BackcolorDevice5"); }
        }

        private string _backcolorDevice6;

        public string BackcolorDevice6
        {
            get { return _backcolorDevice6; }
            set { _backcolorDevice6 = value; OnPropertyChanged("BackcolorDevice6"); }
        }

      
        private int _testnumber;

        public int Testnumber
        {
            get { return _testnumber; }
            set { _testnumber = value; OnPropertyChanged("Testnumber"); }
        }

        private string _AccuracyParameter;

        public string AccuracyParameter
        {
            get { return _AccuracyParameter; }
            set { _AccuracyParameter = value; OnPropertyChanged("AccuracyParameter"); }
        }


        private string _TestPoint;

        public string TestPoint
        {
            get { return _TestPoint; }
            set
            {
                _TestPoint = value;
                OnPropertyChanged("TestPoint");
            }
        }
        
        private string _TestresultDevice1;

        public string TestresultDevice1
        {
            get { return _TestresultDevice1; }
            set
            {
                _TestresultDevice1 = value;

                if (_TestresultDevice1 != null)
                {
                    if (_TestresultDevice1 != clsGlobalVariables.FAIL)
                    {
                        if (clsGlobalVariables.Validateaccuracytestbackcolor)
                        {
                            if (_TestresultDevice1 != null)
                            {
                                if (UpdateTestResult(clsGlobalVariables.accuracyTests, clsGlobalVariables.enmpointcalibration, _TestresultDevice1))
                                {
                                    BackcolorDevice1 = BgColorgreen;
                                }
                                else
                                    BackcolorDevice1 = BgColorred;
                            }
                            else
                                BackcolorDevice1 = BgColorred;

                        }
                        else
                            BackcolorDevice1 = BgColororange;
                    }
                    else
                        BackcolorDevice1 = BgColorred;

                    if (BackcolorDevice1 == BgColorred)                    
                        clsGlobalVariables.listAccTest.Add(false);
                    
                }

                OnPropertyChanged("TestresultDevice1");
            }
        }

        private string _TestresultDevice2;

        public string TestresultDevice2
        {
            get { return _TestresultDevice2; }
            set
            {
                _TestresultDevice2 = value;

                if (_TestresultDevice2 != null)
                {
                    if (_TestresultDevice2 != clsGlobalVariables.FAIL)
                    {
                        if (clsGlobalVariables.Validateaccuracytestbackcolor)
                        {
                            if (_TestresultDevice2 != null)
                            {
                                if (UpdateTestResult(clsGlobalVariables.accuracyTests, clsGlobalVariables.enmpointcalibration, _TestresultDevice2))
                                {
                                    BackcolorDevice2 = BgColorgreen;
                                }
                                else
                                    BackcolorDevice2 = BgColorred;
                            }
                            else
                                BackcolorDevice2 = BgColorred;

                        }
                        else
                            BackcolorDevice2 = BgColororange;
                    }
                    else
                        BackcolorDevice2 = BgColorred;

                    if (BackcolorDevice2 == BgColorred)
                        clsGlobalVariables.listAccTest.Add(false);
                }

                OnPropertyChanged("TestresultDevice2");
            }
        }

        private string _TestresultDevice3;

        public string TestresultDevice3
        {
            get { return _TestresultDevice3; }
            set
            {
                _TestresultDevice3 = value;

                if (_TestresultDevice3 != null)
                {
                    if (_TestresultDevice3 != clsGlobalVariables.FAIL)
                    {
                        if (clsGlobalVariables.Validateaccuracytestbackcolor)
                        {
                            if (_TestresultDevice3 != null)
                            {
                                if (UpdateTestResult(clsGlobalVariables.accuracyTests, clsGlobalVariables.enmpointcalibration, _TestresultDevice3))
                                {
                                    BackcolorDevice3 = BgColorgreen;
                                }
                                else
                                    BackcolorDevice3 = BgColorred;
                            }
                            else
                                BackcolorDevice3 = BgColorred;

                        }
                        else
                            BackcolorDevice3 = BgColororange;
                    }
                    else
                        BackcolorDevice3 = BgColorred;

                    if (BackcolorDevice3 == BgColorred)
                        clsGlobalVariables.listAccTest.Add(false);
                }

                OnPropertyChanged("TestresultDevice3");
            }
        }

        private string _TestresultDevice4;

        public string TestresultDevice4
        {
            get { return _TestresultDevice4; }
            set
            {
                _TestresultDevice4 = value;

                if (_TestresultDevice4 != null)
                {
                    if (_TestresultDevice4 != clsGlobalVariables.FAIL)
                    {
                        if (clsGlobalVariables.Validateaccuracytestbackcolor)
                        {
                            if (_TestresultDevice4 != null)
                            {
                                if (UpdateTestResult(clsGlobalVariables.accuracyTests, clsGlobalVariables.enmpointcalibration, _TestresultDevice4))
                                {
                                    BackcolorDevice4 = BgColorgreen;
                                }
                                else
                                    BackcolorDevice4 = BgColorred;
                            }
                            else
                                BackcolorDevice4 = BgColorred;

                        }
                        else
                            BackcolorDevice4 = BgColororange;
                    }
                    else
                        BackcolorDevice4 = BgColorred;

                    if (BackcolorDevice4 == BgColorred)
                        clsGlobalVariables.listAccTest.Add(false);
                }

                OnPropertyChanged("TestresultDevice4");
            }
        }

        private string _TestresultDevice5;

        public string TestresultDevice5
        {
            get { return _TestresultDevice5; }
            set
            {
                _TestresultDevice5 = value;

                if (_TestresultDevice5 != null)
                {
                    if (_TestresultDevice5 != clsGlobalVariables.FAIL)
                    {
                        if (clsGlobalVariables.Validateaccuracytestbackcolor)
                        {
                            if (_TestresultDevice5 != null)
                            {
                                if (UpdateTestResult(clsGlobalVariables.accuracyTests, clsGlobalVariables.enmpointcalibration, _TestresultDevice5))
                                {
                                    BackcolorDevice5 = BgColorgreen;
                                }
                                else
                                    BackcolorDevice5 = BgColorred;
                            }
                            else
                                BackcolorDevice5 = BgColorred;

                        }
                        else
                            BackcolorDevice5 = BgColororange;
                    }
                    else
                        BackcolorDevice5 = BgColorred;

                    if (BackcolorDevice5 == BgColorred)
                        clsGlobalVariables.listAccTest.Add(false);
                }

                OnPropertyChanged("TestresultDevice5");
            }
        }

        private string _TestresultDevice6;

        public string TestresultDevice6
        {
            get { return _TestresultDevice6; }
            set
            {
                _TestresultDevice6 = value;

                if (_TestresultDevice6 != null)
                {
                    if (_TestresultDevice6 != clsGlobalVariables.FAIL)
                    {
                        if (clsGlobalVariables.Validateaccuracytestbackcolor)
                        {
                            if (_TestresultDevice6 != null)
                            {
                                if (UpdateTestResult(clsGlobalVariables.accuracyTests, clsGlobalVariables.enmpointcalibration, _TestresultDevice6))
                                {
                                    BackcolorDevice6 = BgColorgreen;
                                }
                                else
                                    BackcolorDevice6 = BgColorred;
                            }
                            else
                                BackcolorDevice6 = BgColorred;

                        }
                        else
                            BackcolorDevice6 = BgColororange;
                    }
                    else
                        BackcolorDevice6 = BgColorred;

                    if (BackcolorDevice6 == BgColorred)
                        clsGlobalVariables.listAccTest.Add(false);
                }

                OnPropertyChanged("TestresultDevice6");
            }
        }


        public bool UpdateTestResult(IList<AccuracyTests> accuracyTests, clsGlobalVariables.Enmpointcalibration enmpointcalibration, string output)
        {
            foreach (var test in MainWindowVM.accuracyTestsName)
            {
                if (test == accuracyTests)
                {
                        
                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P1)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P1) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P1) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }

                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P2)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P2) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P2) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

                    }

                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P3)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P3) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P3) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

                    }

                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P4)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P4) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P4) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

                    }

                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P5)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P5) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P5) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

                    }

                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P6)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P6) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P6) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

                    }

                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P7)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P7) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P7) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

                    }

                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P8)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P8) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P8) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

                    }

                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P9)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P9) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P9) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

                    }

                    if (enmpointcalibration == clsGlobalVariables.Enmpointcalibration.P10)
                    {
                        if (output != null)
                        {
                            if ((Convert.ToDouble(test[0].P10) - Convert.ToDouble(test[0].Tolerance) <= Convert.ToDouble(output)) &&
                            (Convert.ToDouble(test[0].P10) + Convert.ToDouble(test[0].Tolerance) >= Convert.ToDouble(output)))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

                    }
                }
            }

            return false;
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
