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
    /// Interaction logic for DisplayMessage.xaml
    /// </summary>
    public partial class DisplayMessage : Window
    {
        public DisplayMessage()
        {
            InitializeComponent();
        }

        public DisplayMessage(string Msg, string Msgtype)
        {
            InitializeComponent();
            switch (Msgtype)
            {
                case clsGlobalVariables.strNotifyMsg:
                    AndroidMsg.Visibility = Visibility.Visible;
                    ErrorMsg.Visibility = Visibility.Hidden;
                    QuestionmarkMsg.Visibility = Visibility.Hidden;
                    break;

                case clsGlobalVariables.strErrorMsg:
                    AndroidMsg.Visibility = Visibility.Hidden;
                    ErrorMsg.Visibility = Visibility.Visible;
                    QuestionmarkMsg.Visibility = Visibility.Hidden;
                    break;
                case clsGlobalVariables.strQuestionMsg:
                    AndroidMsg.Visibility = Visibility.Hidden;
                    ErrorMsg.Visibility = Visibility.Hidden;
                    QuestionmarkMsg.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
            this.txtMsg.Text = Msg;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
