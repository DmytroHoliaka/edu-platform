using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.CoursesViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
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
            // ToDo: Add store in database

            if (FormDetails is null)
            {
                return;
            }

            IEnumerable<GroupViewModel> relatedGroups = FormDetails.GroupVMs.Where(gmv => gmv.IsChecked == true);

            Course targetCourse = new()
            {
                CourseId = selectedCourse.CourseId,
                Name = FormDetails.CourseName,
                Description = string.IsNullOrWhiteSpace(FormDetails.Description) 
                    ? string.Empty : FormDetails.Description,
                Groups = relatedGroups.Select(rg => rg.Group).ToList()
            };

            try
            {
                await courseStore.Update(targetCourse);
                modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
