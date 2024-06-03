using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.EntityFramework.DbContextConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Queries
{
    public class GetAllStudentsQuery(EduPlatformDbContextFactory contextFactory) : IGetAllStudentsQuery
    {
        public async Task<IEnumerable<Student>> ExecuteAsync()
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                IEnumerable<Student> students = await context.Students
                    .Include(s => s.Group)
                    .ToListAsync();

                return students;
            }
        }
    }
}