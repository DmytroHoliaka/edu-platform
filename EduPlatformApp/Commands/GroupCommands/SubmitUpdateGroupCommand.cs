using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class SubmitUpdateGroupCommand(
        ModalNavigationStore modalNavigationStore,
        GroupStore groupStore,
        GroupViewModel selectedGroup,
        UpdateGroupViewModel updateGroupViewModel)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            GroupDetailsFormViewModel formDetails = 
                updateGroupViewModel.GroupDetailsFormVM;

            Group targetGroup = new()
            {
                GroupId = selectedGroup.GroupId,
                Name = formDetails.GroupName,
                CourseId = formDetails.CourseVMs
                    .FirstOrDefault(c => c.IsChecked == true)?.CourseId,
                Course = formDetails.CourseVMs
                    .FirstOrDefault(cvm => cvm.IsChecked == true)?.Course,
                Teachers = formDetails.TeacherVMs
                    .Where(ft => ft.IsChecked == true)
                    .Select(ft => ft.Teacher).ToList(),
                Students = formDetails.StudentVMs
                    .Where(ft => ft.IsChecked == true)
                    .Select(fs => fs.Student).ToList()
            };

            try
            {
                await groupStore.Update(targetGroup);
                modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
