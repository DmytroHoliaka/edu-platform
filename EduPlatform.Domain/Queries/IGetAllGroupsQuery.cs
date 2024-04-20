using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Queries
{
    public interface IGetAllGroupsQuery
    {
        Task<IEnumerable<Group>> ExecuteAsync();
    }
}
