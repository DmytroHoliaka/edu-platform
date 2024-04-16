using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Queries
{
    public interface IGetAllStudentsQuery
    {
        Task<IEnumerable<Student>> Execute();
    }
}
