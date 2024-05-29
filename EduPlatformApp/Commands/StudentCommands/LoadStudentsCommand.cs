using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class LoadStudentsCommand(StudentStore studentStore, StudentSequenceViewModel studentSequenceViewModel) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await studentStore.Load();
            }
            catch (Exception)
            {
                studentSequenceViewModel.ErrorMessage = "Unable to load student data from database";
            }
        }
    }
}
