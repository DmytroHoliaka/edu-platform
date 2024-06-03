using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Queries;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Queries;

[Collection("DatabaseTests")]
public class GetAllCoursesQueryTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_ValidContext_ReturnsSequenceOCourses()
    {
        // Arrange
        List<Course> expected = ModelGenerator.GetPopulatedCourses();

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await fixture.DbManager.ClearDatabase();
            await context.Courses.AddRangeAsync(expected);
            await context.SaveChangesAsync();
        }

        GetAllCoursesQuery query = new(fixture.DbContextFactory);

        // Act
        List<Course> actual = (await query.ExecuteAsync()).ToList();

        // Assert
        Assert.Equal(
            expected: SetCorrectOrder(expected),
            actual: SetCorrectOrder(actual),
            comparer: new CourseEqualityComparer());
    }

    private static List<Course> SetCorrectOrder(List<Course> courses)
    {
        courses = courses.OrderBy(c => c.CourseId).ToList();
        courses.ForEach(c => c.Groups = c.Groups.OrderBy(g => g.GroupId).ToList());

        return courses;
    }
}