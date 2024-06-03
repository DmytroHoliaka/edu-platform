using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Queries;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Queries;

[Collection("DatabaseTests")]
public class GetAllGroupsQueryTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_ValidContext_ReturnsSequenceOfGroups()
    {
        // Arrange
        List<Group> expected = ModelGenerator.GetPopulatedGroups();

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await fixture.DbManager.ClearDatabase();
            await context.Groups.AddRangeAsync(expected);
            await context.SaveChangesAsync();
        }

        GetAllGroupsQuery query = new(fixture.DbContextFactory);

        // Act
        List<Group> actual = (await query.ExecuteAsync()).ToList();

        // Assert
        Assert.Equal(
            expected: SetCorrectOrder(expected),
            actual: SetCorrectOrder(actual),
            comparer: new GroupEqualityComparer());
    }

    private static List<Group> SetCorrectOrder(List<Group> groups)
    {
        groups = groups.OrderBy(g => g.GroupId).ToList();
        groups.ForEach(g => g.Teachers = g.Teachers.OrderBy(t => t.TeacherId).ToList());
        groups.ForEach(g => g.Students = g.Students.OrderBy(s => s.StudentId).ToList());

        return groups;
    }
}