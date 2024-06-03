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

            formDetails.ErrorMessage = null;

            Group group = new()
            {
                GroupId = Guid.NewGuid(),
                Name = formDetails.GroupName,
                CourseId = formDetails.CourseVMs
                    .FirstOrDefault(cvm => cvm.IsChecked)?.CourseId,
                Course = formDetails.CourseVMs
                    .FirstOrDefault(cvm => cvm.IsChecked)?.Course,
                Teachers = formDetails.TeacherVMs
                    .Where(ft => ft.IsChecked)
                    .Select(ft => ft.Teacher).ToList(),
                Students = formDetails.StudentVMs
                    .Where(ft => ft.IsChecked)
                    .Select(fs => fs.Student).ToList()
            };

            try
            {
                await groupStore.Add(group);
                modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formDetails.ErrorMessage = "Cannot create group. Try again later.";
            }
        }
    }
}
