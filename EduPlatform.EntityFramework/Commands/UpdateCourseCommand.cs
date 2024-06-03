using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Commands
{
    public class UpdateCourseCommand(EduPlatformDbContextFactory contextFactory) : IUpdateCourseCommand
    {
        public async Task ExecuteAsync(Course? targetCourse)
        {
            ArgumentNullException.ThrowIfNull(targetCourse, nameof(targetCourse));

            using (EduPlatformDbContext context = contextFactory.Create())
            {
                if (context.Courses.Any(c => c.CourseId != targetCourse.CourseId && 
                                             c.Name == targetCourse.Name))
                {
                    throw new InvalidDataException("The course with this name already exists in the database");
                }

                EntityMapper.SetCourseDbRelationships(context, targetCourse);

                Course? sourceCourseFromDb = context.Courses
                    .Include(c => c.Groups)
                    .FirstOrDefault(c => c.CourseId == targetCourse.CourseId);

                if (sourceCourseFromDb is null)
                {
                    throw new InvalidDataException(
                        "The database does not contain a course with this identifier");
                }

                sourceCourseFromDb.Name = targetCourse.Name;
                sourceCourseFromDb.Description = targetCourse.Description;
                sourceCourseFromDb.Groups = targetCourse.Groups;

                await context.SaveChangesAsync();
            }
        }
    }
}