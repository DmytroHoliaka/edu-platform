using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class SubmitCreateGroupCommand : AsyncCommandBase
    {
        private readonly GroupStore _groupStore;
        private readonly CreateGroupViewModel _createGroupViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;

        public SubmitCreateGroupCommand(GroupStore groupStore,
                                        CreateGroupViewModel createGroupViewModel,
                                        ModalNavigationStore modalNavigationStore)
        {
            _groupStore = groupStore;
            _createGroupViewModel = createGroupViewModel;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            GroupDetailsFormViewModel formDetails =
                _createGroupViewModel.GroupDetailsFormVM;

            Group group = new()
            {
                GroupId = Guid.NewGuid(),
                Name = formDetails.GroupName,
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
                await _groupStore.Add(group);
                _modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Make label with error message*/ }
        }
    }
}
