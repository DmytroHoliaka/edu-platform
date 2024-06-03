using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;

namespace EduPlatform.EntityFramework.Commands
{
    public class CreateGroupCommand(EduPlatformDbContextFactory contextFactory) : ICreateGroupCommand
    {
        public async Task ExecuteAsync(Group? newGroup)
        {
            ArgumentNullException.ThrowIfNull(newGroup, nameof(newGroup));

            using (EduPlatformDbContext context = contextFactory.Create())
            {
                Group? duplicateGroup = context.Groups.FirstOrDefault(g => g.Name == newGroup.Name);

                if (duplicateGroup is not null)
                {
                    throw new InvalidDataException("The group with this name already exists");
                }

                EntityMapper.SetGroupDbRelationships(context, newGroup);

                await context.Groups.AddAsync(newGroup);
                await context.SaveChangesAsync();
            }
        }
    }
}