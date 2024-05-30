using System.Windows;
using System.Windows.Controls;

namespace EduPlatform.WPF.Components
{
    /// <summary>
    /// Interaction logic for TeacherDetailForm.xaml
    /// </summary>
    public partial class TeacherDetailForm : UserControl
    {
        public TeacherDetailForm()
        {
            InitializeComponent();
        }

        private void CheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.IsChecked == false)
                {
                    checkBox.IsEnabled = true;
                } 
            }
        }
    }
}
