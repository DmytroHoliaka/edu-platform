using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class DeleteStudentCommand(
        StudentStore studentStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public StudentViewModel? DeletingStudent { get; set; }

        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override bool CanExecute(object? parameter)
        {
            return DeletingStudent is not null;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // ToDo: Add store in database
            Guid studentId = DeletingStudent!.StudentId;

            try
            {
                await studentStore.Delete(studentId);
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
