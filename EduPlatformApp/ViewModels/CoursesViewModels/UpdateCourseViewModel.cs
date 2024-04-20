using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.TeacherCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Windows.Input;
using EduPlatform.WPF.Commands.CourseCommands;

namespace EduPlatform.WPF.ViewModels.CoursesViewModels
{
    public class UpdateCourseViewModel : ViewModelBase
    {
        public CourseDetailsFormViewModel CourseDetailsFormVM { get; }

        public UpdateCourseViewModel(CourseStore courseStore,
                                     CourseViewModel selectedCourse,
                                     ViewStore viewStore,
                                     ModalNavigationStore modalNavigationStore,
                                     GroupSequenceViewModel groupSequenceVM)
        {
            ICommand submitCommand = new SubmitUpdateCourseCommand(selectedCourse, modalNavigationStore, courseStore);
            ICommand cancelCommand = new CloseFormCommand(modalNavigationStore);

            CourseDetailsFormVM = new(groupSequenceVM, submitCommand, cancelCommand)
            {
                CourseName = selectedCourse.CourseName,
                Description = selectedCourse.Description
            };

            CourseDetailsFormVM.GroupVMs
                .Where(gvm => selectedCourse.Groups.Any(g => g.GroupId == gvm.GroupId))
                .ToList()
                .ForEach(gvm => gvm.IsChecked = true);

            ((SubmitUpdateCourseCommand)submitCommand).FormDetails = CourseDetailsFormVM;
            viewStore.UnfocuseCourse();
        }
    }
}
