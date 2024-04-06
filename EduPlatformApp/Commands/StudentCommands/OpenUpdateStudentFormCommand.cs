using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class OpenUpdateStudentFormCommand : CommandBase
    {
        public StudentViewModel? UpdatingStudent { get; set; }

        private readonly StudentStore _studentStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GroupSequenceViewModel _groupSequenceVM;


        public OpenUpdateStudentFormCommand(StudentStore studentStore,
                                            StudentViewModel? selectedStudent,
                                            ViewStore viewStore,
                                            ModalNavigationStore modalNavigationStore,
                                            GroupSequenceViewModel groupSequenceVM)
        {
            _studentStore = studentStore;
            UpdatingStudent = selectedStudent;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;
            _groupSequenceVM = groupSequenceVM;
        }

        public override bool CanExecute(object? parameter)
        {
            return UpdatingStudent is not null;
        }

        public override void Execute(object? parameter)
        {
            UpdateStudentViewModel createStudentVM =
                new(_studentStore, UpdatingStudent!, _viewStore, _modalNavigationStore, _groupSequenceVM);

            _modalNavigationStore.CurrentViewModel = createStudentVM;
            _viewStore.UnfocuseStudent();
        }
    }
}
