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
                if (context.Students.Any(s => s.StudentId == studentId) == false)
                {
                    throw new InvalidDataException("The specified student is not in the database.");
                }

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
