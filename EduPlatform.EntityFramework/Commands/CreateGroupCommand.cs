using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;

namespace EduPlatform.EntityFramework.Commands
{
    public class CreateGroupCommand(EduPlatformDbContextFactory contextFactory) : ICreateGroupCommand
    {
        public async Task ExecuteAsync(Group newGroup)
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                EntityMapper.SetGroupDbRelationships(context, newGroup);

                await context.Groups.AddAsync(newGroup);
                await context.SaveChangesAsync();
            }
        }
    }
}