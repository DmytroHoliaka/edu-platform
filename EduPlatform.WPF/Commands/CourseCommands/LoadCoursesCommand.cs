using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;

namespace EduPlatform.WPF.Commands.CourseCommands
{
    public class LoadCoursesCommand(CourseStore courseStore, CourseSequenceViewModel courseSequenceViewModel) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await courseStore.Load();
            }
            catch (Exception)
            {
                courseSequenceViewModel.ErrorMessage = "Unable to load course data from database";
            }
        }
    }
}
