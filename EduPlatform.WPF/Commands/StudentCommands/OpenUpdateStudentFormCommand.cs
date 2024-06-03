using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class OpenUpdateStudentFormCommand(
        StudentStore studentStore,
        StudentViewModel? selectedStudent,
        ViewStore viewStore,
        ModalNavigationStore modalNavigationStore,
        GroupSequenceViewModel groupSequenceVM)
        : CommandBase
    {
        public StudentViewModel? UpdatingStudent { get; set; } = selectedStudent;

        public override bool CanExecute(object? parameter)
        {
            return UpdatingStudent is not null;
        }

        public override void Execute(object? parameter)
        {
            UpdateStudentViewModel createStudentVM =
                new(studentStore, UpdatingStudent!, viewStore, modalNavigationStore, groupSequenceVM);

            modalNavigationStore.CurrentViewModel = createStudentVM;
            viewStore.UnfocuseStudent();
        }
    }
}