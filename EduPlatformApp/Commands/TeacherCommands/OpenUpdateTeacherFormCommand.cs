using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class OpenUpdateTeacherFormCommand(
        TeacherStore teacherStore,
        TeacherViewModel? selectedTeacher,
        ViewStore viewStore,
        ModalNavigationStore modalNavigationStore,
        GroupSequenceViewModel groupSequenceVM)
        : CommandBase
    {
        public TeacherViewModel? UpdatingTeacher { get; set; } = selectedTeacher;


        public override bool CanExecute(object? parameter)
        {
            return UpdatingTeacher is not null;
        }

        public override void Execute(object? parameter)
        {
            UpdateTeacherViewModel createTeacherVM =
                new(teacherStore, UpdatingTeacher!, viewStore, modalNavigationStore, groupSequenceVM);

            modalNavigationStore.CurrentViewModel = createTeacherVM;
            viewStore.UnfocuseTeacher();
        }
    }
}
