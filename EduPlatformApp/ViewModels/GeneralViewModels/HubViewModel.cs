using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GeneralViewModels.OverviewViewModel;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.NavigationsViewModel;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModel;

namespace EduPlatform.WPF.ViewModels.GeneralViewModels
{
    public class HubViewModel : ViewModelBase
    {
        public NavigationViewModel NavigationVM { get; }
        public EduPlatformOverviewViewModel EduPlatformOverviewVM { get; }
        public CourseSequenceViewModel CourseSequenceVM { get; }
        public GroupSequenceViewModel GroupSequenceVM { get; }
        public StudentSequenceViewModel StudentSequenceVM { get; }
        public TeacherSequenceViewModel TeacherSequenceVM { get; }

        public HubViewModel
        (
            CourseStore courseStore,
            GroupStore groupStore,
            StudentStore studentStore,
            TeacherStore teacherStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            EduPlatformOverviewVM = new();
            NavigationVM = new();

            GroupSequenceVM = new
            (
                groupStore,
                viewStore,
                modalNavigationStore
            );

            CourseSequenceVM = new
            (
                courseStore,
                viewStore,
                modalNavigationStore
            );

            TeacherSequenceVM = new
            (
                teacherStore,
                viewStore,
                modalNavigationStore
            );

            StudentSequenceVM = new
            (
                studentStore,
                viewStore,
                modalNavigationStore
            );

            GroupSequenceVM.SetCourseSequence(CourseSequenceVM);
            GroupSequenceVM.SetStudentSequence(StudentSequenceVM);
            GroupSequenceVM.SetTeacherSequence(TeacherSequenceVM);
            GroupSequenceVM.ConfigureCommands();
            //GroupSequenceVM.InsertTestData();

            CourseSequenceVM.SetGroupSequence(GroupSequenceVM);
            CourseSequenceVM.ConfigureCommands();
            //CourseSequenceVM.InsertTestData();

            TeacherSequenceVM.SetGroupSequence(GroupSequenceVM);
            TeacherSequenceVM.ConfigureCommands();
            //TeacherSequenceVM.InsertTestData();

            StudentSequenceVM.SetGroupSequence(GroupSequenceVM);
            StudentSequenceVM.ConfigureCommands();
            //StudentSequenceVM.InsertTestData();

            StudentSequenceVM.LoadStudents();
        }
    }
}
