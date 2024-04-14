using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.StudentCommands;
using EduPlatform.WPF.Commands.TeacherCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.CoursesViewModels
{
    public class CreateCourseViewModel : ViewModelBase
    {
        public CourseDetailsFormViewModel CourseDetailsFormVM { get; }

        public CreateCourseViewModel(CourseStore courseStore,
                                     ModalNavigationStore modalNavigationStore,
                                     GroupSequenceViewModel groupSequenceVM)
        {
            ICommand submitCommand = new SubmitCreateCourseCommand(courseStore, modalNavigationStore);
            ICommand cancelCommand = new CloseFormCommand(modalNavigationStore);

            CourseDetailsFormVM =
                new(groupSequenceVM, submitCommand, cancelCommand);

            ((SubmitCreateCourseCommand)submitCommand).FormDetails = CourseDetailsFormVM;
        }
    }
}
