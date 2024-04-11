using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class SubmitCreateTeacherCommand : AsyncCommandBase
    {
        public TeacherDetailsFormViewModel? FormDetails { get; set; }
        private readonly TeacherStore _teacherStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public SubmitCreateTeacherCommand
        (
            TeacherStore teacherStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _teacherStore = teacherStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (FormDetails is null)
            {
                return;
            }

            IEnumerable<GroupViewModel> relatedGroups = FormDetails.GroupVMs.Where(gmv => gmv.IsChecked == true);

            Teacher teacher = new()
            {
                TeacherId = Guid.NewGuid(),
                FirstName = FormDetails.FirstName,
                LastName = FormDetails.LastName,
                Groups = relatedGroups.Select(rg => rg.Group).ToList()
            };

            try
            {
                await _teacherStore.Add(teacher);
                _modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Make label with error message*/ }
        }
    }
}
