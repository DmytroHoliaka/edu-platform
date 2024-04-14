using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.CoursesViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class OpenCreateCourseFormCommand : CommandBase
    {
        private readonly CourseStore _courseStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GroupSequenceViewModel _groupSequenceVM;

        public OpenCreateCourseFormCommand(CourseStore courseStore,
                                           ViewStore viewStore,
                                           ModalNavigationStore modalNavigationStore,
                                           GroupSequenceViewModel groupSequenceVM)
        {
            _courseStore = courseStore;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;
            _groupSequenceVM = groupSequenceVM;
        }

        public override void Execute(object? parameter)
        {
            CreateCourseViewModel createCourseVM =
                new(_courseStore, _modalNavigationStore, _groupSequenceVM);

            _modalNavigationStore.CurrentViewModel = createCourseVM;
            _viewStore.UnfocuseCourse();
        }
    }
}
