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

        public EduPlatformViewModel(GroupStore groupStore, ViewStore viewStore, ModalNavigationStore modalNavigationStore)
        {
            EduPlatformOverviewVM = new();
            StudentSequenceVM = new();
            TeacherSequenceVM = new();
            NavigationVM = new();
            GroupSequenceVM = new
            (
                groupStore,
                viewStore,
                modalNavigationStore, 
                TeacherSequenceVM,
                StudentSequenceVM
            );
        }
    }
}
