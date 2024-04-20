using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;

namespace EduPlatform.EntityFramework.Service
{
    public static class EntityMapper
    {
        public static void SetCourseDbRelationships(EduPlatformDbContext context, Course course)
        {
            IEnumerable<Guid> localGroupIds = EntityExtractor.GetGroupIds(course);

            course.Groups = context.Groups
                .Where(g => localGroupIds.Contains(g.GroupId)).ToList();
        }

        public static void SetGroupDbRelationships(EduPlatformDbContext context, Group group)
        {
            IEnumerable<Guid> localTeacherIds = EntityExtractor.GetTeacherIds(group);
            IEnumerable<Guid> localStudentIds = EntityExtractor.GetStudentIds(group);

            group.Course = context.Courses
                .FirstOrDefault(c => c.CourseId == group.CourseId);

            group.Teachers = context.Teachers
                .Where(t => localTeacherIds.Contains(t.TeacherId))
                .ToList();

            group.Students = context.Students
                .Where(s => localStudentIds.Contains(s.StudentId))
                .ToList();
        }

        public static void SetTeacherDbRelationships(EduPlatformDbContext context, Teacher teacher)
        {
            IEnumerable<Guid> localGroupIds = EntityExtractor.GetGroupIds(teacher);

            teacher.Groups = context.Groups
                .Where(g => localGroupIds.Contains(g.GroupId))
                .ToList();
        }

        public static void SetStudentDbRelationships(EduPlatformDbContext context, Student student)
        {
            student.Group = context.Groups
                .FirstOrDefault(g => g.GroupId == student.GroupId);
        }
    }
}
