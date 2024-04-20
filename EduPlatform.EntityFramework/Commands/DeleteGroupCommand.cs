using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;

namespace EduPlatform.EntityFramework.Commands
{
    public class DeleteGroupCommand(EduPlatformDbContextFactory contextFactory) : IDeleteGroupCommand
    {
        public async Task ExecuteAsync(Guid groupId)
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                Group group = new()
                {
                    GroupId = groupId
                };

                context.Groups.Remove(group);
                await context.SaveChangesAsync();
            }
        }
    }
}
