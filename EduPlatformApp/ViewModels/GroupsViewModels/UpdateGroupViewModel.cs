using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.GroupCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.TeachersViewModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class UpdateGroupViewModel : ViewModelBase
    {
        public GroupDetailsFormViewModel GroupDetailsFormVM { get; }

        public UpdateGroupViewModel
        (
            GroupViewModel selectedGroup,
            ModalNavigationStore modalNavigationStore,
            TeacherSequenceViewModel teacherSequenceVM,
            StudentSequenceViewModel studentSequenceVM,
            GroupStore groupStore
        )
        {
            ICommand? submitCommand = new SubmitUpdateGroupCommand(
                modalNavigationStore,
                groupStore,
                selectedGroup,
                this);

            ICommand? cancelCommand = new CloseFormCommand(modalNavigationStore);

            GroupDetailsFormVM = new
            (
                selectedGroup.GroupId,
                teacherSequenceVM,
                studentSequenceVM,
                submitCommand,
                cancelCommand
            )
            {
                GroupName = selectedGroup.GroupName,
            };

            GroupDetailsFormVM.TeacherVMs
                .Where(tvm => selectedGroup.GroupTeachers.Any(t => t.TeacherId == tvm.TeacherId))
                .ToList()
                .ForEach(tvm => tvm.IsChecked = true);

            GroupDetailsFormVM.StudentVMs
                .Where(svm => selectedGroup.GroupStudents.Any(s => s.StudentId == svm.StudentId))
                .ToList()
                .ForEach(svm => svm.IsChecked = true);
        }
    }
}
