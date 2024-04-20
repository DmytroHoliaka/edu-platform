using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.WPF.Service;

namespace EduPlatform.WPF.Stores
{
    public class CourseStore(
        IGetAllCoursesQuery getAllCoursesQuery,
        ICreateCourseCommand createCourseCommand,
        IUpdateCourseCommand updateCourseCommand,
        IDeleteCourseCommand deleteCourseCommand)
    {
        public event Action<Course>? CourseAdded;
        public event Action<Course>? CourseUpdated;
        public event Action<Guid>? CourseDeleted;

        private readonly IGetAllCoursesQuery _getAllCoursesQuery = getAllCoursesQuery;

        public async Task Add(Course newCourse)
        {
            await createCourseCommand.ExecuteAsync(SerializationCopier.DeepCopy(newCourse)!);
            CourseAdded?.Invoke(newCourse);
        }

        public async Task Update(Course targetCourse)
        {
            await updateCourseCommand.ExecuteAsync(SerializationCopier.DeepCopy(targetCourse)!);
            CourseUpdated?.Invoke(targetCourse);
        }

        public async Task Delete(Guid courseId)
        {
            await deleteCourseCommand.ExecuteAsync(courseId);
            CourseDeleted?.Invoke(courseId);
        }
    }
}
