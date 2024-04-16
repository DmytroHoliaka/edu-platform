using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface IUpdateStudentCommand
    {
        Task ExecuteAsync(Student targetStudent);
    }
}
