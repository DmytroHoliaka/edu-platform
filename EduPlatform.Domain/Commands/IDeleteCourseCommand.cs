namespace EduPlatform.Domain.Commands
{
    public interface IDeleteCourseCommand
    {
        Task ExecuteAsync(Guid courseId);
    }
}
