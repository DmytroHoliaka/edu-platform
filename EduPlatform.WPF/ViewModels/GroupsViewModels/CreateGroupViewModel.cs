using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.GroupCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System.Windows.Input;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class CreateGroupViewModel : ViewModelBase
    {
        public GroupDetailsFormViewModel GroupDetailsFormVM { get; }

        public CreateGroupViewModel(GroupStore groupStore,
            ModalNavigationStore modalNavigationStore,
            CourseSequenceViewModel? courseSequenceVM,
            TeacherSequenceViewModel? teacherSequenceVM,
            StudentSequenceViewModel? studentSequenceVM)
        {
            ICommand submitCommand =
                new SubmitCreateGroupCommand(groupStore, this, modalNavigationStore);

            ICommand cancelCommand =
                new CloseFormCommand(modalNavigationStore);

            GroupDetailsFormVM = new GroupDetailsFormViewModel(
                null,
                courseSequenceVM,
                teacherSequenceVM,
                studentSequenceVM,
                submitCommand,
                cancelCommand);
        }
    }
}