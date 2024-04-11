using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.StudentCommands;
using EduPlatform.WPF.Commands.TeacherCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.TeachersViewModels
{
    public class CreateTeacherViewModel : ViewModelBase
    {
        public TeacherDetailsFormViewModel TeacherDetailsFormVM { get; }

        public CreateTeacherViewModel(TeacherStore teacherStore,
                                      ModalNavigationStore modalNavigationStore,
                                      GroupSequenceViewModel groupSequenceVM)
        {
            ICommand submitCommand = new SubmitCreateTeacherCommand(teacherStore, modalNavigationStore);
            ICommand cancelCommand = new CloseFormCommand(modalNavigationStore);

            TeacherDetailsFormVM =
                new(groupSequenceVM, submitCommand, cancelCommand);

            ((SubmitCreateTeacherCommand)submitCommand).FormDetails = TeacherDetailsFormVM;
        }
    }
}
