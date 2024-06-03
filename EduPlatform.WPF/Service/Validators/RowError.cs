namespace EduPlatform.WPF.Service.Validators;

public class RowError
{
    public int LineNumber {get;}
    public string? ColumnName {get;}
    public string? Explanation { get; }

    public RowError(int lineNumber, string? columnName, string? explanation)
    {
        LineNumber = lineNumber;
        ColumnName = columnName;
        Explanation = explanation;
    }

    public override string ToString()
    {
        return $"Row: {LineNumber}, column: «{ColumnName}». ({Explanation})";
    }
}