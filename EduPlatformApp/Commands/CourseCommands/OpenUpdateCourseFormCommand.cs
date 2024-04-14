using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class OpenUpdateCourseFormCommand : CommandBase
    {
        public CourseViewModel? UpdatingCourse { get; set; }

        private readonly CourseStore _courseStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GroupSequenceViewModel _groupSequenceVM;


        public OpenUpdateCourseFormCommand(CourseStore courseStore,
                                           CourseViewModel? selectedCourse,
                                           ViewStore viewStore,
                                           ModalNavigationStore modalNavigationStore,
                                           GroupSequenceViewModel groupSequenceVM)
        {
            _courseStore = courseStore;
            UpdatingCourse = selectedCourse;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;
            _groupSequenceVM = groupSequenceVM;
        }

        public override bool CanExecute(object? parameter)
        {
            return UpdatingCourse is not null;
        }

        public override void Execute(object? parameter)
        {
            UpdateCourseViewModel createCourseVM =
                new(_courseStore, UpdatingCourse!, _viewStore, _modalNavigationStore, _groupSequenceVM);

            _modalNavigationStore.CurrentViewModel = createCourseVM;
            _viewStore.UnfocuseCourse();
        }
    }
}
