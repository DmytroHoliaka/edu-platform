using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface IUpdateStudentCommand
    {
        Task Execute(Student targetStudent);
    }
}
