using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.CoursesViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
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
