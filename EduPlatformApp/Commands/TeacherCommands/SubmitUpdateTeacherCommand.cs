using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class SubmitUpdateTeacherCommand(
        TeacherViewModel selectedTeacher,
        ModalNavigationStore modalNavigationStore,
        TeacherStore teacherStore)
        : AsyncCommandBase
    {
        public TeacherDetailsFormViewModel? FormDetails { get; set; }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (FormDetails is null)
            {
                return;
            }

            IEnumerable<GroupViewModel> relatedGroups = FormDetails.GroupVMs.Where(gmv => gmv.IsChecked == true);

            Teacher targetStudent = new()
            {
                TeacherId = selectedTeacher.TeacherId,
                FirstName = FormDetails.FirstName,
                LastName = FormDetails.LastName,
                Groups = relatedGroups.Select(rg => rg.Group).ToList()
            };

            try
            {
                await teacherStore.Update(targetStudent);
                modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
