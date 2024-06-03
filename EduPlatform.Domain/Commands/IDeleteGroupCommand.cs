namespace EduPlatform.Domain.Commands
{
    public interface IDeleteGroupCommand
    {
        Task ExecuteAsync(Guid groupId);
    }
}
