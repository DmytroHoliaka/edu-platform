using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands
{
    public class OpenCreateStudentFormCommand : CommandBase
    {
        private readonly GroupStore _groupStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GroupSequenceViewModel _groupSequenceVM;

        public OpenCreateStudentFormCommand(GroupStore groupStore,
                                            ViewStore viewStore,
                                            ModalNavigationStore modalNavigationStore,
                                            GroupSequenceViewModel groupSequenceVM)
        {
            _groupStore = groupStore;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;
            _groupSequenceVM = groupSequenceVM;
        }

        public override void Execute(object? parameter)
        {
            CreateStudentViewModel createStudentVM =
                new(_groupStore, _modalNavigationStore, _groupSequenceVM);

            _modalNavigationStore.CurrentViewModel = createStudentVM;
            _viewStore.UnfocuseGroup();
        }
    }
}
