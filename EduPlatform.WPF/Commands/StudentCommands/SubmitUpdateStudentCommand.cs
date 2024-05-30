using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class SubmitUpdateStudentCommand(
        StudentViewModel selectedStudent,
        ModalNavigationStore modalNavigationStore,
        StudentStore studentStore)
        : AsyncCommandBase
    {
        public StudentDetailsFormViewModel? FormDetails { get; set; }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (FormDetails is null)
            {
                return;
            }

            FormDetails.ErrorMessage = null;
            GroupViewModel? relatedGroup = FormDetails.GroupVMs.FirstOrDefault(gvm => gvm.IsChecked);

            Student targetStudent = new()
            {
                StudentId = selectedStudent.StudentId,
                FirstName = FormDetails.FirstName,
                LastName = FormDetails.LastName,
                GroupId = relatedGroup?.GroupId,
                Group = relatedGroup?.Group,
            };
            try
            {
                await studentStore.Update(targetStudent);
                modalNavigationStore.Close();
            }
            catch (Exception)
            {
                FormDetails.ErrorMessage = "Cannot update student. Try again later.";
            }
        }
    }
}
