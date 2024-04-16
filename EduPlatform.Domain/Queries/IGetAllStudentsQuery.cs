using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Queries
{
    internal interface IGetAllStudentsQuery
    {
        Task<IEnumerable<Student>> Execute();
    }
}
