using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.TeachersViewModel;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class OpenCreateGroupFormCommand : CommandBase
    {
        private readonly GroupStore _groupStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TeacherSequenceViewModel _teacherSequenceVM;
        private readonly StudentSequenceViewModel _studentSequenceVM;

        public OpenCreateGroupFormCommand(GroupStore groupStore,
                                          ViewStore viewStore,
                                          ModalNavigationStore modalNavigationStore,
                                          TeacherSequenceViewModel teacherSequenceVM,
                                          StudentSequenceViewModel studentSequenceVM)
        {
            _groupStore = groupStore;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;
            _teacherSequenceVM = teacherSequenceVM;
            _studentSequenceVM = studentSequenceVM;
        }

        public override void Execute(object? parameter)
        {
            CreateGroupViewModel createGroupViewModel =
                new(_groupStore, _modalNavigationStore, _teacherSequenceVM, _studentSequenceVM);

            _modalNavigationStore.CurrentViewModel = createGroupViewModel;
            _viewStore.UnfocuseGroup();
        }
    }
}
