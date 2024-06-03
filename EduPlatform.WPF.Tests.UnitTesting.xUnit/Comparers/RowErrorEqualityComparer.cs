using EduPlatform.WPF.Service.Validators;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Comparers;

public class RowErrorEqualityComparer : IEqualityComparer<RowError>
{
    public bool Equals(RowError? x, RowError? y)
    {
        return
            x?.LineNumber == y?.LineNumber &&
            x?.ColumnName == y?.ColumnName &&
            x?.Explanation == y?.Explanation;
    }

    public int GetHashCode(RowError obj)
    {
        return HashCode.Combine(obj.LineNumber, obj.ColumnName, obj.Explanation);
    }
}