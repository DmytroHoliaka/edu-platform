using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Windows.Input;
using EduPlatform.WPF.Commands.CourseCommands;

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
