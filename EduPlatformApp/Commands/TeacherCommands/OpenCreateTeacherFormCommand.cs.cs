using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class OpenCreateTeacherFormCommand : CommandBase
    {
        private readonly TeacherStore _teacherStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GroupSequenceViewModel _groupSequenceVM;

        public OpenCreateTeacherFormCommand(TeacherStore teacherStore,
                                            ViewStore viewStore,
                                            ModalNavigationStore modalNavigationStore,
                                            GroupSequenceViewModel groupSequenceVM)
        {
            _teacherStore = teacherStore;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;
            _groupSequenceVM = groupSequenceVM;
        }

        public override void Execute(object? parameter)
        {
            CreateTeacherViewModel createTeacherVM =
                new(_teacherStore, _modalNavigationStore, _groupSequenceVM);

            _modalNavigationStore.CurrentViewModel = createTeacherVM;
            _viewStore.UnfocuseStudent();
        }
    }
}
