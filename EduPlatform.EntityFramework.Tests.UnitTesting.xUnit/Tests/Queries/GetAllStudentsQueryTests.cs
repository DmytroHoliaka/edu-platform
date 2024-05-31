using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Queries;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Queries;

[Collection("DatabaseTests")]
public class GetAllStudentsQueryTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_ValidContext_ReturnsSequenceOfStudents()
    {
        // Arrange
        List<Student> expected = ModelGenerator.GetPopulatedStudents();

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await fixture.DbManager.ClearDatabase();
            await context.Students.AddRangeAsync(expected);
            await context.SaveChangesAsync();
        }

        GetAllStudentsQuery query = new(fixture.DbContextFactory);

        // Act
        List<Student> actual = (await query.ExecuteAsync()).ToList();

        // Assert
        Assert.Equal(
            expected: SetCorrectOrder(expected),
            actual: SetCorrectOrder(actual),
            comparer: new StudentEqualityComparer());
    }

    private static List<Student> SetCorrectOrder(List<Student> students)
    {
        return students.OrderBy(s => s.StudentId).ToList();
    }
}