using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    internal interface IDeleteStudentCommand
    {
        Task Execute(Guid studentId);
    }
}
