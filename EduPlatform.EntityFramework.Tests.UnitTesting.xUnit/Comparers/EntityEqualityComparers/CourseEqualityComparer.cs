using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityIdEqualityComparers;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;

public class CourseEqualityComparer : IEqualityComparer<Course>
{
    public bool Equals(Course? x, Course? y)
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
            x.CourseId == y.CourseId &&
            x.Name == y.Name &&
            x.Description == y.Description &&
            x.Groups.SequenceEqual(y.Groups, new GroupIdEqualityComparer());
    }

    public int GetHashCode(Course obj)
    {
        return HashCode.Combine(
            obj.CourseId,
            obj.Name,
            obj.Description,
            obj.Groups);
    }
}