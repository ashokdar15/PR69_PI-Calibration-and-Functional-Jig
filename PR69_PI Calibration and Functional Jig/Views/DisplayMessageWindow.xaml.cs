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

        DisplayMessageVM vm = null;
        public DisplayMessageWindow(string title, string dispImg, string dispMsg)
        {
            InitializeComponent();
            vm = (DisplayMessageVM)DataContext;
            vm.TitleImgMsg = title;
            vm.DisplayImgPath = dispImg;
            vm.MsgDescription = dispMsg;
            SetImg.Visibility = Visibility.Visible;
            OKBtn.Focus();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
