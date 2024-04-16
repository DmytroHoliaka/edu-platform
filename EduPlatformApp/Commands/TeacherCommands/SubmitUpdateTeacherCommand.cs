using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class SubmitUpdateTeacherCommand : AsyncCommandBase
    {
        public TeacherDetailsFormViewModel? FormDetails { get; set; }

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TeacherStore _teacherStore;
        private readonly TeacherViewModel _selectedTeacher;

        public SubmitUpdateTeacherCommand(TeacherViewModel selectedTeacher,
                                          ModalNavigationStore modalNavigationStore,
                                          TeacherStore teacherStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _teacherStore = teacherStore;
            _selectedTeacher = selectedTeacher;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // ToDo: Add store in database

            if (FormDetails is null)
            {
                return;
            }

            IEnumerable<GroupViewModel> relatedGroups = FormDetails.GroupVMs.Where(gmv => gmv.IsChecked == true);

            Teacher targetStudent = new()
            {
                TeacherId = _selectedTeacher.TeacherId,
                FirstName = FormDetails.FirstName,
                LastName = FormDetails.LastName,
                Groups = relatedGroups.Select(rg => rg.Group).ToList()
            };

            try
            {
                await _teacherStore.Update(targetStudent);
                _modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
