namespace EduPlatform.WPF.Service.Validators;

public interface IFileValidator
{
    IEnumerable<RowError> GetFileErrors(string? path);
}