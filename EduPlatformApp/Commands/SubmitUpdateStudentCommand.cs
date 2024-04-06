using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands
{
    public class SubmitUpdateStudentCommand : AsyncCommandBase
    {
        public StudentDetailsFormViewModel? FormDetails { get; set; }

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly StudentStore _studentStore;
        private readonly StudentViewModel _selectedStudent;

        public SubmitUpdateStudentCommand(StudentViewModel selectedStudent,
                                          ModalNavigationStore modalNavigationStore,
                                          StudentStore studentStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _studentStore = studentStore;
            _selectedStudent = selectedStudent;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // ToDo: Add store in database

            if (FormDetails is null)
            {
                return;
            }

            GroupViewModel relatedGroup = FormDetails.GroupVMs.First(gvm => gvm.IsChecked == true);

            Student targetStudent = new()
            {
                StudentId = _selectedStudent.StudentId,
                FirstName = FormDetails.FirstName,
                LastName = FormDetails.LastName,
                GroupId = relatedGroup.GroupId,
                Group = relatedGroup.Group,
            };
            try
            {
                await _studentStore.Update(_selectedStudent.StudentId, targetStudent);
                _modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
