using EduPlatform.Domain.Models;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityIdEqualityComparers;

public class GroupIdEqualityComparer : IEqualityComparer<Group>
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

        return x.GroupId == y.GroupId;
    }

    public int GetHashCode(Group obj)
    {
        return HashCode.Combine(obj.GroupId);
    }
}