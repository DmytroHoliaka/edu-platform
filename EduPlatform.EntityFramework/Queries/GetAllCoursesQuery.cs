using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.EntityFramework.DbContextConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Queries
{
    public class GetAllCoursesQuery(EduPlatformDbContextFactory contextFactory) : IGetAllCoursesQuery
    {
        public async Task<IEnumerable<Course>> ExecuteAsync()
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                IEnumerable<Course> courses = await context.Courses
                    .Include(c => c.Groups)
                    .ToListAsync();

                return courses;
            }
        }
    }
}
