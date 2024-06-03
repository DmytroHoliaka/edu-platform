using EduPlatform.Domain.Models;

namespace EduPlatform.EntityFramework.Service
{
    public static class EntityExtractor
    {
        public static IEnumerable<Guid> GetTeacherIds(Group group)
        {
            return group.Teachers.Select(t => t.TeacherId);
        }

        public static IEnumerable<Guid> GetStudentIds(Group group)
        {
            return group.Students.Select(s => s.StudentId);
        }

        public static IEnumerable<Guid> GetGroupIds(Teacher teacher)
        {
            return teacher.Groups.Select(g => g.GroupId);
        }

        public static IEnumerable<Guid> GetGroupIds(Course course)
        {
            return course.Groups.Select(g => g.GroupId);
        }
    }
}
