using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;

namespace EduPlatform.EntityFramework.Commands
{
    public class CreateTeacherCommand(EduPlatformDbContextFactory contextFactory) : ICreateTeacherCommand
    {
        public async Task ExecuteAsync(Teacher? newTeacher)
        {
            ArgumentNullException.ThrowIfNull(newTeacher, nameof(newTeacher));

            using (EduPlatformDbContext context = contextFactory.Create())
            {
                EntityMapper.SetTeacherDbRelationships(context, newTeacher);
                
                await context.Teachers.AddAsync(newTeacher);
                await context.SaveChangesAsync();
            }
        }
    }
}
