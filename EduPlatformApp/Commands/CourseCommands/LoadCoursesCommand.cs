using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;

namespace EduPlatform.WPF.Commands.CourseCommands
{
    public class LoadCoursesCommand(CourseStore courseStore) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await courseStore.Load();
            }
            catch (Exception)
            {
                // ToDo: Write correct exception handling
                throw;
            }
        }
    }
}
