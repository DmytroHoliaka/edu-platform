using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class SubmitUpdateGroupCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GroupStore _groupStore;
        private readonly GroupViewModel _selectedGroup;
        private readonly UpdateGroupViewModel _updateGroupViewModel;

        public SubmitUpdateGroupCommand(ModalNavigationStore modalNavigationStore, 
                                        GroupStore groupStore, 
                                        GroupViewModel selectedGroup,
                                        UpdateGroupViewModel updateGroupViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            _groupStore = groupStore;
            _selectedGroup = selectedGroup;
            _updateGroupViewModel = updateGroupViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // ToDo: Add store in database
            GroupDetailsFormViewModel formDetails = 
                _updateGroupViewModel.GroupDetailsFormVM;

            Group targetGroup = new()
            {
                GroupId = _selectedGroup.GroupId,
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
                await _groupStore.Update(targetGroup);
                _modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
