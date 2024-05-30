using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.CourseCommands
{
    public class OpenUpdateCourseFormCommand(
        CourseStore courseStore,
        CourseViewModel? selectedCourse,
        ViewStore viewStore,
        ModalNavigationStore modalNavigationStore,
        GroupSequenceViewModel groupSequenceVM)
        : CommandBase
    {
        public CourseViewModel? UpdatingCourse { get; set; } = selectedCourse;


        public override bool CanExecute(object? parameter)
        {
            return UpdatingCourse is not null;
        }

        public override void Execute(object? parameter)
        {
            UpdateCourseViewModel createCourseVM =
                new(courseStore, UpdatingCourse!, viewStore, modalNavigationStore, groupSequenceVM);

            modalNavigationStore.CurrentViewModel = createCourseVM;
            viewStore.UnfocuseCourse();
        }
    }
}
