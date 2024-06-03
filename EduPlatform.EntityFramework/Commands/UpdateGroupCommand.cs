using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Commands
{
    public class UpdateGroupCommand(EduPlatformDbContextFactory contextFactory) : IUpdateGroupCommand
    {
        public async Task ExecuteAsync(Group? targetGroup)
        {
            ArgumentNullException.ThrowIfNull(targetGroup, nameof(targetGroup));

            using (EduPlatformDbContext context = contextFactory.Create())
            {
                if (context.Groups.Any(g => g.GroupId != targetGroup.GroupId && g.Name == targetGroup.Name))
                {
                    throw new InvalidDataException(
                        "The group with this name already exists in the database");
                }

                EntityMapper.SetGroupDbRelationships(context, targetGroup);

                Group? sourceGroupFromDb = context.Groups
                    .Include(g => g.Course)
                    .Include(g => g.Teachers)
                    .Include(g => g.Students)
                    .FirstOrDefault(g => g.GroupId == targetGroup.GroupId);

                if (sourceGroupFromDb is null)
                {
                    throw new InvalidDataException("The database does not contain a group with this identifier");
                }


                sourceGroupFromDb.Name = targetGroup.Name;
                sourceGroupFromDb.Course = targetGroup.Course;
                sourceGroupFromDb.Teachers = targetGroup.Teachers;
                sourceGroupFromDb.Students = targetGroup.Students;

                await context.SaveChangesAsync();
            }
        }
    }
}