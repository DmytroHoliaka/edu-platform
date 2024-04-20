using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.EntityFramework.DbContextConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Queries
{
    public class GetAllTeachersQuery(EduPlatformDbContextFactory contextFactory) : IGetAllTeachersQuery
    {
        public async Task<IEnumerable<Teacher>> ExecuteAsync()
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                IEnumerable<Teacher> teachers = await context.Teachers.ToListAsync();
                return teachers;
            }
        }
    }
}
