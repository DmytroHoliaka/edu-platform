using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class UpdateCourseCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NullCourse_ThrowsArgumentNullException()
    {
        // Arrange
        Course? course = null;
        UpdateCourseCommand command = new(fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await command.ExecuteAsync(targetCourse: course));
    }

    [Fact]
    public async Task ExecuteAsync_NonExistingCourse_CreatesNewCourseInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Course newCourse = ModelGenerator.GetUnfilledCourse();
        UpdateCourseCommand command = new(fixture.DbContextFactory);

        // Act
        await command.ExecuteAsync(targetCourse: newCourse);

        // Assert
        Assert.Equal(
            expected: newCourse,
            actual: fixture.DbManager.GetSingleCourseFromDatabase(),
            comparer: new CourseEqualityComparer());
    }

    [Fact]
    public async Task ExecuteAsync_ExistingCourse_UpdatesCourseInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Course course = ModelGenerator.GetUnfilledCourse();
        UpdateCourseCommand command = new(fixture.DbContextFactory);

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();
        }

        course.Name = "Updated course";

        // Act
        await command.ExecuteAsync(targetCourse: course);

        // Assert
        Assert.Equal(
            expected: course,
            actual: fixture.DbManager.GetSingleCourseFromDatabase(),
            comparer: new CourseEqualityComparer());
    }
}