using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Queries
{
    public interface IGetAllTeachersQuery
    {
        Task<IEnumerable<Teacher>> ExecuteAsync();
    }
}
