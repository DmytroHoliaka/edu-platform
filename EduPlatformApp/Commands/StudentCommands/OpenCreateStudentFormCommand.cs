using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class OpenCreateStudentFormCommand(
        StudentStore studentStore,
        ViewStore viewStore,
        ModalNavigationStore modalNavigationStore,
        GroupSequenceViewModel groupSequenceVM)
        : CommandBase
    {
        public override void Execute(object? parameter)
        {
            CreateStudentViewModel createStudentVM =
                new(studentStore, modalNavigationStore, groupSequenceVM);

            modalNavigationStore.CurrentViewModel = createStudentVM;
            viewStore.UnfocuseStudent();
        }
    }
}
