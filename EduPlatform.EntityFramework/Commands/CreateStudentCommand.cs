using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DTOs;

namespace EduPlatform.EntityFramework.Commands
{
    public class CreateStudentCommand : DataOperationBase, ICreateStudentCommand
    {
        public CreateStudentCommand(EduPlatformDbContextFactory contextFactory) 
            : base(contextFactory)
        { }

        public async Task ExecuteAsync(Student newStudent)
        {
            using (EduPlatformDbContext context = _contextFactory.Create())
            {
                StudentDto studentDto = new()
                {
                    StudentId = newStudent.StudentId,
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    GroupId = newStudent.GroupId,
                    Group = newStudent.Group,
                };

                context.Students.Add(studentDto);

                await context.SaveChangesAsync();
            }
        }
    }
}
