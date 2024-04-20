using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;

namespace EduPlatform.EntityFramework.Commands
{
    public class CreateStudentCommand(EduPlatformDbContextFactory contextFactory) : ICreateStudentCommand
    {
        public async Task ExecuteAsync(Student newStudent)
        {
            using (EduPlatformDbContext context = contextFactory.Create())
            {
                EntityMapper.SetStudentDbRelationships(context, newStudent);

                await context.Students.AddAsync(newStudent);
                await context.SaveChangesAsync();
            }
        }
    }
}