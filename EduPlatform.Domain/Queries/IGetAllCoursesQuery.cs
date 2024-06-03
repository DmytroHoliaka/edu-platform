using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Queries
{
    public interface IGetAllCoursesQuery
    {
        Task<IEnumerable<Course>> ExecuteAsync();
    }
}
