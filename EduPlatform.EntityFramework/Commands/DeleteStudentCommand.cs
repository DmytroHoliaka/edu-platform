using EduPlatform.Domain.Commands;
using EduPlatform.EntityFramework.DTOs;

namespace EduPlatform.EntityFramework.Commands
{
    public class DeleteStudentCommand : DataOperationBase, IDeleteStudentCommand
    {
        public DeleteStudentCommand(EduPlatformDbContextFactory contextFactory)
            : base(contextFactory)
        { }

        public async Task ExecuteAsync(Guid studentId)
        {
            using (EduPlatformDbContext context = _contextFactory.Create())
            {
                StudentDto studentDto = new()
                {
                    StudentId = studentId
                };

                context.Students.Remove(studentDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
