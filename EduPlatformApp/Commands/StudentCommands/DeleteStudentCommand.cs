using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class DeleteStudentCommand(StudentStore studentStore) : AsyncCommandBase
    {
        public StudentViewModel? DeletingStudent { get; set; }

        public override bool CanExecute(object? parameter)
        {
            return DeletingStudent is not null;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Guid studentId = DeletingStudent!.StudentId;

            try
            {
                await studentStore.Delete(studentId);
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
