using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
using PR69_PI_Calibration_and_Functional_Jig.Model;
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
using System.Windows.Shapes;

namespace PR69_PI_Calibration_and_Functional_Jig.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            InitializeComponent();
            vm = (ConfigurationWindowVM)DataContext;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        ConfigurationWindowVM vm = new ConfigurationWindowVM();

        private void MainTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (MainTreeView.SelectedItem != null)
            {
                if (MainTreeView.SelectedItem.GetType() == typeof(CatIdList))
                {
                    CatIdList _catList = new CatIdList();
                    _catList = (CatIdList)MainTreeView.SelectedItem;
                    clsGlobalVariables.SelectedDeviceNameOfTreeView = _catList.DeviceName;
                }
                vm.IsSaveBtnVis = false;
                vm.AssignDataToFields(MainTreeView.SelectedItem);

            }
        }
 
    }   

}
