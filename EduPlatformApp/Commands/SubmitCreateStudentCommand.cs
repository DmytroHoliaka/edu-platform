using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands
{
    public class SubmitCreateStudentCommand : AsyncCommandBase
    {
        public StudentDetailsFormViewModel? FormDetails { get; set; }
        private readonly StudentStore _studentStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public SubmitCreateStudentCommand
        (
            StudentStore studentStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _studentStore = studentStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override bool CanExecute(object? parameter)
        {
            return FormDetails is not null 
                && FormDetails.GroupVMs.Any(gvm => gvm.IsChecked == true)
                && string.IsNullOrWhiteSpace(FormDetails.FirstName) == false 
                && string.IsNullOrWhiteSpace(FormDetails.LastName) == false;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            GroupViewModel relatedGroup = FormDetails!.GroupVMs.First(gvm => gvm.IsChecked == true);

            Student student = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = FormDetails.FirstName,
                LastName = FormDetails.LastName,
                GroupId = relatedGroup.GroupId,
                Group = relatedGroup.Group,
            };

            try
            {
                await _studentStore.Add(student);
                _modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Make label with error message*/ }
        }
    }
}
