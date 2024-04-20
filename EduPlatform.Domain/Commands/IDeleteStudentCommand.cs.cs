namespace EduPlatform.Domain.Commands
{
    public interface IDeleteStudentCommand
    {
        Task ExecuteAsync(Guid studentId);
    }
}
