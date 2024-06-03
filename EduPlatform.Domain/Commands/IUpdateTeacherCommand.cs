using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface IUpdateTeacherCommand
    {
        Task ExecuteAsync(Teacher targetTeacher);
    }
}
