using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Commands
{
    public class UpdateTeacherCommand(EduPlatformDbContextFactory contextFactory) : IUpdateTeacherCommand
    {
        public async Task ExecuteAsync(Teacher? targetTeacher)
        {
            ArgumentNullException.ThrowIfNull(targetTeacher, nameof(targetTeacher));

            using (EduPlatformDbContext context = contextFactory.Create())
            {
                EntityMapper.SetTeacherDbRelationships(context, targetTeacher);

                Teacher? sourceTeacher = context.Teachers
                    .Include(t => t.Groups)
                    .FirstOrDefault(t => t.TeacherId == targetTeacher.TeacherId);

                if (sourceTeacher is null)
                {
                    throw new InvalidDataException(
                        "The database does not contain a teacher with this identifier");
                }

                sourceTeacher.FirstName = targetTeacher.FirstName;
                sourceTeacher.LastName = targetTeacher.LastName;
                sourceTeacher.Groups = targetTeacher.Groups;

                await context.SaveChangesAsync();
            }
        }
    }
}