using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PR69_PI_Calibration_and_Functional_Jig.Views
{
    /// <summary>
    /// Interaction logic for DisplayTest.xaml
    /// </summary>
    public partial class DisplayTest : Window
    {
        public DisplayTest(int retrycount)
        {
            InitializeComponent();

            if (retrycount >= 3)            
                Retry.Visibility = Visibility.Hidden;
            else
                Retry.Visibility = Visibility.Visible;

            BtnOkDispTest.Focus();
            Keyboard.Focus(BtnOkDispTest);
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            clsGlobalVariables.DisplayTestOk = true;
            clsGlobalVariables.DisplayTestFail = false;
            clsGlobalVariables.DisplayTestRetry = false;
            this.Close();
        }

        private void Retry_Click(object sender, RoutedEventArgs e)
        {
            clsGlobalVariables.DisplayTestOk = false;
            clsGlobalVariables.DisplayTestFail = false;
            clsGlobalVariables.DisplayTestRetry = true;
            this.Close();
        }

        private void Fail_Click(object sender, RoutedEventArgs e)
        {
            clsGlobalVariables.DisplayTestOk = false;
            clsGlobalVariables.DisplayTestFail = true;
            clsGlobalVariables.DisplayTestRetry = false;
            this.Close();
        }
    }
}
