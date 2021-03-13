using PR69_PI_Calibration_and_Functional_Jig.ViewModel;
using System.Windows;
using static PR69_PI_Calibration_and_Functional_Jig.HelperClasses.clsGlobalVariables;

namespace PR69_PI_Calibration_and_Functional_Jig.Views
{
    /// <summary>
    /// Interaction logic for DisplayMessageWindow.xaml
    /// </summary>
    public partial class DisplayMessageWindow : Window
    {

        MainWindowVM vm = null;
        public DisplayMessageWindow(string dispMsg, string dispImg, string title)
        {
            InitializeComponent();
            vm = (MainWindowVM)DataContext;
            vm.TitleImgMsg = title;
            vm.DisplayImgPath = dispImg;
            vm.MsgDescription = dispMsg;
            SetImg.Visibility = Visibility.Visible;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
