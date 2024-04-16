using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.CoursesViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class SubmitUpdateCourseCommand : AsyncCommandBase
    {
        public CourseDetailsFormViewModel? FormDetails { get; set; }

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CourseStore _courseStore;
        private readonly CourseViewModel _selectedCourse;

        public SubmitUpdateCourseCommand(CourseViewModel selectedCourse,
                                         ModalNavigationStore modalNavigationStore,
                                         CourseStore courseStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _courseStore = courseStore;
            _selectedCourse = selectedCourse;
        }

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
                CourseId = _selectedCourse.CourseId,
                Name = FormDetails.CourseName,
                Description = string.IsNullOrWhiteSpace(FormDetails.Description) 
                    ? string.Empty : FormDetails.Description,
                Groups = relatedGroups.Select(rg => rg.Group).ToList()
            };

            try
            {
                await _courseStore.Update(targetCourse);
                _modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Write validation message*/ }
        }
    }
}
