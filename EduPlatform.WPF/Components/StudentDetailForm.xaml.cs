using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace EduPlatform.WPF.Components
{
    /// <summary>
    /// Interaction logic for StudentDetailForm.xaml
    /// </summary>
    public partial class StudentDetailForm
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
