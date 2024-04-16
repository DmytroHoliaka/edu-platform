using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.CoursesViewModels;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class SubmitCreateCourseCommand : AsyncCommandBase
    {
        public CourseDetailsFormViewModel? FormDetails { get; set; }
        private readonly CourseStore _courseStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public SubmitCreateCourseCommand
        (
            CourseStore courseStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _courseStore = courseStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (FormDetails is null)
            {
                return;
            }

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
                await _courseStore.Add(course);
                _modalNavigationStore.Close();
            }
            catch (Exception) { /*ToDo: Make label with error message*/ }
        }
    }
}
