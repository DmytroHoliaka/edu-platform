using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class OpenUpdateTeacherFormCommand : CommandBase
    {
        public TeacherViewModel? UpdatingTeacher { get; set; }

        private readonly TeacherStore _teacherStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GroupSequenceViewModel _groupSequenceVM;


        public OpenUpdateTeacherFormCommand(TeacherStore teacherStore,
                                            TeacherViewModel? selectedTeacher,
                                            ViewStore viewStore,
                                            ModalNavigationStore modalNavigationStore,
                                            GroupSequenceViewModel groupSequenceVM)
        {
            _teacherStore = teacherStore;
            UpdatingTeacher = selectedTeacher;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;
            _groupSequenceVM = groupSequenceVM;
        }

        public override bool CanExecute(object? parameter)
        {
            return UpdatingTeacher is not null;
        }

        public override void Execute(object? parameter)
        {
            UpdateTeacherViewModel createTeacherVM =
                new(_teacherStore, UpdatingTeacher!, _viewStore, _modalNavigationStore, _groupSequenceVM);

            _modalNavigationStore.CurrentViewModel = createTeacherVM;
            _viewStore.UnfocuseStudent();
        }
    }
}
