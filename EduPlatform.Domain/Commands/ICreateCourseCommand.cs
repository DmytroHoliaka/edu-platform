using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface ICreateCourseCommand
    {
        Task ExecuteAsync(Course newCourse);
    }
}
