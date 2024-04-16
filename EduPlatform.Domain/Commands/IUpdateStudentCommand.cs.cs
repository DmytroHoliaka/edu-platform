using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    internal interface IUpdateStudentCommand
    {
        Task Execute(Student targetStudent);
    }
}
