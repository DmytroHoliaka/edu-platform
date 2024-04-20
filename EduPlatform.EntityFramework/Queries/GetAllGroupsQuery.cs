using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.EntityFramework.DbContextConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Queries
{
    public class GetAllGroupsQuery(EduPlatformDbContextFactory contextFactory) : IGetAllGroupsQuery
    {
        public async Task<IEnumerable<Group>> ExecuteAsync()
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                IEnumerable<Group> groups = await context.Groups.ToListAsync();
                return groups;
            }
        }
    }
}
