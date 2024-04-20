using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class DeleteTeacherCommand(
        TeacherStore teacherStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public TeacherViewModel? DeletingTeacher { get; set; }

        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override bool CanExecute(object? parameter)
        {
            return
                DeletingTeacher is not null && 
                DeletingTeacher.Groups.Any(g => g.Teachers.Count <= 1) == false; 
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // ToDo: Add store in database

            Guid teacherId = DeletingTeacher!.TeacherId;
            
            try
            {
                await teacherStore.Delete(teacherId);
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
