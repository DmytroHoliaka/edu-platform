using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.StudentCommands
{
    public class SubmitCreateStudentCommand(
        StudentStore studentStore,
        ModalNavigationStore modalNavigationStore)
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
            GroupViewModel? relatedGroup = FormDetails.GroupVMs.FirstOrDefault(gvm => gvm.IsChecked == true);

            Student student = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = FormDetails.FirstName,
                LastName = FormDetails.LastName,
                GroupId = relatedGroup?.GroupId,
                Group = relatedGroup?.Group,
            };

            try
            {
                await studentStore.Add(student);
                modalNavigationStore.Close();
            }
            catch (Exception)
            {
                FormDetails.ErrorMessage = "Cannot create student. Try again later.";
            }
        }
    }
}