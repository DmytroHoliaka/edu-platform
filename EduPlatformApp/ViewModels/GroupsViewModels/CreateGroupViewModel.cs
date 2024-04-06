using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.GroupCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.TeachersViewModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class CreateGroupViewModel : ViewModelBase
    {
        public GroupDetailsFormViewModel GroupDetailsFormVM { get; }

        public CreateGroupViewModel(GroupStore groupStore,
                                    ModalNavigationStore modalNavigationStore,
                                    TeacherSequenceViewModel teacherSequenceVM,
                                    StudentSequenceViewModel studentSequenceVM)
        {
            ICommand? submitCommand = 
                new SubmitCreateGroupCommand(groupStore, this, modalNavigationStore);

            ICommand? cancelCommand = 
                new CloseFormCommand(modalNavigationStore);

            GroupDetailsFormVM = 
                new(Guid.NewGuid(), teacherSequenceVM, studentSequenceVM, submitCommand, cancelCommand);
        }
    }
}
