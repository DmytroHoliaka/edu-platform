using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityIdEqualityComparers;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;

public class TeacherEqualityComparer : IEqualityComparer<Teacher>
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

        return
            x.TeacherId == y.TeacherId &&
            x.FirstName == y.FirstName &&
            x.LastName == y.LastName &&
            x.Groups.SequenceEqual(y.Groups, new GroupIdEqualityComparer());
    }

    public int GetHashCode(Teacher obj)
    {
        return HashCode.Combine(
            obj.TeacherId,
            obj.FirstName,
            obj.LastName,
            obj.Groups);
    }
}