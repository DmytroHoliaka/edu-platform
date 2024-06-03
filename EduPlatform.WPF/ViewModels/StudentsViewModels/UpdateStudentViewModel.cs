using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.StudentCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.StudentsViewModels
{
    public class UpdateStudentViewModel : ViewModelBase
    {
        public StudentDetailsFormViewModel StudentDetailsFormVM { get; }

        public UpdateStudentViewModel(StudentStore studentStore,
                                      StudentViewModel selectedStudent,
                                      ViewStore viewStore,
                                      ModalNavigationStore modalNavigationStore,
                                      GroupSequenceViewModel groupSequenceVM)
        {
            ICommand submitCommand = new SubmitUpdateStudentCommand(selectedStudent, modalNavigationStore, studentStore);
            ICommand cancelCommand = new CloseFormCommand(modalNavigationStore);

            StudentDetailsFormVM = new(groupSequenceVM, submitCommand, cancelCommand)
            {
                FirstName = selectedStudent.FirstName,
                LastName = selectedStudent.LastName,
            };

            StudentDetailsFormVM.GroupVMs
                .Where(gvm => gvm.GroupId == selectedStudent.GroupId)
                .ToList()
                .ForEach(gvm => gvm.IsChecked = true);

            ((SubmitUpdateStudentCommand)submitCommand).FormDetails = StudentDetailsFormVM;
            viewStore.UnfocuseStudent();
        }
    }
}
