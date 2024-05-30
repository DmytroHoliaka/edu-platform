namespace EduPlatform.WPF.Service.Validators;

public class RowError(int lineNumber, string? columnName, string? explanation)
{
    public override string ToString()
    {
        return $"Row: {lineNumber}, column: «{columnName}». ({explanation})";
    }
}