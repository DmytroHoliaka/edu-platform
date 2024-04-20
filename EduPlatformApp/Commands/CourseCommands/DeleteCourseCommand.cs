using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;

namespace EduPlatform.WPF.Commands.CourseCommands
{
    public class DeleteCourseCommand(CourseStore courseStore) : AsyncCommandBase
    {
        public CourseViewModel? DeletingCourse { get; set; }

        public override bool CanExecute(object? parameter)
        {
            return DeletingCourse is not null;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // ToDo: Add store in database
            Guid courseId = DeletingCourse!.CourseId;

            try
            {
                await courseStore.Delete(courseId);
            }
            catch (Exception)
            {
                /*ToDo: Write validation message*/
            }
        }
    }
}