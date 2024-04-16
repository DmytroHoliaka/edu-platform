using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface ICreateStudentCommand
    {
        Task Execute(Student newStudent);
    }
}
