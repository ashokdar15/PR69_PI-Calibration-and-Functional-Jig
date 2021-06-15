using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PR69_PI_Calibration_and_Functional_Jig.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowVM vm = null;
        public MainWindow()
        {
            InitializeComponent();

            this.Width = System.Windows.SystemParameters.WorkArea.Width;
            this.Height = System.Windows.SystemParameters.WorkArea.Height;
            this.Left = 0;
            this.Top = 0;
            this.WindowState = WindowState.Normal;

            vm = (MainWindowVM)DataContext;
            clsGlobalVariables.mainWindowVM = vm;

        }

        public ScrollViewer GetScrollViewer()
        {
            

            if (VisualTreeHelper.GetChildrenCount(this) == 0)
            {
                return null;
            }
            var x = VisualTreeHelper.GetChild(this, 0);

            if (x == null)
            {
                return null;
            }

            if (VisualTreeHelper.GetChildrenCount (x) == 0)
            {
                return null;
            }

            return VisualTreeHelper.GetChild(x, 0) as ScrollViewer;

            
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DG1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  if (DG1.SelectedItem != null)
             //   DG1.ScrollIntoView(DG1.SelectedItem);
        }

        private void DG2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // if (DG2.SelectedItem != null)
             //   DG2.ScrollIntoView(DG2.SelectedItem);
        }

        private void MydataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG1.SelectedItem != null)
              DG1.ScrollIntoView(DG1.SelectedItem);
        }
    }
}
