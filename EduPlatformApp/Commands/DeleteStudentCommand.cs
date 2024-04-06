using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands
{
    public class DeleteStudentCommand : AsyncCommandBase
    {
        public StudentViewModel? DeletingStudent { get; set; }

        private readonly StudentStore _studentStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public DeleteStudentCommand(StudentStore studentStore,
                                    ModalNavigationStore modalNavigationStore)
        {
            _studentStore = studentStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override bool CanExecute(object? parameter)
        {
            return DeletingStudent is not null;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // ToDo: Add store in database
            if (DeletingStudent is null)
            {
                return;
            }

            Guid studentId = DeletingStudent.StudentId;

            try
            {
                await _studentStore.Delete(studentId);
                _modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
