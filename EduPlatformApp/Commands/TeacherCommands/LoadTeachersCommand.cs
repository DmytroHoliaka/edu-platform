using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class LoadTeachersCommand(TeacherStore teacherStore) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await teacherStore.Load();
            }
            catch (Exception)
            {
                // ToDo: Write correct exception handling
                throw;
            }
        }
    }
}
