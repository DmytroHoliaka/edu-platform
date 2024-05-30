using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.GroupCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System.Windows.Input;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class UpdateGroupViewModel : ViewModelBase
    {
        public GroupDetailsFormViewModel GroupDetailsFormVM { get; }

        public UpdateGroupViewModel
        (
            GroupViewModel selectedGroup,
            ModalNavigationStore modalNavigationStore,
            CourseSequenceViewModel? courseSequenceVM,
            TeacherSequenceViewModel? teacherSequenceVM,
            StudentSequenceViewModel? studentSequenceVM,
            GroupStore groupStore
        )
        {
            ICommand submitCommand = new SubmitUpdateGroupCommand(
                modalNavigationStore,
                groupStore,
                selectedGroup,
                this);

            ICommand cancelCommand = new CloseFormCommand(modalNavigationStore);

            GroupDetailsFormVM = new GroupDetailsFormViewModel(
                selectedGroup,
                courseSequenceVM,
                teacherSequenceVM,
                studentSequenceVM,
                submitCommand,
                cancelCommand
            )
            {
                GroupName = selectedGroup.GroupName,
            };

            
            GroupDetailsFormVM.CourseVMs
                .Where(cvm => cvm.CourseId == selectedGroup.CourseId)
                .ToList()
                .ForEach(cvm => cvm.IsChecked = true);

            GroupDetailsFormVM.TeacherVMs
                .Where(tvm => selectedGroup.TeacherVMs
                    .Any(selected => selected.TeacherId == tvm.TeacherId))
                .ToList()
                .ForEach(tvm => tvm.IsChecked = true);

            GroupDetailsFormVM.StudentVMs
                .Where(svm => selectedGroup.StudentVMs
                    .Any(selected => selected.StudentId == svm.StudentId))
                .ToList()
                .ForEach(svm => svm.IsChecked = true);
        }
    }
}
