using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.StudentCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.StudentsViewModels
{
    public class CreateStudentViewModel : ViewModelBase
    {
        public StudentDetailsFormViewModel StudentDetailsFormVM { get; }

        public CreateStudentViewModel(StudentStore studentStore,
                                      ModalNavigationStore modalNavigationStore,
                                      GroupSequenceViewModel groupSequenceVM)
        {
            ICommand submitCommand = new SubmitCreateStudentCommand(studentStore, modalNavigationStore);
            ICommand cancelCommand = new CloseFormCommand(modalNavigationStore);

            StudentDetailsFormVM =
                new(null, groupSequenceVM, submitCommand, cancelCommand);

            ((SubmitCreateStudentCommand)submitCommand).FormDetails = StudentDetailsFormVM;
        }
    }
}
