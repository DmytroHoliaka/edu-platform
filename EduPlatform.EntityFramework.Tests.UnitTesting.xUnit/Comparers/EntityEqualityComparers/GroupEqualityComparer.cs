using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityIdEqualityComparers;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;

public class GroupEqualityComparer : IEqualityComparer<Group>
{
    public bool Equals(Group? x, Group? y)
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
            x.GroupId == y.GroupId &&
            x.Name == y.Name &&
            x.CourseId == y.CourseId &&
            new CourseIdEqualityComparer().Equals(x.Course, y.Course) &&
            x.Teachers.SequenceEqual(y.Teachers, new TeacherIdEqualityComparer()) &&
            x.Students.SequenceEqual(y.Students, new StudentIdEqualityComparer());
    }

    public int GetHashCode(Group obj)
    {
        return HashCode.Combine(
            obj.GroupId,
            obj.Name,
            obj.CourseId,
            obj.Course,
            obj.Teachers,
            obj.Students);
    }
}