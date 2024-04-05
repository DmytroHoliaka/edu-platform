using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels.OverviewViewModel;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.NavigationsViewModel;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.TeachersViewModel;

namespace EduPlatform.WPF.ViewModels.GeneralViewModels
{
    public class EduPlatformViewModel : ViewModelBase
    {
        public NavigationViewModel NavigationVM { get; }
        public EduPlatformOverviewViewModel EduPlatformOverviewVM { get; }
        public GroupSequenceViewModel GroupSequenceVM { get; }
        public StudentSequenceViewModel StudentSequenceVM { get; }
        public TeacherSequenceViewModel TeacherSequenceVM { get; }

        public EduPlatformViewModel
        (
            GroupStore groupStore, 
            StudentStore studentStore, 
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

            StudentSequenceVM = new
            (
                studentStore,
                viewStore,
                modalNavigationStore
            );

            TeacherSequenceVM = new();

            GroupSequenceVM.SetStudentSequence(StudentSequenceVM);
            GroupSequenceVM.SetTeacherSequence(TeacherSequenceVM);
            GroupSequenceVM.ConfigureCommands();
            GroupSequenceVM.InsertTestData();

            StudentSequenceVM.SetGroupSequence(GroupSequenceVM);
            StudentSequenceVM.ConfigureCommands();
        }
    }
}
