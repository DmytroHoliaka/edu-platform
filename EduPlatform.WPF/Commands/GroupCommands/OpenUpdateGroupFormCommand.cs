using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class OpenUpdateGroupFormCommand(
        GroupStore groupStore,
        ViewStore viewStore,
        ModalNavigationStore modalNavigationStore,
        CourseSequenceViewModel? courseSequenceVM,
        TeacherSequenceViewModel? teacherSequenceVM,
        StudentSequenceViewModel? studentSequenceVM)
        : CommandBase
    {
        public GroupViewModel? UpdatingGroup { get; set; }

        public override bool CanExecute(object? parameter)
        {
            return UpdatingGroup is not null;
        }

        public override void Execute(object? parameter)
        {
            UpdateGroupViewModel updateGroupViewModel = new
            (
                UpdatingGroup!,
                modalNavigationStore,
                courseSequenceVM,
                teacherSequenceVM,
                studentSequenceVM,
                groupStore
            );

            modalNavigationStore.CurrentViewModel = updateGroupViewModel;
            viewStore.UnfocuseGroup();
        }
    }
}
