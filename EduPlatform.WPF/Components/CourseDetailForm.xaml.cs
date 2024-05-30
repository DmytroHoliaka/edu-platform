using System.Windows;
using System.Windows.Controls;

namespace EduPlatform.WPF.Components
{
    /// <summary>
    /// Interaction logic for CourseDetailForm.xaml
    /// </summary>
    public partial class CourseDetailForm : UserControl
    {
        public CourseDetailForm()
        {
            InitializeComponent();
        }

        private void CheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.IsChecked == true)
                {
                    checkBox.IsEnabled = true;
                }
            }
        }
    }
}
