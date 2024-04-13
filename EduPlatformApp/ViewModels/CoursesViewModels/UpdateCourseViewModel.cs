using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.StudentCommands;
using EduPlatform.WPF.Commands.TeacherCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.TeachersViewModels
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
            //ICommand submitCommand = new SubmitUpdateTeacherCommand(selectedCourse, modalNavigationStore, courseStore);
            //ICommand cancelCommand = new CloseFormCommand(modalNavigationStore);

            ICommand submitCommand = null;
            ICommand cancelCommand = null;

            CourseDetailsFormVM = new(groupSequenceVM, submitCommand, cancelCommand)
            {
                CourseName = selectedCourse.CourseName
            };

            CourseDetailsFormVM.GroupVMs
                .Where(gvm => selectedCourse.Groups.Any(g => g.GroupId == gvm.GroupId))
                .ToList()
                .ForEach(gvm => gvm.IsChecked = true);

            //((SubmitUpdateTeacherCommand)submitCommand).FormDetails = CourseDetailsFormVM;
            viewStore.UnfocuseCourse();
        }
    }
}
