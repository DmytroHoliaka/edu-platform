using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.WPF.Service.Utilities;

namespace EduPlatform.WPF.Stores
{
    public class CourseStore(
        IGetAllCoursesQuery getAllCoursesQuery,
        ICreateCourseCommand createCourseCommand,
        IUpdateCourseCommand updateCourseCommand,
        IDeleteCourseCommand deleteCourseCommand)
    {
        public event Action? CoursesLoaded;
        public event Action<Course>? CourseAdded;
        public event Action<Course>? CourseUpdated;
        public event Action<Guid>? CourseDeleted;

        public IEnumerable<Course> Courses => _courses;
        private readonly List<Course> _courses = [];

        public async Task Load()
        {
            IEnumerable<Course> coursesFromDb = await getAllCoursesQuery.ExecuteAsync();
            IEnumerable<Course> clonedCourses = coursesFromDb.Select(SerializationCopier.DeepCopy)!;

            _courses.Clear();
            _courses.AddRange(clonedCourses);

            CoursesLoaded?.Invoke();
        }

        public async Task Add(Course newCourse)
        {
            await createCourseCommand.ExecuteAsync(SerializationCopier.DeepCopy(newCourse)!);

            _courses.Add(newCourse);

            CourseAdded?.Invoke(newCourse);
        }

        public async Task Update(Course targetCourse)
        {
            await updateCourseCommand.ExecuteAsync(SerializationCopier.DeepCopy(targetCourse)!);

            int index = _courses.FindIndex(c => c.CourseId == targetCourse.CourseId);

            if (index == -1)
            {
                _courses.Add(targetCourse);
            }
            else
            {
                _courses[index] = targetCourse;
            }

            CourseUpdated?.Invoke(targetCourse);
        }

        public async Task Delete(Guid courseId)
        {
            await deleteCourseCommand.ExecuteAsync(courseId);

            _courses.RemoveAll(c => c.CourseId == courseId);

            CourseDeleted?.Invoke(courseId);
        }
    }
}
