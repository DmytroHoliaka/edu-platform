using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;

namespace EduPlatform.EntityFramework.Commands
{
    public class DeleteTeacherCommand(EduPlatformDbContextFactory contextFactory) : IDeleteTeacherCommand
    {
        public async Task ExecuteAsync(Guid teacherId)
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                Teacher teacher = new()
                {
                    TeacherId= teacherId
                };

                context.Teachers.Remove(teacher);
                await context.SaveChangesAsync();
            }
        }
    }
}
