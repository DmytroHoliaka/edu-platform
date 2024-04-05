using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.StudentsViewModels
{
    public class CreateStudentViewModel : ViewModelBase
    {
        public StudentDetailsFormViewModel StudentDetailsFormVM { get; }

        public CreateStudentViewModel(GroupStore groupStore,
                                      ModalNavigationStore modalNavigationStore,
                                      GroupSequenceViewModel groupSequenceVM)
        {
            ICommand? submitCommand = null;
            ICommand? cancelCommand = null;

            StudentDetailsFormVM =
                new(Guid.NewGuid(), groupSequenceVM, submitCommand, cancelCommand);
        }
    }
}
