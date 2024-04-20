using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EduPlatform.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ModalControl_ContentChanged(object sender, RoutedEventArgs e)
        {
            if (sender is ContentControl contentControl)
            {
                switch (contentControl.Content)
                {
                    case CreateCourseViewModel _:
                    case UpdateCourseViewModel _:
                    case CreateStudentViewModel _:
                    case UpdateStudentViewModel _:
                    case CreateTeacherViewModel _:
                    case UpdateTeacherViewModel _:
                        contentControl.Width = 490;
                        break;

                    case CreateGroupViewModel _:
                    case UpdateGroupViewModel _:
                        contentControl.Width = 940;
                        break;
                }
            }
        }
    }
}