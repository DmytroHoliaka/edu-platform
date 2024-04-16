using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    internal interface ICreateStudentCommand
    {
        Task Execute(Student newStudent);
    }
}
