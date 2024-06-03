using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityIdEqualityComparers;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;

public class StudentEqualityComparer : IEqualityComparer<Student>
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

        return
            x.StudentId == y.StudentId &&
            x.FirstName == y.FirstName &&
            x.LastName == y.LastName &&
            x.GroupId == y.GroupId &&
            new GroupIdEqualityComparer().Equals(x.Group, y.Group);
    }

    public int GetHashCode(Student obj)
    {
        return HashCode.Combine(
            obj.StudentId,
            obj.FirstName,
            obj.LastName,
            obj.GroupId,
            obj.Group);
    }
}