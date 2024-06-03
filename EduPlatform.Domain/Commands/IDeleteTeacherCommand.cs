namespace EduPlatform.Domain.Commands
{
    public interface IDeleteTeacherCommand
    {
        Task ExecuteAsync(Guid teacherId);
    }
}
