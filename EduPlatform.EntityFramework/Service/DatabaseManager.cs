using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Service;

public class DatabaseManager(EduPlatformDbContextFactory factory)
{
    public async Task ClearDatabase()
    {
        using (EduPlatformDbContext context = factory.Create())
        {
            context.Courses.RemoveRange(context.Courses);
            context.Groups.RemoveRange(context.Groups);
            context.Students.RemoveRange(context.Students);
            context.Teachers.RemoveRange(context.Teachers);

            await context.SaveChangesAsync();
        }
    }

    public Course GetSingleCourseFromDatabase()
    {
        using (EduPlatformDbContext context = factory.Create())
        {
            return context.Courses.Include(c => c.Groups).Single();
        }
    }

    public Group GetSingleGroupFromDatabase()
    {
        using (EduPlatformDbContext context = factory.Create())
        {
            return context.Groups
                .Include(g => g.Course)
                .Include(g => g.Students)
                .Include(g => g.Students)
                .Single();
        }
    }

    public Student GetSingleStudentFromDatabase()
    {
        using (EduPlatformDbContext context = factory.Create())
        {
            return context.Students.Include(s => s.Group).Single();
        }
    }

    public Teacher GetSingleTeacherFromDatabase()
    {
        using (EduPlatformDbContext context = factory.Create())
        {
            return context.Teachers.Include(t => t.Groups).Single();
        }
    }

    // Warning: ToList() is necessary (otherwise, the problem is due to lazy context loading and disposal))
    public IEnumerable<Course> GetAllCoursesFromDatabase()
    {
        using (EduPlatformDbContext context = factory.Create())
        {
            return context.Courses.Include(c => c.Groups).ToList();
        }
    }

    public IEnumerable<Group> GetAllGroupFromDatabase()
    {
        using (EduPlatformDbContext context = factory.Create())
        {
            return context.Groups
                .Include(g => g.Course)
                .Include(g => g.Students)
                .Include(g => g.Teachers)
                .ToList();
        }
    }

    public IEnumerable<Student> GetAllStudentsFromDatabase()
    {
        using (EduPlatformDbContext context = factory.Create())
        {
            return context.Students.Include(s => s.Group).ToList();
        }
    }

    public IEnumerable<Teacher> GetAllTeachersFromDatabase()
    {
        using (EduPlatformDbContext context = factory.Create())
        {
            return context.Teachers.Include(t => t.Groups).ToList();
        }
    }
}