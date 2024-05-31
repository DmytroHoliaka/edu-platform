using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class DeleteCourseCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NonExistingGuid_ThrowsInvalidDataException()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Guid nonExistingGuid = Guid.Empty;
        DeleteCourseCommand command = new(contextFactory: fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidDataException>(
            async () => await command.ExecuteAsync(courseId: nonExistingGuid));
    }

    [Fact]
    public async Task ExecuteAsync_ExistingGuid_DeletesCourseFromDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Course course = ModelGenerator.GetUnfilledCourse();
        DeleteCourseCommand command = new(fixture.DbContextFactory);

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();
        }

        // Act
        await command.ExecuteAsync(courseId: course.CourseId);

        // Assert
        Assert.Empty(fixture.DbManager.GetAllCoursesFromDatabase());
    }
}