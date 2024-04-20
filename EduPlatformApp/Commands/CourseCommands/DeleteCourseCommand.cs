using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class DeleteCourseCommand(
        CourseStore courseStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public CourseViewModel? DeletingCourse { get; set; }

        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

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