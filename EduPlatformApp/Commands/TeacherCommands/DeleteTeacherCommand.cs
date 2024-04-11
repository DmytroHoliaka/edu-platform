using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class DeleteTeacherCommand : AsyncCommandBase
    {
        public TeacherViewModel? DeletingTeacher { get; set; }

        private readonly TeacherStore _teacherStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public DeleteTeacherCommand(TeacherStore teacherStore,
                                    ModalNavigationStore modalNavigationStore)
        {
            _teacherStore = teacherStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override bool CanExecute(object? parameter)
        {
            return DeletingTeacher is not null;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // ToDo: Add store in database
            if (DeletingTeacher is null)
            {
                return;
            }

            Guid teacherId = DeletingTeacher.TeacherId;
            
            try
            {
                await _teacherStore.Delete(teacherId);
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
