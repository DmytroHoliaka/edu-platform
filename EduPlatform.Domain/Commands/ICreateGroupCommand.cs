using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface ICreateGroupCommand
    {
        Task ExecuteAsync(Group newGroup);
    }
}
