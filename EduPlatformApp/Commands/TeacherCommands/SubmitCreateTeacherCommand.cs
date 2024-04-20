using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class SubmitCreateTeacherCommand(
        TeacherStore teacherStore,
        ModalNavigationStore modalNavigationStore)
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

            Teacher teacher = new()
            {
                TeacherId = Guid.NewGuid(),
                FirstName = FormDetails.FirstName,
                LastName = FormDetails.LastName,
                Groups = relatedGroups.Select(rg => rg.Group).ToList()
            };

            try
            {
                await teacherStore.Add(teacher);
                modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Make label with error message*/ }
        }
    }
}
