using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface IUpdateCourseCommand
    {
        Task ExecuteAsync(Course targetCourse);
    }
}
