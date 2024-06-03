using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Commands
{
    public class DeleteCourseCommand(EduPlatformDbContextFactory contextFactory) : IDeleteCourseCommand
    {
        public async Task ExecuteAsync(Guid courseId)
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                Course? deletingCourse = context.Courses
                    .Include(c => c.Groups)
                    .FirstOrDefault(c => c.CourseId == courseId);

                if (deletingCourse is null)
                {
                    throw new InvalidDataException("The specified course is not in the database.");
                }

                if (deletingCourse.Groups.Count > 0)
                {
                    throw new InvalidDataException("It is impossible to delete a course that has groups");
                }

                context.Courses.Remove(deletingCourse);
                await context.SaveChangesAsync();
            }
        }
    }
}