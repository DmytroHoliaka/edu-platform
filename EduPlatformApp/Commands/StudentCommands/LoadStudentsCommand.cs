using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class LoadStudentsCommand(StudentStore studentStore) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await studentStore.Load();
            }
            catch (Exception)
            {
                // ToDo: Write correct exception handling
                throw;
            }
        }
    }
}
