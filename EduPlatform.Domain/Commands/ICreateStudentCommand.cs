using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface ICreateStudentCommand
    {
        Task ExecuteAsync(Student newStudent);
    }
}
