using EduPlatform.Domain.Models;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityIdEqualityComparers;

public class StudentIdEqualityComparer : IEqualityComparer<Student>
{
    public bool Equals(Student? x, Student? y)
    {
        if (x == y)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.StudentId == y.StudentId;
    }

    public int GetHashCode(Student obj)
    {
        return HashCode.Combine(obj.StudentId);
    }
}