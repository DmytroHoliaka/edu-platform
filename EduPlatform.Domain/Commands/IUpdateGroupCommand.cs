using EduPlatform.Domain.Models;

namespace EduPlatform.Domain.Commands
{
    public interface IUpdateGroupCommand
    {
        Task ExecuteAsync(Group targetGroup);
    }
}
