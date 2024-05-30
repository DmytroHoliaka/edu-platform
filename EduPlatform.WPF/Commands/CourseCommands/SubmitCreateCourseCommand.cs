using EduPlatform.Domain.Models;
using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.CourseCommands
{
    public class SubmitCreateCourseCommand(
        CourseStore courseStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public CourseDetailsFormViewModel? FormDetails { get; set; }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (FormDetails is null)
            {
                return;
            }

            FormDetails.ErrorMessage = null;
            IEnumerable<GroupViewModel> relatedGroups = FormDetails.GroupVMs.Where(gmv => gmv.IsChecked == true);

            Course course = new()
            {
                CourseId = Guid.NewGuid(),
                Name = FormDetails.CourseName,
                Description = FormDetails.Description,
                Groups = relatedGroups.Select(rg => rg.Group).ToList()
            };

            try
            {
                await courseStore.Add(course);
                modalNavigationStore.Close();
            }
            catch (Exception)
            {
                FormDetails.ErrorMessage = "Cannot create course. Try again later.";
            }
        }
    }
}
