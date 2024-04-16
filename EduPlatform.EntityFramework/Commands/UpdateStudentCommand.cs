using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace EduPlatform.EntityFramework.Commands
{
    public class UpdateStudentCommand : DataOperationBase, IUpdateStudentCommand
    {
        public UpdateStudentCommand(EduPlatformDbContextFactory contextFactory) 
            : base(contextFactory)
        { }

        public async Task ExecuteAsync(Student targetStudent)
        {
            using (EduPlatformDbContext context = _contextFactory.Create())
            {
                StudentDto targetStudentDto = new()
                {
                    StudentId = targetStudent.StudentId,
                    FirstName = targetStudent.FirstName,
                    LastName = targetStudent.LastName,
                    GroupId = targetStudent.GroupId,
                    Group = targetStudent.Group,
                };

                context.Students.Update(targetStudentDto);

                await context.SaveChangesAsync();
            }
        }
    }
}
