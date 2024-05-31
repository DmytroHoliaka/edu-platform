using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class CreateCourseCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        Course? course = null;
        CreateCourseCommand command = new(contextFactory: fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await command.ExecuteAsync(newCourse: course));
    }

    [Fact]
    public async Task ExecuteAsync_ValidCourse_CreatesNewCourseInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Course expected = ModelGenerator.GetPopulatedCourse();
        CreateCourseCommand command = new(fixture.DbContextFactory);

        // Act
        await command.ExecuteAsync(newCourse: expected);

        // Assert
        Assert.Equal(
            expected: expected,
            actual: fixture.DbManager.GetSingleCourseFromDatabase(),
            comparer: new CourseEqualityComparer());
    }
}