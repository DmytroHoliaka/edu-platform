using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Commands
{
    public class DeleteGroupCommand(EduPlatformDbContextFactory contextFactory) : IDeleteGroupCommand
    {
        public async Task ExecuteAsync(Guid groupId)
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                Group? deletingGroup = context.Groups
                    .Include(g => g.Students)
                    .FirstOrDefault(g => g.GroupId == groupId);

                if (deletingGroup is null)
                {
                    throw new InvalidDataException("There is no group with this identifier in the database");
                }

                if (deletingGroup.Students.Count > 0)
                {
                    throw new InvalidDataException(
                        "It is impossible to delete a group that has students");
                }

                context.Groups.Remove(deletingGroup);
                await context.SaveChangesAsync();
            }
        }
    }
}