using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface ICreateTeacherCommand
    {
        Task ExecuteAsync(Teacher newTeacher);
    }
}
