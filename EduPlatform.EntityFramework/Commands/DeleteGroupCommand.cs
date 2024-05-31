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
                if (context.Groups.Any(g => g.GroupId == groupId) == false)
                {
                    throw new InvalidDataException("The specified group is not in the database.");
                }

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
