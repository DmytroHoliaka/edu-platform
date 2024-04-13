using EduPlatform.WPF.Models;

namespace EduPlatform.WPF.Stores
{
    public class CourseStore
    {
        public event Action<Course>? CourseAdded;
        public event Action<Course>? CourseUpdated;
        public event Action<Guid>? CourseDeleted;

        public async Task Add(Course newCourse)
        {
            CourseAdded?.Invoke(newCourse);
        }

        public async Task Update(Course targetCourse)
        {
            CourseUpdated?.Invoke(targetCourse);
        }

        public async Task Delete(Guid courseId)
        {
            CourseDeleted?.Invoke(courseId);
        }
    }
}
