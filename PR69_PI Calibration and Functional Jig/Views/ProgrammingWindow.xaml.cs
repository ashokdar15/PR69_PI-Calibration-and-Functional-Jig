﻿using PR69_PI_Calibration_and_Functional_Jig.HelperClasses;
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
    /// Interaction logic for ProgrammingWindow.xaml
    /// </summary>
    public partial class ProgrammingWindow : Window
    {
        ProgrammingWindowVM vm = null;
        public ProgrammingWindow()
        {
            InitializeComponent();
            vm = (ProgrammingWindowVM)DataContext;
            clsGlobalVariables.ObjprogVM = vm;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
