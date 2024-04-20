using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;

namespace EduPlatform.EntityFramework.Commands
{
    public class DeleteStudentCommand(EduPlatformDbContextFactory contextFactory) : IDeleteStudentCommand
    {
        public async Task ExecuteAsync(Guid studentId)
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                Student student = new()
                {
                    StudentId = studentId
                };

                context.Students.Remove(student);
                await context.SaveChangesAsync();
            }
        }
    }
}
