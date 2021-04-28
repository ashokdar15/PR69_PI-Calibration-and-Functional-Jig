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
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public PasswordWindow()
        {
            InitializeComponent();
            PwdBx.Focus();
        }

        private void PwdBx_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                OkBtn_Click(this, e);
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            int hh = Convert.ToInt32(date.ToString("HH"));
            int min = Convert.ToInt32(date.ToString("mm"));
            string MIN,HH;
            MIN = min < 10 ? "0" + min.ToString() : min.ToString();
            HH = hh < 10 ? "0" + hh.ToString() : hh.ToString();
            if (PwdBx.Password == "gicpr69" + HH + MIN)
            {
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
            this.Close();


        }
    }
}
