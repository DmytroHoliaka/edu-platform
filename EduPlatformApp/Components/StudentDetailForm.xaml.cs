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
using System.Windows.Threading;

namespace EduPlatform.WPF.Components
{
    /// <summary>
    /// Interaction logic for StudentDetailForm.xaml
    /// </summary>
    public partial class StudentDetailForm : UserControl
    {
        public StudentDetailForm()
        {
            InitializeComponent();
        }

        private void RadioButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.IsChecked == true)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    radioButton.IsChecked = false;
                }), DispatcherPriority.Input);

                e.Handled = true;
            }
        }
    }
}
