using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class SubmitCreateGroupCommand(
        GroupStore groupStore,
        CreateGroupViewModel createGroupViewModel,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            GroupDetailsFormViewModel formDetails =
                createGroupViewModel.GroupDetailsFormVM;

            Group group = new()
            {
                GroupId = Guid.NewGuid(),
                Name = formDetails.GroupName,
                CourseId = formDetails.CourseVMs
                    .FirstOrDefault(cvm => cvm.IsChecked == true)?.CourseId,
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
                await groupStore.Add(group);
                modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Make label with error message*/ }
        }
    }
}
