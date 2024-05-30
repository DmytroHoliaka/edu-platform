using EduPlatform.Domain.Models;
using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.CourseCommands
{
    public class SubmitUpdateCourseCommand(
        CourseViewModel selectedCourse,
        ModalNavigationStore modalNavigationStore,
        CourseStore courseStore)
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
            IEnumerable<GroupViewModel> relatedGroups = FormDetails.GroupVMs.Where(gmv => gmv.IsChecked);

            Course targetCourse = new()
            {
                CourseId = selectedCourse.CourseId,
                Name = FormDetails.CourseName,
                Description = string.IsNullOrWhiteSpace(FormDetails.Description)
                    ? string.Empty
                    : FormDetails.Description,
                Groups = relatedGroups.Select(rg => rg.Group).ToList()
            };

            try
            {
                await courseStore.Update(targetCourse);
                modalNavigationStore.Close();
            }
            catch (Exception)
            {
                FormDetails.ErrorMessage = "Cannot update course. Try again later.";
            }
        }
    }
}