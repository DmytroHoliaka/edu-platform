using EduPlatform.Domain.Models;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityIdEqualityComparers;

public class TeacherIdEqualityComparer : IEqualityComparer<Teacher>
{
    public bool Equals(Teacher? x, Teacher? y)
    {
        if (x == y)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.TeacherId == y.TeacherId;
    }

    public int GetHashCode(Teacher obj)
    {
        return HashCode.Combine(obj.TeacherId);
    }
}