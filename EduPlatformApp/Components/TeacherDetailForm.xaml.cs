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
            CheckBox checkBox = (CheckBox)sender;

            if (checkBox.IsChecked == false)
            {
                checkBox.IsEnabled = true;
            }
        }
    }
}
