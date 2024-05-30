using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModel;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class OpenCreateGroupFormCommand(
        GroupStore groupStore,
        ViewStore viewStore,
        ModalNavigationStore modalNavigationStore,
        CourseSequenceViewModel? courseSequenceVM,
        TeacherSequenceViewModel? teacherSequenceVM,
        StudentSequenceViewModel? studentSequenceVM)
        : CommandBase
    {
        public override void Execute(object? parameter)
        {
            CreateGroupViewModel createGroupViewModel = new(groupStore,
                                                            modalNavigationStore,
                                                            courseSequenceVM,
                                                            teacherSequenceVM,
                                                            studentSequenceVM);

            modalNavigationStore.CurrentViewModel = createGroupViewModel;
            viewStore.UnfocuseGroup();
        }
    }
}
