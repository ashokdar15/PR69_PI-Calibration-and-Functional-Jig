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

namespace KeypadControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCMain : UserControl
    {

        TextBlock lbl = null;
        public UCMain()
        {
            InitializeComponent();
        }

        public string Lables
        {
            get { return (string)GetValue(LablesProperty); }
            set { SetValue(LablesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Lables.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LablesProperty =
            DependencyProperty.Register("Lables", typeof(string), typeof(UCMain), new PropertyMetadata("", new PropertyChangedCallback(Onlablechanged)));

        private static void Onlablechanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCMain userControl1 = d as UCMain;
            userControl1.onlablechanged(e);

        }

        private void onlablechanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                if (e.NewValue.ToString() != null && e.NewValue.ToString() != "")
                {

                    lbl = (new TextBlock()
                    {
                        Text = e.NewValue.ToString(),
                        Foreground = Brushes.White,
                        FontSize = 20,
                        FontWeight = FontWeights.Bold
                    });


                    lbl.Visibility = Visibility.Visible;
                    foreach (var item in canvas.Children)
                    {
                        if (item.GetType() == typeof(System.Windows.Controls.TextBlock))
                        {
                            canvas.Children.Remove((System.Windows.Controls.TextBlock)item);
                            break;
                        }
                    }
                    canvas.Children.Add(lbl);

                    Canvas.SetTop(lbl, 50);
                    Canvas.SetLeft(lbl, 68);

                }
            }
        }
    }
}
