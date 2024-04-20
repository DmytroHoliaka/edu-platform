using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;

namespace EduPlatform.EntityFramework.Commands
{
    public class DeleteCourseCommand(EduPlatformDbContextFactory contextFactory) : IDeleteCourseCommand
    {
        public async Task ExecuteAsync(Guid courseId)
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                Course course = new()
                {
                    CourseId = courseId
                };

                context.Courses.Remove(course);
                await context.SaveChangesAsync();
            }
        }
    }
}
