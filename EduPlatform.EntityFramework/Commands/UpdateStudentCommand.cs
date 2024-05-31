using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Commands
{
    public class UpdateStudentCommand(EduPlatformDbContextFactory contextFactory) : IUpdateStudentCommand
    {
        public async Task ExecuteAsync(Student? targetStudent)
        {
            ArgumentNullException.ThrowIfNull(targetStudent, nameof(targetStudent));

            using (EduPlatformDbContext context = contextFactory.Create())
            {
                EntityMapper.SetStudentDbRelationships(context, targetStudent);

                Student? sourceStudent = context.Students
                    .Include(s => s.Group)
                    .FirstOrDefault(s => s.StudentId == targetStudent.StudentId);

                if (sourceStudent is null)
                {
                    CreateStudentCommand createStudentCommand = new(contextFactory);
                    await createStudentCommand.ExecuteAsync(targetStudent);
                }
                else
                {
                    sourceStudent.FirstName = targetStudent.FirstName;
                    sourceStudent.LastName = targetStudent.LastName;
                    sourceStudent.Group = targetStudent.Group;
                }

                await context.SaveChangesAsync();
            }
        }
    }
}