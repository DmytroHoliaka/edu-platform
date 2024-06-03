using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Queries;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Queries;

[Collection("DatabaseTests")]
public class GetAllTeachersQueryTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_ValidContext_ReturnsSequenceOfTeachers()
    {
        // Arrange
        List<Teacher> expected = ModelGenerator.GetPopulatedTeachers();

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await fixture.DbManager.ClearDatabase();
            await context.Teachers.AddRangeAsync(expected);
            await context.SaveChangesAsync();
        }

        GetAllTeachersQuery query = new(fixture.DbContextFactory);

        // Act
        List<Teacher> actual = (await query.ExecuteAsync()).ToList();

        // Assert
        Assert.Equal(
            expected: SetCorrectOrder(expected),
            actual: SetCorrectOrder(actual),
            comparer: new TeacherEqualityComparer());
    }

    private static List<Teacher> SetCorrectOrder(List<Teacher> teacher)
    {
        teacher = teacher.OrderBy(t => t.TeacherId).ToList();
        teacher.ForEach(t => t.Groups = t.Groups.OrderBy(g => g.GroupId).ToList());

        return teacher;
    }
}