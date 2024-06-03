using EduPlatform.Domain.Models;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityIdEqualityComparers;

public class CourseIdEqualityComparer : IEqualityComparer<Course>
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

        return x.CourseId == y.CourseId;
    }

    public int GetHashCode(Course obj)
    {
        return HashCode.Combine(obj.CourseId);
    }
}