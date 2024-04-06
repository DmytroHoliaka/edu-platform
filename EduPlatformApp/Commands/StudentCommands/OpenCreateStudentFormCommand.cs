using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class OpenCreateStudentFormCommand : CommandBase
    {
        private readonly StudentStore _studentStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GroupSequenceViewModel _groupSequenceVM;

        public OpenCreateStudentFormCommand(StudentStore studentStore,
                                            ViewStore viewStore,
                                            ModalNavigationStore modalNavigationStore,
                                            GroupSequenceViewModel groupSequenceVM)
        {
            _studentStore = studentStore;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;
            _groupSequenceVM = groupSequenceVM;
        }

        public override void Execute(object? parameter)
        {
            CreateStudentViewModel createStudentVM =
                new(_studentStore, _modalNavigationStore, _groupSequenceVM);

            _modalNavigationStore.CurrentViewModel = createStudentVM;
            _viewStore.UnfocuseStudent();
        }
    }
}
