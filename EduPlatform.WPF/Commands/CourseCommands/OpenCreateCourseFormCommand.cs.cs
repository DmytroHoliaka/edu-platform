using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.CourseCommands
{
    public class OpenCreateCourseFormCommand(
        CourseStore courseStore,
        ViewStore viewStore,
        ModalNavigationStore modalNavigationStore,
        GroupSequenceViewModel groupSequenceVM)
        : CommandBase
    {
        public override void Execute(object? parameter)
        {
            CreateCourseViewModel createCourseVM =
                new(courseStore, modalNavigationStore, groupSequenceVM);

            modalNavigationStore.CurrentViewModel = createCourseVM;
            viewStore.UnfocuseCourse();
        }
    }
}
