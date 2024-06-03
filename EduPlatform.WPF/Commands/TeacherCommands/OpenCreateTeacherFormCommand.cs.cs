using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class OpenCreateTeacherFormCommand(
        TeacherStore teacherStore,
        ViewStore viewStore,
        ModalNavigationStore modalNavigationStore,
        GroupSequenceViewModel groupSequenceVM)
        : CommandBase
    {
        public override void Execute(object? parameter)
        {
            CreateTeacherViewModel createTeacherVM =
                new(teacherStore, modalNavigationStore, groupSequenceVM);

            modalNavigationStore.CurrentViewModel = createTeacherVM;
            viewStore.UnfocuseTeacher();
        }
    }
}
